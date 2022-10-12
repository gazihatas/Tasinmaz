using System.Net;
using System.Threading.Tasks;
using dotnetWebApi.Enums;
using dotnetWebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;

namespace dotnetWebApi.Services
{
    public class AuthorizationMiddlewareResultHandlerService : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler DefaultHandler = new AuthorizationMiddlewareResultHandler();
        public async Task HandleAsync(RequestDelegate next, HttpContext context, AuthorizationPolicy policy, 
        PolicyAuthorizationResult authorizeResult)
        {
            if(authorizeResult.Challenged)
            {
                 context.Response.StatusCode=(int)HttpStatusCode.OK;                                    //UnAuthorized: Access is Denied due invalid credential.
                await context.Response.WriteAsJsonAsync(new ErrorResponseModel(ResponseCode.UnAuthorize,"Yetkisiz: Geçersiz kimlik bilgisi nedeniyle erişim engellendi."));
                return;
            }

            if(authorizeResult.Forbidden)
            {
                 context.Response.StatusCode=(int)HttpStatusCode.OK;                                  //Permission: You don't permission to access this resource.
                await context.Response.WriteAsJsonAsync(new ErrorResponseModel(ResponseCode.Forbidden,"İzin: Bu kaynağa erişme izniniz yok."));
                return;
            }

            await DefaultHandler.HandleAsync(next, context, policy, authorizeResult);
        }
    }
}