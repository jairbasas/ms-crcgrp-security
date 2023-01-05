using MediatR;
using Security.Application.Queries.Generics;
using Security.Application.Wrappers;
using Security.Domain.Aggregates.ParameterDetailAggregate;

namespace Security.Application.Commands.ParameterDetailCommand
{
    public class CreateParameterDetailCommand : IRequest<Response<int>>
    {
        public int parameterDetailId { get; set; }
        public string description { get; set; }
        public string fieldValue1 { get; set; }
        public string fieldDescription1 { get; set; }
        public string fieldValue2 { get; set; }
        public string fieldDescription2 { get; set; }
        public string fieldValue3 { get; set; }
        public string fieldDescription3 { get; set; }
        public int? parameterId { get; set; }
    }

    public class CreateParameterDetailCommandHandler : IRequestHandler<CreateParameterDetailCommand, Response<int>>
    {
        readonly IParameterDetailRepository _iParameterDetailRepository;
        readonly IValuesSettings _iValuesSettings;

        public CreateParameterDetailCommandHandler(IParameterDetailRepository iParameterDetailRepository, IValuesSettings iValuesSettings)
        {
            _iParameterDetailRepository = iParameterDetailRepository;
            _iValuesSettings = iValuesSettings;
        }

        public async Task<Response<int>> Handle(CreateParameterDetailCommand request, CancellationToken cancellationToken)
        {
            ParameterDetail parameterDetail = new ParameterDetail(request.parameterDetailId, request.description, request.fieldValue1, request.fieldDescription1, request.fieldValue2, request.fieldDescription2, request.fieldValue3, request.fieldDescription3, request.parameterId);

            var result = await _iParameterDetailRepository.Register(parameterDetail);

            return new Response<int>(result);
        }
    }
}
