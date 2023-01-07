using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Application.Commands.UsersCommand;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;
using System.Net;
using Microsoft.Net.Http.Headers;

namespace Security.Api.Controllers
{
    [Authorize]
    [Route("security/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IUsersQuery _iUsersQuery;
        readonly IMediator _mediator;
        readonly ITokenQuery _iTokenQuery;
        public UsersController(IUsersQuery iUsersQuery, IMediator mediator, ITokenQuery iTokenQuery)
        {
            _iUsersQuery = iUsersQuery ?? throw new ArgumentNullException(nameof(iUsersQuery));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _iTokenQuery = iTokenQuery ?? throw new ArgumentNullException(nameof(iTokenQuery));
        }

        [HttpGet]
        [Route("{userId}")]
        [ProducesResponseType(typeof(Response<UsersViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int userId)
        {
            var result = await _iUsersQuery.GetById(userId);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(Response<IEnumerable<UsersViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBySearch([FromQuery] UsersRequest request)
        {
            var result = await _iUsersQuery.GetBySearch(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("find-all")]
        [ProducesResponseType(typeof(Response<PaginationViewModel<UsersViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByFindAll([FromQuery] UsersRequest request)
        {
            var result = await _iUsersQuery.GetByFindAll(request);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateUsers(CreateUsersCommand command)
        {
            command.companyId = await GetCompanyId();
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(CreateUsers), result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateUsers(UpdateUsersCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("change-password")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangePassword(ChangeUserPasswordCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPut("change-state")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> ChangeState(ChangeUserStateCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        #region Methods

        private async Task<int> GetCompanyId() 
        {
            var accessToken = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");
            int companyId = await _iTokenQuery.GetCompanyToken(accessToken);
            return await Task.FromResult(companyId);
        }

        #endregion

    }
}
