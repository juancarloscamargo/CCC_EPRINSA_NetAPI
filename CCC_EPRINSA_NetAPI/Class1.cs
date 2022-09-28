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
namespace QBM.CompositionApi
{
    using System;
    using System.Linq;
    using VI.Base;
    using VI.DB;
    using VI.DB.Entities;
    using VI.DB.Sync;
    using NLog;
    using QBM.CompositionApi.Definition;
    using QBM.CompositionApi.Dto;


    public class CCC_Eprinsa_API
    {

        private QBM.CompositionApi.Definition.MethodSet _project;


        public CCC_Eprinsa_API(VI.Base.IResolve resolver)
        {
            _project = new QBM.CompositionApi.Definition.MethodSet();
            _project.AppId = "apieprinsa";
            _project.Uid = "CCC-091601C5235CCA488AA2974CF9DBC726";
            _project.Configure(resolver, new QBM.CompositionApi.Definition.IApiProvider[0]);
            QBM.CompositionApi.Session.SessionAuthDbConfig authConfig = new QBM.CompositionApi.Session.SessionAuthDbConfig();
            authConfig.AuthenticationType = QBM.CompositionApi.Config.AuthType.Default;
            authConfig.Product = null;
            authConfig.AdditionalAuthProps = null;
            _project.SessionConfig = authConfig;
        }

        public QBM.CompositionApi.Definition.IMethodSet Project
        {
            get
            {
                return _project;
            }
        }
        public class Eprinsa : IApiProvider
        {
            public void Build(IApiBuilder builder)
            {



                builder.AddMethod(Method.Define("ObtenerDatosOTP")
                    // Insert the statement name (QBMLimitedSQL.Ident_QBMLimitedSQL) and the type
                    .HandleGetBySqlStatement("QER_CCC_Person_GetOTPOptions", SqlStatementType.SqlExecute)
                    .WithParameter("CentralAccount")
                    .AllowUnauthenticated()

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
}

  



