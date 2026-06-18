using App.Core.Models;
using Microsoft.Data.SqlClient;

namespace App.Core.Services
{
    public class TicketService
    {
        private static Ticket Map(SqlDataReader r) => new Ticket
        {
            Id          = r["Id"].ToString()!,
            Title       = r["Title"].ToString()!,
            Description = r["Description"].ToString()!,
            SubmittedBy = r["SubmittedBy"].ToString()!,
            AssignedTo  = r["AssignedTo"].ToString()!,
            Category    = Enum.TryParse(r["Category"].ToString(), out TicketCategory cat) ? cat : TicketCategory.Other,
            Priority    = Enum.TryParse(r["Priority"].ToString(), out TicketPriority pri) ? pri : TicketPriority.Medium,
            Status      = Enum.TryParse(r["Status"].ToString(),   out TicketStatus   st)  ? st  : TicketStatus.Open,
            CreatedAt   = Convert.ToDateTime(r["CreatedAt"]),
            ResolvedAt  = r["ResolvedAt"] == DBNull.Value ? null : Convert.ToDateTime(r["ResolvedAt"])
        };

        public List<Ticket> GetAll()
        {
            var list = new List<Ticket>();
            using var c = Database.Open();
            using var cmd = new SqlCommand("SELECT * FROM Ticket ORDER BY CreatedAt DESC", c);
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(Map(r));
            return list;
        }

        public List<Ticket> Search(string text, string cat, string st, string pri)
        {
            var list = new List<Ticket>();
            using var c = Database.Open();
            string sql = "SELECT * FROM Ticket WHERE (Title LIKE @t OR SubmittedBy LIKE @t OR AssignedTo LIKE @t)";
            if (cat != "All") sql += " AND Category=@cat";
            if (st  != "All") sql += " AND Status=@st";
            if (pri != "All") sql += " AND Priority=@pri";
            sql += " ORDER BY CreatedAt DESC";

            using var cmd = new SqlCommand(sql, c);
            cmd.Parameters.AddWithValue("@t", $"%{text}%");
            if (cat != "All") cmd.Parameters.AddWithValue("@cat", cat);
            if (st  != "All") cmd.Parameters.AddWithValue("@st",  st);
            if (pri != "All") cmd.Parameters.AddWithValue("@pri", pri);
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(Map(r));
            return list;
        }

        public void Add(Ticket t)
        {
            t.Id        = "TKT-" + DateTime.Now.ToString("yyMMddHHmmss");
            t.CreatedAt = DateTime.Now;
            using var c = Database.Open();
            using var cmd = new SqlCommand(@"INSERT INTO Ticket
(Id,Title,Description,SubmittedBy,AssignedTo,Category,Priority,Status,CreatedAt,ResolvedAt)
VALUES(@Id,@Ti,@De,@Su,@As,@Ca,@Pr,@St,@Cr,@Re)", c);
            cmd.Parameters.AddWithValue("@Id", t.Id);
            cmd.Parameters.AddWithValue("@Ti", t.Title);
            cmd.Parameters.AddWithValue("@De", t.Description);
            cmd.Parameters.AddWithValue("@Su", t.SubmittedBy);
            cmd.Parameters.AddWithValue("@As", t.AssignedTo);
            cmd.Parameters.AddWithValue("@Ca", t.Category.ToString());
            cmd.Parameters.AddWithValue("@Pr", t.Priority.ToString());
            cmd.Parameters.AddWithValue("@St", t.Status.ToString());
            cmd.Parameters.AddWithValue("@Cr", t.CreatedAt);
            cmd.Parameters.AddWithValue("@Re", (object?)t.ResolvedAt ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Update(Ticket t)
        {
            if ((t.Status == TicketStatus.Resolved || t.Status == TicketStatus.Closed) && t.ResolvedAt == null)
                t.ResolvedAt = DateTime.Now;
            using var c = Database.Open();
            using var cmd = new SqlCommand(@"UPDATE Ticket SET
Title=@Ti,Description=@De,SubmittedBy=@Su,AssignedTo=@As,
Category=@Ca,Priority=@Pr,Status=@St,ResolvedAt=@Re WHERE Id=@Id", c);
            cmd.Parameters.AddWithValue("@Id", t.Id);
            cmd.Parameters.AddWithValue("@Ti", t.Title);
            cmd.Parameters.AddWithValue("@De", t.Description);
            cmd.Parameters.AddWithValue("@Su", t.SubmittedBy);
            cmd.Parameters.AddWithValue("@As", t.AssignedTo);
            cmd.Parameters.AddWithValue("@Ca", t.Category.ToString());
            cmd.Parameters.AddWithValue("@Pr", t.Priority.ToString());
            cmd.Parameters.AddWithValue("@St", t.Status.ToString());
            cmd.Parameters.AddWithValue("@Re", (object?)t.ResolvedAt ?? DBNull.Value);
            cmd.ExecuteNonQuery();
        }

        public void Delete(string id)
        {
            using var c = Database.Open();
            using var cmd = new SqlCommand("DELETE FROM Ticket WHERE Id=@Id", c);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
