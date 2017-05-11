using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyXamarinLearnings.Services
{
    public interface IDataService
    {
        Task<UserAccountModel> Login(UserLoginModel loginData);
    }
}