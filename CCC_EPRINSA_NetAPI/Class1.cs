using QBM.CompositionApi.Definition;
using System;
using VI.Base;


namespace QBM.CompositionApi.EPRINSA
{
    public class BasicSqlMethod : IApiProvider
    {
        public void Build(IApiBuilder builder)
        {
            builder.AddMethod(Method.Define("sql/ObtenerDatosOTP")

                // Insert the statement name (QBMLimitedSQL.Ident_QBMLimitedSQL) and the type
                .HandleGetBySqlStatement("QER_CCC_Person_GetOTPOptions", SqlStatementType.SqlExecute)
                .WithParameter("CentralAccount")

                // Define the result schema columns and data types
                .WithResultColumns(
                new SqlResultColumn("CCC_SecondaryEmailAddress", ValType.String),
                new SqlResultColumn("PhoneMobile", ValType.String),
                new SqlResultColumn("CustomProperty08", ValType.String),
                new SqlResultColumn("CustomProperty04", ValType.String)
                ));

        }
    }
}
