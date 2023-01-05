using Security.Domain.Core;

namespace Security.Domain.Aggregates.ParameterAggregate
{
    public class Parameter : Audit
    {
        public int parameterId { get; set; }
        public string parameterName { get; set; }
        public int? state { get; set; }

        public Parameter()
        {
        }

        public Parameter(int parameterId, string parameterName, int? state)
        {
            this.parameterId = parameterId;
            this.parameterName = parameterName;
            this.state = state;
        }
    }
}
