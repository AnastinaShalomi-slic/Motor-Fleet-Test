using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// Summary description for GetDebitSettlementAmount_API
/// </summary>
public class GetDebitSettlementAmount_API
{
    TrvApiLogger apiLogger = new TrvApiLogger();
    GetApiRequest GetApiResponse = new GetApiRequest();

    public GetDebitSettlementAmount_API()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DebitResponse GetDebitData(string polNo)
    {
        if (string.IsNullOrWhiteSpace(polNo))
        {
            throw new ArgumentException("Policy number cannot be null or white space");
        }
        
        var endpoint = "http://172.24.90.100:8088/Beegeneral/PaymentAutomationService/Service1.svc/GetOutstandingDebits?polno=" + polNo;

        try
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "GET";

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    var responseContent = reader.ReadToEnd();
                    var parsedResponse = JsonConvert.DeserializeObject<DebitResponse>(responseContent);

                    // Deserialize the Data property to a List<DebitData>
                    if (!string.IsNullOrWhiteSpace(parsedResponse.Data))
                    {
                        parsedResponse.DataList = JsonConvert.DeserializeObject<List<DebitData>>(parsedResponse.Data);
                    }

                    return parsedResponse;
                }
            }
        }
        catch (WebException ex)
        {
            // Log or handle exception here as needed
            return null;
        }
    }

    public class DebitResponse
    {
        public string Data { get; set; } // Change to string to match JSON response
        public int ID { get; set; } // Assuming you have an ID field
        public List<DebitData> DataList { get; set; } // This will hold the deserialized data
    }

    public class DebitData
    {
        public int BRANCH { get; set; }
        public DateTime PAYMENT_DATE { get; set; }
        public string DEPARTMENT { get; set; }
        public int DEBIT_NOTE_NO { get; set; }
        public decimal TOTAL { get; set; }
        public decimal BALANCE { get; set; }
        public string AGENTNAME { get; set; }
        public string DEBTORNAME { get; set; }
        public int DEBTOR_CODE { get; set; }
    }
}