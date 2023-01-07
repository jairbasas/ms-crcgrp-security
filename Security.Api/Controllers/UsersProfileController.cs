using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Application.Commands.UsersProfileCommand;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;
using System.Net;

namespace Security.Api.Controllers
{
    [Authorize]
    [Route("security/users-profiles")]
    [ApiController]
    public class UsersProfileController : ControllerBase
    {
        readonly IUsersProfileQuery _iUsersProfileQuery;
        readonly IMediator _mediator;

        public UsersProfileController(IUsersProfileQuery iUsersProfileQuery, IMediator mediator)
        {
            _iUsersProfileQuery = iUsersProfileQuery ?? throw new ArgumentNullException(nameof(iUsersProfileQuery));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(typeof(Response<UsersProfileViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int userId)
        {
            var result = await _iUsersProfileQuery.GetById(userId);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(Response<IEnumerable<UsersProfileViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBySearch([FromQuery] UsersProfileRequest request)
        {
            var result = await _iUsersProfileQuery.GetBySearch(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("find-all")]
        [ProducesResponseType(typeof(Response<PaginationViewModel<UsersProfileViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByFindAll([FromQuery] UsersProfileRequest request)
        {
            var result = await _iUsersProfileQuery.GetByFindAll(request);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUsersProfile(CreateUsersProfileCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(CreateUsersProfile), result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUsersProfile(UpdateUsersProfileCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
