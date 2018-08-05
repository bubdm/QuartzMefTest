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
    [Export(typeof(IToolboxFullJob))]
    public class JobSet12 : IToolboxFullJob
    {
        public IJobDetail GenerateJobDetail()
        {
            IJobDetail job = JobBuilder.Create<Job12>()
                    .WithIdentity("job12", "Jobset1.Jobset12")
                    .Build();

            return job;
        }

        public IEnumerable<ITrigger> GenerateJobTriggers()
        {
            return null;
        }
    }
}
