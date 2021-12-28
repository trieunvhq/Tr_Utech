using QRMS.API;
using QRMS.AppLIB.Common;
using QRMS.Models;

namespace QRMS.ViewModels
{
    public class BaseCartedContractViewModel : BaseViewModel
    {
        public CartedContractModel DataModel { get; set; } = new CartedContractModel();
        public CartedContractModel OriginalModel;

        public virtual void LoadDataModel()
        {
             //OriginalModel = CartedContractAPI.GetCartedContractByKey();
            if (OriginalModel != null)
            {
                DataModel.CopyPropertiesFrom(OriginalModel);
            }
        }
    }
}