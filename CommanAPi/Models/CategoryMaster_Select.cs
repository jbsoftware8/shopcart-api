using Microsoft.AspNetCore.Mvc.Rendering;

namespace CommanApi.Models
{
    public class CategoryMaster_Select
    {
        public int? CM_CategoryID { get; set; }

        public string? CM_Title { get; set; }
        public string? ParentTitle { get; set; }
        public int? CM_ParentID { get; set; }

        public string? CM_Photo { get; set; }

        public string? CM_Description { get; set; }
        public SelectList SelectListCategory { get; set; }

        public EnumClass.IsActive CM_IsActive { get; set; } = EnumClass.IsActive.Active;


    }
}
