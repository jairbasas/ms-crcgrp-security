using MediatR;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.ProfileMenuAggregate;

namespace Security.Application.Commands.ProfileMenuCommand
{
    public class DeleteProfileMenuCommand : IRequest<Response<int>>
    {
        public int profileId { get; set; }
        public int menuId { get; set; }
    }

    public class DeleteProfileMenuCommandHandler : IRequestHandler<DeleteProfileMenuCommand, Response<int>>
    {
        readonly IProfileMenuRepository _iProfileMenuRepository;

        public DeleteProfileMenuCommandHandler(IProfileMenuRepository iProfileMenuRepository)
        {
            _iProfileMenuRepository = iProfileMenuRepository;
        }
        public async Task<Response<int>> Handle(DeleteProfileMenuCommand request, CancellationToken cancellationToken)
        {
            ProfileMenu profileMenu = new ProfileMenu(request.profileId, request.menuId);
            var result = await _iProfileMenuRepository.Delete(profileMenu);

            return new Response<int>(result);
        }
    }
}
