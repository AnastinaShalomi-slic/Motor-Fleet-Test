using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OracleClient;
using System.Diagnostics;

/// <summary>
/// Summary description for ORCL_Connection
/// </summary>
public class ORCL_Connection
{
    public OracleConnection GetConnection()
    {
        var conString = ConfigurationManager.AppSettings["CONN_STRING_ORCL"];
        string strConnString = conString;
        return new OracleConnection(strConnString);
    }

    public OracleConnection GetConnection_LIFE()
    {
        var conString = ConfigurationManager.AppSettings["CONN_STRING_ORCL_LIFE"];
        string strConnString = conString;
        return new OracleConnection(strConnString);
    }
}