using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Utility;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.SystemsAggregate;

namespace Security.Application.Commands.SystemsCommand
{
    public class UpdateSystemsCommand : IRequest<Response<int>>
    {
        public int systemId { get; set; }
        public string systemName { get; set; }
        public int? state { get; set; }
        public int? updateUserId { get; set; }
        public string updateUserFullname { get; set; }
    }

    public class UpdateSystemsCommandHandler : IRequestHandler<UpdateSystemsCommand, Response<int>>
    {
        readonly ISystemsRepository _iSystemsRepository;

        readonly IValuesSettings _iValuesSettings;

        public UpdateSystemsCommandHandler(ISystemsRepository iSystemsRepository, IValuesSettings iValuesSettings)
        {
            _iSystemsRepository = iSystemsRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(UpdateSystemsCommand request, CancellationToken cancellationToken)
        {
            Systems systems = new Systems(request.systemId, request.systemName, request.state, request.updateUserId, request.updateUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()), request.updateUserId, request.updateUserFullname, DateTime.Now.Peru(_iValuesSettings.GetTimeZone()));

            var result = await _iSystemsRepository.Register(systems);

            return new Response<int>(result);
        }
    }
}
