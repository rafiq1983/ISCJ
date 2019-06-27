using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Common.Models.api
{
  public class CreateFinancialAccountInput
    {
        public FinancialAccountType FinancialAccountType { get; set; }
        public string FinancialAccountName { get; set; }
    }

    public class CreateFinancialAccountOutput
    {
        public Guid FinancialAccountId { get; set; }
    }

    public enum FinancialAccountType
    {
        AccountsReceiveable, AccountsPayable
    }
}
