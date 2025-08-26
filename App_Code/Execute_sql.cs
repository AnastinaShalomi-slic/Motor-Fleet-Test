using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Activities.Expressions;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Execute_sql
/// </summary>
public class Execute_sql
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]);
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    ORCL_Connection orcl_con = new ORCL_Connection();
    public string GetBranch(int brcode)
    {
        string sql = string.Empty;

        sql = "SELECT BRCOD, BRNAM FROM genpay.branch ";
        if (brcode == 10)
        {
            sql += "WHERE BRCOD != 0 ";
        }
        else
        {
            sql += "WHERE BRCOD = " + brcode + " ";
            sql += "AND BRCOD != 0 ";
        }
        sql += "GROUP BY BRCOD, BRNAM ";
        sql += "ORDER BY BRNAM ASC";
        return sql;
    }

    public string GetVMake()
    {
        string sql = string.Empty;
        sql = "select VH_MAKE, MAKE_ID ";
        sql += "from SLIC_CNOTE.VH_MAKES order by VH_MAKE asc";
        return sql;
    }

    public string GetVModel(string mID)
    {
        string sql = string.Empty;

        sql = "SELECT MODEL_NAME, MODEL_ID ";
        sql += "FROM SLIC_CNOTE.VH_MODEL ";
        sql += "WHERE MAKE_ID = '" + mID + "' ";
        sql += "ORDER BY MODEL_NAME ASC";

        return sql;
    }

    public string GetReqDetails(string start_date, string end_date, string req_id, string status, string bank, string branch, string user)
    {
        string sql = string.Empty;
        sql = " select bd.REQ_ID, bd.BANK_CODE, bd.V_TYPE, bd.YOM, bd.SUM_INSU, bd.V_MAKE, bd.V_MODEL, bd.PURPOSE, bd.V_REG_NO, bd.CUS_NAME, ";
        sql += "bd.CUS_PHONE, bd.V_FUEL, bd.ENTERED_BY, to_char(bd.ENTERED_ON,'dd/mm/yyyy') as ENTERED_ON , ";

        sql += "case bd.FLAG ";
        sql += "when 'P' then 'Pending' ";
        sql += "when 'D' then 'Completed' ";
        sql += "when 'M' then 'Need more Info.' ";
        sql += "when 'C' then 'Cancel' ";
        sql += "when 'R' then 'Reject' ";
        sql += "when 'RM' then 'Forward to Risk Managment' ";
        sql += "when 'RI' then 'Forward to Reinsurance' ";
        sql += "end as FLAG, ";
        sql += "mk.vh_make, ";
        sql += "case bd.V_MODEL ";
        sql += "when 0 then bd.other_model ";
        sql += "else ";
        sql += "md.model_name ";
        sql += "end as model_name, ";

        //md.model_name,
        sql += "bd.email, bd.QUO_NO, bn.bbnam ,bn.bbrnch, bd.banc_email, bd.SLIC_REMARK ";

        sql += "from QUOTATION.BANK_REQ_ENTRY_DETAILS bd ";
        sql += "inner join SLIC_CNOTE.VH_MAKES mk ";
        sql += "on mk.make_id = bd.v_make ";
        sql += "inner join SLIC_CNOTE.VH_MODEL md ";
        sql += "on bd.v_model = md.model_id ";
        sql += "inner join GENPAY.BNKBRN bn ";
        sql += "on bn.bbcode = bd.branch_code and ";
        sql += "bn.bcode = bd.bank_code ";
        sql += "where bd.flag !='Z' ";
        if (!string.IsNullOrEmpty(req_id))
        {
            sql += "and bd.req_id= '" + req_id + "' ";
        }

        if (!string.IsNullOrEmpty(bank) && bank != "0")
        {
            sql += "and bd.BANK_CODE= '" + bank + "' ";
        }

        if (!string.IsNullOrEmpty(branch) && branch != "0")
        {
            sql += "and bd.BRANCH_CODE= '" + branch + "' ";
        }

        //if (!string.IsNullOrEmpty(user) && user != "A" && (user.ToString().Substring(user.ToString().Length - 3).ToLower() == "_su"))
        //{
        //    sql += "and bd.ENTERED_BY= '" + user + "' "; // FOR SUPER USER REASON THIS COMMENT
        //}
        if (!string.IsNullOrEmpty(status) && status != "A")
        {
            sql += "and bd.flag = '" + status + "' ";
        }
        if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
        {
            sql += "and(to_date(to_char(bd.entered_on,'dd/MM/yyyy'),'dd/MM/yyyy') > = to_date('" + start_date + "','dd/MM/yyyy') ";
            sql += "and to_date(to_char(bd.entered_on,'dd/MM/yyyy'),'dd/MM/yyyy') <= to_date('" + end_date + "','dd/MM/yyyy')) ";
        }
        sql += "order by bd.ENTERED_ON desc";
        return sql;
    }

    public string GetTimeDuration(string App_date)
    {
        string sql = string.Empty;

        sql = "SELECT TRUNC(difference) AS days, ";
        sql += "TRUNC( MOD(difference * 24, 24) ) AS hours, ";
        sql += "TRUNC( MOD(difference * 24 * 60, 60) ) AS minutes, ";
        sql += "TRUNC( MOD(difference * 24 * 60 * 60, 60) ) AS seconds ";
        sql += "FROM( ";
        sql += "SELECT TO_DATE(to_char(sysdate,'DD-MM-YYYY HH24:MI:SS'), 'DD-MM-YYYY HH24:MI:SS') - ";
        sql += "TO_DATE('" + App_date + "', 'DD-MM-YYYY HH24:MI:SS') ";
        sql += "AS difference ";
        sql += "FROM DUAL)";

        return sql;
    }

    public string GetBankCodes()
    {
        string sql = string.Empty;

        sql = "select distinct(br.bcode) as bcode, trim(Upper(br.bbnam)) as bbnam ";
        sql += "from GENPAY.BNKBRN br ";
        sql += "group by bbnam, bcode ";
        sql += "order by bbnam asc";
        return sql;
    }

    public string GetBankCodesOneItem(string code)
    {
        string sql = string.Empty;

        sql = "select distinct(br.bcode) as bcode, trim(Upper(br.bbnam)) as bbnam ";
        sql += "from GENPAY.BNKBRN br ";
        sql += "where br.bcode = '" + code + "' ";
        sql += "group by bbnam, bcode ";
        sql += "order by bbnam asc";
        return sql;
    }

    public string GetBranch(string bank)
    {
        string sql = string.Empty;

        sql = "select br.bbrnch, br.bbcode ";
        sql += "from GENPAY.BNKBRN br ";
        sql += "where br.bcode = '" + bank + "' ";
        sql += "group by br.bbrnch, br.bbcode ";
        sql += "order by br.bbrnch asc";

        return sql;
    }

    public string GetOfficer(string bank)
    {
        string sql = string.Empty;
        sql = "select EPF, BANK_CODE, BANK_NAME, OFFICER_NAME, DESIGNATION, CONTACT_NO, EMAIL ";
        sql += "from QUOTATION.BANK_SALES_OFFICER ";
        sql += "where BANK_CODE = '" + bank + "' ";
        sql += "and trim(DESIGNATION) = trim('Sales Officer') ";
        sql += "and flag = 'A'";
        return sql;
    }

    public string GetunderWriter(string bank)
    {
        string sql = string.Empty;
        sql = "select EPF, BANK_CODE, BANK_NAME, OFFICER_NAME, DESIGNATION, CONTACT_NO, EMAIL ";
        sql += "from QUOTATION.BANK_SALES_OFFICER ";
        sql += "where BANK_CODE = '" + bank + "' ";
        sql += "and trim(DESIGNATION) = trim('Underwriter') ";
        sql += "and flag = 'A'";
        return sql;
    }

    public string GetFireRate(string bank, string term)
    {
        string sql = string.Empty;
        sql = "SELECT BANK, BASIC, RCC, TR, ADMIN_FEE, POLICY_FEE, Renewal_FEE,BASIC_2,TERM ";
        sql += "FROM QUOTATION.BANCASU_RATE ";
        sql += "WHERE D_TYPE = 'PD' AND DEPARTMENT = 'F' ";
        sql += "AND BANK_CODE = '" + bank + "' ";
        sql += "AND TERM = '" + term + "' ";
        sql += "AND EFFECTIVE_DATE <= SYSDATE";
        return sql;
    }

    //get discount rates 08092021-----
    public string GetDiscountRate(string bank, double noOfYears)
    {
        string sql = string.Empty;
        sql = "select tx.rate from QUOTATION.DISCOUNT_RATE tx ";
        sql += "where tx.bank_code = '" + bank + "' ";
        sql += "and tx.no_of_years = " + noOfYears + "";
        return sql;
    }

    //--------------------------------

    public string GetFireCovers()
    {
        string sql = string.Empty;
        sql = "SELECT DH_SCOPE_WORD ";
        sql += "FROM QUOTATION.FIRE_DH_SCOPE_OF_COVERS ";
        sql += "WHERE ACTIVE = 'Y' ";
        sql += "ORDER BY SEQ_ID ASC";
        return sql;
    }

    public string GetFireDetails(string start_date, string end_date, string ref_id, string policy_no, string status1, string status2, string status3, string bank, string branch, string user, string nIc, string propTerm, string propType)
    {
        string sql = string.Empty;
        sql = "SELECT PE.DH_REFNO,PE.DH_BCODE,PE.DH_BBRCODE,PE.DH_NAME,PE.DH_AGECODE,PE.DH_AGENAME,upper(PE.DH_NIC),PE.DH_BR,PE.DH_PADD1,PE.DH_PADD2,PE.DH_PADD3,PE.DH_PADD4, ";
        sql += "PE.DH_PHONE,PE.DH_EMAIL,PE.DH_IADD1,PE.DH_IADD2,PE.DH_IADD3,PE.DH_IADD4, to_char(PE.DH_PFROM,'dd/mm/yyyy') as fromDate,to_char(PE.DH_PTO,'dd/mm/yyyy') as toDate,PE.DH_UCONSTR,PE.DH_OCCU_CAR, ";
        sql += "PE.DH_OCC_YES_REAS,PE.DH_HAZ_OCCU,PE.DH_HAZ_YES_REA,PE.DH_VALU_BUILD,PE.DH_VALU_WALL,PE.DH_VALU_TOTAL,PE.DH_AFF_FLOOD,PE.DH_AFF_YES_REAS,PE.DH_WBRICK,PE.DH_WCEMENT,PE.DH_DWOODEN,PE.DH_DMETAL,PE.DH_FTILE, ";
        sql += "PE.DH_FCEMENT,PE.DH_RTILE,PE.DH_RASBES ,PE.DH_RGI,PE.DH_RCONCREAT,PE.DH_COV_FIRE,PE.DH_COV_LIGHT,PE.DH_COV_FLOOD, ";
        sql += "PE.DH_CFWATERAVL,PE.DH_CFYESR1,PE.DH_CFYESR2,PE.DH_CFYESR3,PE.DH_CFYESR4,PE.DH_ENTERED_BY,to_char(PE.DH_ENTERED_ON,'dd/mm/yyyy') as Entered_on,PE.DH_HOLD,PE.DH_NO_OF_FLOORS,PE.DH_OVER_VAL, ";

        sql += "case PE.DH_FINAL_FLAG ";
        sql += "when 'N' then 'Completed' ";
        sql += "when 'Y' then 'Pending' ";
        sql += "when 'R' then 'Rejected' ";
        sql += "end as DH_FINAL_FLAG, ";

        sql += "PE.dh_isreq,PE.dh_conditions,PE.dh_isreject,PE.dh_iscodi,PE.dh_bcode_id,PE.dh_bbrcode_id, ";
        //---long term nic prop type --100120222-------NSB Changes loan number---
        sql += "PE.Period, PE.LOAN_NUMBER, ";
        sql += "upper(PE.DH_NIC) as DH_NIC, ";

        sql += "case PE.TERM ";
        sql += "when '1' then 'Annual ' ";
        sql += "when '0' then 'Long Term' ";
        sql += "end as TERM, ";

        sql += "case PE.PROP_TYPE ";
        sql += "when '1' then 'Private Dwelling House Only' ";
        sql += "when '2' then 'Private Dwelling House and Solar Panel' ";
        sql += "when '3' then 'Solar Panel Only' ";
        sql += "end as PROP_TYPE, PE.SLIC_CODE, PE.BPF, ";
        //------------------------------>>>>>>>>>>>>>>>>>>>
        sql += "SC.SC_REF_NO,SC.SC_POLICY_NO,SC.SC_SUM_INSU,SC.SC_NET_PRE,SC.SC_RCC, ";
        sql += "SC.SC_TR,SC.SC_ADMIN_FEE,SC.SC_POLICY_FEE,SC.SC_NBT,SC.SC_VAT,SC.SC_TOTAL_PAY,SC.CREATED_ON,SC.CREATED_BY,SC.FLAG,SC.DEBIT_NO ";
        sql += "FROM ";

        sql += "QUOTATION.FIRE_DH_PROPOSAL_ENTRY PE ";
        sql += "INNER JOIN QUOTATION.FIRE_DH_SCHEDULE_CALC SC ";
        sql += "ON PE.DH_REFNO = SC.SC_REF_NO ";
        sql += "WHERE PE.DH_REFNO IS NOT NULL ";
        sql += "and DH_ISREQ = 'Y' ";

        if (!string.IsNullOrEmpty(ref_id))
        {
            sql += "and PE.DH_REFNO= '" + ref_id + "' ";
        }
        if (!string.IsNullOrEmpty(policy_no))
        {
            sql += "and SC.SC_POLICY_NO= '" + policy_no + "' ";
        }

        if (!string.IsNullOrEmpty(nIc))
        {
            sql += "and upper(PE.DH_NIC)= upper('" + nIc + "') ";
        }

        //changes--22032022--according to NSB
        if (!string.IsNullOrEmpty(propTerm) && propTerm != "A")
        {
            sql += "and PE.TERM = '" + propTerm + "' ";
        }

        if (!string.IsNullOrEmpty(propType) && propType != "A")
        {
            sql += "and PE.PROP_TYPE = '" + propType + "' ";
        }

        if (!string.IsNullOrEmpty(bank) && bank != "0")
        {
            sql += "and PE.dh_bcode_id= '" + bank + "' ";
        }

        if (!string.IsNullOrEmpty(branch) && branch != "0")
        {
            sql += "and PE.dh_bbrcode_id= '" + branch + "' ";
        }

        if (!string.IsNullOrEmpty(status1) && status1 != "A")
        {
            sql += "and PE.DH_HOLD = '" + status1 + "' ";
        }

        if (!string.IsNullOrEmpty(status2) && status2 != "A")
        {
            sql += "and PE.DH_OVER_VAL = '" + status2 + "' ";
        }
        if (!string.IsNullOrEmpty(status3) && status3 != "A")
        {
            sql += "and PE.DH_FINAL_FLAG = '" + status3 + "' ";
        }

        if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
        {
            sql += "and(to_date(to_char(PE.DH_ENTERED_ON,'dd/MM/yyyy'),'dd/MM/yyyy') >= to_date('" + start_date + "','dd/MM/yyyy') ";
            sql += "and to_date(to_char(PE.DH_ENTERED_ON,'dd/MM/yyyy'),'dd/MM/yyyy') <= to_date('" + end_date + "','dd/MM/yyyy')) ";
        }

        sql += "order by PE.DH_ENTERED_ON desc";
        return sql;
    }


    //--------------------- RENEWAL ----------------------------------------------------------------

    public string GetFireRenewalDetails(string start_date, string end_date, string ref_id, string policy_no, string bank, string branch, string user, string nIc)
    {
        string sql = @"
        SELECT 
            PE.DH_REFNO,PE.DH_PTO, PE.DH_BCODE, PE.DH_BBRCODE, PE.DH_NAME, PE.DH_AGECODE,
            PE.DH_AGENAME, UPPER(PE.DH_NIC) AS DH_NIC, PE.DH_BR, PE.DH_PADD1, PE.DH_PADD2,
            PE.DH_PADD3, PE.DH_PADD4, PE.DH_PHONE, PE.DH_EMAIL, PE.DH_IADD1, PE.DH_IADD2, 
            PE.DH_IADD3, PE.DH_IADD4, TO_CHAR(PE.DH_PFROM, 'dd/mm/yyyy') AS fromDate,
            TO_CHAR(PE.DH_PTO, 'dd/mm/yyyy') AS toDate, PE.DH_VALU_TOTAL, PE.DH_ENTERED_BY, 
            TO_CHAR(PE.DH_ENTERED_ON, 'dd/mm/yyyy') AS Entered_on,

            SC.SC_REF_NO, SC.SC_POLICY_NO, SC.SC_SUM_INSU, SC.SC_NET_PRE, SC.SC_RCC, 
            SC.SC_TR, SC.SC_ADMIN_FEE, SC.SC_POLICY_FEE, SC.SC_NBT, SC.SC_VAT, 
            SC.SC_TOTAL_PAY, SC.CREATED_ON, SC.CREATED_BY, SC.FLAG, SC.DEBIT_NO

            FROM QUOTATION.FIRE_DH_PROPOSAL_ENTRY PE
            INNER JOIN QUOTATION.FIRE_DH_SCHEDULE_CALC SC
            ON PE.DH_REFNO = SC.SC_REF_NO
            WHERE PE.DH_REFNO IS NOT NULL
            AND PE.DH_ISREQ = 'Y'";

        // Dynamically add WHERE conditions
        if (!string.IsNullOrEmpty(policy_no))
            sql += " AND SC.SC_POLICY_NO = '" + policy_no + "'";

        if (!string.IsNullOrEmpty(nIc))
            sql += " AND UPPER(PE.DH_NIC) = UPPER('" + nIc + "')";


        if (!string.IsNullOrEmpty(bank) && bank != "0")
        {
            sql += "and PE.dh_bcode_id= '" + bank + "' ";
        }

        if (!string.IsNullOrEmpty(branch) && branch != "0")
        {
            sql += "and PE.dh_bbrcode_id= '" + branch + "' ";
        }

        if (!string.IsNullOrEmpty(start_date))
            sql += " AND PE.DH_PTO >= TO_DATE('" + start_date + "', 'dd/MM/yyyy')";

        if (!string.IsNullOrEmpty(end_date))
            sql += " AND PE.DH_PTO <= TO_DATE('" + end_date + "', 'dd/MM/yyyy')";

        sql += " ORDER BY PE.DH_PTO ";

        return sql;

    }

    public string GetSumHis(string polno)
    {

        string sql = "SELECT TO_CHAR(pmpdt, 'DD/MM/YYYY') AS formatted_date, pmsum FROM( SELECT pmpdt, pmsum FROM genpay.payfle WHERE pmpol = '" + polno + "' ORDER BY pmpdt DESC)WHERE ROWNUM <= 3";

        return sql;
    }

    public string InsertIntoRenwalMasterTemp(string dept, string type, string policyno, string year, string month, string startDate, string endDate, int gcd, double net, double rcc, double tc, double polfee, double vat, double nbt, double total,
                                             string name, string add1, string add2, string add3, string add4, string nic, string cont, string refno, int fbr, string adminfee, string entryDate, string enteredBy, string enteredIp, string branchCode)
    {

        string SQL = "INSERT INTO SLIC_CNOTE.RENEWAL_MASTER_TEMP ( " +
                     "RNDEPT, RNPTP, RNPOL, RNYR, RNMNTH, RNSTDT, RNENDT, RNAGCD, RNNET, RNRCC, RNTC, " +
                     "RNPOLFEE, RNVAT, RNNBT, RNTOT, RNNAM, RNADD1, RNADD2, RNADD3, RNADD4, " +
                     "RNNIC, RNCNT, RNREF, RNFBR, RN_ADMINFEE, RNDATE, RN_BY, RN_IP, RN_BRCD ) " +
                     "VALUES ('" + dept + "', '" + type + "', '" + policyno + "', '" + year + "', '" + month + "', " +
                     "TO_DATE('" + startDate + "', 'YYYY-MM-DD'), TO_DATE('" + endDate + "', 'YYYY-MM-DD'), " +
                     gcd + ", " + net + ", " + rcc + ", " + tc + ", " + polfee + ", " + vat + ", " + nbt + ", " + total + ", " +
                     "'" + name + "', '" + add1 + "', '" + add2 + "', '" + add3 + "', '" + add4 + "', " +
                     "'" + nic + "', '" + cont + "', '" + refno + "', " + fbr + ", " + adminfee + ", " +
                     "TO_DATE('" + entryDate + "', 'YYYY-MM-DD'), '" + enteredBy + "', '" + enteredIp + "', '" + branchCode + "')";
        return SQL;
    }

    public string GetApprovalTempData()
    {
        string SQL = "SELECT * FROM  SLIC_CNOTE.RENEWAL_MASTER_TEMP where rnptp = 'FD_RENEWAL'";

        return SQL;
    }

    public string GetApprovalTempData(string polno)
    {
        string SQL = "SSELECT * FROM  SLIC_CNOTE.RENEWAL_MASTER_TEMP WHERE RNPOL = '" + polno + "'";

        return SQL;
    }



    public string InsertIntoRenwalMaster(string policyNo)
    {
        string SQL = "INSERT INTO SLIC_CNOTE.RENEWAL_MASTER (RNDEPT, RNPTP, RNPOL, RNYR, RNMNTH, RNSTDT, RNENDT, RNAGCD, " +
                                "RNNET, RNRCC, RNTC, RNPOLFEE, RNVAT, RNNBT, RNTOT, RNNAM, RNADD1, RNADD2, RNADD3, RNADD4, " +
                                "RNNIC, RNCNT, RNREF, RNFBR, RN_ADMINFEE, RNDATE, RN_BY, RN_IP, RN_BRCD) " +
                                "SELECT RNDEPT, RNPTP, RNPOL, RNYR, RNMNTH, RNSTDT, RNENDT, RNAGCD, " +
                                "RNNET, RNRCC, RNTC, RNPOLFEE, RNVAT, RNNBT, RNTOT, RNNAM, RNADD1, RNADD2, RNADD3, RNADD4, " +
                                "RNNIC, RNCNT, RNREF, RNFBR, RN_ADMINFEE, RNDATE, RN_BY, RN_IP, RN_BRCD " +
                                "FROM SLIC_CNOTE.RENEWAL_MASTER_TEMP WHERE RNPOL = '" + policyNo + "'";

        return SQL;
    }


    public string DelteFromRenewalTemp(string policyNo)
    {
        string SQL = "DELETE FROM SLIC_CNOTE.RENEWAL_MASTER_TEMP WHERE RNPOL = '" + policyNo + "'";

        return SQL;
    }


    public string GetRenewedPolicyDetails(string start_date, string end_date, string policy_no, string user, string nIc)
    {
        string SQL = @"
        SELECT 
            RNDEPT, RNPTP, RNPOL, RNYR, RNMNTH, 
            TO_CHAR(RNSTDT, 'dd/mm/yyyy') AS RNSTDT, 
            TO_CHAR(RNENDT, 'dd/mm/yyyy') AS RNENDT, 
            RNAGCD, RNNET, RNRCC, RNTC, 
            RNPOLFEE, RNVAT, RNNBT, RNTOT, 
            RNNAM, RNADD1, RNADD2, RNADD3, RNADD4, 
            RNNIC, RNCNT, RNREF, RNFBR, 
            RN_ADMINFEE, TO_CHAR(RNDATE, 'dd/mm/yyyy') AS RNDATE
        FROM SLIC_CNOTE.RENEWAL_MASTER
        WHERE rnpol LIKE 'F%'";

        if (!string.IsNullOrEmpty(start_date))
        {
            SQL += " AND RNSTDT >= TO_DATE('" + start_date + "', 'dd/mm/yyyy')";

        }

        if (!string.IsNullOrEmpty(end_date))
        {
            SQL += " AND RNENDT <= TO_DATE('" + end_date + "', 'dd/mm/yyyy')";
        }



        if (!string.IsNullOrEmpty(policy_no))
        {
            SQL += " AND RNPOL = '" + policy_no + "'";

        }



        if (!string.IsNullOrEmpty(user))
        {
            SQL += " AND RN_BY = '" + user + "'";
        }


        if (!string.IsNullOrEmpty(nIc))
        {
            SQL += " AND RNNIC = '" + nIc + "'";
        }


        return SQL;
    }


    //-----------------------------------------------------------------------------------




    public string GetAgentDetails(string bank)
    {
        string sql = string.Empty;
        sql = "SELECT AGENCY_CODE, AGE_NAME, BANCASS_GI, BANK_ACC, BANK_EMAIL FROM QUOTATION.FIRE_AGENT_INFO ";
        sql += "WHERE ACTIVE = 'Y' ";
        sql += "AND BANK_CODE = '" + bank + "'";
        return sql;
    }

    public string GetPolSeq(string year, string month, string polType)
    {
        string sql = string.Empty;

        sql = "SELECT P_SEQ FROM QUOTATION.FIRE_POLICY_SEQ ";
        sql += "WHERE ACTIVE = 'Y' ";
        sql += "AND P_TYPE = '" + polType + "' ";
        sql += "AND P_YEAR = '" + year + "'";
        return sql;
    }

    public string GetBankCodebyAgent(string agent)
    {
        string sql = string.Empty;
        sql = "SELECT BANK_CODE, AGE_NAME, BANCASS_GI,BANK_ACC, BANK_EMAIL FROM QUOTATION.FIRE_AGENT_INFO ";
        sql += "WHERE ACTIVE = 'Y' ";
        sql += "AND AGENCY_CODE = '" + agent + "'";
        return sql;
    }

    public string GetOfficerEmail(string bankcode)
    {
        string sql = string.Empty;
        sql = "SELECT EMAIL FROM QUOTATION.FIRE_DH_CONTACT_EMAILS ";
        sql += "WHERE BANK_CODE = '" + bankcode + "' ";
        sql += "AND FLAG = 'A' ";
        sql += "AND EMAIL_CC = 'N'";
        return sql;
    }

    public string GetOfficerEmailForCC(string bankcode)
    {
        string sql = string.Empty;
        sql = "SELECT EMAIL FROM QUOTATION.FIRE_DH_CONTACT_EMAILS ";
        sql += "WHERE BANK_CODE = '" + bankcode + "' ";
        sql += "AND FLAG = 'A' ";
        sql += "AND EMAIL_CC = 'Y'";
        return sql;
    }

    public string GetOfficerEmailAll(string bankcode)
    {
        string sql = string.Empty;
        sql = "SELECT EMAIL FROM QUOTATION.FIRE_DH_CONTACT_EMAILS ";
        sql += "WHERE BANK_CODE = '" + bankcode + "' ";
        sql += "AND FLAG = 'A'";
        return sql;
    }

    public string GetDistrict()
    {
        string sql = string.Empty;
        sql = "select district_id, description from SLISCHOOL.District ";
        sql += "where is_active = 'Y' ";
        sql += "order by description asc";
        return sql;
    }

    //<admin panel>
    public string GetTotalPremiums(string start_date, string end_date, string status, string bank, string branch, string user, string propTerm, string propType)
    {
        string sql = string.Empty;

        sql = "select pe.dh_bcode, pe.dh_bbrcode,count(*) as dh_count,sum(sc.sc_total_pay) as total, ";
        sql += "to_char(pe.dh_entered_on, 'dd/mm/yyyy') as created_date ";
        sql += "from QUOTATION.FIRE_DH_PROPOSAL_ENTRY pe ";
        sql += "inner join QUOTATION.FIRE_DH_SCHEDULE_CALC sc ";
        sql += "on pe.dh_refno = sc.sc_ref_no ";
        sql += "where pe.dh_refno is not null ";
        sql += "and pe.DH_ISREQ = 'Y' ";

        //if (!string.IsNullOrEmpty(status) && status == "N")
        //{
        //    sql += "and sc.sc_policy_no is not null ";
        //} //completed
        //else if (!string.IsNullOrEmpty(status) && status == "Y") { sql += "and sc.sc_policy_no is null and pe.dh_isreject is null "; } // pending
        //else if (!string.IsNullOrEmpty(status) && status == "R") { sql += "and sc.sc_policy_no is null and pe.dh_isreject ='Y' "; } // reject

        //else { }

        if (!string.IsNullOrEmpty(status) && status != "A")
        {
            sql += "and PE.DH_FINAL_FLAG = '" + status + "' ";
        }
        //changes--22032022--according to NSB
        if (!string.IsNullOrEmpty(propTerm) && propTerm != "A")
        {
            sql += "and PE.TERM = '" + propTerm + "' ";
        }

        if (!string.IsNullOrEmpty(propType) && propType != "A")
        {
            sql += "and PE.PROP_TYPE = '" + propType + "' ";
        }
        //--->>

        if (!string.IsNullOrEmpty(bank) && bank != "0")
        {
            sql += "and pe.dh_bcode_id = '" + bank + "' ";
        }

        if (!string.IsNullOrEmpty(branch) && branch != "0")
        {
            sql += "and pe.dh_bbrcode_id = '" + branch + "' ";
        }
        if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
        {
            sql += "and(trunc(pe.dh_entered_on) >= to_date('" + start_date + "','dd/MM/yyyy')  and trunc(pe.dh_entered_on) <= to_date('" + end_date + "', 'dd/MM/yyyy')) ";
        }
        sql += "group by pe.dh_bcode, pe.dh_bbrcode,to_char(pe.dh_entered_on,'dd/mm/yyyy') ";
        sql += "order by  to_date(to_char(pe.dh_entered_on, 'dd/mm/yyyy'), 'dd/mm/yyyy') desc";
        return sql;
    }

    // total count and total sum-------------------------------------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public string GetTotalPremiumsCounts(string start_date, string end_date, string status, string bank, string branch, string user, string propTerm, string propType)
    {
        string sql = string.Empty;

        sql = "select count(*) as dh_count,sum(sc.sc_total_pay) as total ";
        sql += "from QUOTATION.FIRE_DH_PROPOSAL_ENTRY pe ";
        sql += "inner join QUOTATION.FIRE_DH_SCHEDULE_CALC sc ";
        sql += "on pe.dh_refno = sc.sc_ref_no ";
        sql += "where pe.dh_refno is not null ";
        sql += "and pe.DH_ISREQ = 'Y' ";

        //if (!string.IsNullOrEmpty(status) && status == "N")
        //{
        //    sql += "and sc.sc_policy_no is not null ";
        //} //completed
        //else if (!string.IsNullOrEmpty(status) && status == "Y") { sql += "and sc.sc_policy_no is null and pe.dh_isreject is null "; } // pending
        //else if (!string.IsNullOrEmpty(status) && status == "R") { sql += "and sc.sc_policy_no is null and pe.dh_isreject ='Y' "; } // reject

        //else { }

        if (!string.IsNullOrEmpty(status) && status != "A")
        {
            sql += "and PE.DH_FINAL_FLAG = '" + status + "' ";
        }
        //changes--22032022--according to NSB
        if (!string.IsNullOrEmpty(propTerm) && propTerm != "A")
        {
            sql += "and PE.TERM = '" + propTerm + "' ";
        }

        if (!string.IsNullOrEmpty(propType) && propType != "A")
        {
            sql += "and PE.PROP_TYPE = '" + propType + "' ";
        }
        //--->>

        if (!string.IsNullOrEmpty(bank) && bank != "0")
        {
            sql += "and pe.dh_bcode_id = '" + bank + "' ";
        }

        if (!string.IsNullOrEmpty(branch) && branch != "0")
        {
            sql += "and pe.dh_bbrcode_id = '" + branch + "' ";
        }
        if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
        {
            sql += "and(trunc(pe.dh_entered_on) >= to_date('" + start_date + "','dd/MM/yyyy')  and trunc(pe.dh_entered_on) <= to_date('" + end_date + "', 'dd/MM/yyyy'))";
        }
        //sql += "group by pe.dh_bcode, pe.dh_bbrcode,to_char(pe.dh_entered_on,'dd/mm/yyyy') ";
        //sql += "order by  to_date(to_char(pe.dh_entered_on, 'dd/mm/yyyy'), 'dd/mm/yyyy') desc";
        return sql;
    }

    //-- fire end---------------->
    //motor admin --------------->
    public string GetMotorTotalPremiums(string start_date, string end_date, string status, string bank, string branch, string user)
    {
        string sql = string.Empty;
        sql = "select ";
        sql += "gp.bbnam, gp.bbrnch, re.bank_code, re.branch_code, count(*) as total_count, sum(t.qtotal_prm) as quo_pre, sum(re.sum_insu) as sum_insu, ";
        sql += "re.V_TYPE, re.YOM, re.V_MAKE, re.V_MODEL, re.PURPOSE, ";
        sql += "to_char(re.entered_on, 'dd/mm/yyyy') as created_date ";
        sql += "from QUOTATION.BANK_REQ_ENTRY_DETAILS re ";
        sql += "inner join ";
        sql += "QUOTATION.Issued_Quotations t ";
        sql += "on re.quo_no = t.qref_no ";
        sql += "inner join GENPAY.BNKBRN gp ";
        sql += "on to_char(gp.bcode) = re.bank_code ";
        sql += "and to_char(gp.bbcode) = re.branch_code ";
        sql += "where re.quo_no is not null ";
        if (!string.IsNullOrEmpty(status) && status == "A")
        {
            sql += "and re.flag = '" + status + "' ";
        }
        if (!string.IsNullOrEmpty(bank) && bank != "0")
        {
            sql += "and re.bank_code = '" + bank + "' ";
        }
        if (!string.IsNullOrEmpty(branch) && branch != "0")
        {
            sql += "and re.branch_code = '" + branch + "' ";
        }
        if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
        {
            sql += "and(trunc(re.entered_on) >= to_date('" + start_date + "', 'dd/MM/yyyy')  and trunc(re.entered_on) <= to_date('" + end_date + "', 'dd/MM/yyyy')) ";
        }
        sql += "group by re.bank_code, re.branch_code,gp.bbnam,gp.bbrnch,to_char(re.entered_on,'dd/mm/yyyy'),re.V_TYPE, re.YOM, re.V_MAKE, re.V_MODEL, re.PURPOSE ";
        sql += "order by  to_date(to_char(re.entered_on, 'dd/mm/yyyy'), 'dd/mm/yyyy') desc ";
        return sql;
    }

    public string GetMotorTotalSumVal(string start_date, string end_date, string status, string bank, string branch, string user)
    {
        string sql = string.Empty;
        sql = "select ";
        sql += "gp.bbnam,gp.bbrnch, re.bank_code, re.branch_code,count(*) as total_count,null as quo_pre,sum(re.sum_insu) as sum_insu, ";
        sql += "re.V_TYPE, re.YOM, re.V_MAKE, re.V_MODEL, re.PURPOSE, ";
        sql += "to_char(re.entered_on, 'dd/mm/yyyy') as created_date ";
        sql += "from QUOTATION.BANK_REQ_ENTRY_DETAILS re ";
        sql += "inner join GENPAY.BNKBRN gp ";
        sql += "on to_char(gp.bcode) = re.bank_code ";
        sql += "and to_char(gp.bbcode) = re.branch_code ";
        sql += "where re.req_id is not null ";
        if (!string.IsNullOrEmpty(status) && status != "A")
        {
            sql += "and re.flag = '" + status + "' ";
        }
        if (!string.IsNullOrEmpty(bank) && bank != "0")
        {
            sql += "and re.bank_code = '" + bank + "' ";
        }
        if (!string.IsNullOrEmpty(branch) && branch != "0")
        {
            sql += "and re.branch_code = '" + branch + "' ";
        }
        if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
        {
            sql += "and(trunc(re.entered_on) >= to_date('" + start_date + "', 'dd/MM/yyyy')  and trunc(re.entered_on) <= to_date('" + end_date + "', 'dd/MM/yyyy')) ";
        }
        sql += "group by re.bank_code, re.branch_code,gp.bbnam,gp.bbrnch,to_char(re.entered_on,'dd/mm/yyyy'),re.V_TYPE, re.YOM, re.V_MAKE, re.V_MODEL, re.PURPOSE ";
        sql += "order by  to_date(to_char(re.entered_on, 'dd/mm/yyyy'), 'dd/mm/yyyy') desc ";
        return sql;
    }

    //-------------------motor total count----------------------------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public string GetMotorTotalPremiumsCounts(string start_date, string end_date, string status, string bank, string branch, string user)
    {
        string sql = string.Empty;
        sql = "select ";
        sql += "count(*) as total_count, sum(t.qtotal_prm) as quo_pre, sum(re.sum_insu) as sum_insu ";
        sql += "from QUOTATION.BANK_REQ_ENTRY_DETAILS re ";
        sql += "inner join ";
        sql += "QUOTATION.Issued_Quotations t ";
        sql += "on re.quo_no = t.qref_no ";
        sql += "inner join GENPAY.BNKBRN gp ";
        sql += "on to_char(gp.bcode) = re.bank_code ";
        sql += "and to_char(gp.bbcode) = re.branch_code ";
        sql += "where re.quo_no is not null ";
        if (!string.IsNullOrEmpty(status) && status == "A")
        {
            sql += "and re.flag = '" + status + "' ";
        }
        if (!string.IsNullOrEmpty(bank) && bank != "0")
        {
            sql += "and re.bank_code = '" + bank + "' ";
        }
        if (!string.IsNullOrEmpty(branch) && branch != "0")
        {
            sql += "and re.branch_code = '" + branch + "' ";
        }
        if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
        {
            sql += "and(trunc(re.entered_on) >= to_date('" + start_date + "', 'dd/MM/yyyy')  and trunc(re.entered_on) <= to_date('" + end_date + "', 'dd/MM/yyyy'))";
        }
        //sql += "group by re.bank_code, re.branch_code,gp.bbnam,gp.bbrnch,to_char(re.entered_on,'dd/mm/yyyy') ";
        //sql += "order by  to_date(to_char(re.entered_on, 'dd/mm/yyyy'), 'dd/mm/yyyy') desc ";
        return sql;
    }

    public string GetMotorTotalCategoryCounts(string start_date, string end_date, string status, string bank, string branch, string user)
    {
        string sql = string.Empty;
        sql = "select ";
        sql += "count(*) as total_count,null as quo_pre,sum(re.sum_insu) as sum_insu ";
        sql += "from QUOTATION.BANK_REQ_ENTRY_DETAILS re ";
        sql += "inner join GENPAY.BNKBRN gp ";
        sql += "on to_char(gp.bcode) = re.bank_code ";
        sql += "and to_char(gp.bbcode) = re.branch_code ";
        sql += "where re.req_id is not null ";
        if (!string.IsNullOrEmpty(status) && status != "A")
        {
            sql += "and re.flag = '" + status + "' ";
        }
        if (!string.IsNullOrEmpty(bank) && bank != "0")
        {
            sql += "and re.bank_code = '" + bank + "' ";
        }
        if (!string.IsNullOrEmpty(branch) && branch != "0")
        {
            sql += "and re.branch_code = '" + branch + "' ";
        }
        if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
        {
            sql += "and(trunc(re.entered_on) >= to_date('" + start_date + "', 'dd/MM/yyyy')  and trunc(re.entered_on) <= to_date('" + end_date + "', 'dd/MM/yyyy'))";
        }
        //sql += "group by re.bank_code, re.branch_code,gp.bbnam,gp.bbrnch,to_char(re.entered_on,'dd/mm/yyyy') ";
        //sql += "order by  to_date(to_char(re.entered_on, 'dd/mm/yyyy'), 'dd/mm/yyyy') desc ";
        return sql;
    }

    //--------------->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    //--------------------------------reports for all types---------------------------------------------------->>>>>>
    public string GetReportsDeatails(string start_date, string end_date, string ref_id, string policy_no, string status1, string status2, string status3, string bank, string branch, string user, string requestFlag, string nIc, string propTerm, string propType)
    {
        string sql = string.Empty;

        sql = "SELECT PE.DH_REFNO, PE.DH_BCODE, PE.DH_BBRCODE, PE.DH_NAME, PE.DH_AGECODE, PE.DH_AGENAME, upper(PE.DH_NIC), PE.DH_BR, PE.DH_PADD1, PE.DH_PADD2, PE.DH_PADD3, PE.DH_PADD4, ";
        sql += "PE.DH_PHONE, PE.DH_EMAIL, PE.DH_IADD1, PE.DH_IADD2, PE.DH_IADD3, PE.DH_IADD4, to_char(PE.DH_PFROM,'dd/mm/yyyy') as fromDate,to_char(PE.DH_PTO,'dd/mm/yyyy') as toDate,PE.DH_UCONSTR,PE.DH_OCCU_CAR, ";
        sql += "PE.DH_OCC_YES_REAS,PE.DH_HAZ_OCCU,PE.DH_HAZ_YES_REA,PE.DH_VALU_BUILD,PE.DH_VALU_WALL,PE.DH_VALU_TOTAL,PE.DH_AFF_FLOOD,PE.DH_AFF_YES_REAS,PE.DH_WBRICK,PE.DH_WCEMENT,PE.DH_DWOODEN,PE.DH_DMETAL,PE.DH_FTILE, ";
        sql += "PE.DH_FCEMENT,PE.DH_RTILE,PE.DH_RASBES ,PE.DH_RGI,PE.DH_RCONCREAT,PE.DH_COV_FIRE,PE.DH_COV_LIGHT,PE.DH_COV_FLOOD, ";
        sql += "PE.DH_CFWATERAVL,PE.DH_CFYESR1,PE.DH_CFYESR2,PE.DH_CFYESR3,PE.DH_CFYESR4,PE.DH_ENTERED_BY,to_char(PE.DH_ENTERED_ON,'dd/mm/yyyy') as Entered_on,PE.DH_HOLD,PE.DH_NO_OF_FLOORS,PE.DH_OVER_VAL, ";

        sql += "case PE.DH_FINAL_FLAG ";
        sql += "when 'N' then 'Completed' ";
        sql += "when 'Y' then 'Pending' ";
        sql += "when 'R' then 'Rejected' ";
        sql += "when 'C' then 'Cancelled' ";
        sql += "end as DH_FINAL_FLAG, ";

        sql += "PE.dh_isreq,PE.dh_conditions,PE.dh_isreject,PE.dh_iscodi,PE.dh_bcode_id,PE.dh_bbrcode_id, pe.updated_by,pe.updated_on,pe.remarks, ";
        sql += "pe.dh_loading_val,pe.ded_precentage,pe.added_deduct,pe.updated_by,to_char(pe.updated_on,'dd/mm/yyyy') as updatedDate, ";

        //---long term nic prop type --100120222---------NSB changes-
        sql += "PE.Period, PE.LOAN_NUMBER, ";
        sql += "upper(PE.DH_NIC) as DH_NIC, ";

        sql += "case PE.TERM ";
        sql += "when '1' then 'Annual ' ";
        sql += "when '0' then 'Long Term' ";
        sql += "end as TERM, ";

        sql += "case PE.PROP_TYPE ";
        sql += "when '1' then 'Private Dwelling House Only' ";
        sql += "when '2' then 'Private Dwelling House and Solar Panel' ";
        sql += "when '3' then 'Solar Panel Only' ";
        sql += "end as PROP_TYPE, ";
        //------------------------------>>>>>>>>>>>>>>>>>>>
        //--additional columns requested 27062022------cus address and covers>>

        sql += "CASE WHEN PE.DH_PADD1 is not null THEN PE.DH_PADD1 END || ";
        sql += "CASE WHEN PE.DH_PADD2 is not null THEN ', ' || PE.DH_PADD2 END || ";
        sql += "CASE WHEN PE.DH_PADD3 is not null THEN ', ' || PE.DH_PADD3 END || ";
        sql += "CASE WHEN PE.DH_PADD4 is not null THEN ', ' || PE.DH_PADD4 END ";
        sql += "AS CUS_ADD_LINES, ";

        sql += "CASE WHEN PE.FIRE_COVER = 'Y' THEN 'Fire' END || ";
        sql += "CASE WHEN PE.OTHER_COVER = 'Y' THEN ', Other Perils' END || ";
        sql += "CASE WHEN PE.TC_COVER = 'Y' THEN ', TC' END || ";
        sql += "CASE WHEN PE.SRCC_COVER = 'Y' THEN ', SRCC' END || ";
        sql += "CASE WHEN PE.FLOOD_COVER = 'Y' THEN ', Flood' END ";
        sql += "AS REQ_COVERS, ";

        //--additional columns requested 01082022------cus address and covers>>

        sql += "CASE WHEN PE.DH_IADD1 is not null THEN PE.DH_IADD1 END || ";
        sql += "CASE WHEN PE.DH_IADD2 is not null THEN ', ' || PE.DH_IADD2 END || ";
        sql += "CASE WHEN PE.DH_IADD3 is not null THEN ', ' || PE.DH_IADD3 END || ";
        sql += "CASE WHEN PE.DH_IADD4 is not null THEN ', ' || PE.DH_IADD4 END ";
        sql += "AS CUS_RISK_ADD, PE.BPF, MT.DESIG, MT.SLIC_CODE,MT.BRANCH, ";// CC.BREGION, CC.BNAME, CC.SLIC_CODE, CC.DESIGNATION, ";

        //--PE.FIRE_COVER,PE.OTHER_COVER,PE.TC_COVER,PE.SRCC_COVER,PE.FLOOD_COVER,
        //------------------------------------------------->>>>>>>>>>>>>>>>>>>

        sql += "SC.SC_REF_NO,SC.SC_POLICY_NO,SC.SC_SUM_INSU,SC.SC_NET_PRE,SC.SC_RCC,(SC.SC_NBT+SC.SC_VAT) AS TAX, ";
        sql += "SC.SC_TR,SC.SC_ADMIN_FEE,SC.SC_POLICY_FEE,SC.SC_NBT,SC.SC_VAT,SC.SC_TOTAL_PAY,SC.CREATED_ON,SC.CREATED_BY,SC.FLAG,SC.SC_RENEWAL_FEE,SC.BP_FEE,SC.DEBIT_NO ";
        sql += "FROM ";

        sql += "QUOTATION.FIRE_DH_PROPOSAL_ENTRY PE ";
        sql += "INNER JOIN QUOTATION.FIRE_DH_SCHEDULE_CALC SC ";
        sql += "ON PE.DH_REFNO = SC.SC_REF_NO ";
        //sql += "INNER JOIN QUOTATION.BANCASSU_SLIC_CODE CC ";
        //sql += "ON PE.SLIC_CODE = CC.SLIC_CODE ";

        //join slic code table------------------------------------------------->>
        sql += "INNER JOIN (SELECT DISTINCT(SLIC_CODE), DESIG ,BRANCH ";
        sql += "FROM( ";
        //sql += "SELECT TRIM(AE.DESIGNATION|| '-' || AE.SLIC_CODE || '-' || AE.ANAME) AS DESIG, TRIM(AE.SLIC_CODE) AS SLIC_CODE FROM QUOTATION.BANCASSU_AE_SLIC_CODE AE ";
        //sql += "WHERE AE.ACTIVE = 'Y' ";
        //if (!string.IsNullOrEmpty(bank) && bank != "0")
        //{
        //    //sql += "AND AE.BCODE = '" + bank + "' ";
        //}
        //if (!string.IsNullOrEmpty(branch) && branch != "0")
        //{
        //    //sql += "AND AE.BBCODE = '" + branch + "' ";
        //}
        //sql += "AND AE.SLIC_CODE IS NOT NULL ";
        //sql += "UNION ALL ";
        //sql += "SELECT 'DIRECT' AS DESIG,'0' AS SLIC_CODE FROM DUAL ";
        //sql += "UNION ALL ";
        //sql += "SELECT TRIM(BS.DESIGNATION|| '-' || BS.SLIC_CODE || '-' || BS.BNAME) AS DESIG, TRIM(BS.SLIC_CODE)AS SLIC_CODE FROM QUOTATION.BANCASSU_BSO_SLIC_CODE BS ";
        //sql += "WHERE BS.ACTIVE = 'Y' ";
        //if (!string.IsNullOrEmpty(bank) && bank != "0")
        //{
        //    //sql += "AND BS.BCODE = '" + bank + "' ";
        //}
        //if (!string.IsNullOrEmpty(branch) && branch != "0")
        //{
        //    //sql += "AND BS.BBCODE = '" + branch + "' ";
        //}
        //sql += "AND BS.SLIC_CODE IS NOT NULL ";
        //sql += "UNION ALL ";
        //sql += "SELECT TRIM(ME.DESIGNATION|| '-' || ME.SLIC_CODE || '-' || ME.MNAME) AS DESIG, TRIM(ME.SLIC_CODE) AS SLIC_CODE FROM QUOTATION.BANCASSU_ME_SLIC_CODE ME ";
        //sql += "WHERE ME.ACTIVE = 'Y' ";
        //if (!string.IsNullOrEmpty(bank) && bank != "0")
        //{
        //    // sql += "AND ME.BCODE = '" + bank + "' ";
        //}
        //if (!string.IsNullOrEmpty(branch) && branch != "0")
        //{
        //    // sql += "AND ME.BBCODE = '" + branch + "' ";
        //}
        //sql += "AND ME.SLIC_CODE IS NOT NULL ";

        sql += "select SLICODE as SLIC_CODE, SLICODE||' ' || mename as DESIG,BRANCH from quotation.bancassurance_me";
        sql += " union all ";
        sql += " SELECT 0 AS SLIC_CODE , 'DIRECT' as DESIG, 'DIRECT' as BRANCH from DUAL ";
        sql += ")) MT ";
        sql += "ON MT.SLIC_CODE = PE.SLIC_CODE ";

        sql += "WHERE PE.DH_REFNO IS NOT NULL ";

        if (!string.IsNullOrEmpty(requestFlag))
        {
            sql += "and DH_ISREQ = '" + requestFlag + "' ";
        }

        if (!string.IsNullOrEmpty(ref_id))
        {
            sql += "and PE.DH_REFNO= '" + ref_id + "' ";
        }
        if (!string.IsNullOrEmpty(policy_no))
        {
            sql += "and SC.SC_POLICY_NO= '" + policy_no + "' ";
        }
        if (!string.IsNullOrEmpty(nIc))
        {
            sql += "and upper(PE.DH_NIC)= upper('" + nIc + "') ";
        }

        if (!string.IsNullOrEmpty(bank) && bank != "0")
        {
            sql += "and PE.dh_bcode_id= '" + bank + "' ";
        }

        if (!string.IsNullOrEmpty(branch) && branch != "0")
        {
            sql += "and PE.dh_bbrcode_id= '" + branch + "' ";
        }

        //changes--22032022--according to NSB
        if (!string.IsNullOrEmpty(propTerm) && propTerm != "A")
        {
            sql += "and PE.TERM = '" + propTerm + "' ";
        }

        if (!string.IsNullOrEmpty(propType) && propType != "A")
        {
            sql += "and PE.PROP_TYPE = '" + propType + "' ";
        }
        //--->>

        //if (!string.IsNullOrEmpty(user) && user != "A")
        //{
        //    sql += "and PE.DH_ENTERED_BY= '" + user + "' "; // FOR SUPER USER REASON THIS COMMENT
        //}
        if (!string.IsNullOrEmpty(status1) && status1 != "A")
        {
            sql += "and PE.DH_HOLD = '" + status1 + "' ";
        }

        if (!string.IsNullOrEmpty(status2) && status2 != "A")
        {
            sql += "and PE.DH_OVER_VAL = '" + status2 + "' ";
        }
        if (!string.IsNullOrEmpty(status3) && status3 != "A")
        {
            sql += "and PE.DH_FINAL_FLAG = '" + status3 + "' ";
        }

        if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
        {
            sql += "and(to_date(to_char(PE.DH_ENTERED_ON,'dd/MM/yyyy'),'dd/MM/yyyy') > = to_date('" + start_date + "','dd/MM/yyyy') ";
            sql += "and to_date(to_char(PE.DH_ENTERED_ON,'dd/MM/yyyy'),'dd/MM/yyyy') <= to_date('" + end_date + "','dd/MM/yyyy')) ";
        }

        sql += "order by PE.DH_ENTERED_ON desc";
        return sql;
    }

    //---------------------------------------------------------------------------------------------------------->>>>>>>

    public string GetNumbers(int limit)
    {
        string sql = string.Empty;

        sql = "select n from ";
        sql += "(select rownum n from dual connect by level <= " + limit + ") ";
        sql += "where n >= 2";
        return sql;
    }

    public string GetSolartes(string bankCode, double maxVal, double minVal)
    {
        string sql = string.Empty;

        sql = "select RATE ";
        sql += "FROM QUOTATION.FIRE_DH_SOLAR_RATE ";
        sql += "where active = 'Y' ";
        sql += "and BANK_CODE = '" + bankCode + "' ";
        sql += "and MIN_VAL <= " + minVal + " ";
        sql += "and MAX_VAL >= " + maxVal + "";
        return sql;
    }

    // SOLAR -->> electrical and accidental
    public string GetSolarElectrical(string bankCode, double maxVal, double minVal)
    {
        string sql = string.Empty;

        sql = "select CLA_VAL ";
        sql += "FROM QUOTATION.FIRE_SOLAR_ELECTRICAL_CLAUSE ";
        sql += "where active = 'Y' ";
        sql += "and BANK_CODE = '" + bankCode + "' ";
        sql += "and MIN_VAL <= " + minVal + " ";
        sql += "and MAX_VAL >= " + maxVal + "";
        return sql;
    }

    public string GetSolarAccidental(string bankCode, double maxVal, double minVal)
    {
        string sql = string.Empty;

        sql = "select CLA_VAL ";
        sql += "FROM QUOTATION.FIRE_SOLAR_ACCIDENTAL_CLAUSE ";
        sql += "where active = 'Y' ";
        sql += "and BANK_CODE = '" + bankCode + "' ";
        sql += "and MIN_VAL <= " + minVal + " ";
        sql += "and MAX_VAL >= " + maxVal + "";
        return sql;
    }

    //------------------------------------
    // get long term rate for all covers---->>
    public string GetLongTermRateForAllCover(string bankCode, double maxVal, double minVal)
    {
        string sql = string.Empty;

        sql = "select RATE ";
        sql += "FROM QUOTATION.FIRE_RATE_LONGTERM_ALL_SUMINSU ";
        sql += "where active = 'Y' ";
        sql += "and BANK_CODE = '" + bankCode + "' ";
        sql += "and MIN_VAL <= " + minVal + " ";
        sql += "and MAX_VAL >= " + maxVal + "";
        return sql;
    }

    //---Get Uploaded Motor Quotations-----------------------------------------------------------------------------
    public string GetUploadQuotaions(string refNo)
    {
        string sql = string.Empty;

        sql = "select Q_NO, Q_NAME,to_char(CREATED_ON,'dd/mm/yyyy') as CREATED_ON, ";
        sql += "case Q_FLAG ";
        sql += "when 'D' then 'Sent' ";
        sql += "when 'C' then 'Cancel' ";
        sql += "end as STATUS ";
        sql += "from QUOTATION.BANCASSU_UPMOTO_QUOTATIONS where Q_REF = '" + refNo + "' ";
        sql += "and P_ACTIVE ='Y' ";
        sql += "order by Q_NO desc";

        return sql;
    }

    //----------------end------------------------------------------------------------------------------------

    public string GetMotorRemarks(string refNo)
    {
        string sql = string.Empty;

        sql = "select nm.r_no, NVL(nm.r_slic, 'N/A') as r_slic, NVL(nm.r_bank, 'N/A') as r_bank from QUOTATION.BANCASSU_MOTOQUOREMARKS nm ";
        sql += "where nm.r_ref = '" + refNo + "' ";
        sql += "and nm.r_flag = 'A' ";
        sql += "order by nm.r_no asc";

        return sql;
    }

    //---Get Uploaded response tean bank side quotation ----------------------------------------------------------------------------
    public string GetUploadBankTickets(string refNo)
    {
        string sql = string.Empty;

        sql = "select T_NO, T_NAME,to_char(CREATED_ON,'dd/mm/yyyy') as CREATED_ON, ";
        sql += "case T_FLAG ";
        sql += "when 'D' then 'Uploaded' ";
        sql += "when 'C' then 'Removed' ";
        sql += "end as STATUS ";
        sql += "from QUOTATION.BANCASSU_BANK_ITUPDOC where T_REF = '" + refNo + "' ";
        sql += "and T_ACTIVE ='Y' ";
        sql += "order by T_NO desc";

        return sql;
    }

    //----------------end------------------------------------------------------------------------------------
    //---Get Uploaded response tean bank side quotation ----------------------------------------------------------------------------
    public string GetUploadSLICTickets(string refNo)
    {
        string sql = string.Empty;

        sql = "select T_NO, T_NAME,to_char(CREATED_ON,'dd/mm/yyyy') as CREATED_ON, ";
        sql += "case T_FLAG ";
        sql += "when 'D' then 'Uploaded' ";
        sql += "when 'C' then 'Removed' ";
        sql += "end as STATUS ";
        sql += "from QUOTATION.BANCASSU_SLIC_ITUPDOC where T_REF = '" + refNo + "' ";
        sql += "and T_ACTIVE ='Y' ";
        sql += "order by T_NO desc";

        return sql;
    }

    //----------------end------------------------------------------------------------------------------------
    // ticket all process sql------------>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

    public string GetBankTickets(string start_date, string end_date, string ticket_id, string reqType, string status, string bank, string branch, string user)
    {
        string sql = string.Empty;

        sql = "Select T_REF,T_BANK_REMARK, T_BANK_CODE, T_BRANCH_CODE,T_BANK_NAME,T_BRANCH_NAME, T_FLAG as valTlag, to_number(SUBSTR( T_REF, 2 )) as initials, T_BANK_EMAIL, ";

        sql += "case T_FLAG ";
        sql += "when '1' then 'Motor insurance' ";
        sql += "when '2' then 'Fire insurance (house and solar panel)' ";
        sql += "when '3' then 'Fire insurance (all other categories)' ";
        sql += "when '4' then 'Title insurance' ";
        sql += "when '5' then 'Marine cargo insurance' ";
        sql += "when '6' then 'Debit note reques' ";
        sql += "when '7' then 'All other categories' ";
        sql += "end as T_FLAG, ";

        sql += "T_CREATED_BY, to_char(T_CREATED_ON,'dd/mm/yyyy') as T_CREATED_ON, T_SLIC_REMARK, ";
        sql += "T_UP_BY_SLIC, to_char(T_UP_ON_SLIC,'dd/mm/yyyy') as T_UP_ON_SLIC, ";

        sql += "case T_STATUS ";
        sql += "when 'P' then 'Pending' ";
        sql += "when 'D' then 'Complete' ";
        sql += "end as T_STATUS ";

        sql += "FROM ";

        sql += "QUOTATION.BANCASSU_INQU_TICKETS ";

        sql += "WHERE T_REF IS NOT NULL ";

        if (!string.IsNullOrEmpty(ticket_id))
        {
            sql += "and T_REF= '" + ticket_id + "' ";
        }

        if (!string.IsNullOrEmpty(bank) && bank != "0")
        {
            sql += "and T_BANK_CODE = '" + bank + "' ";
        }

        if (!string.IsNullOrEmpty(branch) && branch != "0")
        {
            sql += "and T_BRANCH_CODE = '" + branch + "' ";
        }

        //if (!string.IsNullOrEmpty(user) && user != "A")
        //{
        //    sql += "and T_CREATED_BY = '" + user + "' "; // FOR SUPER USER REASON THIS COMMENT 20/01/2022
        //}

        if (!string.IsNullOrEmpty(reqType) && reqType != "0")
        {
            sql += "and T_FLAG = '" + reqType + "' ";
        }

        if (!string.IsNullOrEmpty(status) && status != "0")
        {
            sql += "and T_STATUS = '" + status + "' ";
        }

        if (!string.IsNullOrEmpty(start_date) && !string.IsNullOrEmpty(end_date))
        {
            sql += "and(to_date(to_char(T_CREATED_ON,'dd/MM/yyyy'),'dd/MM/yyyy') > = to_date('" + start_date + "','dd/MM/yyyy') ";
            sql += "and to_date(to_char(T_CREATED_ON,'dd/MM/yyyy'),'dd/MM/yyyy') <= to_date('" + end_date + "','dd/MM/yyyy')) ";
        }

        sql += "order by initials desc";
        return sql;
    }

    //31012022--changes----------------->>>
    public string GetImmediateOfficer(string code)
    {
        string sql = string.Empty;

        sql = "select io.bank_code, io.bank_name, io.officer_name, io.contact_no, io.email ";
        sql += "from QUOTATION.IM_CONTACT_PERSON io ";
        sql += "where io.bank_code = '" + code + "' ";
        sql += "and io.flag = 'A'";

        return sql;
    }

    //--------------------------------QUERIES 26042023 BANK INSENTIVE FEE-------------------------------------------------------
    // SLIC CODE
    //public string GetSLIC_Code(string bankCode, string branchCode)
    //{
    //    string sql = string.Empty;

    //    sql = "select desig, slic_code ";
    //    sql += "from( ";
    //    sql += "select trim(ae.designation || '-' || ae.slic_code || '-' || ae.aname) as desig, trim(ae.slic_code) as slic_code from QUOTATION.BANCASSU_AE_SLIC_CODE ae ";
    //    sql += "where ae.active = 'Y' ";
    //    if (!string.IsNullOrEmpty(bankCode) && bankCode != "0")
    //    {
    //        sql += "and ae.bcode = '" + bankCode + "' ";
    //    }
    //    if (!string.IsNullOrEmpty(branchCode) && branchCode != "0")
    //    {
    //        sql += "and ae.bbcode = '" + branchCode + "' ";
    //    }
    //    sql += "and ae.slic_code is not null ";
    //    sql += "union all ";
    //    sql += "select '' as desig, '0' as slic_code from dual ";
    //    sql += "union all ";
    //    sql += "select trim(bs.designation || '-' || bs.slic_code || '-' || bs.bname) as desig, trim(bs.slic_code) as slic_code from QUOTATION.BANCASSU_BSO_SLIC_CODE bs ";
    //    sql += "where bs.active = 'Y' ";
    //    if (!string.IsNullOrEmpty(bankCode) && bankCode != "0")
    //    {
    //        sql += "and bs.bcode = '" + bankCode + "' ";
    //    }
    //    if (!string.IsNullOrEmpty(branchCode) && branchCode != "0")
    //    {
    //        sql += "and bs.bbcode = '" + branchCode + "' ";
    //    }
    //    sql += "and bs.slic_code is not null ";
    //    sql += "union all ";
    //    sql += "select '' as desig, '0' as slic_code from dual ";
    //    sql += "union all ";
    //    sql += "select trim(me.designation || '-' || me.slic_code || '-' || me.mname) as desig, trim(me.slic_code) as slic_code from QUOTATION.BANCASSU_ME_SLIC_CODE me ";
    //    sql += "where me.active = 'Y' ";
    //    if (!string.IsNullOrEmpty(bankCode) && bankCode != "0")
    //    {
    //        sql += "and me.bcode = '" + bankCode + "' ";
    //    }
    //    if (!string.IsNullOrEmpty(branchCode) && branchCode != "0")
    //    {
    //        sql += "and me.bbcode = '" + branchCode + "' ";
    //    }
    //    sql += "and me.slic_code is not null)";
    //    //sql = "SELECT BREGION, BNAME, SLIC_CODE, DESIGNATION, ";
    //    ////sql += "UPPER(BREGION || '-' || BNAME || '-' || SLIC_CODE || '-' || DESIGNATION) AS ALLCONTENT ";
    //    //sql += "UPPER(DESIGNATION || '-' || SLIC_CODE || '-' || BNAME) AS ALLCONTENT ";
    //    //sql += "FROM QUOTATION.BANCASSU_SLIC_CODE ";
    //    //sql += "WHERE ACTIVE = 'Y' ";
    //    //sql += "ORDER BY SEQ_ID ASC";

    //    return sql;
    //}
    //--------------------------------QUERIES 26042023 BANK INSENTIVE FEE-------------------------------------------------------
    // SLIC CODE
    //public string GetSLIC_Code(string bankCode, string branchCode)
    //{
    //    string sql = string.Empty;

    //    sql = "select desig, slic_code ";
    //    sql += "from( ";
    //    sql += "select trim(ae.designation || '-' || ae.slic_code || '-' || ae.aname) as desig, trim(ae.slic_code) as slic_code from QUOTATION.BANCASSU_AE_SLIC_CODE ae ";
    //    sql += "where ae.active = 'Y' ";
    //    if (!string.IsNullOrEmpty(bankCode) && bankCode != "0")
    //    {
    //        sql += "and ae.bcode = '" + bankCode + "' ";
    //    }
    //    if (!string.IsNullOrEmpty(branchCode) && branchCode != "0")
    //    {
    //        sql += "and ae.bbcode = '" + branchCode + "' ";
    //    }
    //    sql += "and ae.slic_code is not null ";
    //    sql += "union all ";
    //    sql += "select '' as desig, '0' as slic_code from dual ";
    //    sql += "union all ";
    //    sql += "select trim(bs.designation || '-' || bs.slic_code || '-' || bs.bname) as desig, trim(bs.slic_code) as slic_code from QUOTATION.BANCASSU_BSO_SLIC_CODE bs ";
    //    sql += "where bs.active = 'Y' ";
    //    if (!string.IsNullOrEmpty(bankCode) && bankCode != "0")
    //    {
    //        sql += "and bs.bcode = '" + bankCode + "' ";
    //    }
    //    if (!string.IsNullOrEmpty(branchCode) && branchCode != "0")
    //    {
    //        sql += "and bs.bbcode = '" + branchCode + "' ";
    //    }
    //    sql += "and bs.slic_code is not null ";
    //    sql += "union all ";
    //    sql += "select '' as desig, '0' as slic_code from dual ";
    //    sql += "union all ";
    //    sql += "select trim(me.designation || '-' || me.slic_code || '-' || me.mname) as desig, trim(me.slic_code) as slic_code from QUOTATION.BANCASSU_ME_SLIC_CODE me ";
    //    sql += "where me.active = 'Y' ";
    //    if (!string.IsNullOrEmpty(bankCode) && bankCode != "0")
    //    {
    //        sql += "and me.bcode = '" + bankCode + "' ";
    //    }
    //    if (!string.IsNullOrEmpty(branchCode) && branchCode != "0")
    //    {
    //        sql += "and me.bbcode = '" + branchCode + "' ";
    //    }
    //    sql += "and me.slic_code is not null)";
    //    //sql = "SELECT BREGION, BNAME, SLIC_CODE, DESIGNATION, ";
    //    ////sql += "UPPER(BREGION || '-' || BNAME || '-' || SLIC_CODE || '-' || DESIGNATION) AS ALLCONTENT ";
    //    //sql += "UPPER(DESIGNATION || '-' || SLIC_CODE || '-' || BNAME) AS ALLCONTENT ";
    //    //sql += "FROM QUOTATION.BANCASSU_SLIC_CODE ";
    //    //sql += "WHERE ACTIVE = 'Y' ";
    //    //sql += "ORDER BY SEQ_ID ASC";

    //    return sql;
    //}

    public string GetSLIC_Code(string bankCode, string branchCode)
    {
        string sql = string.Empty;
        sql += "SELECT MENAME|| '-  ['|| SLICODE ||'] -- ' || BRANCH AS desig, SLICODE AS slic_code FROM QUOTATION.BANCASSURANCE_ME";
        return sql;
    }

    // BPF-FEE
    public string GetBancFee(string bankCode, double maxVal, double minVal)
    {
        string sql = string.Empty;
        sql = "SELECT SEQ_ID, BANK_CODE, BFEE ";
        sql += "FROM QUOTATION.BANCASSU_BUS_FEE ";
        sql += "WHERE ACTIVE = 'Y' ";
        sql += "AND BANK_CODE = '" + bankCode + "' ";
        sql += "AND MIN_VAL <= " + minVal + " ";
        sql += "AND MAX_VAL >= " + maxVal + "";
        return sql;
    }

    //-- Check BPF allow or not

    public string GetBPF_Allow(string bankCode)
    {
        string sql = string.Empty;
        sql = "SELECT COUNT(*) ";
        sql += "FROM QUOTATION.BANCASSU_BUS_FEE ";
        sql += "WHERE ACTIVE = 'Y' ";
        sql += "AND BANK_CODE = '" + bankCode + "'";
        return sql;
    }

    public string GetBranchBPFAllowOrNot(int bankCode, int branchCode, string userNmae)
    {
        string sql = string.Empty;
        sql = "select ac.username, ac.bank_code, ac.branch_code, ac.active_flag, ";
        sql += "case ac.active_flag ";
        sql += "when 'N' then 'Deactive' ";
        sql += "when 'Y' then 'Active' ";
        sql += "else 'Wrong' ";
        sql += "end as status, ";
        sql += "br.bbnam, br.bbrnch ";
        sql += "from QUOTATION.BPF_ACC_CONTROL ac ";
        sql += "inner join GENPAY.BNKBRN br ";
        sql += "on br.bcode = ac.bank_code ";
        sql += "and br.bbcode = ac.branch_code ";
        sql += "where ac.username is not null ";
        if (!string.IsNullOrEmpty(bankCode.ToString()) && bankCode != 0)
        {
            sql += "and ac.bank_code = " + bankCode + " ";
        }
        if (!string.IsNullOrEmpty(branchCode.ToString()) && branchCode != 0)
        {
            sql += "and ac.branch_code = " + branchCode + " ";
        }
        if (!string.IsNullOrEmpty(userNmae) && userNmae != "0")
        {
            sql += "and ac.username = '" + userNmae + "' ";
        }
        sql += "order by ac.branch_code";

        return sql;
    }

    /*-----------------------------------------------------------------*/

    public string GetOdpBankCodes(int exec_para)
    {
        string sql = string.Empty;
        sql = "select distinct(br.bcode) as bcode, trim(Upper(br.bbnam)) as bbnam ";

        if (exec_para > 0)
            sql += "from GENPAY.BNKBRN br where br.bcode =" + exec_para;
        else
            sql += "from GENPAY.BNKBRN br ";
        sql += " group by bbnam, bcode ";
        sql += "order by bbnam asc";
        return sql;
    }

    public string GetODPolicy(string start_date, string end_date, string policy_no, string nic, int bank_code, int branch_code, string status)
    {
        string dql = string.Empty;
        dql += "SELECT ODP.PRNO, ODP.POLNO, (ODP.TITLE|| ODP.CUS_NAME) as CUSTOMER, ODP.NIC, to_char(ODP.POLICY_SDATE, 'dd/mm/yyyy') as POLICY_SDATE, to_char(ODP.POLICY_EDATE,'dd/mm/yyyy') as POLICY_EDATE, FODP.SUMINSURD, FODP.TOT_PREMIUM,  ODP.CONNOMOB, TO_CHAR(ODP.ENTDATE, 'dd/MM/yyyy') as ENTDATE, ODP.POL_STATUS, ODP.SRID ";
        dql += "FROM QUOTATION.ODP_PROPOSAL_ENTRY ODP ";
        dql += "INNER JOIN QUOTATION.ODP_PREMIUM FODP ";
        dql += "ON ODP.SRID = FODP.SRID ";
        dql += "WHERE  ODP.BANK_CODE = " + bank_code + " AND ODP.BRANCH_CODE =" + branch_code;

        if (!String.IsNullOrEmpty(policy_no))
            dql += " AND ODP.POLNO = '" + policy_no + "'";

        if (!String.IsNullOrEmpty(nic))
            dql += " AND ODP.NIC = '" + nic + "'";

        if (!String.IsNullOrEmpty(status) && status != "0")
            dql += " AND ODP.POL_STATUS = '" + status + "'";

        if (!String.IsNullOrEmpty(start_date) && !String.IsNullOrEmpty(end_date))
        {
            dql += " AND trunc(ODP.ENTDATE) BETWEEN TO_DATE('" + start_date + "', 'DD/MM/YYYY') AND TO_DATE('" + end_date + "', 'DD/MM/YYYY')";
        }

        return dql;
    }

    public string GetODPolicy_BackProcess(string start_date, string end_date, string policy_no, string nic, string status)
    {
        string dql = string.Empty;
        dql += "SELECT ODP.PRNO, ODP.POLNO, (ODP.TITLE||ODP.CUS_NAME) as CUSTOMER, ODP.NIC, to_char(ODP.POLICY_SDATE, 'dd/mm/yyyy') as POLICY_SDATE, to_char(ODP.POLICY_EDATE,'dd/mm/yyyy') as POLICY_EDATE, FODP.SUMINSURD, FODP.TOT_PREMIUM,  ODP.CONNOMOB, TO_CHAR(ODP.ENTDATE, 'dd/MM/yyyy') as ENTDATE, ODP.POL_STATUS, ODP.SRID, substr(ODP.BRANCH_CODE,-3) AS BRANCH_CODE ";
        dql += "FROM QUOTATION.ODP_PROPOSAL_ENTRY ODP ";
        dql += "INNER JOIN QUOTATION.ODP_PREMIUM FODP ";
        dql += "ON ODP.SRID = FODP.SRID ";
        dql += "WHERE ODP.POL_STATUS = 'P'";

        if (!String.IsNullOrEmpty(policy_no))
            dql += " AND ODP.POLNO = '" + policy_no + "'";

        if (!String.IsNullOrEmpty(nic))
            dql += " AND ODP.NIC = '" + nic + "'";

        if (!String.IsNullOrEmpty(start_date) && !String.IsNullOrEmpty(end_date))
        {
            dql += " AND trunc(ODP.ENTDATE) BETWEEN TO_DATE('" + start_date + "', 'DD/MM/YYYY') AND TO_DATE('" + end_date + "', 'DD/MM/YYYY')";
        }

        return dql;
    }

    public string GetBranchPerformance(string start_date, string end_date, string status, int bank_code, int branch_code)
    {
        string dql = string.Empty;
        dql += "SELECT BNK.BBNAM, BNK.BBRNCH, count(*) AS NOFPOL, trunc(OPE.ENTDATE) AS ENTDATE, sum(PINFO.SUMINSURD) AS SUMINS  FROM QUOTATION.ODP_PROPOSAL_ENTRY OPE ";
        dql += "INNER JOIN QUOTATION.ODP_POLICYINFO PINFO ";
        dql += "ON OPE.SRID = PINFO.SRID ";
        dql += "INNER JOIN GENPAY.BNKBRN BNK ";
        dql += "ON BNK.BCODE = OPE.BANK_CODE AND BNK.BBCODE = OPE.BRANCH_CODE ";
        dql += "WHERE POL_STATUS = '" + status + "' AND  OPE.BANK_CODE =" + bank_code + " AND OPE.BRANCH_CODE = " + branch_code;

        if (!String.IsNullOrEmpty(start_date) && !String.IsNullOrEmpty(end_date))
        {
            dql += " AND trunc(OPE.ENTDATE) BETWEEN TO_DATE('" + start_date + "', 'DD/MM/YYYY') AND TO_DATE('" + end_date + "', 'DD/MM/YYYY')";
        }

        dql += " GROUP BY BNK.BBNAM, BNK.BBRNCH, trunc(OPE.ENTDATE)";
        return dql;
    }

    public string GetBranchIncome(string start_date, string end_date, string status, int bank_code, int branch_code)
    {
        string dql = string.Empty;
        dql += "SELECT count(*) AS NOFPOL, sum(PINFO.SUMINSURD) AS SUMINS  FROM QUOTATION.ODP_PROPOSAL_ENTRY OPE ";
        dql += "INNER JOIN QUOTATION.ODP_POLICYINFO PINFO ";
        dql += "ON OPE.SRID = PINFO.SRID ";
        dql += "INNER JOIN GENPAY.BNKBRN BNK ";
        dql += "ON BNK.BCODE = OPE.BANK_CODE AND BNK.BBCODE = OPE.BRANCH_CODE ";
        dql += "WHERE POL_STATUS = '" + status + "' AND  OPE.BANK_CODE =" + bank_code + " AND OPE.BRANCH_CODE = " + branch_code;

        if (!String.IsNullOrEmpty(start_date) && !String.IsNullOrEmpty(end_date))
        {
            dql += " AND trunc(OPE.ENTDATE) BETWEEN TO_DATE('" + start_date + "', 'DD/MM/YYYY') AND TO_DATE('" + end_date + "', 'DD/MM/YYYY')";
        }
        dql += " GROUP BY BNK.BBNAM, BNK.BBRNCH";
        return dql;
    }

    public string GetODPolicy_BackProcessView(string start_date, string end_date, string policy_no, string nic, string status)
    {
        string dql = string.Empty;
        dql += "SELECT ODP.PRNO, ODP.POLNO, (ODP.TITLE || '.' || ODP.CUS_NAME) as CUSTOMER, ODP.NIC, to_char(ODP.POLICY_SDATE, 'dd/mm/yyyy') as POLICY_SDATE, to_char(ODP.POLICY_EDATE,'dd/mm/yyyy') as POLICY_EDATE, FODP.SUMINSURD, FODP.TOT_PREMIUM,  ODP.CONNOMOB, TO_CHAR(ODP.ENTDATE, 'dd/MM/yyyy') as ENTDATE, ODP.POL_STATUS, ODP.SRID ";
        dql += "FROM QUOTATION.ODP_PROPOSAL_ENTRY ODP ";
        dql += "INNER JOIN QUOTATION.ODP_PREMIUM FODP ";
        dql += "ON ODP.SRID = FODP.SRID ";
        dql += "WHERE ODP.POL_STATUS = 'C'";

        if (!String.IsNullOrEmpty(policy_no))
            dql += " AND ODP.POLNO = '" + policy_no + "'";

        if (!String.IsNullOrEmpty(nic))
            dql += " AND ODP.NIC = '" + nic + "'";

        if (!String.IsNullOrEmpty(start_date) && !String.IsNullOrEmpty(end_date))
        {
            dql += " AND trunc(ODP.ENTDATE) BETWEEN TO_DATE('" + start_date + "', 'DD/MM/YYYY') AND TO_DATE('" + end_date + "', 'DD/MM/YYYY')";
        }

        return dql;
    }

    public string GetOdpPerformance(string start_date, string end_date, string status, int bank_code, int branch_code)
    {
        string dql = string.Empty;
        dql += "SELECT BNK.BBNAM, BNK.BBRNCH, count(*) AS NOFPOL, trunc(OPE.ENTDATE) AS ENTDATE, sum(PINFO.SUMINSURD) AS SUMINS  FROM QUOTATION.ODP_PROPOSAL_ENTRY OPE ";
        dql += "INNER JOIN QUOTATION.ODP_POLICYINFO PINFO ";
        dql += "ON OPE.SRID = PINFO.SRID ";
        dql += "INNER JOIN GENPAY.BNKBRN BNK ";
        dql += "ON BNK.BCODE = OPE.BANK_CODE AND BNK.BBCODE = OPE.BRANCH_CODE ";
        dql += "WHERE OPE.PRID = 'ODP' ";

        if (!String.IsNullOrEmpty(status) && status != "A")
            dql += " AND OPE.POL_STATUS = '" + status + "'";

        if (!String.IsNullOrEmpty(bank_code.ToString()) && bank_code > 0)
            dql += " AND OPE.BANK_CODE =" + bank_code;

        if (!String.IsNullOrEmpty(branch_code.ToString()) && branch_code > 0)
            dql += " AND OPE.BRANCH_CODE =" + branch_code;

        if (!String.IsNullOrEmpty(start_date) && !String.IsNullOrEmpty(end_date))
        {
            dql += " AND trunc(OPE.ENTDATE) BETWEEN TO_DATE('" + start_date + "', 'DD/MM/YYYY') AND TO_DATE('" + end_date + "', 'DD/MM/YYYY')";
        }

        dql += " GROUP BY BNK.BBNAM, BNK.BBRNCH, trunc(OPE.ENTDATE)";
        return dql;
    }

    public string GetOdpIncome(string start_date, string end_date, string status, int bank_code, int branch_code)
    {
        string dql = string.Empty;
        dql += "SELECT count(*) AS NOFPOL,  sum(PINFO.SUMINSURD) AS SUMINS  FROM QUOTATION.ODP_PROPOSAL_ENTRY OPE ";
        dql += "INNER JOIN QUOTATION.ODP_POLICYINFO PINFO ";
        dql += "ON OPE.SRID = PINFO.SRID ";
        dql += "INNER JOIN GENPAY.BNKBRN BNK ";
        dql += "ON BNK.BCODE = OPE.BANK_CODE AND BNK.BBCODE = OPE.BRANCH_CODE ";
        dql += "WHERE OPE.PRID = 'ODP' ";

        if (!String.IsNullOrEmpty(status) && status != "A")
            dql += " AND OPE.POL_STATUS = '" + status + "'";

        if (!String.IsNullOrEmpty(bank_code.ToString()) && bank_code > 0)
            dql += " AND OPE.BANK_CODE =" + bank_code;

        if (!String.IsNullOrEmpty(branch_code.ToString()) && branch_code > 0)
            dql += " AND OPE.BRANCH_CODE =" + branch_code;

        if (!String.IsNullOrEmpty(start_date) && !String.IsNullOrEmpty(end_date))
        {
            dql += " AND trunc(OPE.ENTDATE) BETWEEN TO_DATE('" + start_date + "', 'DD/MM/YYYY') AND TO_DATE('" + end_date + "', 'DD/MM/YYYY')";
        }

        dql += " GROUP BY BNK.BBNAM, BNK.BBRNCH";
        return dql;
    }

    public string GetMoreInformation(string start_date, string end_date, string status, int bank_code, int branch_code)
    {
        string dql = string.Empty;
        dql += "SELECT BNK.BBNAM, BNK.BBRNCH, count(*) AS NOFPOL, trunc(OPE.ENTDATE) AS ENTDATE, sum(PINFO.SUMINSURD) AS SUMINS  FROM QUOTATION.ODP_PROPOSAL_ENTRY OPE ";
        dql += "INNER JOIN QUOTATION.ODP_POLICYINFO PINFO ";
        dql += "ON OPE.SRID = PINFO.SRID ";
        dql += "INNER JOIN GENPAY.BNKBRN BNK ";
        dql += "ON BNK.BCODE = OPE.BANK_CODE AND BNK.BBCODE = OPE.BRANCH_CODE ";
        dql += "WHERE OPE.PRID = 'ODP' ";

        if (!String.IsNullOrEmpty(status) && status != "A")
            dql += " AND OPE.POL_STATUS = '" + status + "'";

        if (!String.IsNullOrEmpty(bank_code.ToString()) && bank_code > 0)
            dql += " AND OPE.BANK_CODE =" + bank_code;

        if (!String.IsNullOrEmpty(branch_code.ToString()) && branch_code > 0)
            dql += " AND OPE.BRANCH_CODE =" + branch_code;

        if (!String.IsNullOrEmpty(start_date) && !String.IsNullOrEmpty(end_date))
        {
            dql += " AND trunc(OPE.ENTDATE) BETWEEN TO_DATE('" + start_date + "', 'DD/MM/YYYY') AND TO_DATE('" + end_date + "', 'DD/MM/YYYY')";
        }

        dql += " GROUP BY BNK.BBNAM, BNK.BBRNCH, trunc(OPE.ENTDATE)";
        return dql;
    }

    public string GetOdpPerformanceMoreInfo(string start_date, string end_date, string status, int bank_code, int branch_code)
    {
        string dql = string.Empty;
        //dql += "SELECT BNK.BBNAM, BNK.BBRNCH, OPE.PRNO as Proposal_No, OPE.NIC, OPE.BTUPE as NatureOfBusiness, OPE.CONNOMOB AS ContactNumber, PRMIUM.SUMINSURD, PRMIUM.NETPREMIUM, OPE.MECODE, ME.BRANCH, ME.REGION,  trunc(OPE.ENTDATE) AS ENTDATE   FROM QUOTATION.ODP_PROPOSAL_ENTRY OPE ";
        //dql += "INNER JOIN QUOTATION.ODP_PREMIUM PRMIUM ";
        //dql += "ON OPE.SRID = PRMIUM.SRID ";
        //dql += "INNER JOIN GENPAY.BNKBRN BNK ";
        //dql += "ON BNK.BCODE = OPE.BANK_CODE AND BNK.BBCODE = OPE.BRANCH_CODE ";
        //dql += "LEFT OUTER JOIN QUOTATION.BANCASSURANCE_ME ME ";
        //dql += "ON ME.SLICODE = OPE.MECODE ";
        //dql += "WHERE OPE.PRID = 'ODP' ";

        dql += "SELECT OPE.POLNO, (OPE.TITLE || '.' || OPE.CUS_NAME) as CUSTOMER, OPE.NIC, to_char(OPE.POLICY_SDATE, 'dd/mm/yyyy') as POLICY_SDATE, to_char(OPE.POLICY_EDATE, 'dd/mm/yyyy') as POLICY_EDATE, PRMIUM.SUMINSURD, PRMIUM.TOT_PREMIUM,  OPE.CONNOMOB, TO_CHAR(OPE.ENTDATE, 'dd/MM/yyyy') as ENTDATE, OPE.POL_STATUS, OPE.SRID, BNK.BBNAM, BNK.BBRNCH, OPE.PRNO as Proposal_No, OPE.BTUPE as NatureOfBusiness, OPE.CONNOMOB AS ContactNumber, PRMIUM.NETPREMIUM, OPE.MECODE, ME.BRANCH, ME.REGION,  PRMIUM.POLICY_FEE FROM QUOTATION.ODP_PROPOSAL_ENTRY OPE ";
        dql += "INNER JOIN QUOTATION.ODP_PREMIUM PRMIUM ";
        dql += "ON OPE.SRID = PRMIUM.SRID ";
        dql += "INNER JOIN GENPAY.BNKBRN BNK ";
        dql += "ON BNK.BCODE = OPE.BANK_CODE AND BNK.BBCODE = OPE.BRANCH_CODE ";
        dql += "LEFT OUTER JOIN QUOTATION.BANCASSURANCE_ME ME ";
        dql += "ON ME.SLICODE = OPE.MECODE ";
        dql += "WHERE OPE.PRID = 'ODP' ";

        if (!String.IsNullOrEmpty(status) && status != "A")
            dql += " AND OPE.POL_STATUS = '" + status + "'";

        if (!String.IsNullOrEmpty(bank_code.ToString()) && bank_code > 0)
            dql += " AND OPE.BANK_CODE =" + bank_code;

        if (!String.IsNullOrEmpty(branch_code.ToString()) && branch_code > 0)
            dql += " AND OPE.BRANCH_CODE =" + branch_code;

        if (!String.IsNullOrEmpty(start_date) && !String.IsNullOrEmpty(end_date))
        {
            dql += " AND trunc(OPE.ENTDATE) BETWEEN TO_DATE('" + start_date + "', 'DD/MM/YYYY') AND TO_DATE('" + end_date + "', 'DD/MM/YYYY')";
        }

        dql += " ORDER BY BNK.BBRNCH, ENTDATE  ASC";
        return dql;
    }


    //fire renewal SMS project query

    //--------------------- RENEWAL ----------------------------------------------------------------
    //get fire renewal sms data from bau.fire_renewals table
    public string GetFireRenewalSMSDetails(string start_date, string end_date, string stsus, string department, string policyNo, string branch, string subDpt, string engProduct, string fireProd, string busiUnit)
    {
        string sql = "";

        if (stsus == "N")
        {
            sql = "select fr.*, p.adminfee as adminFeePre,  ";
            sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
            sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
            sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
            sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
            sql += "WHERE fr.RN_SMS_STATUS = 'N' and (fr.int_claim_count = 0 OR fr.int_claim_count IS NULL) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
            sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(department))
            {
                sql += "  AND fr.DEPARTMENT = '" + department + "' ";
            }

            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND fr.policy_no = '" + policyNo + "' ";
            }

            if(subDpt == "EN")
            {
                sql += " and fr.PRODUCT_CODE IN('FEE', 'FMB', 'FPV', 'CMI', 'FPM', 'FCM', 'FCS') ";
            }
            if (subDpt == "FI")
            {
                sql += " and fr.PRODUCT_CODE IN('FPD', 'FBP', 'FTC', 'FHC', 'FRT', 'FCL') ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                string branchno = branch.PadLeft(3, '0');
                //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                //sql += " AND ( ";
                //sql += " CASE ";
                //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                //sql += "         SUBSTR( ";
                //sql += "             fr.policy_no, ";
                //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                //sql += "         ) ";
                //sql += " ";
                //sql += "     ELSE ";
                //sql += "         SUBSTR( ";
                //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                //sql += "         ) ";
                //sql += " END ";
                //sql += ") = '" + branchno + "'";

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 fr.policy_no, ";
                sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";




            }

            if (!string.IsNullOrEmpty(branch) && branch == "10")
            {
                if (busiUnit == "0")
                {
                    sql += " and fr.BRANCH_CODE != 334 and fr.BRANCH_CODE != 333 ";
                }
                else if(busiUnit == "1")
                {
                    sql += " and fr.BRANCH_CODE != 334 and fr.BRANCH_CODE != 333 ";
                }
                else
                {
                    sql += " and fr.BRANCH_CODE = '" + busiUnit + "' ";
                }
            }

            sql += " ORDER BY fr.expire_date ";
        }
        else if (stsus == "SUM_CHANGED")
        {
            sql = "select fr.*, p.adminfee as adminFeePre, ";
            sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
            sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
            sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
            sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
            sql += "WHERE fr.RN_SMS_STATUS = 'N' and (fr.int_claim_count = 0 OR fr.int_claim_count IS NULL) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
            sql += " and (fr.ENDORSE_PREMIUM > 0 ) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(department))
            {
                sql += "  AND fr.DEPARTMENT = '" + department + "' ";
            }

            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND fr.policy_no = '" + policyNo + "' ";
            }

            if (subDpt == "EN")
            {
                sql += " and fr.PRODUCT_CODE IN('FEE', 'FMB', 'FPV', 'CMI', 'FPM', 'FCM', 'FCS') ";
            }
            if (subDpt == "FI")
            {
                sql += " and fr.PRODUCT_CODE IN('FPD', 'FBP', 'FTC', 'FHC', 'FRT', 'FCL') ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                string branchno = branch.PadLeft(3, '0');
                //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                //sql += " AND ( ";
                //sql += " CASE ";
                //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                //sql += "         SUBSTR( ";
                //sql += "             fr.policy_no, ";
                //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                //sql += "         ) ";
                //sql += " ";
                //sql += "     ELSE ";
                //sql += "         SUBSTR( ";
                //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                //sql += "         ) ";
                //sql += " END ";
                //sql += ") = '" + branchno + "'";

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 fr.policy_no, ";
                sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";

            }

            if (!string.IsNullOrEmpty(branch) && branch == "10")
            {
                if (busiUnit == "0")
                {
                    sql += " and fr.BRANCH_CODE != 334 and fr.BRANCH_CODE != 333 ";
                }
                else if (busiUnit == "1")
                {
                    sql += " and fr.BRANCH_CODE != 334 and fr.BRANCH_CODE != 333 ";
                }
                else
                {
                    sql += " and fr.BRANCH_CODE = '" + busiUnit + "' ";
                }
            }

            sql += " ORDER BY fr.expire_date ";
        }
        else if (stsus == "WC")
        {
            sql = "select fr.*, p.adminfee as adminFeePre, ";
            sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
            sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
            sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
            sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
            sql += "WHERE fr.RN_SMS_STATUS = 'N' and (fr.int_claim_count > 0 ) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
            sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(department))
            {
                sql += "  AND fr.DEPARTMENT = '" + department + "' ";
            }

            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND fr.policy_no = '" + policyNo + "' ";
            }

            if (subDpt == "EN")
            {
                sql += " and fr.PRODUCT_CODE IN('FEE', 'FMB', 'FPV', 'CMI', 'FPM', 'FCM', 'FCS') ";
            }
            if (subDpt == "FI")
            {
                sql += " and fr.PRODUCT_CODE IN('FPD', 'FBP', 'FTC', 'FHC', 'FRT', 'FCL') ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                string branchno = branch.PadLeft(3, '0');
                //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                //sql += " AND ( ";
                //sql += " CASE ";
                //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                //sql += "         SUBSTR( ";
                //sql += "             fr.policy_no, ";
                //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                //sql += "         ) ";
                //sql += " ";
                //sql += "     ELSE ";
                //sql += "         SUBSTR( ";
                //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                //sql += "         ) ";
                //sql += " END ";
                //sql += ") = '" + branchno + "'";

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 fr.policy_no, ";
                sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";
            }

            if (!string.IsNullOrEmpty(branch) && branch == "10")
            {
                if (busiUnit == "0")
                {
                    sql += " and fr.BRANCH_CODE != 334 and fr.BRANCH_CODE != 333 ";
                }
                else if (busiUnit == "1")
                {
                    sql += " and fr.BRANCH_CODE != 334 and fr.BRANCH_CODE != 333 ";
                }
                else
                {
                    sql += " and fr.BRANCH_CODE = '" + busiUnit + "' ";
                }
            }

            sql += " ORDER BY fr.expire_date ";
        }
        else if (stsus == "WPPWC")
        {
            sql = "select fr.*, p.adminfee as adminFeePre, ";
            sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
            sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
            sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
            sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
            sql += "WHERE fr.RN_SMS_STATUS = 'N' AND (fr.cancel_status = 'PPW Canceled') ";

            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(department))
            {
                sql += "  AND fr.DEPARTMENT = '" + department + "' ";
            }

            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND fr.policy_no = '" + policyNo + "' ";
            }

            if (subDpt == "EN")
            {
                sql += " and fr.PRODUCT_CODE IN('FEE', 'FMB', 'FPV', 'CMI', 'FPM', 'FCM', 'FCS') ";
            }
            if (subDpt == "FI")
            {
                sql += " and fr.PRODUCT_CODE IN('FPD', 'FBP', 'FTC', 'FHC', 'FRT', 'FCL') ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                string branchno = branch.PadLeft(3, '0');
                //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                //sql += " AND ( ";
                //sql += " CASE ";
                //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                //sql += "         SUBSTR( ";
                //sql += "             fr.policy_no, ";
                //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                //sql += "         ) ";
                //sql += " ";
                //sql += "     ELSE ";
                //sql += "         SUBSTR( ";
                //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                //sql += "         ) ";
                //sql += " END ";
                //sql += ") = '" + branchno + "'";

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 fr.policy_no, ";
                sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";
            }

            if (!string.IsNullOrEmpty(branch) && branch == "10")
            {
                if (busiUnit == "0")
                {
                    sql += " and fr.BRANCH_CODE != 334 and fr.BRANCH_CODE != 333 ";
                }
                else if (busiUnit == "1")
                {
                    sql += " and fr.BRANCH_CODE != 334 and fr.BRANCH_CODE != 333 ";
                }
                else
                {
                    sql += " and fr.BRANCH_CODE = '" + busiUnit + "' ";
                }
            }

            sql += " ORDER BY fr.expire_date ";
        }
        else if (stsus == "SUM_N_CHANGED")
        {
            sql = "select fr.*, p.adminfee as adminFeePre, ";
            sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
            sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
            sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
            sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
            sql += "WHERE fr.SUM_CHA_STATUS = 'N' and RN_SMS_STATUS = 'A' and (fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL) and (fr.int_claim_count = 0 OR fr.int_claim_count IS NULL) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
            sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(department))
            {
                sql += "  AND fr.DEPARTMENT = '" + department + "' ";
            }

            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND fr.policy_no = '" + policyNo + "' ";
            }

            if (subDpt == "EN")
            {
                sql += " and fr.PRODUCT_CODE IN('FEE', 'FMB', 'FPV', 'CMI', 'FPM', 'FCM', 'FCS') ";
            }
            if (subDpt == "FI")
            {
                sql += " and fr.PRODUCT_CODE IN('FPD', 'FBP', 'FTC', 'FHC', 'FRT', 'FCL') ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                string branchno = branch.PadLeft(3, '0');
                //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                //sql += " AND ( ";
                //sql += " CASE ";
                //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                //sql += "         SUBSTR( ";
                //sql += "             fr.policy_no, ";
                //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                //sql += "         ) ";
                //sql += " ";
                //sql += "     ELSE ";
                //sql += "         SUBSTR( ";
                //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                //sql += "         ) ";
                //sql += " END ";
                //sql += ") = '" + branchno + "'";

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 fr.policy_no, ";
                sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";
            }

            if (!string.IsNullOrEmpty(branch) && branch == "10")
            {
                if (busiUnit == "0")
                {
                    sql += " and fr.BRANCH_CODE != 334 and fr.BRANCH_CODE != 333 ";
                }
                else if (busiUnit == "1")
                {
                    sql += " and fr.BRANCH_CODE != 334 and fr.BRANCH_CODE != 333 ";
                }
                else
                {
                    sql += " and fr.BRANCH_CODE = '" + busiUnit + "' ";
                }
            }

            sql += " ORDER BY fr.expire_date ";
        }
        else
        {

        }


        return sql;

    }

    public string Get_SSCValueANDVat(double TotalAmount, string SysDate)
    {
        string SSC_ValANDVat = string.Empty;

        try
        {
            oconn.Open();

            string stored_procedure_name = "GENPAY.CALCULATE_NBL_AND_VAT";

            // create the command object
            OracleCommand cmd = oconn.CreateCommand();
            cmd.Connection = oconn;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = stored_procedure_name;
            //cmd.BindByName = true;

            //Oracle Parameters necessary for the p_saltedhash function          
            cmd.Parameters.AddWithValue("taxLiableAmount", TotalAmount);
            cmd.Parameters.AddWithValue("requestDate", SysDate);

            OracleParameter NBLValue = new OracleParameter("nblAmount", OracleType.Double);
            NBLValue.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(NBLValue);

            OracleParameter VATValue = new OracleParameter("vatAmount", OracleType.Double);
            VATValue.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(VATValue);

            // execute the pl/sql block
            cmd.ExecuteNonQuery();

            SSC_ValANDVat = NBLValue.Value.ToString() + "&" + VATValue.Value;
        }
        catch (Exception ex)
        {

        }
        finally
        {
            oconn.Close();
        }

        return SSC_ValANDVat;

    }

    public double Get_VAT_Precentage()
    {
        string sql = "SELECT NVL(TAX_VALUE,0) FROM BCOMMON.TAX WHERE(trunc(START_DATE) = (SELECT trunc(MAX(START_DATE)) AS EXPR1 "
            + "FROM BCOMMON.TAX TAX_1 WHERE(trunc(START_DATE) <= trunc(sysdate)) AND(TAX_TYPE_ID = 'VAT'))) AND(TAX_TYPE_ID = 'VAT')";

        double VAT_Prec = 0.00;

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        using (OracleCommand cmd = new OracleCommand(sql, oconn))
        {


            OracleDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                if (!reader.IsDBNull(0)) { VAT_Prec = Convert.ToDouble(reader[0].ToString()); }
            }
            reader.Close();
            cmd.Parameters.Clear();
        }

        if (oconn.State == ConnectionState.Open)
        {
            oconn.Close();
        }
        return VAT_Prec;
    }


    //public bool InsertIntoRenwalMasterTemp(List<FireRenewalMast.FireRenewalMastClass> details)
    //{
    //    bool result = true;

    //    foreach (var detail in details)
    //    {
    //        // Constructing the SQL insert query using FireRenewalMast properties
    //        string SQL = "INSERT INTO SLIC_CNOTE.RENEWAL_MASTER_TEMP ( " +
    //                     "RNDEPT, RNPTP, RNPOL, RNYR, RNMNTH, RNSTDT, RNENDT, RNAGCD, RNNET, RNRCC, RNTC, " +
    //                     "RNPOLFEE, RNVAT, RNNBT, RNTOT, RNNAM, RNADD1, RNADD2, RNADD3, RNADD4, " +
    //                     "RNNIC, RNCNT, RNREF, RNFBR, RN_ADMINFEE, RNDATE, RN_BY, RN_IP, RN_BRCD ) " +
    //                     "VALUES ('" + detail.RNDEPT + "', '" + detail.RNPTP + "', '" + detail.RNPOL + "', " +
    //                     detail.RNYR + ", " + detail.RNMNTH + ", " +
    //                     "TO_DATE('" + detail.RNSTDT + "', 'YYYY-MM-DD'), TO_DATE('" + detail.RNENDT + "', 'YYYY-MM-DD'), " +
    //                     detail.RNAGCD + ", " + detail.RNNET + ", " + detail.RNRCC + ", " + detail.RNTC + ", " +
    //                     detail.RNPOLFEE + ", " + detail.RNVAT + ", " + detail.RNNBT + ", " + detail.RNTOT + ", " +
    //                     "'" + detail.RNNAM + "', '" + detail.RNADD1 + "', '" + detail.RNADD2 + "', '" + detail.RNADD3 + "', '" + detail.RNADD4 + "', " +
    //                     "'" + detail.RNNIC + "', '" + detail.RNCNT + "', '" + detail.RNREF + "', " + detail.RNFBR + ", " + detail.RN_ADMINFEE + ", " +
    //                     "TO_DATE('" + DateTime.Now.ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'), '" + HttpContext.Current.User.Identity.Name + "', '" + HttpContext.Current.Request.UserHostAddress + "', '" + "BranchCode" + "')";

    //        // Execute the query for each FireRenewalMast in the list
    //        bool queryResult = orcle_trans.ExecuteInsertQuery(SQL);

    //        // If any query fails, mark the result as false
    //        if (!queryResult)
    //        {
    //            result = false;
    //            break;  // Exit the loop as we encountered an issue with inserting
    //        }
    //    }

    //    return result;
    //}

    //get agent mobile no
    //public string Get_AGent(int agntCd)
    //{
    //    string agtMobNo = string.Empty;

    //    try
    //    {
    //        string sql = "select agency, int, name, phmob from agent.agent where agency = " + agntCd + " ";



    //        if (oconn.State != ConnectionState.Open)
    //        {
    //            oconn.Open();
    //        }
    //        using (OracleCommand cmd = new OracleCommand(sql, oconn))
    //        {


    //            OracleDataReader reader = cmd.ExecuteReader();

    //            while (reader.Read())
    //            {
    //                if (!reader.IsDBNull(0)) { agtMobNo = reader[3].ToString(); }
    //            }
    //            reader.Close();
    //            cmd.Parameters.Clear();
    //        }

    //        if (oconn.State == ConnectionState.Open)
    //        {
    //            oconn.Close();
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //    finally
    //    {
    //        oconn.Close();
    //    }

    //    return agtMobNo;
    //}

    public string Get_AGent(int agntCd, OracleTransaction transaction)
    {
        string agtMobNo = string.Empty;

        try
        {
            string sql = "SELECT agency, int, name, phmob FROM agent.agent WHERE agency = :agency";

            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                cmd.Transaction = transaction; // Attach the transaction
                cmd.Parameters.Add(":agency", agntCd);

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (!reader.IsDBNull(3))
                        {
                            string rawMobile = reader.GetString(3);
                            rawMobile = rawMobile.Trim();
                            if (rawMobile.StartsWith("0"))
                            {
                                rawMobile = rawMobile.Substring(1);
                            }

                            agtMobNo = "94" + rawMobile;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error in Get_AGent: " + ex.Message);
        }

        return agtMobNo;
    }


    public bool InsertIntoRenwalMasterTemp(List<FireRenewalMast.FireRenewalMastClass> details, string userId, string branchId)
    {
        bool result = true;
        // Begin transaction
        OracleTransaction transaction = null;
        LogFile Err = new LogFile();
        //string logPath = HttpContext.Current.Server.MapPath("~D:\WebLogs/Error.txt");

        string logPath = @"D:\WebLogs\FireRenewalErrorlg.txt";

        try
        {
            

            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            // Start a new transaction
            transaction = oconn.BeginTransaction();

            foreach (var detail in details)
            {

                // Constructing the SQL insert query using FireRenewalMast properties
                string SQL = "INSERT INTO SLIC_CNOTE.RENEWAL_MASTER_TEMP ( " +
                             "RNDEPT, RNPTP, RNPOL, RNYR, RNMNTH, RNSTDT, RNENDT, RNAGCD, RNNET, RNRCC, RNTC, " +
                             "RNPOLFEE, RNVAT, RNNBT, RNTOT, RNNAM, RNADD1, RNADD2, RNADD3, " +
                             "RNNIC, RNCNT, RNREF, RNFBR, RN_ADMINFEE, RNDATE, RN_BY, RN_IP, RN_BRCD, RNSUMINSUR, SUMNOTCHASTATUS ) " +
                             "VALUES ('" + detail.RNDEPT + "', '" + detail.RNPTP + "', '" + detail.RNPOL + "', " +
                             detail.RNYR + ", " + detail.RNMNTH + ", " +
                             "TO_DATE('" + DateTime.ParseExact(detail.RNSTDT, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'), TO_DATE('" + DateTime.ParseExact(detail.RNENDT, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'), " +
                             detail.RNAGCD + ", " + detail.RNNET + ", " + detail.RNRCC + ", " + detail.RNTC + ", " +
                             detail.RNPOLFEE + ", " + detail.RNVAT + ", " + detail.RNNBT + ", " + detail.RNTOT + ", " +
                             "'" + detail.RNNAM + "', '" + detail.RNADD1 + "', '" + detail.RNADD2 + "', '" + detail.RNADD3 + "', " +
                             "'" + detail.RNNIC + "', '" + detail.RNCNT + "', '" + detail.RNREF + "', '0', " + detail.RN_ADMINFEE + ", " +
                             "TO_DATE('" + DateTime.Now.ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'), '" + userId + "', '" + HttpContext.Current.Request.UserHostAddress + "', '" + branchId + "', " + detail.RNSUMINSUR + ", '" + detail.SUMNOTCHASTATUS + "')";

                // Execute the query for each FireRenewalMast in the list using the transaction
                OracleCommand cmd = new OracleCommand(SQL, oconn);
                cmd.Transaction = transaction;  // Associate the command with the transaction

                int rowsAffected = cmd.ExecuteNonQuery();  // Execute the SQL command

                if (rowsAffected <= 0)
                {
                    string errorDetails =
                    " " + Environment.NewLine + "Insert failed for:: " + detail.RNPOL + Environment.NewLine +
                    "rowsAffected " + rowsAffected + Environment.NewLine +
                    "SQL: " + SQL + Environment.NewLine +
                    "RNDEPT: " + detail.RNDEPT + Environment.NewLine +
                    "RNPTP: " + detail.RNPTP + Environment.NewLine +
                    "RNPOL: " + detail.RNPOL + Environment.NewLine +
                    "RNYR: " + detail.RNYR + Environment.NewLine +
                    "RNMNTH: " + detail.RNMNTH + Environment.NewLine +
                    "RNSTDT: " + detail.RNSTDT + Environment.NewLine +
                    "RNENDT: " + detail.RNENDT + Environment.NewLine +
                    "RNAGCD: " + detail.RNAGCD + Environment.NewLine +
                    "RNNET: " + detail.RNNET + Environment.NewLine +
                    "RNRCC: " + detail.RNRCC + Environment.NewLine +
                    "RNTC: " + detail.RNTC + Environment.NewLine +
                    "RNPOLFEE: " + detail.RNPOLFEE + Environment.NewLine +
                    "RNVAT: " + detail.RNVAT + Environment.NewLine +
                    "RNNBT: " + detail.RNNBT + Environment.NewLine +
                    "RNTOT: " + detail.RNTOT + Environment.NewLine +
                    "RNNAM: " + detail.RNNAM + Environment.NewLine +
                    "RNADD1: " + detail.RNADD1 + Environment.NewLine +
                    "RNADD2: " + detail.RNADD2 + Environment.NewLine +
                    "RNADD3: " + detail.RNADD3 + Environment.NewLine +
                    "RNNIC: " + detail.RNNIC + Environment.NewLine +
                    "RNCNT: " + detail.RNCNT + Environment.NewLine +
                    "RNREF: " + detail.RNREF + Environment.NewLine +
                    "RN_ADMINFEE: " + detail.RN_ADMINFEE + Environment.NewLine +
                    "RNSUMINSUR: " + detail.RNSUMINSUR + Environment.NewLine +
                    "SUMNOTCHASTATUS: " + detail.SUMNOTCHASTATUS + Environment.NewLine;

                    Err.ErrorLog(logPath, errorDetails);


                    //Err.ErrorLog(logPath, "insert failed for: " + detail.RNPOL + " SQL1: " + SQL + " ");

                    result = false; // Mark as false if no rows were affected
                    break; // Exit loop on failure
                }
                else
                {
                    string updateSQL = "UPDATE slic_cnote.fire_renewals_sms_tbl " +
                   "SET rn_sms_status = :status, updated_date = SYSDATE " +
                   "WHERE policy_no = :policyNo AND year = :year AND month = :month";

                    OracleCommand updateCmd = new OracleCommand(updateSQL, oconn);
                    updateCmd.Transaction = transaction;
                    updateCmd.Parameters.Add(":status", "I");
                    updateCmd.Parameters.Add(":policyNo", detail.RNPOL);
                    updateCmd.Parameters.Add(":year", detail.RNYR);
                    updateCmd.Parameters.Add(":month", detail.RNMNTH);
                    int re = updateCmd.ExecuteNonQuery();

                    //update momas telNo 1 here 

                    string updateSQL2 = "update genpay.momas set fmtel1 = :PIN_fmtel1, " +
                        " FMSUM = :pin_fmsum, FMPRM = :pin_fmprm, FMRCC = :pin_fmrcc, FMTC = :pin_rmtc, FMAGT = :pin_fmagt, " +
                        " FMPOF = :pin_fmpof, fmvat = :pin_fmvat, fmnbl = :pin_fmnbl, fmces = :pin_fmces " +
                        " where fmpol = :PIN_fmpol and fmdept = 'F' ";

                    string pin_fmtel1 = detail.RNCNT.StartsWith("94") ? detail.RNCNT.Substring(2) : detail.RNCNT;

                    OracleCommand updateCmd2 = new OracleCommand(updateSQL2, oconn);
                    updateCmd2.Transaction = transaction;
                    updateCmd2.Parameters.Add(":PIN_fmtel1", pin_fmtel1);
                    updateCmd2.Parameters.Add(":PIN_fmpol", detail.RNPOL);
                    updateCmd2.Parameters.Add(":pin_fmsum", detail.RNSUMINSUR);
                    updateCmd2.Parameters.Add(":pin_fmprm", detail.RNNET);
                    updateCmd2.Parameters.Add(":pin_fmrcc", detail.RNRCC);
                    updateCmd2.Parameters.Add(":pin_rmtc", detail.RNTC);
                    updateCmd2.Parameters.Add(":pin_fmagt", detail.RNAGCD);
                    //updateCmd2.Parameters.Add(":pin_fmnam", detail.RNNAM);
                    updateCmd2.Parameters.Add(":pin_fmpof", detail.RNPOLFEE);
                    updateCmd2.Parameters.Add(":pin_fmvat", detail.RNVAT);
                    updateCmd2.Parameters.Add(":pin_fmnbl", detail.RNNBT);
                    updateCmd2.Parameters.Add(":pin_fmces", detail.RN_ADMINFEE);
                    int momasUpdateRe = updateCmd2.ExecuteNonQuery();

                    if (re <= 0 && momasUpdateRe <= 0)
                    {
                        string errorDetails =
                        " " + Environment.NewLine + "Update failed for:: " + detail.RNPOL + Environment.NewLine +
                        "rowsAffected: " + rowsAffected + "update1 : " + re + ", Update2 : "+ momasUpdateRe +  Environment.NewLine +
                        "SQL: " + SQL + Environment.NewLine +
                        "RNDEPT: " + detail.RNDEPT + Environment.NewLine +
                        "RNPTP: " + detail.RNPTP + Environment.NewLine +
                        "RNPOL: " + detail.RNPOL + Environment.NewLine +
                        "RNYR: " + detail.RNYR + Environment.NewLine +
                        "RNMNTH: " + detail.RNMNTH + Environment.NewLine +
                        "RNSTDT: " + detail.RNSTDT + Environment.NewLine +
                        "RNENDT: " + detail.RNENDT + Environment.NewLine +
                        "RNAGCD: " + detail.RNAGCD + Environment.NewLine +
                        "RNNET: " + detail.RNNET + Environment.NewLine +
                        "RNRCC: " + detail.RNRCC + Environment.NewLine +
                        "RNTC: " + detail.RNTC + Environment.NewLine +
                        "RNPOLFEE: " + detail.RNPOLFEE + Environment.NewLine +
                        "RNVAT: " + detail.RNVAT + Environment.NewLine +
                        "RNNBT: " + detail.RNNBT + Environment.NewLine +
                        "RNTOT: " + detail.RNTOT + Environment.NewLine +
                        "RNNAM: " + detail.RNNAM + Environment.NewLine +
                        "RNADD1: " + detail.RNADD1 + Environment.NewLine +
                        "RNADD2: " + detail.RNADD2 + Environment.NewLine +
                        "RNADD3: " + detail.RNADD3 + Environment.NewLine +
                        "RNNIC: " + detail.RNNIC + Environment.NewLine +
                        "RNCNT: " + detail.RNCNT + Environment.NewLine +
                        "RNREF: " + detail.RNREF + Environment.NewLine +
                        "RN_ADMINFEE: " + detail.RN_ADMINFEE + Environment.NewLine +
                        "RNSUMINSUR: " + detail.RNSUMINSUR + Environment.NewLine +
                        "SUMNOTCHASTATUS: " + detail.SUMNOTCHASTATUS + Environment.NewLine+
                        "pin_fmtel1 : " + pin_fmtel1 + Environment.NewLine;

                        Err.ErrorLog(logPath, errorDetails);

                        //Err.ErrorLog(logPath, "Update failed for: " + detail.RNPOL + " SQL1: " + updateSQL + ", SQL2: " + updateSQL2);

                        result = false; // Mark as false if no rows were affected
                        break; // Exit loop on failure

                    }else if(re > 0 && momasUpdateRe > 0)
                    {

                    }
                    else
                    {
                        string errorDetails =
                        " "+Environment.NewLine + "Update failed for:: " + detail.RNPOL + Environment.NewLine +
                        "rowsAffected : " + rowsAffected + "update1 : " + re + ", Update2 : " + momasUpdateRe + Environment.NewLine +
                        "SQL: " + SQL + Environment.NewLine +
                        "RNDEPT: " + detail.RNDEPT + Environment.NewLine +
                        "RNPTP: " + detail.RNPTP + Environment.NewLine +
                        "RNPOL: " + detail.RNPOL + Environment.NewLine +
                        "RNYR: " + detail.RNYR + Environment.NewLine +
                        "RNMNTH: " + detail.RNMNTH + Environment.NewLine +
                        "RNSTDT: " + detail.RNSTDT + Environment.NewLine +
                        "RNENDT: " + detail.RNENDT + Environment.NewLine +
                        "RNAGCD: " + detail.RNAGCD + Environment.NewLine +
                        "RNNET: " + detail.RNNET + Environment.NewLine +
                        "RNRCC: " + detail.RNRCC + Environment.NewLine +
                        "RNTC: " + detail.RNTC + Environment.NewLine +
                        "RNPOLFEE: " + detail.RNPOLFEE + Environment.NewLine +
                        "RNVAT: " + detail.RNVAT + Environment.NewLine +
                        "RNNBT: " + detail.RNNBT + Environment.NewLine +
                        "RNTOT: " + detail.RNTOT + Environment.NewLine +
                        "RNNAM: " + detail.RNNAM + Environment.NewLine +
                        "RNADD1: " + detail.RNADD1 + Environment.NewLine +
                        "RNADD2: " + detail.RNADD2 + Environment.NewLine +
                        "RNADD3: " + detail.RNADD3 + Environment.NewLine +
                        "RNNIC: " + detail.RNNIC + Environment.NewLine +
                        "RNCNT: " + detail.RNCNT + Environment.NewLine +
                        "RNREF: " + detail.RNREF + Environment.NewLine +
                        "RN_ADMINFEE: " + detail.RN_ADMINFEE + Environment.NewLine +
                        "RNSUMINSUR: " + detail.RNSUMINSUR + Environment.NewLine +
                        "SUMNOTCHASTATUS: " + detail.SUMNOTCHASTATUS + Environment.NewLine +
                        "pin_fmtel1 : " + pin_fmtel1 + Environment.NewLine;

                        Err.ErrorLog(logPath, errorDetails);

                        result = false; // Mark as false if no rows were affected
                        break; // Exit loop on failure
                    }

                }
            }

            // If all records are inserted successfully, commit the transaction
            if (result)
            {
                transaction.Commit();
            }
            else
            {
                // If any record fails, roll back the entire transaction
                transaction.Rollback();
            }
        }
        catch (Exception ex)
        {
            
            Err.ErrorLog(logPath, ex.Message);

            // Handle exceptions and roll back the transaction in case of error
            if (transaction != null)
            {
                transaction.Rollback();
            }

            result = false;
            // Optionally, log the exception
            // Console.WriteLine(ex.Message);
        }
        finally
        {
            // Close the connection if it's still open
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return result;
    }


    //get pay file details for get policy details
    public string GetPayFileDetails(string polno)
    {
        string sql = "SELECT * FROM( SELECT * FROM genpay.payfle WHERE " +
                    " pmpol = '" + polno + "' ORDER BY pmdst DESC ) WHERE ROWNUM = 1; ";

        return sql;
    }


    //public string GetRefnoProcedure(string policyNo, string policyStartDate, string policyEndDate, string vehicleNo, string dept, string busitype)
    //{
    //    string refNo = string.Empty;


    //    using (OracleConnection con = orcl_con.GetConnection())
    //    {
    //        con.Open();
    //        using (OracleCommand cmd = new OracleCommand("SLIC_CNOTE.COMMON_RENEWAL_CR", con))
    //        {
    //            cmd.CommandType = CommandType.StoredProcedure;

    //            // Adding Input Parameters
    //            cmd.Parameters.Add("POLNO", OracleType.VarChar).Value = policyNo;
    //            cmd.Parameters.Add("polStart", OracleType.VarChar).Value = DateTime.Parse(policyStartDate).ToString("dd-MMM-yyyy");
    //            cmd.Parameters.Add("polEnd", OracleType.VarChar).Value = DateTime.Parse(policyEndDate).ToString("dd-MMM-yyyy");
    //            cmd.Parameters.Add("vehicleNo", OracleType.VarChar).Value = vehicleNo;
    //            cmd.Parameters.Add("dept", OracleType.VarChar).Value = dept;
    //            cmd.Parameters.Add("busitype", OracleType.VarChar).Value = busitype;

    //            // Adding Output Parameter for REFNO
    //            cmd.Parameters.Add("REFNO", OracleType.VarChar, 100).Direction = ParameterDirection.Output;

    //            cmd.ExecuteNonQuery();

    //            refNo = cmd.Parameters["REFNO"].Value.ToString();

    //        }
    //    }

    //    return refNo;
    //}

    public string GetRefnoProcedure(string policyNo, string policyStartDate, string policyEndDate, string vehicleNo, string dept, string busitype)
    {
        string refNo = string.Empty;
        LogFile Err = new LogFile();
        string logPath = @"D:\WebLogs\FireRenewalErrorlg.txt";

        try
        {
            DateTime sDate, eDate;

            if (DateTime.TryParseExact(policyStartDate.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out sDate))
            {
                policyStartDate = sDate.ToString("dd/MM/yyyy");
            }
            else
            {
                Err.ErrorLog(logPath, "Invalid format for policyStartDate: " + policyStartDate);
            }

            if (DateTime.TryParseExact(policyEndDate.Trim(), "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out eDate))
            {
                policyEndDate = eDate.ToString("dd/MM/yyyy");
            }
            else
            {
                Err.ErrorLog(logPath, "Invalid format for policyEndDate: " + policyEndDate);
            }


            using (OracleConnection con = orcl_con.GetConnection())
            {
                con.Open();
                using (OracleCommand cmd = new OracleCommand("SLIC_CNOTE.COMMON_RENEWAL_CR", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    string dd = sDate.ToString("dd-MMM-yyyy");
                    // Add Input Parameters
                    cmd.Parameters.Add("POLNO", OracleType.VarChar).Value = policyNo;
                    cmd.Parameters.Add("polStart", OracleType.VarChar).Value = sDate.ToString("dd-MMM-yyyy");
                    cmd.Parameters.Add("polEnd", OracleType.VarChar).Value = eDate.ToString("dd-MMM-yyyy");
                    cmd.Parameters.Add("vehicleNo", OracleType.VarChar).Value = vehicleNo;
                    cmd.Parameters.Add("dept", OracleType.VarChar).Value = dept;
                    cmd.Parameters.Add("busitype", OracleType.VarChar).Value = busitype;

                    // Add Output Parameter
                    cmd.Parameters.Add("REFNO", OracleType.VarChar, 100).Direction = ParameterDirection.Output;

                    cmd.ExecuteNonQuery();

                    refNo = cmd.Parameters["REFNO"].Value.ToString();

                }
            }
        }
        catch (Exception ex)
        {
            string errorDetails =
                "Procedure Error:" + Environment.NewLine +
                "policyNo: " + policyNo + Environment.NewLine +
                "policyStartDate (raw): " + policyStartDate + Environment.NewLine +
                "policyEndDate (raw): " + policyEndDate + Environment.NewLine +
                "vehicleNo: " + vehicleNo + Environment.NewLine +
                "dept: " + dept + Environment.NewLine +
                "busitype: " + busitype + Environment.NewLine +
                "Exception: " + ex.ToString();

            Err.ErrorLog(logPath, errorDetails);
        }

        return refNo;
    }


    public string GetApprovalTempDataFireRNSMS(string start_date, string end_date, string stsus, string policyNo, string branch, string busiUnit, string subDpt)
    {
        string sql = "";

        if (stsus == "N")
        {
            sql = " SELECT rt.*,st.*, p.adminfee as adminfeePRe FROM  SLIC_CNOTE.RENEWAL_MASTER_TEMP rt ";
            sql += " inner join slic_cnote.fire_renewals_sms_tbl st on st.policy_no = rt.rnpol and st.year = rt.rnyr and st.month = rt.rnmnth ";
            sql += " left join genpay.poltyp p on p.ptdep = st.department and p.pttyp = st.product_code ";
            sql += " where rnptp = 'FD_RENEWAL' and st.rn_sms_status = 'I' and (st.int_claim_count = 0 OR st.int_claim_count IS NULL) AND (st.cancel_status IS NULL OR st.cancel_status != 'PPW Canceled')  ";
            sql += " and (st.ENDORSE_PREMIUM = 0 OR st.ENDORSE_PREMIUM is null) and (st.OUTSTANDING = 0 or st.OUTSTANDING is null) ";
            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND rt.rnendt >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND rt.rnendt <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND rt.rnpol = '" + policyNo + "' ";
            }

            if (subDpt == "EN")
            {
                sql += " and st.PRODUCT_CODE IN('FEE', 'FMB', 'FPV', 'CMI', 'FPM', 'FCM', 'FCS') ";
            }
            if (subDpt == "FI")
            {
                sql += " and st.PRODUCT_CODE IN('FPD', 'FBP', 'FTC', 'FHC', 'FRT', 'FCL') ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                string branchno = branch.PadLeft(3, '0');
                //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                //sql += " AND ( ";
                //sql += " CASE ";
                //sql += "     WHEN INSTR(st.policy_no, '/') > 0 THEN ";
                //sql += "         SUBSTR( ";
                //sql += "             st.policy_no, ";
                //sql += "             INSTR(st.policy_no, '/') + 1, ";
                //sql += "             INSTR(st.policy_no, '/', INSTR(st.policy_no, '/') + 1) - INSTR(st.policy_no, '/') - 1 ";
                //sql += "         ) ";
                //sql += " ";
                //sql += "     ELSE ";
                //sql += "         SUBSTR( ";
                //sql += "             REGEXP_SUBSTR(st.policy_no, '[0-9]+'), 3, 3 ";
                //sql += "         ) ";
                //sql += " END ";
                //sql += ") = '" + branchno + "'";

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(st.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 st.policy_no, ";
                sql += "                 INSTR(st.policy_no, '/') + 1, ";
                sql += "                 INSTR(st.policy_no, '/', INSTR(st.policy_no, '/') + 1) - INSTR(st.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(st.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(st.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(st.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";

            }

            if (!string.IsNullOrEmpty(branch) && branch == "10")
            {
                if (busiUnit == "0")
                {
                    sql += " and st.BRANCH_CODE != 334 and st.BRANCH_CODE != 333 ";
                }
                else if (busiUnit == "1")
                {
                    sql += " and st.BRANCH_CODE != 334 and st.BRANCH_CODE != 333 ";
                }
                else
                {
                    sql += " and st.BRANCH_CODE = '" + busiUnit + "' ";
                }
            }

            sql += " ORDER BY rt.rnendt ";
        }
        else if (stsus == "WC")
        {
            sql = " SELECT rt.*,st.*, p.adminfee as adminfeePRe FROM  SLIC_CNOTE.RENEWAL_MASTER_TEMP rt ";
            sql += "  inner join slic_cnote.fire_renewals_sms_tbl st on st.policy_no = rt.rnpol and st.year = rt.rnyr and st.month = rt.rnmnth ";
            sql += " left join genpay.poltyp p on p.ptdep = st.department and p.pttyp = st.product_code ";
            sql += " where rnptp = 'FD_RENEWAL' and st.rn_sms_status = 'I' and (st.int_claim_count > 0 ) AND (st.cancel_status IS NULL OR st.cancel_status != 'PPW Canceled')";
            sql += " and (st.ENDORSE_PREMIUM = 0 OR st.ENDORSE_PREMIUM is null) and (st.OUTSTANDING = 0 or st.OUTSTANDING is null) ";

            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND rt.rnendt >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND rt.rnendt <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }
            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND rt.rnpol = '" + policyNo + "' ";
            }

            if (subDpt == "EN")
            {
                sql += " and st.PRODUCT_CODE IN('FEE', 'FMB', 'FPV', 'CMI', 'FPM', 'FCM', 'FCS') ";
            }
            if (subDpt == "FI")
            {
                sql += " and st.PRODUCT_CODE IN('FPD', 'FBP', 'FTC', 'FHC', 'FRT', 'FCL') ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                //sql += "  AND st.BRANCH_CODE = '" + branch + "' ";
                
                string branchno = branch.PadLeft(3, '0');
                //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                //sql += " AND ( ";
                //sql += " CASE ";
                //sql += "     WHEN INSTR(st.policy_no, '/') > 0 THEN ";
                //sql += "         SUBSTR( ";
                //sql += "             st.policy_no, ";
                //sql += "             INSTR(st.policy_no, '/') + 1, ";
                //sql += "             INSTR(st.policy_no, '/', INSTR(st.policy_no, '/') + 1) - INSTR(st.policy_no, '/') - 1 ";
                //sql += "         ) ";
                //sql += " ";
                //sql += "     ELSE ";
                //sql += "         SUBSTR( ";
                //sql += "             REGEXP_SUBSTR(st.policy_no, '[0-9]+'), 3, 3 ";
                //sql += "         ) ";
                //sql += " END ";
                //sql += ") = '" + branchno + "'";

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(st.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 st.policy_no, ";
                sql += "                 INSTR(st.policy_no, '/') + 1, ";
                sql += "                 INSTR(st.policy_no, '/', INSTR(st.policy_no, '/') + 1) - INSTR(st.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(st.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(st.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(st.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";

            }

            if (!string.IsNullOrEmpty(branch) && branch == "10")
            {
                if (busiUnit == "0")
                {
                    sql += " and st.BRANCH_CODE != 334 and st.BRANCH_CODE != 333 ";
                }
                else if (busiUnit == "1")
                {
                    sql += " and st.BRANCH_CODE != 334 and st.BRANCH_CODE != 333 ";
                }
                else
                {
                    sql += " and st.BRANCH_CODE = '" + busiUnit + "' ";
                }
            }


            sql += " ORDER BY rt.rnendt ";
        }
        else if (stsus == "SUM_CHANGED")
        {
            sql = " SELECT rt.*,st.*, p.adminfee as adminfeePRe FROM  SLIC_CNOTE.RENEWAL_MASTER_TEMP rt ";
            sql += "  inner join slic_cnote.fire_renewals_sms_tbl st on st.policy_no = rt.rnpol and st.year = rt.rnyr and st.month = rt.rnmnth ";
            sql += " left join genpay.poltyp p on p.ptdep = st.department and p.pttyp = st.product_code ";
            sql += " where rnptp = 'FD_RENEWAL' and st.rn_sms_status = 'I' and (st.int_claim_count =0 OR st.int_claim_count IS NULL) AND (st.cancel_status IS NULL OR st.cancel_status != 'PPW Canceled')";
            sql += " and (st.ENDORSE_PREMIUM > 0 ) and (st.OUTSTANDING = 0 or st.OUTSTANDING is null) ";

            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND rt.rnendt >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND rt.rnendt <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }
            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND rt.rnpol = '" + policyNo + "' ";
            }

            //if (!string.IsNullOrEmpty(branch) && branch != "0")
            //{
            //    sql += "  AND st.BRANCH_CODE = '" + branch + "' ";
            //}

            if (subDpt == "EN")
            {
                sql += " and st.PRODUCT_CODE IN('FEE', 'FMB', 'FPV', 'CMI', 'FPM', 'FCM', 'FCS') ";
            }
            if (subDpt == "FI")
            {
                sql += " and st.PRODUCT_CODE IN('FPD', 'FBP', 'FTC', 'FHC', 'FRT', 'FCL') ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                //sql += "  AND st.BRANCH_CODE = '" + branch + "' ";
                string branchno = branch.PadLeft(3, '0');

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(st.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 st.policy_no, ";
                sql += "                 INSTR(st.policy_no, '/') + 1, ";
                sql += "                 INSTR(st.policy_no, '/', INSTR(st.policy_no, '/') + 1) - INSTR(st.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(st.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(st.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(st.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";

            }

            if (!string.IsNullOrEmpty(branch) && branch == "10")
            {
                if (busiUnit == "0")
                {
                    sql += " and st.BRANCH_CODE != 334 and st.BRANCH_CODE != 333 ";
                }
                else if (busiUnit == "1")
                {
                    sql += " and st.BRANCH_CODE != 334 and st.BRANCH_CODE != 333 ";
                }
                else
                {
                    sql += " and st.BRANCH_CODE = '" + busiUnit + "' ";
                }
            }


            sql += " ORDER BY rt.rnendt ";
        }
        else if (stsus == "WPPWC")
        {
            sql = " SELECT rt.*,st.*, p.adminfee as adminfeePRe FROM  SLIC_CNOTE.RENEWAL_MASTER_TEMP rt ";
            sql += "  inner join slic_cnote.fire_renewals_sms_tbl st on st.policy_no = rt.rnpol and st.year = rt.rnyr and st.month = rt.rnmnth ";
            sql += " left join genpay.poltyp p on p.ptdep = st.department and p.pttyp = st.product_code ";
            sql += " where rnptp = 'FD_RENEWAL' and st.rn_sms_status = 'I' and (st.cancel_status = 'PPW Canceled') ";
            
            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND rt.rnendt >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND rt.rnendt <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }
            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND rt.rnpol = '" + policyNo + "' ";
            }

            //if (!string.IsNullOrEmpty(branch) && branch != "0")
            //{
            //    sql += "  AND st.BRANCH_CODE = '" + branch + "' ";
            //}

            if (subDpt == "EN")
            {
                sql += " and st.PRODUCT_CODE IN('FEE', 'FMB', 'FPV', 'CMI', 'FPM', 'FCM', 'FCS') ";
            }
            if (subDpt == "FI")
            {
                sql += " and st.PRODUCT_CODE IN('FPD', 'FBP', 'FTC', 'FHC', 'FRT', 'FCL') ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                //sql += "  AND st.BRANCH_CODE = '" + branch + "' ";
                string branchno = branch.PadLeft(3, '0');

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(st.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 st.policy_no, ";
                sql += "                 INSTR(st.policy_no, '/') + 1, ";
                sql += "                 INSTR(st.policy_no, '/', INSTR(st.policy_no, '/') + 1) - INSTR(st.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(st.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(st.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(st.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";

            }

            if (!string.IsNullOrEmpty(branch) && branch == "10")
            {
                if (busiUnit == "0")
                {
                    sql += " and st.BRANCH_CODE != 334 and st.BRANCH_CODE != 333 ";
                }
                else if (busiUnit == "1")
                {
                    sql += " and st.BRANCH_CODE != 334 and st.BRANCH_CODE != 333 ";
                }
                else
                {
                    sql += " and st.BRANCH_CODE = '" + busiUnit + "' ";
                }
            }


            sql += " ORDER BY rt.rnendt ";

        }
        else
        {

        }



        return sql;
    }

    //get client email address
    public string GetClintMailAddress(string policyNo)
    {
        string toEmail = string.Empty;

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }

        // First query: corporate email
        string sql1 = @"select office_email 
                    from CLIENTDB.CORPORATE_CUSTOMER @live
                    where CUSTOMER_ID = (select client_id from genpay.momas@live where fmpol = :policyNo)";

        using (OracleCommand cmd = new OracleCommand(sql1, oconn))
        {
            cmd.Parameters.Add(new OracleParameter("policyNo", policyNo));

            using (OracleDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read() && !reader.IsDBNull(0))
                {
                    toEmail = reader.GetString(0);
                }
            }
        }

        // If corporate email not found, check personal email
        if (string.IsNullOrEmpty(toEmail))
        {
            string sql2 = @"select personal_email 
                        from CLIENTDB.PERSONAL_CUSTOMER@live
                        where CUSTOMER_ID = (select client_id from genpay.momas@live where fmpol = :policyNo)";

            using (OracleCommand cmd = new OracleCommand(sql2, oconn))
            {
                cmd.Parameters.Add(new OracleParameter("policyNo", policyNo));

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read() && !reader.IsDBNull(0))
                    {
                        toEmail = reader.GetString(0);
                    }
                }
            }
        }

        if (oconn.State == ConnectionState.Open)
        {
            oconn.Close();
        }

        return toEmail;
    }


    //approve sms data list from renewal_temp table
    //insert to renewal mast tbl, update fire_Renewal_sms_tbl sms_status and updatedDate, update genpay.momas table cus mobile no, insert to sms.sms_gateway for customer and agent
    public string InsertIntoRenwalMaster(List<FireRenewalMast.FireRenewalMastClass> details, string userId, string branchId)
    {
        bool result = true;
        string link = string.Empty;
        string emailBody = string.Empty;
        string toEmail = string.Empty;
        // Begin transaction
        OracleTransaction transaction = null;

        LogFile Err = new LogFile();
        string logPath = @"D:\WebLogs\FireRenewalErrorlg.txt";


        try
        {
           

            foreach (var detail in details)
            {
                bool recordResult = true;

                try
                {
                    if (oconn.State != ConnectionState.Open)
                    {
                        oconn.Open();
                    }

                    // Start a new transaction
                    transaction = oconn.BeginTransaction();

                    // Constructing the SQL insert query using FireRenewalMast properties
                    string SQL = "INSERT INTO SLIC_CNOTE.RENEWAL_MASTER ( " +
                                 "RNDEPT, RNPTP, RNPOL, RNYR, RNMNTH, RNSTDT, RNENDT, RNAGCD, RNNET, RNRCC, RNTC, " +
                                 "RNPOLFEE, RNVAT, RNNBT, RNTOT, RNNAM, RNADD1, RNADD2, RNADD3, RNADD4, " +
                                 "RNNIC, RNCNT, RNREF, RNFBR, RN_ADMINFEE, RNDATE, RN_BY, RN_IP, RN_BRCD, RNSUMINSUR ) " +
                                 "VALUES ('" + detail.RNDEPT + "', '" + detail.RNPTP + "', '" + detail.RNPOL + "', " +
                                 detail.RNYR + ", " + detail.RNMNTH + ", " +
                                 "TO_DATE('" + DateTime.ParseExact(detail.RNSTDT, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'), TO_DATE('" + DateTime.ParseExact(detail.RNENDT, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'), " +
                                 detail.RNAGCD + ", " + detail.RNNET + ", " + detail.RNRCC + ", " + detail.RNTC + ", " +
                                 detail.RNPOLFEE + ", " + detail.RNVAT + ", " + detail.RNNBT + ", " + detail.RNTOT + ", " +
                                 "'" + detail.RNNAM + "', '" + detail.RNADD1 + "', '" + detail.RNADD2 + "', '" + detail.RNADD3 + "', '" + detail.RNADD4 + "', " +
                                 "'" + detail.RNNIC + "', '" + detail.RNCNT + "', '" + detail.RNREF + "', '" + detail.RNFBR + "', " + detail.RN_ADMINFEE + ", " +
                                 "TO_DATE('" + DateTime.ParseExact(detail.RNDATE, "dd/MM/yyyy", CultureInfo.InvariantCulture).ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'), '" + detail.RN_BY + "', '" + detail.RN_IP + "', '" + detail.RN_BRCD + "', " + detail.RNSUMINSUR + ")";


                    OracleCommand cmd = new OracleCommand(SQL, oconn);
                    cmd.Transaction = transaction;  // Associate the command with the transaction

                    int rowsAffected = cmd.ExecuteNonQuery();  // Execute the SQL command

                    if (rowsAffected <= 0)
                    {
                        recordResult = false; // Mark as false if no rows were affected
                        break; // Exit loop on failure
                    }
                    else
                    {
                        Oracle_Transaction Oracle_Trans = new Oracle_Transaction();
                        Execute_sql _Sql = new Execute_sql();

                        //Oracle_Trans.GetString(_Sql.DelteFromRenewalTemRenewalSMSp(detail.RNPOL));

                        //get agent moblile no 
                        string agentMobNo = this.Get_AGent(Convert.ToInt32(detail.RNAGCD), transaction);

                        //send sms to customer and agent with renewal notice web link
                        var en = new EncryptDecrypt();


                        //string webLink = "http://172.24.90.100:8084/Slicgeneral/FireRenewalSMSNoticeWebLink?polno=" + en.Encrypt(detail.RNPOL);
                        //string shortUrl = UrlShortener.ShortenUrl(webLink);



                        //replace this with live URL
                        //string longUrl = "http://172.24.90.100:8084/FRWebLk?polno=" + en.Encrypt(detail.RNPOL) +"&mobileNo="+ en.Encrypt(detail.RNCNT);

                        //live project link
                        string longUrl = "http://203.115.11.232:1010/RenewalNoticeWebDownload.aspx?polno=" + detail.RNPOL + "&mobileNo=" + detail.RNCNT;
                        string shortUrl = ShortenerHelper.ShortenUrl(longUrl);

                        //string txtMsg = "Fire Policy " + detail.RNPOL + " is expiring on " + detail.RNENDT + ". Renewal premium is Rs." + detail.RNTOT.ToString("N2") + " and which is subject to future claims until the renewal date. View Details, " + shortUrl + " ";

                        string txtMsg = string.Format(
                                "Fire Policy {0} is expiring on {1}. Renewal premium is Rs.{2:N2} and which is subject to future claims until the renewal date. View Details: {3}",
                                detail.RNPOL,
                                detail.RNENDT,
                                detail.RNTOT,
                                shortUrl
                            );

                        string errorDetails = " Mesage :  " + txtMsg + Environment.NewLine + " Short URL : " + shortUrl + Environment.NewLine;

                        emailBody = txtMsg;

                        //Err.ErrorLog(logPath, errorDetails);

                        int msgSent = this.Send_sms_to_customer_Agent(detail.RNCNT, detail.RNPOL, txtMsg);


                        if (!string.IsNullOrEmpty(agentMobNo) || agentMobNo != "")
                        {
                            int msgSent2 = this.Send_sms_to_customer_Agent(agentMobNo, detail.RNPOL, txtMsg);
                        }

                        if (msgSent <= 0)
                        {
                            recordResult = false; // Mark as false if no rows were affected
                            break;
                        }
                        else
                        {
                            string updateSQL = "UPDATE slic_cnote.fire_renewals_sms_tbl " +
                                                "SET rn_sms_status = :status, updated_date = SYSDATE " +
                                                "WHERE policy_no = :policyNo AND year = :year AND month = :month";

                            OracleCommand updateCmd = new OracleCommand(updateSQL, oconn);
                            updateCmd.Transaction = transaction;
                            updateCmd.Parameters.Add(":status", "A");
                            updateCmd.Parameters.Add(":policyNo", detail.RNPOL);
                            updateCmd.Parameters.Add(":year", detail.RNYR);
                            updateCmd.Parameters.Add(":month", detail.RNMNTH);
                            int up_FireRnSMSTbl = updateCmd.ExecuteNonQuery();

                            if (up_FireRnSMSTbl <= 0)
                            {
                                recordResult = false; // Mark as false if no rows were affected
                                break;
                            }
                            else
                            {
                                Oracle_Trans.GetString(_Sql.DelteFromRenewalTemRenewalSMSp(detail.RNPOL));
                                //for test 
                                link = "true";

                            }


                        }

                    }

                    if (recordResult)
                    {
                        transaction.Commit();

                        //for live
                        //toEmail = this.GetClintMailAddress(detail.RNPOL);

                        //for test
                        toEmail = "shalomid@srilankainsurance.com";

                        if (!string.IsNullOrEmpty(toEmail) && toEmail != "0")
                        {
                            try
                            {
                                SendEmailToClient(emailBody, toEmail, detail.RNPOL, detail.RNTOT);
                            }
                            catch (Exception ex)
                            {
                                // log email error but do not rollback since DB is already committed
                                LogFile Err1 = new LogFile();
                                Err1.ErrorLog(@"D:\WebLogs\FireRenewalErrorlg.txt", "Email error: " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch(Exception ex1)
                {
                    Err.ErrorLog(logPath, ex1.Message);
                }
                
            }

            // If all records are inserted successfully, commit the transaction
            //if (result)
            //{
            //    transaction.Commit();
            //}
            //else
            //{
            //    // If any record fails, roll back the entire transaction
            //    transaction.Rollback();
            //}
        }
        catch (Exception ex)
        {
            Err.ErrorLog(logPath, ex.Message);
            // Handle exceptions and roll back the transaction in case of error
            if (transaction != null)
            {
                transaction.Rollback();

            }            

        }
        finally
        {
            // Close the connection if it's still open
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        //for live
        return link;

        //for test
        //return link;
    }

    //fire renewal notoce email send function
    public void SendEmailToClient(string mailbody, string toemail, string policyNo, double rntot)
    {
        var _renewalemail = new FireRNSMSEmail();

        bool sendMail = _renewalemail.sendRenewalSMSMail(mailbody, toemail, policyNo, rntot);
    }

    //fire renewal sms send for agent and customer
    public int Send_sms_to_customer_Agent(string contactNumber, string polNo, string txt_body)
    {
        LogFile Err = new LogFile();
        string logPath = @"D:\WebLogs\FireRenewalErrorlg.txt";

        string errorDetails = " Mesage in db table :  " + txt_body + Environment.NewLine ;
        //Err.ErrorLog(logPath, errorDetails);

        //string REQ_ID, string BANK_CODE,

        string exe_Insert = "INSERT INTO SMS.SMS_GATEWAY(APPLICATION_ID, JOB_CATEGORY, SMS_TYPE, MOBILE_NUMBER, TEXT_MESSAGE, SHORT_CODE, RECORD_STATUS, JOB_OTHER_INFO) ";
        exe_Insert += "VALUES(:PIN_APPLICATION_ID, :PIN_JOB_CATEGORY, :PIN_SMS_TYPE, :PIN_MOBILE_NUMBER, :PIN_TEXT_MESSAGE, :PIN_SHORT_CODE, :PIN_RECORD_STATUS, :PIN_JOB_OTHER_INFO)";

        int sendCount = 0;

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


            cmd_backup.Parameters.Add(":PIN_APPLICATION_ID", OracleType.VarChar).Value = "FIRE_RN";
            cmd_backup.Parameters.Add(":PIN_JOB_CATEGORY", OracleType.VarChar).Value = "CAT151";
            cmd_backup.Parameters.Add(":PIN_SMS_TYPE", OracleType.VarChar).Value = "I";
            cmd_backup.Parameters.Add(":PIN_MOBILE_NUMBER", OracleType.VarChar).Value = contactNumber; //94777554194
            //cmd_backup.Parameters.Add(":PIN_TEXT_MESSAGE", OracleType.VarChar).Value = txt_body;//"Motor Quotation Request from Bank. Reference ID : " + reqId;

            var paramText = cmd_backup.Parameters.Add(":PIN_TEXT_MESSAGE", OracleType.VarChar);
            paramText.Value = txt_body;
            paramText.Size = 500;

            cmd_backup.Parameters.Add(":PIN_SHORT_CODE", OracleType.VarChar).Value = "SLIC";
            cmd_backup.Parameters.Add(":PIN_RECORD_STATUS", OracleType.VarChar).Value = "N";
            cmd_backup.Parameters.Add(":PIN_JOB_OTHER_INFO", OracleType.VarChar).Value = polNo;

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
            //Error_Code = ex.ErrorCode;
            //Error_Message = ex.Message;
            transaction.Rollback();
        }

        finally
        {
            con.Close();
            con.Dispose();
        }
        return sendCount;
    }

    public string DelteFromRenewalTemRenewalSMSp(string policyNo)
    {
        string SQL = "DELETE FROM SLIC_CNOTE.RENEWAL_MASTER_TEMP WHERE RNPOL = '" + policyNo + "' and rnptp = 'FD_RENEWAL'";

        return SQL;
    }

    public string GetMastData(string polNo)
    {
        string SQL = " SELECT * FROM SLIC_CNOTE.RENEWAL_MASTER m LEFT JOIN SLIC_CNOTE.fire_rn_sms_remarks sr ON sr.policy_no = m.rnpol ";
        SQL += " WHERE m.RNPOL = '" + polNo + "' AND m.rnptp = 'FD_RENEWAL' ";

        return SQL;
    }

    //get gempay.momas detais
    public string GetMomasData(string polNo)
    {
        string SQL = "SELECT * FROM  genpay.momas where fmpol = '" + polNo + "' ";

        return SQL;
    }

    //update customer mobile no 
    public int updateCusMobNo(string mobileNo, string policyNo)
    {
        int up_CusMobNo = 0;
        // Begin transaction
        OracleTransaction transaction = null;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            transaction = oconn.BeginTransaction();

            string updateSQL = "update genpay.momas set fmtel1 = :PIN_fmtel1 where fmpol = :PIN_fmpol and fmdept = 'F' ";


            OracleCommand updateCmd = new OracleCommand(updateSQL, oconn);
            updateCmd.Transaction = transaction;
            updateCmd.Parameters.Add(":PIN_fmtel1", mobileNo);
            updateCmd.Parameters.Add(":PIN_fmpol", policyNo);
            up_CusMobNo = updateCmd.ExecuteNonQuery();

            string mobileNoWithoutZero = mobileNo;
            if (mobileNo.StartsWith("0"))
            {
                mobileNoWithoutZero = mobileNo.Substring(1);
            }

            string updateSQL2 = "UPDATE slic_cnote.fire_renewals_sms_tbl SET phone_no1 = :mobNo2 WHERE policy_no = :pol_no2";

            OracleCommand updateCmd2 = new OracleCommand(updateSQL2, oconn);
            updateCmd2.Transaction = transaction;
            updateCmd2.Parameters.Add(":mobNo2", mobileNoWithoutZero);
            updateCmd2.Parameters.Add(":pol_no2", policyNo);

            int up_CusMobNo2 = updateCmd2.ExecuteNonQuery();

            if (up_CusMobNo > 0 && up_CusMobNo2 > 0)
            {
                up_CusMobNo = 1;
                transaction.Commit();

            }
            else
            {
                transaction.Rollback();
            }

        }

        catch (Exception ex)
        {
            if (transaction != null)
            {
                transaction.Rollback();
            }

        }
        finally
        {
            // Close the connection if it's still open
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }


        return up_CusMobNo;
    }

    //send sms for sum insured not changed list
    public bool SendSMS_SumNotChangedList(List<FireRenewalMast.FireRenewalMastClass> details, string userId, string branchId)
    {
        bool result = true;
        // Begin transaction
        OracleTransaction transaction = null;
        string toEmail = string.Empty;
        string emailBody = string.Empty;

        try
        {
            
            foreach (var detail in details)
            {
                try
                {
                    if (oconn.State != ConnectionState.Open)
                    {
                        oconn.Open();
                    }

                    // Start a new transaction
                    transaction = oconn.BeginTransaction();


                    string SQL = "INSERT INTO SMS.SMS_GATEWAY " +
                             "(APPLICATION_ID, JOB_CATEGORY, SMS_TYPE, MOBILE_NUMBER, TEXT_MESSAGE, SHORT_CODE, RECORD_STATUS, JOB_OTHER_INFO) " +
                             "VALUES(:appId, :jobCat, :smsType, :mobile, :textMsg, :shortCode, :recStatus, :jobInfo)";

                    OracleCommand cmd = new OracleCommand(SQL, oconn);
                    cmd.Transaction = transaction;

                    string message = "We humbly request you to update the sum Insured of fire Insurance policy no '" + detail.RNPOL + "' at the next renewal to avoid average penalties, as our records show that it has not enhanced over the last three years.";

                    emailBody = message;

                    cmd.Parameters.Add(":appId", "FIRE_RN");
                    cmd.Parameters.Add(":jobCat", "CAT151");
                    cmd.Parameters.Add(":smsType", "I");
                    cmd.Parameters.Add(":mobile", detail.RNCNT);
                    cmd.Parameters.Add(":textMsg", message);
                    cmd.Parameters.Add(":shortCode", "SLIC");
                    cmd.Parameters.Add(":recStatus", "N");
                    cmd.Parameters.Add(":jobInfo", detail.RNPOL);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected <= 0)
                    {
                        result = false;
                        break;
                    }
                    else
                    {
                        string updateSQL = "UPDATE slic_cnote.fire_renewals_sms_tbl " +
                       "SET SUM_CHA_STATUS = :status, updated_date = SYSDATE " +
                       "WHERE policy_no = :policyNo AND year = :year AND month = :month";

                        OracleCommand updateCmd = new OracleCommand(updateSQL, oconn);
                        updateCmd.Transaction = transaction;
                        updateCmd.Parameters.Add(":status", "A");
                        updateCmd.Parameters.Add(":policyNo", detail.RNPOL);
                        updateCmd.Parameters.Add(":year", detail.RNYR);
                        updateCmd.Parameters.Add(":month", detail.RNMNTH);
                        int x = updateCmd.ExecuteNonQuery();

                        if (x <= 0)
                        {
                            result = false;
                            break;
                        }
                        else
                        {

                        }
                    }

                    if (result)
                    {
                        transaction.Commit();

                        //for live
                        //toEmail = this.GetClintMailAddress(detail.RNPOL);

                        //for test
                        toEmail = "shalomid@srilankainsurance.com";

                        if (!string.IsNullOrEmpty(toEmail) && toEmail != "0")
                        {
                            try
                            {
                                SendEmailToClient(emailBody, toEmail, detail.RNPOL, detail.RNTOT);
                            }
                            catch (Exception ex)
                            {
                                // log email error but do not rollback since DB is already committed
                                LogFile Err1 = new LogFile();
                                Err1.ErrorLog(@"D:\WebLogs\FireRenewalErrorlg.txt", "Email error in SendSMS_SumNotChangedList execute_sql.cs calss : " + ex.Message);
                            }
                        }
                    }
                    else
                    {
                        transaction.Rollback();
                    }
                }
                catch (Exception ex1)
                {
                    LogFile Err = new LogFile();
                    string logPath = @"D:\WebLogs\FireRenewalErrorlg.txt";

                    Err.ErrorLog(logPath, ex1.Message);
                }

            }

            
        }
        catch (Exception ex)
        {
            LogFile Err = new LogFile();
            string logPath = @"D:\WebLogs\FireRenewalErrorlg.txt";

            Err.ErrorLog(logPath, ex.Message);

            if (transaction != null)
            {
                transaction.Rollback();
            }

            result = false;

        }
        finally
        {

            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return result;
    }


    //get fire renewal SMS reports
    public string GetFireRenewalSMSReports(string start_date, string end_date, string reportType, string category, string branch, string polNo)
    {
        string sql = "";
        string department = "F";

        //this for ppw not canceled, no claims data for sms send, not send and intermediated report query
        if (category == "N")
        {

            if (reportType == "A")
            {
                sql = "select fr.*, p.adminfee, p.adminfee as adminfeePRe, ";
                sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo ";
                sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
                sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
                sql += "WHERE fr.RN_SMS_STATUS = 'A' and (fr.int_claim_count = 0 OR fr.int_claim_count IS NULL) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
                sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

                if (!string.IsNullOrEmpty(start_date))
                {
                    sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(end_date))
                {
                    sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(department))
                {
                    sql += "  AND fr.DEPARTMENT = '" + department + "' ";
                }
                if (!string.IsNullOrEmpty(polNo))
                {
                    sql += "  AND fr.policy_no = '" + polNo + "' ";
                }
                if (!string.IsNullOrEmpty(branch) && branch != "0")
                {
                    //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                    string branchno = branch.PadLeft(3, '0');

                    //sql += " AND ( ";
                    //sql += " CASE ";
                    //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    //sql += "         SUBSTR( ";
                    //sql += "             fr.policy_no, ";
                    //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                    //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    //sql += "         ) ";
                    //sql += " ";
                    //sql += "     ELSE ";
                    //sql += "         SUBSTR( ";
                    //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                    //sql += "         ) ";
                    //sql += " END ";
                    //sql += ") = '" + branchno + "'";

                    sql += " AND ( ";
                    sql += "     CASE ";
                    sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    sql += "             SUBSTR( ";
                    sql += "                 fr.policy_no, ";
                    sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                    sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    sql += "             ) ";
                    sql += "         ELSE ";
                    sql += "             CASE ";
                    sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                    sql += "                 ELSE ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                    sql += "             END ";
                    sql += "     END ";
                    sql += ") = '" + branchno + "'";
                }

                sql += " ORDER BY fr.expire_date ";
            }
            else if (reportType == "I_Status")
            {
                sql = "select fr.*, p.adminfee, p.adminfee as adminfeePRe, ";
                sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo ";
                sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
                sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
                sql += "WHERE fr.RN_SMS_STATUS = 'I' and (fr.int_claim_count = 0 OR fr.int_claim_count IS NULL) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
                sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

                if (!string.IsNullOrEmpty(start_date))
                {
                    sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(end_date))
                {
                    sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(department))
                {
                    sql += "  AND fr.DEPARTMENT = '" + department + "' ";
                }

                if (!string.IsNullOrEmpty(polNo))
                {
                    sql += "  AND fr.policy_no = '" + polNo + "' ";
                }

                if (!string.IsNullOrEmpty(branch) && branch != "0")
                {
                    //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                    string branchno = branch.PadLeft(3, '0');

                    //sql += " AND ( ";
                    //sql += " CASE ";
                    //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    //sql += "         SUBSTR( ";
                    //sql += "             fr.policy_no, ";
                    //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                    //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    //sql += "         ) ";
                    //sql += " ";
                    //sql += "     ELSE ";
                    //sql += "         SUBSTR( ";
                    //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                    //sql += "         ) ";
                    //sql += " END ";
                    //sql += ") = '" + branchno + "'";

                    sql += " AND ( ";
                    sql += "     CASE ";
                    sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    sql += "             SUBSTR( ";
                    sql += "                 fr.policy_no, ";
                    sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                    sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    sql += "             ) ";
                    sql += "         ELSE ";
                    sql += "             CASE ";
                    sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                    sql += "                 ELSE ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                    sql += "             END ";
                    sql += "     END ";
                    sql += ") = '" + branchno + "'";
                }

                sql += " ORDER BY fr.expire_date ";
            }
            else if (reportType == "Not_Sent")
            {
                sql = "select fr.*, p.adminfee, p.adminfee as adminfeePRe, ";
                sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo ";
                sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
                sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
                sql += "WHERE fr.RN_SMS_STATUS = 'N' and (fr.int_claim_count = 0 OR fr.int_claim_count IS NULL) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
                sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

                if (!string.IsNullOrEmpty(start_date))
                {
                    sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(end_date))
                {
                    sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(department))
                {
                    sql += "  AND fr.DEPARTMENT = '" + department + "' ";
                }

                if (!string.IsNullOrEmpty(polNo))
                {
                    sql += "  AND fr.policy_no = '" + polNo + "' ";
                }

                if (!string.IsNullOrEmpty(branch) && branch != "0")
                {
                    //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                    string branchno = branch.PadLeft(3, '0');

                    //sql += " AND ( ";
                    //sql += " CASE ";
                    //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    //sql += "         SUBSTR( ";
                    //sql += "             fr.policy_no, ";
                    //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                    //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    //sql += "         ) ";
                    //sql += " ";
                    //sql += "     ELSE ";
                    //sql += "         SUBSTR( ";
                    //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                    //sql += "         ) ";
                    //sql += " END ";
                    //sql += ") = '" + branchno + "'";

                    sql += " AND ( ";
                    sql += "     CASE ";
                    sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    sql += "             SUBSTR( ";
                    sql += "                 fr.policy_no, ";
                    sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                    sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    sql += "             ) ";
                    sql += "         ELSE ";
                    sql += "             CASE ";
                    sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                    sql += "                 ELSE ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                    sql += "             END ";
                    sql += "     END ";
                    sql += ") = '" + branchno + "'";
                }
                sql += " ORDER BY fr.expire_date ";
            }
            //end of first report set for ppw not canceled, no claim
        }
        //query for sun insured not changed list
        else if (category == "SUM_N_CHANGED")
        {
            if (reportType == "A")
            {

                sql = "select fr.*, p.adminfee, p.adminfee as adminfeePRe, ";
                sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
                sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
                sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
                sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
                sql += "WHERE fr.SUM_CHA_STATUS = 'A' and (fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL) and (fr.int_claim_count = 0 OR fr.int_claim_count IS NULL) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
                sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

                if (!string.IsNullOrEmpty(start_date))
                {
                    sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(end_date))
                {
                    sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(department))
                {
                    sql += "  AND fr.DEPARTMENT = '" + department + "' ";
                }

                if (!string.IsNullOrEmpty(polNo))
                {
                    sql += "  AND fr.policy_no = '" + polNo + "' ";
                }

                if (!string.IsNullOrEmpty(branch) && branch != "0")
                {
                    //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                    string branchno = branch.PadLeft(3, '0');

                    //sql += " AND ( ";
                    //sql += " CASE ";
                    //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    //sql += "         SUBSTR( ";
                    //sql += "             fr.policy_no, ";
                    //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                    //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    //sql += "         ) ";
                    //sql += " ";
                    //sql += "     ELSE ";
                    //sql += "         SUBSTR( ";
                    //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                    //sql += "         ) ";
                    //sql += " END ";
                    //sql += ") = '" + branchno + "'";

                    sql += " AND ( ";
                    sql += "     CASE ";
                    sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    sql += "             SUBSTR( ";
                    sql += "                 fr.policy_no, ";
                    sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                    sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    sql += "             ) ";
                    sql += "         ELSE ";
                    sql += "             CASE ";
                    sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                    sql += "                 ELSE ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                    sql += "             END ";
                    sql += "     END ";
                    sql += ") = '" + branchno + "'";
                }
                sql += " ORDER BY fr.expire_date ";
            }
            else if (reportType == "Not_Sent")
            {

                sql = "select fr.*, p.adminfee, p.adminfee as adminfeePRe, ";
                sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
                sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
                sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
                sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
                sql += "WHERE fr.SUM_CHA_STATUS = 'N' and (fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL) and (fr.int_claim_count = 0 OR fr.int_claim_count IS NULL) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
                sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

                if (!string.IsNullOrEmpty(start_date))
                {
                    sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(end_date))
                {
                    sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(department))
                {
                    sql += "  AND fr.DEPARTMENT = '" + department + "' ";
                }

                if (!string.IsNullOrEmpty(polNo))
                {
                    sql += "  AND fr.policy_no = '" + polNo + "' ";
                }

                if (!string.IsNullOrEmpty(branch) && branch != "0")
                {
                    //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                    string branchno = branch.PadLeft(3, '0');

                    //sql += " AND ( ";
                    //sql += " CASE ";
                    //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    //sql += "         SUBSTR( ";
                    //sql += "             fr.policy_no, ";
                    //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                    //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    //sql += "         ) ";
                    //sql += " ";
                    //sql += "     ELSE ";
                    //sql += "         SUBSTR( ";
                    //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                    //sql += "         ) ";
                    //sql += " END ";
                    //sql += ") = '" + branchno + "'";

                    sql += " AND ( ";
                    sql += "     CASE ";
                    sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    sql += "             SUBSTR( ";
                    sql += "                 fr.policy_no, ";
                    sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                    sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    sql += "             ) ";
                    sql += "         ELSE ";
                    sql += "             CASE ";
                    sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                    sql += "                 ELSE ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                    sql += "             END ";
                    sql += "     END ";
                    sql += ") = '" + branchno + "'";
                }

                sql += " ORDER BY fr.expire_date ";
            }
        }
        //end of sum insured not changed list

        //for with claim 
        if (category == "WC")
        {

            if (reportType == "A")
            {
                sql = "select fr.*, p.adminfee, p.adminfee as adminfeePRe, ";
                sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo ";
                sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
                sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
                sql += "WHERE fr.RN_SMS_STATUS = 'A' and (fr.int_claim_count > 0 ) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
                sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

                if (!string.IsNullOrEmpty(start_date))
                {
                    sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(end_date))
                {
                    sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(department))
                {
                    sql += "  AND fr.DEPARTMENT = '" + department + "' ";
                }

                if (!string.IsNullOrEmpty(polNo))
                {
                    sql += "  AND fr.policy_no = '" + polNo + "' ";
                }

                if (!string.IsNullOrEmpty(branch) && branch != "0")
                {
                    //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                    string branchno = branch.PadLeft(3, '0');

                    sql += " AND ( ";
                    sql += "     CASE ";
                    sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    sql += "             SUBSTR( ";
                    sql += "                 fr.policy_no, ";
                    sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                    sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    sql += "             ) ";
                    sql += "         ELSE ";
                    sql += "             CASE ";
                    sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                    sql += "                 ELSE ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                    sql += "             END ";
                    sql += "     END ";
                    sql += ") = '" + branchno + "'";
                }
                sql += " ORDER BY fr.expire_date ";
            }
            else if (reportType == "I_Status")
            {
                sql = "select fr.*, p.adminfee, p.adminfee as adminfeePRe, ";
                sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo ";
                sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
                sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
                sql += "WHERE fr.RN_SMS_STATUS = 'I' and (fr.int_claim_count > 0 ) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
                sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

                if (!string.IsNullOrEmpty(start_date))
                {
                    sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(end_date))
                {
                    sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(department))
                {
                    sql += "  AND fr.DEPARTMENT = '" + department + "' ";
                }

                if (!string.IsNullOrEmpty(polNo))
                {
                    sql += "  AND fr.policy_no = '" + polNo + "' ";
                }

                if (!string.IsNullOrEmpty(branch) && branch != "0")
                {
                    // sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                    string branchno = branch.PadLeft(3, '0');

                    sql += " AND ( ";
                    sql += "     CASE ";
                    sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    sql += "             SUBSTR( ";
                    sql += "                 fr.policy_no, ";
                    sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                    sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    sql += "             ) ";
                    sql += "         ELSE ";
                    sql += "             CASE ";
                    sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                    sql += "                 ELSE ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                    sql += "             END ";
                    sql += "     END ";
                    sql += ") = '" + branchno + "'";
                }

                sql += " ORDER BY fr.expire_date ";
            }
            else if (reportType == "Not_Sent")
            {
                sql = "select fr.*, p.adminfee, p.adminfee as adminfeePRe, ";
                sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo ";
                sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
                sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
                sql += "WHERE fr.RN_SMS_STATUS = 'N' and (fr.int_claim_count > 0 ) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
                sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

                if (!string.IsNullOrEmpty(start_date))
                {
                    sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(end_date))
                {
                    sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
                }

                if (!string.IsNullOrEmpty(department))
                {
                    sql += "  AND fr.DEPARTMENT = '" + department + "' ";
                }

                if (!string.IsNullOrEmpty(polNo))
                {
                    sql += "  AND fr.policy_no = '" + polNo + "' ";
                }

                if (!string.IsNullOrEmpty(branch) && branch != "0")
                {
                    //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                    string branchno = branch.PadLeft(3, '0');

                    //sql += " AND ( ";
                    //sql += " CASE ";
                    //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    //sql += "         SUBSTR( ";
                    //sql += "             fr.policy_no, ";
                    //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                    //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    //sql += "         ) ";
                    //sql += " ";
                    //sql += "     ELSE ";
                    //sql += "         SUBSTR( ";
                    //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                    //sql += "         ) ";
                    //sql += " END ";
                    //sql += ") = '" + branchno + "'";

                    sql += " AND ( ";
                    sql += "     CASE ";
                    sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                    sql += "             SUBSTR( ";
                    sql += "                 fr.policy_no, ";
                    sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                    sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                    sql += "             ) ";
                    sql += "         ELSE ";
                    sql += "             CASE ";
                    sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                    sql += "                 ELSE ";
                    sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                    sql += "             END ";
                    sql += "     END ";
                    sql += ") = '" + branchno + "'";
                }
                sql += " ORDER BY fr.expire_date ";
            }
            //end of first report set for ppw not canceled, no claim
        }
        //end of with claim

        //report for ppw canceld lis
        if (category == "PPW_Canceled")
        {
            sql = "select fr.*, p.adminfee, p.adminfee as adminfeePRe, ";
            sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo ";
            sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
            sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
            sql += "WHERE fr.cancel_status = 'PPW Canceled' ";

            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }
            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";

                string branchno = branch.PadLeft(3, '0');

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 fr.policy_no, ";
                sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";
            }

            sql += " ORDER BY fr.expire_date ";
        }
        //end of ppw canceld list

        //else if (reportType == "I_Status")
        //{
        //    sql = "select fr.*, p.adminfee, ";
        //    sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
        //    sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
        //    sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
        //    sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
        //    sql += "WHERE fr.RN_SMS_STATUS = 'N' and (fr.int_claim_count > 0 ) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";

        //    if (!string.IsNullOrEmpty(start_date))
        //    {
        //        sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
        //    }

        //    if (!string.IsNullOrEmpty(end_date))
        //    {
        //        sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
        //    }

        //    if (!string.IsNullOrEmpty(department))
        //    {
        //        sql += "  AND fr.DEPARTMENT = '" + department + "' ";
        //    }

        //    sql += " ORDER BY fr.expire_date ";
        //}
        //else if (reportType == "Not_Sent")
        //{
        //    sql = "select fr.*, p.adminfee, ";
        //    sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
        //    sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
        //    sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
        //    sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
        //    sql += "WHERE fr.RN_SMS_STATUS = 'N' AND (fr.cancel_status = 'PPW Canceled') ";

        //    if (!string.IsNullOrEmpty(start_date))
        //    {
        //        sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
        //    }

        //    if (!string.IsNullOrEmpty(end_date))
        //    {
        //        sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
        //    }

        //    if (!string.IsNullOrEmpty(department))
        //    {
        //        sql += "  AND fr.DEPARTMENT = '" + department + "' ";
        //    }

        //    sql += " ORDER BY fr.expire_date ";
        //}
        //else if (stsus == "SUM_N_CHANGED")
        //{
        //    sql = "select fr.*, p.adminfee, ";
        //    sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
        //    sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
        //    sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
        //    sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
        //    sql += "WHERE fr.SUM_CHA_STATUS = 'N' and (fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL) and (fr.int_claim_count = 0 OR fr.int_claim_count IS NULL) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";

        //    if (!string.IsNullOrEmpty(start_date))
        //    {
        //        sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
        //    }

        //    if (!string.IsNullOrEmpty(end_date))
        //    {
        //        sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
        //    }

        //    if (!string.IsNullOrEmpty(department))
        //    {
        //        sql += "  AND fr.DEPARTMENT = '" + department + "' ";
        //    }

        //    sql += " ORDER BY fr.expire_date ";
        //}
        else
        {

        }

        return sql;

    }

    //insert with claim data to renewal_master_temp table
    public bool InsertIntoRenwalMasterTemp_WithCla(List<FireRenewalMast.FireRenewalMastClass> details, string userId, string branchId)
    {
        bool result = true;
        // Begin transaction
        OracleTransaction transaction = null;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            // Start a new transaction
            transaction = oconn.BeginTransaction();

            foreach (var detail in details)
            {

                string SQL = "INSERT INTO SLIC_CNOTE.RENEWAL_MASTER_TEMP ( " +
                " RNDEPT, RNPTP, RNPOL, RNYR, RNMNTH, RNSTDT, RNENDT, RNAGCD, RNNET, RNRCC, RNTC, " +
                " RNPOLFEE, RNVAT, RNNBT, RNTOT, RNNAM, RNADD1, RNADD2, RNADD3, " +
                " RNNIC, RNCNT, RNREF, RNFBR, RN_ADMINFEE, RNDATE, RN_BY, RN_IP, RN_BRCD, RNSUMINSUR, SUMNOTCHASTATUS) " +
                " VALUES ( " +
                " :RNDEPT, :RNPTP, :RNPOL, :RNYR, :RNMNTH, :RNSTDT, :RNENDT, :RNAGCD, :RNNET, :RNRCC, :RNTC, " +
                " :RNPOLFEE, :RNVAT, :RNNBT, :RNTOT, :RNNAM, :RNADD1, :RNADD2, :RNADD3, " +
                 " :RNNIC, :RNCNT, :RNREF, '0', :RN_ADMINFEE, :RNDATE, :RN_BY, :RN_IP, :RN_BRCD, :RNSUMINSUR, :SUMNOTCHASTATUS)";

                OracleCommand cmd = new OracleCommand(SQL, oconn);
                cmd.Transaction = transaction;

                cmd.Parameters.Add(":RNDEPT", detail.RNDEPT);
                cmd.Parameters.Add(":RNPTP", detail.RNPTP);
                cmd.Parameters.Add(":RNPOL", detail.RNPOL);
                cmd.Parameters.Add(":RNYR", detail.RNYR);
                cmd.Parameters.Add(":RNMNTH", detail.RNMNTH);
                cmd.Parameters.Add(":RNSTDT", DateTime.ParseExact(detail.RNSTDT, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                cmd.Parameters.Add(":RNENDT", DateTime.ParseExact(detail.RNENDT, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                cmd.Parameters.Add(":RNAGCD", detail.RNAGCD);
                cmd.Parameters.Add(":RNNET", detail.RNNET);
                cmd.Parameters.Add(":RNRCC", detail.RNRCC);
                cmd.Parameters.Add(":RNTC", detail.RNTC);
                cmd.Parameters.Add(":RNPOLFEE", detail.RNPOLFEE);
                cmd.Parameters.Add(":RNVAT", detail.RNVAT);
                cmd.Parameters.Add(":RNNBT", detail.RNNBT);
                cmd.Parameters.Add(":RNTOT", detail.RNTOT);
                cmd.Parameters.Add(":RNNAM", detail.RNNAM);
                cmd.Parameters.Add(":RNADD1", detail.RNADD1);
                cmd.Parameters.Add(":RNADD2", detail.RNADD2);
                cmd.Parameters.Add(":RNADD3", detail.RNADD3);
                cmd.Parameters.Add(":RNNIC", detail.RNNIC);

                // Add 94 prefix to mobile number
                cmd.Parameters.Add(":RNCNT", "94" + detail.RNCNT);

                cmd.Parameters.Add(":RNREF", detail.RNREF);
                cmd.Parameters.Add(":RN_ADMINFEE", detail.RN_ADMINFEE);
                cmd.Parameters.Add(":RNDATE", DateTime.Now);
                cmd.Parameters.Add(":RN_BY", userId);
                cmd.Parameters.Add(":RN_IP", HttpContext.Current.Request.UserHostAddress);
                cmd.Parameters.Add(":RN_BRCD", branchId);
                cmd.Parameters.Add(":RNSUMINSUR", detail.RNSUMINSUR);
                cmd.Parameters.Add(":SUMNOTCHASTATUS", detail.SUMNOTCHASTATUS);

                // Then execute
                int rowsAffected = cmd.ExecuteNonQuery();

                //insert remark and excess pre. excess amount
                bool rmkInsertQuert = false;
                //if (detail.EXCESSAMO != 0 || detail.EXCESSPRE != 0)
                //{
                //    rmkInsertQuert = this.InsertRmkExcess_WithCla(detail.RNPOL, detail.REMARK, detail.EXCESSPRE, detail.EXCESSAMO, detail.EXCESSPRE2, detail.EXCESSAMO2, transaction);
                //}

                rmkInsertQuert = this.InsertRmkExcess_WithCla(detail.RNPOL, detail.REMARK, detail.EXCESSPRE, detail.EXCESSAMO, detail.EXCESSPRE2, detail.EXCESSAMO2, transaction);


                if (rowsAffected <= 0)
                {
                    result = false; // Mark as false if no rows were affected
                    break; // Exit loop on failure
                }
                else
                {
                    if (oconn.State != ConnectionState.Open)
                    {
                        oconn.Open();
                    }


                    string updateSQL = "UPDATE slic_cnote.fire_renewals_sms_tbl " +
                   "SET rn_sms_status = :status, updated_date = SYSDATE, AGENCY = :agency, " +
                   " BASIC_PREMIUM = :netPremium, RCC = :rcc, TC = :tc, SUM_INSURED_L = :sumInsured, " +
                   " FMTEL1 = :telNo1, TOT_PREMIUM = :TOT_PREMIUM, " +
                   " NBT = :NBT, VAT = :VAT, ADMINFEE = :ADMINFEE " +
                   "WHERE policy_no = :policyNo AND year = :year AND month = :month";

                    OracleCommand updateCmd = new OracleCommand(updateSQL, oconn);
                    updateCmd.Transaction = transaction;
                    updateCmd.Parameters.Add(":status", "I");
                    updateCmd.Parameters.Add(":policyNo", detail.RNPOL);
                    updateCmd.Parameters.Add(":year", detail.RNYR);
                    updateCmd.Parameters.Add(":month", detail.RNMNTH);
                    updateCmd.Parameters.Add(":agency", detail.RNAGCD);
                    updateCmd.Parameters.Add(":netPremium", detail.RNNET);
                    updateCmd.Parameters.Add(":rcc", detail.RNRCC);
                    updateCmd.Parameters.Add(":tc", detail.RNTC);
                    updateCmd.Parameters.Add(":sumInsured", detail.RNSUMINSUR);
                    //updateCmd.Parameters.Add(":customerName", detail.RNNAM);
                    updateCmd.Parameters.Add(":telNo1", detail.RNCNT);
                    updateCmd.Parameters.Add(":TOT_PREMIUM", detail.RNTOT);
                    updateCmd.Parameters.Add(":NBT", detail.RNNBT);
                    updateCmd.Parameters.Add(":VAT", detail.RNVAT);
                    updateCmd.Parameters.Add(":ADMINFEE", detail.RN_ADMINFEE);
                    int updaeCmdRe = updateCmd.ExecuteNonQuery();

                    if (updaeCmdRe <= 0)
                    {
                        result = false; // Mark as false if no rows were affected
                        break; // Exit loop on failure
                    }
                    else
                    {
                        //update momas telNo 1 here 

                        string updateSQL2 = "update genpay.momas set fmtel1 = :PIN_fmtel1, " +
                            " FMSUM = :pin_fmsum, FMPRM = :pin_fmprm, FMRCC = :pin_fmrcc, FMTC = :pin_rmtc, FMAGT = :pin_fmagt, " +
                            " FMPOF = :pin_fmpof, fmvat = :pin_fmvat, fmnbl = :pin_fmnbl, fmces = :pin_fmces " +
                            " where fmpol = :PIN_fmpol and fmdept = 'F' ";


                        OracleCommand updateCmd2 = new OracleCommand(updateSQL2, oconn);
                        updateCmd2.Transaction = transaction;
                        updateCmd2.Parameters.Add(":PIN_fmtel1", detail.RNCNT);
                        updateCmd2.Parameters.Add(":PIN_fmpol", detail.RNPOL);
                        updateCmd2.Parameters.Add(":pin_fmsum", detail.RNSUMINSUR);
                        updateCmd2.Parameters.Add(":pin_fmprm", detail.RNNET);
                        updateCmd2.Parameters.Add(":pin_fmrcc", detail.RNRCC);
                        updateCmd2.Parameters.Add(":pin_rmtc", detail.RNTC);
                        updateCmd2.Parameters.Add(":pin_fmagt", detail.RNAGCD);
                        //updateCmd2.Parameters.Add(":pin_fmnam", detail.RNNAM);
                        updateCmd2.Parameters.Add(":pin_fmpof", detail.RNPOLFEE);
                        updateCmd2.Parameters.Add(":pin_fmvat", detail.RNVAT);
                        updateCmd2.Parameters.Add(":pin_fmnbl", detail.RNNBT);
                        updateCmd2.Parameters.Add(":pin_fmces", detail.RN_ADMINFEE);


                        int up_CusMobNo = updateCmd2.ExecuteNonQuery();

                        if (up_CusMobNo <= 0)
                        {
                            result = false; // Mark as false if no rows were affected
                            break; // Exit loop on failure
                        }
                    }

                }
            }

            // If all records are inserted successfully, commit the transaction
            if (result)
            {
                transaction.Commit();
            }
            else
            {
                // If any record fails, roll back the entire transaction
                transaction.Rollback();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and roll back the transaction in case of error
            if (transaction != null)
            {
                transaction.Rollback();
            }

            result = false;
            // Optionally, log the exception
            // Console.WriteLine(ex.Message);
        }
        finally
        {
            // Close the connection if it's still open
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return result;
    }


    public bool InsertRmkExcess_WithCla(string polNo, string remark, double excessPre, double excessAmo, double excessPre2, double excessAmo2, OracleTransaction transaction)
    {
        bool result = true;

        try
        {
            string checkSQL = "SELECT COUNT(*) FROM SLIC_CNOTE.FIRE_RN_SMS_REMARKS WHERE POLICY_NO = :POLICY_NO";
            OracleCommand checkCmd = new OracleCommand(checkSQL, transaction.Connection);
            checkCmd.Transaction = transaction;
            checkCmd.Parameters.Add(":POLICY_NO", polNo);
            int recordCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            OracleCommand cmd;

            if (recordCount > 0)
            {
                string updateSQL = "UPDATE SLIC_CNOTE.FIRE_RN_SMS_REMARKS SET " +
                                   "REMARK = :REMARK, EXCESS_PRE = :EXCESS_PRE, EXCESS_AMO = :EXCESS_AMO, EXCESS_PRE2 = :EXCESS_PRE2, EXCESS_AMO2 = :EXCESS_AMO2 " +
                                   "WHERE POLICY_NO = :POLICY_NO";
                cmd = new OracleCommand(updateSQL, transaction.Connection);
            }
            else
            {
                string insertSQL = "INSERT INTO SLIC_CNOTE.FIRE_RN_SMS_REMARKS " +
                                   "(POLICY_NO, REMARK, EXCESS_PRE, EXCESS_AMO, EXCESS_PRE2, EXCESS_AMO2 ) " +
                                   "VALUES (:POLICY_NO, :REMARK, :EXCESS_PRE, :EXCESS_AMO, :EXCESS_PRE2, :EXCESS_AMO2 )";
                cmd = new OracleCommand(insertSQL, transaction.Connection);
            }

            cmd.Transaction = transaction;
            cmd.Parameters.Add(":POLICY_NO", polNo);
            cmd.Parameters.Add(":REMARK", remark);
            cmd.Parameters.Add(":EXCESS_PRE", excessPre);
            cmd.Parameters.Add(":EXCESS_AMO", excessAmo);
            cmd.Parameters.Add(":EXCESS_PRE2", excessPre2);
            cmd.Parameters.Add(":EXCESS_AMO2", excessAmo2);

            int rowsAffected = cmd.ExecuteNonQuery();

            //if (rowsAffected <= 0)
            //{
            //    result = false;
            //}
            //else
            //{
            //    result = true;
            //}


        }
        catch (Exception ex)
        {
            result = false;
        }

        return result;
    }

    public string GetBranch2(int brcode)
    {

        string sql = string.Empty;

        sql = "SELECT BRCOD, BRNAM FROM genpay.branch ";
        if (brcode == 10)
        {
            sql += "WHERE BRCOD != 0 ";
            sql += "AND BRCOD = " + brcode + " ";
        }
        else
        {
            sql += "WHERE BRCOD = " + brcode + " ";
            sql += "AND BRCOD != 0 ";
        }
        sql += "GROUP BY BRCOD, BRNAM ";
        sql += "ORDER BY BRNAM ASC";
        return sql;
    }

    //insert with claim data to renewal_master_temp table
    public bool UpdateIntoRenwalMasterTemp_WithCla(List<FireRenewalMast.FireRenewalMastClass> details, string userId, string branchId)
    {
        bool result = true;
        // Begin transaction
        OracleTransaction transaction = null;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            // Start a new transaction
            transaction = oconn.BeginTransaction();

            foreach (var detail in details)
            {


                //insert remark and excess pre. excess amount
                bool rmkInsertQuert = false;
                //if (detail.EXCESSAMO != 0 || detail.EXCESSPRE != 0)
                //{
                //    rmkInsertQuert = this.InsertRmkExcess_WithCla(detail.RNPOL, detail.REMARK, detail.EXCESSPRE, detail.EXCESSAMO, detail.EXCESSPRE2, detail.EXCESSAMO2, transaction);
                //}

                rmkInsertQuert = this.InsertRmkExcess_WithCla(detail.RNPOL, detail.REMARK, detail.EXCESSPRE, detail.EXCESSAMO, detail.EXCESSPRE2, detail.EXCESSAMO2, transaction);


                //if (rmkInsertQuert == false)
                //{
                //    result = false; // Mark as false if no rows were affected
                //    break; // Exit loop on failure
                //}
                //else
                //{
                if (oconn.State != ConnectionState.Open)
                {
                    oconn.Open();
                }


                string updateSQL = "UPDATE slic_cnote.fire_renewals_sms_tbl " +
               " SET updated_date = SYSDATE, " +
               " BASIC_PREMIUM = :netPremium, RCC = :rcc, TC = :tc, TOT_PREMIUM = :TOT_PREMIUM, " +
               " NBT = :NBT, VAT = :VAT, ADMINFEE = :ADMINFEE, SUM_INSURED_L = :SUM_INSURED_L " +
               " WHERE policy_no = :policyNo AND year = :year AND month = :month";

                OracleCommand updateCmd = new OracleCommand(updateSQL, oconn);
                updateCmd.Transaction = transaction;
                updateCmd.Parameters.Add(":policyNo", detail.RNPOL);
                updateCmd.Parameters.Add(":year", detail.RNYR);
                updateCmd.Parameters.Add(":month", detail.RNMNTH);
                updateCmd.Parameters.Add(":netPremium", detail.RNNET);
                updateCmd.Parameters.Add(":rcc", detail.RNRCC);
                updateCmd.Parameters.Add(":tc", detail.RNTC);
                updateCmd.Parameters.Add(":TOT_PREMIUM", detail.RNTOT);
                updateCmd.Parameters.Add(":NBT", detail.RNNBT);
                updateCmd.Parameters.Add(":VAT", detail.RNVAT);
                updateCmd.Parameters.Add(":ADMINFEE", detail.RN_ADMINFEE);
                updateCmd.Parameters.Add(":SUM_INSURED_L", detail.RNSUMINSUR);
                int updaeCmdRe = updateCmd.ExecuteNonQuery();

               // string updateSQL2 = "UPDATE slic_cnote.RENEWAL_MASTER_TEMP " +
               //"SET " +
               //" RNNET = :netPremium, RNRCC = :rcc, RNTC = :tc, " +
               //"RNVAT = :rnvat, RNNBT = :rnnbt, RNTOT = :rntot, rn_adminfee = :rn_adminfee " +
               //"WHERE RNPOL = :policyNo AND RNYR = :year AND RNMNTH = :month";

               // OracleCommand updateCmd2 = new OracleCommand(updateSQL2, oconn);
               // updateCmd2.Transaction = transaction;
               // updateCmd2.Parameters.Add(":policyNo", detail.RNPOL);
               // updateCmd2.Parameters.Add(":year", detail.RNYR);
               // updateCmd2.Parameters.Add(":month", detail.RNMNTH);
               // updateCmd2.Parameters.Add(":netPremium", detail.RNNET);
               // updateCmd2.Parameters.Add(":rcc", detail.RNRCC);
               // updateCmd2.Parameters.Add(":tc", detail.RNTC);
               // updateCmd2.Parameters.Add(":rnvat", detail.RNVAT);
               // updateCmd2.Parameters.Add(":rnnbt", detail.RNNBT);
               // updateCmd2.Parameters.Add(":rntot", detail.RNTOT);
               // updateCmd2.Parameters.Add(":rn_adminfee", detail.RN_ADMINFEE);
               // int updaeCmdRe2 = updateCmd2.ExecuteNonQuery();

                if (updaeCmdRe <= 0 )
                {
                    result = false; // Mark as false if no rows were affected
                    break; // Exit loop on failure
                }
                else if (updaeCmdRe > 0 )
                {
                    // update momas table 
                    string updateMomsSql = "update genpay.momas set " +
                           " FMPRM = :pin_fmprm, FMRCC = :pin_fmrcc, FMTC = :pin_rmtc, " +
                           " fmvat = :pin_fmvat, fmnbl = :pin_fmnbl, fmces = :pin_fmces, FMSUM = :pin_fmsum " +
                           " where fmpol = :PIN_fmpol and fmdept = 'F' ";


                    OracleCommand updateCmdMomas = new OracleCommand(updateMomsSql, oconn);
                    updateCmdMomas.Transaction = transaction;
                    updateCmdMomas.Parameters.Add(":PIN_fmpol", detail.RNPOL);
                    updateCmdMomas.Parameters.Add(":pin_fmprm", detail.RNNET);
                    updateCmdMomas.Parameters.Add(":pin_fmrcc", detail.RNRCC);
                    updateCmdMomas.Parameters.Add(":pin_rmtc", detail.RNTC);
                    updateCmdMomas.Parameters.Add(":pin_fmvat", detail.RNVAT);
                    updateCmdMomas.Parameters.Add(":pin_fmnbl", detail.RNNBT);
                    updateCmdMomas.Parameters.Add(":pin_fmces", detail.RN_ADMINFEE);
                    updateCmdMomas.Parameters.Add(":pin_fmsum", detail.RNSUMINSUR);


                    int up_MomasTbl = updateCmdMomas.ExecuteNonQuery();

                    if (up_MomasTbl <= 0)
                    {
                        result = false; // Mark as false if no rows were affected
                        break; // Exit loop on failure
                    }
                }
                else
                {
                    result = false; // Mark as false if no rows were affected
                    break;
                }

                //}
            }

            // If all records are inserted successfully, commit the transaction
            if (result)
            {
                transaction.Commit();
            }
            else
            {
                // If any record fails, roll back the entire transaction
                transaction.Rollback();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and roll back the transaction in case of error
            if (transaction != null)
            {
                transaction.Rollback();
            }

            result = false;
            // Optionally, log the exception
            // Console.WriteLine(ex.Message);
        }
        finally
        {
            // Close the connection if it's still open
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return result;
    }

    //update data from view progress panel
    //insert with claim data to renewal_master_temp table
    public bool UpdateIntoRenwalMasterTemp_InPregress(List<FireRenewalMast.FireRenewalMastClass> details, string userId, string branchId)
    {
        bool result = true;
        // Begin transaction
        OracleTransaction transaction = null;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            // Start a new transaction
            transaction = oconn.BeginTransaction();

            foreach (var detail in details)
            {


                //insert remark and excess pre. excess amount
                bool rmkInsertQuert = false;
                //if (detail.EXCESSAMO != 0 || detail.EXCESSPRE != 0)
                //{
                //    rmkInsertQuert = this.InsertRmkExcess_WithCla(detail.RNPOL, detail.REMARK, detail.EXCESSPRE, detail.EXCESSAMO, detail.EXCESSPRE2, detail.EXCESSAMO2, transaction);
                //}

                rmkInsertQuert = this.InsertRmkExcess_WithCla(detail.RNPOL, detail.REMARK, detail.EXCESSPRE, detail.EXCESSAMO, detail.EXCESSPRE2, detail.EXCESSAMO2, transaction);


                //if (rmkInsertQuert == false)
                //{
                //    result = false; // Mark as false if no rows were affected
                //    break; // Exit loop on failure
                //}
                //else
                //{
                if (oconn.State != ConnectionState.Open)
                {
                    oconn.Open();
                }


                string updateSQL = "UPDATE slic_cnote.fire_renewals_sms_tbl " +
               "SET updated_date = SYSDATE, " +
               " BASIC_PREMIUM = :netPremium, RCC = :rcc, TC = :tc, TOT_PREMIUM = :TOT_PREMIUM, " +
                " NBT = :NBT, VAT = :VAT, ADMINFEE = :ADMINFEE " +
               "WHERE policy_no = :policyNo AND year = :year AND month = :month";

                OracleCommand updateCmd = new OracleCommand(updateSQL, oconn);
                updateCmd.Transaction = transaction;
                updateCmd.Parameters.Add(":policyNo", detail.RNPOL);
                updateCmd.Parameters.Add(":year", detail.RNYR);
                updateCmd.Parameters.Add(":month", detail.RNMNTH);
                updateCmd.Parameters.Add(":netPremium", detail.RNNET);
                updateCmd.Parameters.Add(":rcc", detail.RNRCC);
                updateCmd.Parameters.Add(":tc", detail.RNTC);
                updateCmd.Parameters.Add(":TOT_PREMIUM", detail.RNTOT);
                updateCmd.Parameters.Add(":NBT", detail.RNNBT);
                updateCmd.Parameters.Add(":VAT", detail.RNVAT);
                updateCmd.Parameters.Add(":ADMINFEE", detail.RN_ADMINFEE);
                int updaeCmdRe = updateCmd.ExecuteNonQuery();

               // string updateSQL2 = "UPDATE slic_cnote.RENEWAL_MASTER_TEMP " +
               //"SET " +
               //" RNNET = :netPremium, RNRCC = :rcc, RNTC = :tc, " +
               //"RNVAT = :rnvat, RNNBT = :rnnbt, RNTOT = :rntot, rn_adminfee = :rn_adminfee " +
               //"WHERE RNPOL = :policyNo AND RNYR = :year AND RNMNTH = :month";

               // OracleCommand updateCmd2 = new OracleCommand(updateSQL2, oconn);
               // updateCmd2.Transaction = transaction;
               // updateCmd2.Parameters.Add(":policyNo", detail.RNPOL);
               // updateCmd2.Parameters.Add(":year", detail.RNYR);
               // updateCmd2.Parameters.Add(":month", detail.RNMNTH);
               // updateCmd2.Parameters.Add(":netPremium", detail.RNNET);
               // updateCmd2.Parameters.Add(":rcc", detail.RNRCC);
               // updateCmd2.Parameters.Add(":tc", detail.RNTC);
               // updateCmd2.Parameters.Add(":rnvat", detail.RNVAT);
               // updateCmd2.Parameters.Add(":rnnbt", detail.RNNBT);
               // updateCmd2.Parameters.Add(":rntot", detail.RNTOT);
               // updateCmd2.Parameters.Add(":rn_adminfee", detail.RN_ADMINFEE);
               // int updaeCmdRe2 = updateCmd2.ExecuteNonQuery();

                if (updaeCmdRe <= 0 )
                {
                    result = false; // Mark as false if no rows were affected
                    break; // Exit loop on failure
                }
                else if (updaeCmdRe > 0 )
                {
                    // update momas table 
                    string updateMomsSql = "update genpay.momas set " +
                           " FMPRM = :pin_fmprm, FMRCC = :pin_fmrcc, FMTC = :pin_rmtc, " +
                           " fmvat = :pin_fmvat, fmnbl = :pin_fmnbl, fmces = :pin_fmces " +
                           " where fmpol = :PIN_fmpol and fmdept = 'F' ";


                    OracleCommand updateCmdMomas = new OracleCommand(updateMomsSql, oconn);
                    updateCmdMomas.Transaction = transaction;
                    updateCmdMomas.Parameters.Add(":PIN_fmpol", detail.RNPOL);
                    updateCmdMomas.Parameters.Add(":pin_fmprm", detail.RNNET);
                    updateCmdMomas.Parameters.Add(":pin_fmrcc", detail.RNRCC);
                    updateCmdMomas.Parameters.Add(":pin_rmtc", detail.RNTC);
                    updateCmdMomas.Parameters.Add(":pin_fmvat", detail.RNVAT);
                    updateCmdMomas.Parameters.Add(":pin_fmnbl", detail.RNNBT);
                    updateCmdMomas.Parameters.Add(":pin_fmces", detail.RN_ADMINFEE);


                    int up_MomasTbl = updateCmdMomas.ExecuteNonQuery();

                    if (up_MomasTbl <= 0)
                    {
                        result = false; // Mark as false if no rows were affected
                        break; // Exit loop on failure
                    }
                }
                else
                {
                    result = false; // Mark as false if no rows were affected
                    break;
                }

                //}
            }

            // If all records are inserted successfully, commit the transaction
            if (result)
            {
                transaction.Commit();
            }
            else
            {
                // If any record fails, roll back the entire transaction
                transaction.Rollback();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and roll back the transaction in case of error
            if (transaction != null)
            {
                transaction.Rollback();
            }

            result = false;
            // Optionally, log the exception
            // Console.WriteLine(ex.Message);
        }
        finally
        {
            // Close the connection if it's still open
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return result;
    }

    //end of update

    //send to approveal ( update momas, fire renewal sms table and intermediate table)

    public bool UpdateIntoRenwalMasterTemp(List<FireRenewalMast.FireRenewalMastClass> details, string userId, string branchId)
    {
        bool result = true;
        // Begin transaction
        OracleTransaction transaction = null;

        LogFile Err = new LogFile();
        string logPath = @"D:\WebLogs\FireRenewalErrorlg.txt";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            // Start a new transaction
            transaction = oconn.BeginTransaction();

            foreach (var detail in details)
            {
                // Constructing the SQL insert query using FireRenewalMast temp properties
                string SQL = "INSERT INTO SLIC_CNOTE.RENEWAL_MASTER_TEMP ( " +
               " RNDEPT, RNPTP, RNPOL, RNYR, RNMNTH, RNSTDT, RNENDT, RNAGCD, RNNET, RNRCC, RNTC, " +
               " RNPOLFEE, RNVAT, RNNBT, RNTOT, RNNAM, RNADD1, RNADD2, RNADD3, " +
               " RNNIC, RNCNT, RNREF, RNFBR, RN_ADMINFEE, RNDATE, RN_BY, RN_IP, RN_BRCD, RNSUMINSUR, SUMNOTCHASTATUS) " +
               " VALUES ( " +
               " :RNDEPT, :RNPTP, :RNPOL, :RNYR, :RNMNTH, :RNSTDT, :RNENDT, :RNAGCD, :RNNET, :RNRCC, :RNTC, " +
               " :RNPOLFEE, :RNVAT, :RNNBT, :RNTOT, :RNNAM, :RNADD1, :RNADD2, :RNADD3, " +
                " :RNNIC, :RNCNT, :RNREF, '0', :RN_ADMINFEE, :RNDATE, :RN_BY, :RN_IP, :RN_BRCD, :RNSUMINSUR, :SUMNOTCHASTATUS)";

                OracleCommand cmd = new OracleCommand(SQL, oconn);
                cmd.Transaction = transaction;

                cmd.Parameters.Add(":RNDEPT", detail.RNDEPT);
                cmd.Parameters.Add(":RNPTP", detail.RNPTP);
                cmd.Parameters.Add(":RNPOL", detail.RNPOL);
                cmd.Parameters.Add(":RNYR", detail.RNYR);
                cmd.Parameters.Add(":RNMNTH", detail.RNMNTH);

                //cmd.Parameters.Add(":RNSTDT", DateTime.ParseExact(detail.RNSTDT, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                //cmd.Parameters.Add(":RNENDT", DateTime.ParseExact(detail.RNENDT, "dd/MM/yyyy", CultureInfo.InvariantCulture));

                DateTime rnsdt, rnendt;

                if (!string.IsNullOrWhiteSpace(detail.RNSTDT) &&
                    DateTime.TryParseExact(detail.RNSTDT.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rnsdt))
                {
                    cmd.Parameters.Add(":RNSTDT", rnsdt);
                }
                else
                {
                    string errorDetails = " Invalid or missing RNSTDT:  " + detail.RNSTDT + Environment.NewLine;
                    Err.ErrorLog(logPath, errorDetails);

                    throw new FormatException("Invalid or missing RNSTDT: " + detail.RNSTDT);

                }

                if (!string.IsNullOrWhiteSpace(detail.RNENDT) &&
                    DateTime.TryParseExact(detail.RNENDT.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out rnendt))
                {
                    cmd.Parameters.Add(":RNENDT", rnendt);
                }
                else
                {
                    string errorDetails = " Invalid or missing RNENDT:  " + detail.RNENDT + Environment.NewLine;
                    Err.ErrorLog(logPath, errorDetails);
                    throw new FormatException("Invalid or missing RNENDT: " + detail.RNENDT);
                }

                cmd.Parameters.Add(":RNAGCD", detail.RNAGCD);
                cmd.Parameters.Add(":RNNET", detail.RNNET);
                cmd.Parameters.Add(":RNRCC", detail.RNRCC);
                cmd.Parameters.Add(":RNTC", detail.RNTC);
                cmd.Parameters.Add(":RNPOLFEE", detail.RNPOLFEE);
                cmd.Parameters.Add(":RNVAT", detail.RNVAT);
                cmd.Parameters.Add(":RNNBT", detail.RNNBT);
                cmd.Parameters.Add(":RNTOT", detail.RNTOT);
                cmd.Parameters.Add(":RNNAM", detail.RNNAM);
                cmd.Parameters.Add(":RNADD1", detail.RNADD1);
                cmd.Parameters.Add(":RNADD2", detail.RNADD2);
                cmd.Parameters.Add(":RNADD3", detail.RNADD3);
                cmd.Parameters.Add(":RNNIC", detail.RNNIC);

                // Add 94 prefix to mobile number
                cmd.Parameters.Add(":RNCNT", detail.RNCNT);

                cmd.Parameters.Add(":RNREF", detail.RNREF);
                cmd.Parameters.Add(":RN_ADMINFEE", detail.RN_ADMINFEE);
                cmd.Parameters.Add(":RNDATE", DateTime.Now);
                cmd.Parameters.Add(":RN_BY", userId);
                cmd.Parameters.Add(":RN_IP", HttpContext.Current.Request.UserHostAddress);
                cmd.Parameters.Add(":RN_BRCD", branchId);
                cmd.Parameters.Add(":RNSUMINSUR", detail.RNSUMINSUR);
                cmd.Parameters.Add(":SUMNOTCHASTATUS", detail.SUMNOTCHASTATUS);

                // Then execute
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected <= 0)
                {
                    result = false; // Mark as false if no rows were affected
                    break; // Exit loop on failure

                    string errorDetails =
                    " " + Environment.NewLine + "Insert failed for:: " + detail.RNPOL + Environment.NewLine +
                    "rowsAffected " + rowsAffected + Environment.NewLine +
                    "SQL: " + SQL + Environment.NewLine +
                    "RNDEPT: " + detail.RNDEPT + Environment.NewLine +
                    "RNPTP: " + detail.RNPTP + Environment.NewLine +
                    "RNPOL: " + detail.RNPOL + Environment.NewLine +
                    "RNYR: " + detail.RNYR + Environment.NewLine +
                    "RNMNTH: " + detail.RNMNTH + Environment.NewLine +
                    "RNSTDT: " + detail.RNSTDT + Environment.NewLine +
                    "RNENDT: " + detail.RNENDT + Environment.NewLine +
                    "RNAGCD: " + detail.RNAGCD + Environment.NewLine +
                    "RNNET: " + detail.RNNET + Environment.NewLine +
                    "RNRCC: " + detail.RNRCC + Environment.NewLine +
                    "RNTC: " + detail.RNTC + Environment.NewLine +
                    "RNPOLFEE: " + detail.RNPOLFEE + Environment.NewLine +
                    "RNVAT: " + detail.RNVAT + Environment.NewLine +
                    "RNNBT: " + detail.RNNBT + Environment.NewLine +
                    "RNTOT: " + detail.RNTOT + Environment.NewLine +
                    "RNNAM: " + detail.RNNAM + Environment.NewLine +
                    "RNADD1: " + detail.RNADD1 + Environment.NewLine +
                    "RNADD2: " + detail.RNADD2 + Environment.NewLine +
                    "RNADD3: " + detail.RNADD3 + Environment.NewLine +
                    "RNNIC: " + detail.RNNIC + Environment.NewLine +
                    "RNCNT: " + detail.RNCNT + Environment.NewLine +
                    "RNREF: " + detail.RNREF + Environment.NewLine +
                    "RN_ADMINFEE: " + detail.RN_ADMINFEE + Environment.NewLine +
                    "RNSUMINSUR: " + detail.RNSUMINSUR + Environment.NewLine +
                    "SUMNOTCHASTATUS: " + detail.SUMNOTCHASTATUS + Environment.NewLine;

                    Err.ErrorLog(logPath, errorDetails);
                }
                else
                {
                    string updateSQL = "UPDATE slic_cnote.fire_renewals_sms_tbl " +
                   "SET rn_sms_status = :status, updated_date = SYSDATE " +
                   "WHERE policy_no = :policyNo AND year = :year AND month = :month";

                    OracleCommand updateCmd = new OracleCommand(updateSQL, oconn);
                    updateCmd.Transaction = transaction;
                    updateCmd.Parameters.Add(":status", "I");
                    updateCmd.Parameters.Add(":policyNo", detail.RNPOL);
                    updateCmd.Parameters.Add(":year", detail.RNYR);
                    updateCmd.Parameters.Add(":month", detail.RNMNTH);
                    int re = updateCmd.ExecuteNonQuery();

                    if (re <= 0)
                    {
                        result = false; // Mark as false if no rows were affected
                        break; // Exit loop on failure
                    }

                }
            }

            // If all records are inserted successfully, commit the transaction
            if (result)
            {
                transaction.Commit();
            }
            else
            {
                // If any record fails, roll back the entire transaction
                transaction.Rollback();
            }
        }
        catch (Exception ex)
        {
            Err.ErrorLog(logPath, ex.Message);

            // Handle exceptions and roll back the transaction in case of error
            if (transaction != null)
            {
                transaction.Rollback();
            }

            result = false;
            // Optionally, log the exception
            // Console.WriteLine(ex.Message);
        }
        finally
        {
            // Close the connection if it's still open
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return result;
    }
    //end


    //get data from fr_renewal_remark table
    public string GetFireRenewalRemarlData(string policyNo)
    {
        string sql = "";

        sql = "select * ";
        sql += "FROM SLIC_CNOTE.FIRE_RN_SMS_REMARKS ";
        sql += "WHERE POLICY_NO = '" + policyNo + "' ";

        return sql;

    }
    //update 

    //edited by admin panel renewal sms table, remark table and intermediate table 
    public bool UpdateIntoRenwalMasterTemp_WithCla_byAdm(List<FireRenewalMast.FireRenewalMastClass> details, string userId, string branchId)
    {
        bool result = true;
        // Begin transaction
        OracleTransaction transaction = null;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            // Start a new transaction
            transaction = oconn.BeginTransaction();

            foreach (var detail in details)
            {


                //insert remark and excess pre. excess amount
                bool rmkInsertQuert = false;
                //if (detail.EXCESSAMO != 0 || detail.EXCESSPRE != 0)
                //{
                //    rmkInsertQuert = this.InsertRmkExcess_WithCla(detail.RNPOL, detail.REMARK, detail.EXCESSPRE, detail.EXCESSAMO, detail.EXCESSPRE2, detail.EXCESSAMO2, transaction);
                //}
                rmkInsertQuert = this.InsertRmkExcess_WithCla(detail.RNPOL, detail.REMARK, detail.EXCESSPRE, detail.EXCESSAMO, detail.EXCESSPRE2, detail.EXCESSAMO2, transaction);


                //if (rmkInsertQuert == false)
                //{
                //    result = false; // Mark as false if no rows were affected
                //    break; // Exit loop on failure
                //}
                //else
                //{
                if (oconn.State != ConnectionState.Open)
                {
                    oconn.Open();
                }


                string updateSQL = "UPDATE slic_cnote.fire_renewals_sms_tbl " +
               "SET updated_date = SYSDATE, " +
               " BASIC_PREMIUM = :netPremium, RCC = :rcc, TC = :tc, TOT_PREMIUM = :TOT_PREMIUM, " +
               " NBT = :NBT, VAT = :VAT, ADMINFEE = :ADMINFEE,SUM_INSURED_L = :SUM_INSURED_L " +
               "WHERE policy_no = :policyNo AND year = :year AND month = :month";

                OracleCommand updateCmd = new OracleCommand(updateSQL, oconn);
                updateCmd.Transaction = transaction;
                updateCmd.Parameters.Add(":policyNo", detail.RNPOL);
                updateCmd.Parameters.Add(":year", detail.RNYR);
                updateCmd.Parameters.Add(":month", detail.RNMNTH);
                updateCmd.Parameters.Add(":netPremium", detail.RNNET);
                updateCmd.Parameters.Add(":rcc", detail.RNRCC);
                updateCmd.Parameters.Add(":tc", detail.RNTC);
                updateCmd.Parameters.Add(":TOT_PREMIUM", detail.RNTOT);
                updateCmd.Parameters.Add(":NBT", detail.RNNBT);
                updateCmd.Parameters.Add(":VAT", detail.RNVAT);
                updateCmd.Parameters.Add(":ADMINFEE", detail.RN_ADMINFEE);
                updateCmd.Parameters.Add(":SUM_INSURED_L", detail.RNSUMINSUR);
                int updaeCmdRe = updateCmd.ExecuteNonQuery();

                string updateSQL2 = "UPDATE slic_cnote.RENEWAL_MASTER_TEMP " +
               "SET " +
               " RNNET = :netPremium, RNRCC = :rcc, RNTC = :tc, RNTOT = :rntot, " +
               " RNPOLFEE = :rnpolfee, RNVAT = :vat, RNNBT = :nbt, RN_ADMINFEE = :adminfee, RNSUMINSUR = :RNSUMINSUR " +
               "WHERE RNPOL = :policyNo AND RNYR = :year AND RNMNTH = :month";

                OracleCommand updateCmd2 = new OracleCommand(updateSQL2, oconn);
                updateCmd2.Transaction = transaction;
                updateCmd2.Parameters.Add(":policyNo", detail.RNPOL);
                updateCmd2.Parameters.Add(":year", detail.RNYR);
                updateCmd2.Parameters.Add(":month", detail.RNMNTH);
                updateCmd2.Parameters.Add(":netPremium", detail.RNNET);
                updateCmd2.Parameters.Add(":rcc", detail.RNRCC);
                updateCmd2.Parameters.Add(":tc", detail.RNTC);
                updateCmd2.Parameters.Add(":rntot", detail.RNTOT);
                updateCmd2.Parameters.Add(":rnpolfee", detail.RNPOLFEE);
                updateCmd2.Parameters.Add(":vat", detail.RNVAT);
                updateCmd2.Parameters.Add(":nbt", detail.RNNBT);
                updateCmd2.Parameters.Add(":adminfee", detail.RN_ADMINFEE);
                updateCmd2.Parameters.Add(":RNSUMINSUR", detail.RNSUMINSUR);
                int updaeCmdRe2 = updateCmd2.ExecuteNonQuery();

                if (updaeCmdRe <= 0 && updaeCmdRe2 <= 0)
                {
                    result = false; // Mark as false if no rows were affected
                    break; 
                }
                else if (updaeCmdRe > 0 && updaeCmdRe2 > 0)
                {
                    // update momas table 
                    string updateMomsSql = "update genpay.momas set " +
                           " FMPRM = :pin_fmprm, FMRCC = :pin_fmrcc, FMTC = :pin_rmtc, " +
                           " fmvat = :pin_fmvat, fmnbl = :pin_fmnbl, fmces = :pin_fmces, FMSUM = :pin_fmsum " +
                           " where fmpol = :PIN_fmpol and fmdept = 'F' ";


                    OracleCommand updateCmdMomas = new OracleCommand(updateMomsSql, oconn);
                    updateCmdMomas.Transaction = transaction;
                    updateCmdMomas.Parameters.Add(":PIN_fmpol", detail.RNPOL);
                    updateCmdMomas.Parameters.Add(":pin_fmprm", detail.RNNET);
                    updateCmdMomas.Parameters.Add(":pin_fmrcc", detail.RNRCC);
                    updateCmdMomas.Parameters.Add(":pin_rmtc", detail.RNTC);
                    updateCmdMomas.Parameters.Add(":pin_fmvat", detail.RNVAT);
                    updateCmdMomas.Parameters.Add(":pin_fmnbl", detail.RNNBT);
                    updateCmdMomas.Parameters.Add(":pin_fmces", detail.RN_ADMINFEE);
                    updateCmdMomas.Parameters.Add(":pin_fmsum", detail.RNSUMINSUR);

                    int up_MomasTbl = updateCmdMomas.ExecuteNonQuery();

                    if (up_MomasTbl <= 0)
                    {
                        result = false; // Mark as false if no rows were affected
                        break; // Exit loop on failure
                    }
                }
                else
                {
                    result = false; // Mark as false if no rows were affected
                    break;
                }

                //}
            }

            // If all records are inserted successfully, commit the transaction
            if (result)
            {
                transaction.Commit();
            }
            else
            {
                // If any record fails, roll back the entire transaction
                transaction.Rollback();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and roll back the transaction in case of error
            if (transaction != null)
            {
                transaction.Rollback();
            }

            result = false;
            // Optionally, log the exception
            // Console.WriteLine(ex.Message);
        }
        finally
        {
            // Close the connection if it's still open
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return result;
    }
    //end

    //reject record
    public bool RejectRenewalByAdmin(string userId, string branchId, string policyNo, string rejectRemark, int year, int month)
    {
        bool result = true;
        // Begin transaction
        OracleTransaction transaction = null;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            // Start a new transaction
            transaction = oconn.BeginTransaction();

            //insert remark and excess pre. excess amount
            bool updateRemarkTable = false;

            updateRemarkTable = this.UpdateRemkTablInReject(policyNo, rejectRemark, userId, transaction);


            if (updateRemarkTable == false)
            {
                result = false; // Mark as false if no rows were affected

            }
            else
            {
                if (oconn.State != ConnectionState.Open)
                {
                    oconn.Open();
                }

                int updaeCmdRe = 0;

                string updateSQL = "UPDATE slic_cnote.fire_renewals_sms_tbl " +
               "SET updated_date = SYSDATE, " +
               " REJECT_REASON = :PIN_REJECT_REASON, RN_SMS_STATUS = :PIN_RN_SMS_STATUS " +
               "WHERE policy_no = :policyNo AND year = :year AND month = :month";

                OracleCommand updateCmd = new OracleCommand(updateSQL, oconn);
                updateCmd.Transaction = transaction;
                updateCmd.Parameters.Add(":policyNo", policyNo);
                updateCmd.Parameters.Add(":year", year);
                updateCmd.Parameters.Add(":month", month);
                updateCmd.Parameters.Add(":PIN_RN_SMS_STATUS", "R");
                updateCmd.Parameters.Add(":PIN_REJECT_REASON", rejectRemark);

                updaeCmdRe = updateCmd.ExecuteNonQuery();

                string deleteTemp = "DELETE FROM SLIC_CNOTE.RENEWAL_MASTER_TEMP WHERE RNPOL = '" + policyNo + "' and rnptp = 'FD_RENEWAL' ";

                OracleCommand updateCmd3 = new OracleCommand(deleteTemp, oconn);
                updateCmd3.Transaction = transaction;
                int deleteTempTbl = updateCmd3.ExecuteNonQuery();

                if (updaeCmdRe <= 0 && deleteTempTbl <= 0)
                {
                    result = false; // Mark as false if no rows were affected

                }
                else if (updaeCmdRe > 0 && deleteTempTbl > 0)
                {
                    // Optionally do something if both succeeded
                    // For example: result = true; // or continue;
                }
                else
                {
                    result = false; // Mark as false if no rows were affected

                }

            }


            // If all records are inserted successfully, commit the transaction
            if (result)
            {
                transaction.Commit();
            }
            else
            {
                // If any record fails, roll back the entire transaction
                transaction.Rollback();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and roll back the transaction in case of error
            if (transaction != null)
            {
                transaction.Rollback();
            }

            result = false;
            // Optionally, log the exception
            // Console.WriteLine(ex.Message);
        }
        finally
        {
            // Close the connection if it's still open
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return result;
    }
    //end

    //update remark table
    public bool UpdateRemkTablInReject(string polNo, string rejectRason, string updatedBy, OracleTransaction transaction)
    {
        bool result = true;

        try
        {
            string checkSQL = "SELECT COUNT(*) FROM SLIC_CNOTE.FIRE_RN_SMS_REMARKS WHERE POLICY_NO = :POLICY_NO";
            OracleCommand checkCmd = new OracleCommand(checkSQL, transaction.Connection);
            checkCmd.Transaction = transaction;
            checkCmd.Parameters.Add(":POLICY_NO", polNo);
            int recordCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            OracleCommand cmd;

            if (recordCount > 0)
            {
                string updateSQL = "UPDATE SLIC_CNOTE.FIRE_RN_SMS_REMARKS SET " +
                                   "REJECT_REASON = :PIN_REJECT_REASON, REJECT_BY = :PIN_REJECT_BY, REJECT_DATE = SYSDATE " +
                                   "WHERE POLICY_NO = :PIN_POLICY_NO";
                cmd = new OracleCommand(updateSQL, transaction.Connection);
            }
            else
            {
                string insertSQL = "INSERT INTO SLIC_CNOTE.FIRE_RN_SMS_REMARKS " +
                                   "(POLICY_NO, REJECT_REASON, REJECT_BY, REJECT_DATE ) " +
                                   "VALUES (:PIN_POLICY_NO, :PIN_REJECT_REASON, :PIN_REJECT_BY, SYSDATE )";
                cmd = new OracleCommand(insertSQL, transaction.Connection);
            }

            cmd.Transaction = transaction;
            cmd.Parameters.Add(":PIN_POLICY_NO", polNo);
            cmd.Parameters.Add(":PIN_REJECT_REASON", rejectRason);
            cmd.Parameters.Add(":PIN_REJECT_BY", updatedBy);
            //cmd.Parameters.Add(":PIN_REJECT_DATE", System.DateTime.Now);


            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected <= 0)
            {
                result = false;
            }
            else
            {
                result = true;
            }


        }
        catch (Exception ex)
        {
            result = false;
        }

        return result;
    }
    //end

    //delete extra excess


    //get rejected records
    //get fire renewal sms data from bau.fire_renewals table
    public string GetFireRenewalRejectedList(string start_date, string end_date, string stsus, string department, string policyNo, string branch)
    {
        string sql = "";

        if (stsus == "N")
        {
            sql = "select fr.*, p.adminfee as adminFeePre, ";
            sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
            sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
            sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
            sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
            sql += "WHERE fr.RN_SMS_STATUS = 'R' and (fr.int_claim_count = 0 OR fr.int_claim_count IS NULL) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
            sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(department))
            {
                sql += "  AND fr.DEPARTMENT = '" + department + "' ";
            }

            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND fr.policy_no = '" + policyNo + "' ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";
                string branchno = branch.PadLeft(3, '0');

                //sql += " AND ( ";
                //sql += " CASE ";
                //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                //sql += "         SUBSTR( ";
                //sql += "             fr.policy_no, ";
                //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                //sql += "         ) ";
                //sql += " ";
                //sql += "     ELSE ";
                //sql += "         SUBSTR( ";
                //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                //sql += "         ) ";
                //sql += " END ";
                //sql += ") = '" + branchno + "'";

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 fr.policy_no, ";
                sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";
            }

            sql += " ORDER BY fr.expire_date ";
        }
        else if (stsus == "SUM_CHANGED")
        {
            sql = "select fr.*, p.adminfee as adminFeePre, ";
            sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
            sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
            sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
            sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
            sql += "WHERE fr.RN_SMS_STATUS = 'R' and (fr.int_claim_count = 0 OR fr.int_claim_count IS NULL) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
            sql += " and (fr.ENDORSE_PREMIUM > 0 ) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(department))
            {
                sql += "  AND fr.DEPARTMENT = '" + department + "' ";
            }

            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND fr.policy_no = '" + policyNo + "' ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                // sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";
                string branchno = branch.PadLeft(3, '0');

                //sql += " AND ( ";
                //sql += " CASE ";
                //sql += "     WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                //sql += "         SUBSTR( ";
                //sql += "             fr.policy_no, ";
                //sql += "             INSTR(fr.policy_no, '/') + 1, ";
                //sql += "             INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                //sql += "         ) ";
                //sql += " ";
                //sql += "     ELSE ";
                //sql += "         SUBSTR( ";
                //sql += "             REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3 ";
                //sql += "         ) ";
                //sql += " END ";
                //sql += ") = '" + branchno + "'";

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 fr.policy_no, ";
                sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";
            }

            sql += " ORDER BY fr.expire_date ";
        }
        else if (stsus == "WC")
        {
            sql = "select fr.*, p.adminfee as adminFeePre, ";
            sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
            sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
            sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
            sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
            sql += "WHERE fr.RN_SMS_STATUS = 'R' and (fr.int_claim_count > 0 ) AND (fr.cancel_status IS NULL OR fr.cancel_status != 'PPW Canceled') ";
            sql += " and (fr.ENDORSE_PREMIUM = 0 OR fr.ENDORSE_PREMIUM is null) and (fr.OUTSTANDING = 0 or fr.OUTSTANDING is null) ";

            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(department))
            {
                sql += "  AND fr.DEPARTMENT = '" + department + "' ";
            }

            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND fr.policy_no = '" + policyNo + "' ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";
                string branchno = branch.PadLeft(3, '0');

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 fr.policy_no, ";
                sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";

            }

            sql += " ORDER BY fr.expire_date ";
        }
        else if (stsus == "WPPWC")
        {
            sql = "select fr.*, p.adminfee as adminFeePre, ";
            sql += "COALESCE(fr.phone_no1, fr.phone_no2, fr.fmtel1, fr.fmtel2 ) AS customerPhoNo, ";
            sql += "CASE WHEN fr.SUM_INSURED_L = fr.SUM_INSURED_LL AND fr.SUM_INSURED_LL = fr.SUM_INSURED_LLL THEN 'Not Changed'  ELSE 'Changed' END AS sum_status ";
            sql += "FROM SLIC_CNOTE.fire_renewals_sms_tbl fr ";
            sql += "left join genpay.poltyp p on p.ptdep = fr.department and p.pttyp = fr.product_code ";
            sql += "WHERE fr.RN_SMS_STATUS = 'R' AND (fr.cancel_status = 'PPW Canceled') ";

            if (!string.IsNullOrEmpty(start_date))
            {
                sql += "AND fr.expire_date >= TO_DATE('" + start_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(end_date))
            {
                sql += "  AND fr.expire_date <= TO_DATE('" + end_date + "', 'DD/MM/YYYY') ";
            }

            if (!string.IsNullOrEmpty(department))
            {
                sql += "  AND fr.DEPARTMENT = '" + department + "' ";
            }

            if (!string.IsNullOrEmpty(policyNo))
            {
                sql += "  AND fr.policy_no = '" + policyNo + "' ";
            }

            if (!string.IsNullOrEmpty(branch) && branch != "0")
            {
                //sql += "  AND fr.BRANCH_CODE = '" + branch + "' ";
                string branchno = branch.PadLeft(3, '0');

                sql += " AND ( ";
                sql += "     CASE ";
                sql += "         WHEN INSTR(fr.policy_no, '/') > 0 THEN ";
                sql += "             SUBSTR( ";
                sql += "                 fr.policy_no, ";
                sql += "                 INSTR(fr.policy_no, '/') + 1, ";
                sql += "                 INSTR(fr.policy_no, '/', INSTR(fr.policy_no, '/') + 1) - INSTR(fr.policy_no, '/') - 1 ";
                sql += "             ) ";
                sql += "         ELSE ";
                sql += "             CASE ";
                sql += "                 WHEN LENGTH(fr.policy_no) = 18 THEN ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 5, 3) ";
                sql += "                 ELSE ";
                sql += "                     SUBSTR(REGEXP_SUBSTR(fr.policy_no, '[0-9]+'), 3, 3) ";
                sql += "             END ";
                sql += "     END ";
                sql += ") = '" + branchno + "'";

            }

            sql += " ORDER BY fr.expire_date ";
        }

        else
        {

        }


        return sql;

    }
    //end

    //insert extra excess values 
    public bool InsertExcessRecord(string excessName, double precentage, double amount, string policyNo)
    {
        bool result = true;
        // Begin transaction
        OracleTransaction transaction = null;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            // Start a new transaction
            transaction = oconn.BeginTransaction();

            string req_val = "select SLIC_CNOTE.EXTRA_EXCESS_SEQ.NEXTVAL from dual";
            int req_id = Convert.ToInt32(orcle_trans.GetString(req_val));

            string SQL = "INSERT INTO SLIC_CNOTE.FIRE_RN_EXTRA_EXCESS ( " +
            " ID, POLICY_NO, EXCESS_DESCRIPTION, EXCESS_PRECENTAGE, EXCESS_AMOUNT )" +
            " VALUES ( " +
            " :ID, :POLICY_NO, :EXCESS_DESCRIPTION, :EXCESS_PRECENTAGE, :EXCESS_AMOUNT )";


                OracleCommand cmd = new OracleCommand(SQL, oconn);
                cmd.Transaction = transaction;

                cmd.Parameters.Add(":ID", req_id);
                cmd.Parameters.Add(":POLICY_NO", policyNo);
                cmd.Parameters.Add(":EXCESS_DESCRIPTION", excessName);
                cmd.Parameters.Add(":EXCESS_PRECENTAGE", precentage);
                cmd.Parameters.Add(":EXCESS_AMOUNT", amount);
                

                // Then execute
                int rowsAffected = cmd.ExecuteNonQuery();
               
                if (rowsAffected <= 0)
                {
                    result = false; // Mark as false if no rows were affected
                    
                }
                else
                {
                    

                }
            

            // If all records are inserted successfully, commit the transaction
            if (result)
            {
                transaction.Commit();
            }
            else
            {
                // If any record fails, roll back the entire transaction
                transaction.Rollback();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and roll back the transaction in case of error
            if (transaction != null)
            {
                transaction.Rollback();
            }

            result = false;
            // Optionally, log the exception
            // Console.WriteLine(ex.Message);
        }
        finally
        {
            // Close the connection if it's still open
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return result;
    }
    //end

    //get data from fr_renewal_remark table
    public string GetFireRenewalExtraAccess(string policyNo)
    {
        string sql = "";

        sql = "select policy_no, id as EXCESS_ID, EXCESS_DESCRIPTION, excess_precentage, EXCESS_AMOUNT  ";
        sql += "FROM SLIC_CNOTE.FIRE_RN_EXTRA_EXCESS ";
        sql += "WHERE POLICY_NO = '" + policyNo + "' ";

        return sql;

    }
    //update extra excess table
    public bool InsertUpdateExtExcess(string polNo, string excessName, double excessPre, double excessAmo, int excessId)
    {
        bool result = true;
        OracleTransaction transaction = null;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            // Start a new transaction
            transaction = oconn.BeginTransaction();

            string checkSQL = "SELECT COUNT(*) FROM SLIC_CNOTE.FIRE_RN_EXTRA_EXCESS WHERE POLICY_NO = :POLICY_NO";
            OracleCommand checkCmd = new OracleCommand(checkSQL, transaction.Connection);
            checkCmd.Transaction = transaction;
            checkCmd.Parameters.Add(":POLICY_NO", polNo);
            int recordCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            OracleCommand cmd;

            if (recordCount > 0 && excessId != 0)
            {
                string updateSQL = "UPDATE SLIC_CNOTE.FIRE_RN_EXTRA_EXCESS SET " +
                                   "EXCESS_DESCRIPTION = :EXCESS_DESCRIPTION, EXCESS_PRECENTAGE = :EXCESS_PRECENTAGE, EXCESS_AMOUNT = :EXCESS_AMOUNT " +
                                   "WHERE POLICY_NO = :POLICY_NO and ID = :ID";
                cmd = new OracleCommand(updateSQL, transaction.Connection);
                cmd.Parameters.Add(":ID", excessId);
            }
            else
            {
                string req_val = "select SLIC_CNOTE.EXTRA_EXCESS_SEQ.NEXTVAL from dual";
                int req_id = Convert.ToInt32(orcle_trans.GetString(req_val));

                string insertSQL = "INSERT INTO SLIC_CNOTE.FIRE_RN_EXTRA_EXCESS " +
                                   "(ID, POLICY_NO, EXCESS_DESCRIPTION, EXCESS_PRECENTAGE, EXCESS_AMOUNT ) " +
                                   "VALUES (:ID, :POLICY_NO, :EXCESS_DESCRIPTION, :EXCESS_PRECENTAGE, :EXCESS_AMOUNT )";
                cmd = new OracleCommand(insertSQL, transaction.Connection);
                cmd.Parameters.Add(":ID", req_id);
            }

            cmd.Transaction = transaction;
            cmd.Parameters.Add(":POLICY_NO", polNo);
            cmd.Parameters.Add(":EXCESS_DESCRIPTION", excessName);
            cmd.Parameters.Add(":EXCESS_PRECENTAGE", excessPre);
            cmd.Parameters.Add(":EXCESS_AMOUNT", excessAmo);


            int rowsAffected = cmd.ExecuteNonQuery();

            //if (rowsAffected <= 0)
            //{
            //    result = false;
            //}
            //else
            //{
            //    result = true;
            //}

            if (result)
            {
                transaction.Commit();
            }
            else
            {
                // If any record fails, roll back the entire transaction
                transaction.Rollback();
            }

        }
        catch (Exception ex)
        {
            result = false;
            transaction.Rollback();
        }

        return result;
    }

    //Delete from extra excess
    public bool DelteFromExtraExcess(string policyNo, int ID)
    {
        bool result = true;
        OracleTransaction transaction = null;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            // Start a new transaction
            transaction = oconn.BeginTransaction();

            string SQL = "DELETE FROM SLIC_CNOTE.FIRE_RN_EXTRA_EXCESS WHERE POLICY_NO = '" + policyNo + "' and ID = " + ID + "";

            OracleCommand updateCmd3 = new OracleCommand(SQL, oconn);
            updateCmd3.Transaction = transaction;
            int deleteTempTbl = updateCmd3.ExecuteNonQuery();

            if(deleteTempTbl <= 0)
            {
                result = false;
            }
            

            // If all records are inserted successfully, commit the transaction
            if (result)
            {
                transaction.Commit();
            }
            else
            {
                // If any record fails, roll back the entire transaction
                transaction.Rollback();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and roll back the transaction in case of error
            if (transaction != null)
            {
                transaction.Rollback();
            }

            result = false;
            // Optionally, log the exception
            // Console.WriteLine(ex.Message);
        }
        finally
        {
            // Close the connection if it's still open
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return result;

    }

    //update OTP in shortUrl tbl
    public bool UpdateOtpandSMSSend(string shortKey, string mobileNo, string otp, DateTime otpExpiry, string policyNo)
    {
        bool result = true;
        // Begin transaction
        OracleTransaction transaction = null;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            // Start a new transaction
            transaction = oconn.BeginTransaction();

            //update otp in shortURl tbl
            string updateSQL = "UPDATE SLIC_CNOTE.shorturls " +
               " SET OTP = :otp, OTP_EXPIRY = :otp_expiry " +
               " WHERE ShortKey = :key ";

            OracleCommand updateCmd = new OracleCommand(updateSQL, oconn);
            updateCmd.Transaction = transaction;
            updateCmd.Parameters.Add(":otp", otp);
            updateCmd.Parameters.Add(":otp_expiry", otpExpiry);
            updateCmd.Parameters.Add(":key", shortKey);
            int updateOtp = updateCmd.ExecuteNonQuery();

            if (updateOtp <= 0)
            {
                result = false;
            }
            else
            {

                //send otp to customer 
                string exe_Insert = "INSERT INTO SMS.SMS_GATEWAY(APPLICATION_ID, JOB_CATEGORY, SMS_TYPE, MOBILE_NUMBER, TEXT_MESSAGE, SHORT_CODE, RECORD_STATUS, JOB_OTHER_INFO) ";
                exe_Insert += "VALUES(:PIN_APPLICATION_ID, :PIN_JOB_CATEGORY, :PIN_SMS_TYPE, :PIN_MOBILE_NUMBER, :PIN_TEXT_MESSAGE, :PIN_SHORT_CODE, :PIN_RECORD_STATUS, :PIN_JOB_OTHER_INFO)";

                // Execute the query for each FireRenewalMast in the list using the transaction
                OracleCommand cmd = new OracleCommand(exe_Insert, oconn);
                cmd.Transaction = transaction;  // Associate the command with the transaction
                cmd.Parameters.Add(":PIN_APPLICATION_ID", OracleType.VarChar).Value = "FR_RN_OTP";
                cmd.Parameters.Add(":PIN_JOB_CATEGORY", OracleType.VarChar).Value = "CAT151";
                cmd.Parameters.Add(":PIN_SMS_TYPE", OracleType.VarChar).Value = "I";
                cmd.Parameters.Add(":PIN_MOBILE_NUMBER", OracleType.VarChar).Value = mobileNo; //94777554194
                cmd.Parameters.Add(":PIN_TEXT_MESSAGE", OracleType.VarChar).Value = "Your Fire Renewal Notice OTP is " + otp;//"Motor Quotation Request from Bank. Reference ID : " + reqId;

                cmd.Parameters.Add(":PIN_SHORT_CODE", OracleType.VarChar).Value = "SLIC";
                cmd.Parameters.Add(":PIN_RECORD_STATUS", OracleType.VarChar).Value = "N";
                cmd.Parameters.Add(":PIN_JOB_OTHER_INFO", OracleType.VarChar).Value = policyNo;
                int rowsAffected = cmd.ExecuteNonQuery();  // Execute the SQL command

                if (rowsAffected <= 0)
                {
                    result = false;
                }
                else
                {
                    result = true;
                }
                
            }




            // If all records are inserted successfully, commit the transaction
            if (result)
            {
                transaction.Commit();
            }
            else
            {
                // If any record fails, roll back the entire transaction
                transaction.Rollback();
            }
        }
        catch (Exception ex)
        {
            // Handle exceptions and roll back the transaction in case of error
            if (transaction != null)
            {
                transaction.Rollback();
            }

            result = false;

        }
        finally
        {
            // Close the connection if it's still open
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return result;
    }

    //get claimlist of policy
    public string GetClaimList(string polno)
    {
        string sql = " SELECT A.PMPOL AS POLICY_NO, A.*,b.* FROM GENPAY.PAYFLE A " +
                    "  JOIN INTERNET.INTFLE B ON A.PMPOL = B.INPNO " +
                    " WHERE A.PMPOL = '" + polno + "' ";

        return sql;
    }
}