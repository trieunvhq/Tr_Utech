using QRMS.AppLIB.Common;
using QRMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace QRMS.API
{
    public class InsuranceContractAPI
    {
        public static List<InsuranceContractModel> GetListInsuranceContract(string title = "", int agentId = 1, string insuranceContract = "", DateTime? issueDateFrom = null, DateTime? issueDateTo = null, string idcard = "", string contractstatus = "")
        {
            try
            {
                List<InsuranceContractModel> listItem = new List<InsuranceContractModel>();
                var pageNumber = 1;
                var insurCode = insuranceContract;
                if (!string.IsNullOrEmpty(insuranceContract))
                {
                    var position = insuranceContract.LastIndexOf('-');
                    if (position > 1)
                    {
                        insurCode = insuranceContract.Substring(0, position);
                    }
                }
                var result = APIHelper.GetObjectFromAPI<BaseModel<List<InsuranceContractModel>>>(Constaint.ServiceAddress, Constaint.APIurl.GetListInsuranceContract,
                        new
                        {
                            page = pageNumber,
                            title = title,
                            agentId = agentId,
                            insuranceContract = insurCode,
                            issueDateFrom = issueDateFrom,
                            issueDateTo = issueDateTo,
                            idcard = idcard,
                            contractstatus = contractstatus
                        });
                /*
                                    page = i,
                                    title = title,
                                    agentId = agentId,
                                    insuranceContract = insurCode,
                                    issueDate = issueDateFrom,
                                    idcard = idcard,
                                    contractstatus = contractstatus*/
                if (result.data != null && result.data.Count > 0)
                {
                    foreach (var item in result.data)
                    {
                        if (!string.IsNullOrEmpty(insuranceContract))
                        {
                            var code = item.ALTER_TIMES == null ? item.CONTRACT_CODE : item.CONTRACT_CODE + "-" + ConvertString(item.ALTER_TIMES.ToString());
                            if (code.Contains(insuranceContract))
                            {
                                listItem.Add(new InsuranceContractModel()
                                {
                                    ID = item.ID,
                                    INSUR_PRODUCT_ID = item.INSUR_PRODUCT_ID,
                                    INSUR_PRODUCT_CODE = item.INSUR_PRODUCT_CODE,
                                    INSUR_PRODUCT_NAME = item.INSUR_PRODUCT_NAME,
                                    CONTRACT_CODE = item.CONTRACT_CODE,
                                    CONTRACT_EXPIRE_DATE = item.CONTRACT_EXPIRE_DATE,
                                    CUSTOMER_ID = item.CUSTOMER_ID,
                                    CUST_NAME = item.CUST_NAME,
                                    CUST_PHONE = item.CUST_PHONE,
                                    IDENTITY_NO = item.IDENTITY_NO,
                                    CONTRACT_STATUS = item.CONTRACT_STATUS,
                                    CONTRACT_ISSUE_TYPE = item.CONTRACT_ISSUE_TYPE,
                                    ALTER_STATUS = item.ALTER_STATUS,
                                    ALTER_TIMES = item.ALTER_TIMES,
                                    AGENT_ID = item.AGENT_ID
                                });
                            }
                        }
                        else
                        {
                            listItem.Add(new InsuranceContractModel()
                            {
                                ID = item.ID,
                                INSUR_PRODUCT_ID = item.INSUR_PRODUCT_ID,
                                INSUR_PRODUCT_CODE = item.INSUR_PRODUCT_CODE,
                                INSUR_PRODUCT_NAME = item.INSUR_PRODUCT_NAME,
                                CONTRACT_CODE = item.CONTRACT_CODE,
                                CONTRACT_EXPIRE_DATE = item.CONTRACT_EXPIRE_DATE,
                                CUSTOMER_ID = item.CUSTOMER_ID,
                                CUST_NAME = item.CUST_NAME,
                                CUST_PHONE = item.CUST_PHONE,
                                IDENTITY_NO = item.IDENTITY_NO,
                                CONTRACT_STATUS = item.CONTRACT_STATUS,
                                CONTRACT_ISSUE_TYPE = item.CONTRACT_ISSUE_TYPE,
                                ALTER_STATUS = item.ALTER_STATUS,
                                ALTER_TIMES = item.ALTER_TIMES,
                                AGENT_ID = item.AGENT_ID
                            });
                        }
                    }

                    if (result.data[0].PageTotal > 1)
                    {
                        pageNumber++;
                        for (int i = pageNumber; i <= result.data[0].PageTotal; i++)
                        {
                            var result2 = APIHelper.GetObjectFromAPI<BaseModel<List<InsuranceContractModel>>>(Constaint.ServiceAddress, Constaint.APIurl.GetListInsuranceContract,
                                new
                                {
                                    page = i,
                                    title = title,
                                    agentId = agentId,
                                    insuranceContract = insurCode,
                                    issueDate = issueDateFrom,
                                    idcard = idcard,
                                    contractstatus = contractstatus
                                });
                            if (result2.data != null && result2.data.Count > 0)
                            {
                                foreach (var item in result2.data)
                                {
                                    if (!string.IsNullOrEmpty(insuranceContract))
                                    {
                                        var code = item.ALTER_TIMES == null ? item.CONTRACT_CODE : item.CONTRACT_CODE + "-" + ConvertString(item.ALTER_TIMES.ToString());
                                        if (code.Contains(insuranceContract))
                                        {
                                            listItem.Add(new InsuranceContractModel()
                                            {
                                                ID = item.ID,
                                                INSUR_PRODUCT_ID = item.INSUR_PRODUCT_ID,
                                                INSUR_PRODUCT_CODE = item.INSUR_PRODUCT_CODE,
                                                INSUR_PRODUCT_NAME = item.INSUR_PRODUCT_NAME,
                                                CONTRACT_CODE = item.CONTRACT_CODE,
                                                CONTRACT_EXPIRE_DATE = item.CONTRACT_EXPIRE_DATE,
                                                CUSTOMER_ID = item.CUSTOMER_ID,
                                                CUST_NAME = item.CUST_NAME,
                                                CUST_PHONE = item.CUST_PHONE,
                                                IDENTITY_NO = item.IDENTITY_NO,
                                                CONTRACT_STATUS = item.CONTRACT_STATUS,
                                                CONTRACT_ISSUE_TYPE = item.CONTRACT_ISSUE_TYPE,
                                                ALTER_STATUS = item.ALTER_STATUS,
                                                ALTER_TIMES = item.ALTER_TIMES,
                                                AGENT_ID = item.AGENT_ID
                                            });
                                        }
                                    }
                                    else
                                    {
                                        listItem.Add(new InsuranceContractModel()
                                        {
                                            ID = item.ID,
                                            INSUR_PRODUCT_ID = item.INSUR_PRODUCT_ID,
                                            INSUR_PRODUCT_CODE = item.INSUR_PRODUCT_CODE,
                                            INSUR_PRODUCT_NAME = item.INSUR_PRODUCT_NAME,
                                            CONTRACT_CODE = item.CONTRACT_CODE,
                                            CONTRACT_EXPIRE_DATE = item.CONTRACT_EXPIRE_DATE,
                                            CUSTOMER_ID = item.CUSTOMER_ID,
                                            CUST_NAME = item.CUST_NAME,
                                            CUST_PHONE = item.CUST_PHONE,
                                            IDENTITY_NO = item.IDENTITY_NO,
                                            CONTRACT_STATUS = item.CONTRACT_STATUS,
                                            CONTRACT_ISSUE_TYPE = item.CONTRACT_ISSUE_TYPE,
                                            ALTER_STATUS = item.ALTER_STATUS,
                                            ALTER_TIMES = item.ALTER_TIMES,
                                            AGENT_ID = item.AGENT_ID
                                        });
                                    }
                                }
                            }
                        }
                    }
                }
                return listItem;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.InsuranceContractAPI.GetListInsuranceContract()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static List<InsuranceContractModel> GetOnePageInsuranceContract(
            string title = ""
            , int agentId = 1
            , string insuranceContract = ""
            , DateTime? issueDateFrom = null
            , DateTime? issueDateTo = null
            , string idcard = ""
            , string contractstatus = ""
            , string contractproduct = ""
            , int pageNumber_ = 1)
        {
            try
            {
                List<InsuranceContractModel> listItem = new List<InsuranceContractModel>();

                var insurCode = insuranceContract;
                if (!string.IsNullOrEmpty(insuranceContract))
                {
                    var position = insuranceContract.LastIndexOf('-');
                    if (position > 1)
                    {
                        insurCode = insuranceContract.Substring(0, position);
                    }
                }
                if (pageNumber_ == 1)
                {
                    var result = APIHelper.GetObjectFromAPI<BaseModel<List<InsuranceContractModel>>>(Constaint.ServiceAddress, Constaint.APIurl.GetListInsuranceContract,
                            new
                            {
                                page = pageNumber_,
                                title = title,
                                agentId = agentId,
                                insuranceContract = insurCode,
                                issueDateFrom = issueDateFrom,
                                issueDateTo = issueDateTo,
                                idcard = idcard,
                                contractstatus = contractstatus,
                                contractproduct = contractproduct
                            });
                    if (result.data != null && result.data.Count > 0)
                    {
                        foreach (var item in result.data)
                        {
                            if (!string.IsNullOrEmpty(insuranceContract))
                            {
                                var code = item.ALTER_TIMES == null ? item.CONTRACT_CODE : item.CONTRACT_CODE + "-" + ConvertString(item.ALTER_TIMES.ToString());
                                if (code.Contains(insuranceContract))
                                {
                                    listItem.Add(new InsuranceContractModel()
                                    {
                                        ID = item.ID,
                                        INSUR_PRODUCT_ID = item.INSUR_PRODUCT_ID,
                                        INSUR_PRODUCT_CODE = item.INSUR_PRODUCT_CODE,
                                        INSUR_PRODUCT_NAME = item.INSUR_PRODUCT_NAME,
                                        CONTRACT_CODE = item.CONTRACT_CODE,
                                        CONTRACT_EXPIRE_DATE = item.CONTRACT_EXPIRE_DATE,
                                        CUSTOMER_ID = item.CUSTOMER_ID,
                                        CUST_NAME = item.CUST_NAME,
                                        CUST_PHONE = item.CUST_PHONE,
                                        IDENTITY_NO = item.IDENTITY_NO,
                                        CONTRACT_STATUS = item.CONTRACT_STATUS,
                                        CONTRACT_ISSUE_TYPE = item.CONTRACT_ISSUE_TYPE,
                                        ALTER_STATUS = item.ALTER_STATUS,
                                        ALTER_TIMES = item.ALTER_TIMES,
                                        AGENT_ID = item.AGENT_ID,
                                        PageTotal = item.PageTotal
                                    });
                                }
                            }
                            else
                            {
                                listItem.Add(new InsuranceContractModel()
                                {
                                    ID = item.ID,
                                    INSUR_PRODUCT_ID = item.INSUR_PRODUCT_ID,
                                    INSUR_PRODUCT_CODE = item.INSUR_PRODUCT_CODE,
                                    INSUR_PRODUCT_NAME = item.INSUR_PRODUCT_NAME,
                                    CONTRACT_CODE = item.CONTRACT_CODE,
                                    CONTRACT_EXPIRE_DATE = item.CONTRACT_EXPIRE_DATE,
                                    CUSTOMER_ID = item.CUSTOMER_ID,
                                    CUST_NAME = item.CUST_NAME,
                                    CUST_PHONE = item.CUST_PHONE,
                                    IDENTITY_NO = item.IDENTITY_NO,
                                    CONTRACT_STATUS = item.CONTRACT_STATUS,
                                    CONTRACT_ISSUE_TYPE = item.CONTRACT_ISSUE_TYPE,
                                    ALTER_STATUS = item.ALTER_STATUS,
                                    ALTER_TIMES = item.ALTER_TIMES,
                                    AGENT_ID = item.AGENT_ID,
                                    PageTotal = item.PageTotal
                                });
                            }
                        }
                    }
                    return listItem;
                }
                else
                {
                    var result2 = APIHelper.GetObjectFromAPI<BaseModel<List<InsuranceContractModel>>>(Constaint.ServiceAddress, Constaint.APIurl.GetListInsuranceContract,
                                new
                                {
                                    page = pageNumber_,
                                    title = title,
                                    agentId = agentId,
                                    insuranceContract = insurCode,
                                    issueDate = issueDateFrom,
                                    idcard = idcard,
                                    contractstatus = contractstatus,
                                    contractproduct = contractproduct
                                });
                    if (result2.data != null && result2.data.Count > 0)
                    {
                        foreach (var item in result2.data)
                        {
                            if (!string.IsNullOrEmpty(insuranceContract))
                            {
                                var code = item.ALTER_TIMES == null ? item.CONTRACT_CODE : item.CONTRACT_CODE + "-" + ConvertString(item.ALTER_TIMES.ToString());
                                if (code.Contains(insuranceContract))
                                {
                                    listItem.Add(new InsuranceContractModel()
                                    {
                                        ID = item.ID,
                                        INSUR_PRODUCT_ID = item.INSUR_PRODUCT_ID,
                                        INSUR_PRODUCT_CODE = item.INSUR_PRODUCT_CODE,
                                        INSUR_PRODUCT_NAME = item.INSUR_PRODUCT_NAME,
                                        CONTRACT_CODE = item.CONTRACT_CODE,
                                        CONTRACT_EXPIRE_DATE = item.CONTRACT_EXPIRE_DATE,
                                        CUSTOMER_ID = item.CUSTOMER_ID,
                                        CUST_NAME = item.CUST_NAME,
                                        CUST_PHONE = item.CUST_PHONE,
                                        IDENTITY_NO = item.IDENTITY_NO,
                                        CONTRACT_STATUS = item.CONTRACT_STATUS,
                                        CONTRACT_ISSUE_TYPE = item.CONTRACT_ISSUE_TYPE,
                                        ALTER_STATUS = item.ALTER_STATUS,
                                        ALTER_TIMES = item.ALTER_TIMES,
                                        AGENT_ID = item.AGENT_ID,
                                        PageTotal = item.PageTotal
                                    });
                                }
                            }
                            else
                            {
                                listItem.Add(new InsuranceContractModel()
                                {
                                    ID = item.ID,
                                    INSUR_PRODUCT_ID = item.INSUR_PRODUCT_ID,
                                    INSUR_PRODUCT_CODE = item.INSUR_PRODUCT_CODE,
                                    INSUR_PRODUCT_NAME = item.INSUR_PRODUCT_NAME,
                                    CONTRACT_CODE = item.CONTRACT_CODE,
                                    CONTRACT_EXPIRE_DATE = item.CONTRACT_EXPIRE_DATE,
                                    CUSTOMER_ID = item.CUSTOMER_ID,
                                    CUST_NAME = item.CUST_NAME,
                                    CUST_PHONE = item.CUST_PHONE,
                                    IDENTITY_NO = item.IDENTITY_NO,
                                    CONTRACT_STATUS = item.CONTRACT_STATUS,
                                    CONTRACT_ISSUE_TYPE = item.CONTRACT_ISSUE_TYPE,
                                    ALTER_STATUS = item.ALTER_STATUS,
                                    ALTER_TIMES = item.ALTER_TIMES,
                                    AGENT_ID = item.AGENT_ID,
                                    PageTotal = item.PageTotal
                                });
                            }
                        }
                    }
                    return listItem;
                }
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.InsuranceContractAPI.GetOnePageInsuranceContract()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static int AddInsuranceContract(int idCarted, string contractCode, int status)
        {
            try
            {
                var result = APIHelper.GetObjectFromAPI<BaseModel<int>>(Constaint.ServiceAddress, "/api/insurancecontract/add1",
                    new
                    {
                        idCarted = idCarted,
                        contractCode = contractCode,
                        status = status
                    });
                return result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.InsuranceContractAPI.AddInsuranceContract()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return -1;
            }
        }

        public static int AddInsuranceContract(InsuranceContractModel obj)
        {
            try
            {
                var result = APIHelper.PostObjectToAPI<BaseModel<int>>(Constaint.ServiceAddress, "/api/insurancecontract/add2", obj);
                return result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.InsuranceContractAPI.AddInsuranceContract()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return -1;
            }
        }

        public static int AddInsuranceContract(InsuranceContractModel objInsur, CustomerModel objCust)
        {
            try
            {
                var obj = new
                {
                    INSUR_CONTRACT = objInsur,
                    CUSTOMER = objCust
                };
                var result = APIHelper.PostObjectToAPI<BaseModel<int>>(Constaint.ServiceAddress, Constaint.APIurl.AddInsuranceContract, obj);
                return result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.InsuranceContractAPI.AddInsuranceContract()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return -1;
            }
        }

        public static string CopyInsuranceContractToCartedContract(int id)
        {
            try
            {
                var result = APIHelper.GetObjectFromAPI<BaseModel<string>>(Constaint.ServiceAddress, "/api/insurancecontract/copycontract",
                    new
                    {
                        id = id
                    });
                return result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.InsuranceContractAPI.CopyInsuranceContractToCartedContract()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }

        public static int AddInsuranceContractSDBS(int id = 0)
        {
            try
            {
                var result = APIHelper.GetObjectFromAPI<BaseModel<int>>(Constaint.ServiceAddress, Constaint.APIurl.AddInsuranceContractSDBS, new { id = id });
                return result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.InsuranceContractAPI.AddInsuranceContractSDBS()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return -1;
            }
        }

        private static string ConvertString(string a)
        {
            try
            {
                return a.PadLeft(2, '0');
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.InsuranceContractAPI.ConvertString()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return string.Empty;
            }
        }

        public static async Task<bool[]> CheckInsuredPersonAsync(string ContractCode, string productCode, params string[] Identity)
        {
            try
            {
                if (string.IsNullOrEmpty(productCode)) return Identity.Select(a => false).ToArray();
                else
                {
                    BaseModel<List<bool>> CallAPI = await APIHelper.PostObjectToAPIAsync<BaseModel<List<bool>>>
                        (Constaint.ServiceAddress, Constaint.APIurl.CheckInsPerExist, new { productCode, Identity }).ConfigureAwait(false);
                    return CallAPI.data.ToArray();
                }
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.InsuranceContractAPI.CheckInsuredPersonAsync()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return new bool[0];
            }
        }

        public static int CopyInsuranceToCarted(int insur_iD, string renewal, int userID)
        {
            try
            {
                var Result = APIHelper.PostObjectToAPI<BaseModel<int>>(Constaint.ServiceAddress, Constaint.APIurl.CopyToCart,
                    new
                    {
                        id = insur_iD,
                        TypeCopy = renewal,
                        userID = userID
                    });
                return Result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.InsuranceContractAPI.CopyInsuranceToCarted()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return 0;
            }
        }

        public static CartedContractModel GetCarMaterialInsuranceContractByKey(int ID, object apiParams = null)
        {
            try
            {
                var result = APIHelper.PostObjectToAPIAsync<BaseModel<CartedContractModel>>(Constaint.ServiceAddress, Constaint.APIurl.GetCarMaterialInsuranceContractByKey, apiParams ?? new { key = ID, keyType = nameof(ID) });
                return result.Result.data;
            }
            catch (Exception ex)
            {
                // Log ex to db
                var FCMToken = Application.Current.Properties.Keys.Contains("Fcmtocken");
                var FCMTockenValue = String.Empty;
                if (FCMToken)
                {
                    FCMTockenValue = Application.Current.Properties["Fcmtocken"].ToString();
                }
                var fcmToken = FCMTockenValue;
                var appType = Constaint.App_Type.Agent;
                var osType = Device.OS.ToString();
                var actionName = "QRMS.API.InsuranceContractAPI.GetCarMaterialInsuranceContractByKey()";
                var userId = FormTypeModel.UserID;
                LogExAPI.AddLogEx(fcmToken, appType, osType, actionName, ex.ToString(), userId);

                return null;
            }
        }


    }
}
