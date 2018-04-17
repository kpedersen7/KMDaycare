﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ChangePassword : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["t"] != null)
        {
            PageControls.Controls.Remove(RequestButton);
            Cryptography c = new Cryptography();
            PasswordChangeTicket ticket = c.GetTicket(Request.QueryString["t"]);
            if (ticket.TicketID <= 0 || DateTime.Compare(ticket.Expiry, DateTime.Now) < 0)
            {
                Response.Redirect("Default.aspx");
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
        //validate email and send email to the email
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
                MailAddress mailSender = new MailAddress(WebConfigurationManager.AppSettings["mailAccount"], "Knottwood Montessori Daycare");
                MailAddress mailRecipient = new MailAddress(EmailTB.Text.Trim());
                MailMessage message = new MailMessage(mailSender, mailRecipient);
                message.IsBodyHtml = true;
                message.Subject = "Knottwood Montessori Daycare - Password Change Requested";
                //NEED TO MAKE PROPER LINK
                message.Body = String.Format("<p>A password change has been requested for your account.</p><a href='ChangePassword.aspx?t={0}'>Click here to change your password</a></p>", ticket);
                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["mailAccount"], WebConfigurationManager.AppSettings["mailPassword"]);
                smtp.EnableSsl = true;
                smtp.Send(message);
                FeedbackLabel.Text = "An email has been sent to the address you entered!";
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
        if (String.Format(EmailTB.Text.Trim()) == "")
        {
            errorMessage += "Email textbox is not filled out! ";
            ok = false;
        }
        if (NewPasswordTB.Text.Trim() != ConfirmNewPasswordTB.Text.Trim())
        {
            errorMessage += "Passwords do not match!";
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