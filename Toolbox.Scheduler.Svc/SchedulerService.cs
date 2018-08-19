using Quartz;
using Quartz.Impl;
using Quartz.Impl.Matchers;
using QuartzMefLib.Framework;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Toolbox.Scheduler.Svc
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class SchedulerService : ISchedulerService
    {
        private IScheduler scheduler;
        //private List<IJobDetail> Jobs = new List<IJobDetail>();
        //private List<ITrigger> Triggers = new List<ITrigger>();
        private List<AbstractTrigger> Triggers = new List<AbstractTrigger>();

        public NameValueCollection LocalConfig =>
            new NameValueCollection
        {
            { "quartz.scheduler.instanceName", "ToolboxScheduler" },
            { "quartz.scheduler.instanceId", "ToolboxScheduler" },
            { "quartz.threadPool.type", "Quartz.Simpl.SimpleThreadPool, Quartz" },
            { "quartz.threadPool.threadCount", "10" },
            { "quartz.jobStore.type", "Quartz.Impl.AdoJobStore.JobStoreTX, Quartz" },
            { "quartz.jobStore.useProperties", "true" },
            { "quartz.jobStore.dataSource", "default" },
            { "quartz.jobStore.tablePrefix", "QRTZ_" },
            { "quartz.jobStore.driverDelegateType", "Quartz.Impl.AdoJobStore.SQLiteDelegate, Quartz" },
            { "quartz.dataSource.default.provider", "SQLite-10" },
            { "quartz.dataSource.default.connectionString", "Data Source=ToolBox.Scheduler.db;Version=3;Foreign Keys=ON;" },
        };

        public SchedulerService()
        {
            try
            {
                // Init DB
                this.CreateQuartzNetTables();

                //Configure simple logging
                Common.Logging.LogManager.Adapter = new Common.Logging.Simple.ConsoleOutLoggerFactoryAdapter { Level = Common.Logging.LogLevel.Info };

                // Grab the Scheduler instance from the Factory 
                // Configure the Scheduler
                ISchedulerFactory schedFact = new StdSchedulerFactory(LocalConfig);

                // get a scheduler
                scheduler = schedFact.GetScheduler();

                // and start it off
                scheduler.Start();

                // get all job definition from MEF
                Importer importer = new Importer();
                importer.DoImport();

                // get jobs and triggers definition from all xml files by serialization
                importer.JobSets.ToList().ForEach(p =>
                {
                    var job = p.GenerateJobDetail();
                    //Jobs.Add(job);

                    var triggers = p.GenerateJobTriggers();
                    if (triggers != null)
                    {
                        foreach (var trig in triggers.ToList())
                        {
                            if (scheduler.GetTrigger(trig.Key) != null)
                            {
                                // if trigger exist in DB replace it because it may have change since last execution
                                scheduler.RescheduleJob(trig.Key, trig);
                            }
                            else
                            {
                                scheduler.ScheduleJob(job, trig);
                            }
                            Triggers.Add(trig.ToSimpleTrigger());
                        }
                    }
                });
            }
            catch (SchedulerException se)
            {
                Console.WriteLine(se);
            }

        }

        ~SchedulerService()
        {
            scheduler?.Shutdown();
        }

        public void AddTriggerToJob(AbstractTrigger trigger)
        {
            Triggers.Add(trigger);

            switch (trigger.TriggerType)
            {
                #region Simpletrigger
                case TriggerType.Simple:
                    var t = (SimpleTrigger)trigger;

                    var job = scheduler.GetJobDetail(new JobKey(t.JobName, t.JobGroupName));
                    if (job == null)
                    {
                        throw new Exception("Job not found");
                    }

                    // todo : Make all cases
                    var trig = TriggerBuilder.Create()
                       .WithIdentity(t.Name, t.GroupName);

                    if (t.StartDateUTC != null)
                        trig = trig.StartAt(new DateTimeOffset(t.StartDateUTC.Value));
                    else
                        trig = trig.StartNow();

                    if (t.EndDateUTC != null)
                        trig = trig.EndAt(new DateTimeOffset(t.EndDateUTC.Value));

                    trig = trig.WithSimpleSchedule(x =>
                    {
                        switch (t.SimpleTriggerType)
                        {
                            case SimpleTriggerType.second:
                                x = x.WithIntervalInSeconds(t.Interval);
                                break;
                            case SimpleTriggerType.minutes:
                                x = x.WithIntervalInMinutes(t.Interval);
                                break;
                            case SimpleTriggerType.hours:
                                x = x.WithIntervalInHours(t.Interval);
                                break;
                            case SimpleTriggerType.days:
                                x = x.WithIntervalInHours(t.Interval * 24);
                                break;
                            case SimpleTriggerType.weeks:
                                x = x.WithIntervalInHours(t.Interval * 24 * 7);
                                break;
                        }

                        if (t.RepeatCount == 0)
                            x = x.RepeatForever();
                        else
                            x = x.WithRepeatCount(t.RepeatCount);
                    });

                    var newTrigger = trig.Build();

                    if (scheduler.GetTrigger(newTrigger.Key) != null)
                    {
                        // if trigger exist in DB replace it
                        scheduler.RescheduleJob(newTrigger.Key, newTrigger);
                    }
                    else
                    {
                        scheduler.ScheduleJob(job, newTrigger);
                    }

                    break;
                #endregion

                case TriggerType.Calendar:

                    break;
            }

            throw new NotImplementedException();
        }

        //public IList<string> GetTriggersGroupsList()
        //{
        //    return scheduler.GetTriggerGroupNames();
        //}

        //public IEnumerable<AbstractTrigger> GetTriggerList(string TriggerGroupName)
        //{
        //    var allTriggerKeys = scheduler.GetTriggerKeys(GroupMatcher<TriggerKey>.AnyGroup());

        //    var result = new List<AbstractTrigger>();

        //    allTriggerKeys.ToList().ForEach(p =>
        //    {
        //        ITrigger trigger = scheduler.GetTrigger(p);

        //        AbstractTrigger trig;
        //        if (trigger.CalendarName == null)
        //        {
        //            var internalTrig = (ISimpleTrigger)trigger;
        //            trig = new SimpleTrigger()
        //            {
        //                GroupName = internalTrig.Key.Group,
        //                Name = internalTrig.Key.Name,
        //                EndDateUTC = internalTrig.EndTimeUtc?.DateTime,
        //                StartDateUTC = internalTrig.StartTimeUtc.DateTime,
        //                JobGroupName = internalTrig.JobKey.Group,
        //                JobName = internalTrig.JobKey.Name,
        //                TriggerType = TriggerType.Simple,
        //                RepeatCount = internalTrig.RepeatCount,
        //            };
        //        }
        //        else
        //        {
        //            trig = new CalendarTrigger()
        //            {
        //            };
        //        }

        //        result.Add(trig);
        //    });

        //    return result;
        //}

        public IEnumerable<AbstractTrigger> GetTriggerList(string jobGroupName, string jobName)
        {
            var result = Triggers;

            if (!string.IsNullOrEmpty(jobGroupName))
            {
                result = result.Where(p => p.JobGroupName == jobGroupName).ToList();
            }

            if (!string.IsNullOrEmpty(jobName))
            {
                result = result.Where(p => p.JobName == jobName).ToList();
            }

            return result;
        }

        private void CreateQuartzNetTables()
        {
            if (!File.Exists("ToolBox.Scheduler.db"))
            {
                using (SQLiteConnection connection = new SQLiteConnection(@"Data Source=ToolBox.Scheduler.db;Version=3;Foreign Keys=ON;"))
                {
                    connection.Open();
                    using (SQLiteCommand command = connection.CreateCommand())
                    {
                        command.CommandType = System.Data.CommandType.Text;
                        command.CommandText = File.ReadAllText("tables_sqlite.sql");
                        command.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
        }
    }
}

