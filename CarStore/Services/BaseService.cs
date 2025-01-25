using System.Net.Http.Headers;
using Cars_Utility;
using CarStore.Models;
using CarStore.Services.IServices;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CarStore.Services
{
    public class BaseService : IBaseService
    {
        public APIResponse responseModel { get ; set ; }
        public IHttpClientFactory httpClient { get ; set ; }
        public BaseService(IHttpClientFactory httpClient)
        {
            this.responseModel = new APIResponse();
            this.httpClient =  httpClient;
        }
        public async Task<T> SendAsync<T>(APIResquest apiRequest)
        {
            try
            {
                var client = httpClient.CreateClient("CarsAPI");
                HttpRequestMessage message = new HttpRequestMessage();
                message.RequestUri = new Uri(apiRequest.Url);
                if (apiRequest.Data != null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(apiRequest.Data),
                        encoding: System.Text.Encoding.UTF8, "application/json");
                }
                switch (apiRequest.apiType)
                {
                    case StaticDetails.ApiTye.GET:
                        message.Method = HttpMethod.Get;
                        break;
                    case StaticDetails.ApiTye.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case StaticDetails.ApiTye.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case StaticDetails.ApiTye.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                }

                HttpResponseMessage apiResponse = null;
                if (!string.IsNullOrEmpty(apiRequest.Token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiRequest.Token);
                }
                apiResponse = await client.SendAsync(message);
                var apiContent = await apiResponse.Content.ReadAsStringAsync();
                var APIResponse = JsonConvert.DeserializeObject<T>(apiContent);
                return APIResponse;
            }
            catch (Exception ex) 
            {
                var dto = new APIResponse
                {
                    ErrorMessages = new List<string> { Convert.ToString(ex.Message) },
                    IsSuccess = false
                };
                var response = JsonConvert.SerializeObject(dto);
                var APIResponse = JsonConvert.DeserializeObject<T>(response);
                return APIResponse; 

            }
        }
    }
}
