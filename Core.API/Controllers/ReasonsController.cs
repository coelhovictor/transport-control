using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.API.Controllers
{
    [Route(Program.ApiPrefix + "/reasons")]
    [ApiController]
    [Authorize]
    public class ReasonsController : ControllerBase
    {
        private readonly IReasonService _reasonService;
        private readonly IUserProfileService _profileService;

        public ReasonsController(IReasonService reasonService, IUserProfileService profileService)
        {
            _reasonService = reasonService;
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReasonDTOGet>>> Get()
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var reasons = await _reasonService.GetReasonsAsync(profile);
            if (reasons == null) return NotFound("Reasons not found");

            return Ok(reasons);
        }

        [HttpGet("{id:int}", Name = "GetReason")]
        public async Task<ActionResult<ReasonDTOGet>> Get(string id)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var reason = await _reasonService.GetByIdAsync(id, profile);
            if (reason == null) return NotFound("Reason not found");

            return Ok(reason);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ReasonDTOSet reasonDTO)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            if (reasonDTO == null) return BadRequest("Invalid data");
            if (await _reasonService.GetByNameAsync(reasonDTO.Name, profile) != null) return Conflict("There is already a Reason with that name");

            await _reasonService.AddAsync(reasonDTO, profile);
            return new CreatedAtRouteResult("GetReason", new { id = reasonDTO.Id }, reasonDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(string id, [FromBody] ReasonDTOSet reasonDTO)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            if (reasonDTO == null) return BadRequest();
            reasonDTO.Id = id;

            await _reasonService.UpdateAsync(reasonDTO, profile);
            return new CreatedAtRouteResult("GetReason", new { id = reasonDTO.Id }, reasonDTO);
        }

        [HttpDelete]
        public async Task<ActionResult<ReasonDTOGet>> Delete(string id)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var reasonDTO = await _reasonService.GetByIdAsync(id, profile);
            if (reasonDTO == null) return NotFound("Reason not found");

            await _reasonService.RemoveAsync(id, profile);
            return Ok(reasonDTO);
        }
    }
}
