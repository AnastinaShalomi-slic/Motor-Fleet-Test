using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ODP_DQL
/// </summary>
public class ODP_DQL
{
    public string GetCustomerODP_Profile(string proposal_no)
    {
        string sql = string.Empty;
        sql += "SELECT ODP.PRNO, ODP.SRID, ODP.TITLE, ODP.CUS_NAME, ODP.ADD_L1, ODP.ADD_L2, ODP.ADD_L3, ODP.ADD_L4, to_char(ODP.DOB,'dd/mm/yyyy') as DOB, ODP.NIC, ODP.CONNOMOB, ODP.E_MAIL, ODP.BTUPE, FODP.SUMINSURD, ";
        sql += "FODP.NETPREMIUM, FODP.SRCC, FODP.TC, FODP.ADMIN_FEE, FODP.POLICY_FEE, FODP.VAT, FODP.TOT_PREMIUM ";
        sql += "FROM QUOTATION.ODP_PROPOSAL_ENTRY ODP ";
        sql += "INNER JOIN QUOTATION.ODP_PREMIUM FODP ";
        sql += "ON ODP.SRID = FODP.SRID ";
        sql += "WHERE ODP.PRNO = '" + proposal_no + "'";
        return sql;
    }

    public string GetPayAdvice(string srid)
    {
        string sql = string.Empty;
        sql += "SELECT PRP.POLNO, (PRP.TITLE||PRP.CUS_NAME) AS CUS_NAME, PRP.NIC, to_char(PINFO.POLICY_SDATE, 'dd/mm/yyyy') as POLICY_SDATE, to_char(PINFO.POLICY_EDATE, 'dd/mm/yyyy') as POLICY_EDATE, PINFO.SUMINSURD, PINFO.TOT_PREMIUM, BNK.BBNAM, BNK.BBRNCH  FROM  QUOTATION.ODP_PROPOSAL_ENTRY PRP ";
        sql += "INNER JOIN QUOTATION.ODP_POLICYINFO PINFO ";
        sql += "ON PRP.SRID = PINFO.SRID ";
        sql += "INNER JOIN GENPAY.BNKBRN BNK ";
        sql += "ON BNK.BCODE = PRP.BANK_CODE AND BNK.BBCODE = PRP.BRANCH_CODE ";
        sql += "WHERE PRP.SRID = '" + srid + "'";
        return sql;
    }

    public string GetPolicyPrintInfo(string srid)
    {
        string sql = string.Empty;
        sql += "SELECT Q1.POLNO, (Q1.TITLE||Q1.CUS_NAME) AS CUS_NAME, Q1.NIC,  Q1.ADD_L1, Q1.ADD_L2, Q1.ADD_L3, Q1.ADD_L4, Q3.BBNAM, Q3.BBRNCH, TO_CHAR(Q1.POLICY_SDATE, 'dd/MM/yyyy') AS POLICY_SDATE, TO_CHAR(Q1.POLICY_EDATE, 'dd/MM/yyyy') AS POLICY_EDATE,  TO_CHAR((Q1.POLICY_EDATE + 1), 'dd/MM/yyyy') AS POLICY_REDATE, ";
        sql += "Q2.SUMINSURD, Q2.NETPREMIUM, Q2.ADMIN_FEE, Q2.POLICY_FEE, Q2.VAT FROM QUOTATION.ODP_PROPOSAL_ENTRY Q1 ";
        sql += "INNER JOIN QUOTATION.ODP_PREMIUM Q2 ";
        sql += "ON Q1.POLNO = Q2.POLNO ";
        sql += "INNER JOIN GENPAY.BNKBRN Q3 ";
        sql += "ON Q3.BCODE = Q1.BANK_CODE AND Q3.BBCODE = Q1.BRANCH_CODE ";
        sql += "WHERE Q1.SRID = '" + srid + "'";
        return sql;
    }
}