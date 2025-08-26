using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for ODP_Transaction
/// </summary>
public class ODP_Transaction
{
    bool Sucess_State;
    int Error_number;
    string _Error_Message;
    string _KeyOut;

    /// <param name="Sucess_State"></param>
    /// Return true if Query Execuit Else false
    public bool Trans_Sucess_State
    {
        get { return this.Sucess_State; }
        set { this.Sucess_State = value; }
    }

    /// <param name="Error_Code"></param>
    /// Return Orale Error Code
    public int Error_Code
    {
        get { return this.Error_number; }
        set { this.Error_number = value; }
    }

    /// <param name="Error_Code"></param>
    /// Return Orale Error_Message
    public string Error_Message
    {
        get { return this._Error_Message; }
        set { this._Error_Message = value; }
    }

    public string KeyOut
    {
        get { return this._KeyOut; }
        set { this._KeyOut = value; }
    }

    public DataTable GetRows(string query_statement, DataTable Result, string red_target)
    {
        ORCL_Connection orcl_con = new ORCL_Connection();
        Result.Clear();
        using (OracleConnection connection = orcl_con.GetConnection())
        {
            try
            {
                connection.Open();
                OracleDataAdapter oraAdapt = new OracleDataAdapter(query_statement, connection);
                oraAdapt.Fill(Result);
                Trans_Sucess_State = true;

                oraAdapt.Dispose();
            }

            catch (OracleException ex)
            {
                Trans_Sucess_State = false;
                Error_Code = ex.ErrorCode;
                Error_Message = ex.Message;

                ODP_Log orclLog = new ODP_Log();
                orclLog.WriteLog("Error [ORCL]:: Reading Data < "+ red_target + " >  [ER_ : " + ex.ErrorCode + " :: " + ex.ToString() + Environment.NewLine);
            }

            finally
            {
                // always call Close when done reading.
                connection.Close();
                connection.Dispose();
            }
        }
        return Result;
    }

    public string AddProposal(ODPEntry oDPEntry)
    {
        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();

        string out_string = string.Empty;
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "QUOTATION.ODP_PROPOSAL";

            cmd.Parameters.Add("PIN_TITLE", OracleType.VarChar).Value = oDPEntry.PIN_TITLE;
            cmd.Parameters.Add("PIN_CUS_NAME", OracleType.VarChar).Value = oDPEntry.PIN_CUS_NAME;
            cmd.Parameters.Add("PIN_ADD_L1", OracleType.VarChar).Value = oDPEntry.PIN_ADD_L1;
            cmd.Parameters.Add("PIN_ADD_L2", OracleType.VarChar).Value = oDPEntry.PIN_ADD_L2;
            cmd.Parameters.Add("PIN_ADD_L3", OracleType.VarChar).Value = oDPEntry.PIN_ADD_L3;
            cmd.Parameters.Add("PIN_ADD_L4", OracleType.VarChar).Value = oDPEntry.PIN_ADD_L4;
            cmd.Parameters.Add("PIN_DOB", OracleType.DateTime).Value = oDPEntry.PIN_DOB;
            cmd.Parameters.Add("PIN_NIC", OracleType.VarChar).Value = oDPEntry.PIN_NIC;
            cmd.Parameters.Add("PIN_CONNOMOB", OracleType.Number).Value = oDPEntry.PIN_CONNOMOB;
            cmd.Parameters.Add("PIN_E_MAIL", OracleType.VarChar).Value = oDPEntry.PIN_E_MAIL;
            cmd.Parameters.Add("PIN_BTUPE", OracleType.VarChar).Value = oDPEntry.PIN_BTUPE;
            cmd.Parameters.Add("PIN_SERBR", OracleType.Number).Value = oDPEntry.PIN_SERBR;
            cmd.Parameters.Add("PIN_BANK_CODE", OracleType.Number).Value = oDPEntry.PIN_BANK_CODE;
            cmd.Parameters.Add("PIN_BRANCH_CODE", OracleType.Number).Value = oDPEntry.PIN_BRANCH_CODE;
            cmd.Parameters.Add("PIN_ENTDBY", OracleType.VarChar).Value = oDPEntry.PIN_ENTDBY;
            cmd.Parameters.Add("PIN_MECODE", OracleType.Number).Value = oDPEntry.PIN_MECODE;
            cmd.Parameters.Add("PIN_SUMINSURD", OracleType.Double).Value = oDPEntry.PIN_SUMINSURD;
            cmd.Parameters.Add("PIN_NETPREMIUM", OracleType.Double).Value = oDPEntry.PIN_NETPREMIUM;
            cmd.Parameters.Add("PIN_SRCC", OracleType.Double).Value = oDPEntry.PIN_SRCC;
            cmd.Parameters.Add("PIN_TC", OracleType.Double).Value = oDPEntry.PIN_TC;
            cmd.Parameters.Add("PIN_ADMIN_FEE", OracleType.Double).Value = oDPEntry.PIN_ADMIN_FEE;
            cmd.Parameters.Add("PIN_POLICY_FEE", OracleType.Double).Value = oDPEntry.PIN_POLICY_FEE;
            cmd.Parameters.Add("PIN_VAT", OracleType.Double).Value = oDPEntry.PIN_VAT;
            cmd.Parameters.Add("PIN_TOT_PREMIUM", OracleType.Double).Value = oDPEntry.PIN_TOT_PREMIUM;

            //RETURN RESULT OF PROCEDURE CALL
            cmd.Parameters.Add("POUTSRID", OracleType.VarChar, 41);
            cmd.Parameters["POUTSRID"].Direction = ParameterDirection.Output;

            cmd.Transaction = transaction;
            int execution = cmd.ExecuteNonQuery();

            if (execution > 0)
                out_string = cmd.Parameters["POUTSRID"].Value.ToString();

            else
                out_string = "#";

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            out_string = "#";
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();

            ODP_Log orclLog = new ODP_Log();
            orclLog.WriteLog("Error [ORCL]:: Proposal Entry [ER_ : " + ex.ErrorCode + " :: " + ex.ToString() + Environment.NewLine);
            
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return out_string;
    }


    public List<ODP_PropsalUpdate> GetUserProfile(string orcl_executor)
    {
        ORCL_Connection orcl_con = new ORCL_Connection();
        List<ODP_PropsalUpdate> us_info = new List<ODP_PropsalUpdate>();

        using (OracleConnection connection = orcl_con.GetConnection())
        {
            using (OracleCommand cmd = new OracleCommand(orcl_executor, connection))
            {
                try
                {
                    connection.Open();

                    using (OracleDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr.HasRows)
                            {
                                us_info.Add(new ODP_PropsalUpdate());
                                us_info[us_info.Count - 1].out_prno = sdr["PRNO"].ToString();
                                us_info[us_info.Count - 1].out_srid = sdr["SRID"].ToString();
                                us_info[us_info.Count - 1].out_title = sdr["TITLE"].ToString();
                                us_info[us_info.Count - 1].out_cus_name = sdr["CUS_NAME"].ToString();
                                us_info[us_info.Count - 1].out_add_l1 = sdr["ADD_L1"].ToString();
                                us_info[us_info.Count - 1].out_add_l2 = sdr["ADD_L2"].ToString();
                                us_info[us_info.Count - 1].out_add_l3 = sdr["ADD_L3"].ToString();
                                us_info[us_info.Count - 1].out_add_l4 = sdr["ADD_L4"].ToString();
                                us_info[us_info.Count - 1].out_dob = sdr["DOB"].ToString();
                                us_info[us_info.Count - 1].out_nic = sdr["NIC"].ToString();
                                us_info[us_info.Count - 1].out_connomob = sdr["CONNOMOB"].ToString();
                                us_info[us_info.Count - 1].out_e_mail = sdr["E_MAIL"].ToString();
                                us_info[us_info.Count - 1].out_btupe = sdr["BTUPE"].ToString();
                                us_info[us_info.Count - 1].out_suminsurd = double.Parse(sdr["SUMINSURD"].ToString());
                                us_info[us_info.Count - 1].out_netpremium = double.Parse(sdr["NETPREMIUM"].ToString());
                                us_info[us_info.Count - 1].out_srcc = double.Parse(sdr["SRCC"].ToString());
                                us_info[us_info.Count - 1].out_tc = double.Parse(sdr["TC"].ToString());
                                us_info[us_info.Count - 1].out_admin_fee = double.Parse(sdr["ADMIN_FEE"].ToString());
                                us_info[us_info.Count - 1].out_policy_fee = double.Parse(sdr["POLICY_FEE"].ToString());
                                us_info[us_info.Count - 1].out_vat = double.Parse(sdr["VAT"].ToString());
                                us_info[us_info.Count - 1].out_tot_premium = double.Parse(sdr["TOT_PREMIUM"].ToString());

                                break;
                            }
                        }
                        Trans_Sucess_State = true;
                        sdr.Dispose();
                    }
                }
                catch (OracleException ex)
                {                   
                    Trans_Sucess_State = false;
                    Error_Code = ex.ErrorCode;
                    Error_Message = ex.Message;

                    ODP_Log orclLog = new ODP_Log();
                    orclLog.WriteLog("Error [ORCL]:: Read Proposal [ER_ : " + ex.ErrorCode + " :: " + ex.ToString() + Environment.NewLine);
                }
                finally
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
            return us_info;
        }
    }


    public bool UpdateProposal(ODPEntry oDPEntry)
    {
        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();

        bool result = false;
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "QUOTATION.ODP_UPDATE_PROPOSAL";

            cmd.Parameters.Add("PIN_PRNO", OracleType.VarChar).Value = oDPEntry.PIN_PRNO;
            cmd.Parameters.Add("PIN_SRID", OracleType.VarChar).Value = oDPEntry.PIN_SRID;
            cmd.Parameters.Add("PIN_TITLE", OracleType.VarChar).Value = oDPEntry.PIN_TITLE;
            cmd.Parameters.Add("PIN_CUS_NAME", OracleType.VarChar).Value = oDPEntry.PIN_CUS_NAME;
            cmd.Parameters.Add("PIN_ADD_L1", OracleType.VarChar).Value = oDPEntry.PIN_ADD_L1;
            cmd.Parameters.Add("PIN_ADD_L2", OracleType.VarChar).Value = oDPEntry.PIN_ADD_L2;
            cmd.Parameters.Add("PIN_ADD_L3", OracleType.VarChar).Value = oDPEntry.PIN_ADD_L3;
            cmd.Parameters.Add("PIN_ADD_L4", OracleType.VarChar).Value = oDPEntry.PIN_ADD_L4;
            cmd.Parameters.Add("PIN_DOB", OracleType.DateTime).Value = oDPEntry.PIN_DOB;
            cmd.Parameters.Add("PIN_NIC", OracleType.VarChar).Value = oDPEntry.PIN_NIC;
            cmd.Parameters.Add("PIN_CONNOMOB", OracleType.Number).Value = oDPEntry.PIN_CONNOMOB;
            cmd.Parameters.Add("PIN_E_MAIL", OracleType.VarChar).Value = oDPEntry.PIN_E_MAIL;
            cmd.Parameters.Add("PIN_BTUPE", OracleType.VarChar).Value = oDPEntry.PIN_BTUPE;
            cmd.Parameters.Add("PIN_SERBR", OracleType.Number).Value = oDPEntry.PIN_SERBR;
            cmd.Parameters.Add("PIN_ENTDBY", OracleType.VarChar).Value = oDPEntry.PIN_ENTDBY;
            cmd.Parameters.Add("PIN_MECODE", OracleType.Number).Value = oDPEntry.PIN_MECODE;
            cmd.Parameters.Add("PIN_SUMINSURD", OracleType.Double).Value = oDPEntry.PIN_SUMINSURD;
            cmd.Parameters.Add("PIN_NETPREMIUM", OracleType.Double).Value = oDPEntry.PIN_NETPREMIUM;
            cmd.Parameters.Add("PIN_SRCC", OracleType.Double).Value = oDPEntry.PIN_SRCC;
            cmd.Parameters.Add("PIN_TC", OracleType.Double).Value = oDPEntry.PIN_TC;
            cmd.Parameters.Add("PIN_ADMIN_FEE", OracleType.Double).Value = oDPEntry.PIN_ADMIN_FEE;
            cmd.Parameters.Add("PIN_POLICY_FEE", OracleType.Double).Value = oDPEntry.PIN_POLICY_FEE;
            cmd.Parameters.Add("PIN_VAT", OracleType.Double).Value = oDPEntry.PIN_VAT;
            cmd.Parameters.Add("PIN_TOT_PREMIUM", OracleType.Double).Value = oDPEntry.PIN_TOT_PREMIUM;

            cmd.Transaction = transaction;
            int execution = cmd.ExecuteNonQuery();

            if (execution > 0)
                result = true;

            else
                result = false;

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();

            ODP_Log orclLog = new ODP_Log();
            orclLog.WriteLog("Error [ORCL]:: Update Proposal [ER_ : " + ex.ErrorCode + " :: " + ex.ToString() + Environment.NewLine);

        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }


    public bool Proposalto_Policy(ODP_ProtoPolicy oDPEntry)
    {
        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();

        bool result = false;
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "QUOTATION.ODP_PROPOSAL_TO_POLICY";

            cmd.Parameters.Add("PIN_PRNO", OracleType.VarChar).Value = oDPEntry.PIN_PRNO;
            cmd.Parameters.Add("PIN_PAYRECIPT_NO", OracleType.VarChar).Value = oDPEntry.PIN_PAYRECIPT_NO;
            //cmd.Parameters.Add("PIN_BANK_CODE", OracleType.Number).Value = oDPEntry.PIN_BANK_CODE;
            //cmd.Parameters.Add("PIN_BRANCH_CODE", OracleType.Number).Value = oDPEntry.PIN_BRANCH_CODE;
            cmd.Parameters.Add("PIN_ENTDBY", OracleType.VarChar).Value = oDPEntry.PIN_ENTDBY;

            cmd.Transaction = transaction;
            int execution = cmd.ExecuteNonQuery();

            if (execution > 0)
                result = true;

            else
                result = false;

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
            ODP_Log orclLog = new ODP_Log();
            orclLog.WriteLog("Error [ORCL]:: Proposal to Policy [ER_ : " + ex.ErrorCode + " :: " + ex.ToString() + Environment.NewLine);
        }
        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }


    public List<ODP_PrtAdvice> GetPaymentAdviceInfo(string orcl_executor)
    {
        ORCL_Connection orcl_con = new ORCL_Connection();
        List<ODP_PrtAdvice> pa_info = new List<ODP_PrtAdvice>();

        using (OracleConnection connection = orcl_con.GetConnection())
        {
            using (OracleCommand cmd = new OracleCommand(orcl_executor, connection))
            {
                try
                {
                    connection.Open();

                    using (OracleDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr.HasRows)
                            {
                                pa_info.Add(new ODP_PrtAdvice());
                                pa_info[pa_info.Count - 1].out_polno = sdr["POLNO"].ToString();
                                pa_info[pa_info.Count - 1].out_cus_name = sdr["CUS_NAME"].ToString();
                                pa_info[pa_info.Count - 1].out_nic = sdr["NIC"].ToString();
                                pa_info[pa_info.Count - 1].out_policy_sdate = sdr["POLICY_SDATE"].ToString();
                                pa_info[pa_info.Count - 1].out_policy_edate = sdr["POLICY_EDATE"].ToString();
                                pa_info[pa_info.Count - 1].out_bbnam = sdr["BBNAM"].ToString();
                                pa_info[pa_info.Count - 1].out_bbrnch = sdr["BBRNCH"].ToString();
                                pa_info[pa_info.Count - 1].out_suminsurd = double.Parse(sdr["SUMINSURD"].ToString());
                                pa_info[pa_info.Count - 1].out_tot_premium = double.Parse(sdr["TOT_PREMIUM"].ToString());
                                break;
                            }
                        }
                        Trans_Sucess_State = true;
                        sdr.Dispose();
                    }
                }
                catch (OracleException ex)
                {
                    //throw new Exception(ex.Message);
                    Trans_Sucess_State = false;
                    Error_Code = ex.ErrorCode;
                    Error_Message = ex.Message;

                    ODP_Log orclLog = new ODP_Log();
                    orclLog.WriteLog("Error [ORCL]:: Read Print Advice [ER_ : " + ex.ErrorCode + " :: " + ex.ToString() + Environment.NewLine);
                }
                finally
                {
                    // always call Close when done reading.
                    connection.Close();
                    connection.Dispose();
                }
            }
            return pa_info;
        }
    }

    public List<ODP_PolicyPrint> GetPolicyPrintInfo(string orcl_executor)
    {
        ORCL_Connection orcl_con = new ORCL_Connection();
        List<ODP_PolicyPrint> policy_info = new List<ODP_PolicyPrint>();

        using (OracleConnection connection = orcl_con.GetConnection())
        {
            using (OracleCommand cmd = new OracleCommand(orcl_executor, connection))
            {
                try
                {
                    connection.Open();

                    using (OracleDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            if (sdr.HasRows)
                            {
                                policy_info.Add(new ODP_PolicyPrint());
                                policy_info[policy_info.Count - 1].out_polno = sdr["POLNO"].ToString();
                                policy_info[policy_info.Count - 1].out_cus_name = sdr["CUS_NAME"].ToString();
                                policy_info[policy_info.Count - 1].out_nic = sdr["NIC"].ToString();
                                policy_info[policy_info.Count - 1].out_add_l1 = sdr["ADD_L1"].ToString();
                                policy_info[policy_info.Count - 1].out_add_l2 = sdr["ADD_L2"].ToString();
                                policy_info[policy_info.Count - 1].out_add_l3 = sdr["ADD_L3"].ToString();
                                policy_info[policy_info.Count - 1].out_add_l4 = sdr["ADD_L4"].ToString();
                                policy_info[policy_info.Count - 1].out_bbnam = sdr["BBNAM"].ToString();
                                policy_info[policy_info.Count - 1].out_bbrnch = sdr["BBRNCH"].ToString();
                                policy_info[policy_info.Count - 1].out_policy_sdate = sdr["POLICY_SDATE"].ToString();
                                policy_info[policy_info.Count - 1].out_policy_edate = sdr["POLICY_EDATE"].ToString();
                                policy_info[policy_info.Count - 1].out_policy_rendate = sdr["POLICY_REDATE"].ToString();
                                policy_info[policy_info.Count - 1].out_suminsurd = double.Parse(sdr["SUMINSURD"].ToString());
                                policy_info[policy_info.Count - 1].out_netpremium = double.Parse(sdr["NETPREMIUM"].ToString());
                                policy_info[policy_info.Count - 1].out_admin_fee = double.Parse(sdr["ADMIN_FEE"].ToString());
                                policy_info[policy_info.Count - 1].out_policy_fee = double.Parse(sdr["POLICY_FEE"].ToString());
                                policy_info[policy_info.Count - 1].out_vat = double.Parse(sdr["VAT"].ToString());
                                break;
                            }
                        }
                        Trans_Sucess_State = true;
                        sdr.Dispose();
                    }
                }
                catch (OracleException ex)
                {
                    //throw new Exception(ex.Message);
                    Trans_Sucess_State = false;
                    Error_Code = ex.ErrorCode;
                    Error_Message = ex.Message;

                    ODP_Log orclLog = new ODP_Log();
                    orclLog.WriteLog("Error [ORCL]:: Read Print Advice [ER_ : " + ex.ErrorCode + " :: " + ex.ToString() + Environment.NewLine);
                }
                finally
                {
                    // always call Close when done reading.
                    connection.Close();
                    connection.Dispose();
                }
            }
            return policy_info;
        }
    }

}