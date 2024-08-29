using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Shared.Absract
{
    public interface IService<T> where T : BaseModel
    {
        List<T> GetAll();
        List<T> GetAll(Expression<Func<T, bool>> predicate);
        T GetById(int id);
        T GetById(Guid id);
        T Add(T entity);
        T Update(T entity);
        bool Delete(int id);
        bool Delete(Guid id);
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);
    }
}
