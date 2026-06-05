using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusTicketing.DTOs;
using BusTicketing.Models;
using BusTicketing.Repositories.Interfaces;
using BusTicketing.Services.Interfaces;

namespace BusTicketing.Services.Implementations
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IBarangayRepository _barangayRepository;

        private const decimal BaseFare = 12m;
        private const double BaseDistance = 5.0;
        private const decimal PerKm = 2.20m;

        public TicketService(ITicketRepository ticketRepository, IBarangayRepository barangayRepository)
        {
            _ticketRepository = ticketRepository;
            _barangayRepository = barangayRepository;
        }

        public decimal ComputeFare(double distance)
        {
            if (distance <= BaseDistance) return BaseFare;
            var extra = Math.Max(0.0, distance - BaseDistance);
            var fare = BaseFare + (decimal)extra * PerKm;
            return Math.Round(fare, 2);
        }

        private static readonly Dictionary<(string from, string to), double> RouteDistances = new()
        {
            [("tagbilaran city", "dauis")] = 3.0,
            [("tagbilaran city", "baclayon")] = 6.0,
            [("tagbilaran city", "corella")] = 8.0,
            [("tagbilaran city", "cortes")] = 6.0,
            [("tagbilaran city", "panglao")] = 18.0,
            [("tagbilaran city", "alburquerque")] = 10.0,
            [("tagbilaran city", "loay")] = 19.0,
            [("tagbilaran city", "maribojoc")] = 14.0,
            [("tagbilaran city", "sikatuna")] = 17.0,
            [("tagbilaran city", "loboc")] = 24.0,
            [("tagbilaran city", "antequera")] = 19.0,
            [("tagbilaran city", "balilihan")] = 22.0,
            [("tagbilaran city", "lila")] = 36.0,
            [("tagbilaran city", "dimiao")] = 32.0,
            [("tagbilaran city", "valencia")] = 42.0,
            [("tagbilaran city", "sevilla")] = 36.0,
            [("tagbilaran city", "bilar")] = 41.0,
            [("tagbilaran city", "carmen")] = 52.0,
            [("tagbilaran city", "batuan")] = 47.0,
            [("tagbilaran city", "catigbian")] = 33.0,
            [("tagbilaran city", "sagbayan")] = 57.0,
            [("tagbilaran city", "tubigon")] = 54.0,
            [("tagbilaran city", "calape")] = 43.0,
            [("tagbilaran city", "loon")] = 30.0,
            [("tagbilaran city", "clarin")] = 63.0,
            [("tagbilaran city", "inabanga")] = 72.0,
            [("tagbilaran city", "buenavista")] = 83.0,
            [("tagbilaran city", "getafe")] = 92.0,
            [("tagbilaran city", "talibon")] = 109.0,
            [("tagbilaran city", "trinidad")] = 91.0,
            [("tagbilaran city", "san isidro")] = 78.0,
            [("tagbilaran city", "danao")] = 83.0,
            [("tagbilaran city", "dagohoy")] = 92.0,
            [("tagbilaran city", "pilar")] = 78.0,
            [("tagbilaran city", "sierra bullones")] = 64.0,
            [("tagbilaran city", "alicia")] = 95.0,
            [("tagbilaran city", "candijay")] = 92.0,
            [("tagbilaran city", "anda")] = 101.0,
            [("tagbilaran city", "guindulman")] = 69.0,
            [("tagbilaran city", "duero")] = 55.0,
            [("tagbilaran city", "jagna")] = 63.0,
            [("tagbilaran city", "garcia hernandez")] = 52.0,
            [("tagbilaran city", "mabini")] = 73.0,
            [("tagbilaran city", "ubay")] = 117.0,
            [("tagbilaran city", "san miguel")] = 95.0,
            [("tagbilaran city", "president carlos p. garcia")] = 137.0,
            [("tagbilaran city", "bien unido")] = 127.0,
            [("tubigon", "talibon")] = 60.0,
            [("tubigon", "ubay")] = 85.0,
            [("jagna", "candijay")] = 30.0,
            [("jagna", "ubay")] = 60.0,
            [("ubay", "bien unido")] = 25.0
        };

       public async Task<double> EstimateDistanceAsync(int fromBarangayId, int toBarangayId)
{
    var from = await _barangayRepository.GetByIdAsync(fromBarangayId);
    var to = await _barangayRepository.GetByIdAsync(toBarangayId);

    if (from == null || to == null)
        throw new ArgumentException("Invalid barangay selection.");

    // Same barangay
    if (from.Id == to.Id)
        return 1.0;

    var fromName = NormalizeMunicipalityName(
        from.Municipality?.Name
    ).ToLowerInvariant();

    var toName = NormalizeMunicipalityName(
        to.Municipality?.Name
    ).ToLowerInvariant();

    // Same municipality
    if (from.MunicipalityId == to.MunicipalityId)
    {
        var distance = 3.0 + Math.Abs(from.Id - to.Id) * 0.4;
        return Math.Round(distance, 2);
    }

    // Direct route exists in dictionary
    if (TryLookupDistance(fromName, toName, out var routeDistance))
    {
        return routeDistance;
    }

    // Get municipality distances from Tagbilaran
    double? fromKm = null;
    double? toKm = null;

    if (fromName == "tagbilaran city")
    {
        fromKm = 0;
    }
    else if (TryLookupDistance("tagbilaran city", fromName, out var fKm))
    {
        fromKm = fKm;
    }

    if (toName == "tagbilaran city")
    {
        toKm = 0;
    }
    else if (TryLookupDistance("tagbilaran city", toName, out var tKm))
    {
        toKm = tKm;
    }

    // Compute distance using kilometer difference
    if (fromKm.HasValue && toKm.HasValue)
    {
        double distance = Math.Abs(
            fromKm.Value - toKm.Value
        );

        // Small barangay adjustment
        distance += Math.Abs(from.Id - to.Id) * 0.10;

        return Math.Round(
            Math.Max(distance, 1.0),
            2
        );
    }

    // Fallback estimate if municipality not found
    var municipalityDelta = Math.Abs(
        from.MunicipalityId - to.MunicipalityId
    );

    var barangayDelta = Math.Abs(
        from.Id - to.Id
    );

    var estimatedDistance =
        8.0 +
        municipalityDelta * 1.5 +
        barangayDelta * 0.12;

    return Math.Round(estimatedDistance, 2);
}
        private static bool TryLookupDistance(string fromName, string toName, out double distance)
        {
            if (RouteDistances.TryGetValue((fromName, toName), out distance) ||
                RouteDistances.TryGetValue((toName, fromName), out distance))
            {
                return true;
            }

            distance = 0;
            return false;
        }

        private static string NormalizeMunicipalityName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return string.Empty;

            var normalized = name.Trim().ToLowerInvariant();
            return normalized switch
            {
                "tagbilaran" => "tagbilaran city",
                "pres. carlos p. garcia" => "president carlos p. garcia",
                _ => normalized
            };
        }

        public async Task<int> CreateTicketAsync(CreateTicketInputDto dto)
        {
            // Basic validation at service layer (repositories confirm existence)
            var from = await _barangayRepository.GetByIdAsync(dto.FromBarangayId);
            var to = await _barangayRepository.GetByIdAsync(dto.ToBarangayId);
            if (from == null || to == null) throw new ArgumentException("Invalid barangay selection.");

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

        public async Task<TicketOutputDto?> GetTicketAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null) return null;

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
