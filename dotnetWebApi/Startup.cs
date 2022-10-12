using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using dotnetWebApi.Data;
using dotnetWebApi.Data.Entities;
using dotnetWebApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;

namespace dotnetWebApi
{
    public class Startup
    {
        //1 Cors
        private readonly string _loginOrigin="_localorigin";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //JWT2
            services.Configure<JWTConfig>(Configuration.GetSection("JWTConfig"));

            //1
            services.AddDbContext<AppDBContext>(opt=>{
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            //2
            services.AddIdentity<AppUser, IdentityRole>(opt=>{}).AddEntityFrameworkStores<AppDBContext>();

            //jWT1
            services.AddAuthentication(x=>{
                x.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options=>{
                var key = Encoding.ASCII.GetBytes(Configuration["JWTConfig:Key"]);
                var issuer=Configuration["JWTConfig:Issuer"];
                var audience=Configuration["JWTConfig:Audience"];

                options.TokenValidationParameters = new TokenValidationParameters(){
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    RequireExpirationTime=true,
                    ValidIssuer=issuer,
                    ValidAudience=audience,
                };
            });

            //2 CORS
              services.AddCors(opt =>{
                opt.AddPolicy(_loginOrigin, builder =>{
                    builder.AllowAnyOrigin();
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                });
            });
            
            //Özel Yanıt için Yetkilendirme Ara Yazılımının davranışını özelleştirme
            services.AddSingleton<IAuthorizationMiddlewareResultHandler,AuthorizationMiddlewareResultHandlerService>();
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "dotnetWebApi", Version = "v1" });
                c.SwaggerDoc("v1", new OpenApiInfo { Title = ".NET CORE 5 | TASINMAZ API", Version = "v1", Description="Tasinmaz uygulaması API'sine hoşgeldin. Powered by Gazi."});
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description ="Lütfen Token girin.",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat="JWT",
                    Scheme = "bearer",
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "dotnetWebApi v1"));
            }

            //Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c=>{
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TASINMAZ API");
            });


            app.UseHttpsRedirection();

            //3 CORS
            app.UseCors(_loginOrigin);

            app.UseRouting();
            //jwt4
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
