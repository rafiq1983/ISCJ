using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Core
{
    public class ResourceService
    {
        private List<IResourceProvider> _providers = null;

        public ResourceService(List<IResourceProvider> providers)
        {
            _providers = providers;
        }


        public string GetString(string key)
        {
            return _providers[0].GetString(key);
        }

        public string GetLabelName(string labelKey)
        {
            for (int i = 0; i < _providers.Count; i++)
            {
                string value = _providers[i].GetString(labelKey);

                if (string.IsNullOrEmpty(value) == false)
                {
                    return value;
                }
            }

            return labelKey;//return same value if not found.
        }

    }
}
