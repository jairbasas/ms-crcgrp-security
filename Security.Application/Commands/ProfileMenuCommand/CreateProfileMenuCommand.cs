using MediatR;
using MoreLinq;
using Security.Application.Queries.Generics;
using Security.Application.Queries.Interfaces;
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
        readonly IMenuQuery _iMenuQuery;

        public CreateProfileMenuCommandHandler(IProfileMenuRepository iProfileMenuRepository, IValuesSettings iValuesSettings, IMenuQuery iMenuQuery)
        {
            _iProfileMenuRepository = iProfileMenuRepository;
            _iValuesSettings = iValuesSettings;
            _iMenuQuery = iMenuQuery;
        }

        public async Task<Response<int>> Handle(CreateProfileMenuCommand request, CancellationToken cancellationToken)
        {
            ProfileMenu profileMenu = null;
            var profileMenuList = new List<ProfileMenu>();

            var menuList = await _iMenuQuery.GetBySearch(new Queries.ViewModels.MenuRequest()
            {
                state = StateEntity.Active
            });

            var menuFound = await _iMenuQuery.GetBySearch(new Queries.ViewModels.MenuRequest() 
            {
                menuIdList = string.Join(",", request.menuIds)
            });

            var menuParentFound = menuFound.data.Where(x => x.menuParentId == null);
            var menuChildrenFound = menuFound.data.Where(x => x.menuParentId != null);

            List<int> menuAuxId = new List<int>();
            if (menuParentFound.Any())
            {
                menuParentFound.ForEach(item => {
                    profileMenu = new ProfileMenu(request.profileId, item.menuId, request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));
                    profileMenuList.Add(profileMenu);
                    var menuChildren = menuList.data.Where(x => x.menuParentId == item.menuId);
                    menuChildren.ForEach(element => {
                        profileMenu = new ProfileMenu(request.profileId, element.menuId, request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));
                    profileMenuList.Add(profileMenu);
                    });
                });
            }

            if (menuChildrenFound.Any())
            {               
                menuChildrenFound.ForEach(item => {
                    profileMenu = new ProfileMenu(request.profileId, item.menuId, request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));
                    profileMenuList.Add(profileMenu);

                    profileMenu = new ProfileMenu(request.profileId, item.menuParentId.Value, request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));
                    profileMenuList.Add(profileMenu);
                });
            }

            profileMenuList = (from d in profileMenuList select d).Distinct().ToList();

            var result = await _iProfileMenuRepository.RegisterAsync(profileMenuList);

            return new Response<int>(result);
        }
    }
}
