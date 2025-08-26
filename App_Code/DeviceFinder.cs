using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.NetworkInformation;
using System.Management;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;


/// <summary>
/// Summary description for DeviceFinder
/// </summary>
public class DeviceFinder
{
	public DeviceFinder()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public string GetDeviceIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];

        //return "ip";
    }
    public string GetMACAddress()
    {
        NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
        String sMacAddress = string.Empty;
        foreach (NetworkInterface adapter in nics)
        {
            if (sMacAddress == String.Empty)// only return MAC Address from first card  
            {
                IPInterfaceProperties properties = adapter.GetIPProperties();
                sMacAddress = adapter.GetPhysicalAddress().ToString();
            }
        } return sMacAddress;
    }

    public string getDeviceOS()
    {
        try
        {

        
        string os = null;
        if (System.Web.HttpContext.Current.Request.UserAgent.IndexOf("Windows NT 5.1") > 0)
        {
            os = "Windows XP";
            return os;
        }
        else if (System.Web.HttpContext.Current.Request.UserAgent.IndexOf("Windows NT 6.0") > 0)
        {
            os = "Windows Vista";
            return os;
        }
        else if (System.Web.HttpContext.Current.Request.UserAgent.IndexOf("Windows NT 6.1") > 0)
        {
            os = "Windows 7";
            return os;
        }

        else if (System.Web.HttpContext.Current.Request.UserAgent.IndexOf("Windows NT 6.2") > 0)
        {
            os = "Windows 8";
            return os;
        }
        else if (System.Web.HttpContext.Current.Request.UserAgent.IndexOf("Windows NT 6.3") > 0)
        {
            os = "Windows 8.1";
            return os;
        }
        else if (System.Web.HttpContext.Current.Request.UserAgent.IndexOf("Windows NT 10") > 0)
        {
            os = "Windows 10";
            return os;
        }
        else if (System.Web.HttpContext.Current.Request.UserAgent.IndexOf("Intel Mac OS X") > 0)
        {

            os = "Intel Mac OS X";
            return os;
        }
        else if (System.Web.HttpContext.Current.Request.UserAgent.IndexOf("Linux") > 0 || System.Web.HttpContext.Current.Request.UserAgent.IndexOf("KFAPWI") > 0)
        {

            os = "Kindle Fire / Android";
            return os;
        }
        else if (System.Web.HttpContext.Current.Request.UserAgent.ToString().Substring(13, 6) == "iPhone")
        {

            os = "iPhone" + System.Web.HttpContext.Current.Request.UserAgent.ToString().Substring(31,18);
            return os;
        }
        else if (System.Web.HttpContext.Current.Request.UserAgent.ToString().Substring(13, 4) == "iPad")
        {

            os = "iPad " + System.Web.HttpContext.Current.Request.UserAgent.ToString().Substring(23, 17);
            return os;
        }
        else
        {
           
            os = System.Web.HttpContext.Current.Request.UserAgent.ToString();
            return os;
        }
        }
        catch (Exception ex)
        {
            return ex.ToString();
        }
    }

    public string GetDeviceName()
    {
        try
        {
            string disMgs = "";
            string[] computer_name = System.Net.Dns.GetHostEntry(System.Web.HttpContext.Current.Request.ServerVariables["remote_addr"]).HostName.Split(new Char[] { '.' });
            String ecn = System.Environment.MachineName;
            disMgs = computer_name[0].ToString();

            return disMgs;
        }
        catch(Exception ex)
        {
            return "Undefined";
            //return ex.ToString();

           
        }
            
    }

    public string GetBrowserType()
    {
        HttpRequest req = System.Web.HttpContext.Current.Request;
        string Curent_browser = req.Browser.Browser;

        return Curent_browser;
    }
}