﻿using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Shared.Abstract
{
    public interface IRepository<T> where T : BaseModel
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        List<T> AddRange(List<T> entities);
        T Update(T entity);
        bool Delete(int id);
        T GetById(int id);
        T GetById(Guid guid);
        T GetFirstOrDefault(Expression<Func<T, bool>> predicate);
        void Save();
        bool Delete(Guid guid); Task<T> GetByIdAsync(int id); Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(int postId);
    }
}
