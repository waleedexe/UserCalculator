using Entities;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SqlRepository
{
    public class EntityRepository<T> : IEntityRepository<T> where T: EntityBase, new()
    {
        private UserDb _context = new UserDb();

        public T Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();

            return entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public IEnumerable<T> GetBy(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Where(predicate).ToList();
        }
    }
}
