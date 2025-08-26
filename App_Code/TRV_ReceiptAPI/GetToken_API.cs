using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

/// <summary>
/// Developed by Praveen 31636
/// </summary>
public class GetToken_API
{
    //TrvApiLogger apiLogger = new TrvApiLogger();

    public GetToken_API()
    {

    }

    public Task<GetTokenDataModel> GetToken()
    {
        var tcs = new TaskCompletionSource<GetTokenDataModel>();
        SetCredentials credentials = new SetCredentials
        {
            client_id = "lviLrtoMK+6xMMFz0UsMSA==",
            client_secret = "drORxPKcxmzyc24I1MdSW0XlaCFgKTpID2lNzh2ARuzqamf9AAMzbFp0C7ztKcRS"
        };

        try
        {
            string baseUrl = ConfigurationManager.AppSettings["ReceiptApiBaseUrl"];
            string endpoint = baseUrl + "/api/Token/getToken";

            var request = (HttpWebRequest)WebRequest.Create(endpoint);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";

            var formData = new Dictionary<string, string>
            {
                { "client_id", credentials.client_id },
                { "client_secret", credentials.client_secret }
            };

            var postData = new StringBuilder();
            foreach (var pair in formData)
            {
                postData.AppendFormat("{0}={1}&", WebUtility.UrlEncode(pair.Key), WebUtility.UrlEncode(pair.Value));
            }

            if (postData.Length > 0)
            {
                postData.Length -= 1;
            }

            var data = Encoding.UTF8.GetBytes(postData.ToString());

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
                                var parsedResponse = JsonConvert.DeserializeObject<GetTokenDataModel>(responseText);
                                tcs.SetResult(parsedResponse);
                            }
                        }
                        catch (Exception ex)
                        {
                            //apiLogger.WriteLog("Failed at GetToken_API: " + ex.ToString());
                            tcs.SetException(ex);
                        }
                    }, null);
                }
                catch (Exception ex)
                {
                   // apiLogger.WriteLog("Failed at GetToken_API: " + ex.ToString());
                    tcs.SetException(ex);
                }
            }, null);
        }
        catch (Exception ex)
        {
            //apiLogger.WriteLog("Failed at GetToken_API: " + ex.ToString());
            tcs.SetException(ex);
        }

        return tcs.Task;
    }

    public class SetCredentials
    {
        public string client_id { get; set; }
        public string client_secret { get; set; }
    }

    public class GetTokenDataModel
    {
        public string Access_Token { get; set; }
        public string Token_Type { get; set; }
        public int Expires_In { get; set; }
    }
}
