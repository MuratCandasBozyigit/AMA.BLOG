using Blog.Business.Absract;
using Blog.Business.Concrete;
using Blog.Business.Shared.Absract;
using Blog.Business.Shared.Concrete;
using Blog.Data.Shared.Abstract;
using Blog.Data.Shared.Concrete;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Business.Configuration
{
    public static class BusinessExtension
    {
        public static void BusinessDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IService<>), typeof(Service<>));
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ITagService, TagService>();
        }
        public static void RepositoryDI(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
