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
    public class JobSet11 : IToolboxFullJob
    {
        public string Description => "Test 1.1";

        public IJobDetail GenerateJobDetail()
        {
            IJobDetail job = JobBuilder.Create<Job11>()
                    .WithIdentity("job11", "Jobset1.Jobset11")
                    .Build();

            return job;
        }

        public IEnumerable<ITrigger> GenerateJobTriggers()
        {
            return new List<ITrigger>()
            {

                TriggerBuilder.Create()
                    .WithIdentity("trigger11", "Jobset1.Jobset21")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(5)
                        .RepeatForever())
                    .Build()
            };
        }
    }
}
