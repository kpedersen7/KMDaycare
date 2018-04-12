using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


/// <summary>
/// Summary description for DailyActivityTimes
/// </summary>
public class DailyActivityTimes
{
    public DailyActivityTimes()
    {
        //
        // TODO: Add constructor logic here
        //
    }


    public bool FindAvailabilityofActivity(DateTime StartDateTime, DateTime EndDateTime, int ClassID)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("FindAvailabilityfordailyactivity", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDatetime", StartDateTime);
                cmd.Parameters.AddWithValue("@EndDateTime", EndDateTime);
                cmd.Parameters.AddWithValue("@ClassID", ClassID);
                con.Open();

                DailyActivity foundactivity = new DailyActivity();
                SqlDataReader sqldr = cmd.ExecuteReader();
                while (sqldr.Read())
                {
                    foundactivity.DailyActivityID = int.Parse(sqldr["DailyActivityID"].ToString());
                    foundactivity.StartDateTime = DateTime.Parse(sqldr["StartDateTime"].ToString());
                    foundactivity.EndDateTime = DateTime.Parse(sqldr["EndDateTime"].ToString());
                    foundactivity.DescriptionofActivity = sqldr["DescriptionofActivity"].ToString();
                    foundactivity.Notes = sqldr["Notes"].ToString();
                    foundactivity.ClassID = int.Parse(sqldr["ClassID"].ToString());
                    //somethingFound = true;
                }
                con.Close();

                if (foundactivity.DailyActivityID != 0)
                {
                    return false; // day is taken
                }
                else
                {
                    return true; // day is not taken
                }
            }
        }

    }

    public bool AddDailyActivity(DailyActivity activityForAdd)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("AddDailyActivity", con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartDatetime", activityForAdd.StartDateTime);
                    cmd.Parameters.AddWithValue("@EndDateTime", activityForAdd.EndDateTime);
                    cmd.Parameters.AddWithValue("@Notes", activityForAdd.Notes);
                    cmd.Parameters.AddWithValue("@DescriptionofActivity", activityForAdd.DescriptionofActivity);
                    cmd.Parameters.AddWithValue("@ClassID", activityForAdd.ClassID);
                    con.Open();

                    SqlDataReader sqldr = cmd.ExecuteReader();
                    con.Close();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }
    }

    public List<DailyActivity> FindDailyActivities(DateTime minDay, DateTime maxDay)
    {
        List<DailyActivity> activities = new List<DailyActivity>();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetDailyActivities", con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@minDay", minDay);
                    cmd.Parameters.AddWithValue("@maxDay", maxDay);
                    con.Open();

                    SqlDataReader sqldr = cmd.ExecuteReader();
                    while (sqldr.Read())
                    {
                        DailyActivity anActivity = new DailyActivity();
                        anActivity.DailyActivityID = int.Parse(sqldr["DailyActivityID"].ToString());
                        anActivity.StartDateTime = DateTime.Parse(sqldr["StartDateTime"].ToString());
                        anActivity.EndDateTime = DateTime.Parse(sqldr["EndDateTime"].ToString());
                        anActivity.DescriptionofActivity = sqldr["DescriptionofActivity"].ToString();
                        anActivity.Notes = sqldr["Notes"].ToString();
                        anActivity.ClassDescription = sqldr["ClassDescription"].ToString();
                        activities.Add(anActivity);
                    }
                    con.Close();
                }
                catch(Exception e)
                {

                }

            }
        }
        return activities;
    }

    public bool DeleteDailyActivity(int id)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteActivity", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@DailyActivityID", id);
                    con.Open();
                    SqlDataReader sqldr = cmd.ExecuteReader();
                    con.Close();
                }
            }
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }
}