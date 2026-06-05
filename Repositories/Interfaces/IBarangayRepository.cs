using System.Collections.Generic;
using System.Threading.Tasks;
using BusTicketing.Models;

namespace BusTicketing.Repositories.Interfaces
{
    public interface IBarangayRepository
    {
        Task<IEnumerable<Barangay>> GetByMunicipalityIdAsync(int municipalityId);
        Task<Barangay?> GetByIdAsync(int id);
    }
}
