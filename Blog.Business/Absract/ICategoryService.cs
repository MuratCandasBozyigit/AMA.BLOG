using Blog.Business.Shared.Absract;
using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Absract
{
    public interface ICategoryService : IService<Category>
    {
        ICollection<Category> GetAllCategories(Category category);
        IEnumerable<Category> GetAllCategories();
        Task<Category> GetByIdAsync(int id); // Burayı 'GetCategoryByIdAsync' olarak değiştirdim
    }   
}
