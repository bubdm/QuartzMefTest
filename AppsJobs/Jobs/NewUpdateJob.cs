using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;
using Toolbox.Scheduler.Interfaces;

namespace ToolBoxCoreJobs.Jobs
{
    [Export(typeof(IToolboxFullJob))]
    public class NewUpdateJob : IToolboxFullJob
    {
        public string Description => "Apps module - Check if new updates are availaible";

        public IJobDetail GenerateJobDetail()
        {
            IJobDetail job = JobBuilder.Create<NewUpdateJobImpl>()
                    .WithIdentity("CheckNewUpdate", "ToolBox.Apps")
                    .Build();

            return job;
        }

        public IEnumerable<ITrigger> GenerateJobTriggers()
        {
            return new List<ITrigger>()
            {
                TriggerBuilder.Create()
                    .WithIdentity("CheckNewUpdateTrigger", "ToolBox.Apps")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(8)
                        .RepeatForever())
                    .Build()
            };
        }
    }

    internal class NewUpdateJobImpl : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            // check for new update
            // notify if new update
        }
    }
}
