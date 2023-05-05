using QBM.CompositionApi.Definition;

using VI.Base;
using QBM.CompositionApi.ApiManager;
using QBM.CompositionApi.Crud;
using VI.DB.Entities;
using QBM.CompositionApi;
using QBM.CompositionApi.Definition;
using System.Linq;
using System.Threading.Tasks;
using VI.DB;

using System;
using VI.DB.Compatibility;
using System.Security.Cryptography;
using VI.DB.Scripting;

public class APIEprinsaRest : IApiProviderFor<EprinsaAPI>
{
    public void Build(IApiBuilder builder)
    {



        builder.AddMethod(Method.Define("sql/ObtenerDatosOTP")
        // Insert the statement name (QBMLimitedSQL.Ident_QBMLimitedSQL) and the type
             .HandleGetBySqlStatement("QER_CCC_Person_GetSecondaryEmailAddress", SqlStatementType.SqlExecute)
             .WithParameter("CentralAccount")


        // Define the result schema columns and data type
                .WithResultColumns(
          new SqlResultColumn("CCC_SecondaryEmailAddress", ValType.String),
          //new SqlResultColumn("PhoneMobile", ValType.String),
          new SqlResultColumn("CustomProperty08", ValType.String),
          new SqlResultColumn("CustomProperty04", ValType.String)
                        ));

        builder.AddMethod(Method.Define("gestionOTP")
                .HandleGet(async qr =>
                {
                    // Load the Person entity for the authenticated user.
                    // Note that methods can only be called in interactive entities.
                    var connexiondb = await qr.Session.Source().GetAsync(new DbObjectKey("CCC_EPRINSA_AccesoAuth", "6e562265-2119-484b-b57a-9f54aa66a2e4"),
                            EntityLoadType.Interactive)
                        .ConfigureAwait(false);

                    // Load the GetCulture method. This one does not take any parameters.


                    // Call the method and return the result (in this case, it's a string).
//                    var DB = new VI.DB.DbObjectKey(connexiondb, UID);
 //                  var result = await VI.DB.JobGeneration.JobGen.Generate(connexiondb, "as");
                    

                    var parms = new System.Collections.Hashtable();

                    parms.Add("OTP_Usuario", "juancar");
                    parms.Add("OTP_Metodo", "1");
                    parms.Add("OTP_Metodo_Dato", "juancarlos.camargo@gmail.com");
     //               return result;
                }));

        builder.AddMethod(Method.Define("getinitials/{OTP_Usuario}/{OTP_Metodo}/{OTO_Metodo_Dato}")
                .WithParameter("OTP_Usuario", typeof(string), isInQuery: false)
                .WithParameter("OTP_Metodo", typeof(int), isInQuery: false)
                .WithParameter("OTP_Metodo_Dato",typeof(string),isInQuery: false)
                .HandleGet(qr =>
                {
                    // Setup the script runner
                    var scriptClass = qr.Session.Scripts().GetScriptClass(ScriptContext.Scripts);
                    var runner = new ScriptRunner(scriptClass, qr.Session);

                    // Add any script input parameters to this array.
                    // In this example, the script parameters are defined as
                    // URL parameters, and their values must be supplied
                    // by the client. This does not have to be the case.
                    var parameters = new object[]
                    {
                        qr.Parameters.Get<string>("OTP_Usuario"),
                        qr.Parameters.Get<string>("OTP_Metodo"),
                        qr.Parameters.Get<string>("OTP_Metodo_Dato"),

                    };

                    var parms = new System.Collections.Hashtable();

                    parms.Add("OTP_Usuario", "juancar");
                    parms.Add("OTP_Metodo", 1);
                    parms.Add("OTP_Metodo_Dato", "juancarlos.camargo@gmail.com");
                    // This assumes that the script returns a string.
                    return runner.Eval("CCC_EPRINSA_RespondeSolicitudOTP", parameters) as string;
                }));

    }
}
public class DataObject
{
    public string Message { get; set; }
}