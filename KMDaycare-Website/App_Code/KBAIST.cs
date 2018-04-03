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

    #region Event Methods
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
    #endregion

    #region Account and User Methods
    public bool CreateAccount(string childFirstName, string childLastName, string parent1FirstName, string parent1LastName, string parent2FirstName, string parent2LastName, string homeAddress, string postalCode, string emergencyContact)
    {
        try
        {
            MemberController members = new MemberController();
            Member m = new Member();
            m.ChildFirstName = childFirstName;
            m.ChildLastName = childLastName;
            m.Parent1FirstName = parent1FirstName;
            m.Parent1LastName = parent1LastName;
            m.Parent2FirstName = parent2FirstName;
            m.Parent2LastName = parent2LastName;
            m.HomeAddress = homeAddress;
            m.PostalCode = postalCode;
            m.EmergencyContact = emergencyContact;
            m.UserName = MakeUsername(m);
            bool success = members.CreateMember(m);
            if (success)
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

    public bool CreateUser(string email, string password, int role)
    {
        try
        {
            UserController users = new UserController();
            User u = new User();
            u.Email = email;
            u.Password = password;
            u.Role = role;
            bool success = users.CreateUser(u);
            if (success)
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

    private string MakeUsername(Member m)
    {
        string username = m.ChildFirstName.Substring(0,1).ToLower() + m.ChildLastName+ m.Parent1FirstName;
        return username;
    }

    public Member GetMember()
    {
        Member m = new Member();
        //get member stuff
        return m;
    }
    #endregion

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

    public bool CheckAvailabilityforActivity(DateTime StartDatetime, DateTime EndDateTime)
    {
        DailyActivityTimes activityTimes = new DailyActivityTimes();
        bool DayIsAvailable = true;
        DayIsAvailable = activityTimes.FindAvailabilityofActivity(StartDatetime, EndDateTime); // call eventTimes.FindAvailability to check day is available

        return DayIsAvailable;
    }

    public bool CreateActivity(DateTime startTime, DateTime endTime, string descriptionofactivity, string notes, int Classid)
    {
        DailyActivityTimes activityTimes = new DailyActivityTimes();
        DailyActivity activityForAdd = new DailyActivity();
        activityForAdd.StartDateTime = startTime;
        activityForAdd.EndDateTime = endTime;
        activityForAdd.DescriptionofActivity = descriptionofactivity;
        activityForAdd.Notes = notes;
        activityForAdd.ClassID = Classid;

        bool success = activityTimes.AddDailyActivity(activityForAdd);
        return success;
    }

    public List<DailyActivity> GetActivities(DateTime minDay, DateTime maxDay)
    {
        DailyActivityTimes activities = new DailyActivityTimes();
        List<DailyActivity> activitiesForDay = activities.FindDailyActivities(minDay, maxDay);
        return activitiesForDay;
    }

    public bool RemoveActivity(int id)
    {
        DailyActivityTimes activities = new DailyActivityTimes();
        try
        {
            bool b = activities.DeleteDailyActivity(id);
            if (b)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        catch (Exception e)
        {
            return false;
        }
    }
}