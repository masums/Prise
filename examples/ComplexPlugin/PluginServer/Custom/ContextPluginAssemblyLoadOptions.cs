using System.Linq;
using Microsoft.AspNetCore.Http;
using Prise.Infrastructure;
using Prise.Infrastructure.NetCore.Contracts;

namespace PluginServer.Custom
{
    public class ContextPluginAssemblyLoadOptions : ILocalAssemblyLoaderOptions
    {
        private readonly IHttpContextAccessor contextAccessor;
        public ContextPluginAssemblyLoadOptions(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }


        /// <summary>
        /// At this point the root path is /bin/debug/netcoreapp3.0/Plugins.
        /// Because we already have set the rootPath to {AppDomain.CurrentDomain.BaseDirectory}\\'Plugins'
        ///     there is no need to prefix this PluginPath with 'Plugins'.
        /// We just need to return the path to the dedicated plugin directory.
        /// After this, the ContextPluginAssemblyNameProvider will suffix the PluginPath with the correct assembly name.
        /// </summary>
        /// <value>The returned value will be /bin/debug/netcoreapp3.0/Plugins/PluginA, for example</value>
        public string PluginPath
        {
            get
            {
                var pluginType = this.contextAccessor.HttpContext.Request.Headers["PluginType"].First();
                return pluginType;
            }
        }

        public DependencyLoadPreference DependencyLoadPreference => DependencyLoadPreference.PreferDependencyContext;
    }
}