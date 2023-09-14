using CommanApi.Models;

namespace CommanApi.Interface
{
    public interface ICategoryService : IDisposable
    {
        Task<IList<CategoryMaster_Select>> GetAllAsync();
        Task<IList<CategoryMaster_Select>> GetAllParentAsync();
        Task<List<CategoryMaster_Select>> GetAllSubCategoryAsync(int id);

        Task<CategoryMaster_Get_Response> CreateUpdateAsync(CategoryMaster_IUD Master, int uid);

        Task<CategoryMaster_Select?> GetByIdAsync(int id);

        Task<CategoryMaster_Get_Response> DeleteAsync(int id);

        Task<CategoryMaster_Get_Response> DeleteAsync(CategoryMaster_IUD Master);


    }
}
