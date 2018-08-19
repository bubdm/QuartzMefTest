using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Toolbox.Persistance.Svc.Framework
{
    public interface IRepository
    {
        bool Add<T>(T item) where T : class;
        IQueryable<T> Get<T>() where T : class;
        bool Save();
    }
}
