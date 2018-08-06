using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Scheduler.Interfaces;

namespace Toolbox.Persistance.Svc.Framework
{
    public class Context : BaseContext
    {
        // Fill this list using MEF - check for the IPluginContext interface on assemblies
        public List<IPluginContext> MefLoadedPlugins;

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ClassA>().ToTable("TableB", "Schema_1");
            //modelBuilder.Entity<ClassA>().HasKey(_a => _a.Id);

            //modelBuilder.Entity<ClassB>().ToTable("TableB", "Schema_1");
            //modelBuilder.Entity<ClassB>().HasKey(_b => _b.Id);

            // TODO : Load Mef plugins

            if (MefLoadedPlugins != null)
                foreach (var pluginContext in MefLoadedPlugins)
                    pluginContext.Setup(modelBuilder);
        }
    }
}
