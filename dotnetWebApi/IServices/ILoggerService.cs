using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace dotnetWebApi.IServices
{
    public interface ILoggerService
    {
        Task<IEnumerable<Log>> GetAll();
        Task Add(Log log);
    }
    
}