using CarStore.Models;
using CarStore.Models.Dto;
using CarStore.Services.IServices;
using Newtonsoft.Json.Linq;
using static Cars_Utility.StaticDetails;

namespace CarStore.Services
{
    public class CarService : BaseService, ICarService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private string ApiURL;
        public CarService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory) 
        {
            _httpClientFactory = httpClientFactory;
            ApiURL = configuration.GetValue<string>("ServiceUrls:CarStoreApi");
        }

        public Task<T> CreateAsync<T>(CarCreateDTO dto , string token)
        {
            return SendAsync<T>(new APIResquest()
            {
                apiType = ApiTye.POST,
                Data = dto,
                Url = ApiURL + "/api/CarStoreApi",
                Token = token
            });
        }

        public Task<T> DeleteAsync<T>(int id , string token)
        {
            return SendAsync<T>(new APIResquest()
            {
                apiType = ApiTye.DELETE,             
                Url = ApiURL + "/api/CarStoreApi/" + id,
                Token = token

            });
        }

        public Task<T> GetAllAsync<T>(string token)
        {
            return SendAsync<T>(new APIResquest()
            {
                apiType = ApiTye.GET,   
                Url = ApiURL + "/api/CarStoreApi",
                Token = token
            });
        }

        public Task<T> GetAsync<T>(int id , string token )
        {
            return SendAsync<T>(new APIResquest()
            {
                apiType = ApiTye.GET,
                Url = ApiURL + "/api/CarStoreApi/" + id ,
                Token = token
            });
        }

        public Task<T> UpdateAsync<T>(CarUpdateDTO dto, string token)
        {
            return SendAsync<T>(new APIResquest()
            {
                apiType = ApiTye.PUT,
                Data = dto,
                Url = ApiURL + "/api/CarStoreApi/" + dto.Id,
                Token = token
            }); 
        }
    }
}
