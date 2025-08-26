using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Web;

public partial class Bank_Fire_ProposalEntry : System.Web.UI.Page
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    Insert_class exe_in = new Insert_class();
    InsertBoardraux _boardraux = new InsertBoardraux();
    string Auth = string.Empty;
    int Resultcount = 1;
    string agentCode, agentName, BGI = string.Empty;
    string overallFlag = "";
    PremiumCalculation premCal = new PremiumCalculation();
    ExcuteOraSide ora_side = new ExcuteOraSide();
    //calcultions for shedule-----

    Update_class exe_up = new Update_class();
    Ip_Class get_ip = new Ip_Class();
    EncryptDecrypt dc = new EncryptDecrypt();
    DeviceFinder findDev = new DeviceFinder();
    DeviceFinder df = new DeviceFinder();
    ORCL_Connection orcl_con = new ORCL_Connection();
    Insert_class insert_ = new Insert_class();
    GetProposalDetails getPropClass = new GetProposalDetails();

    DetailsForfireEmailReq emailSend = new DetailsForfireEmailReq();

    public string[] ReturnCovers = new string[13];
    //public string[] emailID = new string[6];

    List<String> emailID = new List<String>();
    List<String> emailIDForCC = new List<String>();

    string agentCodeCal, agentNameCal, BGICal, BANK_ACC = string.Empty;
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
    public string over_Val = string.Empty;

    string qrefNo, UsrId = "", reqTyp = "", mgs = "";
    string rID = "";
    string filePath = "", ext = "";
    bool cancelBtn = false;
    List<string> optionList;
    private string SessionUserId = "";
    private string SessionbrCode = "";
    int emailRtn = 0;
    string PreviousClaim = "", ccEmailIds = "";
    //
    string BANK, RateTerm = string.Empty;
    double sumInsu, BASIC, RCC, TR, ADMIN_FEE, POLICY_FEE,RENEWAL_FEE, DISCOUNT_RATE, BASIC_2, SOLAR_RATE = 0;

    double CalNetPre, Cal_RCC, Cal_TR, Cal_ADMIN_FEE, Cal_POLICY_FEE, Cal_NBT, Cal_VAT, Cal_Total,Cal_Renewal_Fee = 0;
    double CalNetPreTemp, Cal_RCCTemp, Cal_TRTemp, Cal_ADMIN_FEETemp, Cal_POLICY_FEETemp, Cal_NBTTemp, Cal_VATTemp, Cal_TotalTemp, Cal_Renewal_FeeTemp = 0;

    double calNetPre, calRCC, calTR, calPolicyFee, calAdminFee, calRenewalFee, calNBT, calVat, calTotal, calBPF_Value = 0;
    bool rtnM = false;

    //---end------>>
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        // scriptManager.RegisterPostBackControl(this.btnProceed);
        GetToken_API getToken_API = new GetToken_API();
        var result = getToken_API.GetToken();
        DivDOB.Visible = false; // Initially hide DivDOB

        // ((MainMaster)Master).slected_manu.Value = "appReq";
        Session["r01"] = "";
        if (!Page.IsPostBack)
        {
            try
            {
                hfbankname.Value = Session["temp_bank"].ToString();
                hfbranchname.Value = Session["temp_branch"].ToString();

                Auth = "admin";//Session["Auth_Code"].ToString();
                string propType = string.Empty;
                var en = new EncryptDecrypt();
                int ReCount = 0;
                if (Auth == "admin")
                {

                 

                    if (Session["bank_code"].ToString() != "")
                    {
                        propType = en.Decrypt(Request.QueryString["Type"]).Trim().ToString();
                        //propType = Request.QueryString["Type"];

                        //txtProposalType.Text = propType; // proposal types
                        hfpolType.Value = propType;
                        txtProposalType.Text = propType;
                        UserId.Value = Session["UserId"].ToString();
                        brCode.Value = Session["brcode"].ToString();
                       
                        this.InitializedListDistrict(txt_addline4, "description", "description", this._sql.GetDistrict(), "'district_id'");
                        this.InitializedListDistrict(txt_dweAdd4, "description", "description", this._sql.GetDistrict(), "'district_id'");
                        this.InitializedListSLICCode(ddlSlicCode, "desig", "slic_code", this._sql.GetSLIC_Code(Session["bank_code"].ToString(), Session["branch_code"].ToString()), "'slic_code'");
                        //conditions for policy types-----
                        //1 ==fire and light
                        //2 ==Fire and Lightning + Solar Panel
                        //3 ==solar only
                        chkBf.Checked = true;
                        // 27042023 BPF changes
                        ora_side.BPF_Allow_Check(Session["bank_code"].ToString().Trim(), out ReCount);

                        if(ReCount > 0) { bpfR.Visible = true; }
                        else { bpfR.Visible = false; }
                        // end--------

                        if (propType == "1")
                        {
                            txtHeading.InnerHtml = "Private Dwelling House";
                            this.InitializedListDistrict(ddlNumberOfYears, "n", "n", this._sql.GetNumbers(25), "'n'");
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
                        else if (propType == "2")
                        {
                            txtHeading.InnerHtml = "Private Dwelling House & Solar Panel System";
                            this.InitializedListDistrict(ddlNumberOfYears, "n", "n", this._sql.GetNumbers(5), "'n'");
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
                        else if (propType == "3")
                        {
                            txtHeading.InnerHtml = "Solar Panel System";
                            this.InitializedListDistrict(ddlNumberOfYears, "n", "n", this._sql.GetNumbers(5), "'n'");
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
                            txtHeading.InnerHtml = "Fire and Lightning";
                            this.InitializedListDistrict(ddlNumberOfYears, "n", "n", this._sql.GetNumbers(25), "'n'");
                            Div19.Attributes.Add("style", "display:normal"); //building
                            Div20.Attributes.Add("style", "display:normal"); //wall
                            Div20I.Attributes.Add("style", "display:none"); //solar
                            // below mentions for solar
                            Div37.Attributes.Add("style", "display:none");
                            Div34.Attributes.Add("style", "display:none");
                            Div35.Attributes.Add("style", "display:none");
                            Div36.Attributes.Add("style", "display:none");
                            Div43.Attributes.Add("style", "display:none");

                            var endc = new EncryptDecrypt();

                            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("SE".ToString()));
                        }

                        //------------------------------->>

                        
                        
                        //for (int i = 0; i < chkl1.Items.Count; i++)
                        //{
                        //    chkl1.Items[i].Attributes.Add("onclick", "MutExChkList(this)");
                        //}

                        //chkl5.SelectedIndex = 0;

                        txt_sumInsuTotal.Attributes.Add("readonly", "readonly");
                        txt_toDate.Attributes.Add("readonly", "readonly");
                        //txt_fromDate.Attributes.Add("readonly", "readonly");
                        chkFirLight.Attributes.Add("onclick", "return false;");
                        txt_Ref_no.Attributes.Add("readonly", "readonly");
                        txt_NetPre.Attributes.Add("readonly", "readonly");
                        txt_srcc.Attributes.Add("readonly", "readonly");
                        txt_tr.Attributes.Add("readonly", "readonly");
                        txt_adminFee.Attributes.Add("readonly", "readonly");
                        txtPoliFee.Attributes.Add("readonly", "readonly");
                        txt_nbt.Attributes.Add("readonly", "readonly");
                        txt_vat.Attributes.Add("readonly", "readonly");
                        txtTotalPay.Attributes.Add("readonly", "readonly");
                        txt_renewal.Attributes.Add("readonly", "readonly");

                        //new changes NSB---->
                       if(Session["bank_code"].ToString() == "7719")
                        {
                            Div44.Attributes.Add("style", "display:normal"); /*txtLoanNumber.Text = "";*/
                            bank_code.Value = "7719";
                            termAnual.Enabled = false;
                            termAnual.Attributes.Add("style", "display:none");
                        }
                       else { Div44.Attributes.Add("style", "display:none"); txtLoanNumber.Text = ""; bank_code.Value = ""; }
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
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("Auth".ToString()));
                //Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(ex.Message.ToString()));

            }
        }
        else
        {
            /* IF PAGE.POSTBACK ASSIGHEN SESSION VARIABLE */
            //trreasonfield1.Attributes.Add("Style", "text-align:right;padding-right: 30px; display:normal;");
            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;
         
        }


    }


    protected void ClearText()
    {
    
    }

    public enum PopupMessageType
    {
        Error,
        Message,
        Warning,
        Success
    }

    protected void Search_Click1(object sender, EventArgs e)
    {
        try
        {
            string nic = txt_nic.Text.Trim().ToUpper(); // Get NIC from the input textbox and convert to uppercase           

            // Proceed with API call to fetch customer data
            GetPersonalCustomer_API getPersonalCustomer_API = new GetPersonalCustomer_API();
            var task = getPersonalCustomer_API.GetPersonalCustomerData(nic);

            // Wait for the task to complete
            task.Wait();

            var resultapi = task.Result;

            if (resultapi != null)
            {
                if (resultapi.ResponseCode == 200 && resultapi.Data != null) // Check if the API call was successful and data is not null
                {
                    string status = resultapi.Data.Status;

                    // Set dropdown to the correct value based on API response
                    if (!string.IsNullOrEmpty(status))
                    {
                        ListItem item = ddlInitials.Items.FindByValue(status);
                        if (item != null)
                        {
                            ddlInitials.ClearSelection();
                            item.Selected = true;
                        }
                    }

                    // Get the customer ID from the API result
                    long clientId = resultapi.Data.CustomerId; // Assuming CustomerId is of type long

                    // Store the client ID in a hidden field or other control
                    hiddenClientId.Value = clientId.ToString(); // Store the long value as a string

                    // Populate fields with retrieved data
                    txt_nameOfProp.Text = resultapi.Data.FullName;
                    txt_tele.Text = resultapi.Data.MobilePhoneNo;
                    txt_landLine.Text = resultapi.Data.HomePhoneNo;
                    txt_addline1.Text = resultapi.Data.HomeAddress1;
                    txt_addline2.Text = resultapi.Data.HomeAddress2;
                    txt_addline3.Text = resultapi.Data.HomeAddress3;

                    string HomeAddress4 = resultapi.Data.HomeAddress4;

                    if (!string.IsNullOrEmpty(HomeAddress4))
                    {
                        ListItem item = txt_addline4.Items.FindByValue(HomeAddress4);
                        if (item != null)
                        {
                            txt_addline4.ClearSelection();
                            item.Selected = true;
                        }
                    }

                    Clienttxt.Visible = false;

                    // Show or hide divisions as needed
                    SetDivVisibility(true, DivTermType, Div15, Div16, Div17, Div44, Div18, Div33, Div19,
                        Div20, Div20I, Div21, Div37, Div34, Div35, Div36, Div43, Div22, Div23,
                        Div24, Div25, Div38, Div39, Div40, Div41, Div26, Div27, Div28, Div29,
                        Div30, Div31, Div45, Div32);

                    lblNic.Visible = false; // Hide NIC label if customer found
                }
                else if (resultapi.ResponseCode == 400) // Check if response code is 400
                {
                    HandleCustomerNotFound();
                }
                else
                {
                    HandleCustomerNotFound(); // Handle other error responses
                }
            }
            else
            {
                HandleCustomerNotFound(); // Handle case where resultapi is null
            }
        }
        catch (Exception ex)
        {
            // Handle the exception (log it, show a message, etc.)
            lblNic.Text = "An error occurred while searching for the customer. Please try again.";
            lblNic.Visible = true;
            // Optionally log the exception details for debugging
            // LogException(ex);
        }
    }

    private void HandleCustomerNotFound()
    {
        SearchButton.Visible = false;
        lblNic.Visible = true;
        string warningScript = "custom_alert1('Customer not found. Please enter data and create client.');";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWarningMessage", warningScript, true);
        //lblNic.Text = "Customer not found. Please enter data and create client.";
        Clienttxt.Visible = true; // Show the client creation button
        DivDOB.Visible = true; // Make DivDOB visible

        SetDivVisibility(false, DivTermType, Div15, Div16, Div17, Div44, Div18, Div33, Div19,
            Div20, Div20I, Div21, Div37, Div34, Div35, Div36, Div43, Div22, Div23,
            Div24, Div25, Div38, Div39, Div40, Div41, Div26, Div27, Div28, Div29,
            Div30, Div31, Div45, Div32);

        // Clear all form fields if customer data is not found
        ddlInitials.ClearSelection();
        txt_nameOfProp.Text = "";
        txt_tele.Text = "";
        txt_landLine.Text = "";
        txt_addline1.Text = "";
        txt_addline2.Text = "";
        txt_addline3.Text = "";
        txt_addline4.ClearSelection();       
        chkSameAdd.Checked = false;
        txt_dweAdd1.Text = "";
        txt_dweAdd2.Text = "";
        txt_dweAdd3.Text = "";       
        txt_dweAdd4.ClearSelection();
    }


    private void SetDivVisibility(bool visible, params Control[] divs)
    {
        foreach (var div in divs)
        {
            div.Visible = visible;
        }
    }

    // Method to validate NIC format (example, adjust as per your specific format)
    private bool IsValidNICFormat(string nic)
    {
        // Sri Lankan NIC format variations:
        // - 9 digits followed by 'v', 'V', 'x', or 'X'
        // - 12 digits (without the trailing character)
        string nicPattern = @"^\d{9}[vVxX]?$|^\d{12}$";
        return System.Text.RegularExpressions.Regex.IsMatch(nic, nicPattern);
    }



    protected void create_clientClick(object sender, EventArgs e)
    {
        //lblNic.Visible = false;
        DivDOB.Visible = true;


        try
        {


            AddPersonalCustomer_API api = new AddPersonalCustomer_API();

            AddPersonalCustomer_API.CustomerData customerData = new AddPersonalCustomer_API.CustomerData();



            customerData.branchCode = 999;
            customerData.initials = "";
            customerData.lastName = "";
            customerData.status = ddlInitials.SelectedValue;
            customerData.fullName = txt_nameOfProp.Text;
            customerData.callingName = "";
            customerData.homePhoneNo = txt_landLine.Text;
            customerData.mobilePhoneNo = txt_tele.Text;
            customerData.nicNo = txt_nic.Text.Trim().ToUpper();
            customerData.passportNo = "";
            customerData.country = "";
            customerData.profession = "";
            customerData.subProfession = "";
            customerData.personalEmail = "";
            string dateOfBirth = txt_dob.Text;
            customerData.homeAddress1 = txt_addline1.Text;
            DateTime parsedDate = DateTime.ParseExact(dateOfBirth, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            string formattedDate = parsedDate.ToString("yyyy-MM-dd");
            customerData.dateOfBirth = formattedDate;
            customerData.homeAddress2 = txt_addline2.Text;
            customerData.homeAddress3 = txt_addline3.Text;
            customerData.homeAddress4 = txt_addline4.SelectedValue;
            customerData.officeName = "";
            customerData.designation = "";
            customerData.officePhoneNumber1 = "";
            customerData.officePhoneNumber2 = "";
            customerData.officeFaxNo = "";
            customerData.officeAddress1 = "";
            customerData.officeAddress2 = "";
            customerData.officeAddress3 = "";
            customerData.officeAddress4 = "";
            customerData.vatRegNo = "";
            customerData.svatRegNo = "";
            customerData.userId = "";


            var task = api.AddPersonalCustomer(customerData);


            task.Wait();

            var resultapi = task.Result;

            if (resultapi != null)
            {
                if (resultapi.ResponseCode == 200)
                {
                    // Successful response handling
                    // Registering the JavaScript function to show a pop-up message after postback
                    // Successful response handling
                    // Successful response handling
                    string script = "custom_alert1('Customer successfully created. Please proceed to fill in the policy details.', 'Success');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupMessage", script, true);
                    SearchButton.Visible = true;

                    // Handle nullable Data
                    if (resultapi.Data.HasValue)
                    {
                        //string dataValue = resultapi.Data.Value.ToString(); // Convert the nullable int to string

                        int clientId = resultapi.Data.Value; // Extract the clientId
                        hiddenClientId.Value = clientId.ToString();
                    }
                    else
                    {
                        // Handle the case where Data is null
                        ibiCClient.Text += " (No additional data provided)";
                    }

                    lblNic.Visible = false;
                    SetDivVisibility(true, DivTermType, Div15, Div16, Div17, Div44, Div18, Div33, Div19,
                    Div20, Div20I, Div21, Div37, Div34, Div35, Div36, Div43, Div22, Div23,
                    Div24, Div25, Div38, Div39, Div40, Div41, Div26, Div27, Div28, Div29,
                    Div30, Div31, Div45, Div32);


                    Clienttxt.Visible = false;
                }
                else if (resultapi.ResponseCode == 400)
                {
                    // Warning response handling
                    string warningScript = "custom_alert1('Customer Already Exsit.', 'Alert');";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWarningMessage", warningScript, true);

                }
            }
            else
            {
                string script = "showErrorMessage('Failed to create customer: No response from server.');";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessageNoResponse", script, true);
                //ibiCClient.Text = "Failed to create customer: No response from server.";
            }
        }
        catch (Exception ex)
        {
            string script = String.Format("showErrorMessage('An unexpected error occurred: {0}');", ex.Message.Replace("'", "\\'"));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "ErrorMessageException", script, true);
        }
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.ClearText();
    }
    protected void btn_find_Click1(object sender, EventArgs e)
    {
       
    }

 
    protected void InitializedListHospital(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {


        DataTable getrecord = new DataTable();
        try
        {
            ///btn_clear.Enabled = true;

            getrecord = orcle_trans.GetRows(executor, getrecord);

            if (orcle_trans.Trans_Sucess_State == true)
            {
                if (getrecord.Rows.Count > 1)
                {
                    target_list.DataSource = getrecord;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                    //ddl_model.Items.Insert(0, new ListItem("--All--", "0"));
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
                    //var endc = new EncryptDecrypt();
                    // Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt("Error : Sorry " + target_desc + "can't initialized. Contact system administrator. Dated On :" + System.DateTime.Now.ToString()));
                    //bt_other.Visible = true;
                    // bt_other.Enabled = true;
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
            // var endc = new EncryptDecrypt();
            //Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }


    protected void btnDone_Click(object sender, EventArgs e)
    {

        
        Response.Redirect("proposalTypes.aspx");


    }

    protected void CleareDropDownList(DropDownList ddl)
    {
        var firstitem_1 = ddl.Items[0];
        ddl.Items.Clear();
        ddl.Items.Add(firstitem_1);
    }

    protected bool Phone_number_validate(string txt)
    {
        //String r = "[&160#;]";
        bool outputString = false;
        string inputString = txt;
        Regex re = new Regex(@"^[0-9]{10}$");
        Match match = re.Match(inputString);
        if (match.Success)
        {

            outputString = true;
        }
        else
        {
            outputString = false;
        }

        return outputString;

    }

    protected void btnProceed_Click(object sender, EventArgs e)
    {
        //date_validate.Visible = true;
        string FPDRefNo = string.Empty;
        int count_id = 0;
        string dischrge = string.Empty;
        string addmit = string.Empty;
        double sumInsuVal2 = 0; double sumInsuVal1 = 0; double solarVal = 0;
        bool sucess = false;
        string flag_hold = string.Empty;
        string Q13 = string.Empty;
        string over_val_flag = "";
        string coverFlood = string.Empty;


        try
        {
            string wBrick, wCement, dWooden, dMetal, fTile, fcement, rTile, rAsbas, rGI, rConcret, cFire, cLight, covFlood = string.Empty;

            

            if (Chkbrick.Checked == true) { wBrick = "1"; }
            else { wBrick = "0"; }
            if (Chkcement.Checked == true) { wCement = "1"; }
            else { wCement = "0"; }

            if (ChkWooden.Checked == true) { dWooden = "1"; }
            else { dWooden = "0"; }

            if (ChkMetal.Checked == true) { dMetal = "1"; }
            else { dMetal = "0"; }


            if (Chktile.Checked == true) { fTile = "1"; }
            else { fTile = "0"; }

            if (ChkFloorcement.Checked == true) { fcement = "1"; }
            else { fcement = "0"; }



            if (Chekrooftile.Checked == true) { rTile = "1"; }
            else { rTile = "0"; }

            if (Chasbastos.Checked == true) { rAsbas = "1"; }
            else { rAsbas = "0"; }
            if (ChkGI.Checked == true) { rGI = "1"; }
            else { rGI = "0"; }
            if (Chkconcreat.Checked == true) { rConcret = "1"; }
            else { rConcret = "0"; }




            if (rbfire.Checked == true) { cFire = "1"; }
            else { cFire = "0"; }
            if (rblighting.Checked == true) { cLight = "1"; }
            else { cLight = "0"; }

            

            if (rbflood.Checked == true)
            {
                covFlood = "1";
                flag_hold = "Y";
                coverFlood = "Y";
            }
            else
            {
                covFlood = "0";
                flag_hold = "N";
                coverFlood = "N";
            }

            this.GetAgentDetails(Session["bank_code"].ToString());

            string x = txt_sumInsuTotal.Text.Trim().ToString();

            double total_over = Convert.ToDouble(txt_sumInsuTotal.Text.Trim().ToString());
            //string over_val_flag = "";

            if (hfpolType.Value.ToString().Trim() == "1") //house
            {
                if (total_over > 30000000) { over_val_flag = "Y"; }
                else { over_val_flag = "N"; }
            }
            else if (hfpolType.Value.ToString().Trim() == "3") //solar
            {
                if (total_over > 5000000) { over_val_flag = "Y"; }
                else { over_val_flag = "N"; }
            }
            else  //both
            { }
            //--changes 031020222 changes according to Q13 yes/no

            if(chkl4.SelectedValue.ToString() == "1") { Q13 = "Y"; }
            else if(chkl4.SelectedValue.ToString() == "0") { Q13 = "N"; }
            else { Q13 = "Y"; }

            //--overall flag-----------according to Q13------------->>>    

            if (Q13 == "Y")
            {

                if (rbflood.Checked == true)
                {
                    covFlood = "1";
                    flag_hold = "Y";
                    coverFlood = "Y";
                }
                else
                {
                    covFlood = "0";
                    flag_hold = "N";
                    coverFlood = "N";
                }
            }
            else if (Q13 == "N")
            {
                if (rbflood.Checked == true)
                {
                    covFlood = "1";
                    flag_hold = "N";
                    coverFlood = "Y";
                }
                else
                {
                    covFlood = "0";
                    flag_hold = "N";
                    coverFlood = "N";
                }
            }
            else
            {
                if (rbflood.Checked == true)
                {
                    covFlood = "1";
                    flag_hold = "N";
                    coverFlood = "Y";
                }
                else
                {
                    covFlood = "0";
                    flag_hold = "N";
                    coverFlood = "N";
                }
            }

            if (flag_hold == "Y" || over_val_flag == "Y") { overallFlag = "Y"; } else { overallFlag = "N"; }

            //--------------------------------------------------->>>

            int floorNumber = 0;
            if (string.IsNullOrEmpty(txtNoofFloors.Text.Trim().ToString())) { floorNumber = 0; }
            else { floorNumber = Convert.ToInt32(txtNoofFloors.Text.Trim().ToString()); }

            if (string.IsNullOrEmpty(txt_sumInsu2.Text.Trim()))
            {
                sumInsuVal2 = 0;
            }
            else { sumInsuVal2 = Convert.ToDouble(txt_sumInsu2.Text.Trim().ToString()); }

            if (string.IsNullOrEmpty(txt_sumInsu1.Text.Trim()))
            {
                sumInsuVal1 = 0;
            }
            else { sumInsuVal1 = Convert.ToDouble(txt_sumInsu1.Text.Trim().ToString()); }

            if (string.IsNullOrEmpty(txt_solar.Text.Trim()))
            {
                solarVal = 0;
            }
            else { solarVal = Convert.ToDouble(txt_solar.Text.Trim().ToString()); }


            double bank_val = 0;
            if (string.IsNullOrEmpty(txt_bankVal.Text.Trim()))
            {
                bank_val = 0;
            }
            else { bank_val = Convert.ToDouble(txt_bankVal.Text.Trim().ToString()); }

            string add4, dwell_add4 = string.Empty;
            if (txt_addline4.Text.ToString().Trim() == "0") { add4 = ""; } else { add4 = txt_addline4.Text.ToString().Trim(); }
            if (txt_dweAdd4.Text.ToString().Trim() =="0") { dwell_add4 = ""; } else { dwell_add4 = txt_dweAdd4.Text.ToString().Trim(); }
            //new chnages for second phase----05092021--------
      
            double NoOfyears = 0;
            if (ddlNumberOfYears.Text.ToString().Trim() == "0") { NoOfyears = 0; } else { NoOfyears = Convert.ToDouble(ddlNumberOfYears.Text.ToString().Trim()); }

            string coverFire, coverOther, coverSrcc, coverTc, bpfRequest = string.Empty;
            if (chkFirLight.Checked == true)
            {
                coverFire = "Y";
            }
            else
            {
                coverFire = "N";
            }
            if (chkOtherPerils.Checked == true)
            {
                coverOther = "Y";
            }
            else
            {
                coverOther = "N";
            }
            if (chkSRCC.Checked == true)
            {
                coverSrcc = "Y";
            }
            else
            {
                coverSrcc = "N";
            }
            if (chkTR.Checked == true)
            {
                coverTc = "Y";
            }
            else
            {
                coverTc = "N";
            }

            if (chkBf.Checked == true)
            {
                bpfRequest = "Y";
            }
            else
            {
                bpfRequest = "N";
            }

           
            //----end-----------------------------------------

            sucess = this.exe_in.insert_fire_proposal_details(null, hfbankname.Value.ToString().Trim(), hfbranchname.Value.ToString().Trim(), (ddlInitials.SelectedValue.ToString()+ txt_nameOfProp.Text.Trim().ToString()),
                                        agentCode, agentName, txt_nic.Text.ToString().Trim(), txt_br.Text.ToString().Trim(),
                                        txt_addline1.Text.ToString().Trim(), txt_addline2.Text.ToString().Trim(), txt_addline3.Text.ToString().Trim(), add4,
                                        txt_tele.Text.ToString().Trim(), txt_email.Text.ToString().Trim(), txt_dweAdd1.Text.ToString().Trim(), txt_dweAdd2.Text.ToString().Trim(), txt_dweAdd3.Text.ToString().Trim(), dwell_add4,
                                        Convert.ToDateTime(txt_fromDate.Text.ToString().Trim()), Convert.ToDateTime(txt_toDate.Text.ToString().Trim()),
                                        chkl1.SelectedValue.ToString(), "", "", "", "",
                                        sumInsuVal1, sumInsuVal2,
                                        Convert.ToDouble(txt_sumInsuTotal.Text.Trim().ToString()), chkl4.SelectedValue.ToString(), txt_ninethReason.Text.Trim().ToString(),
                                        wBrick, wCement, dWooden, dMetal, fTile, fcement, rTile, rAsbas, rGI, rConcret,
                                        cFire, cLight, covFlood, chkl5.SelectedValue.ToString(),
                                        txt_wordReason1.Text.ToString().Trim(), txt_wordReason2.SelectedValue.ToString(), txt_wordReason3.SelectedValue.ToString(), txt_wordReason4.SelectedValue.ToString(), Session["userName_code"].ToString(),
                                        null, flag_hold, floorNumber, over_val_flag, overallFlag, "N", Session["bank_code"].ToString(),
                                        Session["branch_code"].ToString(), txt_landLine.Text.ToString().Trim(), bank_val,
                                        RbTermType.SelectedValue.ToString(), NoOfyears, coverFire, coverOther, coverSrcc, coverTc, coverFlood,
                                        hfpolType.Value.ToString().Trim(), solarVal, rbSolOne.SelectedValue.ToString(), rbSolTwo.SelectedValue.ToString(),
                                        txtSolarCountry.Text.ToString().Trim(), txtSoloarModel.Text.ToString().Trim(), NoOfyears,txtLoanNumber.Text.ToString().Trim(), ddlSlicCode.SelectedValue.ToString().Trim(), bpfRequest,
                                        out FPDRefNo);
    

        }
        catch (Exception ex)
        {
            //Response.End();
            //var endc = new EncryptDecrypt();
            //Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

        }
        var endc = new EncryptDecrypt();
        //----------------------------------------------------

      
        if (!string.IsNullOrEmpty(FPDRefNo) && sucess == true)
                {
                    //this.GetPhoneNumberofOfficers(FPDRefNo);
                    // PREMIUM CALCULATION AN DISPALY IN SECOND PAGE -------------->
                    double sumOnsu = Convert.ToDouble(txt_sumInsuTotal.Text.Trim().ToString());
                    string msg = "Successfully Saved.! ";
                    string ref_send = "Reference No: " + FPDRefNo; //FPDRefNo = "1234567890000";


            //---06092021--changes
                    ShedulCal.Visible = true;
                    mainDiv.Visible = false;
                    fd_ref_no = FPDRefNo;

                    txt_Ref_no.Text = FPDRefNo;
                    flag = flag_hold;
                    over_Val = over_val_flag;
                    sumInsu = Convert.ToDouble(sumOnsu);
                    sumInsuVal.Value = sumInsu.ToString();

                    lblreqsend.Visible = false;

            //wording for flood and over limit
            if (hfpolType.Value.ToString().Trim() == "1" || hfpolType.Value.ToString().Trim() == "3") //house
            {
                if (flag == "Y" && over_Val == "N")
                {
                    quo.Visible = true;
                    printProp.Visible = false;
                    spanRefNo.InnerHtml = "SLIC will attend and confirm the policy. Premium may change due to the risk of flood cover.";

                }

                else if (flag == "N" && over_Val == "Y")
                {
                    quo.Visible = true;
                    printProp.Visible = false;
                    spanRefNo.InnerHtml = "SLIC will attend and confirm the policy. Premium may change due to the risk of the insurance property.";
                }
                else if (flag == "Y" && over_Val == "Y") // both
                {
                    quo.Visible = true;
                    printProp.Visible = false;
                    spanRefNo.InnerHtml = "SLIC will attend and confirm the policy. Premium may change due to the risk of flood cover and the insurance property.";
                }
                else
                {
                    quo.Visible = false;
                    printProp.Visible = true;
                    spanRefNo.InnerHtml = "";

                }
            }
           
            else  //both
            { }
            //-------->>>>>>>end---------->>>
            if (flag == "Y" || over_Val == "Y")
                    {
                        quo.Visible = true;
                        printProp.Visible = false;

                    }

                    else
                    {
                        quo.Visible = false;
                        printProp.Visible = true;

                    }
         
             double BuildingsumInsu = sumInsuVal1 + sumInsuVal2; //building sum Insu
             this.PremiumCalculation(BuildingsumInsu, solarVal);
            //--end----

        }
        else
        {
            
            string msg = "Syetem Error".ToString();
            Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

           
        }
            //}

        //catch (System.Threading.ThreadAbortException)
        //{

        //}
        //catch (Exception ex)
        //    {
        //            //Response.End();
        //            var endc = new EncryptDecrypt();
        //            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));

            
        //    }
        //}
        //else { }

    }

    public void PremiumCalculation(double BuildingsumInsu,double solarVal)
    {
        try
        {    //calculation part--08092021-new modifications-------
            string IsLongTerm = "";
            double YearsCount = 0;

            if (RbTermType.SelectedValue.ToString() == "1")
            {
                YearsCount = 1; IsLongTerm = "A";
            }
            else
            {
                IsLongTerm = "L";
                if (ddlNumberOfYears.Text.ToString().Trim() == "0") { YearsCount = 1; } else { YearsCount = Convert.ToDouble(ddlNumberOfYears.Text.ToString().Trim()); }
            }

            //    SOLAR_RATE
            //    DISCOUNT_RATE
            //    RENEWAL_FEE 
            //--------------
            string coverFire, coverOther, coverSrcc, coverTc, bpfRequest = string.Empty;

            if (chkFirLight.Checked == true)
            {
                coverFire = "1";
            }
            else
            {
                coverFire = "0";
            }
            if (chkOtherPerils.Checked == true)
            {
                coverOther = "1";
            }
            else
            {
                coverOther = "0";
            }
            if (chkSRCC.Checked == true)
            {
                coverSrcc = "1";
            }
            else
            {
                coverSrcc = "0";
            }
            if (chkTR.Checked == true)
            {
                coverTc = "1";
            }
            else
            {
                coverTc = "0";
            }

            if (chkBf.Checked == true)
            {
                bpfRequest = "Y";
            }
            else
            {
                bpfRequest = "N";
            }
            bool rtn = false;

            rtn = premCal.PremiumCalculationForAllTypes(Session["bank_code"].ToString().Trim(), YearsCount, IsLongTerm, BuildingsumInsu, solarVal,
                  coverFire, coverOther, coverSrcc, coverTc, bpfRequest, Session["branch_code"].ToString().Trim(), Session["userName_code"].ToString().Trim(),
                  out calNetPre, out calRCC, out calTR, out calPolicyFee, out calAdminFee, out calRenewalFee, out calNBT, out calVat, out calTotal, out calBPF_Value, out rtnM);



            //----------dispaly values-------------------------------
            string cal_ref = "";

            if (flag == "Y")
            {   //for total premiums
             
                bool inserted = this.insert_.insert_fire_sche_cal_details(fd_ref_no, "", sumInsu, calNetPre, calRCC, calTR,
                 calAdminFee, calPolicyFee, calNBT, calVat, calTotal, calBPF_Value, "", Session["userName_code"].ToString(), "P", calRenewalFee, out cal_ref);
            }
            else
            {

               

                bool inserted = this.insert_.insert_fire_sche_cal_details(fd_ref_no, "", sumInsu, calNetPre, calRCC, calTR,
                    calAdminFee, calPolicyFee, calNBT, calVat, calTotal, calBPF_Value, "", Session["userName_code"].ToString(), "D", calRenewalFee, out cal_ref);


            }


            //dispaly settings------
            txt_NetPre.Text = calNetPre.ToString("n2");
            txt_adminFee.Text = calAdminFee.ToString("n2");
            txtTotalPay.Text = calTotal.ToString("n2");
            txt_nbt.Text = calNBT.ToString("n2");
            txt_vat.Text = calVat.ToString("n2");
            txtPoliFee.Text = calPolicyFee.ToString("n2");

            if (RbTermType.SelectedValue.ToString() == "1")
            {
                DivRenewalFee.Visible = false; //renewal fee
                txt_renewal.Text = calRenewalFee.ToString("n2");

                txtPolFee.InnerHtml = "Policy Fee";

                if (chkFirLight.Checked == true && chkOtherPerils.Checked == true && chkSRCC.Checked == true && chkTR.Checked == false)
                {
                    CalDiv4.Visible = true; //srcc row
                    txt_srcc.Text = calRCC.ToString("n2");
                    CalDiv5.Visible = false; //tc row
                    txt_tr.Text = calTR.ToString("n2");
                }
                else if (chkFirLight.Checked == true && chkOtherPerils.Checked == true && chkSRCC.Checked == true && chkTR.Checked == true)
                {
                    CalDiv4.Visible = true; //srcc row
                    txt_srcc.Text = calRCC.ToString("n2");
                    CalDiv5.Visible = true; //tc row
                    txt_tr.Text = calTR.ToString("n2");
                }
                else
                {
                    CalDiv4.Visible = false; //srcc row
                    txt_srcc.Text = calRCC.ToString("n2");
                    CalDiv5.Visible = false; //tc row
                    txt_tr.Text = calTR.ToString("n2");
                }
            }
            else
            {
                txtPolFee.InnerHtml = "Policy Fee for 1st year";
                DivRenewalFee.Visible = true; //renewal fee
                txt_renewal.Text = calRenewalFee.ToString("n2");

                if (chkFirLight.Checked == true && chkOtherPerils.Checked == true && chkSRCC.Checked == true && chkTR.Checked == false)
                {
                    CalDiv4.Visible = true; //srcc row
                    txt_srcc.Text = calRCC.ToString("n2");
                    CalDiv5.Visible = false; //tc row
                    txt_tr.Text = calTR.ToString("n2");
                }
                else if (chkFirLight.Checked == true && chkOtherPerils.Checked == true && chkSRCC.Checked == true && chkTR.Checked == true)
                {
                    CalDiv4.Visible = true; //srcc row
                    txt_srcc.Text = calRCC.ToString("n2");
                    CalDiv5.Visible = true; //tc row
                    txt_tr.Text = calTR.ToString("n2");
                }
                else
                {
                    CalDiv4.Visible = false; //srcc row
                    txt_srcc.Text = calRCC.ToString("n2");
                    CalDiv5.Visible = false; //tc row
                    txt_tr.Text = calTR.ToString("n2");
                }

            }

            //CalDiv7.Visible = false; //policy fee
           
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()), false);
        }

    }
    protected void GetAgentDetails(string bank_code)
    {
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetAgentDetails(bank_code), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {

                    agentCode = details.Rows[0]["AGENCY_CODE"].ToString();
                    agentName = details.Rows[0]["AGE_NAME"].ToString();
                    BGI = details.Rows[0]["BANCASS_GI"].ToString();

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

    protected bool NicValidate(string NIC)
    {
        bool nic_status = false;
        if (((NIC.Length == 10) && (NIC.EndsWith("X") || NIC.EndsWith("x") || NIC.EndsWith("V") || NIC.EndsWith("v")) && (NIC[2] != '4' && NIC[2] != '9')) || (NIC.Length == 12))
        {
            nic_status = true;
        }
        else
        {
            nic_status = false;
        }
        return nic_status;
    }

    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        ReturnCovers = getPropClass.getFireCoverArray();
    //        //lblCover.Text = ReturnCovers[1];
    //        //for (int i = 0; i < ReturnCovers.Length; i++)
    //        //{
    //        //    Label lbl = new Label();
    //        //    lbl.Text = "* " + ReturnCovers[i];
    //        //    dv1.Controls.Add(lbl);
    //        //    dv1.Controls.Add(new LiteralControl("<br/>"));
    //        //}
    //        string IsLongTerm = "";
    //        double YearsCount = 0;

    //        if (RbTermType.SelectedValue.ToString() == "1")
    //        {
    //            YearsCount = 1; IsLongTerm = "N";
    //        }
    //        else
    //        {
    //            IsLongTerm = "Y";
    //            if (ddlNumberOfYears.Text.ToString().Trim() == "0") { YearsCount = 1; } else { YearsCount = Convert.ToDouble(ddlNumberOfYears.Text.ToString().Trim()); }
    //        }

    //        this.createPolicyNumber(IsLongTerm);


    //        bool iSReq = this.exe_up.update_HoldFlagPrintOrNot(txt_Ref_no.Text.Trim().ToString());
    //        if (iSReq)
    //        {
    //            lblreqsend.Visible = false;
    //        }

    //        string Data = "";
    //        string PaymentDate = "";
    //        string debitno = "";

    //        // Fetch details from IRE_DH_SCHEDULE_CALC table using SC_REF_NO
    //        OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]);

    //        // Fetch policy number using refno
    //        using (conn)
    //        {
    //            if (conn.State != ConnectionState.Open)
    //            {
    //                conn.Open();
    //            }

    //            string sql_getdetails = "select DH.DH_BCODE,DH.DH_BBRCODE,DH.DH_NAME,DH.DH_AGECODE,DH.DH_AGENAME,DH.DH_NIC,DH.DH_BR,DH.DH_PADD1,DH.DH_PADD2,DH.DH_PADD3,DH.DH_PADD4,DH.DH_PHONE,DH.DH_IADD1,DH.DH_IADD2,DH.DH_IADD3,DH.DH_IADD4,DH.DH_PFROM,DH.DH_PTO,DH.DH_AFF_YES_REAS,DH.DH_COV_FLOOD,DH.DH_OVER_VAL,DH.DH_BCODE_ID,DH.DH_BBRCODE_ID,DH.LAND_PHONE,DH.TERM,DH.PERIOD,DH.FIRE_COVER,DH.OTHER_COVER,DH.SRCC_COVER,DH.TC_COVER,DH.FLOOD_COVER,DH.DH_SOLAR_SUM,DH.SLIC_CODE,DH.BPF,SC.SC_POLICY_NO,SC.SC_SUM_INSU,SC.SC_NET_PRE,SC.SC_RCC,SC.SC_TR,SC.SC_ADMIN_FEE,SC.SC_POLICY_FEE,SC.SC_NBT,SC.SC_VAT,SC.SC_TOTAL_PAY,SC.SC_RENEWAL_FEE FROM QUOTATION.FIRE_DH_PROPOSAL_ENTRY DH INNER JOIN QUOTATION.FIRE_DH_SCHEDULE_CALC SC ON DH.DH_REFNO = SC.SC_REF_NO where DH_REFNO = :refno";
    //            using (var cmd = new OracleCommand(sql_getdetails, conn))
    //            {
    //                cmd.Parameters.Add(new OracleParameter("refno", txt_Ref_no.Text.Trim()));
    //                using (var reader = cmd.ExecuteReader())
    //                {

    //                    if (reader.Read())
    //                    {
    //                        try
    //                        {


    //                            // Instantiate the API class and model
    //                            SaveDebitDetails_API api = new SaveDebitDetails_API();
    //                            SaveDebitDetails_API.DebitDetailsModel Debitdetailsmodel = new SaveDebitDetails_API.DebitDetailsModel();

    //                            // Map form data to model properties
    //                            //Debitdetailsmodel.sliBranch = 192;
    //                            Debitdetailsmodel.sliBranch = int.Parse(BGICal);
    //                            Debitdetailsmodel.policyNo = prinPolicyNumber;
    //                            //Debitdetailsmodel.policyNo = reader["SC_POLICY_NO"].ToString().Trim();
    //                            Debitdetailsmodel.commenceDate = DateTime.Now;
    //                            Debitdetailsmodel.policyType = "FPD";
    //                            Debitdetailsmodel.departmentCode = "F";
    //                            Debitdetailsmodel.subDepartment = "F";
    //                            Debitdetailsmodel.status = ddlInitials.SelectedValue; // First part before the dot
    //                            Debitdetailsmodel.name1 = txt_nameOfProp.Text;
    //                            Debitdetailsmodel.address1 = reader["DH_PADD1"].ToString().Trim();
    //                            Debitdetailsmodel.address2 = reader["DH_PADD2"].ToString().Trim();
    //                            Debitdetailsmodel.address3 = reader["DH_PADD3"].ToString().Trim();
    //                            Debitdetailsmodel.address4 = reader["DH_PADD4"].ToString().Trim();
    //                            //string sumInsuredString = sumInsuVal.Value.Replace("Rs.", "").Trim(); // Remove "Rs." and any surrounding spaces
    //                            //double sumInsuredValue = Convert.ToDouble(sumInsuredString); // Convert the remaining string to a double
    //                            //Debitdetailsmodel.sumInsured = (int)sumInsuredValue; // Round the double and cast it to an int

    //                            Debitdetailsmodel.sumInsured = reader.IsDBNull(reader.GetOrdinal("SC_SUM_INSU")) ? 0 : Convert.ToInt32(reader["SC_SUM_INSU"]);
    //                            Debitdetailsmodel.premium = reader.IsDBNull(reader.GetOrdinal("SC_NET_PRE")) ? 0 : Convert.ToDouble(reader["SC_NET_PRE"]);
    //                            Debitdetailsmodel.rcc = reader.IsDBNull(reader.GetOrdinal("SC_RCC")) ? 0 : Convert.ToDouble(reader["SC_RCC"]);
    //                            Debitdetailsmodel.tc = reader.IsDBNull(reader.GetOrdinal("SC_TR")) ? 0 : Convert.ToDouble(reader["SC_TR"]);
    //                            Debitdetailsmodel.vat = reader.IsDBNull(reader.GetOrdinal("SC_VAT")) ? 0 : Convert.ToDouble(reader["SC_VAT"]);
    //                            Debitdetailsmodel.taxStatusId = "1";
    //                            Debitdetailsmodel.cess = reader.IsDBNull(reader.GetOrdinal("SC_ADMIN_FEE")) ? 0 : Convert.ToDouble(reader["SC_ADMIN_FEE"]);
    //                            //Debitdetailsmodel.debtorCode = "4051";
    //                            Debitdetailsmodel.policyFee = (reader.IsDBNull(reader.GetOrdinal("SC_POLICY_FEE")) ? 0 : Convert.ToDouble(reader["SC_POLICY_FEE"])) + (reader.IsDBNull(reader.GetOrdinal("SC_RENEWAL_FEE")) ? 0 : Convert.ToDouble(reader["SC_RENEWAL_FEE"]));
    //                            Debitdetailsmodel.paymentAmount1 = reader.IsDBNull(reader.GetOrdinal("SC_TOTAL_PAY")) ? 0 : Convert.ToDouble(reader["SC_TOTAL_PAY"]);
    //                            //Debitdetailsmodel.agentCode = agentCode;
    //                            Debitdetailsmodel.agentCode = reader["DH_AGECODE"].ToString().Trim();
    //                            // Define the SQL query to get DEBIT_CODE based on AGENCY_CODE
    //                            string query2 = "SELECT DEBIT_CODE FROM quotation.fire_agent_info WHERE AGENCY_CODE = :agentCode";
    //                            string debtorCode = "";
    //                            using (OracleCommand cmd2 = new OracleCommand(query2, conn))
    //                            {
    //                                cmd2.Parameters.Add(new OracleParameter(":agentCode", Debitdetailsmodel.agentCode));


    //                                using (OracleDataReader reader2 = cmd2.ExecuteReader())
    //                                {
    //                                    if (reader2.Read())
    //                                    {
    //                                        debtorCode = reader2["DEBIT_CODE"] != DBNull.Value ? reader2["DEBIT_CODE"].ToString() : "";

    //                                    }
    //                                    else
    //                                    {
    //                                        // If no matching AGENCY_CODE found, you can decide to set debtorCode to null or leave it unchanged
    //                                        Debitdetailsmodel.debtorCode = null; // Or leave it as it is
    //                                    }
    //                                }
    //                            }
    //                            Debitdetailsmodel.debtorCode = debtorCode;

    //                            Debitdetailsmodel.rateCode = "N";

    //                            Debitdetailsmodel.sliNo2 = reader["SLIC_CODE"] != DBNull.Value ? Convert.ToInt32(reader["SLIC_CODE"]) : 0;

    //                            if (Debitdetailsmodel.sliNo2 > 0)
    //                            {
    //                                // Split SLIC_CODE into mslino1 (first two digits) and mslino2 (remaining digits)
    //                                int slicCode = Debitdetailsmodel.sliNo2;
    //                                string slicCodeStr = slicCode.ToString();

    //                                int mslino1 = Convert.ToInt32(slicCodeStr.Substring(0, 2)); // First two digits
    //                                int mslino2 = Convert.ToInt32(slicCodeStr.Substring(2));    // Remaining digits

    //                                // Fetch additional details using mslino1 and mslino2
    //                                string query = "SELECT mslino1, branch_code FROM genpay.slidet WHERE mslino1 = :mslino1 AND mslino2 = :mslino2";
    //                                string branchCode = ""; // Initialize branchCode with a default value

    //                                using (OracleCommand cmd2 = new OracleCommand(query, conn))
    //                                {
    //                                    // Add parameters for mslino1 and mslino2
    //                                    cmd2.Parameters.Add(":mslino1", OracleType.Int32).Value = mslino1;
    //                                    cmd2.Parameters.Add(":mslino2", OracleType.Int32).Value = mslino2;

    //                                    using (OracleDataReader reader2 = cmd2.ExecuteReader())
    //                                    {
    //                                        if (reader2.Read())
    //                                        {
    //                                            // Fetch the branch code from the result
    //                                            branchCode = reader2["branch_code"] != DBNull.Value ? reader2["branch_code"].ToString() : "";
    //                                        }
    //                                    }
    //                                }

    //                                // Update Debitdetailsmodel fields
    //                                Debitdetailsmodel.sliNo1 = mslino1; // Set sliNo1 to the first two digits
    //                                Debitdetailsmodel.sliNo2 = mslino2; // Set sliNo2 to the remaining digits

    //                                // Set serviceBranch to the fetched branchCode or use fallback value
    //                                Debitdetailsmodel.serviceBranch = !string.IsNullOrEmpty(branchCode) ? branchCode : BGICal;
    //                            }
    //                            else
    //                            {
    //                                // Handle case where SLIC_CODE is not selected or is zero
    //                                Debitdetailsmodel.sliNo1 = 0;
    //                                Debitdetailsmodel.sliNo2 = 0;
    //                                Debitdetailsmodel.serviceBranch = BGICal; // Fallback branch code
    //                            }

    //                            //Debitdetailsmodel.startDate = DateTime.Now;

    //                            DateTime startDate;
    //                            if (DateTime.TryParse(reader["DH_PFROM"].ToString(), out startDate))
    //                            {
    //                                Debitdetailsmodel.startDate = startDate;
    //                            }
    //                            else
    //                            {
    //                                // Handle the case where the date is not valid, maybe assign a default value
    //                                Debitdetailsmodel.startDate = DateTime.MinValue; // or any default value you prefer
    //                            }

    //                            DateTime expireDate;
    //                            if (DateTime.TryParse(reader["DH_PTO"].ToString(), out expireDate))
    //                            {
    //                                Debitdetailsmodel.expireDate = expireDate;
    //                            }
    //                            else
    //                            {
    //                                // Handle the case where the date is not valid, maybe assign a default value
    //                                Debitdetailsmodel.expireDate = DateTime.MinValue; // or any default value you prefer
    //                            }

    //                            //Debitdetailsmodel.expireDate = DateTime.Now.AddYears(1); // Assuming 1-year duration
    //                            Debitdetailsmodel.paymentDate = DateTime.Now;
    //                            Debitdetailsmodel.paymentMode = 1;
    //                            Debitdetailsmodel.currencyCode = "LKR";
    //                            Debitdetailsmodel.taxStatusId = "";
    //                            Debitdetailsmodel.businessTypeID = 1;
    //                            Debitdetailsmodel.createdUser = "999999";
    //                            Debitdetailsmodel.nbt = reader.IsDBNull(reader.GetOrdinal("SC_NBT")) ? 0 : Convert.ToDouble(reader["SC_NBT"]);
    //                            //Debitdetailsmodel.serviceBranch = BGICal;
    //                            Debitdetailsmodel.clientId = hiddenClientId.Value;


    //                            // Call the API to save the debit note details (synchronously)
    //                            var result = api.AddDetails(Debitdetailsmodel).Result;  // Use .Result to wait for the task to complete synchronously




    //                            if (result.ResponseCode == 200)
    //                            {

    //                                Data = result.Data.ToString();
    //                                PaymentDate = Debitdetailsmodel.paymentDate.ToString("yyyy-MM-dd");

    //                                string year = DateTime.Now.Year.ToString();
    //                                debitno = "D/" + year + "/" + BGICal + "/21/0000" + Data;


    //                                bool DebitNote = exe_up.update_DebitNoteInCalTable(txt_Ref_no.Text.Trim(), Data, PaymentDate, debitno, BGICal);

    //                                string fd_ref = txt_Ref_no.Text.Trim();
    //                                string fd_flag = "Y";
    //                                string fd_sum = sumInsuVal.Value;


    //                                var endc = new EncryptDecrypt();
    //                                //Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("1".ToString()) + "&ref_send=" + endc.Encrypt(ref_send),false);
    //                                Response.Redirect("PrintSchedule.aspx?Ref_no=" + endc.Encrypt(fd_ref) + "&Flag=" + endc.Encrypt(fd_flag) + "&Data=" + endc.Encrypt(Data) + "&PaymentDate=" + endc.Encrypt(PaymentDate) + "&BANCGI=" + endc.Encrypt(BGICal) + "&sumInsu=" + endc.Encrypt(fd_sum.ToString()), false);
    //                            }

    //                            // Handling the case when ResponseCode is 400
    //                            else if (result.ResponseCode == 400)
    //                            {
    //                                string warningScript = "custom_alert1('Debit Note not created. Please click the print button again.', 'Alert');";
    //                                ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWarningMessage", warningScript, true);
    //                            }
    //                        }

    //                        catch (FormatException ex)
    //                        {

    //                        }
    //                    }
    //                }
    //            }
    //        }
    //    }

    //    catch (Exception ex)
    //    {

    //    }

    //}


    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            ReturnCovers = getPropClass.getFireCoverArray();
            //lblCover.Text = ReturnCovers[1];
            //for (int i = 0; i < ReturnCovers.Length; i++)
            //{
            //    Label lbl = new Label();
            //    lbl.Text = "* " + ReturnCovers[i];
            //    dv1.Controls.Add(lbl);
            //    dv1.Controls.Add(new LiteralControl("<br/>"));
            //}
            string IsLongTerm = "";
            double YearsCount = 0;

            if (RbTermType.SelectedValue.ToString() == "1")
            {
                YearsCount = 1; IsLongTerm = "N";
            }
            else
            {
                IsLongTerm = "Y";
                if (ddlNumberOfYears.Text.ToString().Trim() == "0") { YearsCount = 1; } else { YearsCount = Convert.ToDouble(ddlNumberOfYears.Text.ToString().Trim()); }
            }

            this.createPolicyNumber(IsLongTerm);


            bool iSReq = this.exe_up.update_HoldFlagPrintOrNot(txt_Ref_no.Text.Trim().ToString());
            if (iSReq)
            {
                lblreqsend.Visible = false;
            }
            string Data = "";
            string PaymentDate = "";
            string debitno = "";

            // Fetch details from IRE_DH_SCHEDULE_CALC table using SC_REF_NO
            OracleConnection conn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]);

            // Fetch policy number using refno
            using (conn)
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                string sql_policydetails = "select DH.DH_BCODE,DH.DH_BBRCODE,DH.DH_NAME,DH.DH_AGECODE,DH.DH_AGENAME,DH.DH_NIC,DH.DH_BR,DH.DH_PADD1,DH.DH_PADD2,DH.DH_PADD3,DH.DH_PADD4,DH.DH_PHONE,DH.DH_EMAIL,DH.DH_IADD1,DH.DH_IADD2,DH.DH_IADD3,DH.DH_IADD4,DH.DH_PFROM,DH.DH_PTO,DH.DH_AFF_YES_REAS,DH.DH_COV_FLOOD,DH.DH_OVER_VAL,DH.DH_BCODE_ID,DH.DH_BBRCODE_ID,DH.LAND_PHONE,DH.TERM,DH.PERIOD,DH.FIRE_COVER,DH.OTHER_COVER,DH.SRCC_COVER,DH.TC_COVER,DH.FLOOD_COVER,DH.DH_SOLAR_SUM,DH.SLIC_CODE,DH.BPF,SC.SC_POLICY_NO,SC.SC_SUM_INSU,SC.SC_NET_PRE,SC.SC_RCC,SC.SC_TR,SC.SC_ADMIN_FEE,SC.SC_POLICY_FEE,SC.SC_NBT,SC.SC_VAT,SC.SC_TOTAL_PAY,SC.SC_RENEWAL_FEE FROM QUOTATION.FIRE_DH_PROPOSAL_ENTRY DH INNER JOIN QUOTATION.FIRE_DH_SCHEDULE_CALC SC ON DH.DH_REFNO = SC.SC_REF_NO where DH_REFNO = :refno";
                using (var cmd = new OracleCommand(sql_policydetails, conn))
                {
                    cmd.Parameters.Add(new OracleParameter("refno", txt_Ref_no.Text.Trim()));
                    using (var reader = cmd.ExecuteReader())
                    {

                        if (reader.Read())
                        {
                            try
                            {

                                SavePolicyDetails_API savePolicyDetails_API = new SavePolicyDetails_API();
                                SavePolicyDetails_API.PolicyModel policyModel = new SavePolicyDetails_API.PolicyModel();


                                policyModel.policyNo = prinPolicyNumber;
                                policyModel.commenceDate = DateTime.Now;
                                policyModel.status = ddlInitials.SelectedValue; // First part before the dot                            
                                string fullNamenew = txt_nameOfProp.Text;

                                // Ensure the text is not null and split into name1 and name2
                                if (!string.IsNullOrEmpty(fullNamenew))
                                {
                                    policyModel.name1 = fullNamenew.Length > 50 ? fullNamenew.Substring(0, 50) : fullNamenew;
                                    policyModel.name2 = fullNamenew.Length > 50 ? fullNamenew.Substring(50) : null;
                                }
                                else
                                {
                                    policyModel.name1 = null;
                                    policyModel.name2 = null;
                                }
                                policyModel.address1 = reader["DH_PADD1"].ToString().Trim();
                                policyModel.address2 = reader["DH_PADD2"].ToString().Trim();
                                policyModel.address3 = reader["DH_PADD3"].ToString().Trim();
                                policyModel.address4 = reader["DH_PADD4"].ToString().Trim();
                                policyModel.telePhone1 = reader.IsDBNull(reader.GetOrdinal("DH_PHONE")) ? 0 : Convert.ToInt32(reader["DH_PHONE"]);
                                policyModel.faxNo = 0;
                                policyModel.email = reader["DH_EMAIL"].ToString().Trim();
                                policyModel.policyType = "FPD";
                                policyModel.sumInsured = reader.IsDBNull(reader.GetOrdinal("SC_SUM_INSU")) ? 0 : Convert.ToInt32(reader["SC_SUM_INSU"]);
                                policyModel.premium = reader.IsDBNull(reader.GetOrdinal("SC_NET_PRE")) ? 0 : Convert.ToDouble(reader["SC_NET_PRE"]);
                                policyModel.rcc = reader.IsDBNull(reader.GetOrdinal("SC_RCC")) ? 0 : Convert.ToDouble(reader["SC_RCC"]);
                                policyModel.tc = reader.IsDBNull(reader.GetOrdinal("SC_TR")) ? 0 : Convert.ToDouble(reader["SC_TR"]);
                                policyModel.vat = reader.IsDBNull(reader.GetOrdinal("SC_VAT")) ? 0 : Convert.ToDouble(reader["SC_VAT"]);
                                policyModel.cess = reader.IsDBNull(reader.GetOrdinal("SC_ADMIN_FEE")) ? 0 : Convert.ToDouble(reader["SC_ADMIN_FEE"]);
                                policyModel.roadTax = 0;
                                policyModel.policyFee = (reader.IsDBNull(reader.GetOrdinal("SC_POLICY_FEE")) ? 0 : Convert.ToDouble(reader["SC_POLICY_FEE"])) + (reader.IsDBNull(reader.GetOrdinal("SC_RENEWAL_FEE")) ? 0 : Convert.ToDouble(reader["SC_RENEWAL_FEE"]));
                                policyModel.rateCode = "N";
                                policyModel.rateCode2 = "0";
                                policyModel.agentCode = reader.IsDBNull(reader.GetOrdinal("DH_AGECODE")) ? 0 : Convert.ToInt32(reader["DH_AGECODE"]);
                                policyModel.organizationCode = 0;

                                // Define the SQL query to get DEBIT_CODE based on AGENCY_CODE
                                string debitdetailsget = "SELECT DEBIT_CODE FROM quotation.fire_agent_info WHERE AGENCY_CODE = :agentCode";
                                int debtorCodenew = 0;
                                using (OracleCommand cmd2 = new OracleCommand(debitdetailsget, conn))
                                {
                                    cmd2.Parameters.Add(new OracleParameter(":agentCode", policyModel.agentCode));


                                    using (OracleDataReader reader2 = cmd2.ExecuteReader())
                                    {
                                        if (reader2.Read())
                                        {
                                            debtorCodenew = reader2.IsDBNull(reader2.GetOrdinal("DEBIT_CODE")) ? 0 : Convert.ToInt32(reader2["DEBIT_CODE"]);

                                        }
                                        else
                                        {
                                            // If no matching AGENCY_CODE found, you can decide to set debtorCode to null or leave it unchanged
                                            policyModel.debtorCode = 0; // Or leave it as it is
                                        }
                                    }
                                }
                                policyModel.debtorCode = debtorCodenew;


                                DateTime startDatenew;
                                if (DateTime.TryParse(reader["DH_PFROM"].ToString(), out startDatenew))
                                {
                                    policyModel.startDate = startDatenew;
                                }
                                else
                                {
                                    // Handle the case where the date is not valid, maybe assign a default value
                                    policyModel.startDate = DateTime.MinValue; // or any default value you prefer
                                }

                                DateTime expireDatenew;
                                if (DateTime.TryParse(reader["DH_PTO"].ToString(), out expireDatenew))
                                {
                                    policyModel.expireDate = expireDatenew;
                                }
                                else
                                {
                                    // Handle the case where the date is not valid, maybe assign a default value
                                    policyModel.expireDate = DateTime.MinValue; // or any default value you prefer
                                }

                                policyModel.paymentDate = DateTime.Now;

                                policyModel.paymentBranch = BGICal;
                                policyModel.currencyCode = "LKR";
                                policyModel.paymentType = "1";

                                policyModel.sliNo2 = reader["SLIC_CODE"] != DBNull.Value ? Convert.ToInt32(reader["SLIC_CODE"]) : 0;

                                if (policyModel.sliNo2 > 0)
                                {
                                    // Split SLIC_CODE into mslino1 (first two digits) and mslino2 (remaining digits)
                                    int slicCode = policyModel.sliNo2;
                                    string slicCodeStr = slicCode.ToString();

                                    int mslino1 = Convert.ToInt32(slicCodeStr.Substring(0, 2)); // First two digits
                                    int mslino2 = Convert.ToInt32(slicCodeStr.Substring(2));    // Remaining digits

                                    // Fetch additional details using mslino1 and mslino2
                                    string query = "SELECT mslino1, branch_code FROM genpay.slidet WHERE mslino1 = :mslino1 AND mslino2 = :mslino2";
                                    string branchCode = ""; // Initialize branchCode with a default value

                                    using (OracleCommand cmd2 = new OracleCommand(query, conn))
                                    {
                                        // Add parameters for mslino1 and mslino2
                                        cmd2.Parameters.Add(":mslino1", OracleType.Int32).Value = mslino1;
                                        cmd2.Parameters.Add(":mslino2", OracleType.Int32).Value = mslino2;

                                        using (OracleDataReader reader2 = cmd2.ExecuteReader())
                                        {
                                            if (reader2.Read())
                                            {
                                                // Fetch the branch code from the result
                                                branchCode = reader2["branch_code"] != DBNull.Value ? reader2["branch_code"].ToString() : "";
                                            }
                                        }
                                    }

                                    // Update Debitdetailsmodel fields
                                    policyModel.sliNo1 = mslino1; // Set sliNo1 to the first two digits
                                    policyModel.sliNo2 = mslino2; // Set sliNo2 to the remaining digits

                                }
                                else
                                {
                                    // Handle case where SLIC_CODE is not selected or is zero
                                    policyModel.sliNo1 = 0;
                                    policyModel.sliNo2 = 0;
                                }

                                policyModel.stampFee = 0;
                                policyModel.department = "F";
                                policyModel.vatRate = 15;
                                policyModel.nbt = reader.IsDBNull(reader.GetOrdinal("SC_NBT")) ? 0 : Convert.ToDouble(reader["SC_NBT"]);
                                policyModel.createdDate = DateTime.Now;
                                policyModel.createdUser = 999999;
                                policyModel.isUnderWritingPolicy = "N";
                                policyModel.clientId = hiddenClientId.Value;
                                policyModel.vatRegNo = "";
                                policyModel.sVatRegNo = "0";
                                policyModel.sVatAmount = 0;
                                policyModel.subAgentCode = 0;
                                policyModel.vesselRegNo = "";

                                var SaveDebitApiResponse = savePolicyDetails_API.AddPolicy(policyModel).Result;

                                if (SaveDebitApiResponse.ResponseCode == 200)
                                {

                                    // Instantiate the API class and model
                                    SaveDebitDetails_API api = new SaveDebitDetails_API();
                                    SaveDebitDetails_API.DebitDetailsModel Debitdetailsmodel = new SaveDebitDetails_API.DebitDetailsModel();

                                    // Map form data to model properties
                                    //Debitdetailsmodel.sliBranch = 192;
                                    Debitdetailsmodel.sliBranch = int.Parse(BGICal);
                                    Debitdetailsmodel.policyNo = prinPolicyNumber;
                                    Debitdetailsmodel.policyType = "FPD";
                                    Debitdetailsmodel.departmentCode = "F";
                                    Debitdetailsmodel.subDepartment = "F";
                                    Debitdetailsmodel.commenceDate = DateTime.Now;
                                    Debitdetailsmodel.status = ddlInitials.SelectedValue; // First part before the dot

                                    //Debitdetailsmodel.name1 = txt_nameOfProp.Text;


                                    string fullName = txt_nameOfProp.Text;

                                    // Ensure the text is not null and split into name1 and name2
                                    if (!string.IsNullOrEmpty(fullName))
                                    {
                                        Debitdetailsmodel.name1 = fullName.Length > 50 ? fullName.Substring(0, 50) : fullName;
                                        Debitdetailsmodel.name2 = fullName.Length > 50 ? fullName.Substring(50) : null;
                                    }
                                    else
                                    {
                                        Debitdetailsmodel.name1 = null;
                                        Debitdetailsmodel.name2 = null;
                                    }
                                    Debitdetailsmodel.address1 = reader["DH_PADD1"].ToString().Trim();
                                    Debitdetailsmodel.address2 = reader["DH_PADD2"].ToString().Trim();
                                    Debitdetailsmodel.address3 = reader["DH_PADD3"].ToString().Trim();
                                    Debitdetailsmodel.address4 = reader["DH_PADD4"].ToString().Trim();

                                    //string sumInsuredString = sumInsuVal.Value.Replace("Rs.", "").Trim(); // Remove "Rs." and any surrounding spaces
                                    //double sumInsuredValue = Convert.ToDouble(sumInsuredString); // Convert the remaining string to a double
                                    //Debitdetailsmodel.sumInsured = (int)sumInsuredValue; // Round the double and cast it to an int
                                    Debitdetailsmodel.sumInsured = reader.IsDBNull(reader.GetOrdinal("SC_SUM_INSU")) ? 0 : Convert.ToInt32(reader["SC_SUM_INSU"]);
                                    Debitdetailsmodel.premium = reader.IsDBNull(reader.GetOrdinal("SC_NET_PRE")) ? 0 : Convert.ToDouble(reader["SC_NET_PRE"]);
                                    Debitdetailsmodel.rcc = reader.IsDBNull(reader.GetOrdinal("SC_RCC")) ? 0 : Convert.ToDouble(reader["SC_RCC"]);
                                    Debitdetailsmodel.tc = reader.IsDBNull(reader.GetOrdinal("SC_TR")) ? 0 : Convert.ToDouble(reader["SC_TR"]);
                                    Debitdetailsmodel.vat = reader.IsDBNull(reader.GetOrdinal("SC_VAT")) ? 0 : Convert.ToDouble(reader["SC_VAT"]);
                                    Debitdetailsmodel.taxStatusId = "1";
                                    Debitdetailsmodel.cess = reader.IsDBNull(reader.GetOrdinal("SC_ADMIN_FEE")) ? 0 : Convert.ToDouble(reader["SC_ADMIN_FEE"]);
                                    Debitdetailsmodel.roadTax = 0;
                                    Debitdetailsmodel.stampFee = 0;
                                    Debitdetailsmodel.policyFee = (reader.IsDBNull(reader.GetOrdinal("SC_POLICY_FEE")) ? 0 : Convert.ToDouble(reader["SC_POLICY_FEE"])) + (reader.IsDBNull(reader.GetOrdinal("SC_RENEWAL_FEE")) ? 0 : Convert.ToDouble(reader["SC_RENEWAL_FEE"]));
                                    Debitdetailsmodel.excess = 0;
                                    Debitdetailsmodel.organizationCode = 0;
                                    Debitdetailsmodel.paymentAmount1 = reader.IsDBNull(reader.GetOrdinal("SC_TOTAL_PAY")) ? 0 : Convert.ToDouble(reader["SC_TOTAL_PAY"]);
                                    //Debitdetailsmodel.agentCode = agentCode;
                                    Debitdetailsmodel.agentCode = reader["DH_AGECODE"].ToString().Trim();

                                    // Define the SQL query to get DEBIT_CODE based on AGENCY_CODE
                                    string query2 = "SELECT DEBIT_CODE FROM quotation.fire_agent_info WHERE AGENCY_CODE = :agentCode";
                                    string debtorCode = "";
                                    using (OracleCommand cmd2 = new OracleCommand(query2, conn))
                                    {
                                        cmd2.Parameters.Add(new OracleParameter(":agentCode", Debitdetailsmodel.agentCode));


                                        using (OracleDataReader reader2 = cmd2.ExecuteReader())
                                        {
                                            if (reader2.Read())
                                            {
                                                debtorCode = reader2["DEBIT_CODE"] != DBNull.Value ? reader2["DEBIT_CODE"].ToString() : "";

                                            }
                                            else
                                            {
                                                // If no matching AGENCY_CODE found, you can decide to set debtorCode to null or leave it unchanged
                                                Debitdetailsmodel.debtorCode = null; // Or leave it as it is
                                            }
                                        }
                                    }
                                    Debitdetailsmodel.debtorCode = debtorCode;

                                    Debitdetailsmodel.rateCode = "N";

                                    Debitdetailsmodel.sliNo2 = reader["SLIC_CODE"] != DBNull.Value ? Convert.ToInt32(reader["SLIC_CODE"]) : 0;

                                    if (Debitdetailsmodel.sliNo2 > 0)
                                    {
                                        // Split SLIC_CODE into mslino1 (first two digits) and mslino2 (remaining digits)
                                        int slicCode = Debitdetailsmodel.sliNo2;
                                        string slicCodeStr = slicCode.ToString();

                                        int mslino1 = Convert.ToInt32(slicCodeStr.Substring(0, 2)); // First two digits
                                        int mslino2 = Convert.ToInt32(slicCodeStr.Substring(2));    // Remaining digits

                                        // Fetch additional details using mslino1 and mslino2
                                        string query = "SELECT mslino1, branch_code FROM genpay.slidet WHERE mslino1 = :mslino1 AND mslino2 = :mslino2";
                                        string branchCode = ""; // Initialize branchCode with a default value

                                        using (OracleCommand cmd2 = new OracleCommand(query, conn))
                                        {
                                            // Add parameters for mslino1 and mslino2
                                            cmd2.Parameters.Add(":mslino1", OracleType.Int32).Value = mslino1;
                                            cmd2.Parameters.Add(":mslino2", OracleType.Int32).Value = mslino2;

                                            using (OracleDataReader reader2 = cmd2.ExecuteReader())
                                            {
                                                if (reader2.Read())
                                                {
                                                    // Fetch the branch code from the result
                                                    branchCode = reader2["branch_code"] != DBNull.Value ? reader2["branch_code"].ToString() : "";
                                                }
                                            }
                                        }

                                        // Update Debitdetailsmodel fields
                                        Debitdetailsmodel.sliNo1 = mslino1; // Set sliNo1 to the first two digits
                                        Debitdetailsmodel.sliNo2 = mslino2; // Set sliNo2 to the remaining digits

                                        // Set serviceBranch to the fetched branchCode or use fallback value
                                        Debitdetailsmodel.serviceBranch = !string.IsNullOrEmpty(branchCode) ? branchCode : BGICal;
                                    }
                                    else
                                    {
                                        // Handle case where SLIC_CODE is not selected or is zero
                                        Debitdetailsmodel.sliNo1 = 0;
                                        Debitdetailsmodel.sliNo2 = 0;
                                        Debitdetailsmodel.serviceBranch = BGICal; // Fallback branch code
                                    }

                                    //Debitdetailsmodel.startDate = DateTime.Now;

                                    DateTime startDate;
                                    if (DateTime.TryParse(reader["DH_PFROM"].ToString(), out startDate))
                                    {
                                        Debitdetailsmodel.startDate = startDate;
                                    }
                                    else
                                    {
                                        // Handle the case where the date is not valid, maybe assign a default value
                                        Debitdetailsmodel.startDate = DateTime.MinValue; // or any default value you prefer
                                    }

                                    DateTime expireDate;
                                    if (DateTime.TryParse(reader["DH_PTO"].ToString(), out expireDate))
                                    {
                                        Debitdetailsmodel.expireDate = expireDate;
                                    }
                                    else
                                    {
                                        // Handle the case where the date is not valid, maybe assign a default value
                                        Debitdetailsmodel.expireDate = DateTime.MinValue; // or any default value you prefer
                                    }

                                    //Debitdetailsmodel.expireDate = DateTime.Now.AddYears(1); // Assuming 1-year duration
                                    Debitdetailsmodel.paymentDate = DateTime.Now;
                                    Debitdetailsmodel.paymentMode = 1;
                                    Debitdetailsmodel.commissionRate = 15;
                                    Debitdetailsmodel.bankCode1 = 0;
                                    Debitdetailsmodel.bankCode2 = 0;
                                    Debitdetailsmodel.currencyCode = "LKR";
                                    Debitdetailsmodel.taxStatusId = "";
                                    Debitdetailsmodel.businessTypeID = 1;
                                    Debitdetailsmodel.createdUser = "999999";
                                    Debitdetailsmodel.districtID = 0;
                                    Debitdetailsmodel.riskLocationID = 0;
                                    Debitdetailsmodel.nbt = reader.IsDBNull(reader.GetOrdinal("SC_NBT")) ? 0 : Convert.ToDouble(reader["SC_NBT"]);
                                    Debitdetailsmodel.clientId = hiddenClientId.Value;
                                    Debitdetailsmodel.isWithHoldingTax = "N";
                                    Debitdetailsmodel.sVatRegNo = "0";
                                    Debitdetailsmodel.sVatAmount = 0;

                                    // Call the API to save the debit note details (synchronously)
                                    var result = api.AddDetails(Debitdetailsmodel).Result;  // Use .Result to wait for the task to complete synchronously



                                    if (result.ResponseCode == 200)
                                    {

                                        Data = result.Data.ToString();
                                        PaymentDate = Debitdetailsmodel.paymentDate.ToString("yyyy-MM-dd");

                                        string year = DateTime.Now.Year.ToString();
                                        debitno = "D/" + year + "/" + BGICal + "/21/0000" + Data;


                                        bool DebitNote = exe_up.update_DebitNoteInCalTable(txt_Ref_no.Text.Trim(), Data, PaymentDate, debitno, BGICal);

                                        string fd_ref = txt_Ref_no.Text.Trim();
                                        string fd_flag = "Y";
                                        string fd_sum = sumInsuVal.Value;


                                        var endc = new EncryptDecrypt();
                                        //Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("1".ToString()) + "&ref_send=" + endc.Encrypt(ref_send),false);
                                        Response.Redirect("PrintSchedule.aspx?Ref_no=" + endc.Encrypt(fd_ref) + "&Flag=" + endc.Encrypt(fd_flag) + "&Data=" + endc.Encrypt(Data) + "&PaymentDate=" + endc.Encrypt(PaymentDate) + "&BANCGI=" + endc.Encrypt(BGICal) + "&sumInsu=" + endc.Encrypt(fd_sum.ToString()), false);
                                    }

                                    // Handling the case when ResponseCode is 400
                                    else if (result.ResponseCode == 400)
                                    {
                                        string warningScript = "custom_alert1('Debit Note not created. Please click the print button again.', 'Alert');";
                                        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWarningMessage", warningScript, true);
                                    }
                                }

                                // Handling the case when ResponseCode is 400
                                else if (SaveDebitApiResponse.ResponseCode == 400)
                                {
                                    string warningScript = "custom_alert1('Debit Note not created. Please click the print button again.', 'Alert');";
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "PopupWarningMessage", warningScript, true);
                                }



                            }

                            catch (FormatException ex)
                            {

                            }
                        }
                    }
                }
            }
        }

        catch (Exception ex)
        {

        }

    }



    protected void createPolicyNumber(string IsLongTerm)
    {
        string currentMonth = DateTime.Now.ToString("MM");//DateTime.Now.Month.ToString();
        string currentYear = DateTime.Now.ToString("yy");//DateTime.Now.Year.ToString();
        string seq_return = "";
        string LongShort = "";

        if (IsLongTerm == "N") { LongShort = "FFPD"; } else if (IsLongTerm == "Y") { LongShort = "FPDL"; } else { LongShort = ""; }




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
        //FPDA
        if (IsLongTerm == "N") { LongShort = "FFPD"; } else if (IsLongTerm == "Y") { LongShort = "FPDL"; } else { LongShort = "FFPD"; }
        this.GetAgentDetailsCal(Session["bank_code"].ToString());
        //policy_number.InnerHtml = currentYear + currentMonth + BGI + seq_return;
        prinPolicyNumber = LongShort + currentYear + BGICal + seq_return;
        //prinPolicyNumber = "FFPD" + currentYear + currentMonth + BGI + seq_return;
        bool POL_NUM = exe_up.update_policyNumberInCalTable(txt_Ref_no.Text.Trim(), prinPolicyNumber);

        //boardraux table--------->>
        double YearsCountSecond = 0;
        int resultNo = 0;
        if (ddlNumberOfYears.Text.ToString().Trim() == "0") { YearsCountSecond = 1; } else { YearsCountSecond = Convert.ToDouble(ddlNumberOfYears.Text.ToString().Trim()); }

        if (IsLongTerm == "Y")
        {
            
            _boardraux.insert_Boardraux(prinPolicyNumber,Convert.ToDouble(txt_NetPre.Text.ToString().Trim()), txt_fromDate.Text.ToString().Trim(), Convert.ToDouble(txt_srcc.Text.ToString().Trim()),
                Convert.ToDouble(txt_tr.Text.ToString().Trim()), YearsCountSecond, Session["userName_code"].ToString(),"BANK", out resultNo);

        }
        else { }
    }


    protected void GetAgentDetailsCal(string bank_code)
    {
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetAgentDetails(bank_code), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                  
                    agentCodeCal = details.Rows[0]["AGENCY_CODE"].ToString();
                    agentNameCal = details.Rows[0]["AGE_NAME"].ToString();
                    BGICal = details.Rows[0]["BANCASS_GI"].ToString();
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



    protected void btback1_Click(object sender, EventArgs e)
    {
        Response.Redirect("ProposalEntry.aspx");
    }


    protected void GetPhoneNumberofOfficers(string req_id)
    {
        string officerContact = string.Empty;
        int rtnCount = 0;
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetOfficer(Session["bank_code"].ToString()), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    for (int i = 0; i < details.Rows.Count; i++)
                    {
                        officerContact = details.Rows[i]["CONTACT_NO"].ToString().Substring(1);
                        officerContact = "94" + officerContact;

                        string txt_body = "Fire Policy Approval Request from Bank. Reference ID : ";

                        this.insert_.Send_sms_to_customer(officerContact, req_id, txt_body, out rtnCount);

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

    protected void btsendrequest_Click(object sender, EventArgs e)
    {
        try
        {
            bool iSReq = this.exe_up.update_HoldFlagPrintOrNot(txt_Ref_no.Text.Trim().ToString());
            if (iSReq)
            {
                this.GetPhoneNumberofOfficers(txt_Ref_no.Text.Trim().ToString());

                this.sendEmailToSLICOfficer(Session["bank_code"].ToString());

                lblreqsend.Visible = true;
                btsendrequest.Enabled = false;

                string msg = "Approval Request Successfully Sent to SLIC.!";
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/MessagePages.aspx?error=" + endc.Encrypt("DONE".ToString()) + "&APP_MSG=" + endc.Encrypt(msg) + "&code=" + endc.Encrypt("6"), false);


                //email send to slic officers
            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("APP_ERROR".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(ex.Message.ToString()));
        }
    }

    protected void sendEmailToSLICOfficer(string req_id)
    {
        this.GetEmailsrofOfficers(Session["bank_code"].ToString());
        string ccEmails = string.Empty;
        string toEmails = string.Empty;


        ccEmails = String.Join(", ", emailIDForCC);
        toEmails = String.Join(", ", emailID);

        emailSend.fireRequestDetails("NR", Session["bank_code"].ToString(), toEmails, ccEmails, txt_Ref_no.Text.Trim().ToString(), Session["temp_bank"].ToString(), Session["temp_branch"].ToString(), Session["bancass_email"].ToString());

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
            details = orcle_trans.GetRows(this._sql.GetOfficerEmail(Session["bank_code"].ToString()), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    for (int i = 0; i < details.Rows.Count; i++)
                    {
                        emailID.Add(details.Rows[i]["EMAIL"].ToString());
                    }

                }
                else
                {
                    officerEmails = "";

                }
            }
            else
            {
                var endc = new EncryptDecrypt();
                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("ORCL".ToString()) + "&ORL_ERR=" + endc.Encrypt(orcle_trans.Error_Message.ToString()));

            }


            /// second  table for CC user emails

            detailsforCC = orcle_trans.GetRows(this._sql.GetOfficerEmailForCC(Session["bank_code"].ToString()), detailsforCC);

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


    //end---cal---->>>

    protected void rbflood_CheckedChanged(object sender, EventArgs e)
    {
        chkl5.ClearSelection();
    }

   protected void txt_addline4_SelectedIndexChanged(object sender, EventArgs e)
    {

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

    protected void txt_dweAdd4_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btCalBack_Click(object sender, EventArgs e)
    {
        ShedulCal.Visible = false;
        mainDiv.Visible = true;
        //DivTerm.Visible = true;
    }

    [System.Web.Services.WebMethod]
    public static string CheckEmail(string useroremail, string userMobile)
    {
        string bank = HttpContext.Current.Session["temp_bank"].ToString();
        string branch = HttpContext.Current.Session["temp_branch"].ToString();
       
        string retval = "";
        string result = "";
       
        using (OracleConnection conn = new OracleConnection())
        {
            conn.ConnectionString = ConfigurationManager.ConnectionStrings["CONN_STRING_ORCL"].ConnectionString;
            conn.Open();
            using (OracleCommand cmd = new OracleCommand("SLICCOMMON.PROC_AML_ONBOARD_LOG", conn))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("sysName", "BANCASSUARANCE");
                cmd.Parameters.AddWithValue("brCode", bank+" - "+ branch);
                cmd.Parameters.AddWithValue("nicNumber", useroremail);
                cmd.Parameters.AddWithValue("passportNumber", "");
                cmd.Parameters.AddWithValue("polNumber", "");
                cmd.Parameters.AddWithValue("phoneNumber", userMobile);
                


                cmd.Parameters.Add("responseVal", OracleType.VarChar,200).Direction = ParameterDirection.Output;

                OracleDataReader dr = cmd.ExecuteReader();
              
                result = cmd.Parameters["responseVal"].Value.ToString();
              

                dr.Close();

            }
            conn.Close();
        }


        if (result == "AML Listed Person")
        {
            retval = "true";
        }
        else
        {
            retval = "false";
        }

        return retval;
    }

    protected void InitializedListSLICCode(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
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