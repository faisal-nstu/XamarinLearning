using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MyXamarinLearnings.Services
{
    public class DataService: BaseProvider, IDataService
    {
        private string _loginUri;

        public DataService()
        {
            _baseUrl = "http://172.16.229.236:8080";
            _loginUri = string.Format("{0}/access/token", _baseUrl);
        }

        public async Task<UserAccountModel> Login(UserLoginModel loginData)
        {
            var loginResponse = await Post<UserLoginModel, UserAccountModel>(_loginUri,loginData);

            if (loginResponse != null && loginResponse.ResponseCode == 100)
            {
                var userData = loginResponse;
            }
            return loginResponse;
        }
    }

    public class ResponseBase
    {
        [JsonProperty("responseCode")]
        public int ResponseCode { get; set; }
        [JsonProperty("errors")]
        public List<object> Errors { get; set; }
    }
    public class UserAccountModel : ResponseBase
    {
        public string token { get; set; }
        public string customerName { get; set; }
        public string lastLoginDate { get; set; }
        public int accountType { get; set; }
        public int accountStatus { get; set; }
        public object accountStatusMessage { get; set; }
        public int rsaTokenStatus { get; set; }
        public int rsaTokenType { get; set; }
        public string rsaTokenStatusMessage { get; set; }
        public string mobileToken { get; set; }
        public string passwordExpireDate { get; set; }
    }

    public class UserLoginModel
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string ipAddress { get; set; }
    }
}
