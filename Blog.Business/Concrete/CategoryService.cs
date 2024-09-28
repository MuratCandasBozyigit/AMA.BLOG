using Blog.Business.Absract;
using Blog.Business.Shared.Concrete;
using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.Business.Concrete
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private readonly IRepository<Category> _repo;

        // Constructor injection
        public CategoryService(IRepository<Category> categoryRepo) : base(categoryRepo)
        {
            _repo = categoryRepo;
        }

        // Belirli bir category ID'sine göre kategorileri getirir
        public ICollection<Category> GetAllCategories(Category category)
        {
            return _repo.GetAll(x => x.Id == category.Id).ToList();
        }

        // Tüm kategorileri getirir
        public IEnumerable<Category> GetAllCategories()
        {
            return _repo.GetAll().ToList();
        }

        // Asenkron olarak tüm kategorileri getirir
        public async Task<ICollection<Category>> GetAllCategoriesAsync()
        {
            return await Task.Run(() => _repo.GetAll().ToList());
        }
    }
}
