using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAccount : System.Web.UI.Page
{

    protected void Page_PreInit(object sender, EventArgs e)
    {
        SecurityController s = HttpContext.Current.User as SecurityController;
        if (s != null)
        {
            UserController users = new UserController();
            User u = users.GetUser(HttpContext.Current.User.Identity.Name);
            if (s.IsInRole("Admin"))
            {
                Page.MasterPageFile = "~/Admin.master";
            }
            if (!s.IsInRole("Admin") && !s.IsInRole("Parent"))
            {
                Response.Redirect("Default.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityController s = HttpContext.Current.User as SecurityController;
        if (s != null)
        {

            if (s.IsInRole("Admin"))
            {
                DoAdminStuff();

            }

            if (s.IsInRole("Parent"))
            {
                DoNotAdminStuff();
            }
        }
    }

    private void DoAdminStuff()
    {
        if (Request.QueryString["m"] != null)
        {
            KBAIST kb = new KBAIST();
            Member m = kb.GetMember(Request.QueryString["m"]);
            FillForm(m);
        }
        else
        {
            AccountDetails.Visible = false;
        }
    }

    private void DoNotAdminStuff()
    {
        KBAIST kb = new KBAIST();
        UserController users = new UserController();
        User u = users.GetUser(HttpContext.Current.User.Identity.Name);
        Member m = kb.GetMember(u.UserName);
        FillForm(m);

        ChildNameTextbox.Enabled = false;
        Parent1NameTextbox.Enabled = false;
        Parent2NameTextbox.Enabled = false;
        HomeAddressTextbox.Enabled = false;
        PostalCodeTextbox.Enabled = false;
        ContactNumberTextbox.Enabled = false;
        AccountDetails.Controls.Remove(SubmitUpdateButton);
        Page.Controls.Remove(FoundUsersTable);
    }

    private void FillForm(Member m)
    {
        ChildNameTextbox.Text = String.Format(m.ChildFirstName) + " " + String.Format(m.ChildFirstName);
        Parent1NameTextbox.Text = String.Format(m.Parent1FirstName) + " " + String.Format(m.Parent1LastName);
        Parent2NameTextbox.Text = String.Format(m.Parent2FirstName) + " " + String.Format(m.Parent2LastName);
        HomeAddressTextbox.Text = String.Format(m.HomeAddress);
        PostalCodeTextbox.Text = String.Format(m.PostalCode);
        ContactNumberTextbox.Text = String.Format(m.EmergencyContact);
    }

    protected void SubmitUpdateButton_Click(object sender, EventArgs e)
    {

    }

    protected void SearchMemberButton_Click(object sender, EventArgs e)
    {
        KBAIST kb = new KBAIST();
        List<Member> foundMembers = kb.SearchMembers(MemberSearchTextbox.Text);
        foreach (Member m in foundMembers)
        {
            TableRow row = new TableRow();
            TableCell cell = new TableCell();
            LinkButton lb = new LinkButton();
            lb.Text = m.ChildFirstName + " " + m.ChildLastName;
            lb.Attributes.Add("href", "MyAccount.aspx?m=" + m.UserName);
            cell.Controls.Add(lb);
            row.Cells.Add(cell);
            FoundUsersTable.Rows.Add(row);
        }
    }
}