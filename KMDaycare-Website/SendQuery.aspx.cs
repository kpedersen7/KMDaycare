﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class SendQuery : System.Web.UI.Page
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
            else
            {
                Page.MasterPageFile = "~/General.master";
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void SendMail()
    {
        // Gmail Address from where you send the mail
        var fromAddress = YourEmail.Text;
        // any address where the email will be sending
        var toAddress = WebConfigurationManager.AppSettings["mailAccount"];
        //Password of your gmail address
        string fromPassword = WebConfigurationManager.AppSettings["mailPassword"];
        // Passing the values and make a email formate to display
        string subject = YourSubject.Text.ToString();
        string body = "From: " + YourName.Text + "\n";
        body += "Email: " + YourEmail.Text + "\n";
        body += "Subject: " + YourSubject.Text + "\n";
        body += "Question: \n" + Comments.Text + "\n";
        // smtp settings
        var smtp = new System.Net.Mail.SmtpClient();
        {
            smtp.Host = WebConfigurationManager.AppSettings["mailServer"];
            smtp.Port = int.Parse(WebConfigurationManager.AppSettings["mailPort"]);
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.Credentials = new NetworkCredential(WebConfigurationManager.AppSettings["mailAccount"], WebConfigurationManager.AppSettings["mailPassword"]);
            smtp.Timeout = 20000;
        }
        // Passing values to smtp object
        smtp.Send(fromAddress, toAddress, subject, body);
    }


    protected void btnSend_Click(object sender, EventArgs e)
    {
        try
        {
            //here on button click what will done 
            SendMail();
            DisplayMessage.Text = "Your Comments has been sent";
            DisplayMessage.Visible = true;
            YourSubject.Text = "";
            YourEmail.Text = "";
            YourName.Text = "";
            Comments.Text = "";
        }
        catch (Exception) { }
    }
}