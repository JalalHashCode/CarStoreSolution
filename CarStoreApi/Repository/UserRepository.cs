using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CarStoreApi.Data;
using CarStoreApi.Models;
using CarStoreApi.Models.Dto;
using CarStoreApi.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarStoreApi.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private string secretKey;

        public UserRepository(ApplicationDbContext db, IConfiguration configuration, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
            secretKey = configuration.GetValue<string>("ApiSettings:SecretKey");
        }
        public async Task<bool> IsUniqueUser(string username)
        {
            var user = await _db.ApplicationUsers
                .FirstOrDefaultAsync(x => x.UserName == username);

            if (user != null)
            {
                return false;
            }
            return true;
        }

        public async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO)
        {
            var user = await _db.ApplicationUsers
               .FirstOrDefaultAsync(x => x.UserName == loginRequestDTO.UserName);

            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    ErrorMessage = "Please insert valid UserName"
                };
            }

            bool isValidPassword = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (!isValidPassword)
            {
                return new LoginResponseDTO()
                {
                    ErrorMessage = "Please insert valid Password"
                };
            }

            if (user != null && isValidPassword)
            {

                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(secretKey);
                var roles = await _userManager.GetRolesAsync(user);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, roles.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
                {
                    Token = tokenHandler.WriteToken(token),
                    User = _mapper.Map<UserDTO>(user),
                    Role = roles.FirstOrDefault()

                };

                return loginResponseDTO;
            }

            return new LoginResponseDTO()
            {
                ErrorMessage = ""
            };
        }

        public async Task<RegisterationResponseDTO> Register(RegisterationRequestDTO registerationRequestDTO)
        {
            bool isUnique = await IsUniqueUser(registerationRequestDTO.UserName);
            if (!isUnique)
            {
                return new RegisterationResponseDTO()
                {
                    errors = new List<string>()
                    {
                        "This UserName Already Exist"
                    }
                };
            }

            if (registerationRequestDTO != null)
            {

                ApplicationUser user = new()
                {
                    UserName = registerationRequestDTO.UserName,
                    Name = registerationRequestDTO.Name,
                };

                try
                {
                    var roleExists = await _roleManager.RoleExistsAsync("Admin");

                    if (!roleExists)
                    {
                        var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));

                        if (!roleResult.Succeeded)
                        {
                            // Handle the error according to your application's requirements
                        }
                    }

                    var result = await _userManager.CreateAsync(user, registerationRequestDTO.Password);
                    if (result.Succeeded)
                    {

                        await _userManager.AddToRoleAsync(user, "Admin");
                        return new RegisterationResponseDTO()
                        {
                            User = _mapper.Map<UserDTO>(user),
                        };

                    }
                    else
                    {
                        return new RegisterationResponseDTO()
                        {
                            errors = result.Errors.Select(e => e.Description).ToList()
                        };
                    }



                }
                catch (Exception ex)
                {
                    return new RegisterationResponseDTO()
                    {
                        errors = new List<string> { ex.Message }
                    };
                }
            }
            return new RegisterationResponseDTO() { errors = new List<string>() };

        }

    }
}
