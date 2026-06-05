using System.Collections.Generic;
using System.Threading.Tasks;
using BusTicketing.Models;

namespace BusTicketing.Repositories.Interfaces
{
    public interface IMunicipalityRepository
    {
        Task<IEnumerable<Municipality>> GetAllAsync();
        Task<Municipality?> GetByIdAsync(int id);
    }
}
