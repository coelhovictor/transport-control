using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Core.WebUI.Controllers
{

    [Authorize]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;
        private readonly IUserProfileService _profileService;

        public TagsController(ITagService tagService, IUserProfileService profileService)
        {
            _tagService = tagService;
            _profileService = profileService;
        }

        public async Task<IActionResult> Index()
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            var tags = await _tagService.GetTagsAsync(profile);
            return View(tags);
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

            var tag = await _tagService.GetByIdAsync(id, profile);
            if (tag == null) return NotFound("Tag not found");

            return Json(tag);
        }

        [HttpPost]
        public async Task<IActionResult> Create(TagDTOSet tagDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            if (tagDTO == null) return BadRequest("Invalid data");
            if (await _tagService.GetByNameAsync(tagDTO.Name, profile) != null) return Conflict("There is already a Tag with that name");

            tagDTO.Id = Guid.NewGuid().ToString();
            await _tagService.AddAsync(tagDTO, profile);

            return Json(tagDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, TagDTOSet tagDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            if (tagDTO == null) return BadRequest("Invalid data");
            tagDTO.Id = id;

            await _tagService.UpdateAsync(tagDTO, profile);
            return Json(tagDTO);
        }

        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(Utilities.InvalidStateMessages(ModelState));

            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized("Account not found");

            var tagDTO = await _tagService.GetByIdAsync(id, profile);
            if (tagDTO == null) return NotFound("Tag not found");

            await _tagService.RemoveAsync(id, profile);
            return Json(tagDTO);
        }

    }
}
