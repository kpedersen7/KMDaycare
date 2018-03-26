using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for KBAIST
/// </summary>
public class KBAIST
{
    public KBAIST()
    {
    }

    public bool CheckAvailability(DateTime StartDatetime, DateTime EndDateTime)
    {
        EventTimes eventTimes = new EventTimes();
        bool DayIsAvailable = true;
        DayIsAvailable = eventTimes.FindAvailability(StartDatetime, EndDateTime); // call eventTimes.FindAvailability to check day is available

        return DayIsAvailable;
    }

    public bool CreateEvent(DateTime startTime, DateTime endTime, string description, string notes)
    {
        EventTimes eventTimes = new EventTimes();
        Event eventForAdd = new Event();
        eventForAdd.StartDateTime = startTime;
        eventForAdd.EndDateTime = endTime;
        eventForAdd.Description = description;
        eventForAdd.Notes = notes;

        bool success = eventTimes.AddEvent(eventForAdd);
        return success;
    }

    public List<Event> GetEvents(DateTime minDay, DateTime maxDay)
    {
        EventTimes events = new EventTimes();
        List<Event> eventsForDay = events.FindEvents(minDay, maxDay);
        return eventsForDay;
    }

    public bool RemoveEvent(int id)
    {
        EventTimes events = new EventTimes();
        try
        {
            bool b = events.DeleteEvent(id);
            if (b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch(Exception e)
        {
            return false;
        }
    }


    #region Verify Login
    public bool VerifyLogin(string EmailId, string Password)
    {
        try
        {
            bool success = false;
            CheckLogin objChk = new CheckLogin();
            success= objChk.VerifyLogin(EmailId, Password);
            return success;
        }
        catch
        {
            return false;
        }
    }

    #endregion
}