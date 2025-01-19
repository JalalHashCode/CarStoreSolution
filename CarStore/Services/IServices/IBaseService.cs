using CarStore.Models;

namespace CarStore.Services.IServices
{
    public interface IBaseService
    {
        APIResponse responseModel { get; set; }
        Task<T> SendAsync<T>( APIResquest apiRequest);
    }
}
