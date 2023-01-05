using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.MenuAggregate;

namespace Security.Application.Commands.MenuCommand
{
    public class CreateMenuCommand : IRequest<Response<int>>
    {
        public int menuId { get; set; }
        public string menuName { get; set; }
        public int? level { get; set; }
        public string url { get; set; }
        public string icon { get; set; }
        public int? order { get; set; }
        public int? menuParentId { get; set; }
        public int? state { get; set; }
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
    }

    public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, Response<int>>
    {
        readonly IMenuRepository _iMenuRepository;
        readonly IValuesSettings _iValuesSettings;

        public CreateMenuCommandHandler(IMenuRepository iMenuRepository, IValuesSettings iValuesSettings)
        {
            _iMenuRepository = iMenuRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(CreateMenuCommand request, CancellationToken cancellationToken)
        {
            Menu menu = new Menu(request.menuId, request.menuName, request.level, request.url, request.icon, request.order, request.menuParentId, request.state, request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

            var result = await _iMenuRepository.Register(menu);

            return new Response<int>(result);
        }
    }

}
