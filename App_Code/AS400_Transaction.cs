using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data.Odbc;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for AS400_Transaction
/// </summary>
public class AS400_Transaction
{
    OdbcConnection oconn = new OdbcConnection(ConfigurationManager.AppSettings["IBM_DB2"]);
    int kk = 0;

	public AS400_Transaction()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool as400_user_exist(string user_id)
    {
        bool result = false;
        try
        {
            oconn.Open();

            string sql = "Select COUNT(*) from INTRANET.INTUSR where Userid = ?  AND  USRTYP  NOT IN ('A','O')";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.AddWithValue("", user_id);
                int kk = Convert.ToInt32(com.ExecuteScalar());

                if (kk > 0)
                {
                    result = true;
                }
            }

        }
        catch (OdbcException ee)
        {
            string errorMsg = ee.ToString();
        }
        finally
        {
            oconn.Close();
        }
        return result;
    }
    
    public void as400_get_usrInfo(string user_id, out string epf_numbr, out string user_name, out string user_brCode)
    {
        epf_numbr = "";
        user_brCode = "";
        user_name = "";
        try
        {
            oconn.Open();
            string sql = "Select Epfnum, usrname, brnach from INTRANET.INTUSR where Userid = ?";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.Add("@uid", OdbcType.Char);
                com.Parameters["@uid"].Value = user_id;

                OdbcDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    epf_numbr = reader[0].ToString();
                    user_name = reader[1].ToString();
                    user_brCode = reader[2].ToString();
                }
            }
        }
        catch
        {

        }
        finally
        {
            oconn.Close();
        }
    }

    public string Get_As400_Branch(string user_id)
    {
        string user_brCode = string.Empty;
        try
        {
            oconn.Open();
            string sql = "brnach from INTRANET.INTUSR where Userid = ?";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.Add("@uid", OdbcType.Char);
                com.Parameters["@uid"].Value = user_id;

                OdbcDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    user_brCode = reader[2].ToString();
                }
            }
        }
        catch (OdbcException ee)
        {
            string errorMsg = ee.ToString();
        }
        finally
        {
            oconn.Close();
        }

        return user_brCode;
    }

    public void as400_get_usrInfo(string user_id, out string user_name)
    {
        user_name = "";
        try
        {
            oconn.Open();
            string sql = "Select usrname from INTRANET.INTUSR where Userid = ?";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.Add("@uid", OdbcType.Char);
                com.Parameters["@uid"].Value = user_id;

                OdbcDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    user_name = reader[0].ToString();
                }
            }
        }
        catch
        {

        }
        finally
        {
            oconn.Close();
        }
    }


    public bool as400_login(string user_id, string passwrd)
    {
        bool result = false;
        string passwd = fix_f_password(passwrd);
        try
        {
            oconn.Open();

            string sql = "Select COUNT(*) from INTRANET.INTUSR where Userid = ? AND MAINPASS = ? AND  USRTYP  NOT IN ('A','O')";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.Add("@uid", OdbcType.Char);
                com.Parameters.Add("@ps", OdbcType.Char);

                com.Parameters["@uid"].Value = user_id;
                com.Parameters["@ps"].Value = passwd;

                int kk = Convert.ToInt32(com.ExecuteScalar());

                if (kk > 0)
                {
                    result = true;
                }
            }

        }
        catch (OdbcException ee)
        {
            string errorMsg = ee.ToString();
        }
        finally
        {
            oconn.Close();
        }
        return result;
    }


    private string fix_f_password(string passwd)
    {
        string result = passwd;

        char[] arr = new char[12];
        int len = passwd.Length;

        for (int i = 0; i < arr.Length; i++)
        {
            if (i < passwd.Length)
            {
                arr[i] = passwd[i];

            }
            else
            {
                arr[i] = ' ';
            }
        }

        result = arr[10].ToString().Trim() + arr[11].ToString().Trim() + arr[6].ToString().Trim() + arr[7].ToString().Trim() + arr[2].ToString().Trim() + arr[3].ToString().Trim() + arr[8].ToString().Trim() + arr[9].ToString().Trim() + arr[4].ToString().Trim() + arr[5].ToString().Trim() + arr[0].ToString().Trim() + arr[1].ToString().Trim();

        return result;
    }


    public bool as400_authorise(string user_id, string sys_code, string fun_code)
    {

        #region AS400 connection
        bool ok = false;

        try
        {
            oconn.Open();
            string sql = "Select COUNT(*) from INTRANET.USRAUTFUN where Userid = '" + user_id + "' AND Syscod = '" + sys_code + "' AND Funcod = '" + fun_code + "'";
          
            //using (OdbcCommand com = new OdbcCommand(sql, oconn))
            //{
           
               OdbcCommand com = new OdbcCommand(sql, oconn);
                //com.Parameters.AddWithValue("", user_id);
                //com.Parameters.AddWithValue("", sys_code);
                //com.Parameters.AddWithValue("", fun_code); 
                kk = Convert.ToInt32(com.ExecuteScalar());



                if (kk > 0)
                {
                    ok = true;
                }
            //}

        }
        catch (OdbcException ee)
        {
            string errorMsg = ee.ToString();
        }
        finally
        {
            oconn.Close();
        }
        return ok;
        #endregion
    }



    public void as400_get_userBank(string user_name, out string rtn_userName, out string rtn_bankCode, out string rtn_branchCode)
    {
        rtn_userName = "";
        rtn_bankCode = "";
        rtn_branchCode = "";
        try
        {
  
            //ACTIVE_FLAG

            oconn.Open();
            string sql = "Select USERNAME, BANK_CODE, BRANCH_CODE from COOPWEB.USER_BANK where USERNAME = ? AND  ACTIVE_FLAG = 'Y'";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.Add("@uid", OdbcType.Char);
                com.Parameters["@uid"].Value = user_name;

                OdbcDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    rtn_userName = reader[0].ToString();
                    rtn_bankCode = reader[1].ToString();
                    rtn_branchCode = reader[2].ToString();
                }
            }
        }
        catch
        {

        }
        finally
        {
            oconn.Close();
        }
    }


    public void as400_get_userBankEmail(string user_name, out string rtn_email)
    {
        rtn_email = "";
        
        try
        {

            //ACTIVE_FLAG

            oconn.Open();
            string sql = "Select EMAIL from COOPWEB.WEBUSERS where USERNAME = ? AND  ACTIVE_FLAG = 'Y'";
            using (OdbcCommand com = new OdbcCommand(sql, oconn))
            {
                com.Parameters.Add("@uid", OdbcType.Char);
                com.Parameters["@uid"].Value = user_name;

                OdbcDataReader reader = com.ExecuteReader();

                while (reader.Read())
                {
                    rtn_email = reader[0].ToString();
                    
                }
            }
        }
        catch
        {

        }
        finally
        {
            oconn.Close();
        }
    }
}