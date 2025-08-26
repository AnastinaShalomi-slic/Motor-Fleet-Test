using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Developed by Praveen 31636
/// </summary>
public class SavePaymentDetails_API
{
    TrvApiLogger apiLogger = new TrvApiLogger();
    PostApiRequest PostApiRequest = new PostApiRequest();

    public SavePaymentDetails_API()
    {

    }

    public Task<SavePaymentApiResponse> AddDetails(PaymentDetailsModel paymentDetailsModel)
    {
        var tcs = new TaskCompletionSource<SavePaymentApiResponse>();

        try
        {
            if (paymentDetailsModel == null)
            {
                throw new ArgumentException("Payment data cannot be null");
            }

            string baseUrl = ConfigurationManager.AppSettings["ReceiptApiBaseUrl"];
            string endpoint = baseUrl + "/api/Payment/SavePaymentDetails";


            // postapi class
            PostApiRequest.PostApiResponse(endpoint, paymentDetailsModel).ContinueWith(responseTask =>
            {
                if (responseTask.IsFaulted)
                {
                    apiLogger.WriteLog("Failed at SavePaymentDetails_API (PostApiResponse): " + responseTask.Exception);
                    tcs.SetException(responseTask.Exception);
                    return;
                }

                var responseContent = responseTask.Result;
                var responseObject = JsonConvert.DeserializeObject<SavePaymentApiResponse>(responseContent);
                tcs.SetResult(responseObject);
            });
        }
        catch (Exception ex)
        {
            apiLogger.WriteLog("Failed at SavePaymentDetails_API: " + ex.ToString());
            tcs.SetException(ex);
        }

        return tcs.Task;
    }

    public class SavePaymentApiResponse
    {
        public int ResponseCode { get; set; }
        public string Message { get; set; }
        public int Data { get; set; }
    }

    public class PaymentDetailsModel
    {
        public int sliBranch { get; set; }
        public string policyNo { get; set; }
        public string vehicleRegProvinceCode { get; set; }
        public string vehicleRegNo { get; set; }
        public string policyType { get; set; }
        public string departmentCode { get; set; }
        public string subDepartment { get; set; }
        public DateTime commenceDate { get; set; }
        public string title { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string address4 { get; set; }
        public int sumInsured { get; set; }
        public double premium { get; set; }
        public double adminfee { get; set; }
        public double rcc { get; set; }
        public double tc { get; set; }
        public double vat { get; set; }
        public string taxStatusId { get; set; }
        public double cess { get; set; }
        public double roadTax { get; set; }
        public double stampFee { get; set; }
        public double policyFee { get; set; }
        public double excess { get; set; }
        public string rateCode { get; set; }
        public int agentCode { get; set; }
        public int organizationCode { get; set; }
        public DateTime startDate { get; set; }
        public DateTime expireDate { get; set; }
        public DateTime paymentDate { get; set; }
        public int paymentMode { get; set; }
        public int sliNo1 { get; set; }
        public int sliNo2 { get; set; }
        public int businessTypeID { get; set; }
        public string vatRegNo { get; set; }
        public int paymentMethod1 { get; set; }
        public double paymentAmount1 { get; set; }
        public int mailRemNo { get; set; }
        public double commissionRate { get; set; }
        public int bankCode1 { get; set; }
        public int bankCode2 { get; set; }
        public int serviceBranch { get; set; }
        public string clientId { get; set; }
        public string currencyCode { get; set; }
        public double currancyRate { get; set; }
        public string createdUser { get; set; }
        public string manualReceiptNo { get; set; }
        public int districtID { get; set; }
        public int riskLocationID { get; set; }
        public string isWithHoldingTax { get; set; }
        public double vatRate { get; set; }
        public double nbt { get; set; }
        public string sVatRegNo { get; set; }
        public double sVatAmount { get; set; }
        public string status { get; set; }
        public string chequeNo1 { get; set; }
        public int paymentMethod2 { get; set; }
        public double paymentAmount2 { get; set; }
        public string chequeNo2 { get; set; }
        public int bankBranchCode1 { get; set; }
        public int bankBranchCode2 { get; set; }
    }
}
