using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.API.Controllers
{
    [Route(Program.ApiPrefix + "/tags")]
    [ApiController]
    [Authorize]
    public class TagsController : ControllerBase
    {
        private readonly ITagService _tagService;
        private readonly IUserProfileService _profileService;

        public TagsController(ITagService tagService, IUserProfileService profileService)
        {
            _tagService = tagService;
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TagDTOGet>>> Get()
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var tags = await _tagService.GetTagsAsync(profile);
            if (tags == null) return NotFound("Tags not found");

            return Ok(tags);
        }

        [HttpGet("{id:int}", Name = "GetTag")]
        public async Task<ActionResult<TagDTOGet>> Get(string id)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var tag = await _tagService.GetByIdAsync(id, profile);
            if (tag == null) return NotFound("Tag not found");

            return Ok(tag);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TagDTOSet tagDTO)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            if (tagDTO == null) return BadRequest("Invalid data");
            if (await _tagService.GetByNameAsync(tagDTO.Name, profile) != null) return Conflict("There is already a Tag with that name");

            await _tagService.AddAsync(tagDTO, profile);
            return new CreatedAtRouteResult("GetTag", new { id = tagDTO.Id }, tagDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(string id, [FromBody] TagDTOSet tagDTO)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            if (tagDTO == null) return BadRequest();
            tagDTO.Id = id;

            await _tagService.UpdateAsync(tagDTO, profile);
            return new CreatedAtRouteResult("GetTag", new { id = tagDTO.Id }, tagDTO);
        }

        [HttpDelete]
        public async Task<ActionResult<ReasonDTOGet>> Delete(string id)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var tagDTO = await _tagService.GetByIdAsync(id, profile);
            if (tagDTO == null) return NotFound("Tag not found");

            await _tagService.RemoveAsync(id, profile);
            return Ok(tagDTO);
        }
    }
}
