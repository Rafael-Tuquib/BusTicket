using System.ComponentModel.DataAnnotations;

namespace BusTicketing.DTOs
{
    public class CreateTicketInputDto
    {
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a from municipality")]
        public int FromMunicipalityId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a from barangay")]
        public int FromBarangayId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a to municipality")]
        public int ToMunicipalityId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a to barangay")]
        public int ToBarangayId { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Distance must be greater than 0")]
        public double Distance { get; set; }

        [Required]
        [Range(0.0, double.MaxValue, ErrorMessage = "Fare must not be negative")]
        public decimal Fare { get; set; }
    }
}
