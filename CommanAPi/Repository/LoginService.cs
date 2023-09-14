using Dapper;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using CommanApi.Interface;
using CommanApi.Models;
using System.Data;
using System.Security.Claims;
using System.Data.SqlClient;

namespace CommanApi.Repository
{
    public class LoginService : ILoginService
    {
        private readonly ISqlConnectionProvider _sqlConnectionProvider;
        private readonly IConverterService _converterService;

        public LoginService(ISqlConnectionProvider sqlConnectionProvider, IConverterService converterService)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
            _converterService = converterService;
        }

       
        public async Task<RegistrationModel> SignInAsync(HttpContext httpContext, LoginViewModel model)
        {
            var userDetail = await GetUsersByCredentialAsync(model.UserName, model.Password);

            if (userDetail is null)
            {
                return null;
            }
           
            return userDetail;
        }

        private async Task<RegistrationModel?> GetUsersByCredentialAsync(string userName, string passWord)
        {
            var parameters = new DynamicParameters();
            var data = await _sqlConnectionProvider.GetSqlConnection()
                .QueryAsync<RegistrationModel>("SP_UserLogin", parameters,
                    commandType: CommandType.StoredProcedure)
                .ConfigureAwait(false);
            var result = data.FirstOrDefault(x => x.UserName == userName & x.Password == passWord);
            return result;
        }
        
        public async Task<Registration_Get_Response> CreateUpdateAsync(RegistrationModel Master, int uid)
        {
           var data= await CreateOrUpdateOrDeleteAsync(Master, uid);
           return data;
        }
        private async Task<Registration_Get_Response> CreateOrUpdateOrDeleteAsync(RegistrationModel Master, int uid = 0)
        {
            var resutt = new Registration_Get_Response();

            try
            {
                var parameters = new DynamicParameters();
                var type = Master.GetType();
                var properties = type.GetProperties();
                foreach (var property in properties.Where(x => x.PropertyType != typeof(SelectList)))
                {
                    var propValue = property.GetValue(Master);
                    var dbType = _converterService.ConvertToDbType(property.PropertyType);
                    parameters.Add(property.Name, propValue, dbType);
                }
                parameters.Add("IUD", uid, DbType.Int32);

                using var connection = _sqlConnectionProvider.GetSqlConnection();
                using var results = connection.QueryMultiple(
                        command: new CommandDefinition(
                            commandText: "SP_Registration_IUD",
                            parameters: parameters,
                            commandType: CommandType.StoredProcedure)
                        );
                return new Registration_Get_Response
                {
                    Response = results.Read<ResponseDto>(),
                    Registration = results.Read<RegistrationModel>()
                };
            }
            catch(Exception e)
            {
                return resutt;
            }
        }

        public async Task<List<RegistrationModel>> GetAllUser(int id)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("ID", id, DbType.Int32);
                var data = await _sqlConnectionProvider.GetSqlConnection()
                    .QueryAsync<RegistrationModel>("SP_UserLogin", parameters,
                        commandType: CommandType.StoredProcedure);

                return data.ToList();
            }
            catch (Exception x)
            {
                throw x;
            }
        }
    }
}
