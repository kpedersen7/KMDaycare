using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddEvent : System.Web.UI.Page
{
    protected void Page_PreInit(object sender, EventArgs e)
    { 
        Authenticate();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            CheckForCookie();
        }
        else
        {
            CheckForCookie();
        }
        
    }

    #region AUTHENTICATION
    private void Authenticate()
    {
        SecurityController s = HttpContext.Current.User as SecurityController;
        if (s != null)
        {
            if (s.IsInRole("Admin"))
            {
                DoAdminLoad();
            }
            if (s.IsInRole("Parent"))
            {
                Page.MasterPageFile = "~/General.master";
                DoParentLoad();
            }
            if (!s.IsInRole("Admin") && !s.IsInRole("Parent"))
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    private void DoAdminLoad()
    {
        if (Request.QueryString["d"] != null)
        {
            RemoveEvent(int.Parse(Request.QueryString["d"]));
        }
    }

    private void DoParentLoad()
    {
        AdminControlsPanel.Controls.Remove(AdminControls);
    }
    #endregion

    #region EVENT HANDLERS
    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        KBAIST kBaist = new KBAIST();
        DateTime startDateTime = kBaist.MakeSQLDateTime(EventDate.SelectedDate.Year, EventDate.SelectedDate.Month, EventDate.SelectedDate.Day, StartTime.SelectedValue.ToString());
        DateTime endDateTime = kBaist.MakeSQLDateTime(EventDate.SelectedDate.Year, EventDate.SelectedDate.Month, EventDate.SelectedDate.Day, EndTime.SelectedValue.ToString());
        string description = Description.Text.Trim();
        string notes = Notes.Text.Trim();
        bool validated = true;// ValidateInput(startDateTime, endDateTime, description, notes);
        bool dayIsAvailable = kBaist.CheckAvailability(startDateTime, endDateTime);
        if (dayIsAvailable && validated)
        {
            kBaist.CreateEvent(startDateTime, endDateTime, description, notes);
            messageLabel.Text = "Event Created Successfully!";
            Description.Text = String.Empty;
            Notes.Text = String.Empty;
        }
        else if (!dayIsAvailable)
        {
            messageLabel.Text = "Choose another time! That time is already occupied by another event.";
        }
        //CheckForCookie();
        GetEventsForDay(startDateTime);
    }

    protected void EventDate_SelectionChanged(object sender, EventArgs e)
    {
        KBAIST kBaist = new KBAIST();
        DateTime selectedDay = EventDate.SelectedDate;
        TheDate.Text = kBaist.MakeMuricanDate(selectedDay);
        GetEventsForDay(selectedDay);
    }

    protected void StartTime_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

    private bool ValidateInput(DateTime start, DateTime end, string description, string notes)
    {
        try
        {
            if (DateTime.Compare(start, end) >= 0)
            {
                messageLabel.Text = "The end time cannot be before, or the same as, the start time.";
                return false;
            }
            if (DateTime.Compare(start, DateTime.Now) <= 0)
            {
                messageLabel.Text = "The start time must be in the future.";
                return false;
            }
            if (string.IsNullOrEmpty(description))
            {
                messageLabel.Text = "Please give the event a name/description.";
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

    private void GetEventsForDay(DateTime selectedDay)
    {
        SetCookie(selectedDay);
        EventsForDay.Controls.Clear();
        KBAIST kBaist = new KBAIST();
        List<Event> eventsForDay = kBaist.GetEvents(selectedDay, selectedDay.AddDays(1));
        DateTime day = DateTime.Today.AddHours(6);
        while(day <= DateTime.Today.AddHours(18))
        {
            ListItem li = new ListItem();
            li.Text = kBaist.MakeNiceTime(day);
            li.Value = day.TimeOfDay.ToString();
            StartTime.Items.Add(li);
            day = day.AddMinutes(30);
        }
        if (eventsForDay.Count > 0)
        {
            foreach (Event ev in eventsForDay)
            {
                Panel p = new Panel();
                p.Attributes.Add("class", "card");

                
                LinkButton lb = new LinkButton();
                lb.Text = "(x)";
                lb.Attributes.Add("href", "AddEvent.aspx?d=" + ev.EventID);
                lb.Attributes.Add("class", "pull-right");
                p.Controls.Add(lb);

                Panel linkPanel = new Panel();

                Label l = new Label();
                l.Text = kBaist.MakeNiceTime(ev.StartDateTime);
                linkPanel.Controls.Add(l);
                SecurityController s = HttpContext.Current.User as SecurityController;
                if (s.IsInRole("Admin"))
                {
                    linkPanel.Controls.Add(lb);
                }
                p.Controls.Add(linkPanel);

                l = new Label();
                l.Text = ev.Description;
                p.Controls.Add(l);
                l = new Label();
                l.Text = "Notes: " + ev.Notes;
                p.Controls.Add(l);
                l = new Label();
                l.Text = kBaist.MakeNiceTime(ev.EndDateTime);
                p.Controls.Add(l);

                EventsForDay.Controls.Add(p);
            }
        }
//snippet
    }

    private void RemoveEvent(int id)
    {
        KBAIST kb = new KBAIST();
        try
        {
            if (id != 0)
            {
                bool b = kb.RemoveEvent(id);
                if (b)
                {
                    Response.Redirect("AddEvent.aspx?d=0");
                }
                else
                {
                    messageLabel.Text = "Something went wrong, event was not deleted.";
                }
            }
            else
            {
                messageLabel.Text = "Event Deleted Successfully!";
            }
        }
        catch (Exception ex)
        {

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
        if (Response.Cookies["SelectedDay"] != null && Response.Cookies["SelectedDay"].Value != null)
        {
            GetEventsForDay(DateTime.Parse(Request.Cookies["SelectedDay"].Value));
        }
        else
        {
            SetCookie(DateTime.Today);
            GetEventsForDay(DateTime.Today);
        }
    }

}