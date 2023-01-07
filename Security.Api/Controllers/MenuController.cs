using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Security.Application.Commands.MenuCommand;
using Security.Application.Queries.Interfaces;
using Security.Application.Queries.ViewModels.Base;
using Security.Application.Queries.ViewModels;
using Security.Application.Wrappers;
using System.Net;

namespace Security.Api.Controllers
{
    [Authorize]
    [Route("security/menus")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        readonly IMenuQuery _iMenuQuery;
        readonly IMediator _mediator;

        public MenuController(IMenuQuery iMenuQuery, IMediator mediator)
        {
            _iMenuQuery = iMenuQuery ?? throw new ArgumentNullException(nameof(iMenuQuery));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        [Route("{menuId}")]
        [ProducesResponseType(typeof(Response<MenuViewModel>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetById(int menuId)
        {
            var result = await _iMenuQuery.GetById(menuId);

            if (result != null)
                return Ok(result);
            else
                return NotFound();
        }

        [HttpGet]
        [Route("search")]
        [ProducesResponseType(typeof(Response<IEnumerable<MenuViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetBySearch([FromQuery] MenuRequest request)
        {
            var result = await _iMenuQuery.GetBySearch(request);

            return Ok(result);
        }

        [HttpGet]
        [Route("find-all")]
        [ProducesResponseType(typeof(Response<PaginationViewModel<MenuViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByFindAll([FromQuery] MenuRequest request)
        {
            var result = await _iMenuQuery.GetByFindAll(request);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateMenu(CreateMenuCommand command)
        {
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(CreateMenu), result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateMenu(UpdateMenuCommand command)
        {
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        [Route("menu-by-profile")]
        [ProducesResponseType(typeof(Response<IEnumerable<MenuViewModel>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByProfile([FromQuery] MenuRequest request)
        {
            var result = await _iMenuQuery.GetTreeMenu(request);

            return Ok(result);
        }

    }
}
