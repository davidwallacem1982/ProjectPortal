using Microsoft.EntityFrameworkCore;
using Portal.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Portal.Infra.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext context;

        protected DbSet<T> Items { get; set; }

        public Repository(DbContext context)
        {
            this.context = context;
            Items = context.Set<T>();
        }

        public DbContext GetContext() => context;

        public T Add(T entity) => Items.Add(entity).Entity;

        public void Edit(T entity) => context.Entry(entity).State = EntityState.Modified;

        public T Remove(T entity) => Items.Remove(entity).Entity;

        public virtual T FindById(long? id) => Items.Find(id);

        public virtual List<T> ToList() => Items.ToList();

        public virtual List<T> ToList(Expression<Func<T, bool>> predicate) => Items.Where(predicate).ToList();

        public virtual IQueryable<T> ToList(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = Items.Where(predicate);
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    result = result.Include(include);
                }
            }
            return result;
        }

        public virtual T FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes)
        {
            var result = Items.Where(predicate);
            if (includes.Any())
            {
                foreach (var include in includes)
                {
                    result = result.Include(include);
                }
            }
            return result.FirstOrDefault(predicate);
        }

        public int SaveChanges() => context.SaveChanges();

        public void Dispose()
        {
            Items = null;
            GC.SuppressFinalize(this);
        }
    }
}
