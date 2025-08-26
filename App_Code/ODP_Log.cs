using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
/// <summary>
/// Summary description for ODP_Log
/// </summary>
public class ODP_Log
{
    public bool WriteLog(string strMessage)
    {
        try
        {
            FileStream objFilestream = new FileStream(string.Format("{0}\\{1}", ConfigurationManager.AppSettings["OrclLog"], "orcl"), FileMode.Append, FileAccess.Write);
            StreamWriter objStreamWriter = new StreamWriter((Stream)objFilestream);
            objStreamWriter.WriteLine(strMessage);
            objStreamWriter.Close();
            objFilestream.Close();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
