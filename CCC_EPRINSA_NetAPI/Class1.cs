using QBM.CompositionApi.Definition;

using System;
using VI.Base;
using QBM.CompositionApi.ApiManager;
using QER.CompositionApi.Portal;




namespace QBM.CompositionApi
{
    public class Eprinsa : IApiProviderFor<PortalApiProject>
    {
        public void Build(IApiBuilder builder)
        {
            builder.AddMethod(Method.Define("ccc/ObtenerDatosOTP")
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
