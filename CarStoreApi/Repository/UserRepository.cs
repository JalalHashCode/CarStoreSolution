using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CarStoreApi.Data;
using CarStoreApi.Models;
using CarStoreApi.Models.Dto;
using CarStoreApi.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarStoreApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private string secretKey;
        public UserRepository(ApplicationDbContext db, IConfiguration configuration)
        {
            _db = db;
            secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
        }
        public async Task<bool> IsUniqueUser(string username)
        {
            var user = await _db.LocalUsers.FirstOrDefaultAsync(x => x.UserName == username);
            if (user != null)
            {
                return false;
            }
            return true;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = _db.LocalUsers.FirstOrDefault(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower()
            && u.Password == loginRequestDTO.Password);
            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    User = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role),
                    new Claim(ClaimTypes.Name, user.Password)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                User = user,
                
            };
            loginResponseDTO.User.Password = "";
            return loginResponseDTO;
        }

        public async Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            if (registerationRequestDTO != null)
            {
                LocalUser user = new();
                user.UserName = registerationRequestDTO.UserName;
                user.Password = registerationRequestDTO.Password;
                user.Name = registerationRequestDTO.Name;
                user.Role = registerationRequestDTO.Role;
                _db.LocalUsers.Add(user);
                await _db.SaveChangesAsync();
                user.Password = "";
                return user;
            }
            return null; 
        }
            

        
    }
}
