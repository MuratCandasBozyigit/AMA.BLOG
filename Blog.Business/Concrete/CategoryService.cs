using Blog.Business.Absract;
using Blog.Business.Shared.Concrete;
using Blog.Core.Models;
using Blog.Data.Shared.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Concrete
{
    public class CategoryService(IRepository<Category> categoryRepo) : Service<Category>(categoryRepo), ICategoryService
    {
        private readonly IRepository<Category> _repo = categoryRepo;
      
        public ICollection<Category> GetAllCategories(Category category)
        {
          return _repo.GetAll(x => x.Id == category.Id).ToList();
        }

    }
}
