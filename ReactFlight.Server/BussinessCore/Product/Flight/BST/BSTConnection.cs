using Microsoft.AspNetCore.Routing.Constraints;
using ReactFlight.Server.BussinessCore.Common;
using System.Text;
namespace ReactFlight.Server.BussinessCore.Product.Flight.BST
{
    public class BSTConnection
    {
        private readonly string BaseUrl = string.Empty;
        private readonly string Endpoint = string.Empty;
        private readonly string Method = string.Empty;
        private readonly string EncodedAuthString = string.Empty;
        private readonly IConfiguration Configuration;
        private const string BasePath = "E://APIResponse//ResponseData";
        public BSTConnection(IConfiguration configuration, string baseUrl, string endPoint, string method)
        {
            this.Configuration = configuration;
            BaseUrl = baseUrl;
            Endpoint = endPoint;
            Method = method;
        }
        public async Task<string> SendRequest(string DATA)
        {
            string result = string.Empty;
            string authCredential = $"Basic {GetEncodedAuth()}";
            HttpClient _httpClient = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };
            using (var httprequestMessage = new HttpRequestMessage(HttpMethod.Post, _httpClient.BaseAddress + Endpoint))
            {
                httprequestMessage.Headers.Add("Authorization", authCredential);
                httprequestMessage.Content = new StringContent(DATA, Encoding.UTF8, "application/json");
                try
                {
                    HttpResponseMessage response = await _httpClient.SendAsync(httprequestMessage);
                    response.EnsureSuccessStatusCode();
                    result = await response.Content.ReadAsStringAsync();
                }
                catch (HttpRequestException ex)
                {
                    result = $"{ex.StackTrace}:{ex.Message}";
                }
                finally
                {
                    SaveResponse("SearchResponse", result);
                    _httpClient.Dispose();
                    httprequestMessage.Dispose();
                }
            }
            return result;
        }
        public void SaveResponse(string filename, string response)
        {

            if (response == null)
                return;
            if (!Directory.Exists(BasePath))
            {
                Directory.CreateDirectory(BasePath);
            }
            string fileName = new Guid().ToString() + "-" + filename + MyEnum.FileType.TXT;
            string filePath = Path.Combine(BasePath, fileName);
            using (var streamwriter = new StreamWriter(filePath))
            {
                try
                {
                    streamwriter.Write(response);
                }
                finally
                {
                    streamwriter.Flush();
                }
            }
        }
        public string GetEncodedAuth()
        {
            var AuthSection = Configuration.GetSection("BSTCredential");
            string searchMode = Configuration.GetSection("BSTCredential")["SearchMode"] ?? "";
            string encodedAuth = string.Empty;
            if (!string.IsNullOrEmpty(searchMode))
            {
                switch (searchMode)
                {
                    case MyEnum.ApplicationMode.TEST:
                        byte[] credentialsBytes = System.Text.Encoding.UTF8.GetBytes(AuthSection["AccountCode"] + ":" + AuthSection["TestPassword"]);
                        encodedAuth = Convert.ToBase64String(credentialsBytes);
                        return encodedAuth;
                    case MyEnum.ApplicationMode.LIVE:
                        credentialsBytes = System.Text.Encoding.UTF8.GetBytes(AuthSection["AccountCode"] + ":" + AuthSection["LivePassword"]);
                        encodedAuth = Convert.ToBase64String(credentialsBytes);
                        return encodedAuth;
                    default:
                        return encodedAuth;

                }
            }
            return encodedAuth;
        }
    }
}
