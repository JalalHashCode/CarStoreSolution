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
            var usernameExist = _db.LocalUsers.Any(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            var PassExit = _db.LocalUsers.Any(u => u.Password == loginRequestDTO.Password);

            string message = string.Empty;
            if (usernameExist && !PassExit) { message = "Password is Invalid"; }

            if (!usernameExist && PassExit) { message = "UserName is Invalid"; }

            if (!usernameExist && !PassExit) { message = "This User is Not Registered "; }
            if (usernameExist && PassExit)
            {
                var user = _db.LocalUsers.FirstOrDefault(u => u.UserName == loginRequestDTO.UserName
            && u.Password == loginRequestDTO.Password);


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

                return loginResponseDTO;
            }

            return new LoginResponseDTO()
            {
                Message = message
            };
        }

        public async Task<RegisterationResponseDTO> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            bool isUnique = await IsUniqueUser(registerationRequestDTO.UserName);
            if (!isUnique)
            {
                return new RegisterationResponseDTO() { Message = "This UserName already used" };
            }

                if (registerationRequestDTO != null)
            {
                LocalUser user = new()
                {
                    UserName = registerationRequestDTO.UserName,
                    Password = registerationRequestDTO.Password,
                    Name = registerationRequestDTO.Name,
                    Role = registerationRequestDTO.Role
                };
                _db.LocalUsers.Add(user);
                await _db.SaveChangesAsync();


                return new RegisterationResponseDTO()
                {
                    User = user,
                    Message = "Successful Register"
                };
            }
            return new RegisterationResponseDTO() { Message = string.Empty };

        }



    }
}
