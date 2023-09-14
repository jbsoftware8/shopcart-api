using CommanApi.Models;

namespace CommanApi.Interface
{
    public interface ILoginService
    {
        Task<Registration_Get_Response> CreateUpdateAsync(RegistrationModel Master, int uid);
        Task<RegistrationModel> SignInAsync(HttpContext httpContext, LoginViewModel model);
        Task<List<RegistrationModel>> GetAllUser(int id);
    }
}
