using System.Threading.Tasks;
using BusTicketing.DTOs;

namespace BusTicketing.Services.Interfaces
{
    public interface ITicketService
    {
        decimal ComputeFare(double distance);
        Task<double> EstimateDistanceAsync(int fromBarangayId, int toBarangayId);
        Task<int> CreateTicketAsync(CreateTicketInputDto dto);
        Task<TicketOutputDto?> GetTicketAsync(int id);
    }
}
