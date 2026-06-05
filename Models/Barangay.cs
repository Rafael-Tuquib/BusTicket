using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusTicketing.Models
{
    public class Barangay
    {
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Municipality))]
        public int MunicipalityId { get; set; }
        public Municipality Municipality { get; set; } = null!;

        [Required]
        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;
    }
}
