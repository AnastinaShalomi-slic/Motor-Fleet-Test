using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Developed by Praveen 31636
/// </summary>
public class PostApiRequest
{
    TrvApiLogger apiLogger = new TrvApiLogger();

    public PostApiRequest()
    {

    }

    //    public  Task<string> PostApiResponse(string endpoint, object content)
    //    {
    //        var tcs = new TaskCompletionSource<string>();

    //        try
    //        {
    //            var endpointUri = new Uri(endpoint);
    //            ApiTokenManager apiTokenManager = new ApiTokenManager();
    //            apiTokenManager.GetData().ContinueWith(tokenTask =>
    //            {
    //                if (tokenTask.IsFaulted)
    //                {
    //                    apiLogger.WriteLog("Failed at PostApiRequest (GetData): " + tokenTask.Exception);
    //                    tcs.SetException(tokenTask.Exception);
    //                    return;
    //                }

    //                string token = tokenTask.Result;
    //                apiLogger.WriteLog("Succeed at PostApiResponse: apiTokenManager");

    //                var request = (HttpWebRequest)WebRequest.Create(endpointUri);
    //                request.Method = "POST";
    //                request.ContentType = "application/json";
    //                request.Headers["Authorization"] = "Bearer ";//+ token;

    //                var jsonContent = JsonConvert.SerializeObject(content);
    //                var data = Encoding.UTF8.GetBytes(jsonContent);

    //                request.BeginGetRequestStream(ar =>
    //                {
    //                    try
    //                    {
    //                        using (var stream = request.EndGetRequestStream(ar))
    //                        {
    //                            stream.Write(data, 0, data.Length);
    //                        }

    //                        request.BeginGetResponse(ar2 =>
    //                        {
    //                            try
    //                            {
    //                                var response = (HttpWebResponse)request.EndGetResponse(ar2);
    //                                using (var reader = new StreamReader(response.GetResponseStream()))
    //                                {
    //                                    var responseContent = reader.ReadToEnd();
    //                                    tcs.SetResult(responseContent);
    //                                }
    //                            }
    //                            catch (Exception ex)
    //                            {
    //                                apiLogger.WriteLog("Failed at PostApiRequest (GetResponse): " + ex);
    //                                tcs.SetException(ex);
    //                            }
    //                        }, null);
    //                    }
    //                    catch (Exception ex)
    //                    {
    //                        apiLogger.WriteLog("Failed at PostApiRequest (GetRequestStream): " + ex);
    //                        tcs.SetException(ex);
    //                    }
    //                }, null);
    //            });
    //        }
    //        catch (Exception ex)
    //        {
    //            apiLogger.WriteLog("Failed at PostApiRequest: " + ex);
    //            tcs.SetException(ex);
    //        }

    //        return tcs.Task;
    //    }
    //}

    public Task<string> PostApiResponse(string endpoint, object content)
    {
        var tcs = new TaskCompletionSource<string>();

        try
        {
            var endpointUri = new Uri(endpoint);

            // Create the request
            var request = (HttpWebRequest)WebRequest.Create(endpointUri);
            request.Method = "POST";
            request.ContentType = "application/json";

            // Serialize the content to JSON
            var jsonContent = JsonConvert.SerializeObject(content);
            var data = Encoding.UTF8.GetBytes(jsonContent);

            // Begin the request stream operation
            request.BeginGetRequestStream(ar =>
            {
                try
                {
                    using (var stream = request.EndGetRequestStream(ar))
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    // Begin the response operation
                    request.BeginGetResponse(ar2 =>
                    {
                        try
                        {
                            using (var response = (HttpWebResponse)request.EndGetResponse(ar2))
                            using (var reader = new StreamReader(response.GetResponseStream()))
                            {
                                var responseContent = reader.ReadToEnd();
                                tcs.SetResult(responseContent);
                            }
                        }
                        catch (Exception ex)
                        {
                            apiLogger.WriteLog("Failed at PostApiRequest (GetResponse): " + ex);
                            tcs.SetException(ex);
                        }
                    }, null);
                }
                catch (Exception ex)
                {
                    apiLogger.WriteLog("Failed at PostApiRequest (GetRequestStream): " + ex);
                    tcs.SetException(ex);
                }
            }, null);
        }
        catch (Exception ex)
        {
            apiLogger.WriteLog("Failed at PostApiRequest: " + ex);
            tcs.SetException(ex);
        }

        return tcs.Task;
    }
}
