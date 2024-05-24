

using VI.Base;

using QBM.CompositionApi.Crud;
using VI.DB.Entities;


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

using QBM.CompositionApi.ApiManager;
using QBM.CompositionApi.Definition;
using QBM.CompositionApi.DataSources;
using VI.Base.Logging;

[assembly: QBM.CompositionApi.PlugIns.Module("CCC")]

namespace QBM.CompositionApi
{
    public class APIEprinsaRest : IApiProviderFor<QER.CompositionApi.Portal.PortalApiProject>
    {
        public void Build(IApiBuilder builder)
        {






            builder.AddMethod(Method.Define("solicitudotp")
                    .AllowUnauthenticated(true)
                    .WithParameter("OTP_Usuario", typeof(string), isInQuery: true)
                    .WithParameter("OTP_Metodo_Dato", typeof(string), isInQuery: true)
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


            builder.AddMethod(Method.Define("targetsystem/gappasku/GAPUserLicense")
                .FromTable("GAPUserInPaSku")
                .EnableRead()
                .WithAllColumns());

            


            builder.AddMethod(Method
                .Define("targetsystem/gapuser/nuevaCuenta")
                .FromTable("GAPUser")
                .EnableRead()
                .WithAllColumns()
                .With(
                    m => m.Crud.EntityType = EntityType.Interactive
                 )
                .With(
                    m =>
                    {
                        m.EnableDataModelApi = true;
                        m.EnableGroupingApi = true;
                    }
                )
                   .WithCalculatedProperties(new FkProperty(

                    // Property name for the client data model
                    "IdentidadAsociada",

                    // Foreign-key parent table name
                    "Person",

                    // Columna en la tabla destino
                    "UID_Person",

                    // Columna en la tabla origen
                    "UID_Person")
                   )
                .EnableUpdate()
                .EnableCreate()
                .WithWritableAllColumns()
             );





        }
    }
    // This class defines the type of data object that will be sent to the client.
    public class DataObject
    {
        public string Message { get; set; }
    }

    // This class defines the type of data object that will be sent from the client to the server.
    public class PostedMessage
    {
        public string Input { get; set; }
    }

}