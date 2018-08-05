using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Toolbox.Scheduler.Interfaces;

namespace JobSet1
{
    public class Job12 : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello from Job 1.2");
        }
    }
}
