using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UpdateHomeGallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnsave_Click(object sender, EventArgs e)
    {
        StringBuilder sb = new StringBuilder();

        if (fileupload1.HasFile)
        {
            try
            {
                //saving the file
                fileupload1.SaveAs(Server.MapPath("~/HomeGallery/image1.jpg"));

            }
            catch (Exception ex)
            {
                sb.Append("<br/> Error <br/>");
                sb.AppendFormat("Unable to save file <br/> {0}", ex.Message);
            }
        }
        if (fileupload2.HasFile)
        {
            try
            {
                //saving the file
                fileupload2.SaveAs(Server.MapPath("~/HomeGallery/image2.jpg"));

            }
            catch (Exception ex)
            {
                sb.Append("<br/> Error <br/>");
                sb.AppendFormat("Unable to save file <br/> {0}", ex.Message);
            }
        }
        if (fileupload3.HasFile)
        {
            try
            {
                //saving the file
                fileupload3.SaveAs(Server.MapPath("~/HomeGallery/image3.jpg"));

            }
            catch (Exception ex)
            {
                sb.Append("<br/> Error <br/>");
                sb.AppendFormat("Unable to save file <br/> {0}", ex.Message);
            }
        }
        else
        {
            feedbackLabel.Text = sb.ToString();
        }

        DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/"));
        dir.Refresh();
    }
}