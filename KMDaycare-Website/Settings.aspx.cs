using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;

public partial class Settings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SecurityController s = HttpContext.Current.User as SecurityController;
        if (s != null)
        {
            if (!s.IsInRole("Admin"))
            {
                Response.Redirect("Default.aspx");
            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }

        DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/image1.jpg"));
        dir.Refresh();
        dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/image2.jpg"));
        dir.Refresh();
        dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/image3.jpg"));
        dir.Refresh();
        Image1.ImageUrl = "HomeGallery/image1.jpg";
        Image2.ImageUrl = "HomeGallery/image2.jpg";
        Image3.ImageUrl = "HomeGallery/image3.jpg";

        CurrentEmailLabel.Text = WebConfigurationManager.AppSettings["mailAccount"];
        CurrentAlbumURL.HRef = WebConfigurationManager.AppSettings["albumURL"];
    }

    protected void HomeGallery_Save(object sender, EventArgs e)
    {
        if (fileupload1.HasFile)
        {
            try
            {
                fileupload1.SaveAs(Server.MapPath("~/HomeGallery/image1.jpg"));
                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/image1.jpg"));
                dir.Refresh();
                feedbackLabel.Text += "Image 1 uploaded. ";
            }
            catch (Exception ex)
            {
                feedbackLabel.Text += "Image 1 could not be uploaded. ";
            }
        }
        if (fileupload2.HasFile)
        {
            try
            {
                fileupload2.SaveAs(Server.MapPath("~/HomeGallery/image2.jpg"));
                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/image2.jpg"));
                dir.Refresh();
                feedbackLabel.Text += "Image 2 uploaded. ";
            }
            catch (Exception ex)
            {
                feedbackLabel.Text += "Image 2 could not be uploaded. ";
            }
        }
        if (fileupload3.HasFile)
        {
            try
            {
                fileupload3.SaveAs(Server.MapPath("~/HomeGallery/image3.jpg"));
                DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/image3.jpg"));
                dir.Refresh();
                feedbackLabel.Text += "Image 3 uploaded. ";
            }
            catch (Exception ex)
            {
                feedbackLabel.Text += "Image 3 could not be uploaded. ";
            }
        }
    }

    protected void Newsletter_Save(object sender, EventArgs e)
    {
        if (fileupload.HasFile)
        {
            try
            {
                if (fileupload.PostedFile.ContentType != "application/pdf")
                {
                    feedbackLabel.Text = "File must be PDF";
                }
                else
                {
                    //save file
                    fileupload.SaveAs(Server.MapPath("~/Files/newsletter.pdf"));
                    DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/Files/"));
                    dir.Refresh();
                    feedbackLabel.Text = "Newsletter updated.";
                }
            }
            catch (Exception ex)
            {
                feedbackLabel.Text = "Something went wrong, make sure there is a PDF in the in the file upload.";
            }
        }
        else
        {
            feedbackLabel.Text = "Make sure there is a PDF in the in the file upload.";
        }
    }

    protected void EmailAddress_Save(object sender, EventArgs e)
    {
        string email;
        string password;
        if (!string.IsNullOrEmpty(SiteEmailAddress.Text.Trim()) && !string.IsNullOrEmpty(SiteEmailPassword.Text.Trim()))
        {
            email = SiteEmailAddress.Text.Trim();
            password = SiteEmailPassword.Text.Trim();
            bool success = SignInToEmailAddress(email, password);
            if (success)
            {
                feedbackLabel.Text = "Success! A confirmation email was sent to the email address entered.";
            }
            else
            {
                feedbackLabel.Text = "There was a problem setting this email, make sure the email address and password are correct.  You will not be able to send emails.";
            }

        }
        else
        {
            feedbackLabel.Text = "Email and/or password were not filled out.";
        }
    }

    private bool SignInToEmailAddress(string email, string password)
    {
        int port = 0;
        string server = "";
        if (email.Contains("gmail"))
        {
            server = "smtp.gmail.com";
            port = 587;
        }
        if (email.Contains("outlook"))
        {
            server = "smtp-mail.outlook.com";
            port = 587;
        }
        if (email.Contains("yahoo"))
        {
            server = "smtp.mail.yahoo.com";
            port = 465;
        }
        try
        { 
            //send email to the new credentials
            MailAddress mailSender = new MailAddress(email, "Knottwood Montessori Daycare");
            MailAddress mailRecipient = new MailAddress(email);
            MailMessage message = new MailMessage(mailSender, mailRecipient);
            message.IsBodyHtml = true;
            message.Subject = "New email registered as Knottwood Montessori Daycare Sender Email";
            message.Body = "<p>This email was successfully used to sign in as the sender email for Knottwood Montessori Daycare</p><p>(780) 461-3320</p>";
            SmtpClient smtp = new SmtpClient(server, port);
            smtp.Credentials = new NetworkCredential(email, password);
            smtp.EnableSsl = true;
            smtp.Send(message);

            //save configurations to config file
            Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            webConfigApp.AppSettings.Settings["mailAccount"].Value = email;
            webConfigApp.AppSettings.Settings["mailPassword"].Value = password;
            webConfigApp.AppSettings.Settings["mailServer"].Value = server;
            webConfigApp.AppSettings.Settings["mailPort"].Value = port.ToString();
            webConfigApp.Save();
            return true;
        }
        catch(Exception ex)
        {
            return false;
        }
        
    }

    protected void AlbumURL_Save(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(NewAlbumURL.Text.Trim()))
        {
            string newURL = NewAlbumURL.Text.Trim();
            Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            webConfigApp.AppSettings.Settings["albumURL"].Value = newURL;
            webConfigApp.Save();
            Response.Redirect("Settings.aspx");
        }
        else
        {
            feedbackLabel.Text = "URL field has not been filled out properly";
        }
    }
}