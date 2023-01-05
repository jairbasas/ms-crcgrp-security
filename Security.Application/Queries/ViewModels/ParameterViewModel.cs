using Security.Application.Queries.ViewModels.Base;

namespace Security.Application.Queries.ViewModels
{
    public class ParameterViewModel
    {
        public int parameterId { get; set; }
        public string parameterName { get; set; }
        public int? state { get; set; }
    }

    public class ParameterRequest : PaginationRequest
    {
        public int parameterId { get; set; }
    }
}
