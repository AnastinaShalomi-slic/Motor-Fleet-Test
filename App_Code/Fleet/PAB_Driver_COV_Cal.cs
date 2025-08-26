using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PAB_Driver_COV_Cal
/// </summary>
public class PAB_Driver_COV_Cal
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]);
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    ORCL_Connection orcl_con = new ORCL_Connection();

    Execute_sql Exsql = new Execute_sql();
    public PAB_Driver_COV_Cal()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Dictionary<string, object> GetRateDetails(string subCateId)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();

        try
        {
            if (oconn.State != ConnectionState.Open)
                oconn.Open();

            string sql = "SELECT * FROM FLEET.QTRATEM1P WHERE MRTYPE = :subCateId";

            using (OracleCommand cmd = new OracleCommand(sql, oconn))
            {
                cmd.Parameters.Add(":subCateId", OracleType.VarChar).Value = subCateId;

                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows && reader.Read())
                    {
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string colName = reader.GetName(i);
                            object colValue = reader.IsDBNull(i) ? null : reader.GetValue(i);
                            result[colName] = colValue;
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {

            throw new Exception("Error fetching rate details: " + ex.Message);
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
                oconn.Close();
        }

        return result;
    }

    public double PAB_Rate_Calculation(int nuOfPasse, double sumInsureVal, double pabRate)
    {
        double pabVal = 0;

        pabVal = (pabRate * sumInsureVal) / 100 * nuOfPasse;


        return pabVal;
    }



}