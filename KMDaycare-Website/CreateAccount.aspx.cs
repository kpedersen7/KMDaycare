using System;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;

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
        else
        {
            Response.Redirect("Default.aspx");
        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        KBAIST kb = new KBAIST();
        string childFirstName = String.Format(ChildFirstName.Text.Trim());
        string childLastName = String.Format(ChildLastName.Text.Trim());
        string parent1FirstName = String.Format(Parent1FirstName.Text.Trim());
        string parent1LastName = String.Format(Parent1LastName.Text.Trim());
        string parent2FirstName = String.Format(Parent2FirstName.Text.Trim());
        string parent2LastName = String.Format(Parent2LastName.Text.Trim());
        string homeAddress = String.Format(HomeAddress.Text.Trim());
        string postalCode = String.Format(PostalCode.Text.Trim());
        string contactNumber = String.Format(ContactNumber.Text.Trim());
        string email = String.Format(Email.Text.Trim());
        string password = String.Format(Password.Text.Trim());
        bool validated = ValidateInput(childFirstName, childLastName, parent1FirstName, parent1LastName, parent2FirstName, parent2LastName, homeAddress, postalCode, contactNumber, email, password);
        string userName = kb.MakeUsername(childFirstName, childLastName, parent1FirstName);
        bool success1 = kb.CreateAccount(userName, childFirstName, childLastName, parent1FirstName, parent1LastName, parent2FirstName, parent2LastName, homeAddress, postalCode, contactNumber);
        Cryptography c = new Cryptography();
        string encryptedPassword = c.Encrypt(password);
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
                SmtpClient smtp = new SmtpClient(WebConfigurationManager.AppSettings["mailServer"], int.Parse(WebConfigurationManager.AppSettings["mailPort"]));
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

    private bool ValidateInput(string childFirstName, string childLastName, string parent1FirstName, string parent1LastName, string parent2FirstName, string parent2LastName, string homeAddress, string postalCode, string contactNumber, string email, string password)
    {
        try
        {
            string errorMessage = "";
            if (string.IsNullOrEmpty(childFirstName) || string.IsNullOrEmpty(childLastName) || string.IsNullOrEmpty(parent1FirstName) || string.IsNullOrEmpty(parent1LastName) || string.IsNullOrEmpty(contactNumber) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                errorMessage += "One or more required fields are not filled out. ";
                return false;
            }
            if (childFirstName.Length > 50 || childLastName.Length > 50 || parent1FirstName.Length > 50 || parent1LastName.Length > 50 || childFirstName.Length > 50 || email.Length > 50 || password.Length > 25 || contactNumber.Length > 10)
            {
                errorMessage += "One or more required fields contain too many characters. ";
                return false;
            }
            if(password.Length < 6)
            {
                errorMessage += "Password is too short. ";
                return false;
            }
            if(!Regex.IsMatch(email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
            {
                errorMessage += "Email is invalid. ";
                return false;
            }
            if (errorMessage != "")
            {
                feedback.Text = errorMessage;
            }
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
    }
}