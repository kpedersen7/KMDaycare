using System;
using System.Net;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Security;

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["t"] != null)
        {
            PageControls.Controls.Remove(RequestButton);
            Cryptography c = new Cryptography();
            PasswordChangeTicket ticket = c.GetTicket(Request.QueryString["t"]);
            if(ticket != null)
            {
                if (ticket.TicketID <= 0 || DateTime.Compare(ticket.Expiry, DateTime.Now) < 0)
                {
                    Response.Redirect("Default.aspx");
                }
            }
        }
        else
        {
            PageControls.Controls.Remove(NewPasswordTB);
            PageControls.Controls.Remove(ConfirmNewPasswordTB);
            PageControls.Controls.Remove(NewPasswordLabel);
            PageControls.Controls.Remove(ConfirmNewPasswordLabel);
            PageControls.Controls.Remove(SubmitButton);
        }
    }

    protected void RequestButton_Click(object sender, EventArgs e)
    {
        Cryptography c = new Cryptography();
        string email = EmailTB.Text.Trim();
        KBAIST kb = new KBAIST();
        User u = kb.GetUserByEmail(email);
        if (u.Email == null)
        {
            FeedbackLabel.Text = "Invalid email!";
        }
        else
        {
            string ticket = c.CreateTicket(email);
            if (ticket != "")
            {
                try
                {
                    MailAddress mailSender = new MailAddress(WebConfigurationManager.AppSettings["mailAccount"], "Knottwood Montessori Daycare");
                    MailAddress mailRecipient = new MailAddress(EmailTB.Text.Trim());
                    MailMessage message = new MailMessage(mailSender, mailRecipient);
                    message.IsBodyHtml = true;
                    message.Subject = "Knottwood Montessori Daycare - Password Change Requested";
                    string link = "http://webbaist.nait.ca/Projects/Kyle/ChangePassword.aspx";
                    message.Body = String.Format("<p>A password change has been requested for your account.</p><a href='{0}?t={1}'>Click here to change your password</a></p>", link,ticket);
                    SmtpClient smtp = new SmtpClient(WebConfigurationManager.AppSettings["mailServer"], int.Parse(WebConfigurationManager.AppSettings["mailPort"]));
                    smtp.Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["mailAccount"], WebConfigurationManager.AppSettings["mailPassword"]);
                    smtp.EnableSsl = true;
                    smtp.Send(message);
                    FeedbackLabel.Text = "An email containing a link has been sent to this email address.";
                }
                catch(Exception ex)
                {
                    FeedbackLabel.Text = "There was an error sending an email to this address.";
                }
                
            }
            else
            {
                FeedbackLabel.Text = "There was an error helping you change your password.";
            }
        }
    }

    protected void SubmitButton_Click(object sender, EventArgs e)
    {
        bool validated = ValidateInput();
        if (validated)
        {
            bool submitted = SubmitPasswordUpdate();
            if (submitted)
            {
                FormsAuthentication.SignOut();
                Response.Redirect("Login.aspx");
            }
            else
            {
                FeedbackLabel.Text = "Something went wrong changing your password, please make sure the email is correct, if a day has passed since requesting to change password, please request another";
            }
        }
    }

    private bool ValidateInput()
    {
        bool ok = true;
        string errorMessage = "";
        if (String.Format(EmailTB.Text.Trim()).Length < 6)
        {
            errorMessage += "Password must be at least 6 characters. ";
            ok = false;
        }
        if (String.Format(EmailTB.Text.Trim()) == "")
        {
            errorMessage += "Email textbox is not filled out. ";
            ok = false;
        }
        if (NewPasswordTB.Text.Trim() != ConfirmNewPasswordTB.Text.Trim())
        {
            errorMessage += "Passwords do not match.";
            ok = false;
        }
        if(errorMessage != "")
        {
            FeedbackLabel.Text = errorMessage;
            ok = false;
        }
        return ok;
    }

    private bool SubmitPasswordUpdate()
    {
        Cryptography c = new Cryptography();
        PasswordChangeTicket ticket = c.GetTicket(Request.QueryString["t"]);
        if (DateTime.Compare(ticket.Expiry, DateTime.Now) > 0)
        {
            if (ticket.Email == EmailTB.Text.Trim())
            {
                c.UpdatePassword(EmailTB.Text.Trim(), NewPasswordTB.Text.Trim());
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }

    }
}