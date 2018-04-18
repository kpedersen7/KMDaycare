using System;
using System.Security.Cryptography;
using System.Configuration;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for Cryptography
/// </summary>
public class Cryptography
{
    public string Encrypt(string clearText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(clearBytes, 0, clearBytes.Length);
                    cs.Close();
                }
                clearText = Convert.ToBase64String(ms.ToArray());
            }
        }
        return clearText;
    }

    public string Decrypt(string cipherText)
    {
        string EncryptionKey = "MAKV2SPBNI99212";
        byte[] cipherBytes = Convert.FromBase64String(cipherText);
        using (Aes encryptor = Aes.Create())
        {
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
            encryptor.Key = pdb.GetBytes(32);
            encryptor.IV = pdb.GetBytes(16);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(cipherBytes, 0, cipherBytes.Length);
                    cs.Close();
                }
                cipherText = Encoding.Unicode.GetString(ms.ToArray());
            }
        }
        return cipherText;
    }

    private static string GetUniqueKey(int maxSize)
    {
        char[] chars = new char[62];
        chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        byte[] data = new byte[1];
        using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
        {
            crypto.GetNonZeroBytes(data);
            data = new byte[maxSize];
            crypto.GetNonZeroBytes(data);
        }
        StringBuilder result = new StringBuilder(maxSize);
        foreach (byte b in data)
        {
            result.Append(chars[b % (chars.Length)]);
        }
        return result.ToString();
    }

    public string CreateTicket(string email)
    {
        try
        {
            string ticket = GetUniqueKey(10);
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("CreateTicket", con))
                {
                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@ticket", ticket);
                        con.Open();

                        SqlDataReader sqldr = cmd.ExecuteReader();
                        con.Close();
                    }
                    catch (Exception e)
                    {
                        ticket = "";
                    }

                }
            }
            return ticket;
        }
        catch (Exception e)
        {
            return "";
        }
    }

    public PasswordChangeTicket GetTicket(string ticketString)
    {
        PasswordChangeTicket ticket = new PasswordChangeTicket();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("GetTicket", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ticket", ticketString);
                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ticket.TicketID = int.Parse(dr["TicketID"].ToString());
                    ticket.Email = dr["Email"].ToString();
                    ticket.Ticket = dr["Ticket"].ToString();
                    ticket.Expiry = DateTime.Parse(dr["Expiry"].ToString());
                }
                con.Close();
            }
        }
        return ticket;
    }

    public bool UpdatePassword(string email, string password)
    {
        Cryptography c = new Cryptography();
        string encryptedPassword = c.Encrypt(password);
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["KMDaycare"].ConnectionString))
        {
            using (SqlCommand cmd = new SqlCommand("UpdatePassword", con))
            {
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@password", encryptedPassword);
                    con.Open();

                    SqlDataReader sqldr = cmd.ExecuteReader();
                    con.Close();
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }

            }
        }
    }
}