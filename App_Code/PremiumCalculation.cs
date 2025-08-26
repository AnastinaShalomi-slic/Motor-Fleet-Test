using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PremiumCalculation
/// </summary>
public class PremiumCalculation
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    Ip_Class get_ip = new Ip_Class();
    EncryptDecrypt dc = new EncryptDecrypt();
    ORCL_Connection orcl_con = new ORCL_Connection();

    DeviceFinder df = new DeviceFinder();
    public string[] ArryCovers = new string[13];

    // OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["DBConString"]);
    public PremiumCalculation()
    {}
    // rates for Annual or Long (not for Long term All covers-->> its basic rate given to according to sum insured)
    public bool GetRatesForAnnualOrLong(string bank_code, string term, out string BANK, out double BASIC, out double RCC, out double TR, out double ADMIN_FEE, out double POLICY_FEE,
        out double RENEWAL_FEE, out double BASIC_2, out string RateTerm, out bool rtn)
    {
        rtn = false;
        BANK = "";
        BASIC = 0;
        RCC = 0;
        TR = 0;
        ADMIN_FEE = 0;
        POLICY_FEE = 0;
        RENEWAL_FEE = 0;
        BASIC_2 = 0;
        RateTerm = "";

        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetFireRate(bank_code, term), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    BANK = details.Rows[0]["BANK"].ToString();
                    BASIC = Convert.ToDouble(details.Rows[0]["BASIC"].ToString());
                    RCC = Convert.ToDouble(details.Rows[0]["RCC"].ToString());
                    TR = Convert.ToDouble(details.Rows[0]["TR"].ToString());
                    ADMIN_FEE = Convert.ToDouble(details.Rows[0]["ADMIN_FEE"].ToString());
                    POLICY_FEE = Convert.ToDouble(details.Rows[0]["POLICY_FEE"].ToString());
                    RENEWAL_FEE = Convert.ToDouble(details.Rows[0]["Renewal_FEE"].ToString());
                    //
                    BASIC_2 = Convert.ToDouble(details.Rows[0]["BASIC_2"].ToString());
                    RateTerm = details.Rows[0]["TERM"].ToString();

                    rtn = true;
                }
                else
                {

                    rtn =  false;
                }
            }
            else
            {
                rtn = false;
                
            }
        }
        catch (Exception ex)
        {
            rtn = false;
        }

        return rtn;

    }

    // get long term all covers rates----->>
    public bool GetRatesForLongTermAll(string bank_code, double maxVal, double minVal, out double BasicLongAllRate, out bool rtn)
    {
        rtn = false;

        BasicLongAllRate = 0;


        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetLongTermRateForAllCover(bank_code, maxVal, minVal), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    BasicLongAllRate = Convert.ToDouble(details.Rows[0]["rate"].ToString());
                    rtn = true;
                }
                else
                {

                    rtn = false;
                }
            }
            else
            {
                rtn = false;

            }
        }
        catch (Exception ex)
        {
            rtn = false;
        }

        return rtn;

    }



    // discount Rates for long term fire and lighting only
    public bool GetDiscountForLongFireOnly(string bank_code, double years,out double DISCOUNT_RATE, out bool rtn)
    {
        rtn = false;
   
        DISCOUNT_RATE = 0;
    

        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetDiscountRate(bank_code, years), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    DISCOUNT_RATE = Convert.ToDouble(details.Rows[0]["rate"].ToString());
                    rtn = true;
                }
                else
                {

                    rtn = false;
                }
            }
            else
            {
                rtn = false;

            }
        }
        catch (Exception ex)
        {
            rtn = false;
        }

        return rtn;

    }

    // get solar rates----->>
    public bool GetRatesForSolar(string bank_code, double maxVal, double minVal, out double SOLAR_RATE, out bool rtn)
    {
        rtn = false;

        SOLAR_RATE = 0;


        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetSolartes(bank_code, maxVal, minVal), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    SOLAR_RATE = Convert.ToDouble(details.Rows[0]["rate"].ToString());
                    rtn = true;
                }
                else
                {

                    rtn = false;
                }
            }
            else
            {
                rtn = false;

            }
        }
        catch (Exception ex)
        {
            rtn = false;
        }

        return rtn;

    }



    // get BPF rates 27042023 another fee for BSO----->>
    public bool GetBPF_Values(string bank_code, double maxVal, double minVal, out double BPF_Value, out bool rtn)
    {
        rtn = false;

        BPF_Value = 0;


        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetBancFee(bank_code, maxVal, minVal), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    BPF_Value = Convert.ToDouble(details.Rows[0]["BFEE"].ToString());
                    rtn = true;
                }
                else
                {

                    rtn = false;
                }
            }
            else
            {
                rtn = false;

            }
        }
        catch (Exception ex)
        {
            rtn = false;
        }

        return rtn;

    }
    // check this user and branch eligible to obtain BPF -- Autthority check

    public bool GetBPF_AuthforBranch(string bank_code, string branchCode, string uName, out string BPF_Autho, out bool rtn)
    {
        rtn = false;

        BPF_Autho = "";


        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetBranchBPFAllowOrNot(Convert.ToInt32(bank_code), Convert.ToInt32(branchCode), uName), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    BPF_Autho = details.Rows[0]["active_flag"].ToString();
                    rtn = true;
                }
                else
                {

                    rtn = false;
                }
            }
            else
            {
                rtn = false;

            }
        }
        catch (Exception ex)
        {
            rtn = false;
        }

        return rtn;

    }



    // premium calculation -->>

    // 27042023 changes also applied here.

    public bool PremiumCalculationForAllTypes(string bank_code, double year, string term, double buildingSumInsu, double solarVal,
            string chkFirLight,string chkOtherPerils, string chkSRCC, string chkTR, string bpfRequest, string branch_code, string uName,
        out double calNetPre, out double calRCC, out double calTR, out double calPolicyFee, out double calAdminFee, out double calRenewalFee, out double calNBT, out double calVat, out double calTotal, out double calBPF_value, out bool rtn)
    {
        rtn = false;

        calNetPre = 0;
        calRCC = 0;
        calTR = 0;
        calPolicyFee = 0;
        calAdminFee = 0;
        calRenewalFee = 0;
        calNBT = 0;
        calVat = 0;
        calTotal = 0;
        calBPF_value = 0;

        double CalNetPreTemp, Cal_RCCTemp, sumInsu, BpfsumInsu,Cal_TRTemp, Cal_POLICY_FEETemp, Cal_ADMIN_FEETemp, Cal_Renewal_FeeTemp, Cal_TotalTemp, Cal_NBTTemp, Cal_VATTemp;
        CalNetPreTemp = 0;
        // values form methods---
        // basic rates
        string rtnBank, rtnRateTerm = "";
        double rtnBasic, rtnRCC, rtnTR, rtnAdminFee, rtnPolicyFee, rtnRenewalFee, rtnBasic2 = 0;

        // basic rates for long term all covers
        double rtnBasicLongAllRate = 0;
        // return BPF value
        double rtnBPF_Value = 0;
        string BPF_Autho = "";
        // discount rate for fire only
        double rtnDiscountRate = 0;
        // solar rate
        double rtnSolarRate = 0;

        bool rtn1,rtn2,rtn3,rtn4,rtn5, rtn6 = false;
        double BASIC = 0;
        double RCC = 0; double TR = 0; double DISCOUNT_RATE = 0; double SOLAR_RATE = 0;

        try
        {
            GetRatesForAnnualOrLong(bank_code, term, out rtnBank, out rtnBasic, out rtnRCC, out rtnTR, out rtnAdminFee, out rtnPolicyFee,
            out rtnRenewalFee, out rtnBasic2, out rtnRateTerm, out rtn1);

            GetRatesForLongTermAll(bank_code, buildingSumInsu, buildingSumInsu, out rtnBasicLongAllRate, out rtn2);
            GetDiscountForLongFireOnly(bank_code, year, out rtnDiscountRate, out rtn3);
            GetRatesForSolar(bank_code, solarVal, solarVal, out rtnSolarRate, out rtn4);

            //-- BPF value --
            if(bpfRequest == "Y")
            {
                //GetBPF_AuthforBranch(bank_code, branch_code, uName, out BPF_Autho, out rtn6);
                //when super user log it causes a change so comment above
                GetBPF_AuthforBranch(bank_code, branch_code, "", out BPF_Autho, out rtn6);

                if(BPF_Autho == "Y")
                {
                    // for both solar and PDH
                    BpfsumInsu = buildingSumInsu + solarVal;

                    GetBPF_Values(bank_code, BpfsumInsu, BpfsumInsu, out rtnBPF_Value, out rtn5);
                    calBPF_value = rtnBPF_Value;
                }
                else { rtnBPF_Value = 0; calBPF_value = 0; }

               
            }
            else { rtnBPF_Value = 0; calBPF_value = 0; }
            ///--->> rtnBPF_Value add to policy fee for BPF allow banks


            // if dwelling house or solar or both
            if (buildingSumInsu != 0 && solarVal == 0) // buildinig only
            {
                if (term == "A")
                {
                    /* 
                     * basic rate change according to covers asks
                     fire and lighting only 
                     fire and other perils
                     light + other + rcc
                     light + other + rcc + tc = all covers
                    */
                    if (chkFirLight == "1" && chkOtherPerils == "0" && chkSRCC == "0" && chkTR == "0")
                    {
                        BASIC = rtnBasic2;
                        RCC = 0;
                        TR = 0;
                        DISCOUNT_RATE = 0;
                        SOLAR_RATE = rtnSolarRate;
                    }
                    else if (chkFirLight == "1" && chkOtherPerils == "1" && chkSRCC == "0" && chkTR == "0")
                    {
                        BASIC = rtnBasic;
                        RCC = 0;
                        TR = 0;
                        DISCOUNT_RATE = 0;
                        SOLAR_RATE = rtnSolarRate;
                    }
                    else if (chkFirLight == "1" && chkOtherPerils == "1" && chkSRCC == "1" && chkTR == "0")
                    {
                        BASIC = rtnBasic;
                        RCC = rtnRCC;
                        TR = 0;
                        DISCOUNT_RATE = 0;
                        SOLAR_RATE = rtnSolarRate;
                    }
                    else if (chkFirLight == "1" && chkOtherPerils == "1" && chkSRCC == "1" && chkTR == "1")
                    {
                        BASIC = rtnBasic;
                        RCC = rtnRCC;
                        TR = rtnTR;
                        DISCOUNT_RATE = 0;
                        SOLAR_RATE = rtnSolarRate;
                    }
                    else { }

                }
                else if (term == "L")
                {
                    if (chkFirLight == "1" && chkOtherPerils == "0" && chkSRCC == "0" && chkTR == "0")
                    {
                        BASIC = rtnBasic;
                        RCC = 0;
                        TR = 0;
                        DISCOUNT_RATE = rtnDiscountRate;
                        SOLAR_RATE = rtnSolarRate;
                    }
                    else if (chkFirLight == "1" && chkOtherPerils == "1" && chkSRCC == "0" && chkTR == "0")
                    {
                        BASIC = rtnBasicLongAllRate;
                        RCC = 0;
                        TR = 0;
                        DISCOUNT_RATE = 0;
                        SOLAR_RATE = rtnSolarRate;
                    }
                    else if (chkFirLight == "1" && chkOtherPerils == "1" && chkSRCC == "1" && chkTR == "0")
                    {
                        BASIC = rtnBasicLongAllRate;
                        RCC = rtnRCC;
                        TR = 0;
                        DISCOUNT_RATE = 0;
                        SOLAR_RATE = rtnSolarRate;
                    }
                    else if (chkFirLight == "1" && chkOtherPerils == "1" && chkSRCC == "1" && chkTR == "1")
                    {
                        BASIC = rtnBasicLongAllRate;
                        RCC = rtnRCC;
                        TR = rtnTR;
                        DISCOUNT_RATE = 0;
                        SOLAR_RATE = rtnSolarRate;
                    }
                    else { }
                }
                else
                {

                }
            }
            else if (buildingSumInsu == 0 && solarVal != 0) // solar only
            {
                /* 
                    * basic rate change according to covers asks
                    fire and lighting only 
                    fire and other perils
                    light + other + rcc
                    light + other + rcc + tc = all covers
                   */
                if (chkFirLight == "1" && chkOtherPerils == "0" && chkSRCC == "0" && chkTR == "0")
                {
                    BASIC = 0;
                    RCC = 0;
                    TR = 0;
                    DISCOUNT_RATE = 0;
                    SOLAR_RATE = rtnSolarRate;
                }
                else if (chkFirLight == "1" && chkOtherPerils == "1" && chkSRCC == "0" && chkTR == "0")
                {
                    BASIC = 0;
                    RCC = 0;
                    TR = 0;
                    DISCOUNT_RATE = 0;
                    SOLAR_RATE = rtnSolarRate;
                }
                else if (chkFirLight == "1" && chkOtherPerils == "1" && chkSRCC == "1" && chkTR == "0")
                {
                    BASIC = 0;
                    RCC = rtnRCC;
                    TR = 0;
                    DISCOUNT_RATE = 0;
                    SOLAR_RATE = rtnSolarRate;
                }
                else if (chkFirLight == "1" && chkOtherPerils == "1" && chkSRCC == "1" && chkTR == "1")
                {
                    BASIC = 0;
                    RCC = rtnRCC;
                    TR = rtnTR;
                    DISCOUNT_RATE = 0;
                    SOLAR_RATE = rtnSolarRate;
                }
                else { }

            }
            else if (buildingSumInsu != 0 && solarVal != 0) // solar + dwelling house
            { }
            else { }

            // calculation process for all types------------------------->>>>
            if (buildingSumInsu != 0 && solarVal == 0) // building only
            {
                CalNetPreTemp = (((buildingSumInsu * BASIC * year) / 100) - (((buildingSumInsu * BASIC * year) / 100) * DISCOUNT_RATE)) + (SOLAR_RATE * year * solarVal);
                calNetPre = Math.Round((((buildingSumInsu * BASIC * year) / 100) - (((buildingSumInsu * BASIC * year) / 100) * DISCOUNT_RATE)) + (SOLAR_RATE * year * solarVal), 2, MidpointRounding.AwayFromZero);
            }
            else if (buildingSumInsu == 0 && solarVal != 0) // solar only
            {
               if (solarVal < 500000)
                {
                    CalNetPreTemp = 2000 * year;
                    calNetPre = 2000 * year;
                }
                else
                {
                    CalNetPreTemp = (((buildingSumInsu * BASIC * year) / 100) - (((buildingSumInsu * BASIC * year) / 100) * DISCOUNT_RATE)) + (SOLAR_RATE * year * solarVal);
                    calNetPre = Math.Round((((buildingSumInsu * BASIC * year) / 100) - (((buildingSumInsu * BASIC * year) / 100) * DISCOUNT_RATE)) + (SOLAR_RATE * year * solarVal), 2, MidpointRounding.AwayFromZero);
                }
            }
            else { }


            sumInsu = buildingSumInsu + solarVal;

            Cal_RCCTemp = (sumInsu * RCC * year) / 100;
            calRCC = Math.Round(((sumInsu * RCC * year) / 100), 2, MidpointRounding.AwayFromZero);

            Cal_TRTemp = (sumInsu * TR * year) / 100;
            calTR = Math.Round(((sumInsu * TR * year) / 100), 2, MidpointRounding.AwayFromZero);

            // this rtnBPF_Value add to rtnPolicyFee becoz of BPF ayment requested in 26042023 changes

            Cal_POLICY_FEETemp = rtnPolicyFee + rtnBPF_Value;
            calPolicyFee = Math.Round((rtnPolicyFee + rtnBPF_Value), 2, MidpointRounding.AwayFromZero);

            Cal_ADMIN_FEETemp = ((CalNetPreTemp + Cal_RCCTemp + Cal_TRTemp) * rtnAdminFee) / 100;
            calAdminFee = Math.Round((((calNetPre + calRCC + calTR) * rtnAdminFee) / 100), 2, MidpointRounding.AwayFromZero);


            Cal_Renewal_FeeTemp = rtnRenewalFee * (year - 1);
            calRenewalFee = Math.Round((rtnRenewalFee * (year - 1)), 2, MidpointRounding.AwayFromZero);

            // vat calculation---->>

            double sumForTaxVat = 0;
            sumForTaxVat = CalNetPreTemp + Cal_RCCTemp + Cal_TRTemp + Cal_POLICY_FEETemp + Cal_ADMIN_FEETemp + Cal_Renewal_FeeTemp;

            using (OracleConnection conn = orcl_con.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GENPAY.CALCULATE_NBL_AND_VAT", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("taxLiableAmount", sumForTaxVat);
                    cmd.Parameters.AddWithValue("requestDate", DateTime.ParseExact(DateTime.Now.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture));
                    cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                    OracleDataReader dr = cmd.ExecuteReader();

                    Cal_NBTTemp = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());
                    Cal_VATTemp = double.Parse(cmd.Parameters["vatAmount"].Value.ToString());

                    dr.Close();

                }
                conn.Close();
            }


            calNBT = Math.Round((Cal_NBTTemp), 2, MidpointRounding.AwayFromZero);
            calVat = Math.Round((Cal_VATTemp), 2, MidpointRounding.AwayFromZero);

            Cal_TotalTemp = sumForTaxVat + Cal_NBTTemp + Cal_VATTemp;
            calTotal = Math.Round((Cal_TotalTemp), 2, MidpointRounding.AwayFromZero);




        }
        catch (Exception ex)
        {
            rtn = false;
        }

        return rtn;

    }

    public bool VATCalculationForFirstYear(double totalForVat, string CREATED_ON,out double calNBTOne, out double calVatOne, out double calTotalOutOne, out bool rtnLast)
    {

        rtnLast = false;
        double Cal_NBTOneYear, Cal_VATOneYear, Cal_TotalTemp = 0;
        calVatOne = 0; calTotalOutOne = 0; calNBTOne = 0;
        //DateTime CREATED_ON_temp;
        //CREATED_ON_temp = DateTime.ParseExact(CREATED_ON, "yyyy/MM/dd", CultureInfo.InvariantCulture);
       


        try
        {
            //DateTime dt2 = DateTime.ParseExact(CREATED_ON,
            //           "yyyy/MM/dd", CultureInfo.InvariantCulture);

            DateTime date = DateTime.ParseExact(CREATED_ON, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //DateTime date2 = DateTime.ParseExact(date.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture);
            int date2 = Convert.ToInt32(date.ToString("yyyyMMdd"));
            


            using (OracleConnection conn = orcl_con.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GENPAY.CALCULATE_NBL_AND_VAT_DATE", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("taxLiableAmount", totalForVat);
                    cmd.Parameters.AddWithValue("requestDate", date2);
                    cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                    OracleDataReader dr = cmd.ExecuteReader();

                    Cal_NBTOneYear = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());
                    Cal_VATOneYear = double.Parse(cmd.Parameters["vatAmount"].Value.ToString());

                    dr.Close();

                }
                conn.Close();
            }


            calNBTOne = Math.Round((Cal_NBTOneYear), 2, MidpointRounding.AwayFromZero);
            calVatOne = Math.Round((Cal_VATOneYear), 2, MidpointRounding.AwayFromZero);

            Cal_TotalTemp = totalForVat + Cal_NBTOneYear + Cal_VATOneYear;
            calTotalOutOne = Math.Round((Cal_TotalTemp), 2, MidpointRounding.AwayFromZero);

        }
        catch (Exception ex)
        {
            rtnLast = false;
        }
        return rtnLast;
    }
}