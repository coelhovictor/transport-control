using Core.Application.DTOs;
using Core.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.API.Controllers
{
    [Route(Program.ApiPrefix + "/transports")]
    [ApiController]
    [Authorize]
    public class TransportsController : ControllerBase
    {
        private readonly ITransportService _transportService;
        private readonly ITransportTypeService _typeService;
        private readonly IReasonService _reasonService;
        private readonly ITagService _tagService;
        private readonly ITransportTagService _transportTagService;
        private readonly IUserProfileService _profileService;

        public TransportsController(ITransportService transportService, ITransportTypeService typeService,
            IReasonService reasonService, ITagService tagService, ITransportTagService transportTagService,
            IUserProfileService profileService)
        {
            _transportService = transportService;
            _typeService = typeService;
            _reasonService = reasonService;
            _tagService = tagService;
            _transportTagService = transportTagService;
            _profileService = profileService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportDTOGet>>> Get()
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var transports = await _transportService.GetTransportsAsync(profile);
            if (transports == null) return NotFound("Transports not found");

            return Ok(transports);
        }

        [HttpGet("getbydate/{date:DateTime}")]
        public async Task<ActionResult<IEnumerable<TransportDTOGet>>> GetByDate(DateTime date)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var transports = await _transportService.GetTransportsByDateAsync(date, profile);
            if (transports == null) return NotFound("Transports not found");

            return Ok(transports);
        }

        [HttpGet("getbyweek/{date:DateTime}")]
        public async Task<ActionResult<IEnumerable<TransportDateDTO>>> GetByWeek(DateTime date)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var transports = await _transportService.GetTransportsByMonthAsync(date, profile);
            if (transports == null) return NotFound("Transports not found");

            return Ok(transports);
        }

        [HttpGet("getbymonth/{date:DateTime}")]
        public async Task<ActionResult<IEnumerable<TransportDateDTO>>> GetByMonth(DateTime date)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var transports = await _transportService.GetTransportsByMonthAsync(date, profile);
            if (transports == null) return NotFound("Transports not found");

            return Ok(transports);
        }

        [HttpGet("{id:int}", Name = "GetTransport")]
        public async Task<ActionResult<TransportDTOGet>> Get(string id)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var transport = await _transportService.GetByIdAsync(id, profile);
            if (transport == null) return NotFound("Transport not found");

            return Ok(transport);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] TransportDTOSet transportDTO)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            if (transportDTO == null) return BadRequest("Invalid data");

            if (await _typeService.GetByIdAsync(transportDTO.TransportTypeId, profile) == null) return NotFound("Transport Type not found");

            if (await _reasonService.GetByIdAsync(transportDTO.ReasonId, profile) == null) return NotFound("Reason not found");

            await _transportService.AddAsync(transportDTO, profile);
            return new CreatedAtRouteResult("GetTransport", new { id = transportDTO.Id }, transportDTO);
        }

        [HttpPut]
        public async Task<ActionResult> Put(string id, [FromBody] TransportDTOSet transportDTO)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            if (transportDTO == null) return BadRequest();
            transportDTO.Id = id;

            if (await _typeService.GetByIdAsync(transportDTO.TransportTypeId, profile) == null) return NotFound("Transport Type not found");
            if (await _reasonService.GetByIdAsync(transportDTO.ReasonId, profile) == null) return NotFound("Reason not found");

            await _transportService.UpdateAsync(transportDTO, profile);
            return new CreatedAtRouteResult("GetTransport", new { id = transportDTO.Id }, transportDTO);
        }

        [HttpDelete]
        public async Task<ActionResult<TransportDTOGet>> Delete(string id)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var transportDTO = await _transportService.GetByIdAsync(id, profile);
            if (transportDTO == null) return NotFound("Transport not found");

            await _transportService.RemoveAsync(id, profile);
            return Ok(transportDTO);
        }

        [HttpPatch("tag/{id:int}")]
        public async Task<ActionResult> PostTag(string id, string tagId)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var transportDTO = await _transportService.GetByIdAsync(id, profile);
            if(transportDTO == null) return NotFound("Transport not found");

            var tagDTO = await _tagService.GetByIdAsync(tagId, profile);
            if (tagDTO == null) return NotFound("Tag not found");

            if (transportDTO.TransportTags.Count() >= 5) return Conflict("Transport has the maximum amount of tags");
            if(transportDTO.TransportTags.Where(x => x.TagId == tagId).Any()) return Conflict("Transport already has this tag");

            var transportTagDTO = await _transportTagService.AddTagAsync(transportDTO, tagDTO, profile);
            return Ok(transportTagDTO);
        }

        [HttpDelete("tag/{id:int}")]
        public async Task<ActionResult> DeleteTag(string id, string tagId)
        {
            var profile = await _profileService.GetByEmailAsync(User.Identity.Name);
            if (profile == null) return Unauthorized();

            var transportDTO = await _transportService.GetByIdAsync(id, profile);
            if (transportDTO == null) return NotFound("Transport not found");

            var transportTagDTO = transportDTO.TransportTags.Where(x => x.TagId == tagId).FirstOrDefault();
            if (transportTagDTO == null) return NotFound("Transport don't has this tag");

            await _transportTagService.RemoveTagAsync(transportTagDTO.Id);
            return Ok(transportTagDTO);
        }
    }
}
