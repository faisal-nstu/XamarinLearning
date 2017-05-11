using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using MyXamarinLearnings.Models;

namespace MyXamarinLearnings.Services
{
    public class DataService : BaseProvider, IDataService
    {
        public DataService()
        {
            SetConfig();
        }

        private void SetConfig()
        {
            _baseUrl = "http://ec2-34-223-215-182.us-west-2.compute.amazonaws.com/app";

            //_loginUri = string.Format("{0}/api/v1/user/login", _baseUrl);
        }

        public async Task<AddressPredictionResponse> PredictAddress(string address)
        {
            // AIzaSyCiULSx6qURoKdv8tr4bn812cJXG5doZAI 
            // AIzaSyBR8KXMId6TSmLBC5qLJQsuJN00GZpLx5w
            // AIzaSyCS--Zs7L5V478SLoTXb9dSeQGNJKClKM4 
            // AIzaSyAJk5Tg_zJLxcGqeJ-d_fzKw4Cv1rTYXeE
            var uri = $"https://maps.googleapis.com/maps/api/place/autocomplete/json?input={address}&types=geocode&key=AIzaSyAJk5Tg_zJLxcGqeJ-d_fzKw4Cv1rTYXeE";
            var response = await Get<AddressPredictionResponse>(uri);
            return response;
        }
    }
}
