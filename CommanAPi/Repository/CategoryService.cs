using Dapper;
using CommanApi.Models;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using CommanApi.Interface;
using System.Data.SqlClient;

namespace CommanApi.Repository
{
    public class CategoryService : ICategoryService
    {
        private readonly ISqlConnectionProvider _sqlConnectionProvider;
        private readonly IWebHostEnvironment _hostEnvironment;
        private static IList<CategoryMaster_Select> _list;
        private readonly IConfiguration _configuration;

        public CategoryService(ISqlConnectionProvider sqlConnectionProvider, IWebHostEnvironment hostEnvironment, IConfiguration configuration)
        {
            _sqlConnectionProvider = sqlConnectionProvider;
            _hostEnvironment = hostEnvironment;
            _configuration = configuration;
        }

        public async Task<CategoryMaster_Get_Response> CreateUpdateAsync([FromForm] CategoryMaster_IUD Master, int uid)
        {

            var parameters = new DynamicParameters();
            string strFileName = "";
            if (Master.CM_Photourl != null)
            {
                string uploadsFolder = Path.Combine(_configuration.GetSection("FilePath").Value, Path.Combine("wwwroot", "Upload", "category"));
                strFileName = Common.StringChange(Master.CM_Title) + ".jpg";
                string filePath = Path.Combine(uploadsFolder, strFileName);
                int intRetrun = 0;
                if (Master.CM_ParentID == 0)
                {
                    intRetrun = Common.ResizeFile(800, 533, Master.CM_Photourl, filePath);
                }
                else
                {
                    intRetrun = Common.ResizeFile(1000, 533, Master.CM_Photourl, filePath);
                }
                if (intRetrun == 0)
                {
                    throw new Exception("Error..");
                }
            }
            Master.CM_Photo = strFileName;
            parameters.Add("IUD", uid, DbType.Int32);
            parameters.Add("CM_CategoryID", Master.CM_CategoryID, DbType.Int32);
            parameters.Add("CM_Title", Master.CM_Title, DbType.String);
            parameters.Add("CM_ParentID", Master.CM_ParentID, DbType.String);
            parameters.Add("CM_Photo", Master.CM_Photo, DbType.String);
            parameters.Add("CM_Description", Master.CM_Description, DbType.String);
            parameters.Add("CM_IsActive", Master.CM_IsActive, DbType.Int32);
            
            using var connection = _sqlConnectionProvider.GetSqlConnection();
            using var results = connection.QueryMultiple(
                    command: new CommandDefinition(
                        commandText: "SP_CategoryMaster_IUD",
                        parameters: parameters,
                        commandType: CommandType.StoredProcedure)
                    );
            return new CategoryMaster_Get_Response
            {
                Response = results.Read<ResponseDto>(),
                category = results.Read<CategoryMaster_Select>()
            };

        }

        public async Task<CategoryMaster_Get_Response> DeleteAsync(int id)
        {
            var data = new CategoryMaster_IUD
            {
                CM_CategoryID = id,
                CM_IsActive = EnumClass.IsActive.Delete
            };

            var Responsedata = await DeleteAsync(data);
            return Responsedata;
        }

        public async Task<IList<CategoryMaster_Select>> GetAllAsync()
        {


            var data = await _sqlConnectionProvider.GetSqlConnection()
                       .QueryAsync<CategoryMaster_Select>("SP_CategoryMaster_Select", null,
                           commandType: CommandType.StoredProcedure);

            return _list = data.ToArray();

        }


        public async Task<CategoryMaster_Select?> GetByIdAsync(int id)
        {

            await GetAllAsync();
            return _list?.SingleOrDefault(c => c.CM_CategoryID == id);
        }

        public async Task<CategoryMaster_Get_Response> DeleteAsync(CategoryMaster_IUD Master)
        {
            var Responsedata = await CreateUpdateAsync(Master, 2);
            return Responsedata;
        }

        public void Dispose()
        {
            _sqlConnectionProvider.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<IList<CategoryMaster_Select>> GetAllParentAsync()
        {

            var data = await _sqlConnectionProvider.GetSqlConnection()
                       .QueryAsync<CategoryMaster_Select>("SP_ParentCategory_Select", null,
                           commandType: CommandType.StoredProcedure);

            return _list = data.ToArray();
        }

        public async Task<List<CategoryMaster_Select>> GetAllSubCategoryAsync(int id)
        {

            var parameters = new DynamicParameters();
            parameters.Add("CM_CategoryID", id, DbType.Int32);

            var data = await _sqlConnectionProvider.GetSqlConnection()
                  .QueryAsync<CategoryMaster_Select>("SP_SubCategorys_Select", parameters,
                      commandType: CommandType.StoredProcedure);
            return data.ToList();
        }
    }
}