using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddDailyActivity : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Authenticate();
        DateTime selectedDate = ActivityDate.SelectedDate;
        if (!Page.IsPostBack)
        {
            GetActivitiesForDay(selectedDate);
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
        }
        else
        {
            GetActivitiesForDay(selectedDate);
        }
    }

    protected void submit_button_Click(object sender, EventArgs e)
    {
        //SetCookie();
        KBAIST kBaist = new KBAIST();
        DateTime startDateTime = kBaist.MakeSQLDateTime(ActivityDate.SelectedDate.Year, ActivityDate.SelectedDate.Month, ActivityDate.SelectedDate.Day, StartTime.SelectedValue.ToString());
        DateTime endDateTime = kBaist.MakeSQLDateTime(ActivityDate.SelectedDate.Year, ActivityDate.SelectedDate.Month, ActivityDate.SelectedDate.Day, EndTime.SelectedValue.ToString());
        string description = DescriptionofActivity.Text.Trim();
        string notes = Notes.Text.Trim();
        bool dayIsAvailable = kBaist.CheckAvailabilityforActivity(startDateTime, endDateTime, int.Parse(ClassID.SelectedValue));
        //bool validated = ValidateInput(startDateTime, endDateTime, description, notes);
        if (dayIsAvailable)/* && validated*/
        {
            kBaist.CreateActivity(startDateTime, endDateTime, DescriptionofActivity.Text, Notes.Text , int.Parse(ClassID.SelectedValue));
            messageLabel.Text = "Activity Created Successfully!";
            DescriptionofActivity.Text = String.Empty;
            Notes.Text = String.Empty;
        }
        else
        {
            messageLabel.Text = "Choose another time! That time is already occupied by another activity";
        }
    }

    protected void ActivityDate_SelectionChanged(object sender, EventArgs e)
    {
        DateTime selectedDay = ActivityDate.SelectedDate;
        GetActivitiesForDay(selectedDay);
        TheDate.Text = " - " + MakeMuricanDate(selectedDay);
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
        Activities1.Controls.Clear();
        Activities2.Controls.Clear();
        Activities3.Controls.Clear();
        Activities4.Controls.Clear();
        KBAIST kBaist = new KBAIST();
        List<DailyActivity> activitiesForDay = kBaist.GetActivities(selectedDay, selectedDay.AddDays(1));
        if (activitiesForDay.Count > 0)
        {
            foreach (DailyActivity da in activitiesForDay)
            {
                Panel p = new Panel();
                p.Attributes.Add("class", "card");

                LinkButton lb = new LinkButton();
                lb.Text = "(-)";
                lb.Attributes.Add("href", "AddDailyActivity.aspx?d=" + da.DailyActivityID);
                lb.Attributes.Add("class", "pull-right");
                p.Controls.Add(lb);

                Panel linkPanel = new Panel();

                Label l = new Label();
                l.Text = MakeNiceTime(da.StartDateTime);
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
                l.Text = MakeNiceTime(da.EndDateTime);
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
                    case "KinderGarten(5 Years +)":
                        Activities4.Controls.Add(p);
                        break;
                }
            }
        }
    }

    private void RemoveActivity(int id)
    {
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

    private string MakeMuricanDate(DateTime d)
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
        humanFriendlyDate = string.Format("{0} {1} {2}, {3}", dayOfWeek,month,day,year );
        return humanFriendlyDate;
    }

    private string MakeNiceTime(DateTime d)
    {
        DateTime dAtNoon = d.Subtract(d.TimeOfDay);
        dAtNoon = dAtNoon.AddHours(12);
        string time = "";
        if(d > dAtNoon)
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
            if(d.Hour < 10)
            {
                time = time.Substring(1);
            }
            time = time + " AM";
        }
        return time;
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
}