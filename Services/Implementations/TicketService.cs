using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusTicketing.DTOs;
using BusTicketing.Models;
using BusTicketing.Repositories.Interfaces;
using BusTicketing.Services.Interfaces;

namespace BusTicketing.Services.Implementations
{
    /// <summary>
    /// Service for managing ticket operations including distance estimation and fare computation.
    /// 
    /// KEY FIX: Distance is now calculated DIRECTLY between the selected origin and destination,
    /// NOT from Tagbilaran as an intermediary. The system now supports accurate routes between
    /// any two municipalities/barangays in Bohol.
    /// </summary>
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IBarangayRepository _barangayRepository;
        private readonly IMunicipalityRepository _municipalityRepository;

        private const decimal BaseFare = 12m;
        private const double BaseDistance = 5.0;
        private const decimal PerKm = 2.20m;

        public TicketService(
            ITicketRepository ticketRepository,
            IBarangayRepository barangayRepository,
            IMunicipalityRepository municipalityRepository)
        {
            _ticketRepository = ticketRepository;
            _barangayRepository = barangayRepository;
            _municipalityRepository = municipalityRepository;
        }

        /// <summary>
        /// Computes fare based on distance traveled.
        /// 
        /// Fare Structure:
        /// - Base Fare: ₱12.00 (covers up to 5 km)
        /// - Additional: ₱2.20 per km beyond base distance
        /// 
        /// Formula: Fare = 12 + max(0, distance - 5) * 2.20
        /// </summary>
        /// <param name="distance">Distance in kilometers</param>
        /// <returns>Fare amount in Philippine Pesos, rounded to 2 decimal places</returns>
        public decimal ComputeFare(double distance)
        {
            if (distance <= BaseDistance)
                return BaseFare;

            var extra = Math.Max(0.0, distance - BaseDistance);
            var fare = BaseFare + (decimal)extra * PerKm;
            return Math.Round(fare, 2);
        }

        /// <summary>
        /// Estimates the distance between two barangays using geographic coordinates.
        /// 
        /// IMPROVED ALGORITHM:
        /// 1. If same barangay: return 1.0 km (minimum)
        /// 2. If same municipality: calculate based on barangay offset
        /// 3. Use Haversine formula with municipality coordinates for accurate geographic distance
        /// 4. Apply barangay-level adjustments within the same municipality
        /// 5. Fallback estimate if coordinates unavailable
        /// 
        /// IMPORTANT: This now calculates distance BETWEEN the two locations,
        /// not from each location back to Tagbilaran. This fixes the original bug.
        /// </summary>
        /// <param name="fromBarangayId">Starting barangay ID</param>
        /// <param name="toBarangayId">Destination barangay ID</param>
        /// <returns>Distance in kilometers</returns>
        /// <exception cref="ArgumentException">Thrown if barangays are not found</exception>
        public async Task<double> EstimateDistanceAsync(int fromBarangayId, int toBarangayId)
        {
            // Retrieve barangay entities with municipality data
            var from = await _barangayRepository.GetByIdAsync(fromBarangayId);
            var to = await _barangayRepository.GetByIdAsync(toBarangayId);

            if (from?.Municipality == null || to?.Municipality == null)
                throw new ArgumentException("Invalid barangay selection.");

            // Same barangay: return minimum distance
            if (from.Id == to.Id)
                return 1.0;

            // Same municipality: return distance based on barangay spread
            if (from.MunicipalityId == to.MunicipalityId)
            {
                // Base intra-municipal distance plus barangay offset
                var distance = 3.0 + Math.Abs(from.Id - to.Id) * 0.4;
                return Math.Round(distance, 2);
            }

            // MAIN FIX: Calculate distance directly between origin and destination municipalities
            // using geographic coordinates and the Haversine formula
            return await CalculateDistanceBetweenMunicipalitiesAsync(from.Municipality, to.Municipality);
        }

        /// <summary>
        /// Calculates distance between two municipalities using geographic coordinates.
        /// 
        /// Strategy:
        /// 1. Try to use Haversine formula if both municipalities have coordinates in DB
        /// 2. Fall back to static coordinate data if DB coordinates unavailable
        /// 3. Use ID-based estimation as last resort
        /// </summary>
        /// <param name="fromMunicipality">Origin municipality</param>
        /// <param name="toMunicipality">Destination municipality</param>
        /// <returns>Distance in kilometers</returns>
        private async Task<double> CalculateDistanceBetweenMunicipalitiesAsync(
            Municipality fromMunicipality,
            Municipality toMunicipality)
        {
            double? fromLat = null, fromLon = null;
            double? toLat = null, toLon = null;

            // Try to get coordinates from database first
            if (fromMunicipality.Latitude.HasValue && fromMunicipality.Longitude.HasValue)
            {
                fromLat = fromMunicipality.Latitude;
                fromLon = fromMunicipality.Longitude;
            }

            if (toMunicipality.Latitude.HasValue && toMunicipality.Longitude.HasValue)
            {
                toLat = toMunicipality.Latitude;
                toLon = toMunicipality.Longitude;
            }

            // Fall back to static coordinate data if not in DB
            if (!fromLat.HasValue || !fromLon.HasValue)
            {
                if (MunicipalityCoordinateData.TryGetCoordinate(fromMunicipality.Name, out var fromCoord))
                {
                    fromLat = fromCoord.Latitude;
                    fromLon = fromCoord.Longitude;
                }
            }

            if (!toLat.HasValue || !toLon.HasValue)
            {
                if (MunicipalityCoordinateData.TryGetCoordinate(toMunicipality.Name, out var toCoord))
                {
                    toLat = toCoord.Latitude;
                    toLon = toCoord.Longitude;
                }
            }

            // Use Haversine formula if we have coordinates
            if (fromLat.HasValue && fromLon.HasValue && toLat.HasValue && toLon.HasValue)
            {
                double distance = DistanceCalculator.CalculateHaversineDistance(
                    fromLat.Value,
                    fromLon.Value,
                    toLat.Value,
                    toLon.Value
                );

                // Ensure minimum distance
                return Math.Max(distance, 1.0);
            }

            // Fallback: Use municipality ID difference as estimation
            var municipalityDelta = Math.Abs(fromMunicipality.Id - toMunicipality.Id);
            var estimatedDistance = 8.0 + municipalityDelta * 1.5;
            return Math.Round(estimatedDistance, 2);
        }

        /// <summary>
        /// Creates a new ticket record with the provided information.
        /// </summary>
        /// <param name="dto">Ticket creation DTO containing barangay selections, distance, and fare</param>
        /// <returns>The ID of the newly created ticket</returns>
        /// <exception cref="ArgumentException">Thrown if barangays are invalid</exception>
        public async Task<int> CreateTicketAsync(CreateTicketInputDto dto)
        {
            // Validate that barangays exist
            var from = await _barangayRepository.GetByIdAsync(dto.FromBarangayId);
            var to = await _barangayRepository.GetByIdAsync(dto.ToBarangayId);

            if (from == null || to == null)
                throw new ArgumentException("Invalid barangay selection.");

            // Create ticket record
            var ticket = new Ticket
            {
                FromBarangayId = dto.FromBarangayId,
                ToBarangayId = dto.ToBarangayId,
                Distance = dto.Distance,
                Fare = dto.Fare,
                DateCreated = DateTime.Now
            };

            var created = await _ticketRepository.AddAsync(ticket);
            return created.Id;
        }

        /// <summary>
        /// Retrieves a ticket by ID and maps it to the output DTO.
        /// </summary>
        /// <param name="id">Ticket ID</param>
        /// <returns>Ticket DTO if found; null otherwise</returns>
        public async Task<TicketOutputDto?> GetTicketAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                return null;

            return new TicketOutputDto
            {
                Id = ticket.Id,
                FromBarangay = ticket.FromBarangay?.Name ?? string.Empty,
                FromMunicipality = ticket.FromBarangay?.Municipality?.Name ?? string.Empty,
                ToBarangay = ticket.ToBarangay?.Name ?? string.Empty,
                ToMunicipality = ticket.ToBarangay?.Municipality?.Name ?? string.Empty,
                Distance = ticket.Distance,
                Fare = ticket.Fare,
                DateCreated = ticket.DateCreated
            };
        }
    }
}
