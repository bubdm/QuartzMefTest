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
    public class RemindersUpdateJob : IToolboxFullJob
    {
        public string Description => "Apps module - Reminder for mandatories updates pending";

        public IJobDetail GenerateJobDetail()
        {
            IJobDetail job = JobBuilder.Create<RemindersUpdateJobImpl>()
                    .WithIdentity("ReminderUpdate", "ToolBox.Apps")
                    .Build();

            return job;
        }

        public IEnumerable<ITrigger> GenerateJobTriggers()
        {
            return null;
        }
    }

    internal class RemindersUpdateJobImpl : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            // notify for remind update available based on trigger message parameter
            var message = context.Trigger.JobDataMap.First(p => p.Key == "MessageParameters").Value;

            // Call notification popup with good parameters (mediator)
        }
    }
}
