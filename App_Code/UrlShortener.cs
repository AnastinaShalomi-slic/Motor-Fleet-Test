using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

/// <summary>
/// Summary description for UrlShortener
/// </summary>
public class UrlShortener
{
    public UrlShortener()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    private static Dictionary<string, string> urlMap = new Dictionary<string, string>();
    private static string baseUrl = "http://172.24.90.100:8084/Slicgeneral/FireRenewalSMSNoticeWebLink/SLIC_Fire/u.aspx?key=";
    //private static string baseUrl = "http://localhost:53781/SLIC_Fire/u.aspx?key=";

    public static string Shorten(string longUrl)
    {
        string key = Guid.NewGuid().ToString().Substring(0, 6);
        urlMap[key] = longUrl;
        return baseUrl + key;
    }

    public static string GetLongUrl(string key)
    {
        return urlMap.ContainsKey(key) ? urlMap[key] : null;
    }

    public static string ShortenUrl(string longUrl)
    {
        try
        {
            string apiUrl = "http://tinyurl.com/api-create.php?url=" + Uri.EscapeDataString(longUrl);

            using (var client = new WebClient())
            {
                //string shortUrl = client.DownloadString(apiUrl);
                //return shortUrl;

                client.Proxy = WebRequest.GetSystemWebProxy(); // Add this line
                client.Proxy.Credentials = CredentialCache.DefaultCredentials;

                string shortUrl = client.DownloadString(apiUrl);
                return shortUrl;
            }
        }
        catch (Exception ex)
        {
            // Handle error (optional logging)
            return "Error: " + ex.Message;
        }
    }

    private static Dictionary<string, string> shortToLong = new Dictionary<string, string>();
    private static Dictionary<string, string> longToShort = new Dictionary<string, string>();
    private static Random random = new Random();

    public static string Shorten(string longUrl, string baseUrl)
    {
        if (longToShort.ContainsKey(longUrl))
        {
            return longToShort[longUrl];
        }

        string shortCode;
        do
        {
            shortCode = GenerateCode(6);
        } while (shortToLong.ContainsKey(shortCode));

        string shortUrl = baseUrl.TrimEnd('/') + "/" + shortCode;

        shortToLong[shortCode] = longUrl;
        longToShort[longUrl] = shortUrl;

        return shortUrl;
    }

    public static string Resolve(string shortCode)
    {
        return shortToLong.ContainsKey(shortCode) ? shortToLong[shortCode] : null;
    }

    private static string GenerateCode(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }
}