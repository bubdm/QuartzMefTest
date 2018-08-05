using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzMefLib.Framework
{
    public class MefJobFactory : IJobFactory
    {
        public IJob NewJob(TriggerFiredBundle bundle)
        {
            try
            {
                //var jobs = Bootstrapper.Instance.Container.GetExports<IJob>();
                //return jobs.First(job => job.GetType() == bundle.JobDetail.JobType).Value;
                return null;
            }
            catch (Exception exception)
            {
                throw new SchedulerException("Problem instantiating class " + bundle.JobDetail.JobType, exception);
            }
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            throw new NotImplementedException();
        }

        public void ReturnJob(IJob job)
        {
            throw new NotImplementedException();
        }
    }

}
