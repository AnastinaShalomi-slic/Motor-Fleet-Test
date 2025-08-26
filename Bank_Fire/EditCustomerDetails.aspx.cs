using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;


public partial class Bank_Fire_EditCustomerDetails : System.Web.UI.Page
{
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

    string dh_bcode, dh_bbrcode, dh_name, dh_agecode, dh_agename, dh_nic, dh_br,
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

    string SC_POLICY_NO, SC_SUM_INSU, SC_NET_PRE, SC_RCC, SC_TR, SC_ADMIN_FEE, SC_POLICY_FEE, SC_NBT, SC_VAT, SC_TOTAL_PAY, CREATED_ON, CREATED_BY,
            FLAG, SC_Renewal_FEE, BPF_FEE, DEBIT_NO = string.Empty;

    string BANK = string.Empty;
    double sumInsu, BASIC, RCC, TR, ADMIN_FEE, POLICY_FEE, RENEWAL_FEE, DISCOUNT_RATE, BASIC_2 = 0;

    double CalNetPre, Cal_RCC, Cal_TR, Cal_ADMIN_FEE, Cal_POLICY_FEE, Cal_NBT, Cal_VAT, Cal_Total = 0;
    double CalNetPreTemp, Cal_RCCTemp, Cal_TRTemp, Cal_ADMIN_FEETemp, Cal_POLICY_FEETemp, Cal_NBTTemp, Cal_VATTemp, Cal_TotalTemp = 0;

    public double newNet = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);


        if (!Page.IsPostBack)
        {
            try
            {
                Auth = "admin";//Session["Auth_Code"].ToString();

                if (Auth == "admin")
                {

                    if (Session["bank_code"].ToString() != "")
                    {
                        var en = new EncryptDecrypt();
                        UserId.Value = Session["UserId"].ToString();
                        brCode.Value = Session["brcode"].ToString();
                        hfRefId.Value = en.Decrypt(Request.QueryString["REQ_ID"]).Trim().ToString();
                        hfFlag.Value = en.Decrypt(Request.QueryString["V_FLAG"]).ToString().Trim();
                        hfSumInsu.Value = en.Decrypt(Request.QueryString["SUM_INSU"]).ToString().Trim();
                        this.disableFields();
                        this.FillSchedule();
                        this.InitializedListDistrict(txt_addline4, "description", "description", this._sql.GetDistrict(), "'district_id'");
                        this.InitializedListDistrict(txt_dweAdd4, "description", "description", this._sql.GetDistrict(), "'district_id'");

                        if (Session["bank_code"].ToString() == "7719")
                        {
                            Div44.Attributes.Add("style", "display:normal"); /*txtLoanNumber.Text = "";*/

                            termAnual.Enabled = false;
                            termAnual.Attributes.Add("style", "display:none");
                        }
                        else { Div44.Attributes.Add("style", "display:none"); txtLoanNumber.Text = ""; /*bank_code.Value = "";*/ }
                        //---end---->
                        constructDetails.Visible = false;





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
                        out FLAG, out SC_Renewal_FEE, out BPF_FEE, out DEBIT_NO);

            hfbankcode.Value = dh_bcode_id;
            txt_nameOfProp.Text = dh_name;
            txt_nic.Text = dh_nic;
            txt_br.Text = dh_br;
            txt_addline1.Text = dh_padd1;
            txt_addline2.Text = dh_padd2;
            txt_addline3.Text = dh_padd3;
            if (string.IsNullOrEmpty(dh_padd4)) { txt_addline4.Text = "0"; } else { txt_addline4.Text = dh_padd4; }
            txt_tele.Text = dh_phone;
            txt_landLine.Text = LAND_PHONE;
            txt_email.Text = dh_email;
            txt_dweAdd1.Text = dh_iadd1;
            txt_dweAdd2.Text = dh_iadd2;
            txt_dweAdd3.Text = dh_iadd3;
            if (string.IsNullOrEmpty(dh_iadd4)) { txt_dweAdd4.Text = "0"; } else { txt_dweAdd4.Text = dh_iadd4; }
            txt_fromDate.Text = dh_pfrom;
            txt_toDate.Text = dh_pto;
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
                Div43.Attributes.Add("style", "display:none");
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
                Div43.Attributes.Add("style", "display:none");
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
        }
        catch (Exception ex)
        {

            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

        }


    }

    public void disableFields()
    {
        txt_nameOfProp.Enabled = true;

        txt_nic.Enabled = false;
        txt_br.Enabled = false;
        txt_addline1.Enabled = true;
        txt_addline2.Enabled = true;
        txt_addline3.Enabled = true;
        txt_addline4.Enabled = true;
        txt_tele.Enabled = false;
        txt_landLine.Enabled = false;
        txt_email.Enabled = false;
        txt_dweAdd1.Enabled = true;
        txt_dweAdd2.Enabled = true;
        txt_dweAdd3.Enabled = true;
        txt_dweAdd4.Enabled = true;
        txt_fromDate.Enabled = false;
        txt_toDate.Enabled = false;
        txt_bankVal.Enabled = false;
        chkl1.Enabled = false;

        txt_sumInsu1.Enabled = false;
        txt_sumInsu2.Enabled = false;
        txt_sumInsuTotal.Enabled = false;

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
        txtSoloarModel.Enabled = false;
        txtSolarCountry.Enabled = false;
        rbSolTwo.Enabled = false;
        rbSolOne.Enabled = false;
        txt_solar.Enabled = false;
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
        txtLoanNumber.Enabled = false;


    }

    protected void Button2_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/Bank_Fire/ViewProposal.aspx");

    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            bool result = exe_up.update_CustomerDetails(hfRefId.Value, txt_nameOfProp.Text.ToString().Trim(), txt_addline1.Text.ToString().Trim(),
                 txt_addline2.Text.ToString().Trim(), txt_addline3.Text.ToString().Trim(), txt_addline4.Text.ToString().Trim(),
                 txt_dweAdd1.Text.ToString().Trim(), txt_dweAdd2.Text.ToString().Trim(), txt_dweAdd3.Text.ToString().Trim(), txt_dweAdd4.Text.ToString().Trim(), Session["userName_code"].ToString());

            string msg = "Changes applied successfully.";

            if (result)
            {

                //this.createPolicyNumber();
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("5".ToString()), false);


            }
        }

        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }

    protected void InitializedListDistrict(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {

        DataTable getrecord = new DataTable();
        try
        {

            getrecord = orcle_trans.GetRows(executor, getrecord);

            if (orcle_trans.Trans_Sucess_State == true)
            {
                if (getrecord.Rows.Count > 1)
                {
                    target_list.DataSource = getrecord;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                    // ddl_make.Items.Insert(0, new ListItem("--All--", "0"));
                }

                else if (getrecord.Rows.Count == 1)
                {

                    target_list.DataSource = getrecord;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                }

                else
                {
                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt("Error : Sorry " + target_desc + "can't initialized. Contact system administrator. Dated On :" + System.DateTime.Now.ToString()));

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