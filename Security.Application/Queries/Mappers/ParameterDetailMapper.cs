using Security.Application.Queries.ViewModels;

namespace Security.Application.Queries.Mappers
{
    public interface IParameterDetailMapper
    {
        ParameterDetailViewModel MapToParameterDetailViewModel(dynamic r);
    }

    public class ParameterDetailMapper : IParameterDetailMapper
    {
        public ParameterDetailViewModel MapToParameterDetailViewModel(dynamic r)
        {
            ParameterDetailViewModel o = new ParameterDetailViewModel();

            o.parameterDetailId = r.parameter_detail_id;
            o.description = r.description;
            o.fieldValue1 = r.field_value_1;
            o.fieldDescription1 = r.field_description_1;
            o.fieldValue2 = r.field_value_2;
            o.fieldDescription2 = r.field_description_2;
            o.fieldValue3 = r.field_value_3;
            o.fieldDescription3 = r.field_description_3;
            o.parameterId = r.parameter_id;

            return o;
        }
    }
}
