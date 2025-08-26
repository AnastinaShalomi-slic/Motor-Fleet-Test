using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public class SavePolicyDetails_API
{
    TrvApiLogger apiLogger = new TrvApiLogger();
    PostApiRequest postApiRequest = new PostApiRequest();

    public SavePolicyDetails_API()
    {

    }

    public Task<SavePolApiResponse> AddPolicy(PolicyModel policyDetails)
    {
        var tcs = new TaskCompletionSource<SavePolApiResponse>();

        try
        {
            if (policyDetails == null)
            {
                throw new ArgumentException("Policy data cannot be null");
            }

            string baseUrl = ConfigurationManager.AppSettings["ReceiptApiBaseUrl"];
            string endpoint = baseUrl + "/api/PolicyMaster/SavePolicyDetails";

            // postapi class
            postApiRequest.PostApiResponse(endpoint, policyDetails).ContinueWith(responseTask =>
            {
                if (responseTask.IsFaulted)
                {
                    apiLogger.WriteLog("Failed at SavePolicyDetails_API (PostApiResponse): " + responseTask.Exception);
                    tcs.SetException(responseTask.Exception);
                    return;
                }

                var responseContent = responseTask.Result;
                var responseObject = JsonConvert.DeserializeObject<SavePolApiResponse>(responseContent);
                tcs.SetResult(responseObject);
            });
        }
        catch (Exception ex)
        {
            apiLogger.WriteLog("Failed at SavePolicyDetails_API: " + ex.ToString());
            tcs.SetException(ex);
        }

        return tcs.Task;
    }

    public class SavePolApiResponse
    {
        public int ResponseCode { get; set; }
        public string Message { get; set; }
        public string Data { get; set; }
    }

    public class PolicyModel
    {
        public string policyNo { get; set; }
        public DateTime commenceDate { get; set; }
        public string status { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string address4 { get; set; }
        public int telePhone1 { get; set; }
        public int telePhone2 { get; set; }
        public int faxNo { get; set; }
        public string email { get; set; }
        public string policyType { get; set; }
        public int sumInsured { get; set; }
        public double premium { get; set; }
        public double rcc { get; set; }
        public double tc { get; set; }
        public double vat { get; set; }
        public double cess { get; set; }
        public double roadTax { get; set; }
        public double policyFee { get; set; }
        public string rateCode { get; set; }
        public string rateCode2 { get; set; }
        public int agentCode { get; set; }
        public int organizationCode { get; set; }
        public int debtorCode { get; set; }
        public DateTime startDate { get; set; }
        public DateTime expireDate { get; set; }
        public DateTime paymentDate { get; set; }
        public string paymentBranch { get; set; }
        public string currencyCode { get; set; }
        public string paymentType { get; set; }
        public int sliNo1 { get; set; }
        public int sliNo2 { get; set; }
        public decimal stampFee { get; set; }
        public string department { get; set; }
        public int vatRate { get; set; }
        public double nbt { get; set; }
        public DateTime createdDate { get; set; }
        public int createdUser { get; set; }
        public string isUnderWritingPolicy { get; set; }
        public string clientId { get; set; }
        public string vatRegNo { get; set; }
        public string sVatRegNo { get; set; }
        public decimal sVatAmount { get; set; }
        public int subAgentCode { get; set; }
        public string vesselRegNo { get; set; }
    }
}
