using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.OracleClient;

/// <summary>
/// Summary description for TimeCalcuator
/// </summary>
public class TimeCalcuator
{
    OracleConnection orcconn = new OracleConnection(ConfigurationManager.AppSettings["DBConStrQuot"]);
    public TimeCalcuator()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public string UpdatedTimeCalculator(DateTime inputTime)
    {
        string disMgs = "";
        if (orcconn.State != ConnectionState.Open)
        {
            orcconn.Open();
        }
        try
        {
            OracleCommand cmd = orcconn.CreateCommand();

            using (cmd)
            {

                string sqlCurrentTime = "SELECT SYSDATE FROM DUAL";
                cmd.CommandText = sqlCurrentTime;
                DateTime currentDate = DateTime.Parse(cmd.ExecuteScalar().ToString());

                    double seconds = (Convert.ToDateTime(currentDate) - Convert.ToDateTime(inputTime)).TotalSeconds;

                    int second = Convert.ToInt32(seconds);
                    if (second < 3)
                    {
                        disMgs = "Just now".ToString();
                    }
                    else if (second > 2 && second < 60)
                    {
                        disMgs = second + " seconds ago".ToString();
                    }
                    else if (second > 59)
                    {
                        double minutes = (Convert.ToDateTime(currentDate) - Convert.ToDateTime(inputTime)).TotalMinutes;
                        int minute = Convert.ToInt32(minutes);
                        if (minute == 1)
                        {
                            disMgs = minute + " minute ago".ToString();
                        }
                        else if (minute > 1 && minute < 61)
                        {
                            disMgs = minute + " minutes ago".ToString();
                        }
                        else if (minute > 60)
                        {

                            double hours = (Convert.ToDateTime(currentDate) - Convert.ToDateTime(inputTime)).TotalHours;
                            int hour = Convert.ToInt32(hours);
                            if (hour == 1)
                            {
                                disMgs = hour + " hour ago".ToString();
                            }
                            else if (hour > 1 && hour < 24)
                            {
                                disMgs = hour + " hours ago".ToString();
                            }
                            else if (hour > 23)
                            {
                                double days = (Convert.ToDateTime(currentDate) - Convert.ToDateTime(inputTime)).TotalDays;
                                int day = Convert.ToInt32(days);

                                int years = 0;
                                if (day > 365 || day == 365)
                                {
                                    years = Convert.ToInt32(day / 365);
                                }
                                if (day == 1)
                                {
                                    disMgs = "Yesterday at " + inputTime.ToString("hh:mm tt");

                                }
                                else if (day > 1 && day < 7)
                                {
                                    disMgs = Convert.ToDateTime(inputTime).ToString("dddd") + " at " + Convert.ToDateTime(inputTime).ToString("hh:mm tt");

                                }
                                else if (day == 7)
                                {
                                    disMgs = "a week ago".ToString();
                                }
                                else if (day > 6 && day < 29)
                                {
                                    disMgs = day + " days ago".ToString();
                                }
                                else if (day == 29)
                                {
                                    disMgs = day + " days ago".ToString();
                                }
                                else if (day > 28 && day < 365)
                                {
                                    disMgs = Convert.ToDateTime(inputTime).ToString("MMMM dd") + " at " + Convert.ToDateTime(inputTime).ToString("hh:mm tt");

                                }
                                else if (years == 1)
                                {
                                    disMgs = years + " year ago";

                                }
                                else if (years > 1)
                                {
                                    disMgs = years + " years ago";

                                }
                            }

                        }
                    }

                


            }
        }
        catch (Exception)
        {

        }
        finally
        {
            if (orcconn.State == ConnectionState.Open)
            {
                orcconn.Close();
            }
        }
        return disMgs;
    }

    public DateTime CurrentDBTime()
    {
        DateTime currentDateTime = DateTime.Now; ;
        if (orcconn.State != ConnectionState.Open)
        {
            orcconn.Open();
        }
        try
        {
            OracleCommand cmd = orcconn.CreateCommand();

            using (cmd)
            {

                string sqlCurrentTime = "SELECT SYSDATE FROM DUAL";
                cmd.CommandText = sqlCurrentTime;
                currentDateTime = DateTime.Parse(cmd.ExecuteScalar().ToString());

            }
        }
        catch (Exception)
        {

        }
        finally
        {
            if (orcconn.State == ConnectionState.Open)
            {
                orcconn.Close();
            }
        }
        return currentDateTime;
    }

}