using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;

/// <summary>
/// Summary description for Update_class
/// </summary>
public class Update_class
{
	public Update_class()
	{
		//
		// TODO: Add constructor logic here
		//
	}


    bool Sucess_State;
    int Error_number;
    string _Error_Message;

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


    /*-----------------------------------------------------------kavinda 24/12/2019 development------------------------------------*/

    public bool update_quotation(string app_ip, string epf, string remark, string app_status, string ref_no, string req_id)
    {


      string exe_Update = "UPDATE  QUOTATION.QUOTATION_APPROVAL_REQUEST ";
      exe_Update += "SET APPROVED_DATE= SYSDATE, ";
      exe_Update += "APPROVED_IP =:PIN_APP_IP, ";
      exe_Update += "APPROVED_EPF=:PIN_APP_EPF, ";
      exe_Update += "REMARKS=:PIN_REMARK, ";
      exe_Update += "APPROVED_STATUS=:PIN_APP_STATUS ";
      exe_Update += "WHERE QREF_NO= :PIN_REF_NO ";
      exe_Update += "AND APPROVED_STATUS ='N'";



      string exe_doc_table = "update QUOTATION.QUOTATION_DOCUMENTS ";
      exe_doc_table += "set APPROVED_STATUS=:PIN_APP_STATUS_2 ";
      exe_doc_table += "where request_id =:REQ_ID ";
      exe_doc_table += "and APPROVED_STATUS='N'";

        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_APP_IP", OracleType.VarChar).Value = app_ip;
            cmd_backup.Parameters.Add(":PIN_APP_EPF", OracleType.VarChar).Value = epf;
            cmd_backup.Parameters.Add(":PIN_REMARK", OracleType.VarChar).Value = remark;
            cmd_backup.Parameters.Add(":PIN_APP_STATUS", OracleType.VarChar).Value = app_status;
            cmd_backup.Parameters.Add(":PIN_REF_NO", OracleType.VarChar).Value = ref_no;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {

                OracleCommand doc_table = con.CreateCommand();
                doc_table.CommandType = CommandType.Text;
                doc_table.CommandText = exe_doc_table;

                doc_table.Parameters.Add(":PIN_APP_STATUS_2", OracleType.VarChar).Value = app_status;
                doc_table.Parameters.Add(":REQ_ID", OracleType.VarChar).Value = req_id;

                doc_table.Transaction = transaction;
                int doc_updte = doc_table.ExecuteNonQuery();


            }

            else
            {
                result = false;
            }

            //var _ApprovalRequest = new ApprovalRequest();
            //string requestReason = _ApprovalRequest.getUpdateData(ref_no);

            transaction.Commit();


            //var _ApprovalRequestEmails = new ApprovalRequestEmails();
            //if (_ApprovalRequestEmails.sendAprovalEmail("AR", 0, epf, "","", req_id, requestReason, ref_no, epf, app_status, remark,"N"))
            //{
            //    result = true;
            //}
            //else
            //{
            //    result = true;
            //}
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>

    /*-----------------------------------------------------------Update policy number Sequence Table------------------------------------*/

    public bool update_policySeqNumber(string SEQ, string YEAR, string MONTH, string polType)
    {

        string exe_Update = "Update QUOTATION.FIRE_POLICY_SEQ ";
        exe_Update += "set P_SEQ = :PIN_P_SEQ ";
        exe_Update += "where P_TYPE = :PIN_P_TYPE ";
        exe_Update += "and P_YEAR = :PIN_P_YEAR";
        //exe_Update += "and P_MONTH = :PIN_P_MONTH";

        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_P_SEQ", OracleType.VarChar).Value = SEQ;
            cmd_backup.Parameters.Add(":PIN_P_YEAR", OracleType.VarChar).Value = YEAR;
            cmd_backup.Parameters.Add(":PIN_P_TYPE", OracleType.VarChar).Value = polType;
            //cmd_backup.Parameters.Add(":PIN_P_MONTH", OracleType.VarChar).Value = MONTH;
            
            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }       

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>


    /*-----------------------------------------------------------Update policy number Sequence Table------------------------------------*/

    public bool update_policyNumberInCalTable(string REF_NO, string PolicyNumber)
    {



        string exe_Update = "UPDATE QUOTATION.FIRE_DH_SCHEDULE_CALC ";
        exe_Update += "SET SC_POLICY_NO =  :PIN_POL_NO ";
        exe_Update += "WHERE SC_REF_NO = :PIN_REF_NO";
    
        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_REF_NO", OracleType.VarChar).Value = REF_NO;
            cmd_backup.Parameters.Add(":PIN_POL_NO", OracleType.VarChar).Value = PolicyNumber;
        

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>


    /*-----------------------------------------------------------Update Hold Flag------------------------------------*/

    public bool update_HoldFlag(string REF_NO)
    {

        string exe_Update ="UPDATE QUOTATION.FIRE_DH_PROPOSAL_ENTRY ";
        exe_Update += "set DH_FINAL_FLAG = :PIN_FLAG ";
        exe_Update += "where dh_refno = :PIN_REF_NO";

       
        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_REF_NO", OracleType.VarChar).Value = REF_NO;
            cmd_backup.Parameters.Add(":PIN_FLAG", OracleType.VarChar).Value = "N";


            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>

    /*-----------------------------------------------------------Update print or not Flag------------------------------------*/

    public bool update_HoldFlagPrintOrNot(string REF_NO)
    {

        string exe_Update = "UPDATE QUOTATION.FIRE_DH_PROPOSAL_ENTRY ";
        exe_Update += "set DH_ISREQ = :PIN_FLAG ";
        exe_Update += "where dh_refno = :PIN_REF_NO";


        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_REF_NO", OracleType.VarChar).Value = REF_NO;
            cmd_backup.Parameters.Add(":PIN_FLAG", OracleType.VarChar).Value = "Y";


            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>
    /*-----------------------------------------------------------Update print or not Flag------------------------------------*/

    public bool Update_PolicyWithConditions(string REF_NO, string approveFlag, string rFlag, string conditionFlag, string loadingFlag,
        string condtionDetails, string loadingVal, string finalFlag,string bothFlag, string deductFlag, string deducVal, string deducPre, string userId)
    {

        string exe_Update = "UPDATE QUOTATION.FIRE_DH_PROPOSAL_ENTRY ";


        if (rFlag == "Y")
        {

            exe_Update += "set DH_ISREJECT = :PIN_RFlag, ";
            exe_Update += "DH_FINAL_FLAG = :PIN_FLAG, ";
        }
        else if (approveFlag == "Y")
        {
            exe_Update += "set DH_FINAL_FLAG = :PIN_FLAG, ";
        }
        else if (conditionFlag == "Y")
        {
            exe_Update += "set DH_FINAL_FLAG = :PIN_FLAG, ";
            exe_Update += "DH_CONDITIONS = :PIN_CONDITION, ";
            exe_Update += "DH_ISCODI = :PIN_CONDITION_FLAG, ";
        }
        else if (loadingFlag == "Y")
        {
            exe_Update += "set DH_FINAL_FLAG = :PIN_FLAG, ";
            exe_Update += "DH_LOADING_VAL = :PIN_LOADING_VAL, ";
            exe_Update += "DH_LOADING = :PIN_LOADING_FLAG, ";

        }

        else if (bothFlag == "Y")
        {
            exe_Update += "set DH_FINAL_FLAG = :PIN_FLAG, ";
            exe_Update += "DH_CONDITIONS = :PIN_CONDITION, ";
            exe_Update += "DH_ISCODI = :PIN_CONDITION_FLAG, ";
            exe_Update += "DH_LOADING_VAL = :PIN_LOADING_VAL, ";
            exe_Update += "DH_LOADING = :PIN_LOADING_FLAG, ";
        }

        else if (deductFlag == "Y")
        {
            exe_Update += "set DH_FINAL_FLAG = :PIN_FLAG, ";
            exe_Update += "ADDED_DEDUCT = :PIN_ADDED_DEDUCT, ";
            exe_Update += "DED_PRECENTAGE = :PIN_DED_PRECENTAGE, ";
        }

        exe_Update += "UPDATED_BY = :PIN_UPDATED_BY, ";
        exe_Update += "UPDATED_ON = :PIN_UPDATED_ON ";
        exe_Update += "where dh_refno = :PIN_REF_NO";


        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;


            if(conditionFlag=="X" && loadingFlag == "X") { conditionFlag = "Y";loadingFlag = "Y"; }

            if (!string.IsNullOrEmpty(rFlag))
            {
                cmd_backup.Parameters.Add(":PIN_RFlag", OracleType.VarChar).Value = rFlag;
            }
            if (!string.IsNullOrEmpty(condtionDetails))
            {
                cmd_backup.Parameters.Add(":PIN_CONDITION", OracleType.VarChar).Value = condtionDetails;
            }
            if (!string.IsNullOrEmpty(conditionFlag))
            {
                cmd_backup.Parameters.Add(":PIN_CONDITION_FLAG", OracleType.VarChar).Value = conditionFlag;
            }
            if (!string.IsNullOrEmpty(loadingVal))
            {
                cmd_backup.Parameters.Add(":PIN_LOADING_VAL", OracleType.VarChar).Value = loadingVal;
            }
            if (!string.IsNullOrEmpty(loadingFlag))
            {
                cmd_backup.Parameters.Add(":PIN_LOADING_FLAG", OracleType.VarChar).Value = loadingFlag;
            }
            if (!string.IsNullOrEmpty(finalFlag))
            {  
                    cmd_backup.Parameters.Add(":PIN_FLAG", OracleType.VarChar).Value = finalFlag;
            }

            if (!string.IsNullOrEmpty(deducVal) && !string.IsNullOrEmpty(deducPre))
            {
                cmd_backup.Parameters.Add(":PIN_ADDED_DEDUCT", OracleType.VarChar).Value = deducVal;
                cmd_backup.Parameters.Add(":PIN_DED_PRECENTAGE", OracleType.VarChar).Value = deducPre;
            }
            
                //cmd_backup.Parameters.Add(":PIN_RFlag", OracleType.VarChar).Value = rFlag;
            cmd_backup.Parameters.Add(":PIN_REF_NO", OracleType.VarChar).Value = REF_NO;
            //cmd_backup.Parameters.Add(":PIN_FLAG", OracleType.VarChar).Value = "N";
            cmd_backup.Parameters.Add(":PIN_UPDATED_BY", OracleType.VarChar).Value = userId;
            cmd_backup.Parameters.Add(":PIN_UPDATED_ON", OracleType.DateTime).Value = System.DateTime.Now;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>


    /*-----------------------------------------------------------Update policy number Sequence Table------------------------------------*/

    public bool update_shcduleCalLoadingValue(string REF_NO, double NET_PREM, double ADMIN_FEE, double NBT,double VAT,double TOTAL,string UPDATE_BY)
    {


        string exe_Update = "UPDATE QUOTATION.FIRE_DH_SCHEDULE_CALC ";
        exe_Update += "SET SC_NET_PRE =  :PIN_SC_NET_PRE, ";
        exe_Update += "SC_ADMIN_FEE =  :PIN_SC_ADMIN_FEE, ";
        exe_Update += "SC_NBT =  :PIN_SC_NBT, ";
        exe_Update += "SC_VAT =  :PIN_SC_VAT, ";
        exe_Update += "SC_TOTAL_PAY =  :PIN_SC_TOTAL_PAY, ";
        exe_Update += "UPDATED_ON =  SYSDATE, ";
        exe_Update += "UPDATED_BY =  :PIN_UPDATED_BY ";
        exe_Update += "WHERE SC_REF_NO = :PIN_REF_NO";

        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_REF_NO", OracleType.VarChar).Value = REF_NO;
            cmd_backup.Parameters.Add(":PIN_SC_NET_PRE", OracleType.Double).Value = NET_PREM;
            cmd_backup.Parameters.Add(":PIN_SC_ADMIN_FEE", OracleType.Double).Value = ADMIN_FEE;
            cmd_backup.Parameters.Add(":PIN_SC_NBT", OracleType.Double).Value = NBT;
            cmd_backup.Parameters.Add(":PIN_SC_VAT", OracleType.Double).Value = VAT;
            cmd_backup.Parameters.Add(":PIN_SC_TOTAL_PAY", OracleType.Double).Value = TOTAL;
            cmd_backup.Parameters.Add(":PIN_UPDATED_BY", OracleType.VarChar).Value = UPDATE_BY;
           

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>

    /*-----------------------------------------------------------Update customer Details when bank edit------------------------------------*/

    public bool update_CustomerDetails(string DH_REFNO, string DH_NAME, string DH_PADD1, string DH_PADD2, string DH_PADD3, string DH_PADD4, string DH_IADD1, 
        string DH_IADD2, string DH_IADD3, string DH_IADD4,string UPDATED_BY)
    {


        string exe_Update = "UPDATE QUOTATION.FIRE_DH_PROPOSAL_ENTRY ";
        exe_Update += "SET DH_NAME =  :PIN_DH_NAME, ";
        exe_Update += "DH_PADD1 =  :PIN_DH_PADD1, ";
        exe_Update += "DH_PADD2 =  :PIN_DH_PADD2, ";
        exe_Update += "DH_PADD3 =  :PIN_DH_PADD3, ";
        exe_Update += "DH_PADD4 =  :PIN_DH_PADD4, ";

        exe_Update += "DH_IADD1 =  :PIN_DH_IADD1, ";
        exe_Update += "DH_IADD2 =  :PIN_DH_IADD2, ";
        exe_Update += "DH_IADD3 =  :PIN_DH_IADD3, ";
        exe_Update += "DH_IADD4 =  :PIN_DH_IADD4, ";
        exe_Update += "BANK_UPDATED_BY =  :PIN_BANK_UPDATED_BY, ";
        exe_Update += "BANK_UPDATED_ON=  :PIN_BANK_UPDATED_ON ";
        exe_Update += "WHERE DH_REFNO = :PIN_DH_REFNO";

        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_DH_REFNO", OracleType.VarChar).Value = DH_REFNO;
            cmd_backup.Parameters.Add(":PIN_DH_NAME", OracleType.VarChar).Value = DH_NAME;

            cmd_backup.Parameters.Add(":PIN_DH_PADD1", OracleType.VarChar).Value = DH_PADD1;
            cmd_backup.Parameters.Add(":PIN_DH_PADD2", OracleType.VarChar).Value = DH_PADD2;
            cmd_backup.Parameters.Add(":PIN_DH_PADD3", OracleType.VarChar).Value = DH_PADD3;
            cmd_backup.Parameters.Add(":PIN_DH_PADD4", OracleType.VarChar).Value = DH_PADD4;

            cmd_backup.Parameters.Add(":PIN_DH_IADD1", OracleType.VarChar).Value = DH_IADD1;
            cmd_backup.Parameters.Add(":PIN_DH_IADD2", OracleType.VarChar).Value = DH_IADD2;
            cmd_backup.Parameters.Add(":PIN_DH_IADD3", OracleType.VarChar).Value = DH_IADD3;
            cmd_backup.Parameters.Add(":PIN_DH_IADD4", OracleType.VarChar).Value = DH_IADD4;

            cmd_backup.Parameters.Add(":PIN_BANK_UPDATED_BY", OracleType.VarChar).Value = UPDATED_BY;
            cmd_backup.Parameters.Add(":PIN_BANK_UPDATED_ON", OracleType.DateTime).Value = DateTime.Now;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>

        ///-------------upadte cancele flag---------------------------->>
    public bool update_CancelFlag(string REF_NO,string remarks,string userId)
    {

        string exe_Update = "UPDATE QUOTATION.FIRE_DH_PROPOSAL_ENTRY ";
        //exe_Update += "set DH_ISREQ = :PIN_FLAG, ";
        exe_Update += "Set DH_FINAL_FLAG = :PIN_FINAL_FLAG, ";
        exe_Update += "REMARKS = :PIN_REMARKS, ";
        exe_Update += "UPDATED_BY = :PIN_UPDATED_BY, ";
        exe_Update += "UPDATED_ON = :PIN_UPDATED_ON ";
        exe_Update += "where dh_refno = :PIN_REF_NO";

     
        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_REF_NO", OracleType.VarChar).Value = REF_NO;
           // cmd_backup.Parameters.Add(":PIN_FLAG", OracleType.VarChar).Value = "C";
            cmd_backup.Parameters.Add(":PIN_FINAL_FLAG", OracleType.VarChar).Value = "C";
            cmd_backup.Parameters.Add(":PIN_REMARKS", OracleType.VarChar).Value = remarks;
            cmd_backup.Parameters.Add(":PIN_UPDATED_BY", OracleType.VarChar).Value = userId;
            cmd_backup.Parameters.Add(":PIN_UPDATED_ON", OracleType.DateTime).Value = System.DateTime.Now;


            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>

    //motor quotation update ------------09112021---------------->>>>

    public bool update_motorQuotationTable(string refNo, string flag, string quoNo, string createBy, string remarks)
    {

        string exe_Update = "UPDATE QUOTATION.BANK_REQ_ENTRY_DETAILS ";
        exe_Update += "SET QUO_NO = :PIN_txtQuoNo, ";
        exe_Update += "FLAG = :PIN_flag, ";
        exe_Update += "EMAIL_SEND_BY = :PIN_appOfficer, ";
        exe_Update += "SLIC_REMARK = :PIN_remarks, ";
        exe_Update += "EMAIL_SEND_ON = :PIN_sendDate ";
        exe_Update += "WHERE REQ_ID = :PIN_txtReqId";
        
        string exe_doc_table = "update QUOTATION.BANCASSU_UPMOTO_QUOTATIONS ";
        exe_doc_table += "set Q_FLAG =:PIN_Q_flag ";
        exe_doc_table += "where Q_REF =:PIN_Q_REF ";
        exe_doc_table += "and P_ACTIVE='Y'";

        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_txtQuoNo", OracleType.VarChar).Value = quoNo;
            cmd_backup.Parameters.Add(":PIN_flag", OracleType.VarChar).Value = flag;
            cmd_backup.Parameters.Add(":PIN_appOfficer", OracleType.VarChar).Value = createBy;
            cmd_backup.Parameters.Add(":PIN_remarks", OracleType.VarChar).Value = remarks;
            cmd_backup.Parameters.Add(":PIN_sendDate",OracleType.DateTime).Value = System.DateTime.Now;
            cmd_backup.Parameters.Add(":PIN_txtReqId", OracleType.VarChar).Value = refNo;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {

                OracleCommand doc_table = con.CreateCommand();
                doc_table.CommandType = CommandType.Text;
                doc_table.CommandText = exe_doc_table;

                doc_table.Parameters.Add(":PIN_Q_flag", OracleType.VarChar).Value = flag;
                doc_table.Parameters.Add(":PIN_Q_REF", OracleType.VarChar).Value = refNo;

                doc_table.Transaction = transaction;
                int doc_updte = doc_table.ExecuteNonQuery();

                transaction.Commit();
                result = true;

            }

            else
            {
                result = false;
            }


        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }

        return result;
    }

    //------------------------End--------->>>>
    //motor quotation update bank and slic remarks------------09112021---------------->>>>

    public bool update_motorQuotationBankRemarks(string refNo, string createBy, string remarks)
    {

        string exe_Update = "UPDATE QUOTATION.BANCASSU_MOTOQUOREMARKS ";
        exe_Update += "SET R_BANK = :PIN_R_BANK, ";
        exe_Update += "R_UP_BY_BANK = :PIN_R_UP_BY_BANK, ";
        exe_Update += "R_UP_ON_BANK = :PIN_R_UP_ON_BANK ";

        exe_Update += "WHERE R_REF = :PIN_R_REF ";
        exe_Update += "AND R_NO = (select max(r_no) from QUOTATION.BANCASSU_MOTOQUOREMARKS ";
        exe_Update += "where r_ref = :PIN_R_REF ";
        exe_Update += "and r_flag = 'A' ";
        exe_Update += ")";

        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_R_BANK", OracleType.VarChar).Value = remarks;
            cmd_backup.Parameters.Add(":PIN_R_UP_BY_BANK", OracleType.VarChar).Value = createBy;
            cmd_backup.Parameters.Add(":PIN_R_REF", OracleType.VarChar).Value = refNo;
            cmd_backup.Parameters.Add(":PIN_R_UP_ON_BANK", OracleType.DateTime).Value = System.DateTime.Now;


            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {

                transaction.Commit();
                result = true;

            }

            else
            {
                result = false;
            }


        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }

        return result;
    }

    //------------------------End--------->>>>

    public bool update_motorQuotationNeedMoreBank(string refNo, string flag)
    {

        string exe_Update = "UPDATE QUOTATION.BANK_REQ_ENTRY_DETAILS ";
        exe_Update += "SET FLAG = :PIN_flag ";
        exe_Update += "WHERE REQ_ID = :PIN_REQ_ID";

        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_flag", OracleType.VarChar).Value = flag;
            cmd_backup.Parameters.Add(":PIN_REQ_ID", OracleType.VarChar).Value = refNo;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                transaction.Commit();
                result = true;

            }

            else
            {
                result = false;
            }


        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }

        return result;
    }


    /*-----------------------------------------------------------Update Upload document table hide docs------------------------------------*/

    public bool update_doc_table(string DOC_F_NO, string UPDATED_BY)
    {


        string exe_Update = "UPDATE QUOTATION.BANCASSU_UPMOTO_QUOTATIONS ";
        exe_Update += "SET P_ACTIVE =  :PIN_P_ACTIVE, ";
        exe_Update += "UPDATED_BY =  :PIN_UPDATED_BY, ";
        exe_Update += "UPDATED_ON =  :PIN_UPDATED_ON ";
        exe_Update += "WHERE Q_NO = :PIN_Q_NO";

        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_Q_NO", OracleType.VarChar).Value = DOC_F_NO;
            cmd_backup.Parameters.Add(":PIN_P_ACTIVE", OracleType.VarChar).Value = "N";
            cmd_backup.Parameters.Add(":PIN_UPDATED_BY", OracleType.VarChar).Value = UPDATED_BY;
            cmd_backup.Parameters.Add(":PIN_UPDATED_ON", OracleType.DateTime).Value = System.DateTime.Now;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>


    /*-----------------------------------------------------------Update Upload document table hide docs response team ticket------------------------------------*/

    public bool update_Bankdoc_table_ticket(string DOC_F_NO, string UPDATED_BY)
    {


        string exe_Update = "UPDATE QUOTATION.BANCASSU_BANK_ITUPDOC ";
        exe_Update += "SET T_ACTIVE =  :PIN_T_ACTIVE, ";
        exe_Update += "UPDATED_BY =  :PIN_UPDATED_BY, ";
        exe_Update += "UPDATED_ON =  :PIN_UPDATED_ON ";
        exe_Update += "WHERE T_NO = :PIN_T_NO";

        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_T_NO", OracleType.VarChar).Value = DOC_F_NO;
            cmd_backup.Parameters.Add(":PIN_T_ACTIVE", OracleType.VarChar).Value = "N";
            cmd_backup.Parameters.Add(":PIN_UPDATED_BY", OracleType.VarChar).Value = UPDATED_BY;
            cmd_backup.Parameters.Add(":PIN_UPDATED_ON", OracleType.DateTime).Value = System.DateTime.Now;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>

    /*-----------------------------------------------------------Update table response team ticket slic side response------------------------------------*/

    public bool update_table_ticket(string T_NO, string T_SLIC_REMARK, string UPDATED_BY,string T_STATUS)
    {


        string exe_Update = "UPDATE QUOTATION.BANCASSU_INQU_TICKETS ";
        exe_Update += "SET T_SLIC_REMARK =  :PIN_T_SLIC_REMARK, ";
        exe_Update += "T_UP_BY_SLIC =  :PIN_T_UP_BY_SLIC, ";
        exe_Update += "T_UP_ON_SLIC =  :PIN_T_UP_ON_SLIC, ";
        exe_Update += "T_STATUS =  :PIN_T_STATUS ";
        exe_Update += "WHERE T_REF = :PIN_T_REF";

        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_T_REF", OracleType.VarChar).Value = T_NO;
            cmd_backup.Parameters.Add(":PIN_T_SLIC_REMARK", OracleType.VarChar).Value = T_SLIC_REMARK;
            cmd_backup.Parameters.Add(":PIN_T_UP_BY_SLIC", OracleType.VarChar).Value = UPDATED_BY;
            cmd_backup.Parameters.Add(":PIN_T_STATUS", OracleType.VarChar).Value = T_STATUS;
            cmd_backup.Parameters.Add(":PIN_T_UP_ON_SLIC", OracleType.DateTime).Value = System.DateTime.Now;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>

    /*-----------------------------------------------------------Update Upload document table hide docs response team ticket------------------------------------*/

    public bool update_SLICdoc_table_ticket(string DOC_F_NO, string UPDATED_BY)
    {


        string exe_Update = "UPDATE QUOTATION.BANCASSU_SLIC_ITUPDOC ";
        exe_Update += "SET T_ACTIVE =  :PIN_T_ACTIVE, ";
        exe_Update += "UPDATED_BY =  :PIN_UPDATED_BY, ";
        exe_Update += "UPDATED_ON =  :PIN_UPDATED_ON ";
        exe_Update += "WHERE T_NO = :PIN_T_NO";

        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_T_NO", OracleType.VarChar).Value = DOC_F_NO;
            cmd_backup.Parameters.Add(":PIN_T_ACTIVE", OracleType.VarChar).Value = "N";
            cmd_backup.Parameters.Add(":PIN_UPDATED_BY", OracleType.VarChar).Value = UPDATED_BY;
            cmd_backup.Parameters.Add(":PIN_UPDATED_ON", OracleType.DateTime).Value = System.DateTime.Now;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>
    /*-----------------------------------------------------------Update policy number Sequence Table with debit note(mekhala) ------------------------------------*/

    public bool update_DebitNoteInCalTable(string REF_NO, string data, string paymentDate, string debitno, string BGICal)
    {
        string exe_Update = "UPDATE QUOTATION.FIRE_DH_SCHEDULE_CALC ";
        exe_Update += "SET DEBITSEQ = :PIN_DEBSEQ , ";
        exe_Update += "PAYMENT_DATE = :PIN_PAYMENTDATE, ";
        exe_Update += "BANCAGI = :PIN_BANCAGI, ";
        exe_Update += "DEBIT_NO = :PIN_DEBIT_NO ";
        exe_Update += "WHERE SC_REF_NO = :PIN_REF_NO";

        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_REF_NO", OracleType.VarChar).Value = REF_NO;
            cmd_backup.Parameters.Add(":PIN_DEBSEQ", OracleType.VarChar).Value = data;
            cmd_backup.Parameters.Add(":PIN_PAYMENTDATE", OracleType.DateTime).Value = Convert.ToDateTime(paymentDate);
            cmd_backup.Parameters.Add(":PIN_BANCAGI", OracleType.VarChar).Value = BGICal;
            cmd_backup.Parameters.Add(":PIN_DEBIT_NO", OracleType.VarChar).Value = debitno;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }


    //------------------------debit note add End(Mekhala 20240911)--------->>>>

    /*-----------------------------------------------------------Update BPF avalible or not branches------------------------------------*/

    public bool update_BPF_Allow(string flag, string UPDATED_BY, string bank, string branch,string un)
    {



        string exe_Update = "update QUOTATION.BPF_ACC_CONTROL ";
        exe_Update += "set active_flag = :PIN_FLAG, ";
        exe_Update += "UPDATED_BY =  :PIN_UPDATED_BY, ";
        exe_Update += "UPDATED_ON =  :PIN_UPDATED_ON ";
        exe_Update += "where username is not null ";
        exe_Update += "and bank_code = :PIN_BANK ";
        exe_Update += "and branch_code = :PIN_BRANCH ";
        exe_Update += "and username = :PIN_UN";


        bool result = false;

        ORCL_Connection orcl_con = new ORCL_Connection();
        OracleConnection con = new OracleConnection();

        con = orcl_con.GetConnection();
        OracleTransaction transaction = null;
        con.Open();
        try
        {
            transaction = con.BeginTransaction();

            OracleCommand cmd_backup = con.CreateCommand();
            cmd_backup.CommandType = CommandType.Text;
            cmd_backup.CommandText = exe_Update;

            cmd_backup.Parameters.Add(":PIN_UN", OracleType.VarChar).Value = un;
            cmd_backup.Parameters.Add(":PIN_FLAG", OracleType.VarChar).Value = flag;
            cmd_backup.Parameters.Add(":PIN_BANK", OracleType.Int32).Value = Convert.ToInt32(bank);
            cmd_backup.Parameters.Add(":PIN_BRANCH", OracleType.Int32).Value = Convert.ToInt32(branch);

            cmd_backup.Parameters.Add(":PIN_UPDATED_BY", OracleType.VarChar).Value = UPDATED_BY;
            cmd_backup.Parameters.Add(":PIN_UPDATED_ON", OracleType.DateTime).Value = System.DateTime.Now;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {


                result = true;

            }

            else
            {
                result = false;
            }

            transaction.Commit();
        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return result;
    }

    //------------------------End--------->>>>
}