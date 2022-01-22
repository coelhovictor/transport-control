using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Core.WebUI.Controllers
{
    [Authorize]
    public class ReasonsController : Controller
    {
        private readonly IReasonService _reasonService;
        private readonly IUserProfileService _profileService;

        public ReasonsController(IReasonService reasonService, IUserProfileService profileService)
        {
            _reasonService = reasonService;
            _profileService = profileService;
        }

        public async Task<IActionResult> Index()
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            var reasons = await _reasonService.GetReasonsAsync(profile);
            return View(reasons);
        }

        public IActionResult Create()
        {
            return PartialView("_Create");
        }

        public async Task<IActionResult> Get(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            var reason = await _reasonService.GetByIdAsync(id, profile);
            if (reason == null) return NotFound("Reason not found");

            return Json(reason);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ReasonDTOSet reasonDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            if (reasonDTO == null) return BadRequest("Invalid data");
            if (await _reasonService.GetByNameAsync(reasonDTO.Name, profile) != null) return Conflict("There is already a Reason with that name");

            reasonDTO.Id = Guid.NewGuid().ToString();
            await _reasonService.AddAsync(reasonDTO, profile);

            return Json(reasonDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, ReasonDTOSet reasonDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            if (reasonDTO == null) return BadRequest("Invalid data");
            reasonDTO.Id = id;

            await _reasonService.UpdateAsync(reasonDTO, profile);
            return Json(reasonDTO);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            var reasonDTO = await _reasonService.GetByIdAsync(id, profile);
            if (reasonDTO == null) return NotFound("Reason not found");

            await _reasonService.RemoveAsync(id, profile);
            return Json(reasonDTO);
        }
    }
}
