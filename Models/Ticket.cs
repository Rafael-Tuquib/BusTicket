using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusTicketing.Models
{
    public class Ticket
    {
        public int Id { get; set; }

        [Required]
        public int FromBarangayId { get; set; }
        public Barangay FromBarangay { get; set; } = null!;

        [Required]
        public int ToBarangayId { get; set; }
        public Barangay ToBarangay { get; set; } = null!;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Distance must be greater than 0")]
        public double Distance { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Fare must not be negative")]
        public decimal Fare { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }
    }
}
