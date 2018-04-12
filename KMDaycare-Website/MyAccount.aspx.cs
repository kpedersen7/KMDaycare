using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAccount : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityController s = HttpContext.Current.User as SecurityController;
        if (s != null)
        {
            KBAIST kb = new KBAIST();
            UserController users = new UserController();
            User u = users.GetUser(HttpContext.Current.User.Identity.Name);
            if (!s.IsInRole("Admin") && !s.IsInRole("Parent"))
            {
                Response.Redirect("Default.aspx");
            }
            Member m = kb.GetMember(u.UserName);
            ChildName.Text = String.Format(m.ChildFirstName) + " " + String.Format(m.ChildFirstName);
            Parent1Name.Text = String.Format(m.Parent1FirstName) + " " + String.Format(m.Parent1LastName);
            Parent2Name.Text = String.Format(m.Parent2FirstName) + " " + String.Format(m.Parent2LastName);
            HomeAddress.Text = String.Format(m.HomeAddress);
            PostalCode.Text = String.Format(m.PostalCode);
            ContactNumber.Text = String.Format(m.EmergencyContact);
        }
    }
}