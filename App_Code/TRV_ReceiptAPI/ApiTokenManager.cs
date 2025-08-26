using System;
using System.Threading.Tasks;

/// <summary>
/// Developed by Praveen 31636
/// </summary>
/// 

public class ApiTokenManager
{
    TrvApiLogger TrvApiLogger = new TrvApiLogger();
    private static string accessToken;
    private static DateTime tokenExpiration;

    public ApiTokenManager()
    {

    }

    public void SetData(string data, int cacheDurationInSeconds)
    {
        try
        {
            accessToken = data;
            tokenExpiration = DateTime.UtcNow.AddSeconds(cacheDurationInSeconds);
        }
        catch (Exception ex)
        {
            TrvApiLogger.WriteLog("Failed at ApiTokenManager SetData: " + ex.ToString());
        }
    }

    private static bool IsTokenExpired()
    {
        return DateTime.UtcNow >= tokenExpiration;
    }

    public async Task<string> GetData()
    {
        try
        {
            if (IsTokenExpired())
            {
                GetToken_API getToken_API = new GetToken_API();
                var token = await getToken_API.GetToken();

                return token.Access_Token;
            }

            else
            {
                return accessToken;
            }

        }
        catch (Exception ex)
        {
            TrvApiLogger.WriteLog("Failed at ApiCached GetData: " + ex.ToString());
            return null;
        }
    }
}