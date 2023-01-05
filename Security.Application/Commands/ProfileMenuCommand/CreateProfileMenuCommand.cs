using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.ProfileMenuAggregate;

namespace Security.Application.Commands.ProfileMenuCommand
{
    public class CreateProfileMenuCommand : IRequest<Response<int>>
    {
        public int profileId { get; set; }
        public int[] menuIds { get; set; }
        public int registerUserId { get; set; }
        public string? registerUserFullname { get; set; }
    }

    public class CreateProfileMenuCommandHandler : IRequestHandler<CreateProfileMenuCommand, Response<int>>
    {
        readonly IProfileMenuRepository _iProfileMenuRepository;
        readonly IValuesSettings _iValuesSettings;

        public CreateProfileMenuCommandHandler(IProfileMenuRepository iProfileMenuRepository, IValuesSettings iValuesSettings)
        {
            _iProfileMenuRepository = iProfileMenuRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(CreateProfileMenuCommand request, CancellationToken cancellationToken)
        {
            int menuId = 0;

            ProfileMenu profileMenu = new ProfileMenu(request.profileId, menuId, request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.menuIds);

            var result = await _iProfileMenuRepository.RegisterAsync(profileMenu);

            return new Response<int>(result);
        }
    }
}
