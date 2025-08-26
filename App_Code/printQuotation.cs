using System;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;

/// <summary>
/// Summary description for printQuotation
/// </summary>

public class printQuotation
{

    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["DBConString"]);

    public printQuotation()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string pushprintQuotation(string quotNo, out string dateOfIssue, out string vehicleType, out string make, out string sumInsured,
        out string periodOfCover, out string vehicleChasNo, out string yOm, out string fuelType, out string netPremium, out string rcc, out string tr, out string adminFee,
        out string roadSafetyFund, out string stampDuty, out string policyFee, out string nbt, out string taxes, out string totalPre, out string adCovers,
        out string issuedBy, out string pdfDate, out string epfAgentCode, out string passangers,out string insuredName,out string note, out string emailId,out bool IsVechleNo,out string model)
    {
        string mgs = "";
        adminFee = ""; dateOfIssue = ""; vehicleType = ""; make = ""; sumInsured = ""; periodOfCover = ""; vehicleChasNo = ""; yOm = ""; fuelType = ""; netPremium = "";
        rcc = ""; tr = ""; roadSafetyFund = ""; stampDuty = ""; policyFee = ""; nbt = ""; taxes = ""; totalPre = ""; adCovers = ""; issuedBy = ""; pdfDate = ""; epfAgentCode = ""; emailId = ""; model = "";
          passangers = ""; insuredName=""; note = ""; IsVechleNo = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

                string sqlCountQout = "SELECT COUNT(*) FROM quotation.issued_quotations qut WHERE qut.qref_no = :txtQuotNo";

                OracleParameter para1 = new OracleParameter();
                para1.Value = quotNo.ToUpper().Trim();
                para1.ParameterName = "txtQuotNo";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountQout;

                int countQuot = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countQuot == 1)
                {
                    string sqlGetQoutData = "SELECT " +
                          "qut.qent_date, qut.qcateg, qut.qmake, " + //2
                          "qut.qval, qut.qfromdt, qut.qtodat, " + // 5
                          "qut.qprov, qut.qvno1, qut.qvno2, qut.qyear, qut.qfuel, " + // 10
                          "qut.qtotal_net, qut.qnet_rcc, qut.qnet_tr,qut.qadmin_fee, qut.qroad_tax, qut.qstamp_fee, " + //16
                          "qut.qpol_fee, qut.qnbt, qut.qvat, qut.qtotal_prm, " + //20
                          "qut.qaddi_cover, " + //21
                          "qut.qpdf_user_id,  qut.qpdf_date,qut.quser_id, qut.qagents,sysdate, " + //26
                          "qut.qnump, qut.qname, qut.qstat, qut.note, qut.email, qut.qchno, qut.Model " + //33
                          "FROM quotation.issued_quotations qut " +
                          "WHERE qut.qref_no = :txtQuotNo2";

                    OracleParameter para2 = new OracleParameter();
                    para2.Value = quotNo.ToUpper().Trim();
                    para2.ParameterName = "txtQuotNo2";
                    cmd.Parameters.Add(para2);

                    cmd.CommandText = sqlGetQoutData;

                    //cmd.Parameters.Clear();

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            //if (!reader.IsDBNull(0))
                            //{
                            if (!reader.IsDBNull(0))
                            {
                                dateOfIssue = reader.GetDateTime(0).ToString("dd/MM/yyy");
                            }
                            if (!reader.IsDBNull(1))
                            {
                                vehicleType = reader.GetString(1).ToUpper();
                            }
                            if (!reader.IsDBNull(2))
                            {
                                make = reader.GetString(2).ToUpper();
                            }
                            if (!reader.IsDBNull(3))
                            {
                                sumInsured = reader.GetDouble(3).ToString("###,###,###0.00");
                            }
                            if (!reader.IsDBNull(4) || !reader.IsDBNull(5))
                            {
                                periodOfCover = reader.GetDateTime(4).ToString("dd/MM/yyy") + " - " + reader.GetDateTime(5).ToString("dd/MM/yyy");
                            }

                            string veh1 = "", veh2 = "", veh3 = "";

                            if (!reader.IsDBNull(6))
                            {
                                veh1 = reader.GetString(6) + " ";
                            }
                            if (!reader.IsDBNull(7))
                            {
                                veh2 = reader.GetString(7) + " ";

                            }

                            if (!reader.IsDBNull(8))
                            {
                                veh3 = reader.GetString(8);

                            }

                            if (!string.IsNullOrEmpty(veh1) && !string.IsNullOrEmpty(veh2) && !string.IsNullOrEmpty(veh3))
                            {
                                vehicleChasNo = veh1.ToUpper() + veh2.ToUpper() + veh3.ToUpper();
                                IsVechleNo = true;
                            }
                            else
                            {
                                if (!reader.IsDBNull(32))
                                {
                                   
                                    vehicleChasNo = reader.GetString(32).ToUpper();
                                    IsVechleNo = false;

                                }
                                else
                                {
                                    vehicleChasNo = "UNREGISTERED";
                                    IsVechleNo = true;
                                }
                               
                            }

                            if (!reader.IsDBNull(9))
                            {

                                yOm = reader.GetString(9);
                                if (yOm == "0")
                                {
                                    yOm = "";
                                }
                            }
                            if (!reader.IsDBNull(10))
                            {
                                fuelType = reader.GetString(10).ToUpper();

                            }
                            if (!reader.IsDBNull(11))
                            {
                                netPremium = reader.GetDouble(11).ToString("###,###,###0.00");

                            }
                            else
                            {
                                netPremium = "0.00";
                            }
                            if (!reader.IsDBNull(12))
                            {
                                rcc = reader.GetDouble(12).ToString("###,###,###0.00");

                            }
                            else
                            {
                                rcc = "0.00";
                            }
                            if (!reader.IsDBNull(13))
                            {
                                tr = reader.GetDouble(13).ToString("###,###,###0.00");

                            }
                            else
                            {
                                tr = "0.00";
                            }

                            if (!reader.IsDBNull(14))
                            {
                                adminFee = reader.GetDouble(14).ToString("###,###,###0.00");

                            }
                            else
                            {
                                adminFee = "0.00";
                            }
                            if (!reader.IsDBNull(15))
                            {
                                roadSafetyFund = reader.GetDouble(15).ToString("###,###,###0.00");

                            }
                            else
                            {
                                roadSafetyFund = "0.00";
                            }
                            if (!reader.IsDBNull(16))
                            {
                                stampDuty = reader.GetDouble(16).ToString("###,###,###0.00");

                            }
                            else
                            {
                                stampDuty = "0.00";
                            }
                            if (!reader.IsDBNull(17))
                            {
                                policyFee = reader.GetDouble(17).ToString("###,###,###0.00");

                            }
                            else
                            {
                                policyFee = "0.00";
                            }
                            if (!reader.IsDBNull(18))
                            {
                                nbt = reader.GetDouble(18).ToString("###,###,###0.00");

                            }
                            else
                            {
                                nbt = "0.00";
                            }
                            if (!reader.IsDBNull(19))
                            {
                                taxes = reader.GetDouble(19).ToString("###,###,###0.00");

                            }
                            else
                            {
                                taxes = "0.00";
                            }
                            if (!reader.IsDBNull(20))
                            {
                                totalPre = reader.GetDouble(20).ToString("###,###,###0.00");

                            }
                            else
                            {
                                totalPre = "0.00";
                            }
                            if (!reader.IsDBNull(21))
                            {
                                //adCovers = reader.GetString(21).Replace("*", "");
                                if(Convert.ToBoolean(reader.GetString(21).LastIndexOf(",")))
                                {
                                    adCovers = reader.GetString(21).Remove(reader.GetString(21).LastIndexOf(",")).Replace("*", "");
                                }
                                else
                                {
                                    adCovers = reader.GetString(21).Replace("*", "");
                                }
                                

                            }
                            else
                            {
                                adCovers = "No cover extension added !";
                            }
                            if (!reader.IsDBNull(22))
                            {
                                issuedBy = reader.GetString(22);

                            }
                         
                            string Epf = "", agent = "";
                            if (!reader.IsDBNull(24))
                            {
                                Epf = reader.GetString(24);

                            }
                            if (!reader.IsDBNull(25))
                            {
                                agent = reader.GetString(25);

                            }

                            if (!string.IsNullOrEmpty(Epf))
                            {
                                Epf = Epf + "/";
                            }
                            else
                            {
                                Epf = "";
                            }

                            epfAgentCode = Epf + "Agent Code : " + agent;

                            if (!reader.IsDBNull(26))
                            {

                                pdfDate = reader.GetDateTime(26).ToString("dd/MM/yyyy hh:mm ss tt"); // sysdate
                            }

                            if (!reader.IsDBNull(27))
                            {
                                passangers = reader.GetDouble(27).ToString();

                            }
                            else
                            {
                                passangers = "0";
                            }

                            if (!reader.IsDBNull(28))
                            {
                                if(!reader.IsDBNull(29))
                                {
                                    insuredName = reader.GetString(29).ToUpper() + " " + reader.GetString(28).ToUpper();
                                }
                                else
                                {
                                    insuredName =  reader.GetString(28).ToUpper();
                                }
                               

                            }
                            else
                            {
                                insuredName = "UNKNOWN";
                            }

                            if (!reader.IsDBNull(30))
                            {
                                if (Convert.ToBoolean(reader.GetString(30).LastIndexOf(",")))
                                {
                                    note = reader.GetString(30).Remove(reader.GetString(30).LastIndexOf(",")).Replace("*", "");
                                }
                                else
                                {
                                    note = reader.GetString(30).Replace("*", "");
                                }

                                //note = reader.GetString(30);

                            }

                            if (!reader.IsDBNull(31))
                            {

                                emailId = reader.GetString(31).ToLower().Trim();
                              

                            }

                            if (!reader.IsDBNull(33))
                            {

                                model = reader.GetString(33).ToUpper().Trim();


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

        return mgs;
    }

    public int quotationApprovalIs(string refNo)
    {
        int rtn = 0;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();

            using (cmd)
            {
                string sqlCountQuotRefReq = "SELECT COUNT(*) FROM quotation.quotation_approval_request r " +
                                            "WHERE r.qref_no = :txtRefNo AND r.activeflag='Y'";

                cmd.CommandText = sqlCountQuotRefReq;

                OracleParameter para1 = new OracleParameter();
                para1.Value = refNo.Trim().ToUpper();
                para1.ParameterName = "txtRefNo";
                cmd.Parameters.Add(para1);

                int CountQuotRefReq = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                if(CountQuotRefReq > 0)
                {
                    string sqlCountQuotReqApprove = "SELECT COUNT(*) FROM quotation.quotation_approval_request r " +
                                           "WHERE r.qref_no = :txtRefNo AND r.activeflag='Y' AND r.approved_status='Y'";

                    cmd.CommandText = sqlCountQuotReqApprove;

                    OracleParameter para2 = new OracleParameter();
                    para2.Value = refNo.Trim().ToUpper();
                    para2.ParameterName = "txtRefNo";
                    cmd.Parameters.Add(para2);

                    int CountQuotReqApprove = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                    if(CountQuotReqApprove == 1)
                    {
                        rtn = 1;
                    }
                    else
                    {
                        rtn = 0;
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
        return rtn;
    }


    public bool quotDiscountCatcher(string refNo)
    {
        bool rtn = false;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();

            using (cmd)
            {
                string sqlCountQuotRefReq = "SELECT COUNT(*) FROM quotation.issued_quotations q " +
                                            "WHERE  (q.qncb_percent IS NOT NULL OR q.qmr_vl IS NOT NULL) " +
                                            "AND q.qref_no = :txtRefNo";

                cmd.CommandText = sqlCountQuotRefReq;

                OracleParameter para1 = new OracleParameter();
                para1.Value = refNo.Trim().ToUpper();
                para1.ParameterName = "txtRefNo";
                cmd.Parameters.Add(para1);

                int CountQuotRefReq = Convert.ToInt32(cmd.ExecuteScalar().ToString());

               
                    if (CountQuotRefReq == 1)
                    {
                        rtn = true;
                    }
                    else
                    {
                        rtn = false ;
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

        return rtn;
    }

}
