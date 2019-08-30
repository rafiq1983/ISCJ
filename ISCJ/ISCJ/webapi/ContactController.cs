using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogic;
using MA.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MA.Common.Entities;
using MA.Common.Entities.Contacts;
using MA.Common.Models.api;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ISCJ.webapi
{
    [Route("api/[controller]")]
    [Authorize]
    public class ContactController : MA.Core.Web.BaseController
    {

        [HttpPost()]
        public JsonResult AddContact([FromBody]AddContactInput input)
        {
            ContactManager contactManager = new ContactManager();
            return new JsonResult(contactManager.AddNewContact(GetCallContext(), input));
          
        }

        [HttpGet("Search")]
        public List<MA.Common.Entities.Contacts.Contact> PrefixSearch([FromQuery]string q)
        {
            if (string.IsNullOrEmpty(q) == false)
            {
                ContactManager mgr = new ContactManager();
                var lst = mgr.GetAllContacts();
                lst = lst.Where(x => (x.FirstName.ToLower() + " " + x.LastName.ToLower()).IndexOf(q.ToLower()) >= 0).ToList();
                return lst;
            }
            else
                return new List<MA.Common.Entities.Contacts.Contact>();
        }

        // GET: api/<controller>
        [HttpGet]
        public JsonResult GetAllContacts()
    {
            ContactManager mgr = new ContactManager();
            var lst = mgr.GetAllContacts();
            return new JsonResult(lst);
        }

    // GET api/<controller>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
      return "value";
    }

  
    // PUT api/<controller>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody]string value)
    {
    }

    // DELETE api/<controller>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
        
        [HttpGet("/ContactsByType/{contactTypeInput}")]
        [ProducesResponseType(200,Type=typeof(Contact))]
        [Produces("application/json")]
        public List<Contact> GetContactByType(MA.Common.Models.api.EnumContactType contactTypeInput)
        {
            ContactManager mgr = new ContactManager();
            return mgr.GetContactsByContactType(GetUserId(), Convert.ToInt32(contactTypeInput));
        }
        
    }

    public class Emp
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }


    }
}
