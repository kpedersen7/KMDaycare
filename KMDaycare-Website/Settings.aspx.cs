﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Settings : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/image1.jpg"));
        dir.Refresh();
        dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/image2.jpg"));
        dir.Refresh();
        dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/image3.jpg"));
        dir.Refresh();
        Image1.ImageUrl = "HomeGallery/image1.jpg";
        Image2.ImageUrl = "HomeGallery/image2.jpg";
        Image3.ImageUrl = "HomeGallery/image3.jpg";

        SiteEmailAddress.Text = WebConfigurationManager.AppSettings["mailAccount"];
        SiteEmailPassword.Text = WebConfigurationManager.AppSettings["mailPassword"];
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
            }
        }
        else
        {
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
                feedbackLabel.Text = "Something went wrong";
            }
        }
        else
        {

        }
    }

    protected void EmailAddress_Save(object sender, EventArgs e)
    {
        string email;
        string password;
        KBAIST kb = new KBAIST();
        if (SiteEmailAddress.Text != String.Empty && SiteEmailPassword.Text != String.Empty)
        {
            email = SiteEmailAddress.Text;
            password = SiteEmailPassword.Text;
            //bool success = kb.UpdateEmailSettings(email, password);
        }
        else
        {
            feedbackLabel.Text = "Email and/or password were not filled out.";
        }
    }
}