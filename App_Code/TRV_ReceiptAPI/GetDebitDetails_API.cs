using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for GetDebitDetails_API
/// </summary>
public class GetDebitDetails_API
{
    TrvApiLogger apiLogger = new TrvApiLogger();
    PostApiRequest PostApiRequest = new PostApiRequest();

    public GetDebitDetails_API()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public Task<DebitnoteDetailsApiResponse> GetDebitDetailsdata(DebitNotePostDataModel dataModel)
    {
        var tcs = new TaskCompletionSource<DebitnoteDetailsApiResponse>();

        try
        {
            if (dataModel == null)
            {
                throw new ArgumentException("Debit data be null");
            }

            string baseUrl = ConfigurationManager.AppSettings["ReceiptApiBaseUrl"];
            string endpoint = baseUrl + "/api/Payment/GetPaymentDetails";


            // postapi class
            PostApiRequest.PostApiResponse(endpoint, dataModel).ContinueWith(responseTask =>
            {
                if (responseTask.IsFaulted)
                {
                    apiLogger.WriteLog("Failed at GetDebitDetails_API (PostApiResponse): " + responseTask.Exception);
                    tcs.SetException(responseTask.Exception);
                    return;
                }

                var responseContent = responseTask.Result;
                var responseObject = JsonConvert.DeserializeObject<DebitnoteDetailsApiResponse>(responseContent);
                tcs.SetResult(responseObject);
            });
        }
        catch (Exception ex)
        {
            apiLogger.WriteLog("Failed at SaveDebitDetails_API: " + ex.ToString());
            tcs.SetException(ex);
        }

        return tcs.Task;
    }


    //    string baseUrl = ConfigurationManager.AppSettings["ReceiptApiBaseUrl"];
    //    string endpoint = baseUrl + "/api/Payment/GetPaymentDetails";

    //    var tcs = new TaskCompletionSource<DebitnoteDetailsApiResponse>();

    //    // Make the API request (this needs to be a non-blocking method)
    //    PostApiRequest.PostApiResponse(endpoint, dataModel)
    //        .ContinueWith(responseTask =>
    //        {
    //            if (responseTask.IsFaulted)
    //            {
    //            // Log and set the exception if the task failed
    //            apiLogger.WriteLog("Failed at GetDebitDetailsdata (PostApiResponse): " + responseTask.Exception);
    //                tcs.SetException(responseTask.Exception);
    //            }
    //            else
    //            {
    //                var responseContent = responseTask.Result;

    //                if (responseContent == null)
    //                {
    //                    apiLogger.WriteLog("No response received from API.");
    //                    tcs.SetResult(null);
    //                    return;
    //                }

    //                try
    //                {
    //                // Deserialize the response content
    //                var responseObject = JsonConvert.DeserializeObject<DebitnoteDetailsApiResponse>(responseContent);
    //                    tcs.SetResult(responseObject); // Return the deserialized response
    //            }
    //                catch (Exception ex)
    //                {
    //                // Log any deserialization errors
    //                apiLogger.WriteLog("Failed to deserialize response: " + ex.ToString());
    //                    tcs.SetException(ex);
    //                }
    //            }
    //        });

    //    return tcs.Task;
    //}



    public class DebitnoteDetailsApiResponse
    {
        public int ResponseCode { get; set; }
        public string Message { get; set; }
        public DebitNoteData Data { get; set; }
    }

    public class DebitNoteData
    {
        public int sliBranch { get; set; }
        public int paymentLocation { get; set; }
        public int paymentType { get; set; }
        public int sequenceNo { get; set; }
        public string policyNo { get; set; }
        public string policyType { get; set; }
        public string departmentCode { get; set; }
        public string subDepartment { get; set; }
        public DateTime commenceDate { get; set; }
        public string status { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string address3 { get; set; }
        public string address4 { get; set; }
        public int sumInsured { get; set; }
        public double premium { get; set; }
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
        public string debtorCode { get; set; }
        public string agentCode { get; set; }
        public int organizationCode { get; set; }
        public DateTime startDate { get; set; }
        public DateTime expireDate { get; set; }
        public DateTime paymentDate { get; set; }
        public int paymentMode { get; set; }
        public string paymentCode { get; set; }
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
        public string serviceBranch { get; set; }
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
    }

    public class DebitNotePostDataModel
    {
        public int sliBranch { get; set; }
        public int sequenceNo { get; set; }
        public int paymentLocation { get; set; }
        public string paymentDate { get; set; }
        public string paymentType { get; set; }

    }

 }

