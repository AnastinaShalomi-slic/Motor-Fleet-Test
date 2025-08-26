using System;
using System.Data;
using System.Configuration;
using System.Data.OracleClient;
//using HashLibrary;


/// <summary>
/// Summary description for ULCustomer
/// </summary>
public class ULCustomer
{
    OracleConnection oconn = new OracleConnection(ConfigurationManager.AppSettings["DBConStrLife"]);

    public ULCustomer()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    /*public bool RegisterCustomer(string userName, string password, string title, string firstName, string lastname, string otherNames,
                                 string nic, int dateOfBirth, string gender, string addrss1, string addrss2, string addrss3, string addrss4,
                                 string city, string postalCode, string country, string email, string mobileNo, string homeNo, string ofcNo)
    {
        bool returnValue = false;
        //if (userName.Length <= 50 && password.Length <= 50) -- All validations done seperately
       // {            
            try{
                oconn.Open();
                string registerUser = "Insert into COOPWEB.webusers(USR_SEQ_ID, USERNAME, PASWORD, FIRST_NAME, LAST_NAME, OTHER_NAMES, TITLE, NIC_NO, DATE_OF_BIRTH, GENDER," +
                                                                " ADDRESS1, ADDRESS2, ADDRESS3, ADDRESS4, TOWN_CITY, POSTAL_CODE, COUNTRY, EMAIL, MOBILE_NUMBER," +
                                                                " HOME_NUMBER, OFFICE_NUMBER) values (NEXTVAL FOR COOPWEB.WEB01, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";

                    using (OdbcCommand com = new OdbcCommand(registerUser, oconn))
                    {
                        com.Parameters.Add("@username", OdbcType.VarChar);
                        com.Parameters["@username"].Value = userName;

                        com.Parameters.Add("@password", OdbcType.VarChar);
                        com.Parameters["@password"].Value = Hash.Hash.GetHash(password, Hash.Hash.HashType.SHA512);//Hasher.HashString(password.Trim());

                        com.Parameters.Add("@firstname", OdbcType.VarChar);
                        com.Parameters["@firstname"].Value = firstName;

                        com.Parameters.Add("@lastname", OdbcType.VarChar);
                        com.Parameters["@lastname"].Value = lastname;

                        com.Parameters.Add("@othernames", OdbcType.VarChar);
                        com.Parameters["@othernames"].Value = otherNames;

                        com.Parameters.Add("@title", OdbcType.VarChar);
                        com.Parameters["@title"].Value = title;

                        com.Parameters.Add("@nicNo", OdbcType.VarChar);
                        com.Parameters["@nicNo"].Value = nic.ToUpper();

                        com.Parameters.Add("@dateofbirth", OdbcType.Decimal);
                        com.Parameters["@dateofbirth"].Value = dateOfBirth;

                        com.Parameters.Add("@gender", OdbcType.VarChar);
                        com.Parameters["@gender"].Value = gender;

                        com.Parameters.Add("@address1", OdbcType.VarChar);
                        com.Parameters["@address1"].Value = addrss1;

                        com.Parameters.Add("@address2", OdbcType.VarChar);
                        com.Parameters["@address2"].Value = addrss2;

                        com.Parameters.Add("@address3", OdbcType.VarChar);
                        com.Parameters["@address3"].Value = addrss3;

                        com.Parameters.Add("@address4", OdbcType.VarChar);
                        com.Parameters["@address4"].Value = addrss4;

                        com.Parameters.Add("@town", OdbcType.VarChar);
                        com.Parameters["@town"].Value = city;

                        com.Parameters.Add("@postCode", OdbcType.VarChar);
                        com.Parameters["@postCode"].Value = postalCode;

                        com.Parameters.Add("@country", OdbcType.VarChar);
                        com.Parameters["@country"].Value = country;

                        com.Parameters.Add("@email", OdbcType.VarChar);
                        com.Parameters["@email"].Value = email;

                        com.Parameters.Add("@mobileNo", OdbcType.VarChar);
                        com.Parameters["@mobileNo"].Value = mobileNo;

                        com.Parameters.Add("@homeNo", OdbcType.VarChar);
                        com.Parameters["@homeNo"].Value = homeNo;

                        com.Parameters.Add("@officeNo", OdbcType.VarChar);
                        com.Parameters["@officeNo"].Value = ofcNo;

                        int count = com.ExecuteNonQuery();

                        if (count > 0)
                        {

                            string setRegisterLink = "Insert into COOPWEB.WEBUSERS_PREREG(USERNAME, REG_CODE)values (?, ?)";

                            using (OdbcCommand com2 = new OdbcCommand(setRegisterLink, oconn))
                            {
                                string hashedUser = Hash.Hash.GetHash(userName.Trim(), Hash.Hash.HashType.SHA512);//Hasher.HashString(username.Trim());

                                com2.Parameters.Add("@username", OdbcType.VarChar);
                                com2.Parameters["@username"].Value = userName;

                                com2.Parameters.Add("@regcode", OdbcType.VarChar);
                                com2.Parameters["@regcode"].Value = hashedUser;

                                int count2 = com2.ExecuteNonQuery();

                                if (count2 > 0)
                                {
                                    string subject = "SLIC Online Policy Inquiry  - Registration service";
                                    string content = "Please click on following link to complete your registration." +
                                                     "<br/><br/><a href=\"http://www.srilankainsurance.lk/EuroCenterLogin_Inquiry/ConfirmRegister.aspx?regtokn=" + hashedUser +
                                                     "\"> http://www.srilankainsurance.lk/EuroCenterLogin_Inquiry/ConfirmRegister.aspx?regtokn=" + hashedUser + "</a><br/><br/><div style='font-size:10pt; color:#CCCCCC;'>This is a system generated email, please do not reply.</div>";

                                    returnValue = utils.sendEmail(email, subject, content);

                                }
                            }                            
                        }
                    }                 
                                   
            }
            catch
            {
                // trans.Rollback();
                returnValue = false;
            }
            finally
            {
                if (oconn != null) oconn.Close();
            }        

        return returnValue;
    }*/

    public bool ValidateLogin(string userName, string password, int failedLoginCount)
    {
        bool returnValue = false;

        if (userName.Length <= 50 && password.Length <= 20)
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            OracleCommand cmd = oconn.CreateCommand();
            OracleTransaction trans = oconn.BeginTransaction();
            cmd.Transaction = trans;

            try
            {
                using (cmd)
                {
                    string getUser = "select count(*) from COOPWEB.WEBUSERS where UPPER(USERNAME) = :username and PASWORD = :password and ACTIVE_FLAG = 'Y' and USERTYPE = 2";

                    cmd.CommandText = getUser;

                    OracleParameter oUsrName = new OracleParameter();
                    oUsrName.DbType = DbType.String;
                    oUsrName.Value = userName.ToUpper();
                    oUsrName.ParameterName = "username";

                    OracleParameter oPassword = new OracleParameter();
                    oPassword.DbType = DbType.String;
                    oPassword.Value = Hash.Hash.GetHash(password, Hash.Hash.HashType.SHA512);
                    oPassword.ParameterName = "password";
                    
                    cmd.Parameters.Add(oUsrName);
                    cmd.Parameters.Add(oPassword);
                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    //int count = cmd.ExecuteNonQuery();
                    cmd.Parameters.Clear();                           

                    if (count > 0)
                    {
                        string setLoginDate = "Update COOPWEB.WEBUSERS set LAST_LOGIN_DATE = sysdate where UPPER(USERNAME) = :username";

                        cmd.CommandText = setLoginDate;

                        OracleParameter oUserName = new OracleParameter();
                        oUserName.DbType = DbType.String;
                        oUserName.Value = userName.ToUpper();
                        oUserName.ParameterName = "username";

                        cmd.Parameters.Add(oUserName);

                        int count1 = cmd.ExecuteNonQuery();
                        cmd.Parameters.Clear();

                        if (count1 > 0)
                        {
                            returnValue = true;
                        }

                        if (failedLoginCount > 0)
                        {
                            string setLoginCountZero = "Update COOPWEB.WEBUSERS set FAILED_LOGIN_COUNT = 0 where UPPER(USERNAME) = :username";

                            /*using (OdbcCommand com3 = new OdbcCommand(setLoginCountZero, oconn))
                            {
                                com3.Parameters.Add("@username", OdbcType.VarChar);
                                com3.Parameters["@username"].Value = userName;

                                int count2 = com3.ExecuteNonQuery();

                                if (count2 < 0)
                                {
                                    // should log error - Failed login count not updated for some reason.
                                }
                            }   */

                            cmd.CommandText = setLoginCountZero;

                            OracleParameter oUserNam = new OracleParameter();
                            oUserNam.DbType = DbType.String;
                            oUserNam.Value = userName.ToUpper();
                            oUserNam.ParameterName = "username";

                            cmd.Parameters.Add(oUserNam);

                            int count2 = cmd.ExecuteNonQuery();
                            cmd.Parameters.Clear();

                            if (count2 < 0)
                            {
                                //log logger = new log();
                                //logger.write_log("Failed Webusers update at ValidateLogin");
                            }
                        }
                    }
                    else
                    {
                        returnValue = false;
                        string setLoginCount = "Update COOPWEB.WEBUSERS set FAILED_LOGIN_COUNT = (NVL(FAILED_LOGIN_COUNT,0) + 1), LAST_FAILED_DATE = sysdate where UPPER(USERNAME) = :username and ACTIVE_FLAG = 'Y' and USERTYPE = 2";

                        cmd.CommandText = setLoginCount;

                        OracleParameter oUser = new OracleParameter();
                        oUser.DbType = DbType.String;
                        oUser.Value = userName.ToUpper();
                        oUser.ParameterName = "username";

                        cmd.Parameters.Add(oUser);

                        int count3 = cmd.ExecuteNonQuery();

                        if (count3 < 0)
                        {
                            // should log error - Failed login count not updated for some reason.
                        }

                        //log lg = new log();
                       // log logger = new log();
                      //  logger.write_log("login failed for " + userName);

                    }
                }                             

                trans.Commit();

            }
            catch (Exception e)
            {
                // trans.Rollback();
                //returnValue = false;// Log your error
                trans.Rollback();
                returnValue = false;// Log your error
                //log logger = new log();
                //logger.write_log("Failed at ULCustomer - ValidateLogin1: " + e.ToString());
            }
            finally
            {
                if (oconn.State == ConnectionState.Open)
                {
                    oconn.Close();
                }
            }
        }
        else
        {
            // Log error - user name not alpha-numeric or 
            // username or password exceed the length limit!
        }

        return returnValue;
    }

   

    public bool PasswordRecoverySet(string fieldValue, string fieldType)
    {
        bool returnValue = false;
        string userName = "";
        string email = "";

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getUsername = "";

            if (fieldType == "UN")
            {
                getUsername = "Select USERNAME, EMAIL from COOPWEB.WEBUSERS where UPPER(USERNAME) = UPPER(:fieldVal) and ACTIVE_FLAG = 'Y' and USERTYPE = 3";
            }
            else if (fieldType == "EM")
            {
                getUsername = "Select USERNAME, EMAIL from COOPWEB.WEBUSERS where EMAIL = :fieldVal and ACTIVE_FLAG = 'Y' and USERTYPE = 3";
            }

            using (OracleCommand cmd = new OracleCommand(getUsername, oconn))
            {
                cmd.Parameters.AddWithValue("fieldVal", fieldValue);

                OracleDataReader usernameReader = (OracleDataReader)cmd.ExecuteReader();

                if (usernameReader.HasRows)
                {
                    while (usernameReader.Read())
                    {
                        if (!usernameReader.IsDBNull(0))
                        {
                            userName = usernameReader.GetString(0);
                        }
                        if (!usernameReader.IsDBNull(1))
                        {
                            email = usernameReader.GetString(1);
                        }
                    }
                    usernameReader.Close();
                }

                string deleteQuery = "DELETE FROM COOPWEB.WEBUSER_PWRESET WHERE USERNAME = :uname";
                using (OracleCommand cmd2 = new OracleCommand(deleteQuery, oconn))
                {
                    cmd2.Parameters.AddWithValue("uname", userName);
                    int count = cmd2.ExecuteNonQuery();
                }

                string setPassRecovery = "Insert into COOPWEB.WEBUSER_PWRESET(USERNAME, REQ_DATE, RESET_CODE)values (:username, sysdate, :resetcode)";
                using (OracleCommand cmd2 = new OracleCommand(setPassRecovery, oconn))
                {
                    string hashedUser = Hash.Hash.GetHash(userName.Trim(), Hash.Hash.HashType.SHA512);//Hasher.HashString(username.Trim());

                    cmd2.Parameters.AddWithValue("username", userName);
                    cmd2.Parameters.AddWithValue("resetcode", hashedUser);

                    int count = cmd2.ExecuteNonQuery();

                    if (count > 0)
                    {

                        //Db_Email dbe = new Db_Email();

                        //returnValue = dbe.send_html_email(email, "SLIC Global emergency assistance - Password Recovery service", "Password recovery", content);
                        string subject = "SLIC Online Policy Inquiry - Password Recovery service";
                        string content = "Dear " + userName + ", <br/><br/> Please click on following link to reset your password." +
                                         "<br/><br/><a href=\"http://www.srilankainsurance.lk/EuroCenterLogin_Inquiry/ResetPassword.aspx?token=" + hashedUser +
                                         "\"> http://www.srilankainsurance.lk/EuroCenterLogin_Inquiry/ResetPassword.aspx?token=" + hashedUser + "</a><br/><br/><div style='font-size:10pt; color:#AAAAAA;'>This is a system generated email, please do not reply.</div>";

                        //returnValue = dbe.send_html_email(email, subject, "Please copy the url and navigate to it using a browser.      http://www.srilankainsurance.lk/EuroCenterLogin_Inquiry/ResetPassword.aspx?token=" + hashedUser, content);
                        //returnValue = utils.sendEmail(email, subject, content);

                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            returnValue = false;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return returnValue;
    }

    public bool isValidToken(string token)
    {
        bool returnValue = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string getValidToken = "select count(*) from COOPWEB.WEBUSER_PWRESET where RESET_CODE = :token";

            using (OracleCommand cmd = new OracleCommand(getValidToken, oconn))
            {
                cmd.Parameters.AddWithValue("token", token.Trim());

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    returnValue = true;
                }
            }       
           
        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return returnValue;
    }

    public bool isValidRegToken(string token)
    {
        bool returnValue = false;
        try
        {
            oconn.Open();
            string getValidToken = "select count(*) from COOPWEB.WEBUSERS_PREREG where REG_CODE = :token";

            using (OracleCommand cmd = new OracleCommand(getValidToken, oconn))
            {
                cmd.Parameters.AddWithValue("token", token.Trim());

                int count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    returnValue = true;
                }
            }

        }
        catch (Exception ex)
        {

        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return returnValue;
    }

    public bool ResetPassword(string token, string password)
    {
        bool returnValue = false;

        //conn.Open();
        //OracleCommand cmd = conn.CreateCommand();
        //OracleTransaction trans = conn.BeginTransaction();
        //cmd.Transaction = trans;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string username = "";
            string getUsername = "Select USERNAME from COOPWEB.WEBUSER_PWRESET where RESET_CODE = :token";

            using (OracleCommand com1 = new OracleCommand(getUsername, oconn))
            {
                com1.Parameters.AddWithValue("token", token.Trim());

                OracleDataReader usernameReader = (OracleDataReader)com1.ExecuteReader();

                if (usernameReader.HasRows)
                {
                    while (usernameReader.Read())
                    {
                        if (!usernameReader.IsDBNull(0))
                        {
                            username = usernameReader.GetString(0);
                        }
                    }
                    usernameReader.Close();

                    string setNewPassword = "Update COOPWEB.WEBUSERS set PASWORD = :pass, LAST_RESET_DATE = sysdate, FAILED_LOGIN_COUNT = 0 where lower(trim(USERNAME)) = :username";

                    using (OracleCommand com2 = new OracleCommand(setNewPassword, oconn))
                    {
                        com2.Parameters.AddWithValue("pass", Hash.Hash.GetHash(password.Trim(), Hash.Hash.HashType.SHA512));
                        com2.Parameters.AddWithValue("username", username.Trim().ToLower());

                        int count1 = com2.ExecuteNonQuery();                        

                        if (count1 > 0)
                        {
                            string deleteTempRecord = "Delete from COOPWEB.WEBUSER_PWRESET where RESET_CODE = :token";

                            using (OracleCommand com3 = new OracleCommand(deleteTempRecord, oconn))
                            {
                                com3.Parameters.AddWithValue("token", token.Trim());

                                int count2 = com3.ExecuteNonQuery();
                                if (count2 > 0)
                                {
                                    // trans.Commit();
                                    returnValue = true;
                                }
                            }                 

                        }
                    }
                }
            }
            
        }
        catch (Exception ex)
        {
            // trans.Rollback();
            returnValue = false;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return returnValue;
    }

    /*public bool ConfirmRegistration(string token)
    {
        bool returnValue = false;

        //conn.Open();
        //OracleCommand cmd = conn.CreateCommand();
        //OracleTransaction trans = conn.BeginTransaction();
        //cmd.Transaction = trans;

        try
        {
            oconn.Open();
            string username = "";
            string getUsername = "Select USERNAME from COOPWEB.WEBUSERS_PREREG where REG_CODE = ?";

            using (OdbcCommand com1 = new OdbcCommand(getUsername, oconn))
            {
                com1.Parameters.Add("@token", OdbcType.VarChar);
                com1.Parameters["@token"].Value = token.Trim();

                OdbcDataReader usernameReader = (OdbcDataReader)com1.ExecuteReader();

                if (usernameReader.HasRows)
                {
                    while (usernameReader.Read())
                    {
                        if (!usernameReader.IsDBNull(0))
                        {
                            username = usernameReader.GetString(0);
                        }
                    }
                    usernameReader.Close();

                    string setMemberActive = "Update COOPWEB.WEBUSERS set ACTIVE_FLAG = 'Y', LAST_UPD_DATE = (select current timestamp from COOPWEB.date_helper) where USERNAME = ?";

                    using (OdbcCommand com2 = new OdbcCommand(setMemberActive, oconn))
                    {
                        com2.Parameters.Add("@username", OdbcType.VarChar);
                        com2.Parameters["@username"].Value = username.Trim();
                        
                        int count1 = com2.ExecuteNonQuery();

                        if (count1 > 0)
                        {
                            string deleteTempRecord = "Delete from COOPWEB.WEBUSERS_PREREG where REG_CODE = ?";

                            using (OdbcCommand com3 = new OdbcCommand(deleteTempRecord, oconn))
                            {
                                com3.Parameters.Add("@token", OdbcType.VarChar);
                                com3.Parameters["@token"].Value = token.Trim();

                                int count2 = com3.ExecuteNonQuery();
                                if (count2 > 0)
                                {
                                    // trans.Commit();
                                    returnValue = true;
                                }
                            }

                        }
                    }
                }
            }

        }
        catch (Exception ex)
        {
            // trans.Rollback();
            returnValue = false;
        }
        finally
        {
            if (oconn != null) oconn.Close();
        }

        return returnValue;
    }*/

    public bool SendRegNotification(string username)
    {
        bool returnValue = false;

        try
        {
            string subject = "Thank you for registering for SLIC Online Policy Inquiry website";
            string content = "We thank you and welcome you to the SLIC Online Policy Inquiry site!" +
                             "<br/><br/> This site provides you with the Unit Link statistics." +
                             "<br/><br/> Click <a href=\"http://www.srilankainsurance.lk/EuroCenterLogin_Inquiry/Home.aspx\" target='_blank'>here</a> to visit our site." +
                             "<br/><br/><br/> SLIC-Team.<br/><br/><div style='font-size:10pt; color:#CCCCCC;'>This is a system generated email, please do not reply.</div>";

            //utils.sendEmail(username, subject, content);
            returnValue = true;

            //logging of email sent - to do
        }
        catch
        {
            returnValue = false;
        } 
        return returnValue;
    }

    public bool ChangePassword(string username, string oldPassword, string newPassword)
    {
        bool returnValue = false;
        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }
            string updatePassword = "Update COOPWEB.WEBUSERS set PASWORD = :newPasswd where lower(trim(USERNAME)) = :username and PASWORD = :oldPasswd";

            using (OracleCommand com1 = new OracleCommand(updatePassword, oconn))
            {
                com1.Parameters.AddWithValue("newPasswd", Hash.Hash.GetHash(newPassword.Trim(), Hash.Hash.HashType.SHA512));
                com1.Parameters.AddWithValue("username", username.Trim().ToLower());
                com1.Parameters.AddWithValue("oldPasswd", Hash.Hash.GetHash(oldPassword.Trim(), Hash.Hash.HashType.SHA512));
                                                
                int count = com1.ExecuteNonQuery();

                if (count > 0)
                {
                    returnValue = true;
                }

            }       

        }
        catch (Exception ex)
        {
            // Log your error
            returnValue = false;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return returnValue;
    }

    public int GetFailedLoginCount(string username)
    {
        int failedLoginCount = -1;

        try
        {
            if (oconn.State != ConnectionState.Open)
            {
                oconn.Open();
            }

            string getFailedCount = "Select nvl(FAILED_LOGIN_COUNT,0) from COOPWEB.WEBUSERS where UPPER(USERNAME) = UPPER(:username)";

            using (OracleCommand com = new OracleCommand(getFailedCount, oconn))
            {
                com.Parameters.AddWithValue("username", username);

                int count = Convert.ToInt32(com.ExecuteScalar());

                failedLoginCount = (int)count;
            }
            
        }
        catch
        {
            failedLoginCount = -1;
        }
        finally
        {
            if (oconn.State == ConnectionState.Open)
            {
                oconn.Close();
            }
        }

        return failedLoginCount;
    }
}