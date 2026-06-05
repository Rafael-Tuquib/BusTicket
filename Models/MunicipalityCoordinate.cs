namespace BusTicketing.Models
{
    /// <summary>
    /// Represents geographic coordinates (latitude/longitude) for a municipality in Bohol.
    /// Used for accurate distance calculations between any two locations using the Haversine formula.
    /// </summary>
    public class MunicipalityCoordinate
    {
        /// <summary>
        /// Normalized municipality name (matching Municipality.Name)
        /// </summary>
        public string MunicipalityName { get; set; } = string.Empty;

        /// <summary>
        /// Geographic center latitude (decimal degrees)
        /// Range: -90 to +90 (S to N)
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Geographic center longitude (decimal degrees)
        /// Range: -180 to +180 (W to E)
        /// </summary>
        public double Longitude { get; set; }
    }
}
