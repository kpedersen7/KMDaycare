using System;
using System.Collections.Generic;
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
            if (s.IsInRole("Admin"))
            {
                Page.MasterPageFile = "~/Admin.master";
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
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
                    DoParentLoad();
                }
            }
        }
        
    }

    private void DoAdminLoad()
    {
        if (Request.QueryString["m"] != null)
        {
            if(Request.QueryString["m"] == "aProfileAdmin")
            {
                Response.Redirect("MyAccount.aspx");
            }
            KBAIST kb = new KBAIST();
            Member m = kb.GetMember(Request.QueryString["m"]);
            FillForm(m);
        }
        else
        {
            AccountDetails.Visible = false;
        }
    }

    private void DoParentLoad()
    {
        KBAIST kb = new KBAIST();
        UserController users = new UserController();
        User u = users.GetUser(HttpContext.Current.User.Identity.Name);
        if (u.UserName != HttpContext.Current.User.Identity.Name)
        {
            Response.Redirect("MyAccount.aspx?m=" + HttpContext.Current.User.Identity.Name);
        }
        Member m = kb.GetMember(u.UserName);
        FillForm(m);

        ChildNameTextbox.Enabled = false;
        Parent1NameTextbox.Enabled = false;
        Parent2NameTextbox.Enabled = false;
        HomeAddressTextbox.Enabled = false;
        PostalCodeTextbox.Enabled = false;
        ContactNumberTextbox.Enabled = false;
        AccountDetails.Rows.Remove(ButtonRow);
        FoundUsersTable.Rows.Clear();
    }

    private void FillForm(Member m)
    {
        ChildNameTextbox.Text = String.Format(m.ChildFirstName) + " " + String.Format(m.ChildLastName);
        Parent1NameTextbox.Text = String.Format(m.Parent1FirstName) + " " + String.Format(m.Parent1LastName);
        Parent2NameTextbox.Text = String.Format(m.Parent2FirstName) + " " + String.Format(m.Parent2LastName);
        HomeAddressTextbox.Text = String.Format(m.HomeAddress);
        PostalCodeTextbox.Text = String.Format(m.PostalCode);
        ContactNumberTextbox.Text = String.Format(m.EmergencyContact);
    }

    protected void SubmitUpdateButton_Click(object sender, EventArgs e)
    {
        KBAIST kb = new KBAIST();

        string[] childName = ChildNameTextbox.Text.Trim().Split(null);
        string childFirstName = childName[0];
        string childLastName = childName[1];

        string[] parent1Name = Parent1NameTextbox.Text.Trim().Split(null);
        string parent1FirstName = parent1Name[0];
        string parent1LastName = parent1Name[1];

        string[] parent2Name = Parent2NameTextbox.Text.Trim().Split(null);
        string parent2FirstName = parent1Name[0];
        string parent2LastName = parent1Name[1];

        string homeAddress = HomeAddressTextbox.Text.Trim();
        string postalCode = PostalCodeTextbox.Text.Trim();
        string contactNumber = ContactNumberTextbox.Text.Trim();
        bool validated = ValidateInput(childFirstName, childLastName, parent1FirstName, parent1LastName, parent2FirstName, parent2LastName, homeAddress, postalCode, contactNumber);
        if (validated)
        {
            bool b = kb.UpdateAccount(Request.QueryString["m"], childFirstName, childLastName, parent1FirstName, parent1LastName, parent2FirstName, parent2LastName, homeAddress, postalCode, contactNumber);
            if (b)
            {
                FeedbackLabel.Text = "Account updated successfully!";
            }
            else
            {
                FeedbackLabel.Text = "Account was not updated!";
            }
        }
        else
        {
            FeedbackLabel.Text = "One or more fields are not filled out properly.";
        }
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

    private bool ValidateInput(string childFirstName, string childLastName, string parent1FirstName, string parent1LastName, string parent2FirstName, string parent2LastName, string homeAddress, string postalCode, string contactNumber)
    {
        try
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(childFirstName) || string.IsNullOrEmpty(childLastName) || string.IsNullOrEmpty(parent1FirstName) || string.IsNullOrEmpty(parent1LastName) || string.IsNullOrEmpty(contactNumber))
            {
                errorMessage += "One or more required fields are not filled out. ";
                return false;
            }
            if (childFirstName.Length > 50 || childLastName.Length > 50 || parent1FirstName.Length > 50 || parent1LastName.Length > 50 || childFirstName.Length > 50 || contactNumber.Length > 10)
            {
                errorMessage += "One or more required fields contain too many characters. ";
                return false;
            }
            if (errorMessage != "")
            {
                FeedbackLabel.Text = errorMessage;
            }
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}