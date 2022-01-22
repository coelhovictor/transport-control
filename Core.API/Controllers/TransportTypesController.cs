using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.API.Controllers
{
    [Route(Program.ApiPrefix + "/transporttypes")]
    [ApiController]
    [Authorize]
    public class TransportTypesController : ControllerBase
    {
        private readonly ITransportTypeService _typeService;
        private readonly IUserProfileService _profileService;

        public TransportTypesController(ITransportTypeService typeService, IUserProfileService profileService)
        {
            _typeService = typeService;
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportTypeDTOGet>>> Get()
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var types = await _typeService.GetTransportTypesAsync(profile);
            if(types == null) return NotFound("Transport Types not found");

            return Ok(types);
        }

        [HttpGet("{id:int}", Name = "GetTransportType")]
        public async Task<ActionResult<TransportTypeDTOGet>> Get(string id)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var type = await _typeService.GetByIdAsync(id, profile);
            if(type == null) return NotFound("Transport Type not found");

            return Ok(type);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TransportTypeDTOSet typeDTO)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            if (typeDTO == null) return BadRequest("Invalid data");
            if (await _typeService.GetByNameAsync(typeDTO.Name, profile) != null) return Conflict("There is already a Transport Type with that name");

            await _typeService.AddAsync(typeDTO, profile);
            return new CreatedAtRouteResult("GetTransportType", new { id = typeDTO.Id }, typeDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(string id, [FromBody] TransportTypeDTOSet typeDTO)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            if (typeDTO == null)return BadRequest();
            typeDTO.Id = id;

            await _typeService.UpdateAsync(typeDTO, profile);
            return new CreatedAtRouteResult("GetTransportType", new { id = typeDTO.Id }, typeDTO);
        }

        [HttpDelete]
        public async Task<ActionResult<TransportTypeDTOGet>> Delete(string id)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var typeDTO = await _typeService.GetByIdAsync(id, profile);
            if (typeDTO == null) return NotFound("Transport Type not found");

            await _typeService.RemoveAsync(id, profile);
            return Ok(typeDTO);
        }
    }
}
