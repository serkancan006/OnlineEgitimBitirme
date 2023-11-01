using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OnlineEgitimClient.Service
{
    public class CustomHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public CustomHttpClient(IHttpClientFactory clientFactory, IConfiguration configuration)
        {
            _httpClient = clientFactory.CreateClient();
            _baseUrl = configuration["BaseUrls:BaseUrl"];
        }

        private string Url(RequestParameters requestParameters)
        {
            return $"{requestParameters.BaseUrl ?? _baseUrl}/{requestParameters.Controller}{(requestParameters.Action != null ? $"/{requestParameters.Action}" : "")}";
        }

        public async Task<List<T>> Get<T>(RequestParameters requestParameters, string id = null)
        {
            var url = "";
            if (requestParameters.FullEndPoint != null)
            {
                url = requestParameters.FullEndPoint;
            }
            else
            {
                url = $"{Url(requestParameters)}{(id != null ? $"/{id}" : "")}{(requestParameters.QueryString != null ? $"?{requestParameters.QueryString}" : "")}";
            }

            var responseMessage = await _httpClient.GetAsync(url);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(jsonData);
            }
            else
                return null;
            //throw new Exception("API request failed.");
            //return ViewResult();  return View("Error");
        }

        //public async Task<T> Post<T>(RequestParameters requestParameters, T content)
        //{
        //    var url = requestParameters.FullEndPoint ?? Url(requestParameters);

        //    var response = await _httpClient.PostAsJsonAsync(url, content);
        //    response.EnsureSuccessStatusCode();

        //    return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
        //}

        //public async Task<T> Put<T>(RequestParameters requestParameters, T content)
        //{
        //    var url = requestParameters.FullEndPoint ?? Url(requestParameters);

        //    var response = await _httpClient.PutAsJsonAsync(url, content);
        //    response.EnsureSuccessStatusCode();

        //    return await JsonSerializer.DeserializeAsync<T>(await response.Content.ReadAsStreamAsync());
        //}

        //public async Task Delete(RequestParameters requestParameters, string id)
        //{
        //    var url = requestParameters.FullEndPoint ?? $"{Url(requestParameters)}/{id}";

        //    var response = await _httpClient.DeleteAsync(url);
        //    response.EnsureSuccessStatusCode();
        //}


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
