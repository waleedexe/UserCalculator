using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces
{
    public interface IEntityRepository<T> where T: class, new()
    {
        T Add(T entity);
        T GetById(int id);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate);
    }
}
