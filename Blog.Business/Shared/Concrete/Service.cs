using Blog.Business.Shared.Absract;
using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Blog.Business.Shared.Concrete
{
    public class Service<T> : IService<T> where T : BaseModel
    {
        private readonly IRepository<T> _repository;

        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public T Add(T entity)
        {
            return _repository.Add(entity);
        }

        public bool Delete(int id)
        {
            return _repository.Delete(id);
        }

        public bool Delete(Guid guid)
        {
            return _repository.Delete(guid);
        }

        public List<T> GetAll()
        {
            return _repository.GetAll().ToList();
        }

        public List<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return _repository.GetAll(expression).ToList();
        }

        public T GetById(int id)
        {
            return _repository.GetById(id);
        }

        public T GetById(Guid id)
        {
            return _repository.GetById(id);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> expression)
        {
            return _repository.GetFirstOrDefault(expression);
        }

        public T Update(T entity)
        {
            return _repository.Update(entity);
        }
    }
}
