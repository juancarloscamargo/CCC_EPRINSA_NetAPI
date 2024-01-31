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

public class APIEprinsaRest : IApiProviderFor<QER.CompositionApi.Portal.PortalApiProject>
{
    public void Build(IApiBuilder builder)
    {



        


        builder.AddMethod(Method.Define("solicitudotp")
                .AllowUnauthenticated(true)
                .WithParameter("OTP_Usuario",typeof(string),isInQuery:true)
                .WithParameter("OTP_Metodo_Dato",typeof(string),isInQuery:true)
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
                        qr.Parameters.Get<string>("OTP_Metodo_Dato"),

                    };

                    
                    return runner.Eval("CCC_EPRINSA_RespondeSolicitudOTP91", parameters) as string;
                })); ;

        

    }
}
