using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Scheduler.Interfaces;

namespace Scheduler.Database
{
    public class ModuleContext : IPluginContext
    {
        public DbContext Context;

        public int CurrentVersion => 1;

        public void Setup(DbModelBuilder modelBuilder, DbContext context)
        {
            modelBuilder.Entity<AbstractTrigger>().ToTable("AbstractTrigger", "Scheduler");
            modelBuilder.Entity<AbstractTrigger>().HasKey(_c => _c.ID);

            modelBuilder.Entity<SimpleTrigger>().ToTable("SimpleTrigger", "Scheduler");
            modelBuilder.Entity<SimpleTrigger>().HasKey(_c => _c.ID);

            modelBuilder.Entity<CalendarTrigger>().ToTable("CalendarTrigger", "Scheduler");
            modelBuilder.Entity<CalendarTrigger>().HasKey(_c => _c.ID);

            Context = context;
        }

        public void UpgradeDB(int targetVersion)
        {
            // Upgrade databse with all version step
            switch (targetVersion)
            {
                case 1:
                    break;
            }
        }
    }
}
