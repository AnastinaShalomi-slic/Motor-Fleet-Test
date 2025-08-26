using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Fleet_Execute_Sql
/// </summary>
public class Fleet_Execute_Sql
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["CONN_STRING_ORCL"]);
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    ORCL_Connection orcl_con = new ORCL_Connection();

    public Fleet_Execute_Sql()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public string GetVehicleType()
    {
        string sql = string.Empty;

        sql = "select vehicle_type_id, vehicle_type from fleet.t_vehicle_type ";
        sql += "ORDER BY vehicle_type ASC";
        return sql;
    }

    public string GetPolicyType()
    {
        string sql = string.Empty;

        sql = "select policy_type_id, policy_type_name from fleet.policy_type ORDER BY policy_type_name ASC";

        return sql;
    }

    public string GetSubCatTypeByVehiId(string vehiId)
    {
        string sql = string.Empty;

        sql = "select sc.sub_cat_id, sc.sub_category from fleet.t_vehi_sub_cate vs ";
        sql += " inner join fleet.t_sub_cat sc on sc.sub_cat_id = vs.sub_category_id ";
        sql += " where vehicle_id = '"+vehiId+"' ";

        return sql;
    }

    public string GetCoverByVehiType(string vehiId)
    {
        string sql = string.Empty;

        sql = "select c.cover from fleet.t_vehi_cover vc ";
        sql += " inner join fleet.t_cover c on c.cover_id = vc.cover_id ";
        sql += " where vehicle_id = '"+vehiId+"' " ; 


        return sql;
    }
}