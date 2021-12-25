using System.ComponentModel.DataAnnotations;
namespace PISAS_API.Models.Web.Users
{
    public class ChangePasswordDTO
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Mật khẩu hiện tại không được để trống")]
        public string CURRENT_PASSWORD { get; set; }

        [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
        [MaxLength(100, ErrorMessage = " Mật khẩu mới không được vượt quá 100 ký tự")]
        [RegularExpression("^(?=.{8,}$)(?=.*?[a-z])(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[A-Za-z0-9]).*$", ErrorMessage = "Mật khẩu mới phải trên 8 ký tự, có sử dụng chữ Hoa, thường, số và ký tự đặc biệt")]
        public string NEW_PASSWORD { get; set; }

        public string NEW_CONFIRM_PASSWORD { get; set; }

    }
}
