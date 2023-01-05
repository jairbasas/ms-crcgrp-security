using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.SystemsAggregate;

namespace Security.Application.Commands.SystemsCommand
{
    public class CreateSystemsCommand : IRequest<Response<int>>
    {
        public int systemId { get; set; }
        public string systemName { get; set; }
        public int? state { get; set; }
        public int? registerUserId { get; set; }
        public string registerUserFullname { get; set; }
    }

    public class CreateSystemsCommandHandler : IRequestHandler<CreateSystemsCommand, Response<int>>
    {
        readonly ISystemsRepository _iSystemsRepository;
        readonly IValuesSettings _iValuesSettings;

        public CreateSystemsCommandHandler(ISystemsRepository iSystemsRepository, IValuesSettings iValuesSettings)
        {
            _iSystemsRepository = iSystemsRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(CreateSystemsCommand request, CancellationToken cancellationToken)
        {
            Systems systems = new Systems(request.systemId, request.systemName, request.state, request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.registerUserId, request.registerUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

            var result = await _iSystemsRepository.Register(systems);

            return new Response<int>(result);
        }
    }
}
