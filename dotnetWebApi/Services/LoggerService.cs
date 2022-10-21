using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;
using dotnetWebApi.Data;
using dotnetWebApi.IServices;
using Microsoft.EntityFrameworkCore;

namespace dotnetWebApi.Services
{
    public class LoggerService : ILoggerService
    {
         private readonly AppDBContext _context;
        public LoggerService(AppDBContext context)
        {
            _context = context;
        }

        public async Task Add(Log log)
        {
            _context.Logs.Add(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Log>> GetAll()
        {
            return await _context.Logs.ToListAsync();
        }
    }
}