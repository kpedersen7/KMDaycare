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

    public Member GetMember(string username)
    {
        Member m = new Member();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetMember", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserName", username);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    m.UserName = dr["UserName"].ToString();
                    m.ChildFirstName = dr["ChildFirstName"].ToString();
                    m.ChildLastName = dr["ChildLastName"].ToString();
                    m.Parent1FirstName = dr["Parent1FirstName"].ToString();
                    m.Parent1LastName = dr["Parent1LastName"].ToString();
                    m.Parent2FirstName = dr["Parent2FirstName"].ToString();
                    m.Parent2LastName = dr["Parent2LastName"].ToString();
                    m.PostalCode = dr["PostalCode"].ToString();
                    m.HomeAddress = dr["HomeAddress"].ToString();
                    m.EmergencyContact = dr["EmergencyContact"].ToString();
                }
                con.Close();
            }
        }
        return m;
    }
}