using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MA.Common.Entities.Invoices;
using MA.Common.Entities.MasjidMembership;
using MA.Common.Models.api;
using MA.Core;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic
{
    public class MasjidMembershipManager
    {
        public List<MasjidMembership> GetAllMembers()
        {
            using (var db = new Database())
            {
                
                return db.MasjidMembers.Include(e=>e.Contact).ToList();
            }
        }

        public MasjidMembership GetMembershipByContactId(Guid contactId)
        {
            using (var db = new Database())
            {

                return db.MasjidMembers.Include(e => e.Contact).SingleOrDefault(x =>
                    x.ContactId == contactId && x.EffectiveDate < DateTime.UtcNow &&
                    x.ExpirationDate > DateTime.UtcNow);

            }

        }
        public CreateMasjidMembershipOutput CreateMasjidMembership(CallContext context, CreateMasjidMembershipInput input)
        {

            using (var db = new Database())
            {
                MA.Common.Entities.MasjidMembership.MasjidMembership membership = new MA.Common.Entities.MasjidMembership.MasjidMembership();

                if (input.AddNewContact)
                {
                    //db.Contacts.Add(input.Contact);
                    db.Entry(input.MemberContact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    membership.ContactId = input.ContactId.GetValueOrDefault(Guid.Empty);
                }

                membership.EffectiveDate = input.EffectiveDate;

                membership.ExpirationDate = input.ExpirationDate;
                membership.CreateDate = DateTime.Now;
                membership.Contact = input.MemberContact;
                membership.CreateUser = context.UserId;
                db.MasjidMembers.Add(membership);
                InvoiceManager billingMgr = new InvoiceManager();
                billingMgr.PerformBilling(context, db, input.BillingInstructions, "Invoice created for Membership registration", membership.ContactId.ToString(), ReferenceType.MembershipCreation);
                db.SaveChanges();
                return new CreateMasjidMembershipOutput()
                {
                    MembershipId = membership.MembershipId,
                    ContactId = input.ContactId ?? membership.Contact.Guid
                };
            }
        }
    }
}
