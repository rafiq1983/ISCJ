using System;
using System.Collections.Generic;
using System.Text;

namespace MA.Core
{
    public interface IResourceProviderFactory
    {
    }

    public class ResourceServiceProviderFactory:IResourceProviderFactory
    {
         //TODO: See how ILoggerFactory works.  Basically, I want to inject a list of IResourceProviders to the ResourceService

    }


}
