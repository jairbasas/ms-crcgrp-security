using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Application.Commands.SystemsCommand;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;
using System.Net;

namespace Security.Api.Controllers
{
    [Authorize]
    [Route("security/systems")]
    [ApiController]
    public class SystemsController : ControllerBase
    {
        readonly ISystemsQuery _iSystemsQuery;
        readonly IMediator _mediator;

        public SystemsController(ISystemsQuery iSystemsQuery, IMediator mediator)
        {
            _iSystemsQuery = iSystemsQuery ?? throw new ArgumentNullException(nameof(iSystemsQuery));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{systemId}")]
        [ProducesResponseType(typeof(Response<SystemsViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int systemId)
        {
            var result = await _iSystemsQuery.GetById(systemId);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(Response<IEnumerable<SystemsViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBySearch([FromQuery] SystemsRequest request)
        {
            var result = await _iSystemsQuery.GetBySearch(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("find-all")]
        [ProducesResponseType(typeof(Response<PaginationViewModel<SystemsViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByFindAll([FromQuery] SystemsRequest request)
        {
            var result = await _iSystemsQuery.GetByFindAll(request);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateSystems(CreateSystemsCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(CreateSystems), result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateSystems(UpdateSystemsCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
