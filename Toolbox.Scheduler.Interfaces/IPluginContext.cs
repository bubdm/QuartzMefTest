using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Scheduler.Interfaces
{
    public interface IPluginContext
    {
        int CurrentVersion { get; }

        void Setup(DbModelBuilder modelBuilder, DbContext context);

        void UpgradeDB(int targetVersion);
    }
}
