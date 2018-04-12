using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddDailyActivity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            string cookie = null;
            if (Response.Cookies["SelectedDay"].Value != null)
            {
                cookie = Response.Cookies["SelectedDay"].Value;
            }
            if (Request.QueryString["d"] != null)
            {
                try
                {
                    int id = int.Parse(Request.QueryString["d"]);
                    if (id != 0)
                    {
                        RemoveActivity(id);
                    }
                    else
                    {
                        messageLabel.Text = "Activity Deleted Successfully!";
                    }
                }
                catch (Exception ex)
                {

                }
            }
            if (cookie != null)
            {
                try
                {
                    DateTime selectedDate = DateTime.Parse(cookie);
                    ActivityDate.SelectedDate = selectedDate;
                    GetActivitiesForDay(selectedDate);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    protected void submit_button_Click(object sender, EventArgs e)
    {
        SetCookie();
        KBAIST kBaist = new KBAIST();
        bool DayIsAvailable = true;
        int year = ActivityDate.SelectedDate.Year;
        int month = ActivityDate.SelectedDate.Month;
        int day = ActivityDate.SelectedDate.Day;
        DateTime startDateTime = DateTime.Parse(String.Format("{0}-{1}-{2} {3}", year, month, day, StartTime.SelectedValue.ToString()));
        DateTime endDateTime = DateTime.Parse(String.Format("{0}-{1}-{2} {3}", year, month, day, EndTime.SelectedValue.ToString()));
    
        DayIsAvailable = kBaist.CheckAvailabilityforActivity(startDateTime, endDateTime, int.Parse(ClassID.SelectedValue)); // call kbaist.CheckAvailability to check day is available

        if (DayIsAvailable)
        {
            kBaist.CreateActivity(startDateTime, endDateTime, DescriptionofActivity.Text, Notes.Text , int.Parse(ClassID.SelectedValue));
            messageLabel.Text = "Activity Created Successfully!";
            DescriptionofActivity.Text = String.Empty;
            Notes.Text = String.Empty;
         
        }
        else if (!DayIsAvailable)
        {
            messageLabel.Text = "Choose another time! That time is already occupied by another activity";
        }
    }

    protected void ActivityDate_SelectionChanged(object sender, EventArgs e)
    {
        DateTime selectedDay = ActivityDate.SelectedDate;
        GetActivitiesForDay(selectedDay);
    }

    private void GetActivitiesForDay(DateTime selectedDay)
    {
        KBAIST kBaist = new KBAIST();
        List<DailyActivity> activitiesForDay = kBaist.GetActivities(selectedDay, selectedDay.AddDays(1));
        if (activitiesForDay.Count > 0)
        {
            foreach (DailyActivity da in activitiesForDay)
            {
                Panel p = new Panel();
                p.Attributes.Add("class", "card");
                Label l = new Label();
                l.Text = da.DescriptionofActivity;
                l.Attributes.Add("id", "activity" + da.DailyActivityID + "DescriptionofActivity");
                p.Controls.Add(l);
                l = new Label();
                l.Text = da.StartDateTime + " to " + da.EndDateTime;
                p.Controls.Add(l);
                l = new Label();
                l.Text = "Notes: " + da.Notes;
                p.Controls.Add(l);

                l = new Label();
                l.Text = "ClassDescription: " + da.ClassDescription;
                p.Controls.Add(l);
                LinkButton lb = new LinkButton();
                lb.Text = "Delete";
                lb.Attributes.Add("href", "AddDailyActivity.aspx?d=" + da.DailyActivityID);
                p.Controls.Add(lb);
                ActivitiesForDay.Controls.Add(p);
            }
        }
        SetCookie();
    }

    private void RemoveActivity(int id)
    {
        SetCookie();
        KBAIST kb = new KBAIST();
        bool b = kb.RemoveActivity(id);
        if (b)
        {
            Response.Redirect("AddDailyActivity.aspx?d=0");
        }
        else
        {
            messageLabel.Text = "Something went wrong, activity was not deleted.";
        }
    }

    private string MakeFormattedDate(DateTime d)
    {
        int year = d.Year;
        int month = d.Month;
        int day = d.Day;
        return String.Format("{0}-{1}-{2} 00.000", year, month, day);
    }

    private void SetCookie()
    {
        HttpCookie cookie = new HttpCookie("SelectedDay", ActivityDate.SelectedDate.ToString());
        Response.Cookies.Add(cookie);
    }
}