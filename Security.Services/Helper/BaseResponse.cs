
namespace Security.Services.Helper
{
    public class BaseResponse
    {
        public bool succeeded { get; set; }
        public string message { get; set; }
        public List<string> errors { get; set; }
    }
}
