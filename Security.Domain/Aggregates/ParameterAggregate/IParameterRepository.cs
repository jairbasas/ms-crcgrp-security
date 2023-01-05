
namespace Security.Domain.Aggregates.ParameterAggregate
{
    public interface IParameterRepository
    {
        Task<int> Register(Parameter parameter);
    }
}
