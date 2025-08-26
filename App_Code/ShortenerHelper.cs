using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ShortenerHelper
/// </summary>
public class ShortenerHelper
{
    public ShortenerHelper()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    //replace this with testURL
    //private static readonly string baseUrl = "http://172.24.90.100:8084/FRWebLk/u.aspx?key=";

    //replace this with live URL
    //private static readonly string baseUrl = "http://203.115.11.232:1010/u.aspx?key=";

    //private static readonly string baseUrl = "http://203.115.11.232:1010/u.aspx?key=";

    //private static readonly string baseUrl = "http://203.115.11.232:1010/u.aspx" + ((char)63) + "key=";

     private static readonly string baseUrl = "http://203.115.11.232:1010/r/";

    // Output: http://203.115.11.232:1010/u.aspx?key=


    //for local
    //private static readonly string baseUrl = "http://localhost:53781/u.aspx?key=";

    public static string GenerateShortKey(int length = 6)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var random = new Random();
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string ShortenUrl(string longUrl)
    {
        string shortURL = string.Empty;
        string shortKey = GenerateShortKey();

        using (var con = new OracleConnection(ConfigurationManager.ConnectionStrings["CONN_STRING_ORCL"].ConnectionString))
        {
            con.Open();
            string insert = "INSERT INTO SLIC_CNOTE.shorturls (ShortKey, LongUrl, ExpiryDate) VALUES (:key, :url , :expiryDate)";
            using (var cmd = new OracleCommand(insert, con))
            {
                cmd.Parameters.Add("key", shortKey);
                cmd.Parameters.Add("url", longUrl);
                cmd.Parameters.Add("expiryDate", DateTime.Now.AddDays(30)); // expires in 30 days
                cmd.ExecuteNonQuery();
            }
        }


        shortURL = baseUrl + shortKey;
        return shortURL;
    }

    public static string GetLongUrl(string key)
    {
        using (var con = new OracleConnection(ConfigurationManager.ConnectionStrings["CONN_STRING_ORCL"].ConnectionString))
        {
            con.Open();
            string select = @"
            SELECT LongUrl 
            FROM SLIC_CNOTE.shorturls 
            WHERE ShortKey = :key AND (EXPIRYDATE IS NULL OR EXPIRYDATE >= SYSDATE)";
            using (OracleCommand cmd = new OracleCommand(select, con))
            {
                cmd.Parameters.Add("key", key);

                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    return result.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
    }

}