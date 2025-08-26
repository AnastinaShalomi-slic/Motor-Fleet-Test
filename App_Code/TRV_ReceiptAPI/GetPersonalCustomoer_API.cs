using Newtonsoft.Json;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

public class GetPersonalCustomer_API
{
    TrvApiLogger apiLogger = new TrvApiLogger();
    GetApiRequest GetApiResponse = new GetApiRequest();

    public GetPersonalCustomer_API()
    {

    }

    public async Task<CustomerResponse> GetPersonalCustomerData(string nic)
    {
        if (string.IsNullOrWhiteSpace(nic))
        {
            throw new ArgumentException("ID cannoth be null or white space");
        }

        string baseUrl = ConfigurationManager.AppSettings["ReceiptApiBaseUrl"];
        string endpoint = baseUrl + "/api/Customer/GetPersonalCustomerByGlobalID?globalID=" + nic;

        try
        {
            // getapi class
            var responseContent = await GetApiResponse.GetApiResponse(endpoint);
            var parsedResponse = JsonConvert.DeserializeObject<CustomerResponse>(responseContent);
            return parsedResponse;

        }
        catch (Exception ex)
        {
            apiLogger.WriteLog("Failed at GetPaymentDetails: " + ex.ToString());
            return null;
        }
    }

    public class CustomerResponse
    {
        public int ResponseCode { get; set; }
        public string Message { get; set; }
        public CustomerData Data { get; set; }
    }

    public class CustomerData
    {
        public string Initials { get; set; }
        public string LastName { get; set; }
        public string FullName { get; set; }
        public string CallingName { get; set; }
        public string NicNo { get; set; }
        public string OldNicNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PassportNo { get; set; }
        public string Status { get; set; }
        public string Country { get; set; }
        public string Profession { get; set; }
        public string SubProfession { get; set; }
        public string HomePhoneNo { get; set; }
        public string MobilePhoneNo { get; set; }
        public string PersonalEmail { get; set; }
        public string HomeAddress1 { get; set; }
        public string HomeAddress2 { get; set; }
        public string HomeAddress3 { get; set; }
        public string HomeAddress4 { get; set; }
        public string OfficeName { get; set; }
        public string Designation { get; set; }
        public long CustomerId { get; set; }
        public int RiskLocationId { get; set; }
        public string OfficePhoneNumber1 { get; set; }
        public string OfficePhoneNumber2 { get; set; }
        public string OfficeFaxNo { get; set; }
        public string OfficeAddress1 { get; set; }
        public string OfficeAddress2 { get; set; }
        public string OfficeAddress3 { get; set; }
        public string OfficeAddress4 { get; set; }
        public bool IsSVatAuthorized { get; set; }
        public int SVatAuthorizedUser { get; set; }
        public DateTime SVatAuthorizedDate { get; set; }
        public string VatRegNo { get; set; }
        public string IncomeTaxNo { get; set; }
        public string SvatRegNo { get; set; }
        public string VatRegNoBkp { get; set; }
        public string CredibilityId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Remarks { get; set; }
        public int BranchCode { get; set; }
        public bool IsVatWaived { get; set; }
        public bool IsAdminFeeWaived { get; set; }
        public bool IsOnlyVatWaived { get; set; }
        public bool IsNBTWaived { get; set; }
    }
}
