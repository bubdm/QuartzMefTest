using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace QuartzMefLib.Model
{
    public class TriggerXml
    {
        public string Name { get; set; }
        public string GroupName { get; set; }
        public int ScheduleIntervaleInSeconds { get; set; }

        /// <summary>
        /// Number of repetitions, 0 is forever
        /// </summary>
        public int RepeatCount { get; set; } 
        public string JobName { get; set; }
        public string JobGroupName { get; set; }

        public string Description { get; set; }

        public static void SaveSample(string filename)
        {
            var obj = new TriggerXml()
            {
                Description = "description",
                GroupName = "GroupName",
                JobGroupName = "JobGroupName",
                JobName = "JobName",
                Name = "Name",
                RepeatCount = 0,
                ScheduleIntervaleInSeconds = 180
            };

            XmlSerializer xs = new XmlSerializer(typeof(TriggerXml));
            using (StreamWriter wr = new StreamWriter(filename))
            {
                xs.Serialize(wr, obj);
            }

        }

        public static TriggerXml LoadFile(string filename)
        {
            XmlSerializer xs = new XmlSerializer(typeof(TriggerXml));
            TriggerXml p = null;
            using (StreamReader rd = new StreamReader(filename))
            {
                p = xs.Deserialize(rd) as TriggerXml;
            }

            return p;
        }
    }
}
