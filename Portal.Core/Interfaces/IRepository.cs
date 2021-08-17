using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Portal.Core.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        DbContext GetContext();
        T Add(T entity);
        void Edit(T entity);
        List<T> ToList();
        List<T> ToList(Expression<Func<T, bool>> predicate);
        IQueryable<T> ToList(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T FindById(long? id);
        T FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        T Remove(T entity);
        int SaveChanges();
    }
}
