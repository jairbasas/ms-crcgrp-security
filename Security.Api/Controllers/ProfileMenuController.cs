using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Application.Commands.ProfileMenuCommand;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;
using System.Net;

namespace Security.Api.Controllers
{
    [Authorize]
    [Route("security/profile-menus")]
    [ApiController]
    public class ProfileMenuController : ControllerBase
    {
        readonly IProfileMenuQuery _iProfileMenuQuery;
        readonly IMediator _mediator;

        public ProfileMenuController(IProfileMenuQuery iProfileMenuQuery, IMediator mediator)
        {
            _iProfileMenuQuery = iProfileMenuQuery ?? throw new ArgumentNullException(nameof(iProfileMenuQuery));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{profileId}")]
        [ProducesResponseType(typeof(Response<ProfileMenuViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int profileId)
        {
            var result = await _iProfileMenuQuery.GetById(profileId);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(Response<IEnumerable<ProfileMenuViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBySearch([FromQuery] ProfileMenuRequest request)
        {
            var result = await _iProfileMenuQuery.GetBySearch(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("find-all")]
        [ProducesResponseType(typeof(Response<PaginationViewModel<ProfileMenuViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByFindAll([FromQuery] ProfileMenuRequest request)
        {
            var result = await _iProfileMenuQuery.GetByFindAll(request);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateProfileMenu(CreateProfileMenuCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(CreateProfileMenu), result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProfileMenu(UpdateProfileMenuCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteProfileMenu(DeleteProfileMenuCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
