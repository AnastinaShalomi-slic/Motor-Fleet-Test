using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ODP_MailLog
/// </summary>
public class ODP_MailLog
{
    public bool write_log(string msg, string type)
    {
        try
        {           
            StreamWriter sw = new StreamWriter((Stream)this.GetFStreem(type));
            sw.AutoFlush = true;
            sw.Write(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss tt"));
            sw.Write(" : ");
            sw.WriteLine(msg);
            sw.Close();
            return true;
        }
        catch (Exception ex)
        {
            ODP_LogExcp log_tr = new ODP_LogExcp();
            log_tr.WriteLog(ex.Message);
            return false;
        }
    }

    protected FileStream GetFStreem(string type)
    {
        FileStream objFilestream = null;

        if (type == "ODP") // Bonus Cetificate       
            objFilestream = new FileStream(string.Format("{0}\\{1}", ConfigurationManager.AppSettings["SendMailStatus"], "Log"), FileMode.Append, FileAccess.Write, FileShare.Read);
        return objFilestream;
    }

}