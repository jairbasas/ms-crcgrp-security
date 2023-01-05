using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Application.Commands.ProfileCommand;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;
using System.Net;

namespace Security.Api.Controllers
{
    [Authorize]
    [Route("profiles")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        readonly IProfileQuery _iProfileQuery;
        readonly IMediator _mediator;

        public ProfileController(IProfileQuery iProfileQuery, IMediator mediator)
        {
            _iProfileQuery = iProfileQuery ?? throw new ArgumentNullException(nameof(iProfileQuery));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{profileId}")]
        [ProducesResponseType(typeof(Response<ProfileViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int profileId)
        {
            var result = await _iProfileQuery.GetById(profileId);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(Response<IEnumerable<ProfileViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBySearch([FromQuery] ProfileRequest request)
        {
            var result = await _iProfileQuery.GetBySearch(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("find-all")]
        [ProducesResponseType(typeof(Response<PaginationViewModel<ProfileViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByFindAll([FromQuery] ProfileRequest request)
        {
            var result = await _iProfileQuery.GetByFindAll(request);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProfile(CreateProfileCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(CreateProfile), result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProfile(UpdateProfileCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
