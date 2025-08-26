using System;


public partial class SessionTrans : System.Web.UI.Page
{
    ORCL_Connection orcl_con = new ORCL_Connection();
    AS400_Transaction as400 = new AS400_Transaction();
    Oracle_Transaction trans = new Oracle_Transaction();
    ExcuteOraSide ora_side = new ExcuteOraSide();
    EncryptDecrypt dc = new EncryptDecrypt();

    string usrEPF = string.Empty,
        usrBrCode = string.Empty,
        usrName = string.Empty,
        user_type = string.Empty,
        func_code = string.Empty,
        unserNAme = string.Empty,
        authLevelAssign = string.Empty,
        department = string.Empty;

    string user_name = string.Empty;
    string user_pw = string.Empty;


    string bank_code = string.Empty,
           branch_code = string.Empty,
           temp_user_name = string.Empty,
           bancass_email = string.Empty;

    string temp_bank = string.Empty,
           temp_bank_name = string.Empty,
           temp_branch = string.Empty,
           brName = string.Empty;
    int ReCount = 0;

    string marine, genAcc, fire, portfolio, multiDep = string.Empty;
    string tempmarine, tempgenAcc, tempfire, tempportfolio = string.Empty;
    LogFile Err = new LogFile();
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["EPFNum"] = string.Empty;
        Session["brcode"] = string.Empty;
        Session["UserId"] = string.Empty;

        Session["Branch_Name"] = string.Empty;
        Session["Auth_Code"] = "Main";
        Session["AdminForApproval"] = string.Empty;

        Session["bank_code"] = "";
        Session["branch_code"] = "";
        Session["userName_code"] = "";
        Session["bancass_email"] = "";

        Session["temp_bank"] = "";
        Session["temp_branch"] = "";
        Session["bank_name_code"] = "";
        Session["fireValidate"] = "";
        Session["QuoAcc"] = "";
        Session["UserType"] = "";
        Session["AccessAdmin"] = "";

        // for local run 
        // Session["UserId"] = "SEC9641";

        //for 
        //Session["brcode"] = "10";
        //Session["UserId"] = "SEC6752"; //Galle
        //Session["UserId"] = "SEC8324";
        //Session["brcode"] = "10";

        //Session["UserId"] = "SEC10249";

        //Session["UserId"] = "SEC4349";
        //Session["UserId"] = "SEC7126";
        //Session["UserId"] = "SEC5366"; 
        //Session["UserId"] = "SEC9064";
        //Session["UserId"] = "SEC0814";
        //Session["UserId"] = "SEC8528";
        //Session["UserId"] = "SEC6235";
        //Session["UserId"] = "SEC31916";
        //Session["UserId"] = "SEC5366";

        //fbd 
        //Session["UserId"] = "SEC6735";
        //for normal user
        //Session["UserId"] = "A370712";
        //Session["UserId"] = "A025864";
        //brCode.Value = "012"; //Session["brcode"].ToString();

        for (int i = 0; i < Request.Form.Count; i++)
        {
            Session[Request.Form.GetKey(i)] = Request.Form[i].ToString();
        }


        if (!Page.IsPostBack)
        {
            try
            {
                brCode.Value = Session["brcode"].ToString();
                UserId.Value = Session["UserId"].ToString();


                user_name = Session["UserId"].ToString();
                var en = new EncryptDecrypt();

                if (user_name != null)
                {
                    string UserID_Ora = user_name.ToString().ToUpper();


                    ora_side.ora_get_usrInfo(UserID_Ora, out usrEPF, out usrName, out usrBrCode, out user_type);

                    //added by shalomi for get authority for task assign and reassign fire departmet
                    //ora_side.get_usr_auth_for_assign(UserID_Ora, out unserNAme, out authLevelAssign, out department);
                    //Session["userEPF"] = UserID_Ora;
                    //Session["userNAme"] = unserNAme;
                    //Session["authLevelAssign"] = authLevelAssign;
                    //Session["department"] = department;

                    usrEPF = usrEPF.Trim();
                    usrBrCode = usrBrCode.Trim();
                    Session["EPFNum"] = usrEPF;
                    Session["brcode"] = Convert.ToInt32(usrBrCode).ToString();
                    Session["UsrName"] = usrName;
                    Session["UserId"] = UserID_Ora;
                    Session["userName_code"] = usrName;
                    Session["AdminForApproval"] = "Admin";
                    Session["UserType"] = "B";
                    ora_side.get_BranchName(usrBrCode, out brName);
                    Session["Branch_Name"] = brName;
                    //check admin or not fo user access in departments------->>

                    bool rtnAccess = ora_side.get_authorise(UserID_Ora, "FRNSMS", "Admin");
                    if (rtnAccess)
                    {
                        Session["AccessAdmin"] = "Y";

                    }
                    else
                    {
                        Session["AccessAdmin"] = "N";
                    }

                    bool brAccess = ora_side.get_authorise(UserID_Ora, "FRNSMS", "BADMIN");
                    if (brAccess)
                    {
                        Session["BranchAdmin"] = "Y";

                    }
                    else
                    {
                        Session["BranchAdmin"] = "N";
                    }

                    Response.Redirect("~/FireDefault.aspx", false);

                }
                else
                {
                    string msg = "You are not authorized to access this system. Account Locked. Please contact System Administrtor.";

                    Err.ErrorLog(Server.MapPath("Logs/ErrorLog"), msg);

                    var endc = new EncryptDecrypt();
                    Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("AuthLock".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(msg));

                    Response.Redirect("../../../Secworks/Signin.asp");
                }
            }
            catch (Exception ex)
            {
                var endc = new EncryptDecrypt();
                string msg = ex.ToString();

                Err.ErrorLog(Server.MapPath("Logs/ErrorLog"), msg);

                Response.Redirect("~/session_error/sessionError.aspx?error=" + endc.Encrypt("AuthLock".ToString()) + "&APP_ERROR_MSG=" + endc.Encrypt(msg));

                //Session.Clear();
                //Response.Redirect("../../../Secworks/Signin.asp");
            }
        }
        else
        {
            Session["UserId"] = UserId.Value;
            Session["brcode"] = brCode.Value;
        }
        ///--------------------end--------------------->>>>>>>>>>>



    }

}
