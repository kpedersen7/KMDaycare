using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Policies : System.Web.UI.Page
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
        else
        {
            Page.MasterPageFile = "~/General.master";
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
}