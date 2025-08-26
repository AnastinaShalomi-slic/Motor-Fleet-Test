using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for FireRenewalMast
/// </summary>
//
public class FireRenewalMast
{
    ORCL_Connection orcl_con = new ORCL_Connection();
    OracleConnection con = new OracleConnection();
    public FireRenewalMast()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public class FireRenewalMastClass
    {
        public string RNDEPT { get; set; }
        public string RNPTP { get; set; }
        public string RNPOL { get; set; }
        public int RNYR { get; set; }
        public int RNMNTH { get; set; }
        public string RNSTDT { get; set; }
        public string RNENDT { get; set; }
        public double RNAGCD { get; set; }
        public double RNNET { get; set; }
        public double RNRCC { get; set; }
        public double RNTC { get; set; }
        public double RNPOLFEE { get; set; }
        public double RNVAT { get; set; }
        public double RNNBT { get; set; }
        public double RNTOT { get; set; }
        public string RNNAM { get; set; }
        public string RNADD1 { get; set; }
        public string RNADD2 { get; set; }
        public string RNADD3 { get; set; }
        public string RNADD4 { get; set; }
        public string RNNIC { get; set; }
        public string RNCNT { get; set; }
        public string RNREF { get; set; }
        public double RNFBR { get; set; }
        public double RN_ADMINFEE { get; set; }
        public string RNDATE { get; set; }
        public string RN_BY { get; set; }
        public string RN_IP { get; set; }
        public string RN_BRCD { get; set; }
        public double RNSUMINSUR { get; set; }
        public string SUMNOTCHASTATUS { get; set; }
        public string REMARK { get; set; }
        public double EXCESSPRE { get; set; }
        public double EXCESSAMO { get; set; }
        public double EXCESSPRE2 { get; set; }
        public double EXCESSAMO2 { get; set; }
        public string REJECT_REASON { get; set; }
    }



}