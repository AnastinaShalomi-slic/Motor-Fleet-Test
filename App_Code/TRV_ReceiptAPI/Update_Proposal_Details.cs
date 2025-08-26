using System;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

public class Update_Proposal_Details
{
    private OracleConnection oracleConnection;
    TrvApiLogger apiLogger = new TrvApiLogger();

    public Update_Proposal_Details()
    {
        string connectionString = ConfigurationManager.AppSettings["OracleDB"];
        oracleConnection = new OracleConnection(connectionString);
    }

    public bool UpdateReceiptNo(string sq_no, string ref_no)
    {
        bool returnValue = false;

        try
        {
            oracleConnection.Open();

            using (OracleCommand cmd = oracleConnection.CreateCommand())
            {
                // Begin a transaction
                OracleTransaction transaction = oracleConnection.BeginTransaction();
                cmd.Transaction = transaction;
                try
                {
                    string sql = "UPDATE slic_net.proposal_details SET auto_receipt_no = :receiptNo WHERE ref_no = :refNo";
                    cmd.CommandText = sql;
                    cmd.Parameters.Add("receiptNo", OracleType.VarChar).Value = sq_no;
                    cmd.Parameters.Add("refNo", OracleType.VarChar).Value = ref_no;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    transaction.Commit();
                    returnValue = rowsAffected > 0;
                }
                catch (Exception ex)
                {
                    // Rollback the transaction on error
                    transaction.Rollback();
                    apiLogger.WriteLog("Failed at Update_Proposal_Details: " + ex.ToString());
                }
            }
        }
        catch (Exception ex)
        {
            apiLogger.WriteLog("Failed at Update_Proposal_Details: " + ex.ToString());
        }
        finally
        {
            if (oracleConnection.State == ConnectionState.Open)
                oracleConnection.Close();
        }

        return returnValue;
    }
}
