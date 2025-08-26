using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for approvalRequestReport
/// </summary>
public class ApprovalRequestReport
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["DBConStrQuot"]);
    public ApprovalRequestReport()
    {
    }

    public DataTable GetQuotationDetails(int Userbrcode, string fromDate, string toDate, int brCode, string ref_no, string app_status)
    {
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();

        if (oconn.State != ConnectionState.Open)
        {
            oconn.Open();
        }
        try
        {
      

            string sql = string.Empty;

            sql = " SELECT  qref_no, REQUEST_ID, QTOTAL_PRM, IName, brnam, qent_date, app_status,APPROVED_DATE, REQUESTED_DATE, request_id, APPROVED_DATE, DATESTAMP ";
            sql += "  FROM ";
            sql += "  ( ";
            sql += " select qs.qref_no, (qs.QVNO1|| ' '|| qs.qvno2) as VNO, ";
            sql += "qs.QTOTAL_PRM, (qs.qstat||' '|| qs.qname) as IName, br.brnam, to_char(qs.qent_date,'dd/mm/yyyy') as qent_date, ";
            sql += "case approved_status ";
            sql += "when 'Y' then 'Approved' ";
            sql += "when 'N' then 'Pending' ";
            sql += "when 'R' then 'Rejected' ";
            sql += "end as app_status, to_char(DATESTAMP,'dd/mm/yyyy') as REQUESTED_DATE, request_id, APPROVED_DATE, DATESTAMP ";
            sql += "from QUOTATION.ISSUED_QUOTATIONS qs ";
            sql += "inner join genpay.branch br ";
            sql += "on qs.qbranch = br.brcod ";
            sql += "inner join quotation.quotation_approval_request qr ";
            sql += "on qr.qref_no =qs.qref_no ";
            sql += "where br.brcod is not null ";
            sql += "and qr.activeflag = 'Y' and qr.approved_status !='N' ";
            if (app_status != "A")
            {
                sql += "and qr.approved_status='" + app_status + "' ";
            }

            if (Userbrcode == 10)
            {
                if (brCode != 0) { sql += "and  br.brcod = " + brCode + " "; }
                else if (brCode == 0) { }

            }
            else
            {
                if (brCode != 0) { sql += "and  br.brcod = " + brCode + " "; }
            }


            if (!string.IsNullOrEmpty(ref_no)) { sql += "and qs.qref_no='" + ref_no + "' "; }


            if (!string.IsNullOrEmpty(fromDate) && !string.IsNullOrEmpty(toDate))
            {
                sql += "and( qs.qent_date> = to_date('" + fromDate + "','dd/MM/yyyy')  and qs.qent_date<=to_date('" + toDate + "','dd/MM/yyyy')) ";
            }
            //sql += "order by qs.qent_date desc ";
            sql += "order by qr.APPROVED_DATE desc ";
            sql += ")";

            using (OracleDataAdapter oda = new OracleDataAdapter(sql, oconn))
            {

                ds.Clear();

                oda.Fill(ds);

                if (ds != null)
                {
                    dt = ds.Tables[0];
                    //dt = ds.Tables[1];
                }

            }



        }
        catch (Exception)
        {
            dt = null;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return dt;
       

      
    }
}