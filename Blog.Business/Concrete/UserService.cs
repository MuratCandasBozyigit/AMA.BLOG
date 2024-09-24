using Blog.Business.Absract;
using Blog.Business.Shared.Concrete;
using Blog.Data.Shared.Abstract;
using Blog.Dtos.UserDTOs;
using Blog.DTOS.UserDTOs;
using Blog.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Blog.Business.Concrete
{
    public class UserService : Service<AppUser>, IUserService
    {
        private readonly IRepository<AppUser> _userRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor, IRepository<AppUser> userRepository) : base(userRepository)
        {
            _httpContextAccessor = httpContextAccessor;
            _userRepository = userRepository;
        }


        public async Task Logout()
        {
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        #region CRUD
        #region ADD 
        public override AppUser Add(AppUser entity)
        {

            var dataUriPattern = @"^data:(?<type>.+?);base64,(?<data>.+)$";
            var match = Regex.Match(entity.ApplicationForm, dataUriPattern);

            if (match.Success)
            {
                string fileExtension = entity.ApplicationForm.Split(',')[0].Split('/')[1].Split(';')[0];
                byte[] fileBytes = Convert.FromBase64String(entity.ApplicationForm.Split(',')[1]);

                string fileName = $"UyeBasvuruFormu-{entity.UserName}-{DateTime.Now.ToString("dd.MM.yyyy")}-{Guid.NewGuid().ToString().Split('-')[0]}.{fileExtension}";

                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "UyeBasvuruFormlari");
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }
                File.WriteAllBytes(Path.Combine(filePath, fileName), fileBytes);

                entity.ApplicationForm = fileName;


            }




            entity.Password = Guid.NewGuid().ToString().Split('-')[0];
            return base.Add(entity);
        }
        public bool ProfilePictureAdd(string pictureBase64)
        {
            var user = GetById(int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value));
            byte[] bytes = Convert.FromBase64String(pictureBase64.Split(",")[1]);
            string fileName = user.Id + Guid.NewGuid().ToString().Split("-")[1] + ".png";
            if (!Directory.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profile")))
            {
                Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profile"));
            }
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "profile", fileName);
            System.IO.File.WriteAllBytes(filePath, bytes);
            user.ProfilePicture = fileName;
            Update(user);

            return true;
        }

        #endregion
        #region Update 
        public override AppUser Update(AppUser user)
        {
            var currentUser = GetFirstOrDefault(u => u.Id == user.Id);
            if (user.ApplicationForm != currentUser.ApplicationForm && user.ApplicationForm != null)
            {
                var dataUriPattern = @"^data:(?<type>.+?);base64,(?<data>.+)$";
                var match = Regex.Match(user.ApplicationForm, dataUriPattern);

                if (match.Success)
                {
                    string fileExtension = user.ApplicationForm.Split(',')[0].Split('/')[1].Split(';')[0];
                    byte[] fileBytes = Convert.FromBase64String(user.ApplicationForm.Split(',')[1]);

                    string fileName = $"UyeBasvuruFormu-{user.UserName}-{DateTime.Now.ToString("dd.MM.yyyy")}-{Guid.NewGuid().ToString().Split('-')[0]}.{fileExtension}";

                    string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Files", "UyeBasvuruFormlari");
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    File.WriteAllBytes(Path.Combine(filePath, fileName), fileBytes);

                    currentUser.ApplicationForm = fileName;


                }
            }
            currentUser.Password = user.Password;
            currentUser.Email = user.Email;
            currentUser.IsAdmin = user.IsAdmin;
            return base.Update(currentUser);
        }
        public AppUser ProfileUpdate(AppUser user)
        {
            var currentUser = GetFirstOrDefault(u => u.Id == user.Id);
            currentUser.Password = user.Password;
            return base.Update(currentUser);
        }

        #endregion
        #region Get 
        ICollection<UserDto> IUserService.GetAll()
        {
            throw new NotImplementedException();
        }
        public override ICollection<AppUser> GetAll()
        {
            return _userRepository.GetAll().ToList();
        }
        public AppUser GetByEmail(string email)
        {
            return _userRepository.GetFirstOrDefault(x => x.Email == email);
        }
        public AppUser GetPassword(string password)
        {
            return _userRepository.GetFirstOrDefault(x => x.Password == password);
        }
        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            // Şifre doğrulaması burada yapılıyor, isteğe bağlı olarak hash kontrolü ekleyebilirsiniz
            return user != null && user.Password == password;
        }
        public async Task<AppUser> GetByEmailAsync(string email)
        {
            return await _userRepository.GetFirstOrDefaultAsync(x => x.Email == email);
            //
        }

        #endregion 
        #endregion
        #region Log 
        LoginDTO IUserService.Login(LoginDTO appUser)
        {
            AppUser user = _userRepository.GetFirstOrDefault(x => x.Email == appUser.Email && x.Password == appUser.Password);

            if (user != null)
            {


                List<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim(ClaimTypes.Name, user.UserName));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));
                claims.Add(new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "Member"));

                ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);



                _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = appUser.IsRememberMe });
                appUser.Role = user.IsAdmin ? "Admin" : "Member";
                return appUser;
            }
            return appUser;
        }

        public RegisterDTO Register(RegisterDTO appUser)
        {
            var newUser = new AppUser
            {
                UserName = appUser.UserName,
                Email = appUser.Email,
                Password = appUser.Password,
                IsAdmin = appUser.IsAdmin
            };

            _userRepository.Add(newUser);
            _userRepository.Save();
            return appUser;
        }



        #endregion










    }
}


#region NTE 
//ICollection<UserDto> IUserService.GetAll()
//{
//    var payment = _paymentService.GetAll().ToList();

//    return GetAll().GroupJoin(payment, u => u.Id, p => p.UserId, (u, p) => new { U = u, P = p }).Select(x => new UserDto
//    {
//        Id = x.U.Id,
//        ApplicationForm = x.U.ApplicationForm,
//        City = x.U.City,
//        Gender = x.U.Gender,
//        BirthDate = x.U.BirthDate,
//        Email = x.U.Email,
//        FullName = x.U.FullName,
//        Gsm = x.U.Gsm,
//        Password = x.U.Password,
//        Facebook = x.U.Facebook,
//        Payment = x.P.OrderByDescending(p => p.PaymentDate).FirstOrDefault(),
//        RemainingDay = RemainingDaysForMember(x.U.Id),
//        Description = x.U.Description,
//        Instagram = x.U.Instagram,
//        Twitter = x.U.Twitter,
//        DateCreated = x.U.DateCreated,
//        DateUpdated = x.U.DateUpdated,
//        DateDeleted = x.U.DateDeleted,
//        Guid = x.U.Guid,
//        IsActive = x.U.IsActive,
//        IsAdmin = x.U.IsAdmin,
//        IsDeleted = x.U.IsDeleted,
//        MembershipStartDate = x.U.MembershipStartDate,
//        ModifierId = x.U.ModifierId,
//        OwnerId = x.U.OwnerId,
//        ProfilePicture = x.U.ProfilePicture

//    }).ToList(); ;



//} 
#endregion