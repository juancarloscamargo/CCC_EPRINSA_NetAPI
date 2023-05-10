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
using VI.DB.Implementation;
using VI.DB.Sync;
using System.Reflection.Emit;
using VI.DB.JobGeneration;
using System.Collections.Generic;

public class APIEprinsaRest : IApiProviderFor<EprinsaAPI>
{
    public void Build(IApiBuilder builder)
    {



        builder.AddMethod(Method.Define("ObtenerDatosOTP")
        // Insert the statement name (QBMLimitedSQL.Ident_QBMLimitedSQL) and the type
             .HandleGetBySqlStatement("QER_CCC_Person_GetSecondaryEmailAddress", SqlStatementType.SqlExecute)
             .WithParameter("CentralAccount")
             .WithResultColumns(new SqlResultColumn("CCC_SecondaryEmailAddress", ValType.String),
                                   new SqlResultColumn("PhoneMobile", ValType.String),
                                   new SqlResultColumn("CustomProperty08", ValType.String),
                                   new SqlResultColumn("CustomProperty04", ValType.String))
        );
                


        builder.AddMethod(Method.Define("obtenerDatosOTP2")
                .WithParameter("CentralAccoount")
                .FromTable("Person")
                .EnableRead()
                
                // Only include specific columns in the result.
                .WithResultColumns("CCC_SecondaryEmailAddress", "PhoneMobile","CustomProperty08","CustomProperty04"));



        builder.AddMethod(Method.Define("solicitudotp")
                .WithParameter("OTP_Usuario",typeof(string),isInQuery:false)
                .WithParameter("OTP_Metodo_Dato",typeof(string),isInQuery:false)
                .HandleGet(async qr =>
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
                        qr.Parameters.Get<string>("OTP_Metodo_Dato"),

                    };

                    
                    return runner.Eval("CCC_EPRINSA_RespondeSolicitudOTP91", parameters) as string;
                })); ;

        builder.AddMethod(Method.Define("reload")
              .HandleGet(async (qr, ct) =>
                {
                    await qr.Resolver.Resolve<IBranchedMethodSetsProvider>().ReinitializeApiAsync(ct)
                    .ConfigureAwait(false);
                    return "API reloaded";
                }));


        builder.AddMethod(Method.Define("GeneraOTPDatUsu")
                .HandleGet(async qr =>
                {
                    // Load the Person entity for the authenticated user.
                    // Note that methods can only be called in interactive entities.
                    var connexion = new DbObjectKey("CCC_EPRINSA_AccesoAuth", "6e562265-2119-484b-b57a-9f54aa66a2e4");
                    var datoconexion = await qr.Session.Source().GetAsync(new DbObjectKey("CCC_EPRINSA_AccesoAuth", "6e562265-2119-484b-b57a-9f54aa66a2e4"),
                            EntityLoadType.Interactive)
                        .ConfigureAwait(false);
                    
                    var runner = qr.Session.StartUnitOfWork();
                    IDictionary<string, object> parametros = new Dictionary<string, object>();
                    parametros.Add("OTP_Usuario", "juancar");
                    parametros.Add("OTP_Metodo", "1");
                    parametros.Add("OTP_Metodo_Dato", "juancarlos.camargo@gmail.com");

                    runner.Generate(datoconexion, "Evt_Enviar_OTP",parametros);
                    runner.Commit();
                    
                    
                }));

    }
}
public class DataObject
{
    public string Message { get; set; }
}