using DataAccessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        Context c = new Context();

        DbSet<T> _dbSet;

        public GenericRepository()
        {
            _dbSet = c.Set<T>();
        }
        public void Delete(T t)
        {
            var deletedEntity= c.Entry(t);
            deletedEntity.State = EntityState.Deleted;
            _dbSet.Remove(t);
            c.SaveChanges();
        }

        public T Get(Expression<Func<T, bool>> filter)
        {
            return _dbSet.SingleOrDefault(filter);
        }

        public void Insert(T t)
        {
            var addedEntity = c.Entry(t);
            addedEntity.State = EntityState.Added;
            //_dbSet.Add(t);
            c.SaveChanges();
        }

        public List<T> List()
        {
            return _dbSet.ToList(); 
        }

        public List<T> List(Expression<Func<T, bool>> Filter)
        {
            return _dbSet.Where(Filter).ToList();
        }

        public void Update(T t)
        {
            var updatedEntity = c.Entry(t); // entity state kullanımı
            updatedEntity.State = EntityState.Modified;
            c.SaveChanges();
        }
    }
}
