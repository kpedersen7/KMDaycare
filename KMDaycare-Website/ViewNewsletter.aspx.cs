using System;
using System.IO;

public partial class ViewNewsletter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        DirectoryInfo dir = new DirectoryInfo(Server.MapPath("~/HomeGallery/"));
        dir.Refresh();
        Response.Clear();
        string filename = "newsletter";
        Response.ContentType = "application/PDF";
        Response.AddHeader("content-disposition", "inline;filename=" + filename + ".pdf");
        Response.TransmitFile(Server.MapPath("~/Files/newsletter.pdf"));
        Response.End();
    }
}