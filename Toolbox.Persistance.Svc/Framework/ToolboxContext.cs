using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Persistance.Svc.Model;
using Toolbox.Scheduler.Interfaces;

namespace Toolbox.Persistance.Svc.Framework
{
    public class ToolboxContext : BaseContext
    {
        // Fill this list using MEF - check for the IPluginContext interface on assemblies
        public List<IPluginContext> MefLoadedPlugins;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Versions>().ToTable("Versions", "Toolbox");
            modelBuilder.Entity<Versions>().HasKey(e => e.ID);

            // TODO : Load Mef plugins

            if (MefLoadedPlugins != null)
                foreach (var pluginContext in MefLoadedPlugins)
                {
                    //pluginContext.UpgradeDB()
                    pluginContext.Setup(modelBuilder, this);
                }
        }
    }
}
