using Security.Application.Queries.ViewModels;

namespace Security.Application.Queries.Mappers
{
    public interface IParameterMapper
    {
        ParameterViewModel MapToParameterViewModel(dynamic r);
    }

    public class ParameterMapper : IParameterMapper
    {
        public ParameterViewModel MapToParameterViewModel(dynamic r)
        {
            ParameterViewModel o = new ParameterViewModel();

            o.parameterId = r.parameter_id;
            o.parameterName = r.parameter_name;
            o.state = r.state;

            return o;
        }
    }
}
