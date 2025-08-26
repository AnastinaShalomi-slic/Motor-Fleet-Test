using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data.Odbc;
using System.Configuration;
using System.Data;

/// <summary>
/// Summary description for ExcuteOraSide
/// </summary>
public class ExcuteOraSide
{
    Execute_sql Exsql = new Execute_sql();
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]);

    public ExcuteOraSide()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public void get_userBankBranch(string bb_code, out string rtn_bank, out string rtn_branch)
    {
        rtn_bank = "";
        rtn_branch = "";

        int bb_code_2 = Convert.ToInt32(bb_code.ToString());

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                string sql = "select bbnam, bbrnch from GENPAY.BNKBRN  where bbcode=:txtbrcode";

                OracleParameter para1 = new OracleParameter();
                para1.Value = bb_code_2;
                para1.ParameterName = "txtbrcode";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sql;

                string x = cmd.ExecuteScalar().ToString();

                //int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                //cmd.Parameters.Clear();

                if (x !=null)
                {                  

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            
                            if (!reader.IsDBNull(0))
                            {
                                rtn_bank = reader.GetString(0).ToString();
                            }
                            if (!reader.IsDBNull(1))
                            {
                                rtn_branch = reader.GetString(1).ToString();
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
    //check accsess for quotation and fire

    //fire renewal sms project
    public void ora_get_usrInfo(string user_id, out string epf_numbr, out string user_name, out string user_brCode, out string user_type)
    {


        epf_numbr = "";
        user_name = "";
        user_brCode = "";
        user_type = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {


                string sql = "Select Epfnum, usrname, brnach, usrtyp from INTRANET.INTUSR where Userid =:txtuser_id";
                OracleParameter para1 = new OracleParameter();
                para1.Value = user_id;
                para1.ParameterName = "txtuser_id";
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
                                epf_numbr = reader.GetString(0).ToString();
                            }
                            if (!reader.IsDBNull(1))
                            {
                                user_name = reader.GetString(1).ToString();
                            }
                            if (!reader.IsDBNull(2))
                            {
                                user_brCode = reader.GetString(2).ToString();
                            }
                            if (!reader.IsDBNull(3))
                            {
                                user_type = reader.GetString(3).ToString();
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

    public bool get_authorise(string user_id, string sys_code, string fun_code)
    {

        bool ok = false;


        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {
                string sql = "Select COUNT(*) from INTRANET.USRAUTFUN where Userid = :txtuser_id AND Syscod = :txtsys_code AND Funcod = :txtfun_code";


                OracleParameter para1 = new OracleParameter();
                OracleParameter para2 = new OracleParameter();
                OracleParameter para3 = new OracleParameter();
                para1.Value = user_id;
                para1.ParameterName = "txtuser_id";
                cmd.Parameters.Add(para1);

                para2.Value = sys_code;
                para2.ParameterName = "txtsys_code";
                cmd.Parameters.Add(para2);

                para3.Value = fun_code;
                para3.ParameterName = "txtfun_code";
                cmd.Parameters.Add(para3);


                cmd.CommandText = sql;

                string x = cmd.ExecuteScalar().ToString();

                //int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                //cmd.Parameters.Clear();

                if (x == "")
                {
                    ok = false;
                }
                else if (x == "1")
                {
                    ok = true;
                }
                else { ok = false; }
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
        return ok;
    }

    public void get_BranchName(string br_code, out string rtn_br)
    {
        rtn_br = "";
        string rtn_br_code = "";

        int bb_code_2 = Convert.ToInt32(br_code.ToString());

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {
                string sql = Exsql.GetBranch(bb_code_2);
                //string sql = "select distinct(BANK_NAME) from QUOTATION.BANK_SALES_OFFICER where BANK_CODE = :txtbrcode";

                //OracleParameter para1 = new OracleParameter();
                //para1.Value = bb_code_2;
                //para1.ParameterName = "txtbrcode";
                //cmd.Parameters.Add(para1);


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
                                rtn_br_code = reader.GetInt32(0).ToString();
                            }

                            if (!reader.IsDBNull(1))
                            {
                                rtn_br = reader.GetString(1).ToString();
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

    //end 
    public void get_BankName(string bb_code, out string rtn_bank)
    {
        rtn_bank = "";
      

        int bb_code_2 = Convert.ToInt32(bb_code.ToString());

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {
                
                string sql = "select distinct(BANK_NAME) from QUOTATION.BANK_SALES_OFFICER where BANK_CODE = :txtbrcode";

                OracleParameter para1 = new OracleParameter();
                para1.Value = bb_code_2;
                para1.ParameterName = "txtbrcode";
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
                                rtn_bank = reader.GetString(0).ToString();
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

    public void get_fireValidation(string bb_code, out int Resultcount)
    {
        Resultcount = 0;


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
                string sql = "select count(*) from QUOTATION.FIRE_AGENT_INFO where bank_code = :txtbrcode and active = 'Y'";


                OracleParameter para1 = new OracleParameter();
                para1.Value = bb_code;
                para1.ParameterName = "txtbrcode";
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
                                Resultcount = reader.GetInt32(0);
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



    //20/06/2022--->>>>>>>>>>>>>>>>>>>>
    public bool authorise(string user_id, string sys_code, string fun_code)
    {

        bool returnValue = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {
                string sql = "Select COUNT(*) from INTRANET.USRAUTFUN where Userid = :txtUserId AND Syscod = :txtSysCode AND Funcod = :txtFunCode";
                

                OracleParameter para1 = new OracleParameter();
                para1.Value = user_id;
                para1.ParameterName = "txtUserId";
                cmd.Parameters.Add(para1);

                OracleParameter para2 = new OracleParameter();
                para2.Value = sys_code;
                para2.ParameterName = "txtSysCode";
                cmd.Parameters.Add(para2);

                OracleParameter para3 = new OracleParameter();
                para3.Value = fun_code;
                para3.ParameterName = "txtFunCode";
                cmd.Parameters.Add(para3);

                cmd.CommandText = sql;

                //string x = cmd.ExecuteScalar().ToString();
                int x = Convert.ToInt32(cmd.ExecuteScalar().ToString());
                //int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                //cmd.Parameters.Clear();

                if (x != 0)
                {

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            returnValue = true;

                        }

                    }


                }


            }

        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
            returnValue = false;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }

        }
        return returnValue;
    }

    public void get_usrInfo(string user_id, out string epf_numbr, out string user_name, out string user_brCode)
    {
        epf_numbr = "";
        user_name = "";
        user_brCode = "";

 
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {
                string sql = "Select Epfnum, usrname, brnach from INTRANET.INTUSR where Userid = :txtUserId";
               

                OracleParameter para1 = new OracleParameter();
                para1.Value = user_id;
                para1.ParameterName = "txtUserId";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sql;

                string x = cmd.ExecuteScalar().ToString();

                if (x != null)
                {

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {

                            if (!reader.IsDBNull(0))
                            {
                                epf_numbr = reader.GetString(0).ToString();
                            }
                            if (!reader.IsDBNull(1))
                            {
                                user_name = reader.GetString(1).ToString();
                            }
                            if (!reader.IsDBNull(2))
                            {
                                user_brCode = reader.GetString(2).ToString();
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

    public void BPF_Allow_Check(string bankCode, out int rtn_count)
    {
        rtn_count = 0;



        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {
                string sql = Exsql.GetBPF_Allow(bankCode);



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
                                rtn_count = reader.GetInt32(0);
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

    public void GetPayFileDetails(string polno, out string startDate, out string endDate, out string add1, out string add2, out string add3, out string add4)
    {
        endDate = "";
        startDate = "";
        add1 = "";
        add2 = "";
        add3 = "";
        add4 = "";

        LogFile Err = new LogFile();
        //string logPath = HttpContext.Current.Server.MapPath("~D:\WebLogs/Error.txt");

        string logPath = @"D:\WebLogs\FireRenewalErrorlg.txt";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();

            using (cmd)
            {
                string sql = @"SELECT pmad1, pmad2, pmad3, pmdst, pmdex, pmad4 
                           FROM (
                               SELECT * 
                               FROM genpay.payfle @live
                               WHERE pmpol = :polno 
                               ORDER BY pmdst DESC
                           ) 
                           WHERE ROWNUM = 1";

                cmd.CommandText = sql;
                cmd.Parameters.Add(":polno", OracleType.VarChar).Value = polno;

                OracleDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows && reader.Read())
                {
                    if (!reader.IsDBNull(0)) add1 = reader.GetString(0);
                    if (!reader.IsDBNull(1)) add2 = reader.GetString(1);
                    if (!reader.IsDBNull(2)) add3 = reader.GetString(2);
                    if (!reader.IsDBNull(4)) endDate = Convert.ToDateTime(reader.GetValue(4)).ToString("yyyy-MM-dd");
                    if (!reader.IsDBNull(3)) startDate = Convert.ToDateTime(reader.GetValue(3)).ToString("yyyy-MM-dd");
                    if (!reader.IsDBNull(5)) add4 = reader.GetString(5);

                    string errorDetails =
                    " " + Environment.NewLine + "Pay file details: " + Environment.NewLine +
                    "add1 " + reader.GetString(0) + Environment.NewLine +
                    "add2: " + reader.GetString(1) + Environment.NewLine +
                    "add3: " + reader.GetString(2) + Environment.NewLine +
                    "end date: " + reader.GetString(4) + Environment.NewLine +
                    "start date: " + reader.GetString(3) + Environment.NewLine +
                    "add 4: " + reader.GetString(5) + Environment.NewLine;

                    Err.ErrorLog(logPath, errorDetails);
                }
            }
        }
        catch (Exception ex)
        {
            Err.ErrorLog(logPath, ex.Message);
            string exe = ex.ToString(); // Consider logging this
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