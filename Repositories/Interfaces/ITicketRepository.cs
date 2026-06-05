using System.Threading.Tasks;
using BusTicketing.Models;

namespace BusTicketing.Repositories.Interfaces
{
    public interface ITicketRepository
    {
        Task<Ticket> AddAsync(Ticket ticket);
        Task<Ticket?> GetByIdAsync(int id);
    }
}
