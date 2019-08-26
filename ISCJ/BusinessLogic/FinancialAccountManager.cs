using MA.Common.Entities.Invoices;
using MA.Common.Models.api;
using MA.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogic
{
    public class FinancialAccountManager
    {
        public CreateFinancialAccountOutput CreateFinancialAccount(CallContext callContext, CreateFinancialAccountInput input)
        {
            using (var db = new Database())
            {
                FinancialAccount account = new FinancialAccount();
                account.FinancialAccountId = Guid.NewGuid();
                account.FinancialAccountName = input.FinancialAccountName;
                account.FinancialAccountType = input.FinancialAccountType;
                account.TenantId = callContext.TenantId;
                account.CreateDate = DateTime.UtcNow;
                account.CreateUser = callContext.UserId;
                
                db.FinancialAccounts.Add(account);
                db.SaveChanges();
                return new CreateFinancialAccountOutput() { FinancialAccountId = account.FinancialAccountId };
            }
        }

        public List<FinancialAccount> GetFinancialAccounts(CallContext context)
        {
            using (var db = new Database())
            {
                return db.FinancialAccounts.ToList();
            }
        }
    }
}
