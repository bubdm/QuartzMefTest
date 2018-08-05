using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Scheduler.Svc.DTO
{
    public enum TriggerType { Simple, Calendar }

    [DataContract]
    [KnownType(typeof(CalendarTrigger))]
    [KnownType(typeof(SimpleTrigger))]
    public abstract class AbstractTrigger
    {
        public string GroupName { get; set; }
        public string Name { get; set; }
        public string JobGroupName { get; set; }
        public string JobName { get; set; }

        public Dictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// If null, start now
        /// </summary>
        public DateTime? StartDateUTC { get; set; }
        public DateTime? EndDateUTC { get; set; }
        /// <summary>
        /// If 0 repeat forever
        /// </summary>
        public int RepeatCount { get; set; }
        // TODO : Replace by job GUID maybe
        public TriggerType TriggerType { get; set; }

    }
}
