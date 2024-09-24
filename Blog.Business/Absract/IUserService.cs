using Blog.Business.Shared.Absract;
using Blog.Dtos.UserDTOs;
using Blog.Models;
using Blog.Models.Dtos;

namespace Blog.Business.Absract
{
    public interface IUserService : IService<AppUser>
    {
        Task<LoginDTO> Login(LoginDTO appUser);
        bool ProfilePictureAdd(string pictureBase64);
        Task Logout();
        AppUser ProfileUpdate(AppUser appUser);


        AppUser GetByEmail(string email);
        AppUser GetPassword(string password);
        //Task<AppUser> GetByEmail(string email);
        //Task<AppUser> GetPassword(string email);

        //Bunu nerede kullandıgımı hatırlamıyorum dursun şimdilik 
        ICollection<UserDto> GetAll();
      
    }
}
