namespace CommanApi.Models
{
    public class Registration_Get_Response
    {
        public IEnumerable<ResponseDto> Response { get; set; }
        public IEnumerable<RegistrationModel> Registration { get; set; }
    }
}
