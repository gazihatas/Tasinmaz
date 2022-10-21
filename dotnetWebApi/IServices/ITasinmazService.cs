using System.Collections.Generic;
using System.Threading.Tasks;
using Data.Entities;

namespace IServices
{
    public interface ITasinmazService
    {
        Task<IEnumerable<Tasinmaz>> GetAll();
        Task<IEnumerable<Il>> GetAllCities();
        Task<IEnumerable<Ilce>> GetAllDistricts(int id);
        Task<IEnumerable<Mahalle>> GetAllNeighbourhood(int id);
        Task Add(Tasinmaz tasinmazRegister);
        Task Delete(int id);
        Task<Tasinmaz> Get(int id);
        Task Update(Tasinmaz tasinmazRegister);
    }
}