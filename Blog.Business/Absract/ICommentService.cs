﻿using Blog.Business.Shared.Absract;
using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Absract
{
    public interface ICommentService:IService<Comment>
    {
        Task<IEnumerable<Comment>> GetComments(int id);
        Task<IQueryable<Comment>> GetCommentsById(int id);
    }
}
