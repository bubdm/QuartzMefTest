using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Scheduler.Interfaces
{
    public abstract class ToolboxJob : IJob, IDisposable
    {
        public abstract void Dispose();

        public abstract void Execute(IJobExecutionContext context);
        
    }
}
