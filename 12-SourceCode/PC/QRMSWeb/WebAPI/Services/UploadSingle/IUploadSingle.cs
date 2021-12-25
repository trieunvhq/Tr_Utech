using System.Web;

namespace PISAS_API.Service.UploadSigle
{
    public interface IUploadSigle
    {
        HttpPostedFile File { get; set; }

        string OriginName { get; set; }

        string ThumbName { get; set; }

        string KeyMessage { get; set; }

        string Message { get; set; }

        bool Success { get; set; }
    }
}
