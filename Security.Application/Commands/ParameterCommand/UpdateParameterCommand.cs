using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.ParameterAggregate;

namespace Security.Application.Commands.ParameterCommand
{
    public class UpdateParameterCommand : IRequest<Response<int>>
    {
        public int parameterId { get; set; }
        public string parameterName { get; set; }
        public int? state { get; set; }
    }

    public class UpdateParameterCommandHandler : IRequestHandler<UpdateParameterCommand, Response<int>>
    {
        readonly IParameterRepository _iParameterRepository;

        readonly IValuesSettings _iValuesSettings;

        public UpdateParameterCommandHandler(IParameterRepository iParameterRepository, IValuesSettings iValuesSettings)
        {
            _iParameterRepository = iParameterRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(UpdateParameterCommand request, CancellationToken cancellationToken)
        {
            Parameter parameter = new Parameter(request.parameterId, request.parameterName, request.state);

            var result = await _iParameterRepository.Register(parameter);

            return new Response<int>(result);
        }
    }
}
