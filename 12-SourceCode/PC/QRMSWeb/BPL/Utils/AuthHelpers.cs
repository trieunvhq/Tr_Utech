using BLL.FactoryBLL.Web.Users;

namespace BLL.Utils.Helpers.Web
{
    public static class AuthHelpers
    {
        public static DAL.User LoginAcount(int? userID)
        {
            if (userID == null || userID <= 0)
            {
                return null;
            }
            return new UserBLL().GetAccountById(userID ?? 0, false);
            
        }
    }
}