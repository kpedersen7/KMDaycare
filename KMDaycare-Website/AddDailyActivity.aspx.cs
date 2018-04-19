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
        SecurityController s = HttpContext.Current.User as SecurityController;
        if (s != null)
        {
            UserController users = new UserController();
            User u = users.GetUser(HttpContext.Current.User.Identity.Name);
            if (!s.IsInRole("Admin"))
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
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
        DateTime startDateTime = kBaist.MakeSQLDateTime(ActivityDate.SelectedDate.Year, ActivityDate.SelectedDate.Month, ActivityDate.SelectedDate.Day, StartTime.SelectedValue.ToString());
        DateTime endDateTime = kBaist.MakeSQLDateTime(ActivityDate.SelectedDate.Year, ActivityDate.SelectedDate.Month, ActivityDate.SelectedDate.Day, EndTime.SelectedValue.ToString());
        string description = DescriptionofActivity.Text.Trim();
        string notes = Notes.Text.Trim();
        bool dayIsAvailable = kBaist.CheckAvailabilityforActivity(startDateTime, endDateTime, int.Parse(ClassID.SelectedValue));
        bool validated = ValidateInput(startDateTime, endDateTime, description, notes);
        if (dayIsAvailable && validated)
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

    private void SetCookie()
    {
        HttpCookie cookie = new HttpCookie("SelectedDay", ActivityDate.SelectedDate.ToString());
        Response.Cookies.Add(cookie);
    }
}