using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.Database
{
    [DataContract]
    public enum SimpleTriggerType { second, minutes, hours, days, weeks }

    [DataContract]
    public class SimpleTrigger : AbstractTrigger
    {
        public SimpleTriggerType SimpleTriggerType { get; set; }
        public int Interval { get; set; }

        //public SimpleTrigger ToDTO(this ITrigger trig)
        //{
        //    return new SimpleTrigger()
        //    {
        //    };
        //}

        //public ITrigger FromDTO(ITrigger trig)
        //{
        //    return new SimpleTrigger()
        //    {
        //    };
        //}
    }
}
