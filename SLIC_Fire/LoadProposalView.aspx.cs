using System;
using System.Web.UI;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Collections.Generic;
using System.Configuration;

public partial class SLIC_Fire_LoadProposalView : System.Web.UI.Page
{
    private string FromEmailid = ConfigurationManager.AppSettings["senderEmailId"];
    private string FromEmailPasscode = ConfigurationManager.AppSettings["senderEmailPasscode"];
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    Insert_class exe_in = new Insert_class();
    Update_class exe_up = new Update_class();
    Insert_class insert_ = new Insert_class();
    string Auth = string.Empty;
    int Resultcount = 1;
    string agentCode, agentName, BGI, BANK_ACC = string.Empty;
    string overallFlag = "";
    public string prinPolicyNumber = string.Empty;
    string agent_code = string.Empty;
    GetProposalDetails getPropClass = new GetProposalDetails();
    ORCL_Connection orcl_con = new ORCL_Connection();
    InsertBoardraux _boardraux = new InsertBoardraux();
    AS400_Transaction Getas400 = new AS400_Transaction();
    DetailsForfireEmailReq emailSend = new DetailsForfireEmailReq();
    List<String> emailID = new List<String>();
    List<String> emailIDForCC = new List<String>();
    CoopWebORA coop = new CoopWebORA();


    string bancass_email = string.Empty;

    public string dh_bcode, dh_bbrcode, dh_name, dh_agecode, dh_agename, dh_nic, dh_br,
       dh_padd1, dh_padd2, dh_padd3, dh_padd4, dh_phone, dh_email, dh_iadd1, dh_iadd2, dh_iadd3,
       dh_iadd4, dh_pfrom, dh_pto, dh_uconstr, dh_occu_car, dh_occ_yes_reas, dh_haz_occu, dh_haz_yes_rea,
       dh_valu_build, dh_valu_wall,
       dh_valu_total, dh_aff_flood, dh_aff_yes_reas, dh_wbrick,
       dh_wcement, dh_dwooden, dh_dmetal, dh_ftile, dh_fcement, dh_rtile,
       dh_rasbes, dh_rgi, dh_rconcreat, dh_cov_fire,
       dh_cov_light, dh_cov_flood, dh_cfwateravl, dh_cfyesr1, dh_cfyesr2, dh_cfyesr3, dh_cfyesr4,
       dh_entered_by, dh_entered_on, dh_hold, DH_NO_OF_FLOORS, DH_OVER_VAL, DH_FINAL_FLAG,
       dh_isreq, dh_conditions, dh_isreject, dh_iscodi, dh_bcode_id, dh_bbrcode_id, DH_LOADING, DH_LOADING_VAL, LAND_PHONE, DH_VAL_BANKFAC, dh_deductible, dh_deductible_pre,
       TERM, Period, Fire_cover, Other_cover, SRCC_cover, TC_cover, Flood_cover,
        BANK_UPDATED_BY, BANK_UPDATED_ON, PROP_TYPE, DH_SOLAR_SUM, SOLAR_REPAIRE,
       SOLAR_PARTS, SOLAR_ORIGIN, SOLAR_SERIAL, Solar_Period, LOAN_NUMBER = string.Empty;

    public string SC_POLICY_NO, SC_SUM_INSU, SC_NET_PRE,
            SC_RCC, SC_TR, SC_ADMIN_FEE,
            SC_POLICY_FEE, SC_NBT, SC_VAT, SC_TOTAL_PAY, CREATED_ON, CREATED_BY,
            FLAG, SC_Renewal_FEE, BPF_FEE, DEBIT_NO = string.Empty;

    public string BANK, RateTerm = string.Empty;
    public double sumInsu, BASIC, RCC, TR, ADMIN_FEE, POLICY_FEE, RENEWAL_FEE, DISCOUNT_RATE, BASIC_2 = 0;

    public double CalNetPre, Cal_RCC, Cal_TR, Cal_ADMIN_FEE, Cal_POLICY_FEE, Cal_NBT, Cal_VAT, Cal_Total, Cal_Renewal_Fee = 0;
    public double CalNetPreTemp, Cal_RCCTemp, Cal_TRTemp, Cal_ADMIN_FEETemp, Cal_POLICY_FEETemp, Cal_NBTTemp, Cal_VATTemp, Cal_TotalTemp, Cal_Renewal_FeeTemp = 0;

    public double newNet = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        //scriptManager.RegisterPostBackControl(this.ImageButton8);

        // ((MainMaster)Master).slected_manu.Value = "appReq";

        if (!Page.IsPostBack)
        {
            try
            {
                Auth = "admin";//Session["Auth_Code"].ToString();

                if (Auth == "admin")
                {

                    if (Session["UserId"].ToString() != "")
                    {
                        var en = new EncryptDecrypt();
                        UserId.Value = Session["UserId"].ToString();
                        brCode.Value = Session["brcode"].ToString();
                        hfRefId.Value = en.Decrypt(Request.QueryString["REQ_ID"]).Trim().ToString();
                        hfFlag.Value = en.Decrypt(Request.QueryString["V_FLAG"]).ToString().Trim();
                        hfSumInsu.Value = en.Decrypt(Request.QueryString["SUM_INSU"]).ToString().Trim();
                        this.disableFields();
                        wording1.Visible = false;
                        this.FillSchedule();
                        trcondi001.Visible = false;
                       
                        idApp.Visible = false;
                        idCondi.Visible = false;
                        idLoading.Visible = false;
                        idReject.Visible = false;
                        btLoading.Visible = false;
                        idDedc.Visible = false;
                        btDeductApply.Visible = false;

                        //trcodition.Visible= false;

                        
                        
                    }


                    else
                    {
                        var endc = new EncryptDecrypt();

                        Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("SE".ToString()));
                    }

                }
                else
                {
                    string msg = "You are not authorized to access this system.";


                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()));

                }


            }
            catch (Exception ex)
            {
              
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
            }
        }
        else
        {
            /* IF PAGE.POSTBACK ASSIGHEN SESSION VARIABLE */
            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;
        }


    }



    public void FillSchedule()
    {

        try
        {
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
    out dh_isreq, out dh_conditions, out dh_isreject, out dh_iscodi, out dh_bcode_id, out dh_bbrcode_id, out DH_LOADING, out DH_LOADING_VAL, out LAND_PHONE, out DH_VAL_BANKFAC, out dh_deductible, out dh_deductible_pre,
    out TERM, out Period, out Fire_cover, out Other_cover, out SRCC_cover, out TC_cover, out Flood_cover,
    out BANK_UPDATED_BY, out BANK_UPDATED_ON, out PROP_TYPE, out DH_SOLAR_SUM, out SOLAR_REPAIRE,
    out SOLAR_PARTS, out SOLAR_ORIGIN, out SOLAR_SERIAL, out Solar_Period, out LOAN_NUMBER);

            this.getPropClass.GetSchedualCalValues(hfRefId.Value, out SC_POLICY_NO, out SC_SUM_INSU, out SC_NET_PRE,
                        out SC_RCC, out SC_TR, out SC_ADMIN_FEE,
                        out SC_POLICY_FEE, out SC_NBT, out SC_VAT, out SC_TOTAL_PAY, out CREATED_ON, out CREATED_BY,
                        out FLAG, out SC_Renewal_FEE,out BPF_FEE, out DEBIT_NO);


            txtTerm.Text = TERM;
            txtPeriod.Text = Period;
            txtnetpremium.Text = SC_NET_PRE;
            txtRCC.Text = SC_RCC;
            txtTR.Text = SC_TR;
            txtRenewal.Text = SC_Renewal_FEE;
            txtAdmin.Text = SC_ADMIN_FEE;
            txtPolicyFee.Text = SC_POLICY_FEE;


            hfbankcode.Value = dh_bcode_id;
            hfbankUN.Value = dh_entered_by;
            txt_nameOfProp.Text = dh_name;
            txt_nic.Text = dh_nic;
            txt_br.Text = dh_br;
            txt_addline1.Text = dh_padd1;
            txt_addline2.Text = dh_padd2;
            txt_addline3.Text = dh_padd3;
            txt_addline4.Text = dh_padd4;
            //if (string.IsNullOrEmpty(dh_padd4)) { txt_addline4.Text = "0"; } else { txt_addline4.Text = dh_padd4; }
            txt_tele.Text = dh_phone;
            txt_landLine.Text = LAND_PHONE;
            txt_email.Text = dh_email;
            txt_dweAdd1.Text = dh_iadd1;
            txt_dweAdd2.Text = dh_iadd2;
            txt_dweAdd3.Text = dh_iadd3;
            txt_dweAdd4.Text = dh_iadd4;
            //if (string.IsNullOrEmpty(dh_padd4)) { txt_dweAdd4.Text = "0"; } else { txt_dweAdd4.Text = dh_iadd4; }
            txt_fromDate.Text = dh_pfrom;
            txt_toDate.Text = dh_pto;
            txtagent.Text = dh_agecode;
            if (dh_bcode_id.ToString().Trim() == "7719")
            {
                Div46.Attributes.Add("style", "display:normal"); /*txtLoanNumber.Text = "";*/
                                                                 //bank_code.Value = "7719";
                termAnual.Enabled = false;
                termAnual.Attributes.Add("style", "display:none");
            }
            else { Div46.Attributes.Add("style", "display:none"); txtLoanNumber.Text = ""; /*bank_code.Value = "";*/ }
            //---end---->
            constructDetails.Visible = false;
            txtLoanNumber.Text = LOAN_NUMBER;



            txtSoloarModel.Text = SOLAR_SERIAL;
            txtSolarCountry.Text = SOLAR_ORIGIN;
            if (SOLAR_PARTS == "0")
            {
                rbSolTwo.SelectedIndex = 1;
                rbSolTwo.Items[1].Attributes.Add("style", "color: #f0a454;");

            }
            else
            {
                rbSolTwo.SelectedIndex = 0;
                rbSolTwo.Items[0].Attributes.Add("style", "color: #f0a454;");

            }

            if (SOLAR_REPAIRE == "0")
            {
                rbSolOne.SelectedIndex = 1;
                rbSolOne.Items[1].Attributes.Add("style", "color: #f0a454;");

            }
            else
            {
                rbSolOne.SelectedIndex = 0;
                rbSolOne.Items[0].Attributes.Add("style", "color: #f0a454;");

            }
            txt_solar.Text = DH_SOLAR_SUM;


            if (PROP_TYPE == "1")
            {
                txtHeading.InnerHtml = "Private Dwelling House";
                //this.InitializedListDistrict(ddlNumberOfYears, "n", "n", this._sql.GetNumbers(25), "'n'");
                Div19.Attributes.Add("style", "display:normal"); //building
                Div20.Attributes.Add("style", "display:normal"); //wall
                Div20I.Attributes.Add("style", "display:none");//solar


                // below mentions for solar
                Div37.Attributes.Add("style", "display:none");
                Div34.Attributes.Add("style", "display:none");
                Div35.Attributes.Add("style", "display:none");
                Div36.Attributes.Add("style", "display:none");
                Div43.Attributes.Add("style", "display:normal");
            }
            else if (PROP_TYPE == "2")
            {
                txtHeading.InnerHtml = "Private Dwelling House & Solar Panel System";
                //this.InitializedListDistrict(ddlNumberOfYears, "n", "n", this._sql.GetNumbers(5), "'n'");
                Div19.Attributes.Add("style", "display:normal"); //building
                Div20.Attributes.Add("style", "display:normal"); //wall
                Div20I.Attributes.Add("style", "display:normal"); //solar
                                                                  // below mentions for solar
                Div37.Attributes.Add("style", "display:normal");
                Div34.Attributes.Add("style", "display:normal");
                Div35.Attributes.Add("style", "display:normal");
                Div36.Attributes.Add("style", "display:normal");
                Div43.Attributes.Add("style", "display:normal");
            }
            else if (PROP_TYPE == "3")
            {
                txtHeading.InnerHtml = "Solar Panel System";
                //this.InitializedListDistrict(ddlNumberOfYears, "n", "n", this._sql.GetNumbers(5), "'n'");
                Div19.Attributes.Add("style", "display:none"); //building
                Div20.Attributes.Add("style", "display:none"); //wall
                Div20I.Attributes.Add("style", "display:normal"); //solar
                                                                  // below mentions for solar
                Div37.Attributes.Add("style", "display:normal");
                Div34.Attributes.Add("style", "display:normal");
                Div35.Attributes.Add("style", "display:normal");
                Div36.Attributes.Add("style", "display:normal");
                Div43.Attributes.Add("style", "display:normal");
            }
            else
            {
                txtHeading.InnerHtml = "Private Dwelling House";
                //this.InitializedListDistrict(ddlNumberOfYears, "n", "n", this._sql.GetNumbers(25), "'n'");
                Div19.Attributes.Add("style", "display:normal"); //building
                Div20.Attributes.Add("style", "display:normal"); //wall
                Div20I.Attributes.Add("style", "display:none"); //solar
                                                                // below mentions for solar
                Div37.Attributes.Add("style", "display:none");
                Div34.Attributes.Add("style", "display:none");
                Div35.Attributes.Add("style", "display:none");
                Div36.Attributes.Add("style", "display:none");
                Div43.Attributes.Add("style", "display:normal");
            }






            if (dh_uconstr == "0")
            {

                chkl1.SelectedIndex = 1;
                chkl1.Items[1].Attributes.Add("style", "color: #f0a454;");
            }
            else
            {
                chkl1.SelectedIndex = 0;
                chkl1.Items[0].Attributes.Add("style", "color: #f0a454;");
            }




            txt_sumInsu1.Text = dh_valu_build;
            txt_sumInsu2.Text = dh_valu_wall;
            txt_sumInsuTotal.Text = dh_valu_total;

            txt_bankVal.Text = DH_VAL_BANKFAC;

            if (dh_aff_flood == "0")
            {
                chkl4.SelectedIndex = 1;
                chkl4.Items[1].Attributes.Add("style", "color: #f0a454;");

            }
            else
            {
                chkl4.SelectedIndex = 0;
                chkl4.Items[0].Attributes.Add("style", "color: #f0a454;");
                txt_ninethReason.Text = dh_aff_yes_reas;
            }

            txtNoofFloors.Text = DH_NO_OF_FLOORS;
            if (dh_wbrick == "1")
            {
                Chkbrick.Checked = true;
            }
            else { Chkbrick.Checked = false; }



            if (dh_wcement == "1")
            {
                Chkcement.Checked = true;
            }
            else { Chkcement.Checked = false; }


            if (dh_dwooden == "1")
            {
                ChkWooden.Checked = true;
            }
            else { ChkWooden.Checked = false; }


            if (dh_dmetal == "1")
            {
                ChkMetal.Checked = true;
            }
            else { ChkMetal.Checked = false; }

            if (dh_ftile == "1")
            {
                Chktile.Checked = true;
            }
            else { Chktile.Checked = false; }

            if (dh_fcement == "1")
            {
                ChkFloorcement.Checked = true;
            }
            else { ChkFloorcement.Checked = false; }

            if (dh_rtile == "1")
            {
                Chekrooftile.Checked = true;
            }
            else { Chekrooftile.Checked = false; }


            if (dh_rasbes == "1")
            {
                Chasbastos.Checked = true;
            }
            else { Chasbastos.Checked = false; }

            if (dh_rgi == "1")
            {
                ChkGI.Checked = true;
            }
            else { ChkGI.Checked = false; }

            if (dh_rconcreat == "1")
            {
                Chkconcreat.Checked = true;
            }
            else { Chkconcreat.Checked = false; }


            if (dh_cov_fire == "1")
            {
                rbfire.Checked = true;
            }
            else { rbfire.Checked = false; }

            if (dh_cov_light == "1")
            {
                rblighting.Checked = true;
            }
            else { rblighting.Checked = false; }


            if (dh_cov_flood == "1")
            {
                rbflood.Checked = true;

                if (dh_cfwateravl == "0")
                {
                    chkl5.SelectedIndex = 1;
                    chkl5.Items[1].Attributes.Add("style", "color: #f0a454;");

                }
                else
                {

                    chkl5.SelectedIndex = 0;
                    chkl5.Items[0].Attributes.Add("style", "color: #f0a454;");
                    txt_wordReason1.Text = dh_cfyesr1;

                    //----Q.16 --2,3,4


                    if (dh_cfyesr2 == "0")
                    {
                        txt_wordReason2.SelectedIndex = 1;
                        txt_wordReason2.Items[1].Attributes.Add("style", "color: #f0a454;");
                    }
                    else
                    {
                        txt_wordReason2.SelectedIndex = 0;
                        txt_wordReason2.Items[0].Attributes.Add("style", "color: #f0a454;");
                    }
                    if (dh_cfyesr3 == "0")
                    {
                        txt_wordReason3.SelectedIndex = 1;
                        txt_wordReason3.Items[1].Attributes.Add("style", "color: #f0a454;");
                    }
                    else
                    {
                        txt_wordReason3.SelectedIndex = 0;
                        txt_wordReason3.Items[0].Attributes.Add("style", "color: #f0a454;");
                    }
                    if (dh_cfyesr4 == "0")
                    {
                        txt_wordReason4.SelectedIndex = 1;
                        txt_wordReason4.Items[1].Attributes.Add("style", "color: #f0a454;");
                    }
                    else
                    {
                        txt_wordReason4.SelectedIndex = 0;
                        txt_wordReason4.Items[0].Attributes.Add("style", "color: #f0a454;");
                    }
                    //-------

                    //txt_wordReason2.Text = dh_cfyesr2;
                    //txt_wordReason3.Text = dh_cfyesr3;
                    //txt_wordReason4.Text = dh_cfyesr4;
                }

            }
            else
            {
                rbflood.Checked = false;
                rbflood2.Checked = true;
            }



            if (Convert.ToDouble(Period) != 0)
            {
                DivTerm.Attributes.Add("style", "display: normal;");
                ddlNumberOfYears.Text = Period;

            }
            else
            {
                DivTerm.Attributes.Add("style", "display: none;");
                ddlNumberOfYears.Text = Period;
            }


            if (TERM == "1")
            {
                RbTermType.SelectedValue = "1";
                RbTermType.Items[0].Attributes.Add("style", "color: #f0a454;");

            }
            else
            {
                RbTermType.SelectedValue = "0";
                RbTermType.Items[1].Attributes.Add("style", "color: #f0a454;");
            }
            if (Fire_cover == "Y")
            {
                chkFirLight.Checked = true;
            }
            else
            {
                chkFirLight.Checked = false;
            }
            if (Other_cover == "Y")
            {
                chkOtherPerils.Checked = true;
            }
            else
            {
                chkOtherPerils.Checked = false;
            }
            if (SRCC_cover == "Y")
            {
                chkSRCC.Checked = true;
            }
            else
            {
                chkSRCC.Checked = false;
            }
            if (TC_cover == "Y")
            {
                chkTR.Checked = true;
            }
            else
            {
                chkTR.Checked = false;
            }


            //conditions-------------------->>>>
             if (DH_FINAL_FLAG == "Y")
            {
                lblreasonforhold.Visible = true;
                lblapproved.Visible = false;
                lblInterinChanges.Visible = false;
                if (PROP_TYPE == "1")
                {
                    if (dh_hold == "Y") { lblreasonforhold.Text = "Private dwelling house (Fire) - Flood cover requested by customer."; } else { }
                    if (DH_OVER_VAL == "Y") { lblreasonforhold.Text = "Private dwelling house (Fire) - Sum insured more than Rs. 30,000,000/-"; } else { }
                    if ((DH_OVER_VAL == "Y") && (dh_hold == "Y")) { lblreasonforhold.Text = "Private dwelling house (Fire) - Sum insured more than Rs. 30,000,000/- and flood cover requested by customer."; } else { }
                }
                else if (PROP_TYPE == "3")
                {
                    if (dh_hold == "Y") { lblreasonforhold.Text = "Private dwelling house (Solar) - Flood cover requested by customer."; } else { }
                    if (DH_OVER_VAL == "Y") { lblreasonforhold.Text = "Private dwelling house (Solar) - Sum insured more than Rs. 5,000,000/-"; } else { }
                    if ((DH_OVER_VAL == "Y") && (dh_hold == "Y")) { lblreasonforhold.Text = "Private dwelling house (Solar) - Sum insured more than Rs. 5,000,000/- and flood cover requested by customer."; } else { }
                }
                else { }


                if (Session["AdminForFire"].ToString() == "FireAdmin")
                {
                    btnDone.Visible = true;
                    //btnProceed.Visible = true;
                    //btreject.Visible = true;
                    //btconditions.Visible = true;
                    ddl_options.Visible = true;
                    wording1.Visible = true;
                    btViewSchhedule.Visible = false;
                    btback.Visible = false;
                    btbackReject.Visible = false;
                    btChangeDeduct.Visible = false;
                }

                else {
                    btnDone.Visible = true;
                    //btnProceed.Visible = true;
                    //btreject.Visible = true;
                    //btconditions.Visible = true;
                    ddl_options.Visible = false;
                    wording1.Visible = false;
                    btViewSchhedule.Visible = false;
                    btback.Visible = false;
                    btbackReject.Visible = false;
                    btChangeDeduct.Visible = false;
                    btnCancelProp.Visible = false;
                }

            }
            else if (DH_FINAL_FLAG == "N")
            {

                lblapproved.Visible = true;
                lblapproved.Text = "Proposal Scheduled Successfully.!";
                lblInterinChanges.Visible = true;

                if (dh_iscodi == "Y" && DH_LOADING != "Y") { lblInterinChanges.Text = "Interin changes done with conditions."; btChangeDeduct.Visible = false; }
                //else { lblInterinChanges.Visible = false; }

               else if (DH_LOADING == "Y" && dh_iscodi != "Y") { lblInterinChanges.Text = "Interin changes done with Loading value " + DH_LOADING_VAL + "%."; btChangeDeduct.Visible = false; }
                //else { lblInterinChanges.Visible = false; }

               else if (dh_iscodi == "Y" && DH_LOADING == "Y" && string.IsNullOrEmpty(dh_deductible))
                {
                    lblInterinChanges.Text = "Interin changes done with Loading value " + DH_LOADING_VAL + "% and conditions.";
                    btChangeDeduct.Visible = false;
                }

                else if (dh_iscodi == "Y" && DH_LOADING != "Y" && !string.IsNullOrEmpty(dh_deductible))
                {
                    lblInterinChanges.Text = "Interin changes done with conditions and " + dh_deductible_pre + "% and " + dh_deductible + " deducible values."; ;
                    btChangeDeduct.Visible = false;
                }

                else if (dh_iscodi == "Y" && DH_LOADING == "Y" && !string.IsNullOrEmpty(dh_deductible))
                {
                    lblInterinChanges.Text = "Interin changes done with Loading value " + DH_LOADING_VAL + "% , conditions and " + dh_deductible_pre + "% and " + dh_deductible + " deducible values."; ;
                    btChangeDeduct.Visible = false;
                }


                // else { lblInterinChanges.Visible = false; }

                else if (!string.IsNullOrEmpty(dh_deductible)) { lblInterinChanges.Text = "Interin changes done with "+dh_deductible_pre+"% and " + dh_deductible+ " deducible values."; btChangeDeduct.Visible = true; }

               else
                {
                    lblInterinChanges.Visible = false;
                    btChangeDeduct.Visible = false;
                }

                if (Session["AdminForFire"].ToString() == "FireAdmin")
                {

                    lblreasonforhold.Visible = false;
                    btnDone.Visible = false;
                    //btnProceed.Visible = false;
                    //btreject.Visible = false;
                    //btconditions.Visible = false;
                    ddl_options.Visible = false;
                    btViewSchhedule.Visible = true;
                    wording1.Visible = false;
                    btback.Visible = true;
                    btbackReject.Visible = false;
                    //btChangeDeduct.Visible = true;
                }
                else
                {
                    lblreasonforhold.Visible = false;
                    btnDone.Visible = false;
                    //btnProceed.Visible = false;
                    //btreject.Visible = false;
                    //btconditions.Visible = false;
                    ddl_options.Visible = false;
                    btViewSchhedule.Visible = true;
                    wording1.Visible = false;
                    btback.Visible = true;
                    btbackReject.Visible = false;
                    btnCancelProp.Visible = false;
                    //btChangeDeduct.Visible = true;
                }

            }
            else
            {
                lblapproved.Visible = true;
                lblapproved.Text = "&nbsp; Proposal Rejected.&nbsp; ";
                lblapproved.Attributes.Add("style", "color: #7d0606;");
                lblInterinChanges.Visible = true;

                lblInterinChanges.Text = "Proposal Rejected by SLIC."; 
                lblreasonforhold.Visible = false;
                btnDone.Visible = false;
                //btnProceed.Visible = false;
                //btreject.Visible = false;
                //btconditions.Visible = false;
                ddl_options.Visible = false;
                btViewSchhedule.Visible = false;
                wording1.Visible = false;
                btback.Visible = false;
                btbackReject.Visible = true;
                btChangeDeduct.Visible = false;

                if (Session["AdminForFire"].ToString() == "FireAdmin")
                {
                    btnCancelProp.Visible = true;
                }
                else { btnCancelProp.Visible = false; }

                }


            }
        catch (Exception ex)
        {

            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

        }


    }

    public void disableFields()
    {
        txt_nameOfProp.Enabled = false;

        txt_nic.Enabled = false;
        txt_br.Enabled = false;
        txt_addline1.Enabled = false;
        txt_addline2.Enabled = false;
        txt_addline3.Enabled = false;
        txt_addline4.Enabled = false;
        txt_tele.Enabled = false;
        txt_landLine.Enabled = false;
        txt_email.Enabled = false;
        txt_dweAdd1.Enabled = false;
        txt_dweAdd2.Enabled = false;
        txt_dweAdd3.Enabled = false;
        txt_dweAdd4.Enabled = false;
        txt_fromDate.Enabled = false;
        txt_toDate.Enabled = false;
        txt_bankVal.Enabled = false;
        chkl1.Enabled = false;

        txt_sumInsu1.Enabled = false;
        txt_sumInsu2.Enabled = false;
        txt_sumInsuTotal.Enabled = false;

        txtSoloarModel.Enabled = false;
        txtSolarCountry.Enabled = false;
        rbSolTwo.Enabled = false;
        rbSolOne.Enabled = false;
        txt_solar.Enabled = false;


        chkl4.Enabled = false;
        Chkbrick.Enabled = false;
        Chkcement.Enabled = false;
        ChkWooden.Enabled = false;
        ChkMetal.Enabled = false;
        Chktile.Enabled = false;
        ChkFloorcement.Enabled = false;
        Chekrooftile.Enabled = false;
        Chasbastos.Enabled = false;
        ChkGI.Enabled = false;
        Chkconcreat.Enabled = false;
        rbfire.Enabled = false;
        rblighting.Enabled = false;
        rbflood.Enabled = false;
        chkl5.Enabled = false;

        txt_wordReason1.Enabled = false;
        txt_wordReason2.Enabled = false;
        txt_wordReason3.Enabled = false;
        txt_wordReason4.Enabled = false;
        txtNoofFloors.Enabled = false;


        chkSRCC.Enabled = false;
        chkTR.Enabled = false;
        chkOtherPerils.Enabled = false;
        chkFirLight.Enabled = false;
        ddlNumberOfYears.Enabled = false;
        RbTermType.Enabled = false;
        chkSameAdd.Disabled = true;
        txtLoanNumber.Enabled = false;
        // out dh_hold, out DH_NO_OF_FLOORS, out DH_OVER_VAL, out DH_FINAL_FLAG

    }


    protected void btnDone_Click(object sender, EventArgs e)
    {
        Response.Redirect("./Fire_Entered_Policy.aspx", false);
    }

    protected void btViewSchhedule_Click(object sender, EventArgs e)
    {
        try
        {
            string fd_ref = hfRefId.Value.Trim();
            string fd_flag = hfFlag.Value;
            string fd_sum = hfSumInsu.Value;

            // Initialize variables for DEBITSEQ and PAYMENT_DATE
            string Data = string.Empty;
            string BANCGI = string.Empty;
            string PaymentDate = string.Empty;

            // Connection to Oracle database
            using (OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                // Query to fetch DEBITSEQ and PAYMENT_DATE based on SC_REF_NO
                string query = "SELECT DEBITSEQ, BANCAGI, PAYMENT_DATE FROM quotation.fire_dh_schedule_calc WHERE SC_REF_NO = :Ref_no";
                using (OracleCommand cmd = new OracleCommand(query, conn))
                {
                    // Add parameter for SC_REF_NO
                    cmd.Parameters.Add(new OracleParameter("Ref_no", fd_ref));

                    using (OracleDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Assign values from the query result with null checks
                            Data = reader["DEBITSEQ"] != DBNull.Value ? reader["DEBITSEQ"].ToString() : Data;
                            BANCGI = reader["BANCAGI"] != DBNull.Value ? reader["BANCAGI"].ToString() : BANCGI;
                            PaymentDate = reader["PAYMENT_DATE"] != DBNull.Value
                                ? Convert.ToDateTime(reader["PAYMENT_DATE"]).ToString("yyyy-MM-dd")
                                : PaymentDate;
                        }
                    }
                }
            }

            var endc = new EncryptDecrypt();
            Response.Redirect("~/Bank_Fire/PrintSchedule.aspx?Ref_no=" + endc.Encrypt(fd_ref) + "&Flag=" + endc.Encrypt(fd_flag) + "&Data=" + endc.Encrypt(Data) + "&PaymentDate=" + endc.Encrypt(PaymentDate) + "&BANCGI=" + endc.Encrypt(BANCGI) + "&sumInsu=" + endc.Encrypt(fd_sum.ToString()), false);
        }
        catch (Exception ex)
        {

        }
    }

    protected void btback_Click(object sender, EventArgs e)
    {
        Response.Redirect("./ApprovedPolicy.aspx", false);
    }
    protected void btback_Click2(object sender, EventArgs e)
    {
        Response.Redirect("./RejectedPolicy.aspx", false);
    }

    protected void createPolicyNumber()
    {
        string LongShort = "";
        TERM = txtTerm.Text.ToString().Trim();

        if (TERM == "1")
        {
            LongShort = "FFPD";
          
        }
        else
        {
            LongShort = "FPDL";
        }


        string currentMonth = DateTime.Now.ToString("MM");//DateTime.Now.Month.ToString();
        string currentYear = DateTime.Now.ToString("yy");//DateTime.Now.Year.ToString();
        string seq_return = "";

        if (TERM == "1") { LongShort = "FFPD"; } else if (TERM == "0") { LongShort = "FPDL"; } else { LongShort = ""; }





        string seq_id = orcle_trans.GetString(this._sql.GetPolSeq(currentYear, currentMonth, LongShort));

        if (string.IsNullOrEmpty(seq_id))
        {
            string out_word = "";


            //if (IsLongTerm == "N") { LongShort = "FFPD"; } else if(IsLongTerm == "Y") { LongShort = "FPDL"; } else { LongShort = "FFPD"; }

            bool RESULT = insert_.insert_new_seq_for_policyNumber(LongShort, currentYear, currentMonth, "0000000", "", "Y", out out_word);

            string seq_id_second = orcle_trans.GetString(this._sql.GetPolSeq(currentYear, currentMonth, LongShort));

            seq_return = (Convert.ToInt32(seq_id_second) + 1).ToString();

            if (seq_return.Length == 1) { seq_return = "000000" + seq_return; }
            else if (seq_return.Length == 2) { seq_return = "00000" + seq_return; }
            else if (seq_return.Length == 3) { seq_return = "0000" + seq_return; }
            else if (seq_return.Length == 4) { seq_return = "000" + seq_return; }
            else if (seq_return.Length == 5) { seq_return = "00" + seq_return; }
            else if (seq_return.Length == 6) { seq_return = "0" + seq_return; }
            else { seq_return = seq_return; }

            bool result = exe_up.update_policySeqNumber(seq_return, currentYear, currentMonth, LongShort);


        }
        else
        {
            seq_return = (Convert.ToInt32(seq_id) + 1).ToString();

            if (seq_return.Length == 1) { seq_return = "000000" + seq_return; }
            else if (seq_return.Length == 2) { seq_return = "00000" + seq_return; }
            else if (seq_return.Length == 3) { seq_return = "0000" + seq_return; }
            else if (seq_return.Length == 4) { seq_return = "000" + seq_return; }
            else if (seq_return.Length == 5) { seq_return = "00" + seq_return; }
            else if (seq_return.Length == 6) { seq_return = "0" + seq_return; }
            else { seq_return = seq_return; }

            bool result = exe_up.update_policySeqNumber(seq_return, currentYear, currentMonth, LongShort);

        }

        if (TERM == "1") { LongShort = "FFPD"; } else if (TERM == "0") { LongShort = "FPDL"; } else { LongShort = "FFPD"; }
        this.GetAgentDetails(txtagent.Text.ToString().Trim());
        //policy_number.InnerHtml = currentYear + currentMonth + BGI + seq_return;
        prinPolicyNumber = LongShort + currentYear + BGI + seq_return;
        hfPolNo.Value = prinPolicyNumber;
        bool POL_NUM = exe_up.update_policyNumberInCalTable(hfRefId.Value.Trim().ToString(), prinPolicyNumber);
        //boardraux table--------->>

        this.getPropClass.GetSchedualCalValues(hfRefId.Value, out SC_POLICY_NO, out SC_SUM_INSU, out SC_NET_PRE,
                    out SC_RCC, out SC_TR, out SC_ADMIN_FEE,
                    out SC_POLICY_FEE, out SC_NBT, out SC_VAT, out SC_TOTAL_PAY, out CREATED_ON, out CREATED_BY,
                    out FLAG, out SC_Renewal_FEE, out BPF_FEE, out DEBIT_NO);

        double YearsCountSecond = 0; 
        int resultNo = 0;
       
            if (txtPeriod.Text.ToString().Trim() == "0") { YearsCountSecond = 1; } else { YearsCountSecond = Convert.ToDouble(txtPeriod.Text.ToString().Trim()); }
       
        if (TERM != "1")
        {

            _boardraux.insert_Boardraux(prinPolicyNumber, Convert.ToDouble(SC_NET_PRE.ToString().Trim()), txt_fromDate.Text.ToString().Trim(), Convert.ToDouble(SC_RCC.ToString().Trim()),
                Convert.ToDouble(SC_TR.ToString().Trim()), YearsCountSecond, Session["EPFNum"].ToString(),"SLIC", out resultNo);

        }
        else { }

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

                }
                else
                {

                    string msg = "Message : No Records found. Dated on " + System.DateTime.Now.ToString();
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()));
                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));
            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }


  
    protected void ddl_options_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_options.SelectedValue == "0")
        {
            trcondi001.Visible = false;
        }
        else
        {
            trcondi001.Visible = true;
            if(ddl_options.SelectedValue == "1")
            {
                idApp.Visible = true;
                idCondi.Visible = false;
                idLoading.Visible = false;
                idReject.Visible = false;
                btLoading.Visible = false;
                idDedc.Visible = false;
                btnProceedView.Text = "Approve";
                this.FillSchedule();
            }
            else if (ddl_options.SelectedValue == "2")
            {
                idCondi.Visible = true;
                idApp.Visible = false;
                idLoading.Visible = false;
                idReject.Visible = false;
                btLoading.Visible = false;
                idDedc.Visible = false;
                btnProceedView.Text = "Apply Conditions";
                this.FillSchedule();
            }
            else if (ddl_options.SelectedValue == "3")
            {
                idLoading.Visible = true;
                idCondi.Visible = false;
                idApp.Visible = false;
                idReject.Visible = false;
                btLoading.Visible = true;
                idDedc.Visible = false;
                btnProceedView.Text = "Approve Loadings";
                this.FillSchedule();
            }
            else if (ddl_options.SelectedValue == "4")
            {
                idLoading.Visible = true;
                idCondi.Visible = true;
                idApp.Visible = false;
                idReject.Visible = false;
                btLoading.Visible = true;
                idDedc.Visible = false;
                btnProceedView.Text = "Apply Changes";
                this.FillSchedule();
            }
            else if (ddl_options.SelectedValue == "5")
            {
                idLoading.Visible = false;
                idCondi.Visible = false;
                idApp.Visible = false;
                idReject.Visible = true;
                btLoading.Visible = false;
                idDedc.Visible = false;
                btnProceedView.Text = "Reject";
                this.FillSchedule();
            }

            else if (ddl_options.SelectedValue == "6")
            {
                idLoading.Visible = false;
                idCondi.Visible = false;
                idApp.Visible = false;
                idReject.Visible = false;
                btLoading.Visible = false;
                idDedc.Visible = true;
                btnProceedView.Text = "Deducible";
                this.FillSchedule();
            }

            else if (ddl_options.SelectedValue == "7") // condition + deductible
            {

                idCondi.Visible = true;
                idApp.Visible = false;
                idLoading.Visible = false;
                idReject.Visible = false;
                btLoading.Visible = false;
                idDedc.Visible = true;
                btnProceedView.Text = "Apply Conditions and Deducible";

                this.FillSchedule();
            }
            else if (ddl_options.SelectedValue == "8") // condition + deductible + loading
            {

                idLoading.Visible = true;
                btLoading.Visible = true;
                idCondi.Visible = true;
                idApp.Visible = false;                
                idReject.Visible = false;               
                idDedc.Visible = true;
                btnProceedView.Text = "Apply Conditions, Deducible & Loadings";
                this.FillSchedule();
            }
        }
    }

    

    protected void GetDiscountRates(string bank_code, int years)
    {
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetDiscountRate(bank_code, years), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    DISCOUNT_RATE = Convert.ToDouble(details.Rows[0]["rate"].ToString());

                }
                else
                {

                    string msg = "Message : No Records found. Dated on " + System.DateTime.Now.ToString();
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()));
                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));
            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }
    protected void GetRates(string bank_code, string term)
    {
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

                    //chkSRCC.Checked ,chkTR.Checked
                    //basic rate change according to covers asks---
                    // fire and lighting only
                    //
                    //fire and other perils
                    //
                    //light + other+ rcc
                    //
                    //light + other+ rcc+ tc
                    //
                    //
                    if (chkFirLight.Checked == true && chkOtherPerils.Checked == false && chkSRCC.Checked == false && chkTR.Checked == false)
                    {
                        BASIC = BASIC;
                        RCC = 0;
                        TR = 0;
                    }
                    else if (chkFirLight.Checked == true && chkOtherPerils.Checked == true && chkSRCC.Checked == false && chkTR.Checked == false)
                    {
                        BASIC = BASIC_2;
                        RCC = 0;
                        TR = 0;
                    }
                    else if (chkFirLight.Checked == true && chkOtherPerils.Checked == true && chkSRCC.Checked == true && chkTR.Checked == false)
                    {
                        BASIC = BASIC;
                        RCC = RCC;
                        TR = 0;
                    }
                    else if (chkFirLight.Checked == true && chkOtherPerils.Checked == true && chkSRCC.Checked == true && chkTR.Checked == true)
                    {
                        BASIC = BASIC;
                        RCC = RCC;
                        TR = TR;
                    }
                }
                else
                {

                    string msg = "Message : No Records found. Dated on " + System.DateTime.Now.ToString();
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("ERROR_CLOSE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("2".ToString()));
                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));
            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }
    public void PremiumCalculation(double sumInsu, double loading)
    {
        try
        {
            double reNetPremium, reNetPremiumTemp = 0;

            //calculation part--08092021-new modifications-------
            string IsLongTerm = "";
            double YearsCount = 0;

            TERM = txtTerm.Text.ToString().Trim();

            if (TERM == "1")
                {
                YearsCount = 1; IsLongTerm = "N";
            }
            else
            {
                IsLongTerm = "Y";
                if (txtPeriod.Text.ToString().Trim() == "0") { YearsCount = 1; } else { YearsCount = Convert.ToDouble(txtPeriod.Text.ToString().Trim()); }
            }

            this.GetDiscountRates(hfbankcode.Value.ToString().Trim(), Convert.ToInt32(YearsCount));

            //    DISCOUNT_RATE
            //    RENEWAL_FEE
            //--------------


            CalNetPreTemp = Convert.ToDouble(txtnetpremium.Text.ToString()); //  ((sumInsu * BASIC * YearsCount) / 100) - (((sumInsu * BASIC * YearsCount) / 100) * DISCOUNT_RATE);
            CalNetPre = Math.Round(CalNetPreTemp, 2, MidpointRounding.AwayFromZero);

            //add loading to net premium--------------->

            reNetPremium = (CalNetPreTemp * loading) / 100;
            reNetPremium = CalNetPreTemp + reNetPremium;

            reNetPremiumTemp = Math.Round((reNetPremium), 2, MidpointRounding.AwayFromZero);
            //-------------------------------------------


            Cal_RCCTemp = Convert.ToDouble(txtRCC.Text.ToString());
            //Cal_RCC = Math.Round(((sumInsu * RCC * YearsCount) / 100), 2, MidpointRounding.AwayFromZero);

            Cal_TRTemp = Convert.ToDouble(txtTR.Text.ToString());
            //Cal_TR = Math.Round(((sumInsu * TR * YearsCount) / 100), 2, MidpointRounding.AwayFromZero);

            Cal_POLICY_FEETemp = Convert.ToDouble(txtPolicyFee.Text.ToString());
            //Cal_POLICY_FEE = Math.Round((POLICY_FEE), 2, MidpointRounding.AwayFromZero);




            Cal_ADMIN_FEETemp = ((reNetPremium + Cal_RCCTemp + Cal_TRTemp) * ADMIN_FEE) / 100;
            Cal_ADMIN_FEE = Math.Round(Cal_ADMIN_FEETemp, 2, MidpointRounding.AwayFromZero);


            Cal_Renewal_FeeTemp = Convert.ToDouble(txtRenewal.Text.ToString());
            //Cal_Renewal_Fee = Math.Round((RENEWAL_FEE * (YearsCount - 1)), 2, MidpointRounding.AwayFromZero);

            DateTime date = DateTime.ParseExact(CREATED_ON, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            //DateTime date2 = DateTime.ParseExact(date.ToString("yyyy/MM/dd"), "yyyy/MM/dd", CultureInfo.InvariantCulture);
            int date2 = Convert.ToInt32(date.ToString("yyyyMMdd"));
            double sumForTaxVat = 0;
            sumForTaxVat = reNetPremium + Cal_RCCTemp + Cal_TRTemp + Cal_POLICY_FEETemp + Cal_ADMIN_FEETemp + Cal_Renewal_FeeTemp;

            using (OracleConnection conn = orcl_con.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("GENPAY.CALCULATE_NBL_AND_VAT_DATE", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("taxLiableAmount", sumForTaxVat);
                    cmd.Parameters.AddWithValue("requestDate", date2);
                    cmd.Parameters.Add("nblAmount", OracleType.Number).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("vatAmount", OracleType.Number).Direction = ParameterDirection.Output;

                    OracleDataReader dr = cmd.ExecuteReader();

                    Cal_NBTTemp = double.Parse(cmd.Parameters["nblAmount"].Value.ToString());
                    Cal_VATTemp = double.Parse(cmd.Parameters["vatAmount"].Value.ToString());

                    dr.Close();

                }
                conn.Close();
            }


            Cal_NBT = Math.Round((Cal_NBTTemp), 2, MidpointRounding.AwayFromZero);
            Cal_VAT = Math.Round((Cal_VATTemp), 2, MidpointRounding.AwayFromZero);

            Cal_TotalTemp = sumForTaxVat + Cal_NBTTemp + Cal_VATTemp;
            Cal_Total = Math.Round((Cal_TotalTemp), 2, MidpointRounding.AwayFromZero);

            //----------dispaly values-------------------------------

            hfnetcal.Value = reNetPremiumTemp.ToString();
            hfadmincal.Value = Cal_ADMIN_FEE.ToString();
            hfnbtcal.Value = Cal_NBT.ToString();
            hfvatcal.Value = Cal_VAT.ToString();
            hftotalcal.Value = Cal_Total.ToString();


            txt_NetPre.Text = reNetPremiumTemp.ToString();
            txt_adminCal.Text = Cal_ADMIN_FEE.ToString();
            txt_nbt.Text = Cal_NBT.ToString();
            txt_vat.Text = Cal_VAT.ToString();
            txt_total.Text = Cal_Total.ToString();
            //txt_nbt.Text = Cal_NBT.ToString("n2");
            //txt_vat.Text = Cal_VAT.ToString("n2");
            txttotalpay.Text = Cal_Total.ToString("n2");
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()), false);
        }

    }


    protected void btLoading_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(txtprecent.Text))
        {
             TERM = txtTerm.Text.ToString().Trim();
            if (TERM == "1")
            {
                this.GetRates(hfbankcode.Value.ToString().Trim(), "A");
            }
            else
            {
                this.GetRates(hfbankcode.Value.ToString().Trim(), "L");
            }


            this.PremiumCalculation(Convert.ToDouble(hfSumInsu.Value), Convert.ToDouble(txtprecent.Text.Trim().ToString()));
        }
        else { }
    }
    protected void btnCancelProp_Click(object sender, EventArgs e)
    {
        try
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("./CancelConfirmation.aspx?REQ_ID=" + endc.Encrypt(hfRefId.Value.ToString().Trim()), false);
 
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()), false);
        }
    }

    protected void btChangeDeduct_Click(object sender, EventArgs e)
    {
        try
        {
            //condtiontable.Visible = true;
            trcondi001.Visible = true;
            idDedc.Visible = true;
            btDeductApply.Visible = true;
            btnProceedView.Visible = false;

        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }

    }

    protected void btDeductApply_Click(object sender, EventArgs e)
    {
        try
        {

            txttotalpay.Text = Cal_Total.ToString("n2");
            string tempVal = "";
            tempVal = Convert.ToDouble(txtDeducVal.Text.ToString().Trim()).ToString("n2");
            bool resultDesuct = this.exe_up.Update_PolicyWithConditions(hfRefId.Value.ToString().Trim(), null, null, null, null, null, null, "N", "N", "Y", tempVal, txtDeducPre.Text.ToString().Trim(), UserId.Value);
            string flag_out_deduct = "D";
            string msgDeduct = "Conditions Applied.";

            if (resultDesuct)
            {
                if (flag_out_deduct == "R")
                {
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("REJECT".ToString()) + "&APP_MSG=" + endc.Encrypt(msgDeduct) + "&code=" + endc.Encrypt("11".ToString()), false);

                }
                else
                {

                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msgDeduct) + "&code=" + endc.Encrypt("11".ToString()), false);
                }


            }

            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt("Error in Upadte.".ToString()),false);

            }


        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }

    protected void btnProceedView_Click(object sender, EventArgs e)
    {

        try
        {
            //this.createPolicyNumber();

            bool result = false;
            string flag_out = "";
            string msg = "";

            if (ddl_options.SelectedValue == "1")
            {
                result = this.exe_up.Update_PolicyWithConditions(hfRefId.Value.ToString().Trim(), "Y", null, null, null, null, null, "N", "N", null, null, null, UserId.Value);
                flag_out = "A";
                msg = "Successfully Approved.";
            }
            else if (ddl_options.SelectedValue == "2")
            {

                result = this.exe_up.Update_PolicyWithConditions(hfRefId.Value.ToString().Trim(), null, null, "Y", null, txtcoditionSlic.Text.ToString().Trim(), null, "N", "N", null, null, null, UserId.Value);
                flag_out = "C";
                msg = "Conditions Applied and Approved.";


            }
            else if (ddl_options.SelectedValue == "3")
            {
                result = this.exe_up.Update_PolicyWithConditions(hfRefId.Value.ToString().Trim(), null, null, null, "Y", null, txtprecent.Text.ToString().Trim(), "N", "N", null, null, null, UserId.Value);
                flag_out = "L";
                msg = "Loading Added and Approved.";

                //insert prevous records to history table----->
                if (!string.IsNullOrEmpty(txtnetpremium.Text))
                {
                    this.getPropClass.GetSchedualCalValues(hfRefId.Value, out SC_POLICY_NO, out SC_SUM_INSU, out SC_NET_PRE,
                    out SC_RCC, out SC_TR, out SC_ADMIN_FEE,
                    out SC_POLICY_FEE, out SC_NBT, out SC_VAT, out SC_TOTAL_PAY, out CREATED_ON, out CREATED_BY,
                    out FLAG, out SC_Renewal_FEE, out BPF_FEE, out DEBIT_NO);

                    string cal_ref = "";

                    bool inserted = this.insert_.insert_fire_sche_cal_details_history(hfRefId.Value, SC_POLICY_NO, Convert.ToDouble(SC_SUM_INSU), Convert.ToDouble(SC_NET_PRE), Convert.ToDouble(SC_RCC), Convert.ToDouble(SC_TR),
                    Convert.ToDouble(SC_ADMIN_FEE), Convert.ToDouble(SC_POLICY_FEE), Convert.ToDouble(SC_NBT), Convert.ToDouble(SC_VAT), Convert.ToDouble(SC_TOTAL_PAY), CREATED_ON, CREATED_BY, FLAG, Convert.ToDouble(SC_Renewal_FEE), Convert.ToDouble(BPF_FEE), out cal_ref);

                    //double x = Convert.ToDouble(txt_NetPre.Text);

                    //-------------------------------------------->

                    //update current schedule values--------------->

                    bool update_result = this.exe_up.update_shcduleCalLoadingValue(hfRefId.Value,
                        Convert.ToDouble(txt_NetPre.Text), Convert.ToDouble(txt_adminCal.Text), Convert.ToDouble(txt_nbt.Text), Convert.ToDouble(txt_vat.Text), Convert.ToDouble(txt_total.Text), Session["EPFNum"].ToString());

                    //--------------------------------------------->
                }
                else { }
            }
            else if (ddl_options.SelectedValue == "4")
            {
                result = this.exe_up.Update_PolicyWithConditions(hfRefId.Value.ToString().Trim(), null, null, "X", "X", txtcoditionSlic.Text.ToString().Trim(), txtprecent.Text.ToString().Trim(), "N", "Y", null, null, null, UserId.Value);
                flag_out = "B";
                msg = "Conditions and Loading added.";


                //insert prevous records to history table----->
                if (!string.IsNullOrEmpty(txtnetpremium.Text))
                {
                    this.getPropClass.GetSchedualCalValues(hfRefId.Value, out SC_POLICY_NO, out SC_SUM_INSU, out SC_NET_PRE,
                    out SC_RCC, out SC_TR, out SC_ADMIN_FEE,
                    out SC_POLICY_FEE, out SC_NBT, out SC_VAT, out SC_TOTAL_PAY, out CREATED_ON, out CREATED_BY,
                    out FLAG, out SC_Renewal_FEE, out BPF_FEE, out DEBIT_NO);
                    string cal_ref = "";

                    bool inserted = this.insert_.insert_fire_sche_cal_details_history(hfRefId.Value, SC_POLICY_NO, Convert.ToDouble(SC_SUM_INSU), Convert.ToDouble(SC_NET_PRE), Convert.ToDouble(SC_RCC), Convert.ToDouble(SC_TR),
                    Convert.ToDouble(SC_ADMIN_FEE), Convert.ToDouble(SC_POLICY_FEE), Convert.ToDouble(SC_NBT), Convert.ToDouble(SC_VAT), Convert.ToDouble(SC_TOTAL_PAY), CREATED_ON, CREATED_BY, FLAG, Convert.ToDouble(SC_Renewal_FEE), Convert.ToDouble(BPF_FEE), out cal_ref);
                    //-------------------------------------------->
                    //update current schedule values--------------->
                    bool update_result = this.exe_up.update_shcduleCalLoadingValue(hfRefId.Value,
                                           Convert.ToDouble(txt_NetPre.Text), Convert.ToDouble(txt_adminCal.Text), Convert.ToDouble(txt_nbt.Text), Convert.ToDouble(txt_vat.Text), Convert.ToDouble(txt_total.Text), Session["EPFNum"].ToString());

                    //--------------------------------------------->
                }
                else { }




            }
            else if (ddl_options.SelectedValue == "5")
            {
                result = this.exe_up.Update_PolicyWithConditions(hfRefId.Value.ToString().Trim(), null, "Y", null, null, null, null, "R", "N", null, null, null, UserId.Value);
                flag_out = "R";
                msg = "Request Rejected.";
            }

            else if (ddl_options.SelectedValue == "6")
            {
                txttotalpay.Text = Cal_Total.ToString("n2");
                string tempVal = "";
                tempVal = Convert.ToDouble(txtDeducVal.Text.ToString().Trim()).ToString("n2");
                //string deducWording = txtDeducPre.Text.ToString().Trim()+"% Deductible on claim amount or Rs. "+ tempVal + " , which ever is the highest";
                //string deducWording = txtDeducPre.Text.ToString().Trim() + "% - " + tempVal;
                result = this.exe_up.Update_PolicyWithConditions(hfRefId.Value.ToString().Trim(), null, null, null, null, null, null, "N", "N", "Y", tempVal, txtDeducPre.Text.ToString().Trim(), UserId.Value);
                flag_out = "D";
                msg = "Conditions Applied and Approved.";
            }
            else if (ddl_options.SelectedValue == "7") //conditions and deductible
            {
                result = this.exe_up.Update_PolicyWithConditions(hfRefId.Value.ToString().Trim(), null, null, "Y", null, txtcoditionSlic.Text.ToString().Trim(), null, "N", "N", null, null, null, UserId.Value);
            

                txttotalpay.Text = Cal_Total.ToString("n2");
                string tempVal = "";
                tempVal = Convert.ToDouble(txtDeducVal.Text.ToString().Trim()).ToString("n2");
                result = this.exe_up.Update_PolicyWithConditions(hfRefId.Value.ToString().Trim(), null, null, null, null, null, null, "N", "N", "Y", tempVal, txtDeducPre.Text.ToString().Trim(), UserId.Value);




                flag_out = "CD";
                msg = "Conditions and Deductible added.";

            }
            else if (ddl_options.SelectedValue == "8") //conditions + deductible + Loading
            {
                result = this.exe_up.Update_PolicyWithConditions(hfRefId.Value.ToString().Trim(), null, null, "X", "X", txtcoditionSlic.Text.ToString().Trim(), txtprecent.Text.ToString().Trim(), "N", "Y", null, null, null, UserId.Value);
    
                //insert prevous records to history table----->
                if (!string.IsNullOrEmpty(txtnetpremium.Text))
                {
                    this.getPropClass.GetSchedualCalValues(hfRefId.Value, out SC_POLICY_NO, out SC_SUM_INSU, out SC_NET_PRE,
                    out SC_RCC, out SC_TR, out SC_ADMIN_FEE,
                    out SC_POLICY_FEE, out SC_NBT, out SC_VAT, out SC_TOTAL_PAY, out CREATED_ON, out CREATED_BY,
                    out FLAG, out SC_Renewal_FEE, out BPF_FEE, out DEBIT_NO);
                    string cal_ref = "";

                    bool inserted = this.insert_.insert_fire_sche_cal_details_history(hfRefId.Value, SC_POLICY_NO, Convert.ToDouble(SC_SUM_INSU), Convert.ToDouble(SC_NET_PRE), Convert.ToDouble(SC_RCC), Convert.ToDouble(SC_TR),
                    Convert.ToDouble(SC_ADMIN_FEE), Convert.ToDouble(SC_POLICY_FEE), Convert.ToDouble(SC_NBT), Convert.ToDouble(SC_VAT), Convert.ToDouble(SC_TOTAL_PAY), CREATED_ON, CREATED_BY, FLAG, Convert.ToDouble(SC_Renewal_FEE), Convert.ToDouble(BPF_FEE), out cal_ref);
                    //-------------------------------------------->
                    //update current schedule values--------------->
                    bool update_result = this.exe_up.update_shcduleCalLoadingValue(hfRefId.Value,
                                           Convert.ToDouble(txt_NetPre.Text), Convert.ToDouble(txt_adminCal.Text), Convert.ToDouble(txt_nbt.Text), Convert.ToDouble(txt_vat.Text), Convert.ToDouble(txt_total.Text), Session["EPFNum"].ToString());

                    //--------------------------------------------->
                }
                else { }

                txttotalpay.Text = Cal_Total.ToString("n2");
                string tempVal = "";
                tempVal = Convert.ToDouble(txtDeducVal.Text.ToString().Trim()).ToString("n2");
                result = this.exe_up.Update_PolicyWithConditions(hfRefId.Value.ToString().Trim(), null, null, null, null, null, null, "N", "N", "Y", tempVal, txtDeducPre.Text.ToString().Trim(), UserId.Value);




                flag_out = "CDL";
                msg = "Conditions, Deductible and Loading added.";

            }
            else
            {
                flag_out = "";
                result = false;
            }

            if (result)
            {
                if (flag_out == "R")
                {
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("REJECT".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("4".ToString()), false);

                }
                else
                {
                    this.createPolicyNumber();
                    this.GetPhoneNumberofOfficersandSendSMS(hfRefId.Value.ToString(), hfPolNo.Value.ToString(), msg, hfbankcode.Value.ToString());
                    this.sendEmailToSLICOfficer(hfRefId.Value.ToString(), hfPolNo.Value.ToString(), msg, hfbankcode.Value.ToString());
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("4".ToString()), false);
                }


            }

            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt("Error in Upadte Flag".ToString()));

            }

        }

        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }

    }

    //send sms and email to officer and email to bank side
    protected void GetPhoneNumberofOfficersandSendSMS(string req_id, string polno, string status,string bankCode)
    {
        string officerContact = string.Empty;
        int rtnCount = 0;
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetOfficer(bankCode.ToString()), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    for (int i = 0; i < details.Rows.Count; i++)
                    {
                        officerContact = details.Rows[i]["CONTACT_NO"].ToString().Substring(1);
                        officerContact = "94" + officerContact;

                        string txt_body = "Fire policy SLIC response. Ref. ID: "+ req_id+"\nPolicy No: "+ polno +"\nStatus: "+ status;

                        this.insert_.Send_sms_to_customer(officerContact, "", txt_body, out rtnCount);

                    }

                }
                else
                {
                    officerContact = "";

                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));

            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

        }
    }

    protected void sendEmailToSLICOfficer(string req_id, string polno, string status, string bankCode)
    {
        this.GetEmailsrofOfficers(hfbankcode.Value.ToString().Trim());
        coop.get_userBankEmail(hfbankUN.Value.ToString().ToLower(), out bancass_email);
        //Getas400.as400_get_userBankEmail(hfbankUN.Value.ToString().ToLower(), out bancass_email);

        string txt_body = "Fire policy SLIC response. Ref. ID: " + req_id + "<br/>Policy No: " + polno + "<br/>Status: " + status;

        string ccEmails = string.Empty;
        string toEmails = string.Empty;


        ccEmails = String.Join(", ", emailIDForCC);
        //toEmails = String.Join(", ", emailID);
        toEmails = bancass_email;
        
        emailSend.fireRequestDetails("SLICR", "", toEmails, ccEmails, req_id.ToString().Trim().ToString(), txt_body, "", FromEmailid);

    }


    protected void GetEmailsrofOfficers(string bank_code)
    {
        string officerEmails = string.Empty;
        string officerEmailsCC = string.Empty;
        //int rtnCount = 0;
        DataTable details = new DataTable();
        DataTable detailsforCC = new DataTable();
        try
        {
            //details = orcle_trans.GetRows(this._sql.GetOfficerEmail(Session["bank_code"].ToString()), details);

            //if (orcle_trans.Trans_Sucess_State == true)
            //{

            //    if (details.Rows.Count > 0)
            //    {
            //        for (int i = 0; i < details.Rows.Count; i++)
            //        {
            //            emailID.Add(details.Rows[i]["EMAIL"].ToString());
            //        }

            //    }
            //    else
            //    {
            //        officerEmails = "";

            //    }
            //}
            //else
            //{
            //    var endc = new EncryptDecrypt();
            //    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));

            //}


            /// second  table for CC user emails

            detailsforCC = orcle_trans.GetRows(this._sql.GetOfficerEmailAll(hfbankcode.Value.ToString().Trim()), detailsforCC);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (detailsforCC.Rows.Count > 0)
                {
                    for (int i = 0; i < detailsforCC.Rows.Count; i++)
                    {
                        emailIDForCC.Add(detailsforCC.Rows[i]["EMAIL"].ToString());
                    }

                }
                else
                {
                    officerEmailsCC = "";

                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));

            }

        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

        }
    }
    
}