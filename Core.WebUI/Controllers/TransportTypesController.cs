using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Core.WebUI.Controllers
{
    [Authorize]
    public class TransportTypesController : Controller
    {
        private readonly ITransportTypeService _typeService;
        private readonly IUserProfileService _profileService;

        public TransportTypesController(ITransportTypeService typeService, IUserProfileService profileService)
        {
            _typeService = typeService;
            _profileService = profileService;
        }

        public async Task<IActionResult> Index()
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            var types = await _typeService.GetTransportTypesAsync(profile);
            return View(types);
        }

        public async Task<IActionResult> Get(string id)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid data");

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            var type = await _typeService.GetByIdAsync(id, profile);
            if (type == null) return NotFound("Transport Type not found");

            return Json(type);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TransportTypeDTOSet typeDTO)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid data");

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            if (typeDTO == null) return BadRequest("Invalid data");
            if (await _typeService.GetByNameAsync(typeDTO.Name, profile) != null) return Conflict("There is already a Transport Type with that name");

            typeDTO.Id = Guid.NewGuid().ToString();
            await _typeService.AddAsync(typeDTO, profile);

            return Json(typeDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TransportTypeDTOSet typeDTO)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid data");

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            if (typeDTO == null) return BadRequest("Invalid data");
            typeDTO.Id = id;

            await _typeService.UpdateAsync(typeDTO, profile);
            return Json(typeDTO);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid data");

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            var typeDTO = await _typeService.GetByIdAsync(id, profile);
            if (typeDTO == null) return NotFound("Transport Type not found");

            await _typeService.RemoveAsync(id, profile);
            return Json(typeDTO);
        }
    }
}
