﻿using Blog.Business.Absract;
// Doğru isimlendirme
using Blog.Business.Shared.Concrete;
using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Concrete
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly IRepository<Category> _repo;

        public CategoryService(IRepository<Category> categoryRepo) : base(categoryRepo)
        {
            _repo = categoryRepo;
        }

        public ICollection<Category> GetAllCategories(Category category)
        {
            return _repo.GetAll(x => x.Id == category.Id)
                .ToList();
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _repo.GetAll().ToList();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public Category GetCategoriesById(int id)
        {
            return _repo.GetAll().Where(x => x.Id == id).Include(x => x.Tag).SingleOrDefault();
                
        }
    }
}
