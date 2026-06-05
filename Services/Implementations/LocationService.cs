using System.Collections.Generic;
using System.Threading.Tasks;
using BusTicketing.Models;
using BusTicketing.Repositories.Interfaces;
using BusTicketing.Services.Interfaces;

namespace BusTicketing.Services.Implementations
{
    public class LocationService : ILocationService
    {
        private readonly IMunicipalityRepository _municipalityRepository;
        private readonly IBarangayRepository _barangayRepository;

        public LocationService(IMunicipalityRepository municipalityRepository, IBarangayRepository barangayRepository)
        {
            _municipalityRepository = municipalityRepository;
            _barangayRepository = barangayRepository;
        }

        public Task<IEnumerable<Municipality>> GetAllMunicipalitiesAsync() => _municipalityRepository.GetAllAsync();

        public Task<IEnumerable<Barangay>> GetBarangaysByMunicipalityAsync(int municipalityId) =>
            _barangayRepository.GetByMunicipalityIdAsync(municipalityId);

        public Task<Barangay?> GetBarangayByIdAsync(int id) => _barangayRepository.GetByIdAsync(id);
    }
}
