using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using HttpClient = System.Net.Http.HttpClient;

namespace MyXamarinLearnings.Services
{
    public class BaseProvider
    {
        protected string _baseUrl;

        protected HttpClient GetClient()
        {
            return GetClient(_baseUrl);
        }

        protected virtual HttpClient GetClient(string baseUrl)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);

            return client;
        }

        

        protected async Task<T> Get<T>(string url)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    //client.DefaultRequestHeaders.Add("AuthTok","fsfs");
                    var response = await client.GetAsync(url);
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<T>(responseJson);
                    return responseObj;
                }
                catch (HttpRequestException ex)
                {
                    
                }
                
            }
            return default(T);
        }

        protected async Task<TResponse> Post<TRequest,TResponse>(string url, TRequest requestObject)
        {
            using (HttpClient client = GetClient())
            {
                try
                {
                    string requestJson = JsonConvert.SerializeObject(requestObject);
                    //client.DefaultRequestHeaders.Add("AuthTok", "fsfs");


                    HttpContent httpContent = new StringContent(requestJson,Encoding.UTF8, "application/json");
                    var response = await client.PostAsync(url,httpContent);
                    var responseJson = await response.Content.ReadAsStringAsync();
                    var responseObj = JsonConvert.DeserializeObject<TResponse>(responseJson);
                    return responseObj;
                }
                catch (HttpRequestException ex)
                {
                    throw new NetworkException("Cannot connect.");
                }
            }
        }
    }

    public class NetworkException : Exception
    {
        public NetworkException(string cannotConnect)
        {
            
        }
    }
}
