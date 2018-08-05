using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobSet2.Jobs
{
    public class Job21 : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello from Job 2.1");
        }
    }
}
