using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Scheduler.Interfaces;

namespace PersistanceModule
{
    public class PersistanceTest : IPesistanceContract
    {
        public string ModuleName { get => "Test"; }

        public void InitializeDB()
        {
            // create database ans initialize it
        }

        public void UpgradeDB()
        {
            // Upgrade databse with all version step
        }
    }
}
