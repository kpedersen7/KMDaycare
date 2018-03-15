using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CreateNewsletter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void btnsave_Click(object sender, EventArgs e)
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
}