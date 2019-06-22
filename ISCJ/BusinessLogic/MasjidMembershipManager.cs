using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MA.Common.Entities.MasjidMembership;
using MA.Common.Models.api;
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
        public CreateMasjidMembershipOutput CreateMasjidMembership(CreateMasjidMembershipInput input)
        {
            using (var db = new Database())
            {
                MA.Common.Entities.MasjidMembership.MasjidMembership membership = new MA.Common.Entities.MasjidMembership.MasjidMembership();

                if (input.AddNewContact)
                {
                    //db.Contacts.Add(input.Contact);
                    db.Entry(input.Contact).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                }
                else
                {
                    membership.ContactId = input.ContactId.Value;
                }
                membership.EffectiveDate = DateTime.Now;
                membership.ExpirationDate = DateTime.Now.AddYears(1);
                membership.CreateDate = DateTime.Now;
                membership.Contact = input.Contact;
                membership.CreateUser = "Iftikhar";
                db.MasjidMembers.Add(membership);
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
