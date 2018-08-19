using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Toolbox.Scheduler.Svc
{
    [ServiceContract]
    public interface ISchedulerService
    {
        [OperationContract]
        void AddTriggerToJob(AbstractTrigger trigger);

        [OperationContract]
        IEnumerable<AbstractTrigger> GetTriggerList(string jobGroupName, string jobName);
    }
}
