using System;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;
using System.Web.UI.WebControls;

public partial class SLIC_Fire_ApprovedRenewal : System.Web.UI.Page
{
    // private string _connectionString = ConfigurationManager.ConnectionStrings["OracleDB"].ConnectionString;

    Oracle_Transaction Oracle_Trans = new Oracle_Transaction();
    Execute_sql _Sql = new Execute_sql();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
        }
    }

    /// <summary>
    /// Loads policy renewal details into the GridView.
    /// </summary>
    private void GetRenewedPolicyDetails(string start_date, string end_date, string pol_no, string app_status, string nIC)
    {
        DataTable RenewedPolicy_DT = new DataTable();
        


        try
        {
             RenewedPolicy_DT = Oracle_Trans.GetRows(_Sql.GetRenewedPolicyDetails(start_date,end_date,pol_no,"",""), RenewedPolicy_DT);


            if (RenewedPolicy_DT.Rows.Count > 1)
            {
                Grid_Details.Visible = true;
                Grid_Details.DataSource = RenewedPolicy_DT;
                Grid_Details.DataBind();
            }
        }
        catch (Exception ex)
        {
             // Log the error
            // Logger.WriteErrorLog($"Error in LoadPolicyRenewalDetails: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Handles page index changing event for GridView pagination.
    /// </summary>
    protected void Grid_Details_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid_Details.PageIndex = e.NewPageIndex;
        //LoadPolicyRenewalDetails(); // Reload data to reflect pagination
    }

    /// <summary>
    /// Handles the "Renew" button click event.
    /// </summary>
    protected void btn_renew_Click(object sender, EventArgs e)
    {
        try
        {
            Button btnRenew = (Button)sender;
            GridViewRow row = (GridViewRow)btnRenew.NamingContainer;
            string policyNo = row.Cells[1].Text; // Extracting Policy Number

            // Redirecting to the renewal page with the selected policy number
          //  Response.Redirect($"PolicyRenewal.aspx?policyNo={policyNo}");
        }
        catch (Exception ex)
        {
           // Logger.WriteErrorLog($"Error in btn_renew_Click: {ex.Message}", ex);
        }
    }

    private string GetSafeCellText(TableCell cell)
    {
        if (cell == null || string.IsNullOrEmpty(cell.Text) || cell.Text.Trim() == "" || cell.Text.Trim() == "&nbsp;")
        {
            return "";
        }
        return cell.Text.Trim();
    }


    protected void btn_find_Click(object sender, EventArgs e)
    {
        try
        {
            this.GetRenewedPolicyDetails(txt_start_date.Text, txt_to_date.Text, txt_pol_no.Text, "", txtNicNo.Text);
        }
        catch (Exception ex)
        {
            // Logger.WriteErrorLog($"Error in btn_renew_Click: {ex.Message}", ex);
        }
    }

    protected void btn_clear_Click(object sender, EventArgs e)
    {
        txt_start_date.Text = string.Empty;
        txt_to_date.Text = string.Empty;
        txt_pol_no.Text = string.Empty;
        txtNicNo.Text = string.Empty;
    }


}
