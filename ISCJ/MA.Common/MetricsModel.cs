using System;
using System.Collections.Generic;
using System.Text;
using MA.Common.Entities.Registration;

namespace MA.Common
{
    public class AttachMetricInput
    {
        public Guid EntityId { get; set; }
        public string EntityType { get; set; }
        public Guid MetricId { get; set; }

    }

    public class SetMetricsToEntityInput
    {
        public Guid EntityId { get; set; }
        public string EntityType { get; set; }

        public List<Guid> MetricIds { get; set; }
    }


    public class AttachMetricOutput
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }
    }

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

    public class SelectedMetric
    {
        public bool IsSelected { get; set; }
        public Guid MetricId { get; set; }
    }
}
