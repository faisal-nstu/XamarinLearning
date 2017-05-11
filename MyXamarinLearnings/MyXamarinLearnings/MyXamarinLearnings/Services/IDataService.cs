using System.Collections.Generic;
using System.Threading.Tasks;
using MyXamarinLearnings.Models;

namespace MyXamarinLearnings.Services
{
    public interface IDataService
    {
        Task<AddressPredictionResponse> PredictAddress(string addres);
    }
}