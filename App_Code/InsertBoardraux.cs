using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Globalization;

/// <summary>
/// Summary description for InsertBoardraux
/// </summary>
public class InsertBoardraux
{
    Oracle_Transaction or_trn = new Oracle_Transaction();
    bool Sucess_State;
    int Error_number;
    string _Error_Message;
    public InsertBoardraux()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public bool Trans_Sucess_State
    {
        get { return this.Sucess_State; }
        set { this.Sucess_State = value; }
    }

    /// <param name="Error_Code"></param>
    /// Return Orale Error Code
    public int Error_Code
    {
        get { return this.Error_number; }
        set { this.Error_number = value; }
    }

    /// <param name="Error_Code"></param>
    /// Return Orale Error_Message
    public string Error_Message
    {
        get { return this._Error_Message; }
        set { this._Error_Message = value; }
    }


    public bool insert_Boardraux(string polNumber, double netPremium, string startDate,  double srccVal, double tcVal, double noOfYears,
              string created_by,string finishedType,  out int responseVal)
    {
        responseVal = 0;
        bool result = false;
     
        ORCL_Connection orcl_con = new ORCL_Connection();
    
        try
        {
           
            DateTime loadedDate = DateTime.ParseExact(startDate, "dd/MM/yyyy", null);

            using (OracleConnection conn = orcl_con.GetConnection())
            {
                conn.Open();
                using (OracleCommand cmd = new OracleCommand("QUOTATION.PROC_BANCASSU_BORDEREAUX", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("polNumber", polNumber);
                    cmd.Parameters.AddWithValue("netPremium", netPremium);
                    //cmd.Parameters.AddWithValue("startDate", DateTime.Parse(startDate.ToString()));
                    cmd.Parameters.AddWithValue("startDate", loadedDate);
                    
                    cmd.Parameters.AddWithValue("srccVal", srccVal);
                    cmd.Parameters.AddWithValue("tcVal", tcVal);
                    cmd.Parameters.AddWithValue("noOfYears", noOfYears);
                    cmd.Parameters.AddWithValue("created_by", created_by);
                    cmd.Parameters.AddWithValue("finishedType", finishedType);
                    cmd.Parameters.Add("responseVal", OracleType.Number).Direction = ParameterDirection.Output;

                    OracleDataReader dr = cmd.ExecuteReader();

                    responseVal = Int32.Parse(cmd.Parameters["responseVal"].Value.ToString());

                    dr.Close();

                }
                conn.Close();
            }

            if (responseVal > 0)
            {

                result = true;
                responseVal = 0;


            }

            else
            {
                result = false;
            }




            //transaction.Commit();

        }

        catch (OracleException ex)
        {
            result = false;
            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
            //transaction.Rollback();
        }

        finally
        {
            //conn.Close();
            //conn.Dispose();
        }
        return result;
    }
}