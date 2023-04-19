using QBM.CompositionApi.Definition;

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VI.Base;


using QBM.CompositionApi.ApiManager;

using QBM.CompositionApi.Captcha;
using QBM.CompositionApi.Config;
using QBM.CompositionApi.Session;
using System.Reflection;
using System;


namespace QBM.CompositionApi
{
    public class EprinsaAPI : IMethodSetProvider
    {
        private readonly MethodSet _project;

        public EprinsaAPI(IResolve resolver)
        {
            _project = new MethodSet
            {
                AppId = "EprinsaAPI"
            };

            var svc = resolver.Resolve<IExtensibilityService>();

            // Configure all API providers that implement IApiProviderFor<CustomApiProject>
            var apiProvidersByAttribute = svc.FindAttributeBasedApiProviders<EprinsaAPI>();
            _project.Configure(resolver, apiProvidersByAttribute);

            var authConfig = new Session.SessionAuthDbConfig
            {
                AuthenticationType = Config.AuthType.AllManualModules,
                //Product = "WebDesigner",
                SsoAuthentifiers =
                {
                    // Add the names of any single-sign-on authentifiers here
                },
                ExcludedAuthentifiers =
                {
                    // Add the names of any excluded authentifiers here
                }
            };

            // To explicitly set the list allowed authentication modules,
            // set the AuthenticationType to AuthType.Default and set
            // the list of ManualAuthentifiers.

            _project.SessionConfig = authConfig;
        }

        public Task<IEnumerable<IMethodSet>> GetMethodSetsAsync(CancellationToken ct = new CancellationToken())
        {
            return Task.FromResult<IEnumerable<IMethodSet>>(new[] { _project });
        }
    }

}



