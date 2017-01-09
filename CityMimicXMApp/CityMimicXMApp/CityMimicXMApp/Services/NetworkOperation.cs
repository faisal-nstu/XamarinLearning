using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Windows.Networking.BackgroundTransfer;
using Windows.Web;
using Windows.Web.Http;
using Newtonsoft.Json;



namespace CityMimicXMApp.Services
{
    public class NetworkOperation : INetworkOperation
    {
        public async Task<TResponse> ExceutePostOperation<TRequest, TResponse>(Uri uri, TRequest requestObject)
        {
            TResponse response = default(TResponse);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string requestJson = JsonConvert.SerializeObject(requestObject);
                    Random random = new Random();
                    string url = "?random=" + Guid.NewGuid();

                    var newUri = new Uri(uri, url);
                    //var deviceId = GetDeviceId();

                    var httpRequestMessage = new HttpRequestMessage();

                    httpRequestMessage.Method = HttpMethod.Post;
                    httpRequestMessage.RequestUri = newUri;
                    //httpRequestMessage.Headers.Add("DeviceId", deviceId);
                    //httpRequestMessage.Headers.Add("AuthenticationToken", AuthenticationInfo.Current.AccessToken);
                    IHttpContent httpContent = new HttpStringContent(requestJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
                    httpRequestMessage.Content = httpContent;
                    var result = await client.SendRequestAsync(httpRequestMessage);
                    result.EnsureSuccessStatusCode();

                    var responseJson = await result.Content.ReadAsStringAsync();
                    //var jsonSerializerSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
                    //var responseBase = JsonConvert.DeserializeObject<ResponseBase>(responseJson);
                    //if (responseBase != null && responseBase.ResponseCode == 1102)
                    //{
                    //    //await App.GoToLoginPage();
                    //}
                    //else
                    //{
                    //    response = JsonConvert.DeserializeObject<TResponse>(responseJson);
                    //}

                    ////response = JsonConvert.DeserializeObject<TResponse>(responseJson);
                    return response;
                }
            }
            catch (JsonReaderException jsEx)
            {
                jsEx.ToString();//to remove warning message
                string message = "There Was an Error Reading Data";
                throw new NetworkException(message);

            }
            catch (Exception ex)
            {
                var hresult = ex.HResult;
                var status = BackgroundTransferError.GetStatus(hresult);

                if (status == WebErrorStatus.CannotConnect ||
                    status == WebErrorStatus.NotFound ||
                    status == WebErrorStatus.RequestTimeout ||
                    status == WebErrorStatus.HostNameNotResolved)
                {
                    //string message = _resourceLoader.GetString("ErrorCouldNotConnect");
                    throw new NetworkException("Cannot connect.");
                }

                else
                {
                    //string message = _resourceLoader.GetString("ErrorUnknownNetworkProblem");
                    throw new NetworkException("Unknown Network Error");
                }
            }
        }
        public async Task<TResponse> ExceutePostOperationAuthless<TRequest, TResponse>(Uri uri, TRequest requestObject)
        {
            TResponse response = default(TResponse);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string requestJson = JsonConvert.SerializeObject(requestObject);
                    Random random = new Random();
                    string url = "?random=" + Guid.NewGuid();

                    var newUri = new Uri(uri, url);
                    //var deviceId = GetDeviceId();

                    var httpRequestMessage = new HttpRequestMessage();

                    httpRequestMessage.Method = HttpMethod.Post;
                    httpRequestMessage.RequestUri = newUri;
                    //httpRequestMessage.Headers.Add("DeviceId", deviceId);
                    IHttpContent httpContent = new HttpStringContent(requestJson, Windows.Storage.Streams.UnicodeEncoding.Utf8, "application/json");
                    httpRequestMessage.Content = httpContent;
                    var result = await client.SendRequestAsync(httpRequestMessage);
                    result.EnsureSuccessStatusCode();

                    var responseJson = await result.Content.ReadAsStringAsync();

                    response = JsonConvert.DeserializeObject<TResponse>(responseJson);
                    return response;
                }
            }
            catch (JsonReaderException jsEx)
            {
                jsEx.ToString();//to remove warning message
                string message = "There Was an Error Reading Data";
                throw new NetworkException(message);

            }
            catch (Exception ex)
            {
                var hresult = ex.HResult;
                var status = BackgroundTransferError.GetStatus(hresult);

                if (status == WebErrorStatus.CannotConnect ||
                    status == WebErrorStatus.NotFound ||
                    status == WebErrorStatus.RequestTimeout ||
                    status == WebErrorStatus.HostNameNotResolved)
                {
                    //string message = _resourceLoader.GetString("ErrorCouldNotConnect");
                    throw new NetworkException("Cannot connect.");
                }

                else
                {
                    //string message = _resourceLoader.GetString("ErrorUnknownNetworkProblem");
                    throw new NetworkException("Unknown Network Error");
                }
            }
        }
        private string GetDeviceId()
        {
            var token = Windows.System.Profile.HardwareIdentification.GetPackageSpecificToken(null);
            var hardwareId = token.Id;
            var dataReader = Windows.Storage.Streams.DataReader.FromBuffer(hardwareId);

            byte[] bytes = new byte[hardwareId.Length];
            dataReader.ReadBytes(bytes);

            return BitConverter.ToString(bytes).Replace("-", "");
        }

        public async Task<TResponse> ExecuteGetOperation<TResponse>(Uri uri)
        {
            TResponse response = default(TResponse);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Random random = new Random();
                    string url = "?random=" + Guid.NewGuid();

                    var newUri = new Uri(uri, url);
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = newUri,
                        Method = HttpMethod.Get
                    };
                    //var deviceId = GetDeviceId();
                    //request.Headers.Add("DeviceId", deviceId);
                    request.Headers.Add("AuthenticationToken", AuthenticationInfo.Current.AccessToken);
                    var responseObject = await client.SendRequestAsync(request);
                    responseObject.EnsureSuccessStatusCode();
                    var responseJson = await responseObject.Content.ReadAsStringAsync();

                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.MissingMemberHandling = MissingMemberHandling.Ignore;
                    

                    var responseBase = JsonConvert.DeserializeObject<ResponseBase>(responseJson);
                    if (responseBase != null && responseBase.ResponseCode == 1102)
                    {
                        await App.GoToLoginPage();
                    }
                    else
                    {
                        response = JsonConvert.DeserializeObject<TResponse>(responseJson);
                    }
                    return response;
                }
            }
            catch (JsonReaderException jsEx)
            {
                jsEx.ToString();//to remove warning message
                string message = "There Was an Error Reading Data";
                throw new NetworkException(message);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetType().Name + ": " + ex.Message);
                var hresult = ex.HResult;
                var status = BackgroundTransferError.GetStatus(hresult);

                if (status == WebErrorStatus.CannotConnect ||
                    status == WebErrorStatus.NotFound ||
                    status == WebErrorStatus.RequestTimeout ||
                    status == WebErrorStatus.HostNameNotResolved)
                {
                    string message = "Could not Connect.";
                    throw new NetworkException(message);
                }

                else
                {
                    throw new NetworkException("Unknown Network Error");
                }
            }
        }
        public async Task<TResponse> ExecuteGetOperationAuthless<TResponse>(Uri uri)
        {
            TResponse response = default(TResponse);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    Random random = new Random();
                    string url = "?random=" + Guid.NewGuid();

                    var newUri = new Uri(uri, url);
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = newUri,
                        Method = HttpMethod.Get
                    };
                    
                    var responseObject = await client.SendRequestAsync(request);
                    responseObject.EnsureSuccessStatusCode();
                    var responseJson = await responseObject.Content.ReadAsStringAsync();

                    JsonSerializerSettings settings = new JsonSerializerSettings();
                    settings.MissingMemberHandling = MissingMemberHandling.Ignore;


                    var responseBase = JsonConvert.DeserializeObject<ResponseBase>(responseJson);
                    if (responseBase != null && responseBase.ResponseCode == 1102)
                    {
                        await App.GoToLoginPage();
                    }
                    else
                    {
                        response = JsonConvert.DeserializeObject<TResponse>(responseJson);
                    }
                    return response;
                }
            }
            catch (JsonReaderException jsEx)
            {
                jsEx.ToString();//to remove warning message
                string message = "There Was an Error Reading Data";
                throw new NetworkException(message);

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.GetType().Name + ": " + ex.Message);
                var hresult = ex.HResult;
                var status = BackgroundTransferError.GetStatus(hresult);

                if (status == WebErrorStatus.CannotConnect ||
                    status == WebErrorStatus.NotFound ||
                    status == WebErrorStatus.RequestTimeout ||
                    status == WebErrorStatus.HostNameNotResolved)
                {
                    string message = "Could not Connect.";
                    throw new NetworkException(message);
                }

                else
                {
                    throw new NetworkException("Unknown Network Error");
                }
            }
        }

        public Task<string> UploadFile(Uri uri, Dictionary<string, string> headers, byte[] fileBytes)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse> ExceutePostOperation<TRequest, TResponse>(Uri uri, TRequest requestObject, CancellationToken token)
        {
            throw new NotImplementedException();
        }

        public async Task<byte[]> ExecuteFileGetOperation(Uri uri)
        {
            try
            {
                using (System.Net.Http.HttpClient client = new System.Net.Http.HttpClient())
                {
                    string url = uri.AbsoluteUri.Contains("?") ? uri.Query + "&random=" + Guid.NewGuid() : "?random=" + Guid.NewGuid();

                    var newUri = new Uri(uri, url);
                    var request = new System.Net.Http.HttpRequestMessage()
                    {
                        RequestUri = newUri,
                        Method = System.Net.Http.HttpMethod.Get
                    };

                    request.Headers.Add("AuthenticationToken", AuthenticationInfo.Current.AccessToken);
                    var responseObject = await client.SendAsync(request);
                    responseObject.EnsureSuccessStatusCode();
                    byte[] responseJson = await responseObject.Content.ReadAsByteArrayAsync();

                    return responseJson;
                }
            }
            catch (JsonReaderException jsEx)
            {
                jsEx.ToString();//to remove warning message
                string message = "There Was an Error Reading Data";
                throw new NetworkException(message);

            }
            catch (Exception ex)
            {
                var hresult = ex.HResult;
                var status = BackgroundTransferError.GetStatus(hresult);

                if (status == WebErrorStatus.CannotConnect ||
                    status == WebErrorStatus.NotFound ||
                    status == WebErrorStatus.RequestTimeout ||
                    status == WebErrorStatus.HostNameNotResolved)
                {
                    string message = "Couldnot Connect.";
                    throw new NetworkException(message);
                }

                else
                {
                    throw new NetworkException("Unknown Network Error");
                }
            }
        }
    }
}
