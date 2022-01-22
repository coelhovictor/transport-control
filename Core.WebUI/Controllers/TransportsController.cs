using Core.Application.DTOs;
using Core.Application.Interfaces;
using Core.WebUI.ViewModels.Transports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.WebUI.Controllers
{
    [Authorize]
    public class TransportsController : Controller
    {
        private readonly ITransportService _transportService;
        private readonly ITransportTypeService _typeService;
        private readonly ITagService _tagService;
        private readonly IReasonService _reasonService;
        private readonly IUserProfileService _profileService;

        public TransportsController(ITransportService transportService, ITransportTypeService typeService,
            ITagService tagService, IReasonService reasonService, IUserProfileService profileService)
        {
            _transportService = transportService;
            _typeService = typeService;
            _tagService = tagService;
            _reasonService = reasonService;
            _profileService = profileService;
        }

        public async Task<IActionResult> Index(DateTime? start, DateTime? end)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            ViewBag.StartDate = start.HasValue ? start.Value.ToString("dd/MM/yyyy 00:00") : "";
            ViewBag.EndDate = end.HasValue ? end.Value.ToString("dd/MM/yyyy 23:59") : "";

            var types = await _typeService.GetTransportTypesAsync(profile);
            ViewBag.Types = types;

            var tags = await _tagService.GetTagsAsync(profile);
            ViewBag.Tags = tags;

            var reasons = await _reasonService.GetReasonsAsync(profile);
            ViewBag.Reasons = reasons;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(TimelineViewModel model)
        {
            if (!ModelState.IsValid) 
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            var transports = await _transportService.GetTransportsByRangeAsync(model.StartDate, model.EndDate, profile);
            if (!transports.Any()) return PartialView("_EmptyTimeline");
            return PartialView("_Timeline", transports);
        }

        public async Task<IActionResult> Get(string id)
        {
            if (!ModelState.IsValid) 
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            var transport = await _transportService.GetByIdAsync(id, profile);
            if (transport == null) return NotFound("Transport not found");

            return Json(transport);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransportDTOSet transportDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            if (transportDTO == null) return BadRequest("Invalid data");

            transportDTO.Id = Guid.NewGuid().ToString();
            await _transportService.AddAsync(transportDTO, profile);

            return Json(transportDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TransportDTOSet transportDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            if (transportDTO == null) return BadRequest("Invalid data");
            transportDTO.Id = id;

            await _transportService.UpdateAsync(transportDTO, profile);
            return Json(transportDTO);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            var transportDTO = await _transportService.GetByIdAsync(id, profile);
            if (transportDTO == null) return NotFound("Transport not found");

            await _transportService.RemoveAsync(id, profile);
            return Json(transportDTO);
        }
    }
}
