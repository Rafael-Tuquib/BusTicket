using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusTicketing.Data;
using BusTicketing.Models;
using BusTicketing.Repositories.Interfaces;

namespace BusTicketing.Repositories.Implementations
{
    public class MunicipalityRepository : IMunicipalityRepository
    {
        private readonly ApplicationDbContext _db;
        public MunicipalityRepository(ApplicationDbContext db) { _db = db; }

        public async Task<IEnumerable<Municipality>> GetAllAsync()
        {
            return await _db.Municipalities
                .AsNoTracking()
                .Include(m => m.Barangays)
                .ToListAsync();
        }

        public async Task<Municipality?> GetByIdAsync(int id)
        {
            return await _db.Municipalities
                .Include(m => m.Barangays)
                .FirstOrDefaultAsync(m => m.Id == id);
        }
    }
}
