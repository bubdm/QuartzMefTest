using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JobSet2.Jobs;
using Quartz;
using Toolbox.Scheduler.Interfaces;

namespace JobSet2
{
    [Export(typeof(IToolboxFullJob))]
    public class JobSet21 : IToolboxFullJob
    {
        public string Description => "Test 2.1";

        public IJobDetail GenerateJobDetail()
        {
            IJobDetail job = JobBuilder.Create<Job21>()
                    .WithIdentity("job21", "Jobset2.Jobset21")
                    .Build();

            return job;
        }

        public IEnumerable<ITrigger> GenerateJobTriggers()
        {
            return new List<ITrigger>()
            {

                TriggerBuilder.Create()
                    .WithIdentity("trigger21", "Jobset2.Jobset21")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(10)
                        .RepeatForever())
                    .Build()
            };
        }
    }
}
