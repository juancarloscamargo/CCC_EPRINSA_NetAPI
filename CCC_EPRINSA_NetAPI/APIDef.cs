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
    public class PluginMethodSetProvider : IPlugInMethodSetProvider
    {
        public IMethodSetProvider Build(IResolve resolver)
        {
            return new EprinsaAPI(resolver);
        }
    }

    internal class EprinsaAPI : IMethodSetProvider
    {
        private readonly IResolve _resolver;

        public EprinsaAPI(IResolve resolver)
        {
            _resolver = resolver;
        }

        public Task<IEnumerable<IMethodSet>> GetMethodSetsAsync(CancellationToken ct = new CancellationToken())
        {
            var methodSet = new MethodSet
            {
                AppId = "EprinsaCVU",
                SessionConfig = new SessionAuthDbConfig { AuthenticationType = AuthType.FixedCredentials}



            };

            // Include all classes in this assembly that implement IApiProvider
            methodSet.Configure(_resolver, Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => typeof(IApiProvider).IsAssignableFrom(t))
                .OrderBy(t => t.FullName)
                .Select(t => (IApiProvider)t.GetConstructor(new Type[0]).Invoke(new object[0])));

            return Task.FromResult<IEnumerable<IMethodSet>>(new[]
            {
                methodSet
            });
        }
    }
}
