using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.ProfileAggregate;

namespace Security.Application.Commands.ProfileCommand
{
    public class CreateProfileCommand : IRequest<Response<int>>
    {
        public int profileId { get; set; }
        public string profileName { get; set; }
        public int? state { get; set; }
        public int? systemId { get; set; }
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
    }

    public class CreateProfileCommandHandler : IRequestHandler<CreateProfileCommand, Response<int>>
    {
        readonly IProfileRepository _iProfileRepository;
        readonly IValuesSettings _iValuesSettings;

        public CreateProfileCommandHandler(IProfileRepository iProfileRepository, IValuesSettings iValuesSettings)
        {
            _iProfileRepository = iProfileRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
        {
            Profile profile = new Profile(request.profileId, request.profileName, request.state, request.systemId, request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

            var result = await _iProfileRepository.Register(profile);

            return new Response<int>(result);
        }
    }
}
