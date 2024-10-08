﻿using Blog.Business.Shared.Absract;
using Blog.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Absract
{
    public interface IPostService:IService<Post>
    {
        Task<Post> GetPostDetails(int postId);
        Task<Post> GetByIdAsync(int id);
        ICollection<Post> GetAllPosts();
        ICollection<Post> GetAllPosts(Post post);
        ICollection<Post> GetAllPostsByCategoryId(int postId);
        //Post GetPostDetails(int postId);
      
    }
}
