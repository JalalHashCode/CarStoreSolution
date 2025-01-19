using static Cars_Utility.StaticDetails;

namespace CarStore.Models
{
    public class APIResquest
    {
        public ApiTye apiType { get; set; } = ApiTye.GET;
        public string Url { get; set; }
        public object Data { get; set; }
    }
}
