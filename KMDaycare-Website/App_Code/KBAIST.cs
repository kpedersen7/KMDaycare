﻿using System;
using System.Collections.Generic;

/// <summary>
/// KBAIST acts as an interface between the UI and almost every other class in this folder, except the SecurityController and Cryptography classes.
/// </summary>
public class KBAIST
{
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
        catch (Exception e)
        {
            return false;
        }
    }
    #endregion

    #region Account and User Methods
    public bool CreateAccount(string username, string childFirstName, string childLastName, string parent1FirstName, string parent1LastName, string parent2FirstName, string parent2LastName, string homeAddress, string postalCode, string emergencyContact)
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
            m.UserName = username;
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
        catch (Exception e)
        {
            return false;
        }
    }

    public bool UpdateAccount(string username, string childFirstName, string childLastName, string parent1FirstName, string parent1LastName, string parent2FirstName, string parent2LastName, string homeAddress, string postalCode, string emergencyContact)
    {
        try
        {
            MemberController members = new MemberController();
            Member m = new Member();
            m.UserName = username;
            m.ChildFirstName = childFirstName;
            m.ChildLastName = childLastName;
            m.Parent1FirstName = parent1FirstName;
            m.Parent1LastName = parent1LastName;
            m.Parent2FirstName = parent2FirstName;
            m.Parent2LastName = parent2LastName;
            m.HomeAddress = homeAddress;
            m.PostalCode = postalCode;
            m.EmergencyContact = emergencyContact;
            bool b = members.UpdateMember(m);
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

    public bool CreateUser(string email, string username, string password, int role)
    {
        try
        {
            UserController users = new UserController();
            User u = new User();
            u.Email = email;

            u.Password = password;
            u.Role = role;
            u.UserName = username;
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
        catch (Exception e)
        {
            return false;
        }
    }

    public string MakeUsername(string one, string two, string three)
    {
        string username = one.Substring(0, 1).ToLower() + two + three;
        return username;
    }

    public User GetUser(string username)
    {
        UserController users = new UserController();
        User u = users.GetUser(username);
        return u;
    }

    public User GetUserByEmail(string email)
    {
        UserController users = new UserController();
        User u = users.GetUserByEmail(email);
        return u;
    }

    public string GetUserRole(int roleID)
    {
        UserController users = new UserController();
        try
        {
            string role = users.GetUserRole(roleID);
            return role;
        }
        catch (Exception e)
        {
            return e.Message;
        }
    }

    public Member GetMember(string username)
    {
        Member m = new Member();
        MemberController members = new MemberController();
        m = members.GetMember(username);
        return m;
    }

    public bool ToggleUserActiveStatus(string u, int active)
    {
        try
        {
            UserController users = new UserController();
            bool success = users.ToggleUserActiveStatus(u, active);
            return success;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool VerifyLogin(string UserName, string Password)
    {
        try
        {
            UserController users = new UserController();
            bool success = users.VerifyLogin(UserName, Password);
            return success;
        }
        catch
        {
            return false;
        }
    }

    public List<Member> SearchMembers(string text)
    {
        MemberController members = new MemberController();
        List<Member> foundMembers = members.SearchMembers(text);
        return foundMembers;
    }

    #endregion

    #region Activity Methods

    public bool CheckAvailabilityforActivity(DateTime StartDatetime, DateTime EndDateTime, int ClassID)
    {
        DailyActivityTimes activityTimes = new DailyActivityTimes();
        bool DayIsAvailable = true;
        DayIsAvailable = activityTimes.FindAvailabilityofActivity(StartDatetime, EndDateTime, ClassID); // call eventTimes.FindAvailability to check day is available

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

    #endregion

    #region HELPER METHODS
    public string MakeMuricanDate(DateTime d)
    {
        string humanFriendlyDate = "";
        string dayOfWeek = d.Date.DayOfWeek.ToString();
        string month = "";
        switch (d.Month)
        {
            case 1:
                month = "January";
                break;
            case 2:
                month = "February";
                break;
            case 3:
                month = "March";
                break;
            case 4:
                month = "April";
                break;
            case 5:
                month = "May";
                break;
            case 6:
                month = "June";
                break;
            case 7:
                month = "July";
                break;
            case 8:
                month = "August";
                break;
            case 9:
                month = "September";
                break;
            case 10:
                month = "October";
                break;
            case 11:
                month = "November";
                break;
            case 12:
                month = "December";
                break;
            default:
                month = "?";
                break;
        }
        string day = d.Day.ToString();
        string year = d.Year.ToString();
        humanFriendlyDate = string.Format("{0} {1} {2}, {3}", dayOfWeek, month, day, year);
        return humanFriendlyDate;
    }

    public string MakeNiceTime(DateTime d)
    {
        DateTime dAtNoon = d.Subtract(d.TimeOfDay);
        dAtNoon = dAtNoon.AddHours(12);
        string time = "";
        if (d > dAtNoon)
        {
            if (d.Hour != 12)
            {
                time = d.AddHours(12).TimeOfDay.ToString();
            }
            else
            {
                time = d.TimeOfDay.ToString();
            }
            time = time.Remove(5, 3);
            if (d.Hour >= 13 && d.Hour < 22)
            {
                time = time.Substring(1);
            }
            time = time + " PM";
        }
        else
        {

            time = d.TimeOfDay.ToString();
            time = time.Remove(5, 3);
            if (d.Hour < 10)
            {
                time = time.Substring(1);
            }
            time = time + " AM";
        }
        return time;
    }

    public DateTime MakeSQLDateTime(int year, int month, int day, string time)
    {
        DateTime date = DateTime.Parse(String.Format("{0}-{1}-{2} {3}", year, month, day, time));
        return date;
    }
    #endregion
}