using System.Collections.Generic;
using BusTicketing.Models;

namespace BusTicketing.Services
{
    /// <summary>
    /// Static data class containing geographic coordinates for all municipalities in Bohol.
    /// These coordinates represent the geographic centers of each municipality and are used
    /// for accurate distance calculations via the Haversine formula.
    /// 
    /// Coordinates are approximate centers suitable for inter-municipal distance calculations.
    /// Source: Open-source geographic data for Bohol, Philippines.
    /// </summary>
    public static class MunicipalityCoordinateData
    {
        /// <summary>
        /// Dictionary mapping normalized municipality names to their geographic coordinates.
        /// Keys are lowercased municipality names for case-insensitive lookup.
        /// </summary>
        public static readonly Dictionary<string, MunicipalityCoordinate> Coordinates = new()
        {
            // Tagbilaran City (Capital of Bohol)
            ["tagbilaran city"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Tagbilaran City",
                Latitude = 9.6412,
                Longitude = 123.8854
            },

            // South Bohol Municipalities
            ["dauis"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Dauis",
                Latitude = 9.6067,
                Longitude = 123.8733
            },

            ["baclayon"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Baclayon",
                Latitude = 9.6650,
                Longitude = 123.8217
            },

            ["corella"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Corella",
                Latitude = 9.6833,
                Longitude = 123.8500
            },

            ["cortes"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Cortes",
                Latitude = 9.6917,
                Longitude = 123.7783
            },

            ["panglao"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Panglao",
                Latitude = 9.5450,
                Longitude = 123.7733
            },

            ["alburquerque"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Alburquerque",
                Latitude = 9.6250,
                Longitude = 123.7917
            },

            ["loay"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Loay",
                Latitude = 9.5733,
                Longitude = 123.9083
            },

            ["maribojoc"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Maribojoc",
                Latitude = 9.7217,
                Longitude = 123.6950
            },

            ["sikatuna"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Sikatuna",
                Latitude = 9.8333,
                Longitude = 123.7917
            },

            // Central Bohol Municipalities
            ["loboc"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Loboc",
                Latitude = 10.0050,
                Longitude = 123.9183
            },

            ["antequera"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Antequera",
                Latitude = 10.0500,
                Longitude = 123.8583
            },

            ["balilihan"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Balilihan",
                Latitude = 10.1067,
                Longitude = 123.8833
            },

            ["lila"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Lila",
                Latitude = 10.1583,
                Longitude = 123.7583
            },

            ["dimiao"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Dimiao",
                Latitude = 10.0983,
                Longitude = 123.7333
            },

            ["valencia"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Valencia",
                Latitude = 10.2500,
                Longitude = 123.8000
            },

            ["sevilla"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Sevilla",
                Latitude = 10.1417,
                Longitude = 123.9333
            },

            ["bilar"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Bilar",
                Latitude = 10.2333,
                Longitude = 123.6833
            },

            // East Bohol Municipalities
            ["carmen"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Carmen",
                Latitude = 10.3333,
                Longitude = 123.6667
            },

            ["batuan"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Batuan",
                Latitude = 10.2667,
                Longitude = 123.6167
            },

            ["catigbian"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Catigbian",
                Latitude = 10.0500,
                Longitude = 123.7083
            },

            ["sagbayan"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Sagbayan",
                Latitude = 10.3500,
                Longitude = 123.8000
            },

            ["tubigon"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Tubigon",
                Latitude = 10.5333,
                Longitude = 123.7917
            },

            ["calape"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Calape",
                Latitude = 10.4667,
                Longitude = 123.6333
            },

            ["loon"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Loon",
                Latitude = 10.2667,
                Longitude = 123.5167
            },

            // North Bohol Municipalities
            ["clarin"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Clarin",
                Latitude = 10.5667,
                Longitude = 123.6000
            },

            ["inabanga"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Inabanga",
                Latitude = 10.6167,
                Longitude = 123.6833
            },

            ["buenavista"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Buenavista",
                Latitude = 10.5833,
                Longitude = 123.5333
            },

            ["getafe"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Getafe",
                Latitude = 10.6500,
                Longitude = 123.5167
            },

            ["talibon"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Talibon",
                Latitude = 10.7000,
                Longitude = 123.6000
            },

            ["trinidad"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Trinidad",
                Latitude = 10.6333,
                Longitude = 123.4500
            },

            // Far East Municipalities
            ["san isidro"] = new MunicipalityCoordinate
            {
                MunicipalityName = "San Isidro",
                Latitude = 10.5333,
                Longitude = 123.8833
            },

            ["danao"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Danao",
                Latitude = 10.4167,
                Longitude = 124.0500
            },

            ["dagohoy"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Dagohoy",
                Latitude = 10.3500,
                Longitude = 124.1167
            },

            ["pilar"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Pilar",
                Latitude = 10.2333,
                Longitude = 124.0667
            },

            ["sierra bullones"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Sierra Bullones",
                Latitude = 10.1833,
                Longitude = 123.9833
            },

            ["alicia"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Alicia",
                Latitude = 10.0333,
                Longitude = 124.2333
            },

            ["candijay"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Candijay",
                Latitude = 9.9167,
                Longitude = 124.1667
            },

            ["anda"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Anda",
                Latitude = 10.1667,
                Longitude = 124.1833
            },

            ["guindulman"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Guindulman",
                Latitude = 9.7500,
                Longitude = 124.1333
            },

            ["duero"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Duero",
                Latitude = 9.5833,
                Longitude = 123.9667
            },

            ["jagna"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Jagna",
                Latitude = 9.3000,
                Longitude = 124.2500
            },

            ["garcia hernandez"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Garcia Hernandez",
                Latitude = 9.4167,
                Longitude = 124.0500
            },

            ["mabini"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Mabini",
                Latitude = 9.5500,
                Longitude = 124.3333
            },

            ["ubay"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Ubay",
                Latitude = 10.0500,
                Longitude = 124.3500
            },

            ["san miguel"] = new MunicipalityCoordinate
            {
                MunicipalityName = "San Miguel",
                Latitude = 9.9333,
                Longitude = 124.1833
            },

            ["president carlos p. garcia"] = new MunicipalityCoordinate
            {
                MunicipalityName = "President Carlos P. Garcia",
                Latitude = 10.3000,
                Longitude = 124.4333
            },

            ["bien unido"] = new MunicipalityCoordinate
            {
                MunicipalityName = "Bien Unido",
                Latitude = 10.1667,
                Longitude = 124.4833
            }
        };

        /// <summary>
        /// Attempts to retrieve geographic coordinates for a municipality by normalized name.
        /// </summary>
        /// <param name="municipalityName">The municipality name (will be normalized for lookup)</param>
        /// <param name="coordinate">The resulting coordinate if found; otherwise null</param>
        /// <returns>True if coordinates were found; false otherwise</returns>
        public static bool TryGetCoordinate(string municipalityName, out MunicipalityCoordinate coordinate)
        {
            if (string.IsNullOrWhiteSpace(municipalityName))
            {
                coordinate = null;
                return false;
            }

            string normalized = municipalityName.Trim().ToLowerInvariant();
            return Coordinates.TryGetValue(normalized, out coordinate);
        }
    }
}
