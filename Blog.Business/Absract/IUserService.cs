using Blog.Business.Shared.Absract;
using Blog.Dtos.UserDTOs;
using Blog.DTOS.UserDTOs;
using Blog.Models;
using Blog.Models.Dtos;

namespace Blog.Business.Absract
{
    public interface IUserService : IService<AppUser>
    {
        LoginDTO Login(LoginDTO appUser);
        RegisterDTO Register(RegisterDTO appUser);

        bool ProfilePictureAdd(string pictureBase64);

        Task Logout();
        AppUser ProfileUpdate(AppUser appUser);
        AppUser GetByEmail(string email);
        AppUser GetPassword(string password);
        

        //Bunu nerede kullandıgımı hatırlamıyorum dursun şimdilik 
        ICollection<UserDto> GetAll();
      
    }
}
