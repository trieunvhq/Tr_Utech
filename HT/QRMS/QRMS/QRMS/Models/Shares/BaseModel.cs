
using Newtonsoft.Json;

namespace QRMS.Models
{
    [JsonObject(MemberSerialization = MemberSerialization.OptIn)]
    public class BaseModel<T>
    {
        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }
        [JsonProperty(PropertyName = "RespondCode")]
        public string RespondCode { get; set; }
        [JsonProperty(PropertyName = "ErrorCode")]
        public string ErrorCode { get; set; }
        [JsonProperty(PropertyName = "data")]
        public T data { get; set; }
    }
}
