using System.Collections.Generic;
using System.Threading.Tasks;
using BusTicketing.Models;

namespace BusTicketing.Services.Interfaces
{
    public interface ILocationService
    {
        Task<IEnumerable<Municipality>> GetAllMunicipalitiesAsync();
        Task<IEnumerable<Barangay>> GetBarangaysByMunicipalityAsync(int municipalityId);
        Task<Barangay?> GetBarangayByIdAsync(int id);
    }
}
