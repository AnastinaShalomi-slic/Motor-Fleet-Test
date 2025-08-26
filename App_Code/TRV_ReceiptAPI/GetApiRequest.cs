using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

/// <summary>
/// Developed by Praveen 31636
/// </summary>
public class GetApiRequest
{
    TrvApiLogger apiLogger = new TrvApiLogger();

    public GetApiRequest()
    {

    }

    public Task<string> GetApiResponse(string endpoint)
    {
        var tcs = new TaskCompletionSource<string>();

        try
        {
            var endpointUri = new Uri(endpoint);
            ApiTokenManager apiTokenManager = new ApiTokenManager();
            apiTokenManager.GetData().ContinueWith(tokenTask =>
            {
                if (tokenTask.IsFaulted)
                {
                    apiLogger.WriteLog("Failed at GetApiRequest (GetData): " + tokenTask.Exception);
                    tcs.SetException(tokenTask.Exception);
                    return;
                }

                string token = tokenTask.Result;
                apiLogger.WriteLog("Succeed at GetApiResponse: apiTokenManager");

                var request = (HttpWebRequest)WebRequest.Create(endpointUri);
                request.Method = "GET";
                request.Headers["Authorization"] = "Bearer " + token;

                request.BeginGetResponse(ar =>
                {
                    try
                    {
                        var response = (HttpWebResponse)request.EndGetResponse(ar);
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            var responseContent = reader.ReadToEnd();
                            tcs.SetResult(responseContent);
                        }
                    }
                    catch (Exception ex)
                    {
                        apiLogger.WriteLog("Failed at GetApiRequest (GetResponse): " + ex);
                        tcs.SetException(ex);
                    }
                }, null);
            });
        }
        catch (Exception ex)
        {
            apiLogger.WriteLog("Failed at GetApiRequest: " + ex);
            tcs.SetException(ex);
        }

        return tcs.Task;
    }
}
