using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AMIS.APIModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Flurl;
using Flurl.Http;
using log4net;

namespace AMIS.APIManagement
{
    public class PurchaseOrderAPIManagement
    {
        static readonly ILog m_log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static FlurlClient flurlClient = ConnectManagement.getClientInstance();

        public PurchaseOrderAPIManagement()
        {

        }
        /*
        public void GetPurchaseOrder()
        {
            try
            {
                int fromDocEntry = 0;
                fromDocEntry = new PurchaseOrderBPL().GetMaxDocEntry();
                GetPurchaseOrderNumbers(fromDocEntry);
                GetPurchaseOrderInfo();
            }
            catch(Exception ex)
            {
                m_log.Error(ex.Message);
            }            
        }

        private void GetPurchaseOrderInfo()
        {
            m_log.Debug("-----Begin GetPurchaseOrderInfo-----");
            try
            {
				var result = new PurchaseOrderBPL();

				PurchaseOrderNumAPIModel purchaseOrderNumModel = result.PurchaseOrderNeedInfo();
                while (purchaseOrderNumModel != null)
                {
                    var ret_docNums = ConnectManagement.GetObjectFromAPI<RespondedAPI<PurchaseOrderInfo>>(UrlName.GetPurchaseOrderInfo, new { purchaseOrderNumModel.docEntry });
                    if (ret_docNums.code == ResponseStatus.RequestSuccessed)
                    {
                        if (string.IsNullOrEmpty(ret_docNums.message))
                        {
                            if (ret_docNums.data != null && ret_docNums.data.items != null && ret_docNums.data.items.Length > 0)
                            {
                                //Update table PurchaseOrderItem
                                List<PurchaseOrderItemAPIModel> purchaseOrderItemAPIModel = new List<PurchaseOrderItemAPIModel>();
                                foreach (var item in ret_docNums.data.items)
                                {
                                    purchaseOrderItemAPIModel.Add(new PurchaseOrderItemAPIModel
                                    {
                                        PurchaseOrderID = purchaseOrderNumModel.ID,
                                        DeliveryDate = ret_docNums.data.deliveryDate,
                                        DocNum = ret_docNums.data.docNum,
                                        LCode = ret_docNums.data.location,
                                        SCode = ret_docNums.data.vendorCode,
                                        SName = ret_docNums.data.vendorName,
                                        ItemCode = item.itemCode,
                                        ItemName = item.itemName,
                                        Quantity = item.quantity,
                                        Unit = item.unit
                                    });
                                }

								result.AddPurchaseOrderItem(purchaseOrderItemAPIModel);
								//Update table PurchaseOrder
								result.PurchaseOrderInfoUpdated(purchaseOrderNumModel);
                            }

                            purchaseOrderNumModel = result.PurchaseOrderNeedInfo();
                        }
                        else
                        {
                            purchaseOrderNumModel = null;
                        }                        
                    }
                    else
                    {
                        purchaseOrderNumModel = null;
                    }
                }                
            }
            catch (Exception ex)
            {
                m_log.Error(ex.Message);
            }
            finally
            {
                m_log.Debug("-----End GetPurchaseOrderInfo-----");
            }
        }

        private void GetPurchaseOrderNumbers(int docEntry)
        {
            m_log.Debug("-----Begin GetPurchaseOrderNumbers-----");
            try
            {
                var ret_docNums = ConnectManagement.GetObjectFromAPI<RespondedAPI<PurchaseOrderData<docNumber>>>(UrlName.GetPurchaseOrderNumbers, new { docEntry, page = 1, pageSize = 100 });

                if (ret_docNums != null)
                {
                    if (ret_docNums.code == ResponseStatus.RequestSuccessed)
                    {
                        if(string.IsNullOrEmpty(ret_docNums.message))
                        {
                            if (ret_docNums.data != null && ret_docNums.data.contents != null)
                            {
                                int curPage = ret_docNums.data.page;
                                int totalPage = ret_docNums.data.total;
                                int pageSize = ret_docNums.data.pageSize;
                                List<PurchaseOrderNumAPIModel> newDocs = new List<PurchaseOrderNumAPIModel>();
                                foreach (var item in ret_docNums.data.contents)
                                {
                                    newDocs.Add(new PurchaseOrderNumAPIModel
                                    {
                                        docEntry = item.docEntry,
                                        //docNum = item.docNum
                                    });
                                }

                                if (totalPage >= 2)
                                {
                                    for (int i = 2; i <= totalPage; i++)
                                    {
                                        ret_docNums = ConnectManagement.GetObjectFromAPI<RespondedAPI<PurchaseOrderData<docNumber>>>(UrlName.GetPurchaseOrderNumbers, new { docEntry, page = i, pageSize });

                                        if (ret_docNums != null)
                                        {
                                            if (ret_docNums.code == ResponseStatus.RequestSuccessed)
                                            {
                                                if (string.IsNullOrEmpty(ret_docNums.message))
                                                {
                                                    if (ret_docNums.data != null && ret_docNums.data.contents != null)
                                                    {
                                                        curPage = ret_docNums.data.page;
                                                        foreach (var item in ret_docNums.data.contents)
                                                        {
                                                            newDocs.Add(new PurchaseOrderNumAPIModel
                                                            {
                                                                docEntry = item.docEntry,
                                                                //docNum = item.docNum
                                                            });
                                                        }
                                                    }
                                                }
                                                else
                                                {

                                                }                                                    
                                            }
                                            else
                                            {
                                                m_log.Debug(string.Format("-----Request API {0} : {1} -----", UrlName.GetPurchaseOrderNumbers, ret_docNums.code));
                                            }
                                        }
                                    }
                                }

                                if (newDocs.Count > 0)
                                {
									var purchase = new PurchaseOrderBPL();
									purchase.AddPurchaseOrder(newDocs);
                                }
                            }
                        }
                        else
                        {

                        }                        
                    }
                    else
                    {
                        m_log.Debug(string.Format("-----Request API {0} : {1} -----", UrlName.GetPurchaseOrderNumbers, ret_docNums.code));
                    }
                }
            }
            catch(Exception ex)
            {
                m_log.Error(ex.Message);
            }
            finally
            {
                m_log.Debug("-----End GetPurchaseOrderNumbers-----");
            }            
        }
        */
    }
}
