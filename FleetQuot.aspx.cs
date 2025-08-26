using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FleetQuot : System.Web.UI.Page
{
    Fleet_Execute_Sql _sql = new Fleet_Execute_Sql();
    PAB_Driver_COV_Cal _sql2 = new PAB_Driver_COV_Cal();
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitializedListBranch(ddlVehicleType, "vehicle_type", "vehicle_type_id", this._sql.GetVehicleType(), "'vehicle_type_id'");
            this.InitializedListPolicy(ddlPolType, "policy_type_name", "policy_type_id", this._sql.GetPolicyType(), "'policy_type_id'");
           
        }
            
    }

    protected void InitializedListBranch(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {


        DataTable getrecordBranch = new DataTable();
        try
        {

            getrecordBranch = orcle_trans.GetRows(executor, getrecordBranch);

            if (orcle_trans.Trans_Sucess_State == true)
            {
                if (getrecordBranch.Rows.Count > 1)
                {
                    target_list.DataSource = getrecordBranch;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                    
                }

                else if (getrecordBranch.Rows.Count == 1)
                {

                    target_list.DataSource = getrecordBranch;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                }

                else
                {
                    var endc = new EncryptDecrypt();
                    string msg = "Error : Sorry " + target_desc + "can't initialized. Contact system administrator. Dated On :" + System.DateTime.Now.ToString();
                    Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

                }



            }
            else
            {
                var endc = new EncryptDecrypt();
                string msg = orcle_trans.Error_Message.ToString();
                Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

        }
    }

    protected void InitializedListPolicy(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {


        DataTable getrecordBranch = new DataTable();
        try
        {

            getrecordBranch = orcle_trans.GetRows(executor, getrecordBranch);

            if (orcle_trans.Trans_Sucess_State == true)
            {
                if (getrecordBranch.Rows.Count > 1)
                {
                    target_list.DataSource = getrecordBranch;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();


                }

                else if (getrecordBranch.Rows.Count == 1)
                {

                    target_list.DataSource = getrecordBranch;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                }

                else
                {

                    var endc = new EncryptDecrypt();
                    string msg = "Error : Sorry " + target_desc + "can't initialized. Contact system administrator. Dated On :" + System.DateTime.Now.ToString();
                    Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

                }



            }
            else
            {
                var endc = new EncryptDecrypt();
                string msg = orcle_trans.Error_Message.ToString();
                Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

        }
    }

    protected void ddlPolType_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedVehicleTypeId = ddlVehicleType.SelectedValue;

        this.InitializedListSubCat(ddlSubCat, "sub_category", "sub_cat_id", this._sql.GetSubCatTypeByVehiId(selectedVehicleTypeId), "'sub_category'");

        if (ddlVehicleType.SelectedValue != "0")
        {
            LoadCovers();
        }

    }

    protected void InitializedListSubCat(DropDownList target_list, string target_datafield, string target_value, string executor, string target_desc)
    {


        DataTable getrecordBranch = new DataTable();
        try
        {

            getrecordBranch = orcle_trans.GetRows(executor, getrecordBranch);

            if (orcle_trans.Trans_Sucess_State == true)
            {
                if (getrecordBranch.Rows.Count > 1)
                {
                    target_list.DataSource = getrecordBranch;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();

                }

                else if (getrecordBranch.Rows.Count == 1)
                {

                    target_list.DataSource = getrecordBranch;
                    target_list.DataTextField = target_datafield;
                    target_list.DataValueField = target_value;
                    target_list.DataBind();
                }

                else
                {

                    var endc = new EncryptDecrypt();
                    string msg = "Error : Sorry " + target_desc + "can't initialized. Contact system administrator. Dated On :" + System.DateTime.Now.ToString();
                    Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

                }



            }
            else
            {
                var endc = new EncryptDecrypt();
                string msg = orcle_trans.Error_Message.ToString();
                Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

            }
        }
        catch (Exception ex)
        {
            var endc = new EncryptDecrypt();
            string msg = ex.ToString();
            Response.Redirect("~/session_error/ErrorPage.aspx?APP_MSG=" + endc.Encrypt(msg.ToString()) + "&code=" + endc.Encrypt("2".ToString()), false);

        }
    }

    private void LoadCovers()
    {

        DataTable dt = new DataTable();
        string vehitype = ddlVehicleType.SelectedValue;

        if(vehitype != "0")
        {
            dt = orcle_trans.GetRows(this._sql.GetCoverByVehiType(vehitype), dt);
        }
        if (dt.Rows.Count > 0)
        {
            gvCovers.DataSource = dt;
            gvCovers.DataBind();
        }
        else
        {
            gvCovers.DataSource = null;
            gvCovers.DataBind();
        }
            
        
    }

    protected void btn_PAB_DRV_Cal(object sender, EventArgs e)
    {
        string subcat = ddlSubCat.SelectedValue;

        foreach (GridViewRow row in gvCovers.Rows)
        {
            CheckBox chkSelect = row.FindControl("chkSelect") as CheckBox;

            if (chkSelect != null && chkSelect.Checked)
            {
                string coverName = row.Cells[0].Text;  

                TextBox txtPassengers = row.FindControl("txtPassengers") as TextBox;
                string noOfPassengers = txtPassengers != null ? txtPassengers.Text : "";

                TextBox txtSumInsured = row.FindControl("txtSumInsured") as TextBox;
                string sumInsured = txtSumInsured != null ? txtSumInsured.Text : "0";

                var rate = _sql2.GetRateDetails(subcat);

                double mbrate = rate["MRPADCA"] != null ? Convert.ToDouble(rate["MRPADCA"]) : 0;

               double pab3Val = _sql2.PAB_Rate_Calculation(Convert.ToInt32(noOfPassengers), Convert.ToDouble(sumInsured), mbrate);

                pabRate.Text = pab3Val.ToString();
            }
        }


    }
}