using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for SaveDebitSettlement_API
/// </summary>
public class SaveDebitSettlement_API
{
    public SaveDebitSettlement_API()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public DebitResponse SaveDebitsettlementData(string polNo, decimal paymentAmount)
    {
        if (string.IsNullOrWhiteSpace(polNo) || paymentAmount <= 0)
        {
            throw new ArgumentException("Policy number cannot be null or white space, and payment amount must be greater than zero.");
        }

        var endpoint = "http://172.24.90.100:8088/Beegeneral/PaymentAutomationService/Service1.svc/DebitSettlement?" +"policyNumber=" + polNo + "&paymentAmount=" + paymentAmount;

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
                        parsedResponse.DataList = JsonConvert.DeserializeObject<List<DebitsettlementData>>(parsedResponse.Data);
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
        public List<DebitsettlementData> DataList { get; set; } // This will hold the deserialized data
    }

    public class DebitsettlementData
    {     
        public int DebitNoteBranch { get; set; }
        public int DebitNoteNo { get; set; }
        public int ReceiptNo { get; set; }
        public int ReceiptBranch { get; set; }
        public long DateofCommencement { get; set; } // Use long for Unix timestamp
        public decimal SumInsured { get; set; }
        public int AgentCode { get; set; }
        public int DebtorCode { get; set; }
        public long PolicyStartDate { get; set; } // Use long for Unix timestamp
        public long PolicyEndDate { get; set; } // Use long for Unix timestamp
        public string DebitNoteDate { get; set; } // Stored as string from JSON
        public string ReceiptDate { get; set; } // Stored as string from JSON
        public decimal Premium { get; set; }
        public decimal Rcc { get; set; }
        public decimal Tc { get; set; }
        public decimal RoadSafety { get; set; }
        public decimal AdminFee { get; set; }
        public decimal Tax { get; set; }
        public decimal StampDuty { get; set; }
        public decimal Total { get; set; }
        public decimal Outstanding { get; set; }
        public decimal PolicyFee { get; set; }
        public string DebitNoteBranchName { get; set; }
        public string Department { get; set; }
        public string PolicyNo { get; set; }
        public string PolicyType { get; set; }
        public string Status { get; set; }
        public string Name { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string Address4 { get; set; }
        public string SubDepartment { get; set; }
        public string FileServiceBranch { get; set; }
        public string FileServiceBranchName { get; set; }
        public string VehicleNumber1 { get; set; }
        public string VehicleNumber2 { get; set; }
        public string DataEnteredEpf { get; set; }
        public string ClientCode { get; set; }
        public string AgentName { get; set; }
        public string DebtorName { get; set; }
    }
}
