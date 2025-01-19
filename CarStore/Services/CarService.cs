using CarStore.Models;
using CarStore.Models.Dto;
using CarStore.Services.IServices;
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

        public Task<T> CreateAsync<T>(CarCreateDTO dto)
        {
            return SendAsync<T>(new APIResquest()
            {
                apiType = ApiTye.POST,
                Data = dto,
                Url = ApiURL + "/api/CarStoreApi"
            });
        }

        public Task<T> DeleteAsync<T>(int id)
        {
            return SendAsync<T>(new APIResquest()
            {
                apiType = ApiTye.DELETE,             
                Url = ApiURL + "/api/CarStoreApi/" + id
            });
        }

        public Task<T> GetAllAsync<T>()
        {
            return SendAsync<T>(new APIResquest()
            {
                apiType = ApiTye.GET,   
                Url = ApiURL + "/api/CarStoreApi"
            });
        }

        public Task<T> GetAsync<T>(int id)
        {
            return SendAsync<T>(new APIResquest()
            {
                apiType = ApiTye.GET,
                Url = ApiURL + "/api/CarStoreApi/" + id
            });
        }

        public Task<T> UpdateAsync<T>(CarUpdateDTO dto)
        {
            return SendAsync<T>(new APIResquest()
            {
                apiType = ApiTye.PUT,
                Data = dto,
                Url = ApiURL + "/api/CarStoreApi/" + dto.Id
            }); 
        }
    }
}
