using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Insert_class
/// </summary>
public class Insert_class
{
    Oracle_Transaction or_trn = new Oracle_Transaction();
    public Insert_class()
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
    
    public bool insert_quotation_details( string V_TYPE, int YOM, double SUM_INSU, int V_MAKE,
                int V_MODEL, string PURPOSE, string V_REG_NO, string CUS_NAME, string CUS_PHONE, string V_FUEL,
                string ENTERED_BY, string EMAIL,string FLAG, string BANK_CODE, string BRANCH_CODE, string OTHER_MODEL, string BANC_EMAIL,string bank_code,out string Gen_req_id)
    {
        //string REQ_ID, string BANK_CODE,

        string exe_Insert = "INSERT INTO QUOTATION.BANK_REQ_ENTRY_DETAILS( ";
        exe_Insert += "REQ_ID, BANK_CODE, V_TYPE, YOM, SUM_INSU, V_MAKE, V_MODEL, PURPOSE, V_REG_NO, CUS_NAME, CUS_PHONE, V_FUEL, ENTERED_BY, ENTERED_ON, EMAIL,FLAG,BRANCH_CODE,OTHER_MODEL,BANC_EMAIL) ";
        exe_Insert += "VALUES(:PIN_REQ_ID, :PIN_BANK_CODE, :PIN_V_TYPE, :PIN_YOM, :PIN_SUM_INSU, :PIN_V_MAKE, :PIN_V_MODEL, :PIN_PURPOSE, :PIN_V_REG_NO, :PIN_CUS_NAME, :PIN_CUS_PHONE, :PIN_V_FUEL, :PIN_ENTERED_BY, sysdate,:PIN_EMAIL,:PIN_FLAG,:PIN_BRANCH_CODE,:PIN_OTHER_MODEL,:PIN_BANC_EMAIL)";

        string req_val = "select QUOTATION.banc_id_seq.nextval from dual";
        string req_id = or_trn.GetString(req_val);        

        bool result = false;
        Gen_req_id = "";
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
            cmd_backup.CommandText = exe_Insert;


            cmd_backup.Parameters.Add(":PIN_REQ_ID", OracleType.VarChar).Value = bank_code + req_id;
            cmd_backup.Parameters.Add(":PIN_BANK_CODE", OracleType.VarChar).Value = BANK_CODE;
            cmd_backup.Parameters.Add(":PIN_V_TYPE", OracleType.VarChar).Value = V_TYPE.ToUpper();
            cmd_backup.Parameters.Add(":PIN_YOM", OracleType.VarChar).Value = YOM;
            cmd_backup.Parameters.Add(":PIN_SUM_INSU", OracleType.VarChar).Value = SUM_INSU;

            cmd_backup.Parameters.Add(":PIN_V_MAKE", OracleType.VarChar).Value = V_MAKE;
            cmd_backup.Parameters.Add(":PIN_V_MODEL", OracleType.VarChar).Value = V_MODEL;
            cmd_backup.Parameters.Add(":PIN_PURPOSE", OracleType.VarChar).Value = PURPOSE.ToUpper();
            cmd_backup.Parameters.Add(":PIN_V_REG_NO", OracleType.VarChar).Value = V_REG_NO.ToUpper();
            cmd_backup.Parameters.Add(":PIN_CUS_NAME", OracleType.VarChar).Value = CUS_NAME.ToUpper();
            cmd_backup.Parameters.Add(":PIN_BRANCH_CODE", OracleType.VarChar).Value = BRANCH_CODE;
            

            cmd_backup.Parameters.Add(":PIN_CUS_PHONE", OracleType.VarChar).Value = CUS_PHONE;
            cmd_backup.Parameters.Add(":PIN_V_FUEL", OracleType.VarChar).Value = V_FUEL.ToUpper();
            cmd_backup.Parameters.Add(":PIN_ENTERED_BY", OracleType.VarChar).Value = ENTERED_BY;
            cmd_backup.Parameters.Add(":PIN_EMAIL", OracleType.VarChar).Value = EMAIL.ToLower();
            cmd_backup.Parameters.Add(":PIN_FLAG", OracleType.VarChar).Value = FLAG.ToUpper();
            cmd_backup.Parameters.Add(":PIN_OTHER_MODEL", OracleType.VarChar).Value = OTHER_MODEL;
            cmd_backup.Parameters.Add(":PIN_BANC_EMAIL", OracleType.VarChar).Value = BANC_EMAIL.ToLower();

          
            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {

                result = true;
                Gen_req_id = bank_code + req_id;


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


    //----SMS send to officer----------------------------------------------------------------------------------------------------

    public int Send_sms_to_customer(string contactNumber,string reqId,string txt_body,out int sendCount)
    {
        //string REQ_ID, string BANK_CODE,

        string exe_Insert = "INSERT INTO SMS.SMS_GATEWAY(APPLICATION_ID, JOB_CATEGORY, SMS_TYPE, MOBILE_NUMBER, TEXT_MESSAGE, SHORT_CODE, RECORD_STATUS, JOB_OTHER_INFO) ";
        exe_Insert += "VALUES(:PIN_APPLICATION_ID, :PIN_JOB_CATEGORY, :PIN_SMS_TYPE, :PIN_MOBILE_NUMBER, :PIN_TEXT_MESSAGE, :PIN_SHORT_CODE, :PIN_RECORD_STATUS, :PIN_JOB_OTHER_INFO)";

        sendCount = 0;

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
            cmd_backup.CommandText = exe_Insert;


            cmd_backup.Parameters.Add(":PIN_APPLICATION_ID", OracleType.VarChar).Value = "BANCAS_FIRE_PRO";
            cmd_backup.Parameters.Add(":PIN_JOB_CATEGORY", OracleType.VarChar).Value = "CAT151";
            cmd_backup.Parameters.Add(":PIN_SMS_TYPE", OracleType.VarChar).Value = "I";
            cmd_backup.Parameters.Add(":PIN_MOBILE_NUMBER", OracleType.VarChar).Value =contactNumber; //94777554194
            cmd_backup.Parameters.Add(":PIN_TEXT_MESSAGE", OracleType.VarChar).Value = txt_body+ reqId;//"Motor Quotation Request from Bank. Reference ID : " + reqId;

            cmd_backup.Parameters.Add(":PIN_SHORT_CODE", OracleType.VarChar).Value = "SLIC%20MOTOR";
            cmd_backup.Parameters.Add(":PIN_RECORD_STATUS", OracleType.VarChar).Value = "N";
            cmd_backup.Parameters.Add(":PIN_JOB_OTHER_INFO", OracleType.VarChar).Value = reqId;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {

                sendCount = 1;
            }

            else
            {
                sendCount = 0;
            }


            transaction.Commit();

        }

        catch (OracleException ex)
        {
            sendCount = 0;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return sendCount;
    }


    //---------------------END---------------->>

    /*-----------------------------------------------------------kavinda 07/07/2020 development------------------------------------*/

    public bool insert_fire_proposal_details(string REFNO, string BCODE, string BBRCODE, string NAME, string AGECODE, string AGENAME, string NIC, string BR,
                                             string PADD1, string PADD2, string PADD3, string PADD4, string PHONE, string EMAIL, string IADD1, string IADD2, string IADD3, string IADD4,
                                            DateTime PFROM, DateTime PTO, string UCONSTR, string OCCU_CAR, string OCC_YES_REAS, string HAZ_OCCU, string HAZ_YES_REA,
                                            double VALU_BUILD, double VALU_WALL,
                                            double VALU_TOTAL, string AFF_FLOOD, string AFF_YES_REAS, string WBRICK, string WCEMENT, string DWOODEN, string DMETAL,
                                            string FTILE, string FCEMENT, string RTILE, string RASBES, string RGI, string RCONCREAT,
                                            string COV_FIRE, string COV_LIGHT, string COV_FLOOD, string CFWATERAVL,
                                            string CFYESR1, string CFYESR2, string CFYESR3, string CFYESR4, string ENTERED_BY,
                                            string ENTERED_ON, string HOLD, int no_of_floor, string over_val,string overall_flag, string isReq, string bank_Id,string branch_Id, string LAND_PHONE, double DH_VAL_BANKFAC,
                                            string TERM, double Period, string Fire_cover, string Other_cover, string SRCC_cover,
                                            string TC_cover,string Flood_cover,
                                            string PROP_TYPE, double DH_SOLAR_SUM, string SOLAR_REPAIRE, string SOLAR_PARTS,
                                            string SOLAR_ORIGIN, string SOLAR_SERIAL, double Solar_Period, string LOAN_NUMBER, string SLIC_CODE, string BPF,
                                            out string Gen_refNO)


    {



        string exe_Insert = "INSERT INTO QUOTATION.FIRE_DH_PROPOSAL_ENTRY(DH_REFNO,DH_BCODE,DH_BBRCODE,DH_NAME,DH_AGECODE,DH_AGENAME,DH_NIC,DH_BR,DH_PADD1,DH_PADD2, ";
        exe_Insert += "DH_PADD3,DH_PADD4,DH_PHONE,DH_EMAIL,DH_IADD1,DH_IADD2,DH_IADD3,DH_IADD4,DH_PFROM,DH_PTO,DH_UCONSTR,DH_OCCU_CAR,DH_OCC_YES_REAS,DH_HAZ_OCCU,DH_HAZ_YES_REA,DH_VALU_BUILD, ";
        exe_Insert += "DH_VALU_WALL,DH_VALU_TOTAL,DH_AFF_FLOOD,DH_AFF_YES_REAS,DH_WBRICK,DH_WCEMENT,DH_DWOODEN,DH_DMETAL,DH_FTILE,DH_FCEMENT,DH_RTILE,DH_RASBES,DH_RGI, ";
        exe_Insert += "DH_RCONCREAT,DH_COV_FIRE,DH_COV_LIGHT,DH_COV_FLOOD,DH_CFWATERAVL,DH_CFYESR1,DH_CFYESR2,DH_CFYESR3,DH_CFYESR4,DH_ENTERED_BY,DH_ENTERED_ON,DH_HOLD,DH_NO_OF_FLOORS,DH_OVER_VAL,DH_FINAL_FLAG,DH_ISREQ,dh_bcode_id,dh_bbrcode_id, LAND_PHONE,DH_VAL_BANKFAC, ";
        exe_Insert += "TERM, Period, Fire_cover, Other_cover, SRCC_cover, TC_cover, Flood_cover, ";
        exe_Insert += "PROP_TYPE,DH_SOLAR_SUM,SOLAR_REPAIRE,SOLAR_PARTS,SOLAR_ORIGIN,SOLAR_SERIAL,Solar_Period,LOAN_NUMBER,SLIC_CODE,BPF) ";

        exe_Insert += "VALUES ";
        exe_Insert += "(:PIN_DH_REFNO,:PIN_DH_BCODE,:PIN_DH_BBRCODE,:PIN_DH_NAME,:PIN_DH_AGECODE,:PIN_DH_AGENAME,:PIN_DH_NIC,:PIN_DH_BR,:PIN_DH_PADD1,:PIN_DH_PADD2, ";
        exe_Insert += ":PIN_DH_PADD3,:PIN_DH_PADD4,:PIN_DH_PHONE,:PIN_DH_EMAIL,:PIN_DH_IADD1,:PIN_DH_IADD2,:PIN_DH_IADD3,:PIN_DH_IADD4,:PIN_DH_PFROM,:PIN_DH_PTO, ";
        exe_Insert += ":PIN_DH_UCONSTR,:PIN_DH_OCCU_CAR,:PIN_DH_OCC_YES_REAS,:PIN_DH_HAZ_OCCU,:PIN_DH_HAZ_YES_REA,:PIN_DH_VALU_BUILD,:PIN_DH_VALU_WALL,:PIN_DH_VALU_TOTAL, ";
        exe_Insert += ":PIN_DH_AFF_FLOOD,:PIN_DH_AFF_YES_REAS,:PIN_DH_WBRICK,:PIN_DH_WCEMENT,:PIN_DH_DWOODEN,:PIN_DH_DMETAL,:PIN_DH_FTILE,:PIN_DH_FCEMENT,:PIN_DH_RTILE,:PIN_DH_RASBES,:PIN_DH_RGI, ";
        exe_Insert += ":PIN_DH_RCONCREAT,:PIN_DH_COV_FIRE,:PIN_DH_COV_LIGHT,:PIN_DH_COV_FLOOD,:PIN_DH_CFWATERAVL,:PIN_DH_CFYESR1,:PIN_DH_CFYESR2,:PIN_DH_CFYESR3,:PIN_DH_CFYESR4,:PIN_DH_ENTERED_BY,sysdate,:PIN_DH_HOLD,:PIN_DH_NO_OF_FLOORS,:PIN_DH_OVER_VAL,:PIN_DH_FINAL_FLAG,:PIN_ISREQ,:PIN_dh_bcode_id,:PIN_dh_bbrcode_id,:PIN_LAND_PHONE,:PIN_DH_VAL_BANKFAC, ";

        exe_Insert += ":PIN_TERM, :PIN_Period, :PIN_Fire_cover, :PIN_Other_cover, :PIN_SRCC_cover, :PIN_TC_cover, :PIN_Flood_cover, ";
        exe_Insert += ":PIN_PROP_TYPE,:PIN_DH_SOLAR_SUM,:PIN_SOLAR_REPAIRE,:PIN_SOLAR_PARTS,:PIN_SOLAR_ORIGIN,:PIN_SOLAR_SERIAL,:PIN_Solar_Period,:PIN_LOAN_NUMBER,:PIN_SLIC_CODE,:PIN_BPF)";

        

        string ref_val = "select QUOTATION.FIRE_DH_ID_SEQ.nextval from dual";
        string ref_id = or_trn.GetString(ref_val);

        bool result = false;
        Gen_refNO = "";
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
            cmd_backup.CommandText = exe_Insert;


            cmd_backup.Parameters.Add(":PIN_DH_REFNO", OracleType.VarChar).Value = "FFPD/" + ref_id;
            cmd_backup.Parameters.Add(":PIN_DH_BCODE", OracleType.VarChar).Value = BCODE;
            cmd_backup.Parameters.Add(":PIN_DH_BBRCODE", OracleType.VarChar).Value = BBRCODE;
            cmd_backup.Parameters.Add(":PIN_DH_NAME", OracleType.VarChar).Value = NAME;
            cmd_backup.Parameters.Add(":PIN_DH_AGECODE", OracleType.VarChar).Value = AGECODE;

            cmd_backup.Parameters.Add(":PIN_DH_AGENAME", OracleType.VarChar).Value = AGENAME;
            cmd_backup.Parameters.Add(":PIN_DH_NIC", OracleType.VarChar).Value = NIC;
            cmd_backup.Parameters.Add(":PIN_DH_BR", OracleType.VarChar).Value = BR;
            cmd_backup.Parameters.Add(":PIN_DH_PADD1", OracleType.VarChar).Value = PADD1;
            cmd_backup.Parameters.Add(":PIN_DH_PADD2", OracleType.VarChar).Value = PADD2;
            cmd_backup.Parameters.Add(":PIN_DH_PADD3", OracleType.VarChar).Value = PADD3;


            cmd_backup.Parameters.Add(":PIN_DH_PADD4", OracleType.VarChar).Value = PADD4;
            cmd_backup.Parameters.Add(":PIN_DH_PHONE", OracleType.VarChar).Value = PHONE;
            
            cmd_backup.Parameters.Add(":PIN_DH_EMAIL", OracleType.VarChar).Value = EMAIL;
            cmd_backup.Parameters.Add(":PIN_DH_IADD1", OracleType.VarChar).Value = IADD1;
            cmd_backup.Parameters.Add(":PIN_DH_IADD2", OracleType.VarChar).Value = IADD2;
            cmd_backup.Parameters.Add(":PIN_DH_IADD3", OracleType.VarChar).Value = IADD3;
            cmd_backup.Parameters.Add(":PIN_DH_IADD4", OracleType.VarChar).Value = IADD4;
            cmd_backup.Parameters.Add(":PIN_DH_PFROM", OracleType.DateTime).Value = PFROM;
            cmd_backup.Parameters.Add(":PIN_DH_PTO", OracleType.DateTime).Value = PTO;

            
            cmd_backup.Parameters.Add(":PIN_DH_UCONSTR", OracleType.VarChar).Value = UCONSTR;
            cmd_backup.Parameters.Add(":PIN_DH_OCCU_CAR", OracleType.VarChar).Value = OCCU_CAR;
            cmd_backup.Parameters.Add(":PIN_DH_OCC_YES_REAS", OracleType.VarChar).Value = OCC_YES_REAS;
            cmd_backup.Parameters.Add(":PIN_DH_HAZ_OCCU", OracleType.VarChar).Value = HAZ_OCCU;
            cmd_backup.Parameters.Add(":PIN_DH_HAZ_YES_REA", OracleType.VarChar).Value = HAZ_YES_REA;
            cmd_backup.Parameters.Add(":PIN_DH_VALU_BUILD", OracleType.Double).Value = VALU_BUILD;

            cmd_backup.Parameters.Add(":PIN_DH_VALU_WALL", OracleType.Double).Value = VALU_WALL;
            cmd_backup.Parameters.Add(":PIN_DH_VALU_TOTAL", OracleType.Double).Value = VALU_TOTAL;
            cmd_backup.Parameters.Add(":PIN_DH_AFF_FLOOD", OracleType.VarChar).Value = AFF_FLOOD;
            cmd_backup.Parameters.Add(":PIN_DH_AFF_YES_REAS", OracleType.VarChar).Value = AFF_YES_REAS;
            cmd_backup.Parameters.Add(":PIN_DH_WBRICK", OracleType.VarChar).Value = WBRICK;
            cmd_backup.Parameters.Add(":PIN_DH_WCEMENT", OracleType.VarChar).Value = WCEMENT;
            cmd_backup.Parameters.Add(":PIN_DH_DWOODEN", OracleType.VarChar).Value = DWOODEN;
            cmd_backup.Parameters.Add(":PIN_DH_DMETAL", OracleType.VarChar).Value = DMETAL;
           
            cmd_backup.Parameters.Add(":PIN_DH_FTILE", OracleType.VarChar).Value = FTILE;
            cmd_backup.Parameters.Add(":PIN_DH_FCEMENT", OracleType.VarChar).Value = FCEMENT;
            cmd_backup.Parameters.Add(":PIN_DH_RTILE", OracleType.VarChar).Value = RTILE;
            cmd_backup.Parameters.Add(":PIN_DH_RASBES", OracleType.VarChar).Value = RASBES;
            cmd_backup.Parameters.Add(":PIN_DH_RGI", OracleType.VarChar).Value = RGI;
            cmd_backup.Parameters.Add(":PIN_DH_RCONCREAT", OracleType.VarChar).Value = RCONCREAT;
            cmd_backup.Parameters.Add(":PIN_DH_COV_FIRE", OracleType.VarChar).Value = COV_FIRE;
            cmd_backup.Parameters.Add(":PIN_DH_COV_LIGHT", OracleType.VarChar).Value = COV_LIGHT;

            cmd_backup.Parameters.Add(":PIN_DH_COV_FLOOD", OracleType.VarChar).Value = COV_FLOOD;
            cmd_backup.Parameters.Add(":PIN_DH_CFWATERAVL", OracleType.VarChar).Value = CFWATERAVL;
            cmd_backup.Parameters.Add(":PIN_DH_CFYESR1", OracleType.VarChar).Value = CFYESR1;
            cmd_backup.Parameters.Add(":PIN_DH_CFYESR2", OracleType.VarChar).Value = CFYESR2;
            cmd_backup.Parameters.Add(":PIN_DH_CFYESR3", OracleType.VarChar).Value = CFYESR3;
            cmd_backup.Parameters.Add(":PIN_DH_CFYESR4", OracleType.VarChar).Value = CFYESR4;
            cmd_backup.Parameters.Add(":PIN_DH_ENTERED_BY", OracleType.VarChar).Value = ENTERED_BY;
            //cmd_backup.Parameters.Add(":PIN_DH_ENTERED_ON", OracleType.VarChar).Value = ENTERED_ON;

            cmd_backup.Parameters.Add(":PIN_DH_HOLD", OracleType.VarChar).Value = HOLD;
            cmd_backup.Parameters.Add(":PIN_DH_NO_OF_FLOORS", OracleType.Int32).Value = no_of_floor;
            cmd_backup.Parameters.Add(":PIN_DH_OVER_VAL", OracleType.VarChar).Value = over_val;
            cmd_backup.Parameters.Add(":PIN_DH_FINAL_FLAG", OracleType.VarChar).Value = overall_flag;
            cmd_backup.Parameters.Add(":PIN_ISREQ", OracleType.VarChar).Value = isReq;
            cmd_backup.Parameters.Add(":PIN_dh_bcode_id", OracleType.VarChar).Value = bank_Id;
            cmd_backup.Parameters.Add(":PIN_dh_bbrcode_id", OracleType.VarChar).Value = branch_Id;
            cmd_backup.Parameters.Add(":PIN_LAND_PHONE", OracleType.VarChar).Value = LAND_PHONE;
            cmd_backup.Parameters.Add(":PIN_DH_VAL_BANKFAC", OracleType.Double).Value = DH_VAL_BANKFAC;

            cmd_backup.Parameters.Add(":PIN_TERM", OracleType.VarChar).Value = TERM;
            cmd_backup.Parameters.Add(":PIN_Period", OracleType.Double).Value = Period;
            cmd_backup.Parameters.Add(":PIN_Fire_cover", OracleType.VarChar).Value = Fire_cover;
            cmd_backup.Parameters.Add(":PIN_Other_cover", OracleType.VarChar).Value = Other_cover;
            cmd_backup.Parameters.Add(":PIN_SRCC_cover", OracleType.VarChar).Value = SRCC_cover;
            cmd_backup.Parameters.Add(":PIN_TC_cover", OracleType.VarChar).Value = TC_cover;
            cmd_backup.Parameters.Add(":PIN_Flood_cover", OracleType.VarChar).Value = Flood_cover;


            cmd_backup.Parameters.Add(":PIN_PROP_TYPE", OracleType.VarChar).Value = PROP_TYPE;
            cmd_backup.Parameters.Add(":PIN_DH_SOLAR_SUM", OracleType.Double).Value = DH_SOLAR_SUM;
            cmd_backup.Parameters.Add(":PIN_SOLAR_REPAIRE", OracleType.VarChar).Value = SOLAR_REPAIRE;
            cmd_backup.Parameters.Add(":PIN_SOLAR_PARTS", OracleType.VarChar).Value = SOLAR_PARTS;
            cmd_backup.Parameters.Add(":PIN_SOLAR_ORIGIN", OracleType.VarChar).Value = SOLAR_ORIGIN;
            cmd_backup.Parameters.Add(":PIN_SOLAR_SERIAL", OracleType.VarChar).Value = SOLAR_SERIAL;
            cmd_backup.Parameters.Add(":PIN_Solar_Period", OracleType.Double).Value = Solar_Period;
            cmd_backup.Parameters.Add(":PIN_LOAN_NUMBER", OracleType.VarChar).Value = LOAN_NUMBER;
            cmd_backup.Parameters.Add(":PIN_SLIC_CODE", OracleType.VarChar).Value = SLIC_CODE;
            cmd_backup.Parameters.Add(":PIN_BPF", OracleType.VarChar).Value = BPF;


            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {

                result = true;
                Gen_refNO = "FFPD/" + ref_id;


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
    //insert to schedule calculation----------------------->>>

    public bool insert_fire_sche_cal_details


        (string sc_ref_no, string sc_policy_no, double sc_sum_insu, double sc_net_pre, double sc_rcc, double sc_tr, double sc_admin_fee,
        double sc_policy_fee, double sc_nbt, double sc_vat, double sc_total_pay, double calBPF_Value, string created_on, string created_by, string flag,double SC_Renewal_FEE, out string cal_ref)

    {
     string exe_Insert = "insert into QUOTATION.FIRE_DH_SCHEDULE_CALC(sc_ref_no, sc_policy_no, sc_sum_insu, sc_net_pre, sc_rcc, sc_tr, sc_admin_fee, sc_policy_fee, sc_nbt, ";
     exe_Insert += "sc_vat, sc_total_pay, created_on, created_by, flag, SC_Renewal_FEE, BP_FEE) values ";
     exe_Insert += "(:PIN_sc_ref_no,:PIN_sc_policy_no,:PIN_sc_sum_insu,:PIN_sc_net_pre,:PIN_sc_rcc,:PIN_sc_tr,:PIN_sc_admin_fee,:PIN_sc_policy_fee,:PIN_sc_nbt, ";
     exe_Insert += ":PIN_sc_vat,:PIN_sc_total_pay,SYSDATE,:PIN_created_by,:PIN_flag, :PIN_SC_Renewal_FEE, :PIN_BP_FEE)"; 

        

       // string ref_val = "select QUOTATION.FIRE_DH_ID_SEQ.nextval from dual";
        //string ref_id = or_trn.GetString(ref_val);

        bool result = false;
        cal_ref = "";
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
            cmd_backup.CommandText = exe_Insert;

            cmd_backup.Parameters.Add(":PIN_sc_ref_no", OracleType.VarChar).Value = sc_ref_no;
            cmd_backup.Parameters.Add(":PIN_sc_policy_no", OracleType.VarChar).Value = sc_policy_no;
            cmd_backup.Parameters.Add(":PIN_sc_sum_insu", OracleType.Double).Value = sc_sum_insu;
            cmd_backup.Parameters.Add(":PIN_sc_net_pre", OracleType.Double).Value = sc_net_pre;
            cmd_backup.Parameters.Add(":PIN_sc_rcc", OracleType.Double).Value = sc_rcc;
            cmd_backup.Parameters.Add(":PIN_sc_tr", OracleType.Double).Value = sc_tr;
            cmd_backup.Parameters.Add(":PIN_sc_admin_fee", OracleType.Double).Value = sc_admin_fee;
            cmd_backup.Parameters.Add(":PIN_sc_policy_fee", OracleType.Double).Value = sc_policy_fee;
            cmd_backup.Parameters.Add(":PIN_SC_Renewal_FEE", OracleType.Double).Value = SC_Renewal_FEE;
            cmd_backup.Parameters.Add(":PIN_BP_FEE", OracleType.Double).Value = calBPF_Value;

            cmd_backup.Parameters.Add(":PIN_sc_nbt", OracleType.Double).Value = sc_nbt;
            cmd_backup.Parameters.Add(":PIN_sc_vat", OracleType.Double).Value = sc_vat;
            cmd_backup.Parameters.Add(":PIN_sc_total_pay", OracleType.Double).Value = sc_total_pay;
            cmd_backup.Parameters.Add(":PIN_created_by", OracleType.VarChar).Value = created_by;
            //cmd_backup.Parameters.Add(":PIN_DH_ENTERED_ON", OracleType.VarChar).Value = ENTERED_ON;
            cmd_backup.Parameters.Add(":PIN_flag", OracleType.VarChar).Value = flag;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {

                result = true;
                cal_ref = "DONE";


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




    public bool insert_new_seq_for_policyNumber


      (string P_TYPE, string P_YEAR,string P_MONTH, string P_SEQ,string CREATED_ON, string ACTIVE, out string return_result)

    {
        

        string exe_Insert = "insert into QUOTATION.FIRE_POLICY_SEQ(P_TYPE, P_YEAR, P_MONTH, P_SEQ, CREATED_ON, ACTIVE) ";
        exe_Insert += "values(:PIN_P_TYPE,:PIN_P_YEAR,:PIN_P_MONTH,:PIN_P_SEQ,sysdate,:PIN_ACTIVE)";

        // string ref_val = "select QUOTATION.FIRE_DH_ID_SEQ.nextval from dual";
        //string ref_id = or_trn.GetString(ref_val);

        bool result = false;
        return_result = "";
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
            cmd_backup.CommandText = exe_Insert;

            cmd_backup.Parameters.Add(":PIN_P_TYPE", OracleType.VarChar).Value = P_TYPE;
            cmd_backup.Parameters.Add(":PIN_P_YEAR", OracleType.VarChar).Value = P_YEAR;
            cmd_backup.Parameters.Add(":PIN_P_MONTH", OracleType.VarChar).Value = P_MONTH;
            cmd_backup.Parameters.Add(":PIN_P_SEQ", OracleType.VarChar).Value = P_SEQ;
            cmd_backup.Parameters.Add(":PIN_ACTIVE", OracleType.VarChar).Value = ACTIVE;

             cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {

                result = true;
                return_result = "DONE";


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


    //----------->insert history for fire schedule cal

    //insert to schedule calculation----------------------->>>

    public bool insert_fire_sche_cal_details_history


        (string sc_ref_no, string sc_policy_no, double sc_sum_insu, double sc_net_pre, double sc_rcc, double sc_tr, double sc_admin_fee,
        double sc_policy_fee, double sc_nbt, double sc_vat, double sc_total_pay, string created_on, string created_by, string flag, double renewalFee, double bpf, out string cal_ref)

    {
        string exe_Insert = "insert into QUOTATION.FIRE_DH_SCHEDULE_CALC_HISTORY(sc_ref_no, sc_policy_no, sc_sum_insu, sc_net_pre, sc_rcc, sc_tr, sc_admin_fee, sc_policy_fee, sc_nbt, ";
        exe_Insert += "sc_vat, sc_total_pay, created_on, created_by, flag, SC_RENEWAL_FEE, BP_FEE) values ";
        exe_Insert += "(:PIN_sc_ref_no,:PIN_sc_policy_no,:PIN_sc_sum_insu,:PIN_sc_net_pre,:PIN_sc_rcc,:PIN_sc_tr,:PIN_sc_admin_fee,:PIN_sc_policy_fee,:PIN_sc_nbt, ";
        exe_Insert += ":PIN_sc_vat,:PIN_sc_total_pay,SYSDATE,:PIN_created_by,:PIN_flag, :PIN_SC_RENEWAL_FEE, :PIN_BP_FEE)";



        // string ref_val = "select QUOTATION.FIRE_DH_ID_SEQ.nextval from dual";
        //string ref_id = or_trn.GetString(ref_val);

        bool result = false;
        cal_ref = "";
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
            cmd_backup.CommandText = exe_Insert;

            cmd_backup.Parameters.Add(":PIN_sc_ref_no", OracleType.VarChar).Value = sc_ref_no;
            cmd_backup.Parameters.Add(":PIN_sc_policy_no", OracleType.VarChar).Value = sc_policy_no;
            cmd_backup.Parameters.Add(":PIN_sc_sum_insu", OracleType.Double).Value = sc_sum_insu;
            cmd_backup.Parameters.Add(":PIN_sc_net_pre", OracleType.Double).Value = sc_net_pre;
            cmd_backup.Parameters.Add(":PIN_sc_rcc", OracleType.Double).Value = sc_rcc;
            cmd_backup.Parameters.Add(":PIN_sc_tr", OracleType.Double).Value = sc_tr;
            cmd_backup.Parameters.Add(":PIN_sc_admin_fee", OracleType.Double).Value = sc_admin_fee;
            cmd_backup.Parameters.Add(":PIN_sc_policy_fee", OracleType.Double).Value = sc_policy_fee;
            cmd_backup.Parameters.Add(":PIN_sc_nbt", OracleType.Double).Value = sc_nbt;
            cmd_backup.Parameters.Add(":PIN_sc_vat", OracleType.Double).Value = sc_vat;
            cmd_backup.Parameters.Add(":PIN_sc_total_pay", OracleType.Double).Value = sc_total_pay;
            cmd_backup.Parameters.Add(":PIN_created_by", OracleType.VarChar).Value = created_by;
            cmd_backup.Parameters.Add(":PIN_SC_RENEWAL_FEE", OracleType.Double).Value = renewalFee;
            cmd_backup.Parameters.Add(":PIN_BP_FEE", OracleType.Double).Value = bpf;
            cmd_backup.Parameters.Add(":PIN_flag", OracleType.VarChar).Value = flag;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {

                result = true;
                cal_ref = "DONE";


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



    // long term total value table-----
    public bool insert_fire_sche_cal_LongShort_TotalVal


       (string sc_ref_no, string sc_policy_no, double sc_sum_insu, double sc_net_pre, double sc_rcc, double sc_tr, double sc_admin_fee,
       double sc_policy_fee, double sc_nbt, double sc_vat, double sc_total_pay, string created_on, string created_by, string flag, double SC_Renewal_FEE, out string cal_ref)

    {
        string exe_Insert = "insert into QUOTATION.FIRE_SCHEDULE_LONGTERM_TOTAL(sc_ref_no, sc_policy_no, sc_sum_insu, sc_net_pre, sc_rcc, sc_tr, sc_admin_fee, sc_policy_fee, sc_nbt, ";
        exe_Insert += "sc_vat, sc_total_pay, created_on, created_by, flag, SC_Renewal_FEE) values ";
        exe_Insert += "(:PIN_sc_ref_no,:PIN_sc_policy_no,:PIN_sc_sum_insu,:PIN_sc_net_pre,:PIN_sc_rcc,:PIN_sc_tr,:PIN_sc_admin_fee,:PIN_sc_policy_fee,:PIN_sc_nbt, ";
        exe_Insert += ":PIN_sc_vat,:PIN_sc_total_pay,SYSDATE,:PIN_created_by,:PIN_flag, :PIN_SC_Renewal_FEE)";



        // string ref_val = "select QUOTATION.FIRE_DH_ID_SEQ.nextval from dual";
        //string ref_id = or_trn.GetString(ref_val);

        bool result = false;
        cal_ref = "";
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
            cmd_backup.CommandText = exe_Insert;

            cmd_backup.Parameters.Add(":PIN_sc_ref_no", OracleType.VarChar).Value = sc_ref_no;
            cmd_backup.Parameters.Add(":PIN_sc_policy_no", OracleType.VarChar).Value = sc_policy_no;
            cmd_backup.Parameters.Add(":PIN_sc_sum_insu", OracleType.Double).Value = sc_sum_insu;
            cmd_backup.Parameters.Add(":PIN_sc_net_pre", OracleType.Double).Value = sc_net_pre;
            cmd_backup.Parameters.Add(":PIN_sc_rcc", OracleType.Double).Value = sc_rcc;
            cmd_backup.Parameters.Add(":PIN_sc_tr", OracleType.Double).Value = sc_tr;
            cmd_backup.Parameters.Add(":PIN_sc_admin_fee", OracleType.Double).Value = sc_admin_fee;
            cmd_backup.Parameters.Add(":PIN_sc_policy_fee", OracleType.Double).Value = sc_policy_fee;
            cmd_backup.Parameters.Add(":PIN_SC_Renewal_FEE", OracleType.Double).Value = SC_Renewal_FEE;

            cmd_backup.Parameters.Add(":PIN_sc_nbt", OracleType.Double).Value = sc_nbt;
            cmd_backup.Parameters.Add(":PIN_sc_vat", OracleType.Double).Value = sc_vat;
            cmd_backup.Parameters.Add(":PIN_sc_total_pay", OracleType.Double).Value = sc_total_pay;
            cmd_backup.Parameters.Add(":PIN_created_by", OracleType.VarChar).Value = created_by;
            //cmd_backup.Parameters.Add(":PIN_DH_ENTERED_ON", OracleType.VarChar).Value = ENTERED_ON;
            cmd_backup.Parameters.Add(":PIN_flag", OracleType.VarChar).Value = flag;

            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {

                result = true;
                cal_ref = "DONE";


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


    // insert remarks table bank and slic-----
    public bool insertMotorRemarksToWays
       (string refNo, string flag, string createBy, string remarks)
    {
        string exe_Insert = "insert into QUOTATION.BANCASSU_MOTOQUOREMARKS (R_NO, R_REF, R_SLIC, R_CREATED_BY, R_CREATED_ON, R_FLAG) ";
        exe_Insert += "values ";
        exe_Insert += "(:PIN_R_NO, :PIN_R_REF, :PIN_R_SLIC, :PIN_R_CREATED_BY, :PIN_R_CREATED_ON, :PIN_R_FLAG)";


        string req_val = "select QUOTATION.bacassu_motorespo_seq.NEXTVAL from dual";
        int req_id = Convert.ToInt32(or_trn.GetString(req_val));

        // string ref_val = "select QUOTATION.FIRE_DH_ID_SEQ.nextval from dual";
        //string ref_id = or_trn.GetString(ref_val);
        string cal_ref = "";
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
            cmd_backup.CommandText = exe_Insert;

            cmd_backup.Parameters.Add(":PIN_R_NO", OracleType.Int32).Value = req_id;
            cmd_backup.Parameters.Add(":PIN_R_REF", OracleType.VarChar).Value = refNo;
            cmd_backup.Parameters.Add(":PIN_R_SLIC", OracleType.VarChar).Value = remarks;
            cmd_backup.Parameters.Add(":PIN_R_CREATED_BY", OracleType.VarChar).Value = createBy;
            cmd_backup.Parameters.Add(":PIN_R_CREATED_ON", OracleType.DateTime).Value = System.DateTime.Now;
            cmd_backup.Parameters.Add(":PIN_R_FLAG", OracleType.VarChar).Value = flag;


            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {

                result = true;
                cal_ref = "DONE";


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

    // insert response team tickets from bank-------------->>>
    
    public bool insert_ticket_details(string T_REF, string T_BANK_REMARK, string T_BANK_CODE, string T_BRANCH_CODE, string T_CREATED_BY, string T_FLAG, string T_STATUS, string T_B_NAME, string T_BR_NAME,string T_BANK_EMAIL, out string Gen_t_id)
    {
        //string REQ_ID, string BANK_CODE,

        string exe_Insert = "INSERT INTO QUOTATION.BANCASSU_INQU_TICKETS( ";
        exe_Insert += "T_REF,T_BANK_REMARK,T_BANK_CODE,T_BRANCH_CODE,T_CREATED_BY,T_CREATED_ON, T_FLAG,T_STATUS,T_BANK_NAME,T_BRANCH_NAME,T_BANK_EMAIL) ";
        exe_Insert += "VALUES(:PIN_T_REF,:PIN_T_BANK_REMARK,:PIN_T_BANK_CODE,:PIN_T_BRANCH_CODE,:PIN_T_CREATED_BY,:PIN_T_CREATED_ON,:PIN_T_FLAG, :PIN_T_STATUS,:PIN_T_BANK_NAME,:PIN_T_BRANCH_NAME,:PIN_T_BANK_EMAIL)";

        //string req_val = "select QUOTATION.banc_id_seq.nextval from dual";
        //string req_id = or_trn.GetString(req_val);

        bool result = false;
        Gen_t_id = "";
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
            cmd_backup.CommandText = exe_Insert;


            cmd_backup.Parameters.Add(":PIN_T_REF", OracleType.VarChar).Value = T_REF;
            cmd_backup.Parameters.Add(":PIN_T_BANK_REMARK", OracleType.VarChar).Value = T_BANK_REMARK;
            cmd_backup.Parameters.Add(":PIN_T_BANK_CODE", OracleType.VarChar).Value = T_BANK_CODE;
            cmd_backup.Parameters.Add(":PIN_T_BRANCH_CODE", OracleType.VarChar).Value = T_BRANCH_CODE;
            cmd_backup.Parameters.Add(":PIN_T_CREATED_BY", OracleType.VarChar).Value = T_CREATED_BY;
            cmd_backup.Parameters.Add(":PIN_T_CREATED_ON", OracleType.DateTime).Value = System.DateTime.Now;
            cmd_backup.Parameters.Add(":PIN_T_FLAG", OracleType.VarChar).Value = T_FLAG;
            cmd_backup.Parameters.Add(":PIN_T_STATUS", OracleType.VarChar).Value = T_STATUS;

            cmd_backup.Parameters.Add(":PIN_T_BANK_NAME", OracleType.VarChar).Value = T_B_NAME;
            cmd_backup.Parameters.Add(":PIN_T_BRANCH_NAME", OracleType.VarChar).Value = T_BR_NAME;
            cmd_backup.Parameters.Add(":PIN_T_BANK_EMAIL", OracleType.VarChar).Value = T_BANK_EMAIL;
            
            cmd_backup.Transaction = transaction;
            int execution = cmd_backup.ExecuteNonQuery();


            if (execution > 0)
            {

                result = true;
                Gen_t_id = T_REF;


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