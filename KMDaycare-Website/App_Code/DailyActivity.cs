using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


public class DailyActivity
{
    public int DailyActivityID { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public string DescriptionofActivity { get; set; }
    public string Notes { get; set; }
    public int ClassID { get; set; }

    public string ClassDescription { get; set; }
}