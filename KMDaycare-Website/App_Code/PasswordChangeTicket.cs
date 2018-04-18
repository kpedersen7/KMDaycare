using System;

/// <summary>
/// Summary description for PasswordChangeTicket
/// </summary>
public class PasswordChangeTicket
{
    public int TicketID { get; set; }
    public string Email { get; set; }
    public string Ticket { get; set; }
    public DateTime Expiry { get; set; }
}