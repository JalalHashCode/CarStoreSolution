using CarStoreApi.Models;
using CarStoreApi.Models.Dto;

namespace CarStoreApi.Repository.IRepository
{
    public interface IUserRepository
    {
        Task<bool> IsUniqueUser(string username);
        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);
        Task<LocalUser> Register(RegisterationRequestDTO registerationRequestDTO);
    }
}
