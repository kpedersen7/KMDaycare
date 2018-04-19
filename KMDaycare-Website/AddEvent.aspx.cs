using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddEvent : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityController s = HttpContext.Current.User as SecurityController;
        if (s != null)
        {
            UserController users = new UserController();
            User u = users.GetUser(HttpContext.Current.User.Identity.Name);
            if (!s.IsInRole("Admin") && !s.IsInRole("Parent"))
            {
                Response.Redirect("Default.aspx");
            }
            if (s.IsInRole("Parent"))
            {
                DoParentLoad();
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
                        RemoveEvent(id);
                    }
                    else
                    {
                        messageLabel.Text = "Event Deleted Successfully!";
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
                    EventDate.SelectedDate = selectedDate;
                    GetEventsForDay(selectedDate);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }

    private void DoParentLoad()
    {
        AdminControlsPanel.Controls.Remove(AdminControls);
    }

    protected void submit_button_Click(object sender, EventArgs e)
    {
        SetCookie();
        KBAIST kBaist = new KBAIST();
        DateTime startDateTime = kBaist.MakeSQLDateTime(EventDate.SelectedDate.Year, EventDate.SelectedDate.Month, EventDate.SelectedDate.Day, StartTime.SelectedValue.ToString());
        DateTime endDateTime = kBaist.MakeSQLDateTime(EventDate.SelectedDate.Year, EventDate.SelectedDate.Month, EventDate.SelectedDate.Day, EndTime.SelectedValue.ToString());
        string description = Description.Text.Trim();
        string notes = Notes.Text.Trim();
        bool validated = ValidateInput(startDateTime, endDateTime, description, notes);
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
    }

    protected void EventDate_SelectionChanged(object sender, EventArgs e)
    {
        DateTime selectedDay = EventDate.SelectedDate;
        GetEventsForDay(selectedDay);
    }

    private bool ValidateInput(DateTime start, DateTime end, string description, string notes)
    {
        try
        {
            if(DateTime.Compare(start, end) >= 0)
            {
                messageLabel.Text = "The end time cannot be before, or the same as, the start time.";
                return false;
            }
            if(DateTime.Compare(start, DateTime.Now) <= 0)
            {
                messageLabel.Text = "The start time must be in the future.";
                return false;
            }
            if (string.IsNullOrEmpty(description))
            {
                messageLabel.Text = "Please give the event a name/description.";
                return false;
            }
            if(description.Length > 100)
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
        catch(Exception e)
        {
            messageLabel.Text = "Whoops, something is wrong with one or more of the input fields.";
            messageLabel.Text = "Nice try, Dave";
            return false;
        }
    }

    private void GetEventsForDay(DateTime selectedDay)
    {
        KBAIST kBaist = new KBAIST();
        List<Event> eventsForDay = kBaist.GetEvents(selectedDay, selectedDay.AddDays(1));
        if (eventsForDay.Count > 0)
        {
            foreach (Event ev in eventsForDay)
            {
                Panel p = new Panel();
                p.Attributes.Add("class", "card");
                Label l = new Label();
                l.Text = ev.Description;
                l.Attributes.Add("id", "event" + ev.EventID + "Description");
                p.Controls.Add(l);
                l = new Label();
                l.Text = ev.StartDateTime + " to " + ev.EndDateTime;
                p.Controls.Add(l);
                l = new Label();
                l.Text = "Notes: " + ev.Notes;
                p.Controls.Add(l);

                LinkButton lb = new LinkButton();
                lb.Text = "Delete";
                lb.Attributes.Add("href", "AddEvent.aspx?d=" + ev.EventID);
                p.Controls.Add(lb);
                EventsForDay.Controls.Add(p);
            }
        }
        SetCookie();
    }

    private void RemoveEvent(int id)
    {
        SetCookie();
        KBAIST kb = new KBAIST();
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

    private void SetCookie()
    {
        HttpCookie cookie = new HttpCookie("SelectedDay", EventDate.SelectedDate.ToString());
        Response.Cookies.Add(cookie);
    }
}