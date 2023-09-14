namespace CommanApi.Models
{
    public class EnumClass
    {
        public enum Gender
        {
            Male = 0,
            Female = 1
        }

        public enum MaritalStatus
        {
            Single = 0,
            Married = 1,
            Divorced = 2
        }

        public enum Religion
        {
            Hindu = 0,
            Muslim = 1,
            Christian = 2
        }

        public enum IsActive
        {
            Delete = -1,  // failure
            InActive = 0, // pending 
            Active = 1, // success 
            Confirm = 2
        }

        public enum Months
        {
            January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
        }

        public enum HikeType
        {
            Amount = 0,
            Percentage = 1
        }
        public enum Relation
        {
            Father = 0,
            Husband = 1,
            Wife = 2,
            Son,
            Daughter,
            Mother,
            Brother,
            Sister,
            Cousin
        }

        public enum AllowanceCategory
        {
            Fixed = 0,
            variant = 1
        }
    }
}
