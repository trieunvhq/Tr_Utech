using System.ComponentModel.DataAnnotations;
namespace PISAS_API.Models.Web.Users
{
    public class ResetPasswordDTO
    {
        public int ID { get; set; }


        [Required(ErrorMessage = "Tên tài khoản không được để trống!")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [RegularExpression("^([" + @"\w\.\" + "-]+)@([" + @"\w\-" + "]+)((" + @"\.(\w)" + "{2,3})+)$", ErrorMessage = "Email không đúng định dạng")]
        public string EMAIL { get; set; }
    }
}
