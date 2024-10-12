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
    public class TagService(IRepository<Tag> tagRepo) : Service<Tag>(tagRepo), ITagService
    {
        private readonly IRepository<Tag> _repo;
       
        public Tag GeyById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
