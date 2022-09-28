using QBM.CompositionApi.Definition;
using VI.Base;
using QBM.CompositionApi.ApiManager;
using QBM.CompositionApi.Crud;
using VI.DB.Entities;

public class Eprinsa : IApiProvider
{
    public void Build(IApiBuilder builder)
    {



        builder.AddMethod(Method.Define("ObtenerDatosOTP")
            // Insert the statement name (QBMLimitedSQL.Ident_QBMLimitedSQL) and the type
            .AllowUnauthenticated()
            .HandleGetBySqlStatement("QER_CCC_Person_GetOTPOptions", SqlStatementType.SqlExecute)
            //.WithParameter("CentralAccount")
            

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

        builder.AddMethod(Method.Define("testvalidation")
            .AllowUnauthenticated()
            .WithParameter("p", description: "Parameter must be 'a' or 'b'")
            .WithValidator("Parameter validation", qr =>
                {
                    var value = qr.Parameters.Get<string>("p");
                    if (value != "a" && value != "b")
                    return new ValidationError("Invalid value for parameter");
                return null;
                })
            .HandleGet(qr => new DataObject { Message = "Hello world!" }));

        builder.AddMethod(Method.Define("example")
               .AllowUnauthenticated()
               .FromTable("Person")
               .EnableRead()
               .WithClause(new LimitedSqlWhereClause("CCC-91A6C808737E344FA0502A4F938B9FD7")));
    }
}
public class DataObject
{
    public string Message { get; set; }
}