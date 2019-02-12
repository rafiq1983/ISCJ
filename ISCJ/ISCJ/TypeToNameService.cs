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
        switch(value)
        {
          case "1":
            return "Student";
          default:
            return "Unknown";
        }
      }
      return type + ":" + value;
    }
  }
}
