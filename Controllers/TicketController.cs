using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using BusTicketing.DTOs;
using BusTicketing.Services.Interfaces;

namespace BusTicketing.Controllers
{
    public class TicketController : Controller
    {
        private readonly ILocationService _locationService;
        private readonly ITicketService _ticketService;

        public TicketController(ILocationService locationService, ITicketService ticketService)
        {
            _locationService = locationService;
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                var municipalities = await _locationService.GetAllMunicipalitiesAsync();
                ViewBag.Municipalities = municipalities.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList();
                return View(new CreateTicketInputDto());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Unable to load form: {ex.Message}");
                return View(new CreateTicketInputDto());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTicketInputDto dto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var municipalities = await _locationService.GetAllMunicipalitiesAsync();
                    ViewBag.Municipalities = municipalities.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList();
                    // Preload barangays for selected municipalities to preserve selections
                    ViewBag.FromBarangays = dto.FromMunicipalityId > 0 ? (await _locationService.GetBarangaysByMunicipalityAsync(dto.FromMunicipalityId)).Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList() : null;
                    ViewBag.ToBarangays = dto.ToMunicipalityId > 0 ? (await _locationService.GetBarangaysByMunicipalityAsync(dto.ToMunicipalityId)).Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList() : null;
                    return View(dto);
                }

                var ticketId = await _ticketService.CreateTicketAsync(dto);
                return RedirectToAction(nameof(Receipt), new { id = ticketId });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Could not create ticket: {ex.Message}");
                var municipalities = await _locationService.GetAllMunicipalitiesAsync();
                ViewBag.Municipalities = municipalities.Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }).ToList();
                ViewBag.FromBarangays = dto.FromMunicipalityId > 0 ? (await _locationService.GetBarangaysByMunicipalityAsync(dto.FromMunicipalityId)).Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList() : null;
                ViewBag.ToBarangays = dto.ToMunicipalityId > 0 ? (await _locationService.GetBarangaysByMunicipalityAsync(dto.ToMunicipalityId)).Select(b => new SelectListItem { Value = b.Id.ToString(), Text = b.Name }).ToList() : null;
                return View(dto);
            }
        }

        [HttpPost]
        public IActionResult ComputeFare([FromForm] double distance)
        {
            try
            {
                if (distance <= 0) return BadRequest(new { error = "Distance must be greater than 0." });
                var fare = _ticketService.ComputeFare(distance);
                return Json(new { recommendedFare = fare });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> EstimateRoute([FromForm] int fromBarangayId, [FromForm] int toBarangayId)
        {
            try
            {
                if (fromBarangayId <= 0 || toBarangayId <= 0)
                    return BadRequest(new { error = "Both start and end barangays must be selected." });

                var distance = await _ticketService.EstimateDistanceAsync(fromBarangayId, toBarangayId);
                var fare = _ticketService.ComputeFare(distance);

                return Json(new { distance, fare });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Receipt(int id)
        {
            try
            {
                var ticket = await _ticketService.GetTicketAsync(id);
                if (ticket == null) return NotFound();
                return View(ticket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error loading ticket: {ex.Message}");
            }
        }
    }
}