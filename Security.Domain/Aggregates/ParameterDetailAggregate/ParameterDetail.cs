using Security.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Domain.Aggregates.ParameterDetailAggregate
{
    public class ParameterDetail : Audit
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

        public ParameterDetail()
        {
        }

        public ParameterDetail(int parameterDetailId, string description, string fieldValue1, string fieldDescription1, string fieldValue2, string fieldDescription2, string fieldValue3, string fieldDescription3, int? parameterId)
        {
            this.parameterDetailId = parameterDetailId;
            this.description = description;
            this.fieldValue1 = fieldValue1;
            this.fieldDescription1 = fieldDescription1;
            this.fieldValue2 = fieldValue2;
            this.fieldDescription2 = fieldDescription2;
            this.fieldValue3 = fieldValue3;
            this.fieldDescription3 = fieldDescription3;
            this.parameterId = parameterId;
        }
    }
}
