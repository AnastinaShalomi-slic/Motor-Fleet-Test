using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;

using Image = iTextSharp.text.Image;
using Font = iTextSharp.text.Font;
using Rectangle = iTextSharp.text.Rectangle;
using Color = iTextSharp.text.Color;
using System.Globalization;

public partial class Bank_Fire_PrintSchedule : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    Update_class exe_up = new Update_class();
    Ip_Class get_ip = new Ip_Class();
    EncryptDecrypt dc = new EncryptDecrypt();
    DeviceFinder findDev = new DeviceFinder();
    DeviceFinder df = new DeviceFinder();
    ORCL_Connection orcl_con = new ORCL_Connection();
    Insert_class insert_ = new Insert_class();
    GetProposalDetails getPropClass = new GetProposalDetails();
    Insert_class exe_in = new Insert_class();
    Update_class _update = new Update_class();
    PremiumCalculation premCal = new PremiumCalculation();
    string Auth = string.Empty;

    string agentCode, agentName, BGI, BANK_ACC, BANK_EMAIL = string.Empty;

    public string[] ReturnCovers = new string[13];
    public string[] roofValues = new string[4];

    string shortDes = string.Empty;
    string days = string.Empty;
    string hours = string.Empty;
    string minutes = string.Empty;
    string seconds = string.Empty;

    string day_msg = string.Empty;
    string hours_msg = string.Empty;
    string minutes_msg = string.Empty;
    public string fd_ref_no = string.Empty;
    public string flag = string.Empty;
    public string quo_no_temp = string.Empty;
    public string prinPolicyNumber = string.Empty;
    public string reDeduct1, reDeduct2, reDeduct3 = string.Empty;

    public string deductVal1, deductVal2 = string.Empty;

    string qrefNo, UsrId = "", reqTyp = "", mgs = "";
    string rID = "";
    string filePath = "", ext = "";
    bool cancelBtn = false;
    List<string> optionList;
    private string SessionUserId = "";
    private string SessionbrCode = "";
    int emailRtn = 0;
    string PreviousClaim = "", ccEmailIds = "";


    double sum_insuVal = 0;
    string DH_OPTION1 = "";
    string DH_OPTION2 = "";
    string DH_OPTION3 = "";
    string DH_OPTION4 = "";
    string DH_OPTION5 = "";
    string DH_OPTION6 = "";
    string DH_OPTION7 = "";

    string SOLAR_OPTION1 = "";
    string SOLAR_OPTION2 = "";
    string SOLAR_OPTION3 = "";
    string SOLAR_OPTION4 = "";
    string SOLAR_OPTION5 = "";
    string SOLAR_OPTION6 = "";
    string SOLAR_OPTION7 = "";
    string SOLAR_OPTION8 = "";

    string BANK = string.Empty;
    double SOLAR_ACCIDENTAL, SOLAR_ELECT = 0;

    // double sumInsu, BASIC, RCC, TR, ADMIN_FEE, POLICY_FEE = 0;


    string SC_POLICY_NO, SC_SUM_INSU, SC_NET_PRE,
             SC_RCC, SC_TR, SC_ADMIN_FEE,
             SC_POLICY_FEE, SC_NBT, SC_VAT, SC_TOTAL_PAY, CREATED_ON, CREATED_BY,
             FLAG, SC_Renewal_FEE, BPF_FEE, DEBIT_NO = string.Empty;
   


    string dh_bcode,dh_bbrcode,dh_name,dh_agecode,dh_agename,dh_nic,dh_br,
        dh_padd1,dh_padd2,dh_padd3,dh_padd4,dh_phone,dh_email,dh_iadd1,dh_iadd2,dh_iadd3,
        dh_iadd4,dh_pfrom,dh_pto,dh_uconstr,dh_occu_car,dh_occ_yes_reas,dh_haz_occu,dh_haz_yes_rea,
        dh_valu_build,dh_valu_wall,
        dh_valu_total,dh_aff_flood,dh_aff_yes_reas,dh_wbrick,
        dh_wcement,dh_dwooden,dh_dmetal,dh_ftile,dh_fcement,dh_rtile,
        dh_rasbes,dh_rgi,dh_rconcreat,dh_cov_fire,
        dh_cov_light,dh_cov_flood,dh_cfwateravl,dh_cfyesr1,dh_cfyesr2,dh_cfyesr3,dh_cfyesr4,
        dh_entered_by,dh_entered_on, dh_hold, DH_NO_OF_FLOORS, DH_OVER_VAL, DH_FINAL_FLAG, 
        dh_isreq,dh_conditions,dh_isreject,dh_iscodi,dh_bcode_id,dh_bbrcode_id, DH_LOADING, DH_LOADING_VAL,LAND_PHONE, DH_VAL_BANKFAC,dh_deductible, dh_deductible_pre,
        TERM,Period,Fire_cover,Other_cover,SRCC_cover,TC_cover,Flood_cover,
        BANK_UPDATED_BY, BANK_UPDATED_ON, PROP_TYPE, DH_SOLAR_SUM, SOLAR_REPAIRE,
        SOLAR_PARTS, SOLAR_ORIGIN, SOLAR_SERIAL, Solar_Period, LOAN_NUMBER = string.Empty;

    string slic = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {    
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);

        if (!Page.IsPostBack)
        {
            try
            {
                slic = Session["EPFNum"].ToString();

                string ref_noValidate = string.Empty;
                string final_flag = string.Empty;
                string debitNo = string.Empty;
                string datepayment = string.Empty;
                string BANGICODE = string.Empty;

                var en = new EncryptDecrypt();
                if (Request.QueryString["Ref_no"] != null)
                {
                    ref_noValidate=en.Decrypt(Request.QueryString["Ref_no"]).Trim().ToString();
                    final_flag = en.Decrypt(Request.QueryString["Flag"]).Trim().ToString();
                    debitNo = en.Decrypt(Request.QueryString["Data"]).Trim().ToString();
                    datepayment = en.Decrypt(Request.QueryString["PaymentDate"]).Trim().ToString();
                    BANGICODE = en.Decrypt(Request.QueryString["BANCGI"]).Trim().ToString();

                    string year = DateTime.Now.Year.ToString();
                    //txt_debit.InnerHtml = "D/" + year + "/" + BANGICODE + "/21/0000" + debitNo;

                    hfDebitNo.Value = debitNo;
                    hfPaymentDate.Value = datepayment;
                    hfBANGICODE.Value = BANGICODE;

                    if (ref_noValidate == "pending")
                    {
                        if (final_flag == "Pending")
                        {
                            Response.Redirect("~/session_error/sessionError.aspx?error=" + dc.Encrypt("URL".ToString()) + "&code=" + dc.Encrypt("5".ToString()), false);

                        }
                        else if (final_flag == "Rejected")
                        {
                            Response.Redirect("~/session_error/sessionError.aspx?error=" + dc.Encrypt("URL".ToString()) + "&code=" + dc.Encrypt("7".ToString()), false);

                        }
                        else { }
                    }
                    else
                    {
                        hfRefId.Value = en.Decrypt(Request.QueryString["Ref_no"]).Trim().ToString();
                        string ref22 = en.Decrypt(Request.QueryString["Ref_no"]).Trim().ToString();

                        if (string.IsNullOrEmpty(hfRefId.Value) || hfRefId.Value == "#") //|| string.IsNullOrEmpty(app) || app == "#" || string.IsNullOrEmpty(requ) || requ == "#"
                        {

                            // ShowPopupMessage("***************",PopupMessageType.Message);
                            var endc = new EncryptDecrypt();
                            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("URL".ToString()));
                        }
                        else
                        {
                            var endc = new EncryptDecrypt();

                            hfFlag.Value = en.Decrypt(Request.QueryString["Flag"]).ToString().Trim();
                            hfSumInsu.Value = en.Decrypt(Request.QueryString["sumInsu"]).ToString().Trim();
                            if (flag == "Y")
                            {

                            }

                            else
                            {

                            }
                            this.FillSchedule(); //----> fill shedule
                            if (Session["bank_code"].ToString() != "")
                            {
                                if (Session["bank_code"].ToString() == "7010" || Session["bank_code"].ToString() == "7135" || Session["bank_code"].ToString() == "7755" || Session["bank_code"].ToString() == "7719")
                                {

                                    btPayAdvance.Visible = true;

                                }
                                else { btPayAdvance.Visible = false; }

                                btPDF.Visible = true; btPropType.Visible = true;

                            }
                            else { btPDF.Visible = true; btPayAdvance.Visible = false; btPropType.Visible = false; }
                        }
                    }
                }
                //remove this when finish------------>
                else { hfSumInsu.Value = "0";
                    hfFlag.Value = "Y";
                    hfRefId.Value = "0";
                }

            }
            catch (Exception ex)
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()),false);
            }

        }
        // Check the user login session to control btdebit_Click button visibility
        if (!string.IsNullOrEmpty(slic))
        {
            user_name.Value = Session["UsrName"].ToString() + " ( " + Session["EPFNum"].ToString().Substring(1) + " )";
            //user_name.Text = "SEC****";
            btdebit.Visible = true;
        }
        else
        {
            btdebit.Visible = false; // Hide the button for others
        }

    }




    public void FillSchedule() {

        try
        {

            sum_insuVal = Convert.ToDouble(hfSumInsu.Value);
            this.getPropClass.GetSchedualCalValues(hfRefId.Value, out SC_POLICY_NO, out SC_SUM_INSU, out SC_NET_PRE,
                out SC_RCC, out SC_TR, out SC_ADMIN_FEE,
                out SC_POLICY_FEE, out SC_NBT, out SC_VAT, out SC_TOTAL_PAY, out CREATED_ON, out CREATED_BY,
                out FLAG,out SC_Renewal_FEE, out BPF_FEE, out DEBIT_NO);


            //------------------------->>-------------policy details-------------------->>>

            this.getPropClass.GetEnteredDetails(hfRefId.Value, out dh_bcode, out dh_bbrcode, out dh_name, out dh_agecode, out dh_agename, out dh_nic, out dh_br,
    out dh_padd1, out dh_padd2, out dh_padd3, out dh_padd4, out dh_phone, out dh_email, out dh_iadd1, out dh_iadd2, out dh_iadd3,
    out dh_iadd4, out dh_pfrom, out dh_pto, out dh_uconstr, out dh_occu_car, out dh_occ_yes_reas, out dh_haz_occu, out dh_haz_yes_rea,
    out dh_valu_build, out dh_valu_wall,
    out dh_valu_total, out dh_aff_flood, out dh_aff_yes_reas, out dh_wbrick,
    out dh_wcement, out dh_dwooden, out dh_dmetal, out dh_ftile, out dh_fcement, out dh_rtile,
    out dh_rasbes, out dh_rgi, out dh_rconcreat, out dh_cov_fire,
    out dh_cov_light, out dh_cov_flood, out dh_cfwateravl, out dh_cfyesr1, out dh_cfyesr2, out dh_cfyesr3, out dh_cfyesr4,
    out dh_entered_by, out dh_entered_on, out dh_hold,out DH_NO_OF_FLOORS, out DH_OVER_VAL, out DH_FINAL_FLAG,
    out dh_isreq, out dh_conditions, out dh_isreject, out dh_iscodi, out dh_bcode_id, out dh_bbrcode_id, out DH_LOADING, out DH_LOADING_VAL, out LAND_PHONE, out DH_VAL_BANKFAC, out dh_deductible, out dh_deductible_pre,
    out TERM, out Period, out Fire_cover, out Other_cover, out SRCC_cover, out TC_cover, out Flood_cover,
    out BANK_UPDATED_BY, out BANK_UPDATED_ON, out PROP_TYPE, out DH_SOLAR_SUM, out SOLAR_REPAIRE,
    out SOLAR_PARTS, out SOLAR_ORIGIN, out SOLAR_SERIAL, out Solar_Period, out LOAN_NUMBER);

            //---------12092021------------->>
            //calRCC = Math.Round(((sumInsu * RCC * year) / 100), 2, MidpointRounding.AwayFromZero);
            double year = 0;
            if (Convert.ToDouble(Period.Trim()) == 0) { year = 1; }
            else { year = Convert.ToDouble(Period.Trim()); }

            double netPerOneYear, adminFeeOneYear, policyFeeOneYear, nbtOneYear, vatOneYear, totalOneYear, renewalFeeOneYear, srccOneYear, trOneYear = 0;

            netPerOneYear = Math.Round(((Convert.ToDouble(SC_NET_PRE) / year)), 2, MidpointRounding.AwayFromZero);
            adminFeeOneYear = Math.Round(((Convert.ToDouble(SC_ADMIN_FEE) / year)), 2, MidpointRounding.AwayFromZero);
            policyFeeOneYear = Math.Round(((Convert.ToDouble(SC_POLICY_FEE))), 2, MidpointRounding.AwayFromZero);
            renewalFeeOneYear = Math.Round(((Convert.ToDouble(SC_Renewal_FEE) / year)), 2, MidpointRounding.AwayFromZero);
            srccOneYear = Math.Round(((Convert.ToDouble(SC_RCC) / year)), 2, MidpointRounding.AwayFromZero);
            trOneYear = Math.Round(((Convert.ToDouble(SC_TR) / year)), 2, MidpointRounding.AwayFromZero);

            //---12072022 vat for one year with ssc levy changes
            bool rtn = false;
            double totalForVat, calNBTOne, calVatOne, calTotalOutOne = 0;
            bool rtnLast = false;

            totalForVat = netPerOneYear + adminFeeOneYear + policyFeeOneYear + srccOneYear + trOneYear;
            //vat changes 19012024 
            rtn = premCal.VATCalculationForFirstYear(totalForVat, CREATED_ON, out calNBTOne, out calVatOne, out calTotalOutOne, out rtnLast);
            //---------
            //nbtOneYear = Math.Round(((Convert.ToDouble(SC_NBT) / year)), 2, MidpointRounding.AwayFromZero);
            //vatOneYear = Math.Round(((Convert.ToDouble(SC_VAT) / year)), 2, MidpointRounding.AwayFromZero);

            nbtOneYear = Math.Round(((Convert.ToDouble(calNBTOne))), 2, MidpointRounding.AwayFromZero);
            vatOneYear = Math.Round(((Convert.ToDouble(calVatOne))), 2, MidpointRounding.AwayFromZero);

            //totalOneYear = Math.Round(((Convert.ToDouble(SC_NET_PRE) / year) / 100), 2, MidpointRounding.AwayFromZero);

            policy_number.InnerHtml = SC_POLICY_NO;
            txt_debit.InnerHtml = DEBIT_NO;
            //NSB Loan Number changes-->>>
            if (dh_bcode_id.ToString().Trim() == "7719") { DivLoan.Visible = true; txtLoan.InnerHtml = LOAN_NUMBER.Trim(); }
            else { DivLoan.Visible = false; txtLoan.InnerHtml = LOAN_NUMBER.Trim(); }
            ///--------->>>>>>>>>>>>>>>>>>
            netPremium.InnerHtml = netPerOneYear.ToString("###,###,###0.00");

            //adminFee.InnerHtml = adminFeeOneYear.ToString("###,###,###0.00");

            /*social security contribution(SSC) add to admin fee from 01/07/2022
            now admin fee= admin fee + ssc
            */

            adminFee.InnerHtml = (adminFeeOneYear + nbtOneYear).ToString("###,###,###0.00");

            policyFee.InnerHtml = policyFeeOneYear.ToString("###,###,###0.00");
            nbtVal.InnerHtml = nbtOneYear.ToString("###,###,###0.00");
            vatVal.InnerHtml = vatOneYear.ToString("###,###,###0.00");


            totalOneYear = Math.Round(((netPerOneYear + adminFeeOneYear + policyFeeOneYear + nbtOneYear + vatOneYear + srccOneYear + trOneYear)), 2, MidpointRounding.AwayFromZero);
            totalPayble.InnerHtml = totalOneYear.ToString("###,###,###0.00");

            //remain balnace for long term
            double remainPremium = Convert.ToDouble(SC_TOTAL_PAY) - (totalOneYear);
            remainPremium = Math.Round((Convert.ToDouble(SC_TOTAL_PAY) - (totalOneYear)), 2, MidpointRounding.AwayFromZero);
            lblremainPremium.InnerText = remainPremium.ToString("###,###,###0.00");

            //----------
            //--NSB changes--
            this.GetAgentDetails(dh_agecode);
            bankEmail.InnerHtml = BANK_EMAIL;

            if (TERM.ToString() == "1")
            {
                DivRenewalFee.Visible = false; //renewal fee
                txt_renewal.InnerHtml = renewalFeeOneYear.ToString("###,###,###0.00"); 

                txtPolFee.InnerHtml = "Policy Fee";

                if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "N")
                {
                    Div27.Visible = true; //srcc row
                    srccVal.InnerHtml = srccOneYear.ToString("###,###,###0.00");
                    Div28.Visible = false; //tc row
                    trVal.InnerHtml = trOneYear.ToString("###,###,###0.00");
                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "Y")
                {
                    Div27.Visible = true; //srcc row
                    srccVal.InnerHtml = srccOneYear.ToString("###,###,###0.00");
                    Div28.Visible = true; //tc row
                    trVal.InnerHtml = trOneYear.ToString("###,###,###0.00");
                }
                else
                {
                    Div27.Visible = false; //srcc row
                    srccVal.InnerHtml = srccOneYear.ToString("###,###,###0.00");
                    Div28.Visible = false; //tc row
                    trVal.InnerHtml = trOneYear.ToString("###,###,###0.00");
                }
            }
            else
            {
                txtPolFee.InnerHtml = "Policy Fee for 1st year";
                DivRenewalFee.Visible = false; //renewal fee
                txt_renewal.InnerHtml = renewalFeeOneYear.ToString("###,###,###0.00");
                if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "N")
                {
                    Div27.Visible = true; //srcc row
                    srccVal.InnerHtml = srccOneYear.ToString("###,###,###0.00");
                    Div28.Visible = false; //tc row
                    trVal.InnerHtml = trOneYear.ToString("###,###,###0.00");
                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "Y")
                {
                    Div27.Visible = true; //srcc row
                    srccVal.InnerHtml = srccOneYear.ToString("###,###,###0.00");
                    Div28.Visible = true; //tc row
                    trVal.InnerHtml = trOneYear.ToString("###,###,###0.00");
                }
                else
                {
                    Div27.Visible = false; //srcc row
                    srccVal.InnerHtml = srccOneYear.ToString("###,###,###0.00");
                    Div28.Visible = false; //tc row
                    trVal.InnerHtml = trOneYear.ToString("###,###,###0.00");
                }

            }

            //------------------------->>>

            nameOfInsu.InnerHtml = dh_name;

            if (dh_padd3 != "" && dh_padd4 == "") {
                address.InnerHtml = dh_padd1 + ", " + dh_padd2 + "," + dh_padd3;
            }
            else if (dh_padd3 != "" && dh_padd4 != "") { address.InnerHtml = dh_padd1 + ", " + dh_padd2 + ", " + dh_padd3+", "+ dh_padd4; }
            else if (dh_padd3 == "" && dh_padd4 != "") { address.InnerHtml = dh_padd1 + ", " + dh_padd2 + ", "  + dh_padd4; }
            else
            {
                address.InnerHtml = dh_padd1 + ", " + dh_padd2;
            }

            fInterst.InnerHtml = dh_bcode + "-" + dh_bbrcode;
            fInterest2.InnerHtml = dh_bcode + "-" + dh_bbrcode;
            if (!string.IsNullOrEmpty(dh_conditions))
            {
                if (PROP_TYPE == "1" || PROP_TYPE == "3")
                {
                    trcon1.Visible = true; trcon2.Visible = true;
                    condiReasons.InnerHtml = "* " + dh_conditions;
                    
                }

                else { trcon1.Visible = true; trcon2.Visible = true; }
            }

            else
            {
                trcon1.Visible = true; trcon2.Visible = true;
            }

            //changes 10032022--->>
            if (dh_aff_flood =="0" && Flood_cover =="Y" && DH_FINAL_FLAG=="N")
            {
                if (PROP_TYPE == "1" || PROP_TYPE == "3")
                {
                    Div74.Visible = true;
                    lblQ13.InnerHtml = "* This policy is issued subject to there were no reported flood losses for the last 5 years with effect from the commencement date of this policy, and SLIC reserves all rights to repudiate flood claims if there is a discrepancy in declared flood claim history in the online proposal form.";

                }

                else { Div74.Visible = false; }
            }

            else
            {
                Div74.Visible = false;
                lblQ13.InnerHtml = "";
            }
            //----end---->>>>
            CondiAll.InnerHtml = "* This policy is issued subject to that all rights reserved with SLIC to decide the continuity of insurance cover or review premium terms and conditions applicable after scrutinizing the claims experience and any other adverse features in respect of each peril insured herein under.";

            ///-----deafult warrenty conditions------
            ///
            defaultCondition.InnerHtml = "*<u> Premium Payment Warranty 60 days</u>";

            

            DateTime date = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(1);
            string toDateNext = date.ToString("dd/MM/yyyy");
            if (TERM.ToString() == "1")
            { period.InnerHtml = "From : " + dh_pfrom + "&nbsp;&nbsp;&nbsp;" + "  To : " + toDateNext; }
            else
            { period.InnerHtml = "From : " + dh_pfrom + "&nbsp;&nbsp;&nbsp;" + "  To : " + toDateNext+ " (First Year)"; }
            
            sumInsu.InnerHtml = "Rs. "+dh_valu_total;
            agentNameCode.InnerHtml = dh_agecode+ "&nbsp;-&nbsp;" + dh_agename;
            branchNameCode.InnerHtml = dh_bbrcode_id + "-" + dh_bbrcode;

            if (dh_uconstr == "1") {
                constructDetails.InnerHtml= "Under Construction";
                if (PROP_TYPE == "3")
                {
                    //claus1.InnerHtml = "* Reinstatement Value Clause";
                    claus1.InnerHtml = "";
                }
            }
            else if (dh_uconstr == "0")
            {
                if (PROP_TYPE == "1" || PROP_TYPE == "2" )//|| PROP_TYPE == "3"
                {
                    constructDetails.InnerHtml = "Completed"; claus1.InnerHtml = "* Reinstatement Value Clause";
                }

                else if(PROP_TYPE == "3")
                {
                    constructDetails.InnerHtml = "Completed";
                    claus1.InnerHtml = "";
                }
                
            }
            else { constructDetails.InnerHtml = "--"; }

            if(dh_wbrick=="1" && dh_wcement == "1") { exWall.InnerHtml = "Brick and Cement"; }
               else if (dh_wcement == "1") { exWall.InnerHtml = "Cement"; }
            else if (dh_wbrick == "1") { exWall.InnerHtml = "Brick"; }
            else { exWall.InnerHtml = ""; }

            numOfFloors.InnerHtml = DH_NO_OF_FLOORS;

            if (dh_rtile == "1" && dh_rasbes == "1" && dh_rgi == "1" && dh_rconcreat == "1")
            {
                roofDetails.InnerHtml = "Tile, Asbestos, GI Sheets and Concrete";
            }

            else if (dh_rtile == "1" && dh_rasbes == "1" && dh_rgi == "1")
            {
                roofDetails.InnerHtml = "Tile, Asbestos and GI Sheets";
            }


            else if (dh_rtile == "1" && dh_rconcreat == "1" && dh_rgi == "1")
            {
                roofDetails.InnerHtml = "Tile, GI Sheets and Concrete";
            }
            else if (dh_rtile == "1" && dh_rasbes == "1")
            {
                roofDetails.InnerHtml = "Tile and Asbestos";
            }
            else if (dh_rtile == "1" && dh_rgi == "1")
            {
                roofDetails.InnerHtml = "Tile and GI Sheets";
            }
            else if (dh_rtile == "1" && dh_rconcreat == "1")
            {
                roofDetails.InnerHtml = "Tile and Concrete";
            }
            else if (dh_rtile == "1") { roofDetails.InnerHtml = "Tile"; }



            //------->
            else if (dh_rasbes == "1" && dh_rgi == "1" && dh_rconcreat == "1")
            {
                roofDetails.InnerHtml = "Asbestos, GI Sheets and Concrete";
            }

            else if (dh_rasbes == "1" && dh_rgi == "1")
            {
                roofDetails.InnerHtml = "Asbestos and GI Sheets";
            }

            else if (dh_rasbes == "1")
            {
                roofDetails.InnerHtml = "Asbestos";
            }
            //---->
            else if (dh_rgi == "1" && dh_rconcreat == "1")
            {
                roofDetails.InnerHtml = "GI Sheets and Concrete";
            }

            else if (dh_rgi == "1")
            {
                roofDetails.InnerHtml = "GI Sheets";
            }

            else if (dh_rconcreat == "1")
            {
                roofDetails.InnerHtml = "Concrete";
            }
            else { }


            if (dh_iadd3!= "" && dh_iadd4 == "")
            {
                situated.InnerHtml = dh_iadd1 + ", " + dh_iadd2 + ", " + dh_iadd3;
            }
            else if (dh_iadd3 != "" &&  dh_iadd4 != "") { situated.InnerHtml = dh_iadd1 + ", " + dh_iadd2 + ", " + dh_iadd3 + ", " + dh_iadd4; }
            else if (dh_iadd3 == "" && dh_iadd4 != "") { situated.InnerHtml = dh_iadd1 + ", " + dh_iadd2 + ", " + dh_iadd4; }
            else
            {
                situated.InnerHtml = dh_iadd1 + ", " + dh_iadd2;
            }

            sumInWord1.InnerHtml = dh_valu_build;
            if (dh_valu_wall=="0.00") { Div20.Visible = false; sumInWord2.InnerHtml = dh_valu_wall; }
            else { Div20.Visible = true; sumInWord2.InnerHtml = dh_valu_wall; }

            
            SumInsuDisplay.InnerHtml = dh_valu_total;
            bool sucsess=this.isCheckNumber(dh_iadd1);
            if (sucsess) { waranty1.InnerHtml = ""; }
            else { waranty1.InnerHtml = "<u><b>Identification Warranty</b></u></br>" +
                    "The said property is the only property so situated and answering to the foregoing description in which the insured " +
                    "has interests";
                 }

           

            //////solar conditons/warrenty-------------------->>>>>>>
            ///
            string PeriodTemp = (Convert.ToInt32(Period) - 1).ToString();

            ///--- -checked_ shedual

            //conditions for policy types-----
            //1 ==fire and light
            //2 ==Fire and Lightning + Solar Panel
            //3 ==solar only
            if (PROP_TYPE == "1")
            {
                fLbl.InnerHtml = "* On the building of private dwelling house including permanent fixtures and fittings";
                //lblNote.InnerHtml = "Note - We advise you to insure your building / solar panel for the new replacement value (Reinstatement basis) in order to avoid reduction for under insurance at the time of a claim.";
                Div46.Visible = true;
                //changes -- 04012022--->> 
                if(Convert.ToDouble(DH_VAL_BANKFAC.Trim()) == 0)
                {
                    FValuelbl.InnerHtml = dh_valu_total;
                }
                else { FValuelbl.InnerHtml = DH_VAL_BANKFAC; }
                //----->>
                Div68.Visible = false; //solar excess 
                //covers logics re-arrange 13092021----------------------------->>
                ReturnCovers = getPropClass.getFireCoverArray();
                this.getPropClass.GetDeductibles(sum_insuVal, out DH_OPTION1, out DH_OPTION2, out DH_OPTION3, out DH_OPTION4, out DH_OPTION5, out DH_OPTION6, out DH_OPTION7);

                if (Fire_cover == "Y" && Other_cover == "N" && SRCC_cover == "N" && TC_cover == "N") //fire and lighting
                {
                    //-dedcutible changes-----13092021----------->>>>>>>>>>>>

                    if (string.IsNullOrEmpty(DH_OPTION1)) { deduct1.InnerText = ""; Div35.Visible = false; }

                    else { deduct1.InnerText = "* " + DH_OPTION1; Div35.Visible = false; }

                    if (string.IsNullOrEmpty(DH_OPTION2)) { deduct2.InnerText = ""; Div36.Visible = false; }

                    else { deduct2.InnerText = "* " + DH_OPTION2; Div36.Visible = false; }

                    if (string.IsNullOrEmpty(DH_OPTION3)) { deduct3.InnerText = ""; Div37.Visible = false; }

                    else { deduct3.InnerText = "* " + DH_OPTION3; Div37.Visible = false; }


                    if (string.IsNullOrEmpty(DH_OPTION4)) { deduct4.InnerText = ""; Div38.Visible = false; }

                    else
                    {   //if flood not request
                        Div38.Visible = false;
                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            deduct4.InnerText = "* " + DH_OPTION4;
                            Div38.Visible = false;
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                deduct4.InnerText = "* " + DH_OPTION4;
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                reDeduct1 = DH_OPTION4.Substring(0, 8);
                                reDeduct2 = DH_OPTION4.Substring(10, 24);
                                reDeduct3 = DH_OPTION4.Substring(42);
                                deductVal1 = dh_deductible_pre;//dh_deductible.Substring(0, 2);
                                deductVal2 = dh_deductible;//dh_deductible.Substring(6);
                                deduct4.InnerText = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + " " + deductVal2 + " " + reDeduct3;
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(DH_OPTION5)) { deduct5.InnerText = ""; Div59.Visible = false; }

                    else { deduct5.InnerText = "* " + DH_OPTION5; Div59.Visible = false; }

                    if (string.IsNullOrEmpty(DH_OPTION6)) { deduct6.InnerText = ""; Div60.Visible = false; }

                    else { deduct6.InnerText = "* " + DH_OPTION6; Div60.Visible = false; }

                    if (string.IsNullOrEmpty(DH_OPTION7)) { deduct7.InnerText = ""; Div61.Visible = false; }

                    else
                    {
                        string  FireOnlReDeduct1 = DH_OPTION7.Substring(0, 4);
                        string FireOnlReDeduct2 = DH_OPTION7.Substring(9);
                        string FireOnlReDeduct3 = FireOnlReDeduct1+ FireOnlReDeduct2;
                        deduct7.InnerText = "* " + FireOnlReDeduct3; Div61.Visible = true;
                    }


                    ///------------------------------>>>>-------------------------------------------






                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        Label lbl = new Label();
                        lbl.Text = "* " + ReturnCovers[i];


                        if (i + 1 == 1)
                        {

                            dv1.Controls.Add(lbl);
                            dv1.Controls.Add(new LiteralControl("<br/>"));


                        }
                        else
                        {
                            dv1.Controls.Remove(lbl);
                            dv1.Controls.Remove(new LiteralControl("<br/>"));

                        }
                    }

                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "N" && TC_cover == "N") //fire + other
                {

                    //-dedcutible changes-----30112021----------->>>>>>>>>>>>


                    if (string.IsNullOrEmpty(DH_OPTION1)) { deduct1.InnerText = ""; Div35.Visible = false; }

                    else { deduct1.InnerText = "* " + DH_OPTION1; Div35.Visible = false; }

                    if (string.IsNullOrEmpty(DH_OPTION2)) { deduct2.InnerText = ""; Div36.Visible = false; }

                    else { deduct2.InnerText = "* " + DH_OPTION2; Div36.Visible = false; }

                    if (string.IsNullOrEmpty(DH_OPTION3)) { deduct3.InnerText = ""; Div37.Visible = false; }

                    else { deduct3.InnerText = "* " + DH_OPTION3; Div37.Visible = true; }


                    if (string.IsNullOrEmpty(DH_OPTION4)) { deduct4.InnerText = ""; Div38.Visible = false; }

                    else
                    {   //if flood not request
                        //Div38.Visible = false;
                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            deduct4.InnerText = "* " + DH_OPTION4;
                            Div38.Visible = false;
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                deduct4.InnerText = "* " + DH_OPTION4;
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                reDeduct1 = DH_OPTION4.Substring(0, 8);
                                reDeduct2 = DH_OPTION4.Substring(10, 24);
                                reDeduct3 = DH_OPTION4.Substring(42);
                                deductVal1 = dh_deductible_pre;//dh_deductible.Substring(0, 2);
                                deductVal2 = dh_deductible;//dh_deductible.Substring(6);
                                deduct4.InnerText = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + " " + deductVal2 + " " + reDeduct3;
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(DH_OPTION5)) { deduct5.InnerText = ""; Div59.Visible = false; }

                    else { deduct5.InnerText = "* " + DH_OPTION5; Div59.Visible = true; }

                    if (string.IsNullOrEmpty(DH_OPTION6)) { deduct6.InnerText = ""; Div60.Visible = false; }

                    else { deduct6.InnerText = "* " + DH_OPTION6; Div60.Visible = true; }

                    if (string.IsNullOrEmpty(DH_OPTION7)) { deduct7.InnerText = ""; Div61.Visible = false; }

                    else { deduct7.InnerText = "* " + DH_OPTION7; Div61.Visible = true; }


                    ///------------------------------>>>>-------------------------------------------




                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        Label lbl = new Label();
                        lbl.Text = "* " + ReturnCovers[i];


                        if (i + 1 == 2 || i + 1 == 3)//i + 1 == 2 ||
                        {

                            dv1.Controls.Remove(lbl);
                            dv1.Controls.Remove(new LiteralControl("<br/>"));

                        }
                        else
                        {
                            if (dh_cov_flood == "0")
                            {
                                if (i + 1 == 7)
                                {
                                    dv1.Controls.Remove(lbl);
                                    dv1.Controls.Remove(new LiteralControl("<br/>"));


                                }
                                else
                                {
                                    dv1.Controls.Add(lbl);
                                    dv1.Controls.Add(new LiteralControl("<br/>"));
                                }
                            }
                            else
                            {
                                dv1.Controls.Add(lbl);
                                dv1.Controls.Add(new LiteralControl("<br/>"));
                            }

                        }
                    }


                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "N") // fire + other + srcc
                {


                    //-dedcutible changes-----30112021----------->>>>>>>>>>>>


                    if (string.IsNullOrEmpty(DH_OPTION1)) { deduct1.InnerText = ""; Div35.Visible = false; }

                    else { deduct1.InnerText = "* " + DH_OPTION1; Div35.Visible = true; }

                    if (string.IsNullOrEmpty(DH_OPTION2)) { deduct2.InnerText = ""; Div36.Visible = false; }

                    else { deduct2.InnerText = "* " + DH_OPTION2; Div36.Visible = false; }

                    if (string.IsNullOrEmpty(DH_OPTION3)) { deduct3.InnerText = ""; Div37.Visible = false; }

                    else { deduct3.InnerText = "* " + DH_OPTION3; Div37.Visible = true; }


                    if (string.IsNullOrEmpty(DH_OPTION4)) { deduct4.InnerText = ""; Div38.Visible = false; }

                    else
                    {   //if flood not request
                        //Div38.Visible = false;
                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            deduct4.InnerText = "* " + DH_OPTION4;
                            Div38.Visible = false;
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                deduct4.InnerText = "* " + DH_OPTION4;
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                reDeduct1 = DH_OPTION4.Substring(0, 8);
                                reDeduct2 = DH_OPTION4.Substring(10, 24);
                                reDeduct3 = DH_OPTION4.Substring(42);
                                deductVal1 = dh_deductible_pre;//dh_deductible.Substring(0, 2);
                                deductVal2 = dh_deductible;//dh_deductible.Substring(6);
                                deduct4.InnerText = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + " " + deductVal2 + " " + reDeduct3;
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(DH_OPTION5)) { deduct5.InnerText = ""; Div59.Visible = false; }

                    else { deduct5.InnerText = "* " + DH_OPTION5; Div59.Visible = true; }

                    if (string.IsNullOrEmpty(DH_OPTION6)) { deduct6.InnerText = ""; Div60.Visible = false; }

                    else { deduct6.InnerText = "* " + DH_OPTION6; Div60.Visible = true; }

                    if (string.IsNullOrEmpty(DH_OPTION7)) { deduct7.InnerText = ""; Div61.Visible = false; }

                    else { deduct7.InnerText = "* " + DH_OPTION7; Div61.Visible = true; }


                    ///------------------------------>>>>-------------------------------------------


                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        Label lbl = new Label();
                        lbl.Text = "* " + ReturnCovers[i];


                        if (i + 1 == 3)
                        {

                            dv1.Controls.Remove(lbl);
                            dv1.Controls.Remove(new LiteralControl("<br/>"));

                        }
                        else
                        {
                            if (dh_cov_flood == "0")
                            {
                                if (i + 1 == 7)
                                {
                                    dv1.Controls.Remove(lbl);
                                    dv1.Controls.Remove(new LiteralControl("<br/>"));


                                }
                                else
                                {
                                    dv1.Controls.Add(lbl);
                                    dv1.Controls.Add(new LiteralControl("<br/>"));
                                }
                            }
                            else
                            {
                                dv1.Controls.Add(lbl);
                                dv1.Controls.Add(new LiteralControl("<br/>"));
                            }

                        }
                    }


                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "Y") // fire + other + srcc +tr
                {


                    //-dedcutible changes-----30112021----------->>>>>>>>>>>>


                    if (string.IsNullOrEmpty(DH_OPTION1)) { deduct1.InnerText = ""; Div35.Visible = false; }

                    else { deduct1.InnerText = "* " + DH_OPTION1; Div35.Visible = true; }

                    if (string.IsNullOrEmpty(DH_OPTION2)) { deduct2.InnerText = ""; Div36.Visible = false; }

                    else { deduct2.InnerText = "* " + DH_OPTION2; Div36.Visible = true; }

                    if (string.IsNullOrEmpty(DH_OPTION3)) { deduct3.InnerText = ""; Div37.Visible = false; }

                    else { deduct3.InnerText = "* " + DH_OPTION3; Div37.Visible = true; }


                    if (string.IsNullOrEmpty(DH_OPTION4)) { deduct4.InnerText = ""; Div38.Visible = false; }

                    else
                    {   //if flood not request
                        //Div38.Visible = false;
                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            deduct4.InnerText = "* " + DH_OPTION4;
                            Div38.Visible = false;
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                deduct4.InnerText = "* " + DH_OPTION4;
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                reDeduct1 = DH_OPTION4.Substring(0, 8);
                                reDeduct2 = DH_OPTION4.Substring(10, 24);
                                reDeduct3 = DH_OPTION4.Substring(42);
                                deductVal1 = dh_deductible_pre;//dh_deductible.Substring(0, 2);
                                deductVal2 = dh_deductible;//dh_deductible.Substring(6);
                                deduct4.InnerText = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + " " + deductVal2 + " " + reDeduct3;
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(DH_OPTION5)) { deduct5.InnerText = ""; Div59.Visible = false; }

                    else { deduct5.InnerText = "* " + DH_OPTION5; Div59.Visible = true; }

                    if (string.IsNullOrEmpty(DH_OPTION6)) { deduct6.InnerText = ""; Div60.Visible = false; }

                    else { deduct6.InnerText = "* " + DH_OPTION6; Div60.Visible = true; }

                    if (string.IsNullOrEmpty(DH_OPTION7)) { deduct7.InnerText = ""; Div61.Visible = false; }

                    else { deduct7.InnerText = "* " + DH_OPTION7; Div61.Visible = true; }


                    ///------------------------------>>>>-------------------------------------------



                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        Label lbl = new Label();
                        lbl.Text = "* " + ReturnCovers[i];

                        if (dh_cov_flood == "0")
                        {
                            if (i + 1 == 7)
                            {
                                dv1.Controls.Remove(lbl);
                                dv1.Controls.Remove(new LiteralControl("<br/>"));


                            }
                            else
                            {
                                dv1.Controls.Add(lbl);
                                dv1.Controls.Add(new LiteralControl("<br/>"));
                            }
                        }
                        else
                        {
                            dv1.Controls.Add(lbl);
                            dv1.Controls.Add(new LiteralControl("<br/>"));
                        }

                    }
                }

                else
                {
                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        Label lbl = new Label();
                        lbl.Text = "* " + ReturnCovers[i];
                        dv1.Controls.Remove(lbl);
                        dv1.Controls.Remove(new LiteralControl("<br/>"));
                    }

                }

                Div65.Attributes.Add("style", "display:none"); // extentions title
                Div63.Attributes.Add("style", "display:none"); //1
                Div66.Attributes.Add("style", "display:none"); //2

                Div19.Attributes.Add("style", "display:normal");
                Div20.Attributes.Add("style", "display:normal");
                Div51.Attributes.Add("style", "display:none");
                lblSolarSum.InnerHtml = DH_SOLAR_SUM;

                
                double sumTotal = Convert.ToDouble(dh_valu_total);
                if (sumTotal > 20000000)
                {
                    Div52.Attributes.Add("style", "display:normal");
                    waranty2.InnerHtml = "* <u>Building Maintenance Warranty</u><br/>" +
                                        "It is warranted that preventive / periodical for all the Building, Roof, Gutters & Drain Lines should be done on regular basis.";
                }
                else
                {
                    Div52.Attributes.Add("style", "display:none");
                    waranty2.InnerHtml = "* <u>Building Maintenance Warranty</u><br/>" +
                                        "It is warranted that preventive / periodical for all the Building, Roof, Gutters & Drain Lines should be done on regular basis.";
                }

                    Div53.Attributes.Add("style", "display:none");
                //waranty3
                    Div69.Attributes.Add("style", "display:none");
                //waranty4
                    Div70.Attributes.Add("style", "display:none");
                //waranty5


                //Conditions table for solar

                Div54.Attributes.Add("style", "display:none");
                Div55.Attributes.Add("style", "display:none");
                Div56.Attributes.Add("style", "display:none");
                Div57.Attributes.Add("style", "display:none");
                //long term wording
                if (TERM.ToString() == "1")
                {
                    Div58.Attributes.Add("style", "display:none");
                    Div62.Attributes.Add("style", "display:none");
                }
                else
                {
                    Div58.Attributes.Add("style", "display:normal");
                    Div62.Attributes.Add("style", "display:normal");

                    DateTime renewFromDate = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(1);
                    string renwalFrom = date.ToString("dd/MM/yyyy");

                    //DateTime renewEndDate = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(Convert.ToInt32(PeriodTemp)); changed15122022 to below
					DateTime renewEndDate = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(Convert.ToInt32(PeriodTemp)+1);
                    string renwalEnd = renewEndDate.ToString("dd/MM/yyyy");
                    
                    longFrom.InnerHtml = renwalFrom + " to " + renwalEnd; 
                    longYear.InnerHtml = PeriodTemp;
                    //lblremainPremium
                   
                }


            }
            else if (PROP_TYPE == "2")
            {
                Div19.Attributes.Add("style", "display:normal");
                Div20.Attributes.Add("style", "display:normal");
                Div51.Attributes.Add("style", "display:normal");
                lblSolarSum.InnerHtml = DH_SOLAR_SUM;

                Div53.Attributes.Add("style", "display:normal");
                waranty3.InnerHtml = "* <b><u>Building Maintenance Warranty</u></b><br/>" +
                                    "It is warranted that preventive / periodical for all the Building, Roof, Gutters & Drain Lines should be done on regular basis.";

                //Conditions table for solar

                Div54.Attributes.Add("style", "display:normal");

                Div55.Attributes.Add("style", "display:normal");

                lblconditionSolar1.InnerHtml = "1. Theft Cover <br/>" +
                        "2. Mysterious & unexplainable losses <br/>" +
                        "3. Any loss of or damage caused to pipes & tubes of the solar panel(If a water heater is interconnected) <br/>" +
                        "4. Manufacturing defects/wear & tear/gradual Erosion & Corrosion";

                lblconditionSolar2.InnerHtml = "1. Submission of complete inventory on Solar Power panel with their make, model, serialnos, year of manufacture etc.<br/>" +
                                "2. Availability of Repairer, Sole agent & Spare parts in Sri Lanka in the event of a damage.<br/>" +
                                "3.	Safety measure should be implemented when shifting the Item.<br/>" +
                                "4.	Claim settlement will be done on indemnity value basis.<br/>" +
                                "5. Cover does not operate during the period of Installation / testing & commissioning.";


                Div56.Attributes.Add("style", "display:normal");
                Div57.Attributes.Add("style", "display:normal");
                //long term wording
                if (TERM.ToString() == "1")
                {
                    Div58.Attributes.Add("style", "display:normal");
                 
                    Div52.Attributes.Add("style", "display:none");//(long only) 
                    waranty2.InnerHtml = "* <b><u>Lightning Arrestors Warranty</u></b><br/>" +
                                        "It is warranted that suitable lightning arrestor should be installed to premises";
                }
                else
                {
                    Div58.Attributes.Add("style", "display:normal");
                    //longFrom.InnerHtml = Convert.ToDateTime(dh_pfrom).ToString();
                    DateTime dateSecond = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    string toDateSecond = dateSecond.ToString("dd-MMMM-yyyy");

                    longFrom.InnerHtml = toDateSecond;
                    longYear.InnerHtml = PeriodTemp;
                    Div52.Attributes.Add("style", "display:normal");//(long only) 
                    waranty2.InnerHtml = "* <b><u>Lightning Arrestors Warranty</u></b><br/>" +
                                        "It is warranted that suitable lightning arrestor should be installed to premises";
                }
            }
            else if (PROP_TYPE == "3")
            {
                //lblNote.InnerHtml = "";
                Div46.Visible = false;
                fLbl.InnerHtml = "* On Solar power system & Standard Accessories";
                //changes -- 04012022--->> 
                if (Convert.ToDouble(DH_VAL_BANKFAC.Trim()) == 0)
                {
                    FValuelbl.InnerHtml = dh_valu_total;
                }
                else { FValuelbl.InnerHtml = DH_VAL_BANKFAC; }
                //----->>
                //FValuelbl.InnerHtml = dh_valu_total;
                double sumInsuSolar = Convert.ToDouble(dh_valu_total);

                bool SolarRtn1, SolarRtn2 = false;
                getPropClass.GetElectricalClauseForSolar(dh_bcode_id, sumInsuSolar, sumInsuSolar, out SOLAR_ELECT, out SolarRtn1);
                getPropClass.GetAccidentalClauseForSolar(dh_bcode_id, sumInsuSolar, sumInsuSolar, out SOLAR_ACCIDENTAL, out SolarRtn2);

                Div65.Attributes.Add("style", "display:normal"); // extentions title
                Div63.Attributes.Add("style", "display:normal"); //1
                Div66.Attributes.Add("style", "display:normal"); //2

                string solarElect = SOLAR_ELECT.ToString("###,###,###0");
                string solarAcc = SOLAR_ACCIDENTAL.ToString("###,###,###0");

                solarExt1.InnerHtml = "1. Electrical inclusion clause-Without Burn Mark(in the event of any loss of or damage) caused to insured items over and above 03 years of age from the date of manufacture and the claim settlement will be done according to the terms and condition of stranded fire insurance policy of SLIC which covered only named perils and based on indemnity basis)<br/>" +
                    "* Maximum limit up to Rs. " + solarElect + "/-<br/>"
                + "(as per the endorsement attaching herewith)<br/>";

                solarExt2.InnerHtml = "2. Accidental Damage (any sudden & unforeseen physical loss or damage directly caused to the insured property up to a limit to Rs. "+ solarAcc + "/- )";

                //covers logics re-arrange 13092021----------------------------->>
                ReturnCovers = getPropClass.getFireCoverArray();
                this.getPropClass.GetDeductiblesForSolar(sum_insuVal, out SOLAR_OPTION1, out SOLAR_OPTION2, out SOLAR_OPTION3, out SOLAR_OPTION4, out SOLAR_OPTION5, out SOLAR_OPTION6, out SOLAR_OPTION7, out SOLAR_OPTION8);

                if (Fire_cover == "Y" && Other_cover == "N" && SRCC_cover == "N" && TC_cover == "N") //fire and lighting
                {
                    //-dedcutible changes-----13092021----------->>>>>>>>>>>>

                    if (string.IsNullOrEmpty(SOLAR_OPTION1)) { deduct1.InnerText = ""; Div35.Visible = false; }

                    else { deduct1.InnerText = "* " + SOLAR_OPTION1; Div35.Visible = false; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION2)) { deduct2.InnerText = ""; Div36.Visible = false; }

                    else { deduct2.InnerText = "* " + SOLAR_OPTION2; Div36.Visible = false; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION3)) { deduct3.InnerText = ""; Div37.Visible = false; }

                    else { deduct3.InnerText = "* " + SOLAR_OPTION3; Div37.Visible = false; }


                    if (string.IsNullOrEmpty(SOLAR_OPTION4)) { deduct4.InnerText = ""; Div38.Visible = false; }

                    else
                    {   //if flood not request
                        Div38.Visible = false;
                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            deduct4.InnerText = "* " + SOLAR_OPTION4;
                            Div38.Visible = false;
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                deduct4.InnerText = "* " + SOLAR_OPTION4;
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                reDeduct1 = SOLAR_OPTION4.Substring(0, 8);
                                reDeduct2 = SOLAR_OPTION4.Substring(10, 24);
                                reDeduct3 = SOLAR_OPTION4.Substring(42);
                                deductVal1 = dh_deductible_pre;//dh_deductible.Substring(0, 2);
                                deductVal2 = dh_deductible;//dh_deductible.Substring(6);
                                deduct4.InnerText = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + " " + deductVal2 + " " + reDeduct3;
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION5)) { deduct5.InnerText = ""; Div59.Visible = false; }

                    else { deduct5.InnerText = "* " + SOLAR_OPTION5; Div59.Visible = false; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION6)) { deduct6.InnerText = ""; Div60.Visible = false; }

                    else { deduct6.InnerText = "* " + SOLAR_OPTION6; Div60.Visible = false; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION7)) { deduct7.InnerText = ""; Div61.Visible = false; }

                    else { deduct7.InnerText = "* " + SOLAR_OPTION7; Div61.Visible = false; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION8)) { deduct8.InnerText = ""; Div68.Visible = false; }

                    else { deduct8.InnerText = "* " + SOLAR_OPTION8; Div68.Visible = true; }

                    ///------------------------------>>>>-------------------------------------------






                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        Label lbl = new Label();
                        lbl.Text = "* " + ReturnCovers[i];


                        if (i + 1 == 1)
                        {

                            dv1.Controls.Add(lbl);
                            dv1.Controls.Add(new LiteralControl("<br/>"));


                        }
                        else
                        {
                            dv1.Controls.Remove(lbl);
                            dv1.Controls.Remove(new LiteralControl("<br/>"));

                        }
                    }

                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "N" && TC_cover == "N") //fire + other
                {

                    //-dedcutible changes-----30112021----------->>>>>>>>>>>>


                    if (string.IsNullOrEmpty(SOLAR_OPTION1)) { deduct1.InnerText = ""; Div35.Visible = false; }

                    else { deduct1.InnerText = "* " + SOLAR_OPTION1; Div35.Visible = false; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION2)) { deduct2.InnerText = ""; Div36.Visible = false; }

                    else { deduct2.InnerText = "* " + SOLAR_OPTION2; Div36.Visible = false; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION3)) { deduct3.InnerText = ""; Div37.Visible = false; }

                    else { deduct3.InnerText = "* " + SOLAR_OPTION3; Div37.Visible = true; }


                    if (string.IsNullOrEmpty(SOLAR_OPTION4)) { deduct4.InnerText = ""; Div38.Visible = false; }

                    else
                    {   //if flood not request
                        //Div38.Visible = false;
                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            deduct4.InnerText = "* " + SOLAR_OPTION4;
                            Div38.Visible = false;
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                deduct4.InnerText = "* " + SOLAR_OPTION4;
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                reDeduct1 = SOLAR_OPTION4.Substring(0, 8);
                                reDeduct2 = SOLAR_OPTION4.Substring(10, 24);
                                reDeduct3 = SOLAR_OPTION4.Substring(42);
                                deductVal1 = dh_deductible_pre;//dh_deductible.Substring(0, 2);
                                deductVal2 = dh_deductible;//dh_deductible.Substring(6);
                                deduct4.InnerText = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + " " + deductVal2 + " " + reDeduct3;
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION5)) { deduct5.InnerText = ""; Div59.Visible = false; }

                    else { deduct5.InnerText = "* " + SOLAR_OPTION5; Div59.Visible = true; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION6)) { deduct6.InnerText = ""; Div60.Visible = false; }

                    else { deduct6.InnerText = "* " + SOLAR_OPTION6; Div60.Visible = true; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION7)) { deduct7.InnerText = ""; Div61.Visible = false; }

                    else { deduct7.InnerText = "* " + SOLAR_OPTION7; Div61.Visible = true; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION8)) { deduct8.InnerText = ""; Div68.Visible = false; }

                    else { deduct8.InnerText = "* " + SOLAR_OPTION8; Div68.Visible = true; }

                    ///------------------------------>>>>-------------------------------------------




                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        Label lbl = new Label();

                        if (i + 1 == 11)
                        {
                            lbl.Text = "* " + ReturnCovers[i]+ " (10 % of the sum insured)";
                        }

                        else if (i + 1 == 13) { lbl.Text = "* " + ReturnCovers[i] + " (with barn marks)"; }
                        else { lbl.Text = "* " + ReturnCovers[i]; }

                            if (i + 1 == 2 || i + 1 == 3)//i + 1 == 2 || 
                        {

                            dv1.Controls.Remove(lbl);
                            dv1.Controls.Remove(new LiteralControl("<br/>"));

                        }
                        else
                        {
                            if (dh_cov_flood == "0")
                            {
                                if (i + 1 == 7)
                                {
                                    dv1.Controls.Remove(lbl);
                                    dv1.Controls.Remove(new LiteralControl("<br/>"));


                                }
                                else
                                {
                                    dv1.Controls.Add(lbl);
                                    dv1.Controls.Add(new LiteralControl("<br/>"));

                                }
                            }
                            else
                            {
                                dv1.Controls.Add(lbl);
                                dv1.Controls.Add(new LiteralControl("<br/>"));
                            }

                        }
                    }


                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "N") // fire + other + srcc
                {


                    //-dedcutible changes-----30112021----------->>>>>>>>>>>>


                    if (string.IsNullOrEmpty(SOLAR_OPTION1)) { deduct1.InnerText = ""; Div35.Visible = false; }

                    else { deduct1.InnerText = "* " + SOLAR_OPTION1; Div35.Visible = true; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION2)) { deduct2.InnerText = ""; Div36.Visible = false; }

                    else { deduct2.InnerText = "* " + SOLAR_OPTION2; Div36.Visible = false; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION3)) { deduct3.InnerText = ""; Div37.Visible = false; }

                    else { deduct3.InnerText = "* " + SOLAR_OPTION3; Div37.Visible = true; }


                    if (string.IsNullOrEmpty(SOLAR_OPTION4)) { deduct4.InnerText = ""; Div38.Visible = false; }

                    else
                    {   //if flood not request
                        //Div38.Visible = false;
                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            deduct4.InnerText = "* " + SOLAR_OPTION4;
                            Div38.Visible = false;
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                deduct4.InnerText = "* " + SOLAR_OPTION4;
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                reDeduct1 = SOLAR_OPTION4.Substring(0, 8);
                                reDeduct2 = SOLAR_OPTION4.Substring(10, 24);
                                reDeduct3 = SOLAR_OPTION4.Substring(42);
                                deductVal1 = dh_deductible_pre;//dh_deductible.Substring(0, 2);
                                deductVal2 = dh_deductible;//dh_deductible.Substring(6);
                                deduct4.InnerText = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + " " + deductVal2 + " " + reDeduct3;
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION5)) { deduct5.InnerText = ""; Div59.Visible = false; }

                    else { deduct5.InnerText = "* " + SOLAR_OPTION5; Div59.Visible = true; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION6)) { deduct6.InnerText = ""; Div60.Visible = false; }

                    else { deduct6.InnerText = "* " + SOLAR_OPTION6; Div60.Visible = true; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION7)) { deduct7.InnerText = ""; Div61.Visible = false; }

                    else { deduct7.InnerText = "* " + SOLAR_OPTION7; Div61.Visible = true; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION8)) { deduct8.InnerText = ""; Div68.Visible = false; }

                    else { deduct8.InnerText = "* " + SOLAR_OPTION8; Div68.Visible = true; }
                    ///------------------------------>>>>-------------------------------------------


                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        Label lbl = new Label();
                        //lbl.Text = "* " + ReturnCovers[i];
                        if (i + 1 == 11)
                        {
                            lbl.Text = "* " + ReturnCovers[i] + " (10 % of the sum insured)";
                        }

                        else if (i + 1 == 13) { lbl.Text = "* " + ReturnCovers[i] + " (with barn marks)"; }
                        else { lbl.Text = "* " + ReturnCovers[i]; }

                        if (i + 1 == 3)
                        {

                            dv1.Controls.Remove(lbl);
                            dv1.Controls.Remove(new LiteralControl("<br/>"));

                        }
                        else
                        {
                            if (dh_cov_flood == "0")
                            {
                                if (i + 1 == 7)
                                {
                                    dv1.Controls.Remove(lbl);
                                    dv1.Controls.Remove(new LiteralControl("<br/>"));


                                }
                                else
                                {
                                    dv1.Controls.Add(lbl);
                                    dv1.Controls.Add(new LiteralControl("<br/>"));
                                }
                            }
                            else
                            {
                                dv1.Controls.Add(lbl);
                                dv1.Controls.Add(new LiteralControl("<br/>"));
                            }

                        }
                    }


                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "Y") // fire + other + srcc +tr
                {


                    //-dedcutible changes-----30112021----------->>>>>>>>>>>>


                    if (string.IsNullOrEmpty(SOLAR_OPTION1)) { deduct1.InnerText = ""; Div35.Visible = false; }

                    else { deduct1.InnerText = "* " + SOLAR_OPTION1; Div35.Visible = true; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION2)) { deduct2.InnerText = ""; Div36.Visible = false; }

                    else { deduct2.InnerText = "* " + SOLAR_OPTION2; Div36.Visible = true; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION3)) { deduct3.InnerText = ""; Div37.Visible = false; }

                    else { deduct3.InnerText = "* " + SOLAR_OPTION3; Div37.Visible = true; }


                    if (string.IsNullOrEmpty(SOLAR_OPTION4)) { deduct4.InnerText = ""; Div38.Visible = false; }

                    else
                    {   //if flood not request
                        //Div38.Visible = false;
                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            deduct4.InnerText = "* " + SOLAR_OPTION4;
                            Div38.Visible = false;
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                deduct4.InnerText = "* " + SOLAR_OPTION4;
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;
                                reDeduct1 = SOLAR_OPTION4.Substring(0, 8);
                                reDeduct2 = SOLAR_OPTION4.Substring(10, 24);
                                reDeduct3 = SOLAR_OPTION4.Substring(42);
                                deductVal1 = dh_deductible_pre;//dh_deductible.Substring(0, 2);
                                deductVal2 = dh_deductible;//dh_deductible.Substring(6);
                                deduct4.InnerText = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + " " + deductVal2 + " " + reDeduct3;
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION5)) { deduct5.InnerText = ""; Div59.Visible = false; }

                    else { deduct5.InnerText = "* " + SOLAR_OPTION5; Div59.Visible = true; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION6)) { deduct6.InnerText = ""; Div60.Visible = false; }

                    else { deduct6.InnerText = "* " + SOLAR_OPTION6; Div60.Visible = true; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION7)) { deduct7.InnerText = ""; Div61.Visible = false; }

                    else { deduct7.InnerText = "* " + SOLAR_OPTION7; Div61.Visible = true; }

                    if (string.IsNullOrEmpty(SOLAR_OPTION8)) { deduct8.InnerText = ""; Div68.Visible = false; }

                    else { deduct8.InnerText = "* " + SOLAR_OPTION8; Div68.Visible = true; }
                    ///------------------------------>>>>-------------------------------------------



                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        Label lbl = new Label();
                        //lbl.Text = "* " + ReturnCovers[i];
                        if (i + 1 == 11)
                        {
                            lbl.Text = "* " + ReturnCovers[i] + " (10 % of the sum insured)";
                        }

                        else if (i + 1 == 13) { lbl.Text = "* " + ReturnCovers[i] + " (with barn marks)"; }
                        else { lbl.Text = "* " + ReturnCovers[i]; }

                        if (dh_cov_flood == "0")
                        {
                            if (i + 1 == 7)
                            {
                                dv1.Controls.Remove(lbl);
                                dv1.Controls.Remove(new LiteralControl("<br/>"));


                            }
                            else
                            {
                                dv1.Controls.Add(lbl);
                                dv1.Controls.Add(new LiteralControl("<br/>"));
                            }
                        }
                        else
                        {
                            dv1.Controls.Add(lbl);
                            dv1.Controls.Add(new LiteralControl("<br/>"));
                        }

                    }
                }

                else
                {
                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        Label lbl = new Label();
                        lbl.Text = "* " + ReturnCovers[i];
                        dv1.Controls.Remove(lbl);
                        dv1.Controls.Remove(new LiteralControl("<br/>"));
                    }

                }

                Div19.Attributes.Add("style", "display:none");
                Div20.Attributes.Add("style", "display:none");
                Div51.Attributes.Add("style", "display:normal");
                lblSolarSum.InnerHtml = DH_SOLAR_SUM;


                //warrenties--->>>
                Div52.Attributes.Add("style", "display:normal");
               
                waranty2.InnerHtml = "<u><b>Hot Work Warranty</u></b><br/>" +
"It is warranted and it is agreed as a condition precedent to the insurer’s liability that during the currency of this policy, that before the use of a naked flame or other heat source or oxyacetylene, electric arc or similar welding, cutting, grinding or other spark emitting equipment is used by any person(whether a third party contractor, an employee or other) at the premises(other than in connection with the insured's trade processes), the Insured shall take necessary precautions to carry out such work as follows;<br/>" +
"1. Equipment for hot works has been checked and found in good state of repair.</br>" +
"2. Hot works shall be carried out by trained personnel only and in the presence of at least one worker or supervisor equipped with a fire extinguisher and trained in fire fighting.<br/>" +
"3. At least two suitable fire extinguishers or a hose reel are immediately available for use.</br>" +
"4. Items which are susceptible to ignition and fire including flammable liquids and combustible materials shall be removed from the area which is subject to hot works.</br>" +
"5. The work area and all adjacent areas to which sparks and heat might have spread(such as floors below and above, and areas on other sides of the walls) have been inspected and found to be free of fire following completion of the hot works.</br>" +
"6. A trained person, not directly involved with the work, should provide a continuous fire watch  during the Period of hot works and for at least 1(one) hour after its ceases following each period of work, in the work area and those adjoining areas to which sparks and heat may spread.</br>" +
"7. Floors must be swept clean before and after the hot works.";

                Div53.Attributes.Add("style", "display:normal");
                waranty3.InnerHtml = "<u><b>Machinery Maintenance Warranty</u></b><br/>" +
                    "All Plant & Machinery, Electrical & Electronic equipment should be maintained in accordance with the manufacturer’s maintenance specifications & records should be kept with the Insured for future reference.";

                Div69.Attributes.Add("style", "display:normal");
                waranty4.InnerHtml = "<u><b>Surge Arrestors warranty </u></b><br/> " +
                     "It is warranted that main power distribution panel should be equipped with surge arrestors and the same should be maintained in effective condition during the period of insurance.";

                Div70.Attributes.Add("style", "display:normal");
                waranty5.InnerHtml = "<u><b>Building maintenance warranty </u></b><br/> " +
                    "It is warranted that preventive / periodical maintenance for all the Building, Roof, Gutters & Drain Lines should be done on regular basis.";

                //end warrenties-->>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>


                //Conditions table for solar

                Div54.Attributes.Add("style", "display:normal");
                Div55.Attributes.Add("style", "display:normal");
                Div56.Attributes.Add("style", "display:normal");
                Div57.Attributes.Add("style", "display:normal");
                lblconditionSolar1.InnerHtml = "1. Theft Cover <br/>" +
                        "2. Mysterious & unexplainable losses <br/>" +
                        "3. Any loss of or damage caused to pipes & tubes of the solar panel(If a water heater is interconnected) <br/>" +
                        "4. Manufacturing defects/wear & tear/gradual Erosion & Corrosion";



                //if (!string.IsNullOrEmpty(dh_conditions))
                //{


                //    lblconditionSolar2.InnerHtml = "This policy is issued subject to;<br/>" +
                //    "1. Submission of serial No(S), Model No(s), Year of Manufacture / Purchase & values separately of solar panel / Solar Panels covered under this insurance.<br/>" +
                //    "2. Local Repairer, Sole agent & spare parts must be available in Sri Lanka in the event of a repairable damage.<br/>" +
                //    "3. Safety Measures should be implemented when shifting the Item / Items.<br/>" +
                //    "4. Claim settlement will be done on indemnity value basis.<br/>" +
                //    "5. Cover does not operate during the period of Installation / testing & commissioning.<br/>" + "6. "+dh_conditions+".";

                //}

                //else
                //{
                    lblconditionSolar2.InnerHtml = "This policy is issued subject to;<br/>" +
                    "1. Submission of serial No(S), Model No(s), Year of Manufacture / Purchase & values separately of solar panel / Solar Panels covered under this insurance.<br/>" +
                    "2. Availability of Repairer, Sole agent & Spare parts in Sri Lanka in the event of a damage.<br/>" +
                    "3. Safety Measures should be implemented when shifting the Item / Items.<br/>" +
                    "4. Claim settlement will be done on indemnity value basis.<br/>" +
                    "5. Cover does not operate during the period of Installation / testing & commissioning.";
               // }

               
                //long term wording
                if (TERM.ToString() == "1")
                {
                    Div58.Attributes.Add("style", "display:none");
                    Div62.Attributes.Add("style", "display:none");


                }
                else
                {
                    Div58.Attributes.Add("style", "display:normal");
                    Div62.Attributes.Add("style", "display:normal");

                    DateTime renewFromDate = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(1);
                    string renwalFrom = date.ToString("dd/MM/yyyy");

                    //DateTime renewEndDate = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(Convert.ToInt32(PeriodTemp));changed15122022 to below
					DateTime renewEndDate = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(Convert.ToInt32(PeriodTemp)+1);
                    string renwalEnd = renewEndDate.ToString("dd/MM/yyyy");

                    longFrom.InnerHtml = renwalFrom + " to " + renwalEnd;
                    longYear.InnerHtml = PeriodTemp;

                }
            }
            else
            {
               
            }
            

            //---------------------------------------------------------->>>>>
        }
        catch (Exception ex) {

            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

        }


    }

    public bool isCheckNumber( string InputVal)
    {
        bool returnVal = false;
        int countVal = 0;
        try
        {
            string input = InputVal;
            // Split on one or more non-digit characters.
            string[] numbers = Regex.Split(input, @"\D+");
            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    //Console.WriteLine("Number: {0}", i);
                    countVal++;
                }
            }

            if (countVal > 0) { returnVal = true; }
            else { returnVal = false; }
        }
        catch (Exception ex) { }


        return returnVal;
    }

    public enum PopupMessageType
    {
        Error,
        Message,
        Warning,
        Success
    }
    
    public void ShowMessage(string Message, WarningType type)
    {
        //gets the controls from the page
        Panel PanelMessage = Master.FindControl("Message") as Panel;
        Label labelMessage = PanelMessage.FindControl("labelMessage") as Label;

        //sets the message and the type of alert, than displays the message
        labelMessage.Text = Message;
        PanelMessage.CssClass = string.Format("alert alert-{0} alert-dismissable", type.ToString().ToLower());
        PanelMessage.Attributes.Add("role", "alert");
        PanelMessage.Visible = true;
    }


    public enum WarningType
    {
        Success,
        Info,
        Warning,
        Danger
    }



    //-->print pdf
    private void PrintPdf()
    {
        int count_1 = 0;



        try
        {
            sum_insuVal = Convert.ToDouble(hfSumInsu.Value);
           


            this.getPropClass.GetSchedualCalValues(hfRefId.Value, out SC_POLICY_NO, out SC_SUM_INSU, out SC_NET_PRE,
                out SC_RCC, out SC_TR, out SC_ADMIN_FEE,
                out SC_POLICY_FEE, out SC_NBT, out SC_VAT, out SC_TOTAL_PAY, out CREATED_ON, out CREATED_BY,
                out FLAG, out SC_Renewal_FEE, out BPF_FEE, out DEBIT_NO);
            //------------------------->>-------------policy details-------------------->>>

            this.getPropClass.GetEnteredDetails(hfRefId.Value, out dh_bcode, out dh_bbrcode, out dh_name, out dh_agecode, out dh_agename, out dh_nic, out dh_br,
    out dh_padd1, out dh_padd2, out dh_padd3, out dh_padd4, out dh_phone, out dh_email, out dh_iadd1, out dh_iadd2, out dh_iadd3,
    out dh_iadd4, out dh_pfrom, out dh_pto, out dh_uconstr, out dh_occu_car, out dh_occ_yes_reas, out dh_haz_occu, out dh_haz_yes_rea,
    out dh_valu_build, out dh_valu_wall,
    out dh_valu_total, out dh_aff_flood, out dh_aff_yes_reas, out dh_wbrick,
    out dh_wcement, out dh_dwooden, out dh_dmetal, out dh_ftile, out dh_fcement, out dh_rtile,
    out dh_rasbes, out dh_rgi, out dh_rconcreat, out dh_cov_fire,
    out dh_cov_light, out dh_cov_flood, out dh_cfwateravl, out dh_cfyesr1, out dh_cfyesr2, out dh_cfyesr3, out dh_cfyesr4,
    out dh_entered_by, out dh_entered_on, out dh_hold, out DH_NO_OF_FLOORS, out DH_OVER_VAL, out DH_FINAL_FLAG,
    out dh_isreq, out dh_conditions, out dh_isreject, out dh_iscodi, out dh_bcode_id, out dh_bbrcode_id, out DH_LOADING, out DH_LOADING_VAL,out LAND_PHONE, out DH_VAL_BANKFAC, out dh_deductible, out dh_deductible_pre,
    out TERM, out Period, out Fire_cover, out Other_cover, out SRCC_cover, out TC_cover, out Flood_cover,
    out BANK_UPDATED_BY, out BANK_UPDATED_ON, out PROP_TYPE, out DH_SOLAR_SUM, out SOLAR_REPAIRE,
    out SOLAR_PARTS, out SOLAR_ORIGIN, out SOLAR_SERIAL, out Solar_Period, out LOAN_NUMBER);

            this.GetAgentDetails(dh_agecode);
            string address1 = string.Empty;


            if (dh_padd3 != "" && dh_padd4 == "")
            {
                address1 = dh_padd1 + ", " + dh_padd2 + "," + dh_padd3;
            }
            else if (dh_padd3 != "" && dh_padd4 != "") { address1 = dh_padd1 + ", " + dh_padd2 + ", " + dh_padd3 + ", " + dh_padd4; }
            else if (dh_padd3 == "" && dh_padd4 != "") { address1 = dh_padd1 + ", " + dh_padd2 + ", " + dh_padd4; }
            else
            {
                address1 = dh_padd1 + ", " + dh_padd2;
            }


            DateTime date = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(1);
            string toDateNext = date.ToString("dd/MM/yyyy");
            string period_val = "";
            if (TERM.ToString() == "1")
            { period_val = "From : " + dh_pfrom + "  " + "  To : " + toDateNext; }
            else
            { period_val = "From : " + dh_pfrom + "  " + "  To : " + toDateNext+ " (First Year)"; }

            
            string sumInsuVal = "Rs. " + dh_valu_total;
            string agentNameCode = dh_agecode + " - " + dh_agename;
            string branchNameCode = dh_bbrcode_id + "-" + dh_bbrcode;

            string constructDetails = "";
            if (dh_uconstr == "1")
            {
                constructDetails = "Under Construction";
            }
            else if (dh_uconstr == "0")
            {
                constructDetails = "Completed"; //claus1.InnerHtml = "* Completed";
            }
            else { constructDetails = ""; }


            string exWall = string.Empty;

            if (dh_wbrick == "1" && dh_wcement == "1") { exWall = "Brick and Cement"; }
            else if (dh_wcement == "1") { exWall = "Cement"; }
            else if (dh_wbrick == "1") { exWall = "Brick"; }
            else { exWall = ""; }

            string numOfFloors = DH_NO_OF_FLOORS;

            string roofDetails = string.Empty;

            if (dh_rtile == "1" && dh_rasbes == "1" && dh_rgi == "1" && dh_rconcreat == "1")
            {
                roofDetails = "Tile, Asbestos, GI Sheets and Concrete";
            }

            else if (dh_rtile == "1" && dh_rasbes == "1" && dh_rgi == "1")
            {
                roofDetails = "Tile, Asbestos and GI Sheets";
            }

            else if (dh_rtile == "1" && dh_rconcreat == "1" && dh_rgi == "1")
            {
                roofDetails = "Tile, GI Sheets and Concrete";
            }
            else if (dh_rtile == "1" && dh_rasbes == "1")
            {
                roofDetails = "Tile and Asbestos";
            }
            else if (dh_rtile == "1" && dh_rgi == "1")
            {
                roofDetails = "Tile and GI Sheets";
            }
            else if (dh_rtile == "1" && dh_rconcreat == "1")
            {
                roofDetails = "Tile and Concrete";
            }
            else if (dh_rtile == "1") { roofDetails = "Tile"; }



            //------->
            else if (dh_rasbes == "1" && dh_rgi == "1" && dh_rconcreat == "1")
            {
                roofDetails = "Asbestos, GI Sheets and Concrete";
            }

            else if (dh_rasbes == "1" && dh_rgi == "1")
            {
                roofDetails = "Asbestos and GI Sheets";
            }

            else if (dh_rasbes == "1")
            {
                roofDetails = "Asbestos";
            }
            //---->
            else if (dh_rgi == "1" && dh_rconcreat == "1")
            {
                roofDetails = "GI Sheets and Concrete";
            }

            else if (dh_rgi == "1")
            {
                roofDetails = "GI Sheets";
            }

            else if (dh_rconcreat == "1")
            {
                roofDetails = "Concrete";
            }

            string situated = string.Empty;
            
            if (dh_iadd3 != "" && dh_iadd4 == "")
            {
                situated = dh_iadd1 + ", " + dh_iadd2 + ", " + dh_iadd3;
            }
            else if (dh_iadd3 != "" && dh_iadd4 != "") { situated= dh_iadd1 + ", " + dh_iadd2 + ", " + dh_iadd3 + ", " + dh_iadd4; }
            else if (dh_iadd3 == "" && dh_iadd4 != "") { situated = dh_iadd1 + ", " + dh_iadd2 + ", " + dh_iadd4; }
            else
            {
                situated= dh_iadd1 + ", " + dh_iadd2;
            }

            bool sucsess = this.isCheckNumber(dh_iadd1);
            if (sucsess) { waranty1.InnerHtml = ""; }
            else { waranty1.InnerHtml = "* Warranty No.36."; }

            Document document = new Document(PageSize.A4, 45, 45, 40, 40);
            MemoryStream output = new MemoryStream();
            PdfWriter writer = PdfWriter.GetInstance(document, output);
            PdfPTable tableLayout = new PdfPTable(7);
            PdfPTable tableLayout2 = new PdfPTable(4);
            document.SetPageSize(iTextSharp.text.PageSize.A4);
            //string ip = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            document.Open();


            Font titleFont1 = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
            Font whiteFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
            Font subTitleFont = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
            Font boldTableFont = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
            Font endingMessageFont = FontFactory.GetFont("Times New Roman", 10, Font.ITALIC);
            Font bodyFont = FontFactory.GetFont("Times New Roman", 10, Font.NORMAL);
            Font bodyFont2 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
            Font bodyFont3 = FontFactory.GetFont("Times New Roman", 9, Font.NORMAL);
            Font bodyFont4 = FontFactory.GetFont("Times New Roman", 8, Font.NORMAL);
            Font bodyFont4_bold = FontFactory.GetFont("Times New Roman", 7, Font.BOLD);
            Font bodyFont5 = FontFactory.GetFont("Times New Roman", 7, Font.NORMAL);
            Font linebreak = FontFactory.GetFont("Times New Roman", 5, Font.NORMAL);
            Font bodyFont2_bold = FontFactory.GetFont("Times New Roman", 9, Font.BOLD);
            Font bodyFont3_bold = FontFactory.GetFont("Times New Roman", 10, Font.BOLD);
            Font bodyFont2_bold_und = FontFactory.GetFont("Times New Roman", 9, Font.BOLD | Font.UNDERLINE);

            string brnameVal = string.Empty;

            string conDetails = "Buildings are deemed to be constructed with walls of Bricks / Cement / Cement Blocks / Concrete and Doors & Windows with Wooden / Metal / Aluminum and Roof with Asbestos / Tile / GI Sheets / Concrete / Metal, unless the insurer has been advised otherwise. (Please refer special notes below)";
            document.Add(new Paragraph("\n\n"));

            //header table-----------------------------****
            int[] clmwidths112 = { 100, 0 };
            int[] logoWidth = { 50, 50 };
            PdfPTable addrees = new PdfPTable(2);
            PdfPTable tbl15 = new PdfPTable(2);

            tbl15.SetWidths(clmwidths112);
            addrees.SetWidths(logoWidth);



            addrees.WidthPercentage = 100;
            addrees.HorizontalAlignment = Element.ALIGN_CENTER;
            addrees.SpacingBefore = 10;
            addrees.SpacingAfter = 0;
            addrees.DefaultCell.Border = 0;

            tbl15.WidthPercentage = 100;
            tbl15.HorizontalAlignment = Element.ALIGN_CENTER;
            tbl15.SpacingBefore = 10;
            tbl15.SpacingAfter = 0;
            tbl15.DefaultCell.Border = 0;

            int[] clmwidths111 = { 35, 2, 70, 20 };

            PdfPTable tbl14 = new PdfPTable(4);

            tbl14.SetWidths(clmwidths111);

            tbl14.WidthPercentage = 100;
            tbl14.HorizontalAlignment = Element.ALIGN_CENTER;
            tbl14.SpacingBefore = 10;
            tbl14.SpacingAfter = 0;
            tbl14.DefaultCell.Border = 0;


            int[] clmwidths121 = { 40, 20, 20, 55 };

            PdfPTable tbl16 = new PdfPTable(4);

            tbl16.SetWidths(clmwidths121);

            tbl16.WidthPercentage = 100;
            tbl16.HorizontalAlignment = Element.ALIGN_LEFT;
            tbl16.SpacingBefore = 10;
            tbl16.SpacingAfter = 0;
            tbl16.DefaultCell.Border = 0;


            int[] clmwidths123 = { 35, 2, 70, 20 };

            PdfPTable tbl17 = new PdfPTable(4);

            tbl17.SetWidths(clmwidths123);

            tbl17.WidthPercentage = 100;
            tbl17.HorizontalAlignment = Element.ALIGN_LEFT;
            tbl17.SpacingBefore = 10;
            tbl17.SpacingAfter = 0;
            tbl17.DefaultCell.Border = 0;

            //var physicalPath = Server.MapPath("~/Images/slic_logo1.png");
            //Image logo_temp = Image.GetInstance(physicalPath);
            //logo_temp.ScalePercent(40f, 30f);
            //logo_temp.SetAbsolutePosition(60, 320);
            //document.Add(logo_temp);

            //var physicalPath2 = Server.MapPath("~/Images/slicLogo.png");

            //Image logo_temp2 = Image.GetInstance(physicalPath2);
            //logo_temp2.ScalePercent(22f, 20f);
            //logo_temp2.SetAbsolutePosition(350, 700);
            //document.Add(logo_temp2);

            var physicalPath = Server.MapPath("~/Images/BancassuranceLogo.png");
            Image logo_temp = Image.GetInstance(physicalPath);
            logo_temp.ScalePercent(22f, 20f);
            logo_temp.SetAbsolutePosition(30, 700);
            logo_temp.ScaleAbsolute(520f, 125f); // Adjust size as needed
            document.Add(logo_temp);

            PdfPCell cell = new PdfPCell(new Phrase("\n\n\n\n\n\n\n\n", bodyFont2));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl15.AddCell(cell);
            tbl15.AddCell(new Phrase("", bodyFont2));

            cell = new PdfPCell(new Phrase("Policy Schedule", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            tbl15.AddCell(cell);
            tbl15.AddCell(new Phrase("", bodyFont2));

            cell = new PdfPCell(new Phrase("Standard Fire Policy Schedule for Private Dwelling House", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            tbl15.AddCell(cell);
            tbl15.AddCell(new Phrase("", bodyFont2));


            cell = new PdfPCell(new Phrase("\n", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_CENTER;
            cell.Border = 0;
            tbl15.AddCell(cell);
            tbl15.AddCell(new Phrase("", bodyFont2));

            cell = new PdfPCell(new Phrase("Policy Number", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            tbl14.AddCell(new Phrase(SC_POLICY_NO, bodyFont2));

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);


            //NSB Loan Number changes-->>>
            if (dh_bcode_id.ToString().Trim() == "7719")
            {
                cell = new PdfPCell(new Phrase("Bank Loan Number", bodyFont2_bold));
                cell.HorizontalAlignment = 0;
                cell.Border = 0;
                tbl14.AddCell(cell);

                cell = new PdfPCell(new Phrase(":", bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                tbl14.AddCell(cell);

                tbl14.AddCell(new Phrase(LOAN_NUMBER.Trim(), bodyFont2));

                cell = new PdfPCell(new Phrase("", bodyFont2_bold));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                tbl14.AddCell(cell);
            }
            else { }
            ///--------->>>>>>>>>>>>>>>>>>

            cell = new PdfPCell(new Phrase("Debit Note Number", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            tbl14.AddCell(new Phrase(txt_debit.InnerHtml, bodyFont2));

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase("Name of the Insured", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            tbl14.AddCell(new Phrase(dh_name, bodyFont2));

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase("Address", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            tbl14.AddCell(new Phrase(address1, bodyFont2));

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase("Financial Interests", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);
            
           tbl14.AddCell(new Phrase(dh_bcode + "-" + dh_bbrcode, bodyFont2));
           cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase("Period of Insurance", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);
            tbl14.AddCell(new Phrase(period_val, bodyFont2));

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase("Sum Insured", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            tbl14.AddCell(new Phrase(sumInsuVal, bodyFont2));

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase("Agency Code & Name", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            tbl14.AddCell(new Phrase(agentNameCode, bodyFont2)); 
            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase("Branch Code & Name", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            tbl14.AddCell(new Phrase(branchNameCode, bodyFont2));

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase("Construction Details", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            tbl14.AddCell(new Phrase(constructDetails, bodyFont2));

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);



            //NSB Changes------------------>>>>>>
            

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);


            cell = new PdfPCell(new Phrase(conDetails, bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);


            cell = new PdfPCell(new Phrase("Number of Floors : "+ numOfFloors, bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            tbl14.AddCell(cell);

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl14.AddCell(cell);
            //------------------------------>>>>>

            //--->.

            //cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            //cell.HorizontalAlignment = 0;
            //cell.Border = 0;
            //tbl16.AddCell(cell);

            //cell = new PdfPCell(new Phrase("Walls", bodyFont2));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //tbl16.AddCell(cell);

            //tbl16.AddCell(new Phrase(":", bodyFont2));

            //cell = new PdfPCell(new Phrase(exWall, bodyFont2_bold));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //tbl16.AddCell(cell);


            //cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            //cell.HorizontalAlignment = 0;
            //cell.Border = 0;
            //tbl16.AddCell(cell);

            //cell = new PdfPCell(new Phrase("Number of Floors", bodyFont2));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //tbl16.AddCell(cell);

            //tbl16.AddCell(new Phrase(": "+ numOfFloors, bodyFont2));

            //cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //tbl16.AddCell(cell);


            //cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            //cell.HorizontalAlignment = 0;
            //cell.Border = 0;
            //tbl16.AddCell(cell);

            //cell = new PdfPCell(new Phrase("Roof", bodyFont2));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //tbl16.AddCell(cell);

            //tbl16.AddCell(new Phrase(":", bodyFont2));

            //cell = new PdfPCell(new Phrase(roofDetails, bodyFont2_bold));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //tbl16.AddCell(cell);

           

            cell = new PdfPCell(new Phrase("Situated at", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl17.AddCell(cell);

            cell = new PdfPCell(new Phrase(":", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl17.AddCell(cell);

            tbl17.AddCell(new Phrase(situated, bodyFont2));

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl17.AddCell(cell);



            cell = new PdfPCell(new Phrase("", bodyFont2_bold)); //remove \n for nsb changes
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl17.AddCell(cell);

            cell = new PdfPCell(new Phrase("", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl17.AddCell(cell);

            tbl17.AddCell(new Phrase("", bodyFont2));

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl17.AddCell(cell);

            cell = new PdfPCell(new Phrase("Detail of items to be insured", bodyFont2_bold));
            cell.HorizontalAlignment = 0;
            cell.Border = 0;
            tbl17.AddCell(cell);

            cell = new PdfPCell(new Phrase("", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl17.AddCell(cell);

            tbl17.AddCell(new Phrase(" ", bodyFont2));

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tbl17.AddCell(cell);

            // table viewer---------------------------------------------------------------

            int[] colWidth = { 80, 30 };

            PdfPTable tableFooter = new PdfPTable(2);

            tableFooter.SetWidths(colWidth);

            tableFooter.WidthPercentage = 100;
            tableFooter.HorizontalAlignment = Element.ALIGN_CENTER;
            tableFooter.SpacingBefore = 10;
            tableFooter.SpacingAfter = 0;
            tableFooter.DefaultCell.Border = 10;



            int[] colWidthcover = { 100 };

            PdfPTable CoverTable = new PdfPTable(1);

            CoverTable.SetWidths(colWidthcover);

            CoverTable.WidthPercentage = 100;
            CoverTable.HorizontalAlignment = Element.ALIGN_LEFT;
            CoverTable.SpacingBefore = 10;
            CoverTable.SpacingAfter = 0;
            CoverTable.DefaultCell.Border = 0;


            int[] colWidthdeductible = { 100 };

            PdfPTable DeductTable = new PdfPTable(1);

            DeductTable.SetWidths(colWidthdeductible);

            DeductTable.WidthPercentage = 100;
            DeductTable.HorizontalAlignment = Element.ALIGN_LEFT;
            DeductTable.SpacingBefore = 10;
            DeductTable.SpacingAfter = 0;
            DeductTable.DefaultCell.Border = 0;



            int[] colWidthpremium = { 8, 8, 2, 4 };

            PdfPTable PremiumTable = new PdfPTable(4);

            PremiumTable.SetWidths(colWidthpremium);
            PremiumTable.WidthPercentage = 70;
            PremiumTable.HorizontalAlignment = Element.ALIGN_LEFT;
            PremiumTable.SpacingBefore = 0;
            PremiumTable.SpacingAfter = 0;
            PremiumTable.DefaultCell.Border = 0;

            int[] colWidthBreak = { 100 };

            PdfPTable breakTable = new PdfPTable(1);

            breakTable.SetWidths(colWidthBreak);

            breakTable.WidthPercentage = 100;
            breakTable.HorizontalAlignment = Element.ALIGN_CENTER;
            breakTable.SpacingBefore = 10;
            breakTable.SpacingAfter = 0;
            breakTable.DefaultCell.Border = 10;

            //------------1---
            PdfPCell cellFooter = new PdfPCell(new Phrase("Particulars of Risk", bodyFont2_bold));
            cellFooter.HorizontalAlignment = 0;
            cellFooter.Border = 50;
            cellFooter.HorizontalAlignment = Element.ALIGN_LEFT;
            cellFooter.Border = Rectangle.BOX;
            cellFooter.BorderWidth = 1;
            cellFooter.DisableBorderSide(Rectangle.BOTTOM_BORDER);
            cellFooter.Padding = 8;

            tableFooter.AddCell(cellFooter);

            //------------2---
            PdfPCell cellFooter2 = new PdfPCell(new Phrase("Sum Insured (Rs.)", bodyFont2_bold));
            cellFooter2.Border = 50;
            cellFooter2.HorizontalAlignment = Element.ALIGN_CENTER;
            cellFooter2.Border = Rectangle.BOX;
            cellFooter2.BorderWidth = 1;

            cellFooter2.DisableBorderSide(Rectangle.BOTTOM_BORDER);
            cellFooter2.DisableBorderSide(Rectangle.LEFT_BORDER);
            cellFooter2.Padding = 3;
            tableFooter.AddCell(cellFooter2);

            //conditions for solar + buildings--------------07102021
            if (PROP_TYPE == "1" || PROP_TYPE == "2")
            {
                //------------3---
                PdfPCell cellFooter5 = new PdfPCell(new Phrase("Building together with permanent fixtures & fittings including electrical wiring for permanent lighting & switches.", bodyFont2));
                cellFooter5.HorizontalAlignment = 0;
                cellFooter5.Border = 50;
                cellFooter5.HorizontalAlignment = Element.ALIGN_LEFT;
                cellFooter5.Border = Rectangle.BOX;
                cellFooter5.BorderWidth = 1;
                cellFooter5.DisableBorderSide(Rectangle.BOTTOM_BORDER);
                cellFooter5.Padding = 8;

                tableFooter.AddCell(cellFooter5);

                //------------4---


                PdfPCell cellFooter6 = new PdfPCell(new Phrase(dh_valu_build, bodyFont));

                cellFooter6.HorizontalAlignment = 0;
                cellFooter6.Border = 50;
                cellFooter6.HorizontalAlignment = Element.ALIGN_RIGHT;
                //cell.BorderColor = BaseColor.BLACK;
                cellFooter6.Border = Rectangle.BOX;
                cellFooter6.BorderWidth = 1;
                cellFooter6.DisableBorderSide(Rectangle.LEFT_BORDER);
                cellFooter6.DisableBorderSide(Rectangle.BOTTOM_BORDER);
                cellFooter6.Padding = 5;

                tableFooter.AddCell(cellFooter6);

                if (dh_valu_wall == "0.00") { }
                else
                {

                    //------------3-I--
                    PdfPCell cellFooter3I = new PdfPCell(new Phrase("Value of the boundary and parapet wall.", bodyFont2));
                    cellFooter3I.HorizontalAlignment = 0;
                    cellFooter3I.Border = 50;
                    cellFooter3I.HorizontalAlignment = Element.ALIGN_LEFT;
                    cellFooter3I.Border = Rectangle.BOX;
                    cellFooter3I.BorderWidth = 1;
                    cellFooter3I.DisableBorderSide(Rectangle.BOTTOM_BORDER);
                    cellFooter3I.Padding = 8;

                    tableFooter.AddCell(cellFooter3I);

                    //------------4--I-

                    PdfPCell cellFooter4I = new PdfPCell(new Phrase(dh_valu_wall, bodyFont));

                    cellFooter4I.HorizontalAlignment = 0;
                    cellFooter4I.Border = 50;
                    cellFooter4I.HorizontalAlignment = Element.ALIGN_RIGHT;
                    //cell.BorderColor = BaseColor.BLACK;
                    cellFooter4I.Border = Rectangle.BOX;
                    cellFooter4I.BorderWidth = 1;
                    cellFooter4I.DisableBorderSide(Rectangle.LEFT_BORDER);
                    cellFooter4I.DisableBorderSide(Rectangle.BOTTOM_BORDER);
                    cellFooter4I.Padding = 5;

                    tableFooter.AddCell(cellFooter4I);

                }
            }
            else { }
            //solar---------->>
            if (PROP_TYPE == "2" || PROP_TYPE == "3")
            {
                //------------3-II--
                PdfPCell cellFooter3II = new PdfPCell(new Phrase("On solar panel system & standard accessories", bodyFont2));
                cellFooter3II.HorizontalAlignment = 0;
                cellFooter3II.Border = 50;
                cellFooter3II.HorizontalAlignment = Element.ALIGN_LEFT;
                cellFooter3II.Border = Rectangle.BOX;
                cellFooter3II.BorderWidth = 1;
                cellFooter3II.DisableBorderSide(Rectangle.BOTTOM_BORDER);
                cellFooter3II.Padding = 8;

                tableFooter.AddCell(cellFooter3II);

                //------------4--II-

                PdfPCell cellFooter4II = new PdfPCell(new Phrase(DH_SOLAR_SUM, bodyFont));

                cellFooter4II.HorizontalAlignment = 0;
                cellFooter4II.Border = 50;
                cellFooter4II.HorizontalAlignment = Element.ALIGN_RIGHT;
                //cell.BorderColor = BaseColor.BLACK;
                cellFooter4II.Border = Rectangle.BOX;
                cellFooter4II.BorderWidth = 1;
                cellFooter4II.DisableBorderSide(Rectangle.LEFT_BORDER);
                cellFooter4II.DisableBorderSide(Rectangle.BOTTOM_BORDER);
                cellFooter4II.Padding = 5;

                tableFooter.AddCell(cellFooter4II);
            }
            else { }
            //-------------5--
            PdfPCell cellFooter9 = new PdfPCell(new Phrase("Total Sum Insured", bodyFont2_bold));
            cellFooter9.HorizontalAlignment = 0;
            cellFooter9.Border = 50;
            cellFooter9.HorizontalAlignment = Element.ALIGN_LEFT;
            cellFooter9.Border = Rectangle.BOX;
            cellFooter9.BorderWidth = 1;
            cellFooter9.Padding = 8;

            tableFooter.AddCell(cellFooter9);

            //----------------end cell


            //------------6---
            PdfPCell cellFooter10 = new PdfPCell(new Phrase(dh_valu_total, bodyFont3_bold));
            cellFooter10.HorizontalAlignment = 0;
            cellFooter10.Border = 50;
            cellFooter10.HorizontalAlignment = Element.ALIGN_RIGHT;
            cellFooter10.Border = Rectangle.BOX;
            cellFooter10.BorderWidth = 1;
            cellFooter10.DisableBorderSide(Rectangle.LEFT_BORDER);
            cellFooter10.Padding = 8;

            tableFooter.AddCell(cellFooter10);

            //----------------end cell
            // table viewer2---------------------------------------------------------------
            //cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //CoverTable.AddCell(cell);

            //cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //CoverTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("This Insurance is subject to the Covers/Clauses/Endorsements indicated herein & attached hereto:", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            CoverTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            CoverTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Scope of Cover", bodyFont3_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            CoverTable.AddCell(cell);



            ///------------------------------>>>>-------------------------------------------
            ///

            //------------cover and deductibles---------------15092021------------------>>


            //covers logics re-arrange 13092021----------------------------->>
            cell = new PdfPCell(new Phrase("", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            DeductTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Excess/Deductibles Applicable\n", bodyFont3_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            DeductTable.AddCell(cell);

            if (PROP_TYPE == "1")
            {
                ReturnCovers = getPropClass.getFireCoverArray();
                this.getPropClass.GetDeductibles(sum_insuVal, out DH_OPTION1, out DH_OPTION2, out DH_OPTION3, out DH_OPTION4, out DH_OPTION5, out DH_OPTION6, out DH_OPTION7);
                if (Fire_cover == "Y" && Other_cover == "N" && SRCC_cover == "N" && TC_cover == "N") //fire and lighting
                {
                    //-dedcutible changes-----13092021----------->>>>>>>>>>>>

                    if (string.IsNullOrEmpty(DH_OPTION1))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION2))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION3))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION4))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION4, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {   //if flood not request

                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            //DH_OPTION3 = "* " + reDeduct1 + "" + reDeduct2;

                            cell = new PdfPCell(new Phrase(DH_OPTION4, bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Border = 0;
                            //DeductTable.AddCell(cell);
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {

                                DH_OPTION4 = "* " + DH_OPTION4;
                                cell = new PdfPCell(new Phrase(DH_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                //DeductTable.AddCell(cell);
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;

                                reDeduct1 = DH_OPTION4.Substring(0, 8);
                                reDeduct2 = DH_OPTION4.Substring(10, 24);
                                reDeduct3 = DH_OPTION4.Substring(42);


                                deductVal1 = dh_deductible_pre;
                                deductVal2 = dh_deductible;
                                DH_OPTION4 = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + " " + deductVal2 + " " + reDeduct3;
                                cell = new PdfPCell(new Phrase(DH_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                //DeductTable.AddCell(cell);
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(DH_OPTION5))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION6))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION7))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        string FireOnlReDeduct1 = DH_OPTION7.Substring(0, 4);
                        string FireOnlReDeduct2 = DH_OPTION7.Substring(9);
                        string FireOnlReDeduct3 = FireOnlReDeduct1 + FireOnlReDeduct2;
                      

                        cell = new PdfPCell(new Phrase("* " + FireOnlReDeduct3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }
                    ///------------------------------>>>>-------------------------------------------

                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {

                        if (i + 1 == 1)
                        {
                            cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cell.Border = 0;
                            CoverTable.AddCell(cell);

                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cell.Border = 0;
                            //CoverTable.AddCell(cell);

                        }
                    }

                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "N" && TC_cover == "N") //fire + other
                {

                    //-dedcutible changes-----13092021----------->>>>>>>>>>>>

                    if (string.IsNullOrEmpty(DH_OPTION1))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION2))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION3))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION4))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION4, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {   //if flood not request

                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            //DH_OPTION3 = "* " + reDeduct1 + "" + reDeduct2;

                            cell = new PdfPCell(new Phrase(DH_OPTION4, bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Border = 0;
                            //DeductTable.AddCell(cell);
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {

                                DH_OPTION4 = "* " + DH_OPTION4;
                                cell = new PdfPCell(new Phrase(DH_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                DeductTable.AddCell(cell);
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;

                                reDeduct1 = DH_OPTION4.Substring(0, 8);
                                reDeduct2 = DH_OPTION4.Substring(10, 24);
                                reDeduct3 = DH_OPTION4.Substring(42);


                                deductVal1 = dh_deductible_pre;
                                deductVal2 = dh_deductible;
                                DH_OPTION4 = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + " " + deductVal2 + " " + reDeduct3;
                                cell = new PdfPCell(new Phrase(DH_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                DeductTable.AddCell(cell);
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(DH_OPTION5))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION6))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION7))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }
                    ///------------------------------>>>>-------------------------------------------




                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {


                        if (i + 1 == 2 || i + 1 == 3)//i + 1 == 2 || 
                        {

                            cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cell.Border = 0;
                            //CoverTable.AddCell(cell);

                        }
                        else
                        {
                            if (dh_cov_flood == "0")
                            {
                                if (i + 1 == 7)
                                {
                                    cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                    cell.Border = 0;
                                    //CoverTable.AddCell(cell);


                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                    cell.Border = 0;
                                    CoverTable.AddCell(cell);
                                }
                            }
                            else
                            {
                                cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cell.Border = 0;
                                CoverTable.AddCell(cell);
                            }

                        }
                    }


                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "N") // fire + other + srcc
                {


                    //-dedcutible changes-----13092021----------->>>>>>>>>>>>

                    if (string.IsNullOrEmpty(DH_OPTION1))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION2))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION3))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION4))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION4, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {   //if flood not request

                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            //DH_OPTION3 = "* " + reDeduct1 + "" + reDeduct2;

                            cell = new PdfPCell(new Phrase(DH_OPTION4, bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Border = 0;
                            //DeductTable.AddCell(cell);
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {

                                DH_OPTION4 = "* " + DH_OPTION4;
                                cell = new PdfPCell(new Phrase(DH_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                DeductTable.AddCell(cell);
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;

                                reDeduct1 = DH_OPTION4.Substring(0, 8);
                                reDeduct2 = DH_OPTION4.Substring(10, 24);
                                reDeduct3 = DH_OPTION4.Substring(42);


                                deductVal1 = dh_deductible_pre;
                                deductVal2 = dh_deductible;
                                DH_OPTION4 = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + " " + deductVal2 + " " + reDeduct3;
                                cell = new PdfPCell(new Phrase(DH_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                DeductTable.AddCell(cell);
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(DH_OPTION5))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION6))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION7))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }
                    ///------------------------------>>>>-------------------------------------------


                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {

                        if (i + 1 == 3)
                        {
                            cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cell.Border = 0;
                            //CoverTable.AddCell(cell);

                        }
                        else
                        {
                            if (dh_cov_flood == "0")
                            {
                                if (i + 1 == 7)
                                {
                                    cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                    cell.Border = 0;
                                    //CoverTable.AddCell(cell);


                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                    cell.Border = 0;
                                    CoverTable.AddCell(cell);
                                }
                            }
                            else
                            {
                                cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cell.Border = 0;
                                CoverTable.AddCell(cell);
                            }

                        }
                    }


                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "Y") // fire + other + srcc +tr
                {


                    //-dedcutible changes-----13092021----------->>>>>>>>>>>>

                    if (string.IsNullOrEmpty(DH_OPTION1))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION2))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION3))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION4))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION4, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {   //if flood not request

                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            //DH_OPTION3 = "* " + reDeduct1 + "" + reDeduct2;

                            cell = new PdfPCell(new Phrase(DH_OPTION4, bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Border = 0;
                            //DeductTable.AddCell(cell);
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {

                                DH_OPTION4 = "* " + DH_OPTION4;
                                cell = new PdfPCell(new Phrase(DH_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                DeductTable.AddCell(cell);
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;

                                reDeduct1 = DH_OPTION4.Substring(0, 8);
                                reDeduct2 = DH_OPTION4.Substring(10, 24);
                                reDeduct3 = DH_OPTION4.Substring(42);


                                deductVal1 = dh_deductible_pre;
                                deductVal2 = dh_deductible;
                                DH_OPTION4 = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + " " + deductVal2 + " " + reDeduct3;
                                cell = new PdfPCell(new Phrase(DH_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                DeductTable.AddCell(cell);
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(DH_OPTION5))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION6))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(DH_OPTION7))
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + DH_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }
                    ///------------------------------>>>>-------------------------------------------



                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {

                        if (dh_cov_flood == "0")
                        {
                            if (i + 1 == 7)
                            {
                                cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cell.Border = 0;
                                //CoverTable.AddCell(cell);


                            }
                            else
                            {
                                cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cell.Border = 0;
                                CoverTable.AddCell(cell);
                            }
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cell.Border = 0;
                            CoverTable.AddCell(cell);
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        cell.Border = 0;
                        //CoverTable.AddCell(cell);
                    }

                }


                //---------------------------------------------------------------------->>>>>
            }
            //end------------------------------------------------------------------->>>>>
            else if (PROP_TYPE == "3")
            {
                double sumInsuSolar = Convert.ToDouble(dh_valu_total);

                bool SolarRtn1, SolarRtn2 = false;
                getPropClass.GetElectricalClauseForSolar(dh_bcode_id, sumInsuSolar, sumInsuSolar, out SOLAR_ELECT, out SolarRtn1);
                getPropClass.GetAccidentalClauseForSolar(dh_bcode_id, sumInsuSolar, sumInsuSolar, out SOLAR_ACCIDENTAL, out SolarRtn2);

                string solarElect = SOLAR_ELECT.ToString("###,###,###0");
                string solarAcc = SOLAR_ACCIDENTAL.ToString("###,###,###0");

                string solarExt1 = "1. Electrical inclusion clause-Without Burn Mark(in the event of any loss of or damage) caused to insured items over and above 03 years of age from the date of manufacture and the claim settlement will be done according to the terms and condition of stranded fire insurance policy of SLIC which covered only named perils and based on indemnity basis)\n" +
                    "* Maximum limit up to Rs. " + solarElect + "/-"+ "\n(as per the endorsement attaching herewith)\n\n";
                //"(as per the endorsement attaching herewith)";

                string solarExt2 = "2. Accidental Damage (any sudden & unforeseen physical loss or damage directly caused to the insured property up to a limit to Rs. " + solarAcc + "/- )";

                //covers logics re-arrange 13092021----------------------------->>
                ReturnCovers = getPropClass.getFireCoverArray();
                this.getPropClass.GetDeductiblesForSolar(sum_insuVal, out SOLAR_OPTION1, out SOLAR_OPTION2, out SOLAR_OPTION3, out SOLAR_OPTION4, out SOLAR_OPTION5, out SOLAR_OPTION6, out SOLAR_OPTION7, out SOLAR_OPTION8);


                if (Fire_cover == "Y" && Other_cover == "N" && SRCC_cover == "N" && TC_cover == "N") //fire and lighting
                {
                    //-dedcutible changes-----13092021----------->>>>>>>>>>>>

                    if (string.IsNullOrEmpty(SOLAR_OPTION1))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION2))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION3))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION4))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION4, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {   //if flood not request

                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            //DH_OPTION3 = "* " + reDeduct1 + "" + reDeduct2;

                            cell = new PdfPCell(new Phrase(SOLAR_OPTION4, bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Border = 0;
                            //DeductTable.AddCell(cell);
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {

                                SOLAR_OPTION4 = "* " + SOLAR_OPTION4;
                                cell = new PdfPCell(new Phrase(SOLAR_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                //DeductTable.AddCell(cell);
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;

                                reDeduct1 = SOLAR_OPTION4.Substring(0, 8);
                                reDeduct2 = SOLAR_OPTION4.Substring(10, 24);
                                reDeduct3 = SOLAR_OPTION4.Substring(42);


                                deductVal1 = dh_deductible_pre;
                                deductVal2 = dh_deductible;
                                SOLAR_OPTION4 = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + "" + deductVal2 + " " + reDeduct3;
                                cell = new PdfPCell(new Phrase(SOLAR_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                //DeductTable.AddCell(cell);
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION5))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION6))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION7))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);

                    }
                    if (string.IsNullOrEmpty(SOLAR_OPTION8))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION8, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION8, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }
                    ///------------------------------>>>>-------------------------------------------

                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {

                        if (i + 1 == 1)
                        {
                            cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cell.Border = 0;
                            CoverTable.AddCell(cell);

                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cell.Border = 0;
                            //CoverTable.AddCell(cell);

                        }
                    }

                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "N" && TC_cover == "N") //fire + other
                {

                    //-dedcutible changes-----13092021----------->>>>>>>>>>>>

                    if (string.IsNullOrEmpty(SOLAR_OPTION1))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION2))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION3))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION4))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION4, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {   //if flood not request

                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            //DH_OPTION3 = "* " + reDeduct1 + "" + reDeduct2;

                            cell = new PdfPCell(new Phrase(SOLAR_OPTION4, bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Border = 0;
                            //DeductTable.AddCell(cell);
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {

                                SOLAR_OPTION4 = "* " + SOLAR_OPTION4;
                                cell = new PdfPCell(new Phrase(SOLAR_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                DeductTable.AddCell(cell);
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;

                                reDeduct1 = SOLAR_OPTION4.Substring(0, 8);
                                reDeduct2 = SOLAR_OPTION4.Substring(10, 24);
                                reDeduct3 = SOLAR_OPTION4.Substring(42);


                                deductVal1 = dh_deductible_pre;
                                deductVal2 = dh_deductible;
                                SOLAR_OPTION4 = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + "" + deductVal2 + " " + reDeduct3;
                                cell = new PdfPCell(new Phrase(SOLAR_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                DeductTable.AddCell(cell);
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION5))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION6))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION7))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION8))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION8, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION8, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }
                    ///------------------------------>>>>-------------------------------------------




                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        string coverLables = "";
                        if (i + 1 == 11)
                        {
                            coverLables = "* " + ReturnCovers[i] + " (10 % of the sum insured)";
                        }

                        else if (i + 1 == 13) { coverLables = "* " + ReturnCovers[i] + " (with barn marks)"; }
                        else { coverLables = "* " + ReturnCovers[i]; }

                        if (i + 1 == 2 || i + 1 == 3)//i + 1 == 2 || 
                        {

                            cell = new PdfPCell(new Phrase(coverLables, bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cell.Border = 0;
                            //CoverTable.AddCell(cell);

                        }
                        else
                        {
                            if (dh_cov_flood == "0")
                            {
                                if (i + 1 == 7)
                                {
                                    cell = new PdfPCell(new Phrase(coverLables, bodyFont2));
                                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                    cell.Border = 0;
                                    //CoverTable.AddCell(cell);


                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase(coverLables, bodyFont2));
                                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                    cell.Border = 0;
                                    CoverTable.AddCell(cell);
                                }
                            }
                            else
                            {
                                cell = new PdfPCell(new Phrase(coverLables, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cell.Border = 0;
                                CoverTable.AddCell(cell);
                            }

                        }
                    }


                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "N") // fire + other + srcc
                {


                    //-dedcutible changes-----13092021----------->>>>>>>>>>>>

                    if (string.IsNullOrEmpty(SOLAR_OPTION1))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION2))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION3))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION4))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION4, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {   //if flood not request

                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            //DH_OPTION3 = "* " + reDeduct1 + "" + reDeduct2;

                            cell = new PdfPCell(new Phrase(SOLAR_OPTION4, bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Border = 0;
                            //DeductTable.AddCell(cell);
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {

                                SOLAR_OPTION4 = "* " + SOLAR_OPTION4;
                                cell = new PdfPCell(new Phrase(SOLAR_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                DeductTable.AddCell(cell);
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;

                                reDeduct1 = SOLAR_OPTION4.Substring(0, 8);
                                reDeduct2 = SOLAR_OPTION4.Substring(10, 24);
                                reDeduct3 = SOLAR_OPTION4.Substring(42);


                                deductVal1 = dh_deductible_pre;
                                deductVal2 = dh_deductible;
                                SOLAR_OPTION4 = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + "" + deductVal2 + " " + reDeduct3;
                                cell = new PdfPCell(new Phrase(SOLAR_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                DeductTable.AddCell(cell);
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION5))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION6))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION7))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION8))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION8, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION8, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }
                    ///------------------------------>>>>-------------------------------------------


                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        string coverLables = "";
                        if (i + 1 == 11)
                        {
                            coverLables = "* " + ReturnCovers[i] + " (10 % of the sum insured)";
                        }

                        else if (i + 1 == 13) { coverLables = "* " + ReturnCovers[i] + " (with barn marks)"; }
                        else { coverLables = "* " + ReturnCovers[i]; }

                        if (i + 1 == 3)
                        {
                            cell = new PdfPCell(new Phrase(coverLables, bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cell.Border = 0;
                            //CoverTable.AddCell(cell);

                        }
                        else
                        {
                            if (dh_cov_flood == "0")
                            {
                                if (i + 1 == 7)
                                {
                                    cell = new PdfPCell(new Phrase(coverLables, bodyFont2));
                                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                    cell.Border = 0;
                                    //CoverTable.AddCell(cell);


                                }
                                else
                                {
                                    cell = new PdfPCell(new Phrase(coverLables, bodyFont2));
                                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                    cell.Border = 0;
                                    CoverTable.AddCell(cell);
                                }
                            }
                            else
                            {
                                cell = new PdfPCell(new Phrase(coverLables, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cell.Border = 0;
                                CoverTable.AddCell(cell);
                            }

                        }
                    }


                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "Y") // fire + other + srcc +tr
                {


                    //-dedcutible changes-----13092021----------->>>>>>>>>>>>

                    if (string.IsNullOrEmpty(SOLAR_OPTION1))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION1, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION2))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION2, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION3))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION3, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION4))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION4, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {   //if flood not request

                        if (dh_cov_flood == "0")
                        {
                            //reDeduct1 = DH_OPTION3.Substring(0, 50);
                            //reDeduct2 = DH_OPTION3.Substring(56);
                            //DH_OPTION3 = "* " + reDeduct1 + "" + reDeduct2;

                            cell = new PdfPCell(new Phrase(SOLAR_OPTION4, bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_LEFT;
                            cell.Border = 0;
                            //DeductTable.AddCell(cell);
                        }

                        else
                        {
                            if (string.IsNullOrEmpty(dh_deductible))
                            {

                                SOLAR_OPTION4 = "* " + SOLAR_OPTION4;
                                cell = new PdfPCell(new Phrase(SOLAR_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                DeductTable.AddCell(cell);
                            }

                            else
                            {
                                deducID.InnerText = ""; deducID.Visible = false; dedcuRow.Visible = false;

                                reDeduct1 = SOLAR_OPTION4.Substring(0, 8);
                                reDeduct2 = SOLAR_OPTION4.Substring(10, 24);
                                reDeduct3 = SOLAR_OPTION4.Substring(42);


                                deductVal1 = dh_deductible_pre;
                                deductVal2 = dh_deductible;
                                SOLAR_OPTION4 = "* " + reDeduct1 + deductVal1 + "" + reDeduct2 + "" + deductVal2 + " " + reDeduct3;
                                cell = new PdfPCell(new Phrase(SOLAR_OPTION4, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                                cell.Border = 0;
                                DeductTable.AddCell(cell);
                            }

                        }

                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION5))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION5, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION6))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION6, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }

                    if (string.IsNullOrEmpty(SOLAR_OPTION7))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION7, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }
                    if (string.IsNullOrEmpty(SOLAR_OPTION8))
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION8, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        //DeductTable.AddCell(cell);
                    }

                    else
                    {
                        cell = new PdfPCell(new Phrase("* " + SOLAR_OPTION8, bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_LEFT;
                        cell.Border = 0;
                        DeductTable.AddCell(cell);
                    }
                    ///------------------------------>>>>-------------------------------------------



                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        string coverLables = "";
                        if (i + 1 == 11)
                        {
                            coverLables = "* " + ReturnCovers[i] + " (10 % of the sum insured)";
                        }

                        else if (i + 1 == 13) { coverLables = "* " + ReturnCovers[i] + " (with barn marks)"; }
                        else { coverLables = "* " + ReturnCovers[i]; }

                        if (dh_cov_flood == "0")
                        {
                            if (i + 1 == 7)
                            {
                                cell = new PdfPCell(new Phrase(coverLables, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cell.Border = 0;
                                //CoverTable.AddCell(cell);


                            }
                            else
                            {
                                cell = new PdfPCell(new Phrase(coverLables, bodyFont2));
                                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                                cell.Border = 0;
                                CoverTable.AddCell(cell);
                            }
                        }
                        else
                        {
                            cell = new PdfPCell(new Phrase(coverLables, bodyFont2));
                            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                            cell.Border = 0;
                            CoverTable.AddCell(cell);
                        }

                    }
                }
                else
                {
                    for (int i = 0; i < ReturnCovers.Length; i++)
                    {
                        cell = new PdfPCell(new Phrase("* " + ReturnCovers[i], bodyFont2));
                        cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        cell.Border = 0;
                        //CoverTable.AddCell(cell);
                    }

                }
                //extention for solar--->> 
                
                if (Fire_cover == "Y" && Other_cover == "N" && SRCC_cover == "N" && TC_cover == "N")
                {
                    cell = new PdfPCell(new Phrase("\n\n", bodyFont2_bold));//remove \n\n\n 
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.Border = 0;
                    CoverTable.AddCell(cell);
                }
                else
                {
                    if (dh_cov_flood == "1")
                    {
                        cell = new PdfPCell(new Phrase("\n\n\n\n\n", bodyFont2_bold));
                        cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        cell.Border = 0;
                        CoverTable.AddCell(cell);
                    }
                    else
                    {
                        cell = new PdfPCell(new Phrase("\n\n\n\n\n\n", bodyFont2_bold));//remove \n\n\n 
                        cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                        cell.Border = 0;
                        CoverTable.AddCell(cell);
                    }
                   
                }

                cell = new PdfPCell(new Phrase("Extension of covers", bodyFont2_bold));
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.Border = 0;
                CoverTable.AddCell(cell);
                cell = new PdfPCell(new Phrase(solarExt1, bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.Border = 0;
                CoverTable.AddCell(cell);
                cell = new PdfPCell(new Phrase(solarExt2, bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.Border = 0;
                CoverTable.AddCell(cell);
                // end---->>>>>>
            }
            else { }
                //----------------------------solar------------------------------------------>>>>>


                //end------------------------------------------------------------------->>>>>
                cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            CoverTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Premium Details", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            breakTable.AddCell(cell);


            //--------premium Tbale--------------------------------------------------->>>>
            string polFee = string.Empty;
            double year = 0;
            if (Convert.ToDouble(Period.Trim()) == 0) { year = 1; }
            else { year = Convert.ToDouble(Period.Trim()); }

            double netPerOneYear, adminFeeOneYear, policyFeeOneYear, nbtOneYear, vatOneYear, totalOneYear, renewalFeeOneYear, srccOneYear, trOneYear = 0;


            string txtnetPerOneYear, txtadminFeeOneYear, txtpolicyFeeOneYear, txtnbtOneYear, txtvatOneYear, txttotalOneYear, txtrenewalFeeOneYear, txtsrccOneYear, txttrOneYear = string.Empty;

            netPerOneYear = Math.Round(((Convert.ToDouble(SC_NET_PRE) / year)), 2, MidpointRounding.AwayFromZero);
            adminFeeOneYear = Math.Round(((Convert.ToDouble(SC_ADMIN_FEE) / year)), 2, MidpointRounding.AwayFromZero);
            policyFeeOneYear = Math.Round(((Convert.ToDouble(SC_POLICY_FEE))), 2, MidpointRounding.AwayFromZero);
            renewalFeeOneYear = Math.Round(((Convert.ToDouble(SC_Renewal_FEE) / year)), 2, MidpointRounding.AwayFromZero);
            //totalOneYear = Math.Round(((Convert.ToDouble(SC_NET_PRE) / year) / 100), 2, MidpointRounding.AwayFromZero);
            srccOneYear = Math.Round(((Convert.ToDouble(SC_RCC) / year)), 2, MidpointRounding.AwayFromZero);
            trOneYear = Math.Round(((Convert.ToDouble(SC_TR) / year)), 2, MidpointRounding.AwayFromZero);

            policy_number.InnerHtml = SC_POLICY_NO;
            txt_debit.InnerHtml = DEBIT_NO;

            txtnetPerOneYear = netPerOneYear.ToString("###,###,###0.00");
            //---12072022 vat for one year with ssc levy changes
            bool rtn = false;
            double totalForVat, calNBTOne, calVatOne, calTotalOutOne = 0;
            bool rtnLast = false;

            totalForVat = netPerOneYear + adminFeeOneYear + policyFeeOneYear + srccOneYear + trOneYear;
            rtn = premCal.VATCalculationForFirstYear(totalForVat, CREATED_ON,out calNBTOne, out calVatOne, out calTotalOutOne, out rtnLast);
            //---------
            //nbtOneYear = Math.Round(((Convert.ToDouble(SC_NBT) / year)), 2, MidpointRounding.AwayFromZero);
            //vatOneYear = Math.Round(((Convert.ToDouble(SC_VAT) / year)), 2, MidpointRounding.AwayFromZero);

            nbtOneYear = Math.Round(((Convert.ToDouble(calNBTOne))), 2, MidpointRounding.AwayFromZero);
            vatOneYear = Math.Round(((Convert.ToDouble(calVatOne))), 2, MidpointRounding.AwayFromZero);

            txtadminFeeOneYear = (adminFeeOneYear + nbtOneYear).ToString("###,###,###0.00");

            txtpolicyFeeOneYear = policyFeeOneYear.ToString("###,###,###0.00");
            txtnbtOneYear = nbtOneYear.ToString("###,###,###0.00");
            txtvatOneYear = vatOneYear.ToString("###,###,###0.00");
            txtrenewalFeeOneYear = renewalFeeOneYear.ToString("###,###,###0.00");
            txtsrccOneYear = srccOneYear.ToString("###,###,###0.00");
            txttrOneYear = trOneYear.ToString("###,###,###0.00");

            totalOneYear = Math.Round(((netPerOneYear + adminFeeOneYear + policyFeeOneYear + nbtOneYear + vatOneYear + srccOneYear + trOneYear)), 2, MidpointRounding.AwayFromZero);
            txttotalOneYear = totalOneYear.ToString("###,###,###0.00");

            //remain balnace for long term
            double remainPremium = Convert.ToDouble(SC_TOTAL_PAY) - (totalOneYear);
            remainPremium = Math.Round((Convert.ToDouble(SC_TOTAL_PAY) - (totalOneYear)), 2, MidpointRounding.AwayFromZero);
            string lblremainPremiumPdf = remainPremium.ToString("###,###,###0.00");
            //----------

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Net Premium", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Rs. ", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            cell.PaddingRight = 8;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(txtnetPerOneYear, bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

           

          
            if (TERM.ToString() == "1")
            {
                //DivRenewalFee.Visible = false; //renewal fee
                //txt_renewal.InnerHtml = SC_Renewal_FEE;

                polFee = "Policy Fee";

                if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "N")
                {
                    cell = new PdfPCell(new Phrase("", bodyFont2_bold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("SRCC", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Rs. ", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 0;
                    cell.PaddingRight = 8;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(txtsrccOneYear, bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    
                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "Y")
                {
                    cell = new PdfPCell(new Phrase("", bodyFont2_bold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("SRCC", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Rs. ", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 0;
                    cell.PaddingRight = 8;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(txtsrccOneYear, bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);


                    cell = new PdfPCell(new Phrase("", bodyFont2_bold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("TR", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Rs. ", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 0;
                    cell.PaddingRight = 8;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(txttrOneYear, bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);
                }
                else
                {
                 
                }
            }
            else
            {
               
              

                if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "N")
                {
                    cell = new PdfPCell(new Phrase("", bodyFont2_bold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("SRCC", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Rs. ", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 0;
                    cell.PaddingRight = 8;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(txtsrccOneYear, bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);
                }
                else if (Fire_cover == "Y" && Other_cover == "Y" && SRCC_cover == "Y" && TC_cover == "Y")
                {
                    cell = new PdfPCell(new Phrase("", bodyFont2_bold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("SRCC", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Rs. ", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 0;
                    cell.PaddingRight = 8;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(txtsrccOneYear, bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);


                    cell = new PdfPCell(new Phrase("", bodyFont2_bold));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("TR", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("Rs. ", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 0;
                    cell.PaddingRight = 8;
                    PremiumTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase(txttrOneYear, bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                    cell.Border = 0;
                    PremiumTable.AddCell(cell);
                }
                else
                {
                  
                }
                //cell = new PdfPCell(new Phrase("", bodyFont2_bold));
                //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.Border = 0;
                //PremiumTable.AddCell(cell);

                //cell = new PdfPCell(new Phrase("Renewal Fees", bodyFont2));
                //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //cell.Border = 0;
                //PremiumTable.AddCell(cell);

                //cell = new PdfPCell(new Phrase("Rs. ", bodyFont2));
                //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //cell.Border = 0;
                //cell.PaddingRight = 8;
                //PremiumTable.AddCell(cell);

                //cell = new PdfPCell(new Phrase(txtrenewalFeeOneYear, bodyFont2));
                //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                //cell.Border = 0;
                //PremiumTable.AddCell(cell);

                polFee = "Policy Fee for 1st year";
            }
            //--27062022---socail security contribution levy applied (replace NBT field with it) and ssc value add to admin fee 01/07/2022
            //admin fee
            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Admin Fee", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Rs. ", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            cell.PaddingRight = 8;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(txtadminFeeOneYear, bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);
            ///---------------------------------


            //policyFee fee--
            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(polFee, bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Rs. ", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            cell.PaddingRight = 8;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(txtpolicyFeeOneYear, bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);
            //end---

            //--27062022---socail security contribution levy applied (replace NBT field with it) remove this and this value add to admin fee 01/07/2022
            //policyFee fee--
            //cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //PremiumTable.AddCell(cell);

            //cell = new PdfPCell(new Phrase("Social Sec. Con.", bodyFont2));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //PremiumTable.AddCell(cell);

            //cell = new PdfPCell(new Phrase("Rs. ", bodyFont2));
            //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.Border = 0;
            //cell.PaddingRight = 8;
            //PremiumTable.AddCell(cell);

            //cell = new PdfPCell(new Phrase(txtnbtOneYear, bodyFont2));
            //cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            //cell.Border = 0;
            //PremiumTable.AddCell(cell);
            //end---
            //---end--------->>



            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("VAT", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Rs. ", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            cell.PaddingRight = 8;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(txtvatOneYear, bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 10;
            cell.DisableBorderSide(Rectangle.LEFT_BORDER);
            cell.DisableBorderSide(Rectangle.RIGHT_BORDER);
            cell.DisableBorderSide(Rectangle.TOP_BORDER);
            PremiumTable.AddCell(cell);

            //------------>>break for line 

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Total Payable", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("Rs. ", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            cell.PaddingRight = 8;
            cell.DisableBorderSide(Rectangle.RIGHT_BORDER);
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase(txttotalOneYear, bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 10;
            cell.DisableBorderSide(Rectangle.LEFT_BORDER);
            cell.DisableBorderSide(Rectangle.RIGHT_BORDER);
            PremiumTable.AddCell(cell);


            //------------>>break for line 

            cell = new PdfPCell(new Phrase("", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            cell.DisableBorderSide(Rectangle.RIGHT_BORDER);
            PremiumTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 10;
            cell.DisableBorderSide(Rectangle.LEFT_BORDER);
            cell.DisableBorderSide(Rectangle.RIGHT_BORDER);
            PremiumTable.AddCell(cell);

           
            //--------->


            cell = new PdfPCell(new Phrase("", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            DeductTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("\nClauses", bodyFont3_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            DeductTable.AddCell(cell);

            cell = new PdfPCell(new Phrase("* Bank / Mortgage clause.", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            DeductTable.AddCell(cell);
 


            if (dh_uconstr == "1")
            {
                if (PROP_TYPE == "3")
                {
                    //cell = new PdfPCell(new Phrase("* Reinstatement Value Clause.", bodyFont2));
                    //cell = new PdfPCell(new Phrase("", bodyFont2));
                    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    //cell.Border = 0;
                    //DeductTable.AddCell(cell);

                    //cell = new PdfPCell(new Phrase("", bodyFont2));
                    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    //cell.Border = 0;
                    //DeductTable.AddCell(cell);
                }
                else
                {
                    cell = new PdfPCell(new Phrase("", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    DeductTable.AddCell(cell);
                }
            }
            else if (dh_uconstr == "0") //complete
            {
                if (PROP_TYPE == "2" || PROP_TYPE == "1") //|| PROP_TYPE == "3"
                {
                    cell = new PdfPCell(new Phrase("* Reinstatement Value Clause.", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    DeductTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    DeductTable.AddCell(cell);
                }
                else {
                    //cell = new PdfPCell(new Phrase("", bodyFont2));
                    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    //cell.Border = 0;
                    //DeductTable.AddCell(cell);

                    //cell = new PdfPCell(new Phrase("", bodyFont2));
                    //cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    //cell.Border = 0;
                    //DeductTable.AddCell(cell);
                }
            }
            else
            {
                cell = new PdfPCell(new Phrase("", bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                DeductTable.AddCell(cell);
            }


            cell = new PdfPCell(new Phrase("\nWarranties\n", bodyFont3_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            DeductTable.AddCell(cell);

            if (PROP_TYPE == "1")
            {
                bool sucsess1 = this.isCheckNumber(dh_iadd1);
                if (sucsess1)
                {
                    cell = new PdfPCell(new Phrase("", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    DeductTable.AddCell(cell);

                }
                else
                {
                    cell = new PdfPCell(new Phrase("* Identification warranty", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    DeductTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("The said property is the only property so situated and answering to the foregoing description in which the insured has interests", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.Border = 0;
                    DeductTable.AddCell(cell);

                    cell = new PdfPCell(new Phrase("", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    DeductTable.AddCell(cell);
                }

                double sumTotal = Convert.ToDouble(dh_valu_total);
                if (sumTotal > 20000000)
                {
                    cell = new PdfPCell(new Phrase("* Building Maintenance Warranty", bodyFont2)); //solar all terms
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    DeductTable.AddCell(cell);
                    cell = new PdfPCell(new Phrase("It is warranted that preventive / periodical for all the Building, Roof, Gutters & Drain Lines should be done on regular basis.\n\n", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                    cell.Border = 0;
                    DeductTable.AddCell(cell);

                }
                cell = new PdfPCell(new Phrase("* Premium Payment Warranty 60 days", bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                DeductTable.AddCell(cell);
            }
            //warrenty for solar-------07102021------
            if (PROP_TYPE == "3")
            {
               
                    cell = new PdfPCell(new Phrase("Hot Work Warranty", bodyFont2_bold)); //solar long term only 
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    DeductTable.AddCell(cell);

                string hotWorks ="It is warranted and it is agreed as a condition precedent to the insurer’s liability that during the currency of this policy, that before the use of a naked flame or other heat source or oxyacetylene, electric arc or similar welding, cutting, grinding or other spark emitting equipment is used by any person(whether a third party contractor, an employee or other) at the premises(other than in connection with the insured's trade processes), the Insured shall take necessary precautions to carry out such work as follows;\n\n" +
"1. Equipment for hot works has been checked and found in good state of repair.\n\n" +
"2. Hot works shall be carried out by trained personnel only and in the presence of at least one worker or supervisor equipped with a fire extinguisher and trained in fire fighting.\n\n" +
"3. At least two suitable fire extinguishers or a hose reel are immediately available for use.\n\n" +
"4. Items which are susceptible to ignition and fire including flammable liquids and combustible materials shall be removed from the area which is subject to hot works.\n\n" +
"5. The work area and all adjacent areas to which sparks and heat might have spread(such as floors below and above, and areas on other sides of the walls) have been inspected and found to be free of fire following completion of the hot works.\n\n" +
"6. A trained person, not directly involved with the work, should provide a continuous fire watch  during the Period of hot works and for at least 1(one) hour after its ceases following each period of work, in the work area and those adjoining areas to which sparks and heat may spread.\n\n" +
"7. Floors must be swept clean before and after the hot works.\n\n";
                    cell = new PdfPCell(new Phrase(hotWorks, bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    DeductTable.AddCell(cell);


                cell = new PdfPCell(new Phrase("Machinery Maintenance Warranty", bodyFont2_bold)); //solar long term only 
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                DeductTable.AddCell(cell);
                cell = new PdfPCell(new Phrase("All Plant & Machinery, Electrical & Electronic equipment should be maintained in accordance with the manufacturer’s maintenance specifications & records should be kept with the Insured for future reference.\n", bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                DeductTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Surge Arrestors Warranty", bodyFont2_bold)); //solar long term only 
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                DeductTable.AddCell(cell);
                cell = new PdfPCell(new Phrase("It is warranted that main power distribution panel should be equipped with surge arrestors and the same should be maintained in effective condition during the period of insurance.\n", bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                DeductTable.AddCell(cell);

                cell = new PdfPCell(new Phrase("Building Maintenance Warranty", bodyFont2_bold)); //solar long term only 
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                DeductTable.AddCell(cell);
                cell = new PdfPCell(new Phrase("It is warranted that preventive / periodical maintenance for all the Building, Roof, Gutters & Drain Lines should be done on regular basis.\n\n", bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                DeductTable.AddCell(cell);
            }
            else { }
            //---------------------------------------

           

            //conditions-------------------------

            //cell = new PdfPCell(new Phrase("", bodyFont2));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //DeductTable.AddCell(cell);



            
            //solar only condtion section--------------07102021----->>
            if (PROP_TYPE == "3")
            {
                cell = new PdfPCell(new Phrase("\nExclusions", bodyFont3_bold));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                DeductTable.AddCell(cell);


               string lblconditionSolar1 = "1. Theft Cover\n\n" +
                        "2. Mysterious & unexplainable losses\n\n" +
                        "3. Any loss of or damage caused to pipes & tubes of the solar panel(If a water heater is interconnected)\n\n" +
                        "4. Manufacturing defects/wear & tear/gradual Erosion & Corrosion";

               
                cell = new PdfPCell(new Phrase(lblconditionSolar1, bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                DeductTable.AddCell(cell);



                //--------section 1-2
               

                //if (!string.IsNullOrEmpty(dh_conditions))
                //{
                    

                //    cell = new PdfPCell(new Phrase("\n6. " + dh_conditions, bodyFont2));
                //    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                //    cell.Border = 0;
                //    DeductTable.AddCell(cell);
                //}

            }

            else { }
          
            ///----------------->>>>>>>>>>>>>>>>

            int[] colWidthfooter5 = { 100 };

            PdfPTable tableFooter5 = new PdfPTable(1);

            tableFooter5.SetWidths(colWidthfooter5);

            tableFooter5.WidthPercentage = 100;
            tableFooter5.HorizontalAlignment = Element.ALIGN_CENTER;
            tableFooter5.SpacingBefore = 10;
            tableFooter5.SpacingAfter = 0;
            tableFooter5.DefaultCell.Border = 0;

            //cell = new PdfPCell(new Phrase("Note - We advise you to Insure your Building for the new replacement value (Reinstatement basis) in order to avoid reduction in claim for depreciation.", bodyFont2_bold));
            //cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            //cell.Border = 0;
            //cell.Padding = 5;
            //tableFooter5.AddCell(cell);

            //cell = new PdfPCell(new Phrase("", bodyFont2));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //cell.Padding = 5;
            //tableFooter5.AddCell(cell);



            //-----solar or building long term-- 07102021---------->>.
            if (TERM.ToString() == "0")
            {
                string PeriodTemp = (Convert.ToInt32(Period) - 1).ToString();
                DateTime renewFromDate = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(1);
                string renwalFrom = date.ToString("dd/MM/yyyy");

                //DateTime renewEndDate = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(Convert.ToInt32(PeriodTemp)); --changed15122022
				DateTime renewEndDate = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture).AddYears(Convert.ToInt32(PeriodTemp)+1);
                string renwalEnd = renewEndDate.ToString("dd/MM/yyyy");

                string toDateSecond = renwalFrom + " to " + renwalEnd;
                //longYear.InnerHtml = PeriodTemp;


               
               // DateTime dateSecond = DateTime.ParseExact(dh_pfrom, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //string toDateSecond = dateSecond.ToString("dd-MMMM-yyyy");

                cell = new PdfPCell(new Phrase("\nIt is hereby declared & agreed that the within written policy will be automatically renewed for a further period of " +
                    PeriodTemp +
                    " years, with effect from " +
                    toDateSecond +" and premium of Rs. "+ lblremainPremiumPdf+" will be adjusted annually.\n\n", bodyFont2_bold));
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.Border = 0;
                cell.Padding = 0;
                tableFooter5.AddCell(cell);

                cell = new PdfPCell(new Phrase("Further declared and agreed that the premium on SRCC, TR & Taxes will be revised based on amendments being done on rates applicable by NITF & the inland revenue department respectively\n", bodyFont2_bold));
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.Border = 0;
                cell.Padding = 0;
                tableFooter5.AddCell(cell);

                
            }
            else { }
            //finamcel interet table---
            int[] colWidthFinancel = { 80, 20 };

            PdfPTable tableFooterFinacel = new PdfPTable(2);

            tableFooterFinacel.SetWidths(colWidthFinancel);

            tableFooterFinacel.WidthPercentage = 100;
            tableFooterFinacel.HorizontalAlignment = Element.ALIGN_CENTER;
            tableFooterFinacel.SpacingBefore = 10;
            tableFooterFinacel.SpacingAfter = 0;
            tableFooterFinacel.DefaultCell.Border = 0;


            cell = new PdfPCell(new Phrase("Financial Interests :", bodyFont3_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            cell.Padding = 0;
            tableFooterFinacel.AddCell(cell);

            cell = new PdfPCell(new Phrase("", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            cell.Padding = 0;
            tableFooterFinacel.AddCell(cell);


            cell = new PdfPCell(new Phrase(" " + dh_bcode + "-" + dh_bbrcode, bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tableFooterFinacel.AddCell(cell);

            cell = new PdfPCell(new Phrase("", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            cell.Padding = 0;
            tableFooterFinacel.AddCell(cell);


            cell = new PdfPCell(new Phrase("\n  Particulars of Risk:", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            cell.Padding = 0;
            tableFooterFinacel.AddCell(cell);

            cell = new PdfPCell(new Phrase("\n  Assigned Value (Rs.)", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_RIGHT;
            cell.Border = 0;
            cell.Padding = 0;
            tableFooterFinacel.AddCell(cell);

            if (PROP_TYPE == "3")
            {
                cell = new PdfPCell(new Phrase("\n  * On Solar power system & Standard Accessories", bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                cell.Padding = 0;
                tableFooterFinacel.AddCell(cell);
            }
            else if (PROP_TYPE == "1")
            {
                cell = new PdfPCell(new Phrase("\n  * On the building of private dwelling house including permanent fixtures and fittings", bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                cell.Padding = 0;
                tableFooterFinacel.AddCell(cell);
            }
            else
            {
                cell = new PdfPCell(new Phrase("", bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                cell.Padding = 0;
                tableFooterFinacel.AddCell(cell);
            }

            //changes -- 04012022--->> 
            if (Convert.ToDouble(DH_VAL_BANKFAC.Trim()) == 0)
            {
                cell = new PdfPCell(new Phrase("\n  " + dh_valu_total, bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = 0;
                cell.Padding = 0;
                tableFooterFinacel.AddCell(cell);

            }
            else
               {
                cell = new PdfPCell(new Phrase("\n  " + DH_VAL_BANKFAC, bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_RIGHT;
                cell.Border = 0;
                cell.Padding = 0;
                tableFooterFinacel.AddCell(cell);
               }
            //----->>

            


            // note table--
            int[] colWidthfooterNote = { 100 };

            PdfPTable tableFooterNote = new PdfPTable(1);

            tableFooterNote.SetWidths(colWidthfooterNote);

            tableFooterNote.WidthPercentage = 100;
            tableFooterNote.HorizontalAlignment = Element.ALIGN_CENTER;
            tableFooterNote.SpacingBefore = 10;
            tableFooterNote.SpacingAfter = 0;
            tableFooterNote.DefaultCell.Border = 0;


            //conditions------solar + house
            if (PROP_TYPE == "3")
            {
                string lblconditionSolar2 = "";

                //if (!string.IsNullOrEmpty(dh_conditions))
                //{
                //    lblconditionSolar2 = "This policy is issued subject to;\n\n" +
                //         "1. Submission of serial No(S), Model No(s), Year of Manufacture / Purchase & values separately of solar panel / Solar Panels covered under this insurance.\n\n" +
                //         "2. Local Repairer, Sole agent & spare parts must be available in Sri Lanka in the event of a repairable damage.\n\n" +
                //         "3. Safety Measures should be implemented when shifting the Item / Items.\n\n" +
                //         "4. Claim settlement will be done on indemnity value basis.\n\n" +
                //         "5. Cover does not operate during the period of Installation / testing & commissioning.\n\n" + "6. " + dh_conditions + ".";
                //}
                //else
                //{
                    lblconditionSolar2 = "This policy is issued subject to;\n\n" +
                                           "1. Submission of serial No(S), Model No(s), Year of Manufacture / Purchase & values separately of solar panel / Solar Panels covered under this insurance.\n\n" +
                                           "2. Availability of Repairer, Sole agent & Spare parts in Sri Lanka in the event of a damage.\n\n" +
                                           "3. Safety Measures should be implemented when shifting the Item / Items.\n\n" +
                                           "4. Claim settlement will be done on indemnity value basis.\n\n" +
                                           "5. Cover does not operate during the period of Installation / testing & commissioning.";
                //}

                cell = new PdfPCell(new Phrase("\nConditions Applicable to Solar Panel System", bodyFont3_bold));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                tableFooterNote.AddCell(cell);

                cell = new PdfPCell(new Phrase(lblconditionSolar2, bodyFont2));
                cell.HorizontalAlignment = Element.ALIGN_LEFT;
                cell.Border = 0;
                tableFooterNote.AddCell(cell);
            }

            cell = new PdfPCell(new Phrase("\nConditions Applicable to the Policy", bodyFont3_bold));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tableFooterNote.AddCell(cell);

            cell = new PdfPCell(new Phrase("\n1. This policy is issued subject to that all rights reserved with SLIC to decide the continuity of insurance cover or review premium terms and conditions applicable after scrutinizing the claims experience and any other adverse features in respect of each peril insured herein under.\n\n", bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_LEFT;
            cell.Border = 0;
            tableFooterNote.AddCell(cell);

            if (PROP_TYPE == "1" || PROP_TYPE == "3")
            {
               
                if (!string.IsNullOrEmpty(dh_conditions))
                {
                    cell = new PdfPCell(new Phrase("2. " + dh_conditions+"\n\n", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    tableFooterNote.AddCell(cell);
                }
            }
            //changes 10032022--- for Q13>>
            if (dh_aff_flood == "0" && Flood_cover == "Y" && DH_FINAL_FLAG == "N")
            {
                if (PROP_TYPE == "1" || PROP_TYPE == "3")
                {
                    string numVal = "";
                    if (string.IsNullOrEmpty(dh_conditions)){ numVal = "2. "; }
                    else { numVal = "3. "; }
                    string lblQ13 = "This policy is issued subject to there were no reported flood losses for the last 5 years with effect from the commencement date of this policy, and SLIC reserves all rights to repudiate flood claims if there is a discrepancy in declared flood claim history in the online proposal form.";
                    cell = new PdfPCell(new Phrase(numVal + lblQ13 + "\n\n", bodyFont2));
                    cell.HorizontalAlignment = Element.ALIGN_LEFT;
                    cell.Border = 0;
                    tableFooterNote.AddCell(cell);
                }

              
            }

            //----end---->>>>
            // NSB chnages---->>
            string Bemail = BANK_EMAIL;
            cell = new PdfPCell(new Phrase("Note -\n1. If any deviations in respect of construction details mentioned above, such changes should be emailed to "+ Bemail + " within 21 days from the date of policy commencement. If not, we will consider construction details of the proposed building fall into categories as mentioned under construction details listed above.", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            cell.Padding = 0;
            tableFooterNote.AddCell(cell);

            if (PROP_TYPE == "1")
            {
                cell = new PdfPCell(new Phrase("\n2. We advise you to insure your building / solar panel for the new replacement value (Reinstatement basis) in order to avoid reduction for under insurance at the time of a claim.", bodyFont2_bold));
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.Border = 0;
                cell.Padding = 0;
                tableFooterNote.AddCell(cell);
            }
            else if (PROP_TYPE == "3")
            {
                cell = new PdfPCell(new Phrase("", bodyFont2_bold));
                cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
                cell.Border = 0;
                cell.Padding = 0;
                tableFooterNote.AddCell(cell);
            }

                cell = new PdfPCell(new Phrase("\nIn witness where of the Undersigned being duly authorized by the " +
                                      "Insurers and on behalf of the Insures has(have) hereunder set his(their) hands.", bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            cell.Padding = 0;
            tableFooterNote.AddCell(cell);
            //--->>>
            //----------------------------------------------

            //cell = new PdfPCell(new Phrase("", bodyFont2));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //cell.Padding = 5;
            //tableFooter5.AddCell(cell);
            //------------------------------------------>>>>>


            //int[] colWidth4 = { 50, 50 };

            //PdfPTable tableFooter3 = new PdfPTable(2);

            //tableFooter3.SetWidths(colWidth4);

            //tableFooter3.WidthPercentage = 100;
            //tableFooter3.HorizontalAlignment = Element.ALIGN_CENTER;
            //tableFooter3.SpacingBefore = 10;
            //tableFooter3.SpacingAfter = 0;
            //tableFooter3.DefaultCell.Border = 0;


            //cell = new PdfPCell(new Phrase("FIRE UNDERWRITING DEPARTMENT.\nSRI LANKA INSURANCE CORPORATION LIMITED.", bodyFont2_bold));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //cell.Padding = 0;
            //tableFooter3.AddCell(cell);

            ////date of issue------------>>

            //cell = new PdfPCell(new Phrase("", bodyFont2));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //cell.Padding = 0;
            //tableFooter3.AddCell(cell);

            ////string strDate = DateTime.Now.ToString("dddd, MMMM dd yyyy");

            //cell = new PdfPCell(new Phrase("\nDate of issued :" + strDate, bodyFont2));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //cell.Padding = 0;
            //tableFooter3.AddCell(cell);
            //cell = new PdfPCell(new Phrase("", bodyFont2));
            //cell.HorizontalAlignment = Element.ALIGN_LEFT;
            //cell.Border = 0;
            //cell.Padding = 0;
            //tableFooter3.AddCell(cell);

            int[] colWidth6 = { 100 };

            PdfPTable tableAutho = new PdfPTable(1);

            tableAutho.SetWidths(colWidth6);

            tableAutho.WidthPercentage = 100;
            tableAutho.HorizontalAlignment = Element.ALIGN_CENTER;
            tableAutho.SpacingBefore = 10;
            tableAutho.SpacingAfter = 0;
            tableAutho.DefaultCell.Border = 10;
            PdfPCell cellAuth1 = new PdfPCell(new Phrase("FIRE UNDERWRITING DEPARTMENT.\nSRI LANKA INSURANCE CORPORATION GENERAL LIMITED.", bodyFont2_bold));
            cellAuth1.HorizontalAlignment = 0;
            cellAuth1.Border = 0;
            cellAuth1.HorizontalAlignment = Element.ALIGN_LEFT;


            string strDate = DateTime.Now.ToString("dddd, MMMM dd yyyy");

            PdfPCell cellAuth12 = new PdfPCell(new Phrase("Date of issued :" + strDate, bodyFont2));
            cellAuth12.HorizontalAlignment = 0;
            cellAuth12.Border = 0;
            cellAuth12.HorizontalAlignment = Element.ALIGN_LEFT;


            PdfPCell cellAuth13 = new PdfPCell(new Phrase("This is a system generated print and therefore requires no authorized signature.", bodyFont2));
            cellAuth13.HorizontalAlignment = 0;
            cellAuth13.Border = 0;
            cellAuth13.HorizontalAlignment = Element.ALIGN_LEFT;

            //cell.BorderColor = BaseColor.BLACK;
            cellAuth1.Border = Rectangle.BOX;
            cellAuth1.BorderWidth = 0;
            //cellFooter.DisableBorderSide(Rectangle.RIGHT_BORDER);
            cellAuth1.DisableBorderSide(Rectangle.BOTTOM_BORDER);
            cellAuth1.Padding = 0;
            cellAuth12.Padding = 0;
            cellAuth13.Padding = 0;
            tableAutho.AddCell(cellAuth1);
            tableAutho.AddCell(cellAuth12);
            tableAutho.AddCell(cellAuth13);

            //----------->>>Endorsment for solar only------------------------------------->>

            int[] colWidthSolarEndo = { 100 };

            PdfPTable tableSolarEndo = new PdfPTable(1);

            tableAutho.SetWidths(colWidthSolarEndo);

            tableSolarEndo.WidthPercentage = 100;
            tableSolarEndo.HorizontalAlignment = Element.ALIGN_CENTER;
            tableSolarEndo.SpacingBefore = 10;
            tableSolarEndo.SpacingAfter = 0;
            tableSolarEndo.DefaultCell.Border = 10;

            //cell = new PdfPCell(new Phrase("Annexure A\n\n", bodyFont3_bold));
            //cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            //cell.Border = 0;
            //cell.Padding = 0;
            //tableSolarEndo.AddCell(cell);
            cell = new PdfPCell(new Phrase("Policy No:"+ SC_POLICY_NO + "\n\n", bodyFont3_bold));
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            cell.Padding = 0;
            tableSolarEndo.AddCell(cell);

            cell = new PdfPCell(new Phrase("Endorsement for Electrical Inclusion Clause (without burn marks)\n\n", bodyFont2_bold_und));
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            cell.Padding = 0;
            tableSolarEndo.AddCell(cell);

            string Endophrase1 = "In consideration the payment of an additional premium, it is hereby declared and agreed, loss or damage by fire to the electrical appliances and installation insured by item ……. of this policy arising from or occasioned by overrunning, excessive pressure short-circuiting, arcing, self heating or leakage of electricity, from whatever cause (lightning included) is covered subject to the terms and conditions of this policy, but it is expressly understood that no liability exists under this policy for loss or damage to any electrical machine, apparatus, fixture or fitting or to any portion of the electrical installation, unless caused by fire or lightning. Burden of proof of the cause of loss lies with the insured where there are no visible fire marks.\n\n";

            string Endophrase2 = "Basis of Settlement of Claims\n\n";

            string Endophrase3 = "a) In cases where damage to an insured item can  be repaired, the Insurers shall pay expenses necessarily incurred to restore the damaged item to its former state of serviceability plus the cost of dismantling and re-erection incurred for the purpose of effecting the repairs as well as ordinary freight to and from a repair shop, customs duties and dues, if any, to the extent such expenses have been included in the sum insured. If the repairs are executed at a workshop owned by the Insured, the Insurers shall pay the cost of materials and wages incurred for the purpose of the repairs plus a reasonable percentage to cover overhead charges. No deduction shall be made for depreciation in respect of parts replaced, but the value of any salvage shall be taken into account.\n\n" +

    "If the costs of repairs as detailed herein above equal or exceed the actual value of the insured items immediately before the occurrence of the damage, the settlement shall be made on the basis provide for in (b) below.\n\n" +

    "b) In case where an insured item is destroyed, the Insurers shall pay the actual value of the item immediately before the occurrence of the loss, including costs for ordinary freight, erection, customs duties and dues. If any, to the extent such expenses have been included in the sum insured, such actual value to be calculated by deducting proper depreciation from the replacement value of the items. The Insurers shall also pay any normal charges for the dismantling of the item destroyed but the value of any salvage shall be taken into account. The destroyed item shall no longer be covered under this policy and all necessary data on the relevant substitute item shall be indicated for its inclusion in the schedule.\n\n" +

    "Any extra charges incurred for overtime, night work, work on public holidays or express freight shall be covered by this insurance only if especially agreed in writing.\n\n" +

    "The costs of any alternation, additions, improvements or overhauls, shall not be recoverable under this policy.\n\n" +

    "The costs of any provisional repairs shall be borne by the Insurer if such repairs constitute part of the final repairs and do not increase the total repair expenses.\n\n" +

    "The Insurer shall make payments only after being satisfied by production of the necessary bills and documents that the repairs have been effected or replacement has taken place, as the case may be.\n\n\n\n\n\n";


            string Endophrase4 = "Manager,\n\n" +
    "Fire Insurance (Underwriting) Department,\n\n" +
    "Sri Lanka Insurance Corporation General Limited.";

            cell = new PdfPCell(new Phrase(Endophrase1, bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            cell.Padding = 0;
            tableSolarEndo.AddCell(cell);

            cell = new PdfPCell(new Phrase(Endophrase2, bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            cell.Padding = 0;
            tableSolarEndo.AddCell(cell);

            cell = new PdfPCell(new Phrase(Endophrase3, bodyFont2));
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            cell.Padding = 0;
            tableSolarEndo.AddCell(cell);

            cell = new PdfPCell(new Phrase(Endophrase4, bodyFont2_bold));
            cell.HorizontalAlignment = Element.ALIGN_JUSTIFIED;
            cell.Border = 0;
            cell.Padding = 0;
            tableSolarEndo.AddCell(cell);
            //--->>>

            //-----------------------------------------------------------------------------

            // table viewer3---------------------------------------------------------------

            document.Add(tbl15);
            document.Add(tbl14);
            document.Add(tbl16);
            document.Add(tbl17);
            document.Add(tableFooter);
            
            document.Add(CoverTable);
            if (PROP_TYPE == "1")
            {
                document.NewPage();
            }
            document.Add(breakTable);

            document.Add(PremiumTable);
            
            document.Add(DeductTable);
           // document.Add(tableFooter4);
            document.Add(tableFooter5);
            document.Add(tableFooterFinacel);
            document.Add(tableFooterNote);

            //document.Add(tableFooter3);
            document.Add(tableAutho);
            if (PROP_TYPE == "3")
            {
                document.NewPage();
                document.Add(tableSolarEndo);
            }
            //------------end Footer Table----------------------------------------------------


            ///------------pay advice print-----------------------------------------------------------

       

            document.Close();

            //output.Position = 0;
            Response.Buffer = false;
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();


            Response.ContentType = "application/pdf";
           
            Response.AddHeader("Content-Disposition", string.Format("inline;filename=Fire_Policy_Schedule.pdf"));
            Response.BinaryWrite(output.ToArray());



        }
        catch (Exception ex) {

            var endc = new EncryptDecrypt();
    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

        }


    }
    // Method to add single cell to the header  
    private static void AddCellToHeader(PdfPTable tableLayout, string cellText)
    {
        tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.TIMES_ROMAN, 8, 1, Color.WHITE)))
        {
            HorizontalAlignment = Element.ALIGN_RIGHT,
            Padding = 3,
            Border = 0,
            BackgroundColor = Color.DARK_GRAY
        });
    }

    private static void AddCellToHeader2(PdfPTable tableLayout, string cellText)
    {
        tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.TIMES_ROMAN, 8, 1, Color.WHITE)))
        {
            HorizontalAlignment = Element.ALIGN_LEFT,
            Padding = 3,
            Border = 0,
            BackgroundColor = Color.DARK_GRAY
        });
    }
    private static void AddCellToHeader3(PdfPTable tableLayout, string cellText)
    {
        tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.TIMES_ROMAN, 8, 1, Color.WHITE)))
        {
            HorizontalAlignment = Element.ALIGN_CENTER,
            Padding = 3,
            Border = 0,
            BackgroundColor = Color.DARK_GRAY
        });
    }
    // Method to add single cell to the body  
    private static void AddCellToBody(PdfPTable tableLayout, string cellText)
    {
        tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.TIMES_ROMAN, 7, 1, Color.BLACK)))
        {
            HorizontalAlignment = Element.ALIGN_RIGHT,
            Padding = 3,
            Border = 0,
            BackgroundColor = Color.WHITE


        });
    }
    private static void AddCellToBody2(PdfPTable tableLayout, string cellText)
    {
        tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.TIMES_ROMAN, 7, 1, Color.BLACK)))
        {
            HorizontalAlignment = Element.ALIGN_LEFT,
            Padding = 3,
            Border = 0,
            BackgroundColor = Color.WHITE


        });
    }
    private static void AddCellToBody3(PdfPTable tableLayout, string cellText)
    {
        tableLayout.AddCell(new PdfPCell(new Phrase(cellText, new Font(Font.TIMES_ROMAN, 7, 1, Color.BLACK)))
        {
            HorizontalAlignment = Element.ALIGN_CENTER,
            Padding = 3,
            Border = 0,
            BackgroundColor = Color.WHITE


        });
    }
    protected string ReplaceSpace(string txt)
    {
        //String r = "[&160#;]";
        string inputString = txt;
        Regex re = new Regex("&nbsp;");
        string outputString = re.Replace(inputString, "");
        return outputString;

    }

    protected void btPDF_Click(object sender, EventArgs e)
    {
        this.PrintPdf();
    }
    protected void btCat_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Bank_Fire/proposalTypes.aspx", false);
    }
    protected void btPayAdvance_Click(object sender, EventArgs e)
    {
        bool rtnSlip = false;

        if (Session["bank_code"].ToString() == "7010" || Session["bank_code"].ToString() == "7135" || Session["bank_code"].ToString() == "7755" || Session["bank_code"].ToString() == "7719")
        {

            rtnSlip = true;
            

        }      
        else { rtnSlip = false; }

        if (rtnSlip)
        {
            //btPDF.Visible = true;
            PrintSlipPdf pdf = new PrintSlipPdf();
            pdf.print_PaySlip(hfRefId.Value);
        }
        else { }
    
       
    }

    protected void btdebit_Click(object sender, EventArgs e)
    {
        var endc = new EncryptDecrypt();

        // Retrieve the values from the hidden fields
        string debitNo = hfDebitNo.Value;
        string PaymentDate = hfPaymentDate.Value;
        string BANCGI = hfBANGICODE.Value;
        string dh_bbrcode = branchNameCode.InnerHtml;
        string refN = hfRefId.Value;

        // Encrypt the values and redirect
        Response.Redirect("DebitNote.aspx?Data=" + endc.Encrypt(debitNo) + "&PaymentDate=" + endc.Encrypt(PaymentDate) + "&BANCGI=" + endc.Encrypt(BANCGI) + "&dh_bbrcode=" + endc.Encrypt(branchNameCode.InnerHtml) + "&refN=" + endc.Encrypt(refN), false);
    }

    protected void GetAgentDetails(string agent_code)
    {
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetBankCodebyAgent(agent_code), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {

                    agentCode = details.Rows[0]["BANK_CODE"].ToString();
                    agentName = details.Rows[0]["AGE_NAME"].ToString();
                    BGI = details.Rows[0]["BANCASS_GI"].ToString();
                    BANK_ACC = details.Rows[0]["BANK_ACC"].ToString();
                    BANK_EMAIL = details.Rows[0]["BANK_EMAIL"].ToString();

                }
                else
                {

                    string msg = "Message : No Records found. Dated on " + System.DateTime.Now.ToString();
                    var endc = new EncryptDecrypt();
                }
            }
            else
            {
            }
        }
        catch (Exception ex)
        {
        }
    }
}