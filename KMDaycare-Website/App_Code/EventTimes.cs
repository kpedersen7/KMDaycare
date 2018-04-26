using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

/// <summary>
/// Summary description for EventTimes
/// </summary>
public class EventTimes
{
    public bool FindAvailability(DateTime StartDateTime, DateTime EndDateTime)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("FindAvailability", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StartDatetime", StartDateTime);
                cmd.Parameters.AddWithValue("@EndDateTime", EndDateTime);
                con.Open();

                Event foundEvent = new Event();
                SqlDataReader sqldr = cmd.ExecuteReader();
                while (sqldr.Read())
                {
                    foundEvent.EventID = int.Parse(sqldr["EventID"].ToString());
                    foundEvent.StartDateTime = DateTime.Parse(sqldr["StartDateTime"].ToString());
                    foundEvent.EndDateTime = DateTime.Parse(sqldr["EndDateTime"].ToString());
                    foundEvent.Description = sqldr["Description"].ToString();
                    foundEvent.Notes = sqldr["Notes"].ToString();
                    //somethingFound = true;
                }
                con.Close();

                if (foundEvent.EventID != 0)
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

    public bool AddEvent(Event eventForAdd)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("AddEvent", con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@StartDatetime", eventForAdd.StartDateTime);
                    cmd.Parameters.AddWithValue("@EndDateTime", eventForAdd.EndDateTime);
                    cmd.Parameters.AddWithValue("@Notes", eventForAdd.Notes);
                    cmd.Parameters.AddWithValue("@Description", eventForAdd.Description);
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

    public List<Event> FindEvents(DateTime minDay, DateTime maxDay)
    {
        List<Event> events = new List<Event>();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetEvents", con))
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
                        Event anEvent = new Event();
                        anEvent.EventID = int.Parse(sqldr["EventID"].ToString());
                        anEvent.StartDateTime = DateTime.Parse(sqldr["StartDateTime"].ToString());
                        anEvent.EndDateTime = DateTime.Parse(sqldr["EndDateTime"].ToString());
                        anEvent.Description = sqldr["Description"].ToString();
                        anEvent.Notes = sqldr["Notes"].ToString();
                        events.Add(anEvent);
                    }
                    con.Close();
                }
                catch
                {

                }

            }
        }
        return events;
    }

    public bool DeleteEvent(int id)
    {
        try
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("DeleteEvent", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@eventID", id);
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