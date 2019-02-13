using BusinessLogic;
using MA.Common.Entities.Contacts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ISCJ
{
  public class TypeToNameService
  {
    public string GetDescription(string type, string value)
    {
      if(type=="contacttype")
      {
        List<ContactType> contactTypes = ContactManager.GetContacTypes();
        var contactType = contactTypes.SingleOrDefault(x => x.ID.ToString() == value);
        if (contactType == null)
          return "unknown";
        else
          return contactType.Description;
     }
      return type + ":" + value;
    }
  }
}
