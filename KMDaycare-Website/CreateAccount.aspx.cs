using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateAccount : System.Web.UI.Page
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
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        KBAIST kb = new KBAIST();
        string userName = MakeUsername(ChildFirstName.Text, ChildLastName.Text, Parent1FirstName.Text);
        bool success1 = kb.CreateAccount(ChildFirstName.Text, ChildLastName.Text, Parent1FirstName.Text, Parent1LastName.Text, Parent2FirstName.Text, Parent2LastName.Text, HomeAddress.Text, PostalCode.Text, EmergencyContactNumber.Text);

        Cryptography c = new Cryptography();
        string encryptedPassword = c.Encrypt(Password.Text);
        try
        {
            bool success2 = kb.CreateUser(Email.Text, userName, encryptedPassword, int.Parse(RoleDD.SelectedValue));
            if (success1 && success2)
            {
                MailAddress mailSender = new MailAddress(WebConfigurationManager.AppSettings["mailAccount"], "Knottwood Montessori Daycare");
                MailAddress mailRecipient = new MailAddress(Email.Text);
                MailMessage message = new MailMessage(mailSender, mailRecipient);
                message.IsBodyHtml = true;
                message.Subject = "Account Registered at Knottwood Montessori Daycare";
                message.Body = "<p>Hello! An account as been made for you at Knottwood Montessori Daycare!</p> <p>Username: " + userName + "</p> <p>Password: " + Password.Text + "</p> <p><a href='knottwoodmotessori.com/Login.aspx'>Login to your account here!</a></p>";
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["mailAccount"], WebConfigurationManager.AppSettings["mailPassword"]);
                smtp.EnableSsl = true;
                smtp.Send(message);
                feedback.Text = "Account has been created successfully!";
            }
            else
            {
                feedback.Text = "Something went wrong, please try again.";
            }
        }
        catch(Exception ex)
        {
            feedback.Text = "Something went wrong, please try again.";
        }
    }

    private string MakeUsername(string childfname, string childlname, string parentfname)
    {
        string username = childfname.Substring(0, 1).ToLower() + childlname + parentfname;
        return username;
    }
}