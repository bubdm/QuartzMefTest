using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Persistance.Svc.Framework
{
    public class BaseContext : DbContext, IRepository
    {
        public bool Add<T>(T item) where T : class
        {
            try { Set<T>().Add(item); return true; } catch { return false; }
        }
        public bool Save()
        {
            try { SaveChanges(); return true; } catch { return false; }
        }
    }
}
