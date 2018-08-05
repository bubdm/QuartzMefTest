using Quartz;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Scheduler.Interfaces;

namespace QuartzMefLib.Framework
{
    public class Importer
    {
        [ImportMany(typeof(IToolboxFullJob))]
        public IEnumerable<IToolboxFullJob> JobSets;


        public void DoImport()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();
            //Adds all the parts found in all assemblies in 
            //the same directory as the executing program
            catalog.Catalogs.Add(
             new DirectoryCatalog(
              Path.GetDirectoryName(
               Assembly.GetExecutingAssembly().Location)));

            //Create the CompositionContainer with the parts in the catalog
            CompositionContainer container = new CompositionContainer(catalog);

            //Fill the imports of this object
            container.ComposeParts(this);
        }

    }
}
