using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Configuration;

public partial class Settings : System.Web.UI.Page
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
            SetEmailServerAndPort(email);
            Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
            webConfigApp.AppSettings.Settings["mailAccount"].Value = email;
            webConfigApp.AppSettings.Settings["mailPassword"].Value = password;
            webConfigApp.Save();
            Response.Redirect("Settings.aspx");
        }
        else
        {
            feedbackLabel.Text = "Email and/or password were not filled out.";
        }
    }

    private void SetEmailServerAndPort(string email)
    {
        Configuration webConfigApp = WebConfigurationManager.OpenWebConfiguration("~");
        if (email.Contains("gmail"))
        {
            webConfigApp.AppSettings.Settings["mailServer"].Value = "smtp.gmail.com";
            webConfigApp.AppSettings.Settings["mailPort"].Value = "587";
        }
        if (email.Contains("outlook"))
        {
            webConfigApp.AppSettings.Settings["mailServer"].Value = "smtp-mail.outlook.com";
            webConfigApp.AppSettings.Settings["mailPort"].Value = "587";
        }
        if (email.Contains("yahoo"))
        {
            webConfigApp.AppSettings.Settings["mailServer"].Value = "smtp.mail.yahoo.com";
            webConfigApp.AppSettings.Settings["mailPort"].Value = "465";
        }
        webConfigApp.Save();
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