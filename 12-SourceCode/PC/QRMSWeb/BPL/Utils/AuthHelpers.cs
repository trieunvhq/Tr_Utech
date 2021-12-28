using BLL.FactoryBLL.Web.Users;
using BPL.Models.Web;

namespace BLL.Utils.Helpers.Web
{
    public static class AuthHelpers
    {
        public static UserModel LoginAcount(int? userID)
        {
            if (userID == null || userID <= 0)
            {
                return null;
            }
            return new UserBLL().GetAccountById(userID ?? 0, false);
            
        }
    }
}