using MediatR;
using Microsoft.AspNetCore.Mvc;
using Security.Application.Commands.AuthenticationCommand;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;
using System.Net;

namespace Security.Api.Controllers
{
    [Route("authentications")]
    [ApiController]
    public class AuthenticationsController : ControllerBase
    {
        private readonly IMediator _iMediator;
        private readonly IUserAuthenticationQuery _iuserAuthenticationQuery;

        public AuthenticationsController(IUserAuthenticationQuery iuserAuthenticationQuery, IMediator iMediator)
        {
            this._iuserAuthenticationQuery = iuserAuthenticationQuery ?? throw new ArgumentNullException(nameof(iuserAuthenticationQuery));
            this._iMediator = iMediator ?? throw new ArgumentNullException(nameof(iMediator));
        }

        [HttpPost]
        [ProducesResponseType(typeof(Response<UserAuthenticationViewModel>), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAuthenticationCommand(CreateAuthenticationCommand createAuthenticationCommand)
        {
            var commandResult = await _iMediator.Send(createAuthenticationCommand);
            var result = CreatedAtAction(nameof(CreateAuthenticationCommand), commandResult);

            return result;
        }
    }
}
