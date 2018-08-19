using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Persistance.Svc.Model
{
    public class Versions
    {
        public int ID { get; set; }
        public string ModuleName { get; set; }
        public int Version { get; set; }
        public int VersionDate { get; set; }
    }
}
