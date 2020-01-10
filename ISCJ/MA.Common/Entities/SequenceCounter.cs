using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Entities
{
    public class SequenceCounter
    {
        public int CounterId { get; set; }
        public string CounterType { get; set; }
        public Guid? TenantId { get; set; }
        public int CounterValue { get; set; }
        public string CounterName { get; set; }
    }
}
