using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic
{
  class CacheService
  {
    public const string ContactTypesKey = "ContactTypes";
    public const string IslamicSchoolGradeListKey = "IslamicSchoolGradeKey";
    public const string PublicSchoolGradeListKey = "PublicSchoolGradeKey";

    private static System.Collections.Generic.Dictionary<string, object> _cacheData = new Dictionary<string, object>();
    
    public static void AddData(string key, object data, bool update=false)
    {
      if (_cacheData.ContainsKey(key) == false) //todo: isa: are dictionary thread safe?
        _cacheData.Add(key, data);
      else if (update == true)
        _cacheData[key] = data;
    
    }

    public static object GetData(string key)
    {
      if (_cacheData.ContainsKey(key) == false)
        return null;
      else
        return _cacheData[key];
    }
  }
}
