using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommanApi.Models
{
    public class CategoryMaster_IUD
    {
        public int? CM_CategoryID { get; set; }
        public string? CM_Title { get; set; }
        public int? CM_ParentID { get; set; }
        public string? CM_Photo { get; set; }

        public string? CM_Description { get; set; }

        public IFormFile CM_Photourl { get; set; }

        public EnumClass.IsActive CM_IsActive { get; set; } = EnumClass.IsActive.Active;
        public SelectList SelectListCategory { get; set; }

    }
}
