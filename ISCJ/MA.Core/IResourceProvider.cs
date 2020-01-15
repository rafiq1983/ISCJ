using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Core
{
   public interface IResourceProvider
   {
       string GetString(string key);
   }
}
