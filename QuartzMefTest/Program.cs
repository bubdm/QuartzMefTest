using Quartz;
using Quartz.Impl;
using QuartzMefLib.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QuartzMefTest
{
    class Program
    {
        static void Main(string[] args)
        {
            List<IJobDetail> Jobs = new List<IJobDetail>();
            List<ITrigger> Triggers = new List<ITrigger>();

            try
            {

                //Configure simple logging
                Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };

                // Grab the Scheduler instance from the Factory 
                IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();

                // and start it off
                scheduler.Start();

                // get all job definition from MEF
                Importer importer = new Importer();
                importer.DoImport();

                // get jobs and triggers definition from all xml files by serialization
                List<IJobDetail> jobsDetails = new List<IJobDetail>();
                importer.JobSets.ToList().ForEach(p =>
                {
                    var job = p.GenerateJobDetail();
                    Jobs.Add(job);

                    var triggers = p.GenerateJobTriggers();
                    if (triggers != null)
                    {
                        foreach (var trig in triggers)
                        {
                            scheduler.ScheduleJob(job, trig);
                            Triggers.Add(trig);
                        }
                    }
                });

                // some sleep to show what's happening
                Thread.Sleep(TimeSpan.FromSeconds(60));

                // and last shut down the scheduler when you are ready to close your program
                scheduler.Shutdown();
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }


            Console.WriteLine("Press any key to close the application");
            Console.ReadKey();
        }
    }
}
