using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Scheduler.Interfaces;

namespace Apps.Database
{
    public class ModuleContext : IPluginContext
    {
        public void Setup(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Triggers>().ToTable("Triggers", "Scheduler");
            modelBuilder.Entity<Triggers>().HasKey(_c => _c.ID);
        }
    }
}
