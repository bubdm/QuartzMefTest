using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Scheduler.Interfaces
{
    public interface IToolboxFullJob
    {
        IJobDetail GenerateJobDetail();
        IEnumerable<ITrigger> GenerateJobTriggers();
        string Description { get; }
    }
}
