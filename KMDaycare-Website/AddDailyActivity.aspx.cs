﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddDailyActivity : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    {
        Authenticate();
    }

    private void Authenticate()
    {
        SecurityController s = HttpContext.Current.User as SecurityController;
        if (s != null)
        {
            if (!s.IsInRole("Admin"))
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckForCookie();
            if (Request.QueryString["d"] != null)
            {
                RemoveActivity(int.Parse(Request.QueryString["d"]));
            }
        }
        else
        {
            //CheckForCookie();
        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        KBAIST kBaist = new KBAIST();
        DateTime startDateTime = kBaist.MakeSQLDateTime(ActivityDate.SelectedDate.Year, ActivityDate.SelectedDate.Month, ActivityDate.SelectedDate.Day, StartTime.SelectedValue.ToString());
        DateTime endDateTime = kBaist.MakeSQLDateTime(ActivityDate.SelectedDate.Year, ActivityDate.SelectedDate.Month, ActivityDate.SelectedDate.Day, EndTime.SelectedValue.ToString());
        string description = DescriptionofActivity.Text.Trim();
        string notes = Notes.Text.Trim();
        bool dayIsAvailable = kBaist.CheckAvailabilityforActivity(startDateTime, endDateTime, int.Parse(ClassID.SelectedValue));
        //bool validated = ValidateInput(startDateTime, endDateTime, description, notes);
        if (dayIsAvailable)/* && validated*/
        {
            kBaist.CreateActivity(startDateTime, endDateTime, DescriptionofActivity.Text, Notes.Text, int.Parse(ClassID.SelectedValue));
            messageLabel.Text = "Activity Created Successfully!";
            DescriptionofActivity.Text = String.Empty;
            Notes.Text = String.Empty;
        }
        else
        {
            messageLabel.Text = "The times you have picked conflict with a pre-existing event, please check the schedule and try again.";
        }
        GetActivitiesForDay(DateTime.Parse(Request.Cookies["SelectedDay"].Value));
        //CheckForCookie();
    }

    protected void ActivityDate_SelectionChanged(object sender, EventArgs e)
    {
        KBAIST kBaist = new KBAIST();
        DateTime selectedDay = ActivityDate.SelectedDate;
        GetActivitiesForDay(selectedDay);
    }

    private bool ValidateInput(DateTime start, DateTime end, string description, string notes)
    {
        try
        {
            if (DateTime.Compare(start, end) >= 0)
            {
                messageLabel.Text = "The end time cannot be before, or the same as, the start time.";
                return false;
            }
            if (DateTime.Compare(start, DateTime.Now) >= 0)
            {
                messageLabel.Text = "The start time must be in the future.";
                return false;
            }
            if (string.IsNullOrEmpty(description))
            {
                messageLabel.Text = "Please give the activity a name/description.";
                return false;
            }
            if (description.Length > 100)
            {
                messageLabel.Text = "Name cannot exceed 100 characters.";
                return false;
            }
            if (notes.Length > 500)
            {
                messageLabel.Text = "Notes cannot exceed 500 characters.";
                return false;
            }
            return true;
        }
        catch (Exception e)
        {
            messageLabel.Text = "Whoops, something is wrong with one or more of the input fields.";
            messageLabel.Text = "Nice try, Dave";
            return false;
        }
    }

    private void GetActivitiesForDay(DateTime selectedDay)
    {
        SetCookie(selectedDay);
        Activities1.Controls.Clear();
        Activities2.Controls.Clear();
        Activities3.Controls.Clear();
        Activities4.Controls.Clear();
        KBAIST kBaist = new KBAIST();
        TheDate.Text = " - " + kBaist.MakeMuricanDate(selectedDay);
        List<DailyActivity> activitiesForDay = kBaist.GetActivities(selectedDay, selectedDay.AddDays(1));
        if (activitiesForDay.Count > 0)
        {
            foreach (DailyActivity da in activitiesForDay)
            {
                Panel p = new Panel();
                p.Attributes.Add("class", "card");

                LinkButton lb = new LinkButton();
                lb.Text = "(x)";
                lb.Attributes.Add("href", "AddDailyActivity.aspx?d=" + da.DailyActivityID);
                lb.Attributes.Add("class", "pull-right");
                p.Controls.Add(lb);

                Panel linkPanel = new Panel();

                Label l = new Label();
                l.Text = kBaist.MakeNiceTime(da.StartDateTime);
                linkPanel.Controls.Add(l);
                linkPanel.Controls.Add(lb);
                p.Controls.Add(linkPanel);

                l = new Label();
                l.Text = da.DescriptionofActivity;
                l.Attributes.Add("id", "activity" + da.DailyActivityID + "DescriptionofActivity");
                p.Controls.Add(l);


                l = new Label();
                l.Text = "Notes: " + da.Notes;
                p.Controls.Add(l);

                l = new Label();
                l.Text = kBaist.MakeNiceTime(da.EndDateTime);
                p.Controls.Add(l);

                switch (da.ClassDescription)
                {
                    case "6-9 months":
                        Activities1.Controls.Add(p);
                        break;
                    case "3 Years Old":
                        Activities2.Controls.Add(p);
                        break;
                    case "PreSchool (4 Years+)":
                        Activities3.Controls.Add(p);
                        break;
                    case "KinderGarten (5 Years+)":
                        Activities4.Controls.Add(p);
                        break;
                }
            }
        }
    }

    private void RemoveActivity(int id)
    {
        KBAIST kb = new KBAIST();
        try
        {
            if (id != 0)
            {
                bool b = kb.RemoveActivity(id);
                if (b)
                {
                    Response.Redirect("AddDailyActivity.aspx");
                }
                else
                {
                    messageLabel.Text = "Something went wrong, activity was not deleted.";
                }
            }
        }
        catch (Exception ex)
        {
            messageLabel.Text = "Something went wrong, activity was not deleted.";
        }
        CheckForCookie();
    }

    private void SetCookie(DateTime d)
    {
        HttpCookie cookie = new HttpCookie("SelectedDay", d.ToString());
        cookie.Expires = DateTime.Now.AddHours(1);
        Response.Cookies.Add(cookie);
    }

    private void CheckForCookie()
    {
        if (Request.Cookies["SelectedDay"] != null && Request.Cookies["SelectedDay"].Value != null)
        {
            GetActivitiesForDay(DateTime.Parse(Request.Cookies["SelectedDay"].Value));
        }
        else
        {
            SetCookie(DateTime.Today);
            GetActivitiesForDay(DateTime.Today);
        }
    }


}