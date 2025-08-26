using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

public class AddPersonalCustomer_API
{
    TrvApiLogger apiLogger = new TrvApiLogger();
    PostApiRequest postApiRequest = new PostApiRequest();

    public AddPersonalCustomer_API()
    {
    }

    public Task<CustomerResponse> AddPersonalCustomer(CustomerData customerData)
    {
        var tcs = new TaskCompletionSource<CustomerResponse>();

        if (customerData == null)
        {
            throw new ArgumentException("CustomerData cannot be null");
        }

        string baseUrl = ConfigurationManager.AppSettings["ReceiptApiBaseUrl"];
        string endpoint = baseUrl + "/api/Customer/SavePersonalCustomer";

        try
        {
            var request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "POST";
            request.ContentType = "application/json";

            var jsonData = JsonConvert.SerializeObject(customerData);
            var data = Encoding.UTF8.GetBytes(jsonData);

            request.BeginGetRequestStream(ar =>
            {
                try
                {
                    using (var stream = request.EndGetRequestStream(ar))
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    request.BeginGetResponse(ar2 =>
                    {
                        try
                        {
                            var response = (HttpWebResponse)request.EndGetResponse(ar2);
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                var responseText = reader.ReadToEnd();
                                var responseObject = JsonConvert.DeserializeObject<CustomerResponse>(responseText);
                                tcs.SetResult(responseObject);
                            }
                        }
                        catch (Exception ex)
                        {
                            var erroResponse = new CustomerResponse
                            {
                                ResponseCode = -1,
                                Message = "Error: " + ex.Message
                            };

                            apiLogger.WriteLog("Failed at AddPersonalCustomer_API: " + ex.ToString());
                            tcs.SetResult(erroResponse);
                        }
                    }, null);
                }
                catch (Exception ex)
                {
                    var erroResponse = new CustomerResponse
                    {
                        ResponseCode = -1,
                        Message = "Error: " + ex.Message
                    };

                    apiLogger.WriteLog("Failed at AddPersonalCustomer_API: " + ex.ToString());
                    tcs.SetResult(erroResponse);
                }
            }, null);
        }
        catch (Exception ex)
        {
            var erroResponse = new CustomerResponse
            {
                ResponseCode = -1,
                Message = "Error: " + ex.Message
            };

            apiLogger.WriteLog("Failed at AddPersonalCustomer_API: " + ex.ToString());
            tcs.SetResult(erroResponse);
        }

        return tcs.Task;
    }

    public class CustomerResponse
    {
        public int ResponseCode { get; set; }
        public string Message { get; set; }
        public int? Data { get; set; }
    }

    public class CustomerData
    {
        public int branchCode { get; set; }
        public string initials { get; set; }
        public string lastName { get; set; }
        public string fullName { get; set; }
        public string callingName { get; set; }
        public string homePhoneNo { get; set; }
        public string mobilePhoneNo { get; set; }
        public string nicNo { get; set; }
        public string passportNo { get; set; }
        public string dateOfBirth { get; set; }
        public string status { get; set; }
        public string country { get; set; }
        public string profession { get; set; }
        public string subProfession { get; set; }
        public string personalEmail { get; set; }
        public string homeAddress1 { get; set; }
        public string homeAddress2 { get; set; }
        public string homeAddress3 { get; set; }
        public string homeAddress4 { get; set; }
        public string officeName { get; set; }
        public string designation { get; set; }
        public string officePhoneNumber1 { get; set; }
        public string officePhoneNumber2 { get; set; }
        public string officeFaxNo { get; set; }
        public string officeAddress1 { get; set; }
        public string officeAddress2 { get; set; }
        public string officeAddress3 { get; set; }
        public string officeAddress4 { get; set; }
        public string vatRegNo { get; set; }
        public string svatRegNo { get; set; }
        public string userId { get; set; }
    }
}
