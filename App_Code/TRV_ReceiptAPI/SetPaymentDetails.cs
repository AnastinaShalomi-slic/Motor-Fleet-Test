using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

public class SetPaymentDetails
{
    SavePaymentDetails_API savePaymentDetails_API = new SavePaymentDetails_API();
    SavePaymentDetails_API.PaymentDetailsModel paymentDetailsModel = new SavePaymentDetails_API.PaymentDetailsModel();
    TrvApiLogger apiLogger = new TrvApiLogger();

    public SetPaymentDetails()
    {

    }

    string pol_no;
    string sqNo;

    public async Task<string> AddPaymentDetails(string refno)
    {   
        try
        {

            //TRV_PolMast trv_proposal_tbl = new TRV_PolMast(refno);

            paymentDetailsModel.sliBranch = 999;
           // paymentDetailsModel.policyNo = trv_proposal_tbl.policy_no;

           // pol_no = trv_proposal_tbl.policy_no;

           // TRV_PolMast trv_polmast_tbl = new TRV_PolMast(refno, pol_no);

            //getting data from polmast table

            OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["OracleDB"]);

            // Fetch policy number using refno
            using (oconn)
            {
                if (oconn.State != ConnectionState.Open)
                {
                    oconn.Open();
                }

                string sql_getpolno = "SELECT POLICY_NUMBER FROM SLIC_NET.PROPOSAL_DETAILS WHERE REF_NO = :refno";
                using (var cmd2 = new OracleCommand(sql_getpolno, oconn))
                {
                    cmd2.Parameters.Add(new OracleParameter("refno", refno));
                    pol_no = cmd2.ExecuteScalar().ToString(); // Retrieves the first column of the first row

                    if (string.IsNullOrEmpty(pol_no))
                    {
                        apiLogger.WriteLog("Policy number not found for the provided reference number: " + refno);
                        throw new Exception("Policy number not found for the provided reference number.");
                    }
                    else
                    {
                        apiLogger.WriteLog("Policy number retrieved: " + pol_no);
                    }
                }

                // Fetch details from TRV_POL_MAST table using polno
                string sql_getdetails = "SELECT pol_type, ref_no, DEPART_DATE, RETURN_DATE , TITLE ,FULL_NAME , ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, MOBILE_NUMBER, PLAN, SUM_INS_USD, USD_RATE, FINAL_PREMIUM_RS, NET_PREMIUM_RS, NBT_RS, VAT_RS, ADMIN_FEE_RS, POLICY_FEE_RS FROM SLIGEN.TRV_POL_MAST WHERE POLNO = :polno";
                using (var cmd = new OracleCommand(sql_getdetails, oconn))
                {
                    cmd.Parameters.Add(new OracleParameter("polno", pol_no));
                    using (var reader = cmd.ExecuteReader())
                    {
                      
                        if (reader.Read())
                        {
                            apiLogger.WriteLog("Succeed at AddPaymentDetails: reader.Read()");
                            try
                            {
                                paymentDetailsModel.policyType = "TPI";
                                paymentDetailsModel.departmentCode = "G";
                                paymentDetailsModel.subDepartment = "G";
                                paymentDetailsModel.name1 = reader["FULL_NAME"].ToString().Trim();
                                paymentDetailsModel.status = reader["TITLE"].ToString().Trim();
                                paymentDetailsModel.address1 = reader["ADDRESS1"].ToString().Trim();
                                paymentDetailsModel.address2 = reader["ADDRESS2"].ToString().Trim();
                                paymentDetailsModel.address3 = reader["ADDRESS3"].ToString().Trim();
                                paymentDetailsModel.address4 = reader["ADDRESS4"].ToString().Trim();
                                int sumInsured = reader.IsDBNull(reader.GetOrdinal("SUM_INS_USD")) ? 0 : Convert.ToInt32(reader["SUM_INS_USD"]);
                                int usdrate = reader.IsDBNull(reader.GetOrdinal("USD_RATE")) ? 0 : Convert.ToInt32(reader["USD_RATE"]);
                                paymentDetailsModel.sumInsured = sumInsured * usdrate;
                                paymentDetailsModel.premium = reader.IsDBNull(reader.GetOrdinal("NET_PREMIUM_RS")) ? 0 : Convert.ToDouble(reader["NET_PREMIUM_RS"]);
                                paymentDetailsModel.vat = reader.IsDBNull(reader.GetOrdinal("VAT_RS")) ? 0 : Convert.ToDouble(reader["VAT_RS"]);
                                paymentDetailsModel.policyFee = reader.IsDBNull(reader.GetOrdinal("POLICY_FEE_RS")) ? 0 : Convert.ToDouble(reader["POLICY_FEE_RS"]);

                               // Handle policy start and end dates

                                DateTime startDate;

                                if (DateTime.TryParse(reader["DEPART_DATE"].ToString(), out startDate))
                                {
                                    paymentDetailsModel.startDate = startDate;

                                    paymentDetailsModel.commenceDate = startDate;
                                    apiLogger.WriteLog("Succeed at AddPaymentDetails: " + reader["DEPART_DATE"].ToString().Trim());
                                }
                                else
                                {
                                    paymentDetailsModel.startDate = DateTime.Now;
                                }

                                DateTime endDate;

                                if (DateTime.TryParse(reader["RETURN_DATE"].ToString(), out endDate))
                                {
                                    paymentDetailsModel.expireDate = endDate;

                                    apiLogger.WriteLog("Succeed at AddPaymentDetails: " + reader["RETURN_DATE"].ToString().Trim());
                                }
                                else
                                {
                                    paymentDetailsModel.expireDate = DateTime.Now;
                                }

                                paymentDetailsModel.paymentAmount1 = reader.IsDBNull(reader.GetOrdinal("FINAL_PREMIUM_RS")) ? 0 : Convert.ToDouble(reader["FINAL_PREMIUM_RS"]);
                                paymentDetailsModel.paymentDate = DateTime.Now;
                                paymentDetailsModel.currencyCode = "LKR";
                                paymentDetailsModel.taxStatusId = "";
                                paymentDetailsModel.isWithHoldingTax = "Y";
                                paymentDetailsModel.businessTypeID = 1;
                                paymentDetailsModel.paymentMode = 1;
                                //admin fee
                                paymentDetailsModel.cess = reader.IsDBNull(reader.GetOrdinal("ADMIN_FEE_RS")) ? 0 : Convert.ToDouble(reader["ADMIN_FEE_RS"]);                              
                                paymentDetailsModel.nbt = reader.IsDBNull(reader.GetOrdinal("NBT_RS")) ? 0 : reader.GetDouble(reader.GetOrdinal("NBT_RS"));                            
                                var apiReaponse = await savePaymentDetails_API.AddDetails(paymentDetailsModel);

                                sqNo = apiReaponse.Data.ToString();

                                //update slic_net.proposal_details table.
                                if (!string.IsNullOrEmpty(sqNo))
                                {
                                    string update_query = "UPDATE SLIC_NET.PROPOSAL_DETAILS SET POL_ISSUED = 'Y' WHERE REF_NO = :refno";
                                    using (var cmd_update = new OracleCommand(update_query, oconn))
                                    {
                                        cmd_update.Parameters.Add(new OracleParameter("refno", refno));
                                        cmd_update.ExecuteNonQuery();
                                    }
                                }
                                else
                                {
                                    apiLogger.WriteLog("Failed at AddPaymentDetails: slic_net.proposal_details 'pol_issued' update failed.");
                                }
                                apiLogger.WriteLog("Succeed at AddPaymentDetails: " + " " + sqNo);
                                return sqNo;
                            }
                            catch (Exception ex)
                            {
                                apiLogger.WriteLog("Failed at AddPaymentDetails: " + ex.ToString());
                                return null;
                            }

                        }
                    }
                }
            }
             return sqNo;
        }
        catch (Exception ex)
        {
            apiLogger.WriteLog("Failed at AddPaymentDetails: " + ex.ToString());
            return null;
        }
    }
}
    