using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for MemberController
/// </summary>
public class MemberController
{
    public bool CreateMember(Member memberForAdd)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("CreateMember", con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@userName", memberForAdd.UserName);
                    cmd.Parameters.AddWithValue("@childFirstName", memberForAdd.ChildFirstName);
                    cmd.Parameters.AddWithValue("@childLastName", memberForAdd.ChildLastName);
                    cmd.Parameters.AddWithValue("@parent1FirstName", memberForAdd.Parent1FirstName);
                    cmd.Parameters.AddWithValue("@parent1LastName", memberForAdd.Parent1LastName);
                    cmd.Parameters.AddWithValue("@parent2FirstName", memberForAdd.Parent2FirstName);
                    cmd.Parameters.AddWithValue("@parent2LastName", memberForAdd.Parent2LastName);
                    cmd.Parameters.AddWithValue("@homeAddress", memberForAdd.HomeAddress);
                    cmd.Parameters.AddWithValue("@postalCode", memberForAdd.PostalCode);
                    cmd.Parameters.AddWithValue("@emergencyContact", memberForAdd.EmergencyContact);
                    con.Open();

                    SqlDataReader sqldr = cmd.ExecuteReader();
                    con.Close();
                    return true;
                }
                catch
                {
                    return false;
                }

            }
        }
    }
}