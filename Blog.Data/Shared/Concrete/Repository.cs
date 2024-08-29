using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Data.Shared.Concrete
{
    public class Repository<T> : IRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private int? _ownerAndUpdateId;

        public Repository(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _httpContextAccessor = httpContextAccessor;

            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated == true)
            {
                _ownerAndUpdateId = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            }
        }

        public T Add(T entity)
        {
            _dbSet.Add(entity);
            Save();
            return entity;
        }

        public List<T> AddRange(List<T> entities)
        {
            _dbSet.AddRange(entities);
            Save();
            return entities;
        }

        public bool Delete(int id)
        {
            T entity = _dbSet.Find(id);
            if (entity == null)
                return false;

            entity.IsDeleted = true;
            Update(entity);
            return true;
        }

        public bool Delete(Guid guid)
        {
            T entity = _dbSet.FirstOrDefault(x => x.GuidId == guid);
            return entity != null && Delete(entity.Id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.Where(x => !x.IsDeleted);
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return GetAll().Where(predicate);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T GetById(Guid guid)
        {
            return _dbSet.FirstOrDefault(x => x.GuidId == guid);
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.FirstOrDefault(predicate);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public T Update(T entity)
        {
            _dbSet.Update(entity);
            Save();
            return entity;
        }
    }
}
