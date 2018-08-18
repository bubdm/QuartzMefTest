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
    public class AutoUpdateJob : IToolboxFullJob
    {
        public string Description => "Core module - Check if an update is available for all modules";

        public IJobDetail GenerateJobDetail()
        {
            IJobDetail job = JobBuilder.Create<AutoUpdateJobImpl>()
                    .WithIdentity("AutoUpdate", "ToolBox.Core")
                    .Build();

            return job;
        }

        public IEnumerable<ITrigger> GenerateJobTriggers()
        {
            return new List<ITrigger>()
            {
                TriggerBuilder.Create()
                    .WithIdentity("AutoUpdateTrigger", "ToolBox.Core")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(12)
                        .RepeatForever())
                    .Build()
            };
        }
    }

    internal class AutoUpdateJobImpl : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            // check for new update
            // install if new update
        }
    }
}
