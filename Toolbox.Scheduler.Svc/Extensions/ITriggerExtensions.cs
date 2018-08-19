using Quartz;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Scheduler.Svc;

namespace Quartz
{
    public static class ITriggerExtensions
    {
        public static SimpleTrigger ToSimpleTrigger(this ITrigger trigger)
        {
            return new SimpleTrigger()
            {

            };
        }
    }
}
