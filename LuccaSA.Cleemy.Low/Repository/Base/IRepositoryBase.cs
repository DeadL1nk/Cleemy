using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LuccaSA.Cleemy.Low
{
    //Ces services peuvent être appelées depuis n'importe quelle classe de repo.
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> SortBy(IQueryable<T> entity, string orderByQueryString);
    }
}
