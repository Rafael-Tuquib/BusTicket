using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusTicketing.Data;
using BusTicketing.Models;
using BusTicketing.Repositories.Interfaces;

namespace BusTicketing.Repositories.Implementations
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _db;
        public TicketRepository(ApplicationDbContext db) { _db = db; }

        public async Task<Ticket> AddAsync(Ticket ticket)
        {
            _db.Tickets.Add(ticket);
            await _db.SaveChangesAsync();
            // ensure navigation props loaded if needed
            await _db.Entry(ticket).Reference(t => t.FromBarangay).LoadAsync();
            await _db.Entry(ticket).Reference(t => t.ToBarangay).LoadAsync();
            return ticket;
        }

        public async Task<Ticket?> GetByIdAsync(int id)
        {
            return await _db.Tickets
                .AsNoTracking()
                .Include(t => t.FromBarangay)
                    .ThenInclude(b => b.Municipality)
                .Include(t => t.ToBarangay)
                    .ThenInclude(b => b.Municipality)
                .FirstOrDefaultAsync(t => t.Id == id);
        }
    }
}
