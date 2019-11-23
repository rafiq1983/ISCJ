using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common
{
    public class AddMetricInput
    {
        public string MetricName { get; set; }
        public string MetricDescription { get; set; }
        public MetricValueTypeDefinition MetricValueDefinition { get; set; }
        
    }


    public class AddMetricOutput
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
        public Guid MetricId { get; set; }
    }


    public abstract class MetricValueTypeDefinition
    {
        public string DataType { get; set; }


    }

    public class MetricIntegerValueDefinition : MetricValueTypeDefinition
    {
        public MetricIntegerValueDefinition()
        {
            DataType = this.GetType().ToString();
        }
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }

    public class MetricStringValueDefinition : MetricValueTypeDefinition
    {
        public string[] AllowedStrings { get; set; }
    }
}
