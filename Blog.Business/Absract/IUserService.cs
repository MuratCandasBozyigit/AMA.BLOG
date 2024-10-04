using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Absract
{
    public interface IUserService
    {
        Task<List<AppUser>> GetAllUsersAsync();
    }
}
