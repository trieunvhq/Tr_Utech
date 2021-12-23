
namespace WebAPI.Models
{
    public class BaseModel
    {
        public string Message { get; set; }
        public string RespondCode { get; set; }
        public string ErrorCode { get; set; }
        public virtual object data { get; set; }
    }
}
