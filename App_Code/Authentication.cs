using System;
using System.Data.Odbc;
using System.Configuration;
using System.Data.OracleClient;
using System.Data;

/// <summary>
/// Summary description for Authentication
/// </summary>
public class Authentication
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]);

    public Authentication()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region Get Failed Login Count

    public int failedCount(string user_id)
    {
        int failedLoginCount = 0;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string sql = "Select ATTEMPMA from INTRANET.INTUSR where Userid = upper(:txtUserId)";

            using (OracleCommand com = new OracleCommand(sql, oconn))
            {
                com.Parameters.AddWithValue("txtUserId", user_id);

                int count = Convert.ToInt32(com.ExecuteScalar());

                failedLoginCount = (int)count;
            }

        }
        catch(OdbcException ex)
        {
            string errorMsg = ex.ToString();
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return failedLoginCount;
    }


    
  
    #endregion

    #region Update Failed Login Count
    public bool UpdateFailedLogin(string Username)
    {
        bool success = false;
        string setLoginCountZero = "Update  INTRANET.INTUSR set ATTEMPMA = ATTEMPMA+1 , lstvsttm=sysdate where UPPER(Userid) = :username";

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        OracleCommand cmd = oconn.CreateCommand();

        cmd.CommandText = setLoginCountZero;

        OracleParameter oUserNam = new OracleParameter();
        oUserNam.DbType = DbType.String;
        oUserNam.Value = Username.ToUpper();
        oUserNam.ParameterName = "username";

        cmd.Parameters.Add(oUserNam);

        int count2 = cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();

        success = true;
        return success;
    }

    #endregion
    #region Reset Failed Login Count
    public bool reSetFailedLogin(string Username)
    {
        bool success = false;
        string setLoginCountZero = "Update  INTRANET.INTUSR set ATTEMPMA = 0 , lstvsttm=sysdate where UPPER(Userid) = :username";

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        OracleCommand cmd = oconn.CreateCommand();

        cmd.CommandText = setLoginCountZero;

        OracleParameter oUserNam = new OracleParameter();
        oUserNam.DbType = DbType.String;
        oUserNam.Value = Username.ToUpper();
        oUserNam.ParameterName = "username";

        cmd.Parameters.Add(oUserNam);

        int count2 = cmd.ExecuteNonQuery();
        cmd.Parameters.Clear();

        success = true;
        return success;
    }

    #endregion
}