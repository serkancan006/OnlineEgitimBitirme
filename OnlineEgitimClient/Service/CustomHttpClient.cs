using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Text;

namespace OnlineEgitimClient.Service
{
    public class CustomHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string? _baseUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CustomHttpClient(IHttpClientFactory clientFactory, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _httpClient = clientFactory.CreateClient();
            _baseUrl = configuration["BaseUrls:BaseUrl"];
            _httpContextAccessor = httpContextAccessor;

        }
      

        private string Url(RequestParameters requestParameters)
        {
            return $"{requestParameters.BaseUrl ?? _baseUrl}/{requestParameters.Controller}{(requestParameters.Action != null ? $"/{requestParameters.Action}" : "")}";
        }

        public async Task<HttpResponseMessage> Get(RequestParameters requestParameters, int? id = null)
        {
            string url;
            if (requestParameters.FullEndPoint != null)
            {
                url = requestParameters.FullEndPoint;
            }
            else
            {
                url = $"{Url(requestParameters)}{(id != null ? $"/{id}" : "")}{(requestParameters.QueryString != null ? $"?{requestParameters.QueryString}" : "")}";
            }
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["Token"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await _httpClient.GetAsync(url);
            return responseMessage;
        }
        //queryString: `imageId=${imageId}`
        public async Task<HttpResponseMessage> Post<T>(RequestParameters requestParameters, T content, int? id = null)
        {
            //var url = requestParameters.FullEndPoint ?? Url(requestParameters);
            string url;
            if (requestParameters.FullEndPoint != null)
            {
                url = requestParameters.FullEndPoint;
            }
            else
            {
                url = $"{Url(requestParameters)}{(id != null ? $"/{id}" : "")}{(requestParameters.QueryString != null ? $"?{requestParameters.QueryString}" : "")}";
            }

            var jsonData = JsonConvert.SerializeObject(content);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["Token"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await _httpClient.PostAsync(url, stringContent);
            return responseMessage;
        }

        public async Task<HttpResponseMessage> Put<T>(RequestParameters requestParameters, T content)
        {
            var url = requestParameters.FullEndPoint ?? Url(requestParameters);

            var jsonData = JsonConvert.SerializeObject(content);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["Token"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await _httpClient.PutAsync(url, stringContent);
            return responseMessage;
        }

        public async Task<HttpResponseMessage> Delete(RequestParameters requestParameters, int id)
        {
            var url = requestParameters.FullEndPoint ?? $"{Url(requestParameters)}/{id}";
            var token = _httpContextAccessor.HttpContext?.Request.Cookies["Token"];
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var responseMessage = await _httpClient.DeleteAsync(url);
            return responseMessage;
            //response.EnsureSuccessStatusCode();
        }

    }

    public partial class RequestParameters
    {
        public string? Controller { get; set; }
        public string? Action { get; set; }
        public string? QueryString { get; set; }
        public string? BaseUrl { get; set; }
        public string? FullEndPoint { get; set; }
    }
}
