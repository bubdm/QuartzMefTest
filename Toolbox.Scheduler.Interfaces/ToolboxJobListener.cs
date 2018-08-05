using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quartz;

namespace Toolbox.Scheduler.Interfaces
{
    public class ToolboxJobListener : IToolboxJobListener
    {
        public ToolboxJobListener()
        {
            Name = GetType().Name;
        }

        //public async Task JobToBeExecuted(IJobExecutionContext context,
        //    CancellationToken token = default(CancellationToken))
        //{
        //    await WriteMesssage("JobToBeExecuted", token);
        //}

        //public async Task JobExecutionVetoed(IJobExecutionContext context,
        //    CancellationToken token = default(CancellationToken))
        //{
        //    await WriteMesssage("JobExecutionVetoed", token);
        //}

        //public async Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException,
        //    CancellationToken token = default(CancellationToken))
        //{
        //    await WriteMesssage("JobWasExecuted", token);
        //}

        public string Name { get; set; }

        private void WriteMesssage(string message)
        {
            Console.WriteLine("{0}.{1}", GetType().Name, message);
        }

        public void JobToBeExecuted(IJobExecutionContext context)
        {
            WriteMesssage("JobToBeExecuted");
        }

        public void JobExecutionVetoed(IJobExecutionContext context)
        {
            WriteMesssage("JobExecutionVetoed");
        }

        public void JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException)
        {
            WriteMesssage("JobWasExecuted");
        }
    }
}
