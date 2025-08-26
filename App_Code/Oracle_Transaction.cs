using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OracleClient;
using System.Data;

/// <summary>
/// Summary description for Oracle_Transaction
/// </summary>
public class Oracle_Transaction
{
    ORCL_Connection orcl_con = new ORCL_Connection();
    

        bool Sucess_State;
        int Error_number;
        string _Error_Message;

    /// <param name="Sucess_State"></param>
    /// Return true if Query Execuit Else false
    public bool Trans_Sucess_State
    {
        get
        {
            return this.Sucess_State;
        }
        set
        {
            this.Sucess_State = value;
        }
    }

    /// <param name="Error_Code"></param>
    /// Return Orale Error Code
    public int Error_Code
    {
        get
        {
            return this.Error_number;
        }
        set
        {
            this.Error_number = value;
        }
    }

    /// <param name="Error_Code"></param>
    /// Return Orale Error_Message
    public string Error_Message
    {
        get
        {
            return this._Error_Message;
        }
        set
        {
            this._Error_Message = value;
        }
    }
    

    public string GetString(string Executer)
    {
        string string_out = string.Empty;

        using (OracleConnection con = orcl_con.GetConnection())
        {
            using (OracleCommand cmd = new OracleCommand(Executer, con))
            {
                try
                {
                    con.Open();
                    OracleDataReader oreader = cmd.ExecuteReader();
                    while (oreader.Read())
                    {
                        Trans_Sucess_State = true;

                        if (oreader.HasRows == true)
                        {
                            string_out = oreader[0].ToString().Trim();                    
                            break;
                        }
                    }
                }

                catch (OracleException ex)
                {
                    Trans_Sucess_State = false;
                    Error_Code = ex.ErrorCode;
                    Error_Message = ex.Message;
                }

                finally
                {
                    // always call Close when done reading.        
                    con.Close();
                    con.Dispose();
                }
            }
        }

        return string_out;
    }

    public List<string> GetStringList(string Executer)
    {
        List<string> stringList = new List<string>();

        using (OracleConnection con = orcl_con.GetConnection())
        using (OracleCommand cmd = new OracleCommand(Executer, con))
        {
            try
            {
                con.Open();
                using (OracleDataReader oreader = cmd.ExecuteReader())
                {
                    Trans_Sucess_State = true;

                    while (oreader.Read())
                    {
                        if (!oreader.IsDBNull(0)) // Ensure the value is not NULL
                        {
                            stringList.Add(oreader[0].ToString().Trim());
                            stringList.Add(oreader[1].ToString().Trim());
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                Trans_Sucess_State = false;
                Error_Code = ex.ErrorCode;
                Error_Message = ex.Message;
            }
            finally
            {
                con.Close();
            }
        }

        return stringList;
    }


    public bool ExecuteInsertQuery(string query)
    {
        try
        {   
            using(OracleConnection connection = orcl_con.GetConnection())
            {
                connection.Open();

                using (OracleCommand cmd = new OracleCommand(query, connection))
                {
                    cmd.CommandType = CommandType.Text;
                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; 
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
            return false;
        }
    }


    public DataTable GetRows(string query_statement, DataTable Result)
    {
        Result.Clear();
        using (OracleConnection connection = orcl_con.GetConnection())
        {
            try
            {
                connection.Open();
                OracleDataAdapter oraAdapt = new OracleDataAdapter(query_statement, connection);
                oraAdapt.Fill(Result);
                Trans_Sucess_State = true;

                oraAdapt.Dispose();
            }

            catch (OracleException ex)
            {
                //throw new Exception(ex.Message);
                Trans_Sucess_State = false;
                Error_Code = ex.ErrorCode;
                Error_Message = ex.Message;
            }

            finally
            {
                // always call Close when done reading.
                connection.Close();
                connection.Dispose();
            }
        }
        return Result;
    }



    public bool Is_authenticated(string search_Para, string TABLE_NAME)
    {
        bool HasRow_ = false;

        using (OracleConnection con = orcl_con.GetConnection())
        {
            using (OracleCommand cmd = new OracleCommand("SELECT USER_EPF FROM IBSL." + TABLE_NAME + " WHERE USER_EPF = :user_id", con))
            {
                try
                {
                    cmd.Parameters.AddWithValue(":user_id", search_Para);
                    con.Open();
                    OracleDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        if (dr.HasRows == true)
                        {
                            Trans_Sucess_State = true;
                            HasRow_ = true;
                            break;
                        }
                    }
                }

                catch (OracleException ex)
                {
                    Trans_Sucess_State = false;
                    Error_Code = ex.ErrorCode;
                    Error_Message = ex.Message;
                }

                finally
                {
                    // always call Close when done reading.        
                    con.Close();
                    con.Dispose();
                }
            }
        }

        return HasRow_;
    }

    public DataTable GetSpRows(object[] ExecParameter, DataTable Dt_Result)
    {
        Dt_Result.Clear();

        OracleConnection con = orcl_con.GetConnection();
        OracleCommand cmd = con.CreateCommand();
        cmd.Parameters.Clear();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SLIGEN." + ExecParameter[7]; //Name Of Stord Procedure 
        try
        {
            con.Open();
            OracleDataAdapter orcl_adeptor = new OracleDataAdapter(cmd);
            cmd.Parameters.Add("PIN_DEPT", OracleType.VarChar).Value = ExecParameter[0];
            cmd.Parameters.Add("PIN_SUB_DEPT", OracleType.VarChar).Value = ExecParameter[1];
            cmd.Parameters.Add("PIN_DATE_FROM", OracleType.VarChar).Value = ExecParameter[2];
            cmd.Parameters.Add("PIN_DATE_TO", OracleType.VarChar).Value = ExecParameter[3];
            cmd.Parameters.Add("PIN_OPERATOR", OracleType.VarChar).Value = ExecParameter[4];
            cmd.Parameters.Add("PIN_AMOUNT", OracleType.Number).Value = ExecParameter[5];
            cmd.Parameters.Add("PIN_IDENTITY", OracleType.VarChar).Value = ExecParameter[6];
            cmd.Parameters.Add("RESULTS", OracleType.Cursor).Direction = ParameterDirection.Output; //RECORD DET OUTER PARAMETER

            int exec = cmd.ExecuteNonQuery();
            orcl_adeptor.Fill(Dt_Result);

            Trans_Sucess_State = true;

            orcl_adeptor.Dispose();
        }

        catch (OracleException ex)
        {
            //throw new Exception(ex.Message);
            Trans_Sucess_State = false;

            Error_Code = ex.ErrorCode;
            Error_Message = ex.Message;
        }

        finally
        {
            cmd.Parameters.Clear();
            con.Close();
            con.Dispose();
        }

        return Dt_Result; //Return DataSet 
    }
}