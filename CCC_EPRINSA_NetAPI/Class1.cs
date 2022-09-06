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




namespace QER.CompositionApi.Eprinsa 
{

    public class CCC_PasswordPortalApiProject : IMethodSetProvider
    {
        private class _ConfigureApiProject : IApiProvider
        {
            public void Build(IApiBuilder builder)
            {
                builder.Resolver.Resolve<IMethodSetSettings>().ThrowExceptionOnUnknownUrlParameters = true;
            }
        }
        private readonly MethodSet _project;
        public CCC_PasswordPortalApiProject(IResolve resolver)
        {
            _project = new MethodSet
            {
                AppId = "passwordreset",
                Module = "QER"
            };
            IApiProvider[] second = resolver.Resolve<IExtensibilityService>().FindAttributeBasedApiProviders<CCC_PasswordPortalApiProject>();
            _project.Configure(resolver, new IApiProvider[3]
            {
                new _ConfigureApiProject(),
                new RecaptchaApiProvider(),
                new MultiLanguageConfigApi()
            }.Concat(second));
            IApiServerConfig config = resolver.Resolve<IApiServerConfig>();
        }

        public Task<IEnumerable<IMethodSet>> GetMethodSetsAsync(CancellationToken ct = default)
        {
            throw new System.NotImplementedException();
        }
    }

    



    public class Eprinsa : IApiProviderFor<CCC_PasswordPortalApiProject>
        {
            public void Build(IApiBuilder builder)
            {



                builder.AddMethod(Method.Define("ObtenerDatosOTP")
                    // Insert the statement name (QBMLimitedSQL.Ident_QBMLimitedSQL) and the type
                    .HandleGetBySqlStatement("QER_CCC_Person_GetOTPOptions", SqlStatementType.SqlExecute)
                    .WithParameter("CentralAccount")
                    //.AllowUnauthenticated()

                    // Define the result schema columns and data types
                    .WithResultColumns(
                    new SqlResultColumn("CCC_SecondaryEmailAddress", ValType.String),
                    new SqlResultColumn("PhoneMobile", ValType.String),
                    new SqlResultColumn("CustomProperty08", ValType.String),
                    new SqlResultColumn("CustomProperty04", ValType.String)
                    ));

                builder.AddMethod(Method.Define("helloworld")
                   .AllowUnauthenticated()
                   .HandleGet(qr => new DataObject { Message = "Hello world!" }));


            }
        }
        public class DataObject
        {
            public string Message { get; set; }
        }
    }
