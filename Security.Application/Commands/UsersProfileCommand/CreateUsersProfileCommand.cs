using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.UsersProfileAggregate;

namespace Security.Application.Commands.UsersProfileCommand
{
    public class CreateUsersProfileCommand : IRequest<Response<int>>
    {
        public int userId { get; set; }
        public int profileId { get; set; }
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
    }

    public class CreateUsersProfileCommandHandler : IRequestHandler<CreateUsersProfileCommand, Response<int>>
    {
        readonly IUsersProfileRepository _iUsersProfileRepository;
        readonly IValuesSettings _iValuesSettings;

        public CreateUsersProfileCommandHandler(IUsersProfileRepository iUsersProfileRepository, IValuesSettings iValuesSettings)
        {
            _iUsersProfileRepository = iUsersProfileRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(CreateUsersProfileCommand request, CancellationToken cancellationToken)
        {
            UsersProfile usersProfile = new UsersProfile(request.userId, request.profileId, request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

            var result = await _iUsersProfileRepository.Register(usersProfile);

            return new Response<int>(result);
        }
    }
}
