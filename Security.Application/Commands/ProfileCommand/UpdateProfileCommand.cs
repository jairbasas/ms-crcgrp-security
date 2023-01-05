using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.ProfileAggregate;

namespace Security.Application.Commands.ProfileCommand
{
    public class UpdateProfileCommand : IRequest<Response<int>>
    {
        public int profileId { get; set; }
        public string profileName { get; set; }
        public int? state { get; set; }
        public int? systemId { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
    }

    public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand, Response<int>>
    {
        readonly IProfileRepository _iProfileRepository;

        readonly IValuesSettings _iValuesSettings;

        public UpdateProfileCommandHandler(IProfileRepository iProfileRepository, IValuesSettings iValuesSettings)
        {
            _iProfileRepository = iProfileRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(UpdateProfileCommand request, CancellationToken cancellationToken)
        {
            Profile profile = new Profile(request.profileId, request.profileName, request.state, request.systemId, request.updateUserId, request.updateUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.updateUserId, request.updateUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

            var result = await _iProfileRepository.Register(profile);

            return new Response<int>(result);
        }
    }
}
