using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.UsersProfileAggregate;

namespace Security.Application.Commands.UsersProfileCommand
{
    public class UpdateUsersProfileCommand : IRequest<Response<int>>
    {
        public int userId { get; set; }
        public int profileId { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
    }

    public class UpdateUsersProfileCommandHandler : IRequestHandler<UpdateUsersProfileCommand, Response<int>>
    {
        readonly IUsersProfileRepository _iUsersProfileRepository;

        readonly IValuesSettings _iValuesSettings;

        public UpdateUsersProfileCommandHandler(IUsersProfileRepository iUsersProfileRepository, IValuesSettings iValuesSettings)
        {
            _iUsersProfileRepository = iUsersProfileRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(UpdateUsersProfileCommand request, CancellationToken cancellationToken)
        {
            UsersProfile usersProfile = new UsersProfile(request.userId, request.profileId, request.updateUserId, request.updateUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.updateUserId, request.updateUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

            var result = await _iUsersProfileRepository.Register(usersProfile);

            return new Response<int>(result);
        }
    }
}
