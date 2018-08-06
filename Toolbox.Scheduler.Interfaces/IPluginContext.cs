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
        void Setup(DbModelBuilder modelBuilder);
    }
}
