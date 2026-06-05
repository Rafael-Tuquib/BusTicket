using System;

namespace BusTicketing.DTOs
{
    public class TicketOutputDto
    {
        public int Id { get; set; }
        public string FromMunicipality { get; set; } = string.Empty;
        public string FromBarangay { get; set; } = string.Empty;
        public string ToMunicipality { get; set; } = string.Empty;
        public string ToBarangay { get; set; } = string.Empty;
        public double Distance { get; set; }
        public decimal Fare { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
