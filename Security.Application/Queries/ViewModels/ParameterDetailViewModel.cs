using Security.Application.Queries.ViewModels.Base;

namespace Security.Application.Queries.ViewModels
{
    public class ParameterDetailViewModel
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

    public class ParameterDetailRequest : PaginationRequest
    {
        public int? parameterDetailId { get; set; }
        public int? parameterId { get; set; }
    }
}
