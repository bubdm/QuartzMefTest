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
    public class ScheduleInstallJob : IToolboxFullJob
    {
        public string Description => "Apps module - Client job, install application scheduled by user";

        public IJobDetail GenerateJobDetail()
        {
            IJobDetail job = JobBuilder.Create<ScheduleInstallJobImpl>()
                    .WithIdentity("ScheduleInstall", "ToolBox.Apps")
                    .Build();

            return job;
        }

        public IEnumerable<ITrigger> GenerateJobTriggers()
        {
            return null;
        }
    }

    internal class ScheduleInstallJobImpl : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            // install application in parameters
            var apps = context.Trigger.JobDataMap.First(p => p.Key == "AppList").Value;

            // Call installation with apps parameter
        }
    }
}
