

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
using VI.Base.Encryption;

[assembly: QBM.CompositionApi.PlugIns.Module("CCC")]
class Licencias
{
    string EnterpriseStarter;
    string FrontlineStarter;
    string BusinesStandard;
    string BusinessPlus;
    string CloudIdentity;
}



namespace QBM.CompositionApi
{
    

    public class APIEprinsaRest : IApiProviderFor<QER.CompositionApi.Portal.PortalApiProject>
    {
        public void Build(IApiBuilder builder)
        {

                                    
            builder.AddMethod(Method.Define("ccc/resetGAP")
                    .WithParameter("GAPXObjectKey", typeof(string), isInQuery: true)
                    .HandleGet(qr =>
                    {
                        // Setup the script runner
                        var scriptClass = qr.Session.Scripts().GetScriptClass(ScriptContext.Scripts);
                        var runner = new ScriptRunner(scriptClass, qr.Session);
                        var parameters = new object[]
                        {
                        qr.Parameters.Get<string>("GAPXObjectKey"),
                        "Password"
                        };

                        
                        return runner.Eval("CCC_EPRINSA_ResetGAPPassword", parameters) as string;
                    }));

            builder.AddMethod(Method.Define("ccc/GAPStockLicencias")
                    
                    .HandleGet(qr =>
                    {
                        // Setup the script runner
                        var scriptClass = qr.Session.Scripts().GetScriptClass(ScriptContext.Scripts);
                        var runner = new ScriptRunner(scriptClass, qr.Session);                    
                        return runner.Eval("CCC_EPRINSA_ResetGAPPassword") as Licencias;
                    }));

            builder.AddMethod(Method.Define("ccc/GAPUserLicense")
                .FromTable("GAPUserInPaSku")
                .EnableRead()
                .WithAllColumns());

            


            builder.AddMethod(Method
                .Define("ccc/nuevaCuenta")
                .FromTable("GAPUser")                
                .EnableRead()                
                   .WithAllColumns()
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
                .With(
                    m =>
                    {
                        m.EnableDataModelApi = true;
                        m.EnableGroupingApi = true;
                    }
                )
                .EnableUpdate()
                  .WithWritableAllColumns()
                  .With(
                       m => m.Crud.EntityType = EntityType.Interactive
                   )
                    
                .EnableCreate()
                    .WithWritableAllColumns()
             );

        



         }
    }
    

}