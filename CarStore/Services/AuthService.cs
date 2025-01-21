using CarStore.Models;
using CarStore.Models.Dto;
using CarStore.Services.IServices;
using static Cars_Utility.StaticDetails;

namespace CarStore.Services
{
    public class AuthService : BaseService, IAuthService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string ApiURL;
        public AuthService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            ApiURL = configuration.GetValue<string>("ServiceUrls:CarStoreApi");
        }

        public Task<T> LoginAsync<T>(LoginRequestDTO loginRequestDTO)
        {
            return SendAsync<T>(new APIResquest()
            {
                apiType = ApiTye.POST,
                Data = loginRequestDTO,
                Url = ApiURL + "/api/UsersAuth/Login"
            });
        }

        public Task<T> RegisterAsync<T>(RegisterationRequestDTO registerationRequestDTO)
        {
            return SendAsync<T>(new APIResquest()
            {
                apiType = ApiTye.POST,
                Data = registerationRequestDTO,
                Url = ApiURL + "/api/UsersAuth/Register"
            });
        }
    }
}
