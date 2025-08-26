using System;
using System.Data.OracleClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;

/// <summary>
/// Summary description for GetProposalDetails
/// </summary>
public class GetProposalDetails
{
    Oracle_Transaction orcle_trans = new Oracle_Transaction();
    Execute_sql _sql = new Execute_sql();
    Ip_Class get_ip = new Ip_Class();
    EncryptDecrypt dc = new EncryptDecrypt();

    DeviceFinder df = new DeviceFinder();
    public string[] ArryCovers = new string[13];

    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["DBConString"]);

    public GetProposalDetails()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public string GetEnteredDetails(

        string dh_refno,out string dh_bcode,out string dh_bbrcode,out string dh_name,out string dh_agecode,out string dh_agename,out string dh_nic,out string dh_br,
  out string dh_padd1,out string dh_padd2,out string dh_padd3,out string dh_padd4,out string dh_phone,out string dh_email,out string dh_iadd1,out string dh_iadd2,out string dh_iadd3,
  out string dh_iadd4,out string dh_pfrom,out string dh_pto,out string dh_uconstr,out string dh_occu_car,out string dh_occ_yes_reas,out string dh_haz_occu,out string dh_haz_yes_rea,
  out string dh_valu_build,out string dh_valu_wall,
  out string dh_valu_total,out string dh_aff_flood,out string dh_aff_yes_reas,out string dh_wbrick,
  out string dh_wcement,out string dh_dwooden,out string dh_dmetal,out string dh_ftile,out string dh_fcement,out string dh_rtile,
  out string dh_rasbes,out string dh_rgi, out string dh_rconcreat,out string dh_cov_fire,
  out string dh_cov_light,out string dh_cov_flood,out string dh_cfwateravl,out string dh_cfyesr1,out string dh_cfyesr2,out string dh_cfyesr3,out string dh_cfyesr4,
  out string dh_entered_by,out string dh_entered_on,out string dh_hold,out string DH_NO_OF_FLOORS, out string DH_OVER_VAL, out string DH_FINAL_FLAG,
  out string dh_isreq, out string dh_conditions, out string dh_isreject, out string dh_iscodi, out string dh_bcode_id, out string dh_bbrcode_id,out string DH_LOADING, out string DH_LOADING_VAL, out string LAND_PHONE, out string DH_VAL_BANKFAC, out string dh_deductible, out string dh_deductible_pre,
  out string TERM, out string Period, out string Fire_cover, out string Other_cover, out string SRCC_cover, out string TC_cover, out string Flood_cover,
  out string BANK_UPDATED_BY,
  out string BANK_UPDATED_ON,
  out string PROP_TYPE,
  out string DH_SOLAR_SUM,
  out string SOLAR_REPAIRE,
  out string SOLAR_PARTS,
  out string SOLAR_ORIGIN,
  out string SOLAR_SERIAL,
  out string Solar_Period,
  out string LOAN_NUMBER)

    {
        string mgs = "";


        dh_bcode = "";dh_bbrcode = "";dh_name = "";dh_agecode = "";dh_agename = "";dh_nic = "";dh_br = "";dh_padd1 = ""; dh_padd2 = "";dh_padd3 = "";dh_padd4 = "";dh_phone = "";
        dh_email = "";dh_iadd1 = "";dh_iadd2 = "";dh_iadd3 = "";dh_iadd4 = "";dh_pfrom = "";dh_pto = "";
        dh_uconstr = "";dh_occu_car = "";dh_occ_yes_reas = "";dh_haz_occu = "";dh_haz_yes_rea = "";
        dh_valu_build = "";dh_valu_wall = "";
        dh_valu_total = "";dh_aff_flood = "";dh_aff_yes_reas = "";dh_wbrick = ""; dh_wcement = "";
        dh_dwooden = "";dh_dmetal = "";dh_ftile = "";dh_fcement = "";dh_rtile = "";dh_rasbes = "";dh_rgi = "";dh_rconcreat = "";dh_cov_fire = "";
        dh_cov_light = "";dh_cov_flood = "";dh_cfwateravl = "";
        dh_cfyesr1 = "";dh_cfyesr2 = "";dh_cfyesr3 = "";dh_cfyesr4 = "";dh_entered_by = "";dh_entered_on = "";dh_hold = ""; DH_NO_OF_FLOORS = ""; DH_OVER_VAL = ""; DH_FINAL_FLAG = "";
        dh_isreq = ""; dh_conditions = ""; dh_isreject = ""; dh_iscodi = ""; dh_bcode_id = ""; dh_bbrcode_id = "";
        DH_LOADING = ""; DH_LOADING_VAL = ""; LAND_PHONE=""; DH_VAL_BANKFAC = ""; dh_deductible = ""; dh_deductible_pre = "";
        TERM = ""; Period = ""; Fire_cover = ""; Other_cover = ""; SRCC_cover = ""; TC_cover = ""; Flood_cover = "";
        BANK_UPDATED_BY = ""; BANK_UPDATED_ON = ""; PROP_TYPE = ""; DH_SOLAR_SUM = "";
        SOLAR_REPAIRE = ""; SOLAR_PARTS = ""; SOLAR_ORIGIN = ""; SOLAR_SERIAL = "";
        Solar_Period = ""; LOAN_NUMBER = "";
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {
                
                string sqlCountprop = "SELECT count(*) FROM QUOTATION.FIRE_DH_PROPOSAL_ENTRY where DH_REFNO = :txtReftNo";

                OracleParameter para1 = new OracleParameter();
                para1.Value = dh_refno.ToUpper().Trim();
                para1.ParameterName = "txtReftNo";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountprop;

                int countRef = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countRef == 1)
                {
                   

                    string sqlGetProptData = "select "+
                    "dh_bcode, dh_bbrcode, dh_name, dh_agecode, dh_agename, dh_nic, dh_br, dh_padd1, dh_padd2, dh_padd3, "+
                    "dh_padd4, dh_phone, dh_email, dh_iadd1, dh_iadd2, dh_iadd3, dh_iadd4, dh_pfrom, dh_pto, dh_uconstr, dh_occu_car, "+
                    "dh_occ_yes_reas, dh_haz_occu, dh_haz_yes_rea, dh_valu_build, "+
                    "dh_valu_wall, dh_valu_total, dh_aff_flood, dh_aff_yes_reas, dh_wbrick, dh_wcement, dh_dwooden, "+
                    "dh_dmetal, dh_ftile, dh_fcement, dh_rtile, dh_rasbes, dh_rgi, dh_rconcreat, "+
                    "dh_cov_fire, dh_cov_light, dh_cov_flood, dh_cfwateravl, dh_cfyesr1, dh_cfyesr2, dh_cfyesr3, dh_cfyesr4, dh_entered_by, dh_entered_on, dh_hold, DH_NO_OF_FLOORS, DH_OVER_VAL, DH_FINAL_FLAG, " +
                    "dh_isreq, dh_conditions, dh_isreject, dh_iscodi, dh_bcode_id, dh_bbrcode_id, DH_LOADING, DH_LOADING_VAL, LAND_PHONE, DH_VAL_BANKFAC, ADDED_DEDUCT, DED_PRECENTAGE, " +
                    "TERM, Period, Fire_cover, Other_cover, SRCC_cover, TC_cover, Flood_cover, " +
                    "BANK_UPDATED_BY, BANK_UPDATED_ON, PROP_TYPE, DH_SOLAR_SUM, SOLAR_REPAIRE, SOLAR_PARTS, SOLAR_ORIGIN, " +
                    "SOLAR_SERIAL, Solar_Period,LOAN_NUMBER " +
                    "from QUOTATION.FIRE_DH_PROPOSAL_ENTRY " +
                    "where dh_refno = :txtRefNo2";


                    OracleParameter para2 = new OracleParameter();
                    para2.Value = dh_refno.ToUpper().Trim();
                    para2.ParameterName = "txtRefNo2";
                    cmd.Parameters.Add(para2);

                    cmd.CommandText = sqlGetProptData;

                    //cmd.Parameters.Clear();

                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {
                            
                            if (!reader.IsDBNull(0))
                            {
                                dh_bcode = reader.GetString(0);
                            }
                            if (!reader.IsDBNull(1))
                            {
                                dh_bbrcode = reader.GetString(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                dh_name = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                dh_agecode = reader.GetString(3);
                               
                            }
                            
                            if (!reader.IsDBNull(4)) 
                            {
                                dh_agename = reader.GetString(4);
                            }

                            if (!reader.IsDBNull(5))
                            {
                                dh_nic = reader.GetString(5);
                            }
                          
                            if (!reader.IsDBNull(6))
                            {
                                dh_br = reader.GetString(6);
                            }
                            if (!reader.IsDBNull(7))
                            {
                                dh_padd1 = reader.GetString(7);

                            }

                            if (!reader.IsDBNull(8))
                            {
                                dh_padd2 = reader.GetString(8);

                            }                          

                            if (!reader.IsDBNull(9))
                            {

                                dh_padd3 = reader.GetString(9);
                               
                            }
                            if (!reader.IsDBNull(10))
                            {
                                dh_padd4 = reader.GetString(10);

                            }
                            if (!reader.IsDBNull(11))
                            {
                                dh_phone = reader.GetString(11);

                            }
                            
                            if (!reader.IsDBNull(12))
                            {
                                dh_email = reader.GetString(12);

                            }
                           
                            if (!reader.IsDBNull(13))
                            {
                                dh_iadd1 = reader.GetString(13);

                            }
                            
                            if (!reader.IsDBNull(14))
                            {
                                dh_iadd2 = reader.GetString(14);

                            }
                            
                            if (!reader.IsDBNull(15))
                            {
                                dh_iadd3 = reader.GetString(15);

                            }
                            
                            if (!reader.IsDBNull(16))
                            {
                                dh_iadd4 = reader.GetString(16);

                            }
                         
                            
                            if (!reader.IsDBNull(17))
                            {
                                dh_pfrom = reader.GetDateTime(17).ToString("dd/MM/yyy");

                            }
                           
                            if (!reader.IsDBNull(18))
                            {
                                dh_pto = reader.GetDateTime(18).ToString("dd/MM/yyy");

                            }
                            
                            if (!reader.IsDBNull(19))
                            {
                                dh_uconstr = reader.GetString(19);

                            }
                           
                            if (!reader.IsDBNull(20))
                            {
                                dh_occu_car = reader.GetString(20);

                            }
                            
                            if (!reader.IsDBNull(21))
                            {

                                dh_occ_yes_reas = reader.GetString(21);

                            }
                            
                            if (!reader.IsDBNull(22))
                            {
                                dh_haz_occu = reader.GetString(22);

                            }

                          
                            if (!reader.IsDBNull(23))
                            {
                                dh_haz_yes_rea = reader.GetString(23);

                            }
                            if (!reader.IsDBNull(24))
                            {
                                
                                    dh_valu_build = reader.GetDouble(24).ToString("###,###,###0.00");

                            }
                            else
                            {
                                dh_valu_build = "0.00";
                            }

                            if (!reader.IsDBNull(25))
                            {

                                dh_valu_wall = reader.GetDouble(25).ToString("###,###,###0.00");

                            }
                            else
                            {
                                dh_valu_wall = "0.00";
                            }

                            if (!reader.IsDBNull(26))
                            {

                                dh_valu_total = reader.GetDouble(26).ToString("###,###,###0.00");

                            }
                            else
                            {
                                dh_valu_total = "0.00";
                            }


                            if (!reader.IsDBNull(27))
                            {
                                dh_aff_flood = reader.GetString(27);

                            }
      
                            if (!reader.IsDBNull(28))
                            {

                                dh_aff_yes_reas = reader.GetString(28);
                            }

                            if (!reader.IsDBNull(29))
                            {
                                dh_wbrick = reader.GetString(29);
                            }
                           

                            if (!reader.IsDBNull(30))
                            {
                                dh_wcement = reader.GetString(30);
                            }

                            if (!reader.IsDBNull(31))
                            {
                                dh_dwooden = reader.GetString(31);
                            }

                            if (!reader.IsDBNull(32))
                            {
                                dh_dmetal = reader.GetString(32);
                            }

                            if (!reader.IsDBNull(33))
                            {
                                dh_ftile = reader.GetString(33);
                            }

                            if (!reader.IsDBNull(34))
                            {
                                dh_fcement = reader.GetString(34);
                            }

                            if (!reader.IsDBNull(35))
                            {
                                dh_rtile = reader.GetString(35);
                            }
                            if (!reader.IsDBNull(36))
                            {
                                dh_rasbes = reader.GetString(36);
                            }
                            if (!reader.IsDBNull(37))
                            {
                                dh_rgi = reader.GetString(37);
                            }

                            if (!reader.IsDBNull(38))
                            {
                                dh_rconcreat = reader.GetString(38);
                            }
                            if (!reader.IsDBNull(39))
                            {
                                dh_cov_fire = reader.GetString(39);
                            }
                            if (!reader.IsDBNull(40))
                            {
                                dh_cov_light = reader.GetString(40);
                            }
                            if (!reader.IsDBNull(41))
                            {
                                dh_cov_flood = reader.GetString(41);
                            }
                            if (!reader.IsDBNull(42))
                            {
                                dh_cfwateravl = reader.GetString(42);
                            }

                            if (!reader.IsDBNull(43))
                            {
                                dh_cfyesr1 = reader.GetString(43);
                            }
                            if (!reader.IsDBNull(44))
                            {
                                dh_cfyesr2 = reader.GetString(44);
                            }
                            if (!reader.IsDBNull(45))
                            {
                                dh_cfyesr3 = reader.GetString(45);
                            }
                            if (!reader.IsDBNull(46))
                            {
                                dh_cfyesr4 = reader.GetString(46);
                            }
                            if (!reader.IsDBNull(47))
                            {
                                dh_entered_by = reader.GetString(47);
                            }

                            if (!reader.IsDBNull(48))
                            {
                                dh_entered_on = reader.GetDateTime(48).ToString("dd/MM/yyy");
                            }
                            if (!reader.IsDBNull(49))
                            {
                                dh_hold = reader.GetString(49);
                            }
                            if (!reader.IsDBNull(50))
                            {
                                DH_NO_OF_FLOORS = reader.GetInt32(50).ToString();
                            }
                            if (!reader.IsDBNull(51))
                            {
                                DH_OVER_VAL = reader.GetString(51);
                            }
                            if (!reader.IsDBNull(52))
                            {
                                DH_FINAL_FLAG = reader.GetString(52);
                            }

                            if (!reader.IsDBNull(53))
                            {
                                dh_isreq = reader.GetString(53);
                            }

                            if (!reader.IsDBNull(54))
                            {
                                dh_conditions = reader.GetString(54);
                            }
                            if (!reader.IsDBNull(55))
                            {
                                dh_isreject = reader.GetString(55);
                            }

                            if (!reader.IsDBNull(56))
                            {
                                dh_iscodi = reader.GetString(56);
                            }

                            if (!reader.IsDBNull(57))
                            {
                                dh_bcode_id = reader.GetString(57);
                            }

                            if (!reader.IsDBNull(58))
                            {
                                dh_bbrcode_id = reader.GetString(58);
                            }

                            if (!reader.IsDBNull(59))
                            {
                                DH_LOADING = reader.GetString(59);
                            }
                            
                            if (!reader.IsDBNull(60))
                            {
                                DH_LOADING_VAL = reader.GetString(60);
                            }

                            if (!reader.IsDBNull(61))
                            {
                                LAND_PHONE = reader.GetString(61);
                            }

                            if (!reader.IsDBNull(62))
                            {

                                DH_VAL_BANKFAC = reader.GetDouble(62).ToString("###,###,###0.00");

                            }
                            else
                            {
                                DH_VAL_BANKFAC = "0.00";
                            }
                            if (!reader.IsDBNull(63))
                            {
                                dh_deductible = reader.GetString(63);
                            }
                            if (!reader.IsDBNull(64))
                            {
                                dh_deductible_pre = reader.GetString(64);
                            }
                            if (!reader.IsDBNull(65))
                            {
                                TERM = reader.GetString(65);
                            }
                            if (!reader.IsDBNull(66))
                            {
                                Period = reader.GetDouble(66).ToString();
                            }
                            if (!reader.IsDBNull(67))
                            {
                                Fire_cover = reader.GetString(67);
                            }
                            if (!reader.IsDBNull(68))
                            {
                                Other_cover = reader.GetString(68);
                            }
                            if (!reader.IsDBNull(69))
                            {
                                SRCC_cover = reader.GetString(69);
                            }
                            if (!reader.IsDBNull(70))
                            {
                                TC_cover = reader.GetString(70);
                            }
                            if (!reader.IsDBNull(71))
                            {
                                Flood_cover = reader.GetString(71);
                            }

                            if (!reader.IsDBNull(72))
                            {

                                BANK_UPDATED_BY = reader.GetString(72);
                            }
                            if (!reader.IsDBNull(73))
                            {
                                BANK_UPDATED_ON = reader.GetDateTime(73).ToString("dd/MM/yyy");
                            }
                            if (!reader.IsDBNull(74))
                            {
                                PROP_TYPE = reader.GetString(74);
                            }


                            if (!reader.IsDBNull(75))
                            {

                                DH_SOLAR_SUM = reader.GetDouble(75).ToString("###,###,###0.00");

                            }
                            else
                            {
                                DH_SOLAR_SUM = "0.00";
                            }


                            if (!reader.IsDBNull(76))
                            {
                                SOLAR_REPAIRE = reader.GetString(76);
                            }
                            if (!reader.IsDBNull(77))
                            {
                                SOLAR_PARTS = reader.GetString(77);
                            }
                            if (!reader.IsDBNull(78))
                            {
                                SOLAR_ORIGIN = reader.GetString(78);
                            }
                            if (!reader.IsDBNull(79))
                            {
                                SOLAR_SERIAL = reader.GetString(79);
                            }
                            if (!reader.IsDBNull(80))
                            {
                                Solar_Period = reader.GetDouble(80).ToString();
                            }
                            if (!reader.IsDBNull(81))
                            {
                                LOAN_NUMBER = reader.GetString(81);
                            }
                            

                        }

                    }


                }

            }

        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }


        }

        return mgs;
    }


    public string GetDeductibles(
       double sum_insu,out string DH_OPTION1, out string DH_OPTION2, out string DH_OPTION3, out string DH_OPTION4, out string DH_OPTION5,
        out string DH_OPTION6, out string DH_OPTION7)
    {
        string mgs = "";
        DH_OPTION1 = ""; DH_OPTION2 = ""; DH_OPTION3 = ""; DH_OPTION4="";
        DH_OPTION5 = ""; DH_OPTION6 = ""; DH_OPTION7 = "";

        sum_insu = Math.Round(sum_insu);

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {
                string sqlCountDeduc = "select count(*) FROM QUOTATION.FIRE_DH_DEDUCTIBLES where active = 'Y' " +
               "and dh_min_val <= :txtSumInsu " +
               "and dh_max_val >=:txtSumInsu";

                

                OracleParameter para1 = new OracleParameter();
                para1.Value = sum_insu;
                para1.ParameterName = "txtSumInsu";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountDeduc;

                int countRef = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countRef == 1)
                {


                    string sqlGetDeducData = "select DH_OPTION1,DH_OPTION2,DH_OPTION3,DH_OPTION4,DH_OPTION5,DH_OPTION6,DH_OPTION7 " +
                                             "FROM QUOTATION.FIRE_DH_DEDUCTIBLES " +
                                             "where active = 'Y' " +
                                             "and dh_min_val <= :txtSumInsu2 " +
                                             "and dh_max_val >= :txtSumInsu2";


                    OracleParameter para2 = new OracleParameter();
                    para2.Value = sum_insu;
                    para2.ParameterName = "txtSumInsu2";
                    cmd.Parameters.Add(para2);

                    cmd.CommandText = sqlGetDeducData;

                    //cmd.Parameters.Clear();

                   
                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {

                            if (!reader.IsDBNull(0))
                            {
                                DH_OPTION1 = reader.GetString(0);
                            }
                            if (!reader.IsDBNull(1))
                            {
                                DH_OPTION2 = reader.GetString(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                DH_OPTION3 = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                DH_OPTION4 = reader.GetString(3);
                            }
                            if (!reader.IsDBNull(4))
                            {
                                DH_OPTION5 = reader.GetString(4);
                            }
                            if (!reader.IsDBNull(5))
                            {
                                DH_OPTION6 = reader.GetString(5);
                            }
                            if (!reader.IsDBNull(6))
                            {
                                DH_OPTION7 = reader.GetString(6);
                            }
                        }

                    }

                }

            }

        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return mgs;
    }

    public string GetDeductiblesForSolar(
      double sum_insu, out string DH_OPTION1, out string DH_OPTION2, out string DH_OPTION3, out string DH_OPTION4, out string DH_OPTION5,
       out string DH_OPTION6, out string DH_OPTION7, out string DH_OPTION8)
    {
        string mgs = "";
        DH_OPTION1 = ""; DH_OPTION2 = ""; DH_OPTION3 = ""; DH_OPTION4 = "";
        DH_OPTION5 = ""; DH_OPTION6 = ""; DH_OPTION7 = ""; DH_OPTION8 = "";

        sum_insu = Math.Round(sum_insu);

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {
                string sqlCountDeduc = "select count(*) FROM QUOTATION.FIRE_SOLAR_DEDUCTIBLES where active = 'Y' " +
               "and dh_min_val <= :txtSumInsu " +
               "and dh_max_val >=:txtSumInsu";



                OracleParameter para1 = new OracleParameter();
                para1.Value = sum_insu;
                para1.ParameterName = "txtSumInsu";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountDeduc;

                int countRef = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countRef == 1)
                {


                    string sqlGetDeducData = "select DH_OPTION1,DH_OPTION2,DH_OPTION3,DH_OPTION4,DH_OPTION5,DH_OPTION6,DH_OPTION7,DH_OPTION8 " +
                                             "FROM QUOTATION.FIRE_SOLAR_DEDUCTIBLES " +
                                             "where active = 'Y' " +
                                             "and dh_min_val <= :txtSumInsu2 " +
                                             "and dh_max_val >= :txtSumInsu2";


                    OracleParameter para2 = new OracleParameter();
                    para2.Value = sum_insu;
                    para2.ParameterName = "txtSumInsu2";
                    cmd.Parameters.Add(para2);

                    cmd.CommandText = sqlGetDeducData;

                    //cmd.Parameters.Clear();


                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {

                            if (!reader.IsDBNull(0))
                            {
                                DH_OPTION1 = reader.GetString(0);
                            }
                            if (!reader.IsDBNull(1))
                            {
                                DH_OPTION2 = reader.GetString(1);
                            }
                            if (!reader.IsDBNull(2))
                            {
                                DH_OPTION3 = reader.GetString(2);
                            }
                            if (!reader.IsDBNull(3))
                            {
                                DH_OPTION4 = reader.GetString(3);
                            }
                            if (!reader.IsDBNull(4))
                            {
                                DH_OPTION5 = reader.GetString(4);
                            }
                            if (!reader.IsDBNull(5))
                            {
                                DH_OPTION6 = reader.GetString(5);
                            }
                            if (!reader.IsDBNull(6))
                            {
                                DH_OPTION7 = reader.GetString(6);
                            }
                            if (!reader.IsDBNull(7))
                            {
                                DH_OPTION8 = reader.GetString(7);
                            }
                        }

                    }

                }

            }

        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return mgs;
    }
    public string[] getFireCoverArray()
    {
        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetFireCovers(), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {
                for (int i = 0; i < details.Rows.Count; i++) {
                    if (details.Rows.Count > 0)
                    {

                        ArryCovers[i] = details.Rows[i]["DH_SCOPE_WORD"].ToString();
                    }
                    else
                    {
                        ArryCovers[i] = string.Empty;
                    }
                }             
                //store to array------------->
            }
            else
            {

            }
        }
        catch (Exception ex)
        {

        }

        return ArryCovers;

    }


    public string GetSchedualCalValues(
    string SC_REF_NO,out string SC_POLICY_NO,out string SC_SUM_INSU,out string SC_NET_PRE,
         out string SC_RCC,out string SC_TR,out string SC_ADMIN_FEE,
         out string SC_POLICY_FEE,out string SC_NBT,out string SC_VAT,out string SC_TOTAL_PAY,out string CREATED_ON,out string CREATED_BY,
         out string FLAG, out string SC_Renewal_FEE, out string BP_FEE, out string DEBIT_NO)
    {
        string mgs = "";
    SC_POLICY_NO= ""; SC_SUM_INSU= ""; SC_NET_PRE= ""; SC_RCC= ""; SC_TR= ""; SC_ADMIN_FEE= "";
    SC_POLICY_FEE= "";SC_NBT= "";SC_VAT= "";SC_TOTAL_PAY= "";CREATED_ON= "";CREATED_BY= "";FLAG= "";
    SC_Renewal_FEE = "";
    BP_FEE = ""; DEBIT_NO = "";


        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            OracleCommand cmd = oconn.CreateCommand();


            using (cmd)
            {

  
                string sqlCountcal = "select count(*) from QUOTATION.FIRE_DH_SCHEDULE_CALC where sc_ref_no = :txtRefNo";
              


                OracleParameter para1 = new OracleParameter();
                para1.Value = SC_REF_NO;
                para1.ParameterName = "txtRefNo";
                cmd.Parameters.Add(para1);


                cmd.CommandText = sqlCountcal;

                int countRef = Convert.ToInt32(cmd.ExecuteScalar().ToString());

                cmd.Parameters.Clear();

                if (countRef == 1)
                {
                   string sqlGetCalData = "select sc_policy_no, sc_sum_insu, sc_net_pre, sc_rcc, sc_tr, " +
                            "sc_admin_fee, sc_policy_fee, sc_nbt, sc_vat, sc_total_pay, created_on, created_by, flag,SC_Renewal_FEE,BP_FEE,DEBIT_NO " +
                            "from QUOTATION.FIRE_DH_SCHEDULE_CALC "+
                            "where sc_ref_no = :txtRefNo2";

             
                    OracleParameter para2 = new OracleParameter();
                    para2.Value = SC_REF_NO;
                    para2.ParameterName = "txtRefNo2";
                    cmd.Parameters.Add(para2);

                    cmd.CommandText = sqlGetCalData;

                    //cmd.Parameters.Clear();
  
                    OracleDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        if (reader.Read())
                        {

                            if (!reader.IsDBNull(0))
                            {
                                SC_POLICY_NO = reader.GetString(0);
                            }
                            if (!reader.IsDBNull(1))
                            {
                                SC_SUM_INSU = reader.GetDouble(1).ToString("###,###,###0.00");
                            }
                            if (!reader.IsDBNull(2))
                            {
                                SC_NET_PRE = reader.GetDouble(2).ToString("###,###,###0.00");
                            }
                            if (!reader.IsDBNull(3))
                            {
                                SC_RCC = reader.GetDouble(3).ToString("###,###,###0.00");
                            }


                            if (!reader.IsDBNull(4))
                            {
                                SC_TR = reader.GetDouble(4).ToString("###,###,###0.00");
                            }
                            if (!reader.IsDBNull(5))
                            {
                                SC_ADMIN_FEE = reader.GetDouble(5).ToString("###,###,###0.00");
                            }
                            if (!reader.IsDBNull(6))
                            {
                                SC_POLICY_FEE = reader.GetDouble(6).ToString("###,###,###0.00");
                            }
                            if (!reader.IsDBNull(7))
                            {
                                SC_NBT = reader.GetDouble(7).ToString("###,###,###0.00");
                            }
                            else { SC_NBT = ""; }
                            if (!reader.IsDBNull(8))
                            {
                                SC_VAT = reader.GetDouble(8).ToString("###,###,###0.00");
                            }
                            else { SC_VAT = ""; }
                            if (!reader.IsDBNull(9))
                            {
                                SC_TOTAL_PAY = reader.GetDouble(9).ToString("###,###,###0.00");
                            }
                            
                            if (!reader.IsDBNull(10))
                            {
                                CREATED_ON = reader.GetDateTime(10).ToString("dd/MM/yyy");
                            }
                            if (!reader.IsDBNull(11))
                            {
                                CREATED_BY = reader.GetString(11);
                            }
                            if (!reader.IsDBNull(12))
                            {
                                FLAG = reader.GetString(12);
                            }
                            if (!reader.IsDBNull(13))
                            {
                                SC_Renewal_FEE = reader.GetDouble(13).ToString("###,###,###0.00");
                            }

                            if (!reader.IsDBNull(14))
                            {
                                BP_FEE = reader.GetDouble(14).ToString("###,###,###0.00");
                            }

                            if (!reader.IsDBNull(15))
                            {
                                DEBIT_NO = reader.GetString(15);
                            }

                        }

                    }

                }

            }

        }
        catch (Exception ex)
        {
            string exe = ex.ToString();
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return mgs;
    }


    // solar electrical and accidental values

    // get solar rates----->>
    public bool GetElectricalClauseForSolar(string bank_code, double maxVal, double minVal, out double SOLAR_ELECT, out bool rtn)
    {
        rtn = false;

        SOLAR_ELECT = 0;


        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetSolarElectrical(bank_code, maxVal, minVal), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    SOLAR_ELECT = Convert.ToDouble(details.Rows[0]["CLA_VAL"].ToString());
                    rtn = true;
                }
                else
                {

                    rtn = false;
                }
            }
            else
            {
                rtn = false;

            }
        }
        catch (Exception ex)
        {
            rtn = false;
        }

        return rtn;

    }

    public bool GetAccidentalClauseForSolar(string bank_code, double maxVal, double minVal, out double SOLAR_ACCIDENTAL, out bool rtn)
    {
        rtn = false;

        SOLAR_ACCIDENTAL = 0;


        DataTable details = new DataTable();
        try
        {
            details = orcle_trans.GetRows(this._sql.GetSolarAccidental(bank_code, maxVal, minVal), details);

            if (orcle_trans.Trans_Sucess_State == true)
            {

                if (details.Rows.Count > 0)
                {
                    SOLAR_ACCIDENTAL = Convert.ToDouble(details.Rows[0]["CLA_VAL"].ToString());
                    rtn = true;
                }
                else
                {

                    rtn = false;
                }
            }
            else
            {
                rtn = false;

            }
        }
        catch (Exception ex)
        {
            rtn = false;
        }

        return rtn;

    }
}