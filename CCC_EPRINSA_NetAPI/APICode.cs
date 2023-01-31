using QBM.CompositionApi.Definition;

using VI.Base;
using QBM.CompositionApi.ApiManager;
using QBM.CompositionApi.Crud;
using VI.DB.Entities;

public class APIEprinsaRest : IApiProvider  
{
    public void Build(IApiBuilder builder)
    {



        builder.AddMethod(Method.Define("sql/ObtenerDatosOTP")
        // Insert the statement name (QBMLimitedSQL.Ident_QBMLimitedSQL) and the type
                 .AllowUnauthenticated()
             .HandleGetBySqlStatement("QER_CCC_Person_GetSecondaryEmailAddress", SqlStatementType.SqlExecute)
             .WithParameter("CentralAccount")


        // Define the result schema columns and data type
                .WithResultColumns(
          new SqlResultColumn("CCC_SecondaryEmailAddress", ValType.String)
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

        

        builder.AddMethod(Method.Define("sql/changecount")

    // Insert the statement name (QBMLimitedSQL.Ident_QBMLimitedSQL) and the type
    .HandleGetBySqlStatement("SystemConfig_ChangeCount", SqlStatementType.SqlExecute)

    // Define the result schema columns and data types
    .WithResultColumns(new SqlResultColumn("TableName", ValType.String),
        new SqlResultColumn("Count", ValType.Int)));

    }
}
public class DataObject
{
    public string Message { get; set; }
}