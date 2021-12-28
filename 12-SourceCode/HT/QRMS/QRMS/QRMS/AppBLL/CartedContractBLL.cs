//using PISAS;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace QRMS.AppBLL
{
    public static class CartedContractBLL
    {
        //Thông tin đơn bảo hiểm
        public static int AddCartedContract(CartedContractModel obj)
        {
            return AppDAL.CartedContractDAL.AddCartedContract(obj);
        }

        public static decimal FeeBaseOnDays(this decimal basicFee, int activeDays)
        {
            decimal fee = 0;
            if (activeDays <= 30)
                fee = basicFee / 12;
            else
                fee = basicFee * activeDays / 365;
            return fee;
        }
    }
}
