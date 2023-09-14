namespace CommanApi.Models
{
    public class CategoryMaster_Get_Response
    {
        public IEnumerable<ResponseDto> Response { get; set; }
        public IEnumerable<CategoryMaster_Select> category { get; set; }
    }
}
