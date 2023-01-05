using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.ProfileMenuAggregate;

namespace Security.Application.Commands.ProfileMenuCommand
{
    public class UpdateProfileMenuCommand : IRequest<Response<int>>
    {
        public int profileId { get; set; }
        public int menuId { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
    }

    public class UpdateProfileMenuCommandHandler : IRequestHandler<UpdateProfileMenuCommand, Response<int>>
    {
        readonly IProfileMenuRepository _iProfileMenuRepository;

        readonly IValuesSettings _iValuesSettings;

        public UpdateProfileMenuCommandHandler(IProfileMenuRepository iProfileMenuRepository, IValuesSettings iValuesSettings)
        {
            _iProfileMenuRepository = iProfileMenuRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(UpdateProfileMenuCommand request, CancellationToken cancellationToken)
        {
            ProfileMenu profileMenu = new ProfileMenu(request.profileId, request.menuId, request.updateUserId, request.updateUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.updateUserId, request.updateUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), new int[0]);

            var result = await _iProfileMenuRepository.Register(profileMenu);

            return new Response<int>(result);
        }
    }
}
