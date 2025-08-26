using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for CoopWebORA
/// </summary>
public class CoopWebORA
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["DBConStrLife"]);
    public CoopWebORA()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void get_userBank(string user_name, out string rtn_userName, out string rtn_bankCode, out string rtn_branchCode)
   
    {
        rtn_userName = "";
        rtn_bankCode = "";
        rtn_branchCode = "";

        //int bb_code_2 = Convert.ToInt32(bb_code.ToString());

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {
                string sql = "Select USERNAME, BANK_CODE, BRANCH_CODE from COOPWEB.USER_BANK where USERNAME = :txtuserName AND  ACTIVE_FLAG = 'Y'";
                

                OracleParameter para1 = new OracleParameter();
                para1.Value = user_name;
                para1.ParameterName = "txtuserName";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sql;

                string x = cmd.ExecuteScalar().ToString();

                //int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                //cmd.Parameters.Clear();

                if (x != null)
                {

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {

                            if (!reader.IsDBNull(0))
                            {
                                rtn_userName = reader.GetString(0).ToString();
                            }
                            if (!reader.IsDBNull(1))
                            {
                                rtn_bankCode = reader.GetDouble(1).ToString();
                            }
                            if (!reader.IsDBNull(2))
                            {
                                rtn_branchCode = reader.GetDouble(2).ToString();
                            }
                        }

                    }


                }


            }

        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }

        }

    }

    public void get_userBankEmail(string user_name, out string rtn_email)
  
    {
        rtn_email = "";


        //int bb_code_2 = Convert.ToInt32(bb_code.ToString());

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {
                string sql = "Select EMAIL from COOPWEB.WEBUSERS where USERNAME = :txtUserName AND  ACTIVE_FLAG = 'Y'";
             

                OracleParameter para1 = new OracleParameter();
                para1.Value = user_name;
                para1.ParameterName = "txtUserName";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sql;

                string x = cmd.ExecuteScalar().ToString();

                //int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                //cmd.Parameters.Clear();

                if (x != null)
                {

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {

                            if (!reader.IsDBNull(0))
                            {
                                rtn_email = reader.GetString(0).ToString();
                            }

                        }

                    }


                }


            }

        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }

        }

    }

    

}