namespace App.Core.Models
{
    public enum TicketStatus   { Open, InProgress, Resolved, Closed }
    public enum TicketPriority { Low, Medium, High, Critical }
    public enum TicketCategory { Hardware, Software, Network, Security, Account, Other }

    public class Ticket
    {
        public string         Id          { get; set; } = "";
        public string         Title       { get; set; } = "";
        public string         Description { get; set; } = "";
        public string         SubmittedBy { get; set; } = "";
        public string         AssignedTo  { get; set; } = "";
        public TicketCategory Category    { get; set; } = TicketCategory.Other;
        public TicketPriority Priority    { get; set; } = TicketPriority.Medium;
        public TicketStatus   Status      { get; set; } = TicketStatus.Open;
        public DateTime       CreatedAt   { get; set; } = DateTime.Now;
        public DateTime?      ResolvedAt  { get; set; }
    }

    public class Agent
    {
        public string Id         { get; set; } = "";
        public string Name       { get; set; } = "";
        public string Email      { get; set; } = "";
        public string Department { get; set; } = "";
        public string Phone      { get; set; } = "";
    }
}
