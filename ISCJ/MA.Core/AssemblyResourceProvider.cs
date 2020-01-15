using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;

namespace MA.Core
{
    
    public abstract class AssemblyResourceProvider:IResourceProvider
    {
        private ResourceManager _mgr = null;

        public AssemblyResourceProvider(string baseName, string assemblyName)
        {
            var assembly = System.Reflection.Assembly.Load(assemblyName);

            //resource managers are per .resx files.
            _mgr = new ResourceManager(baseName, assembly); //when using this constructor, base name should be
            //just assembly name.resx file name.

        }

        public string GetString(string key)
        {
            return _mgr.GetString("logoName");
        }
    }

    
    public class FieldLabelResourceProvider : AssemblyResourceProvider
    {
        public FieldLabelResourceProvider(string baseName, string assemblyName) : base(baseName + "." + "FieldLabels",
            assemblyName)
        {
            ;
        }

        public FieldLabelResourceProvider(string assemblyName) : base(assemblyName + "." + "FieldLabels",
            assemblyName)
        {
            ;
        }
    }

    public class SmartAssemblyResourceProvider : IResourceProvider
    {
        private Assembly _assembly = null;
        public Dictionary<string, ResourceManager> _resouceManagers = new Dictionary<string, ResourceManager>();

        public SmartAssemblyResourceProvider(string assemblyName)
        {
            _assembly = System.Reflection.Assembly.Load(assemblyName);
            
        }

        public string GetString(string resourceKey)
        {
            string resourceName;
            string baseName = GetResourceBaseName(resourceKey, out resourceName);

            if (_resouceManagers.ContainsKey(baseName)==false)
            {
                lock (_resouceManagers)
                {
                  
                    ResourceManager mgr = new ResourceManager(baseName, _assembly);
                    _resouceManagers.Add(baseName, mgr);
                }
            }

            return _resouceManagers[baseName].GetString(resourceName);

        }

        private string GetResourceBaseName(string resourceKey, out string resourceName)
        {
            string[] parts = resourceKey.Split('.');
            string baseName = "";
            resourceName = parts[parts.Length - 1];
            for (int i = 0; i < parts.Length - 1; i++)
            {
                if (i < parts.Length - 2)
                {
                    baseName += parts[i] + ".";
                }
                else
                {
                    baseName += parts[i];
                }
            }

            return baseName;
        }
    }
}
