using App.Core.Models;
using Microsoft.Data.SqlClient;

namespace App.Core.Services
{
    public class AgentService
    {
        private static Agent Map(SqlDataReader r) => new Agent
        {
            Id         = r["Id"].ToString()!,
            Name       = r["Name"].ToString()!,
            Email      = r["Email"].ToString()!,
            Department = r["Department"].ToString()!,
            Phone      = r["Phone"].ToString()!
        };

        public List<Agent> GetAll()
        {
            var list = new List<Agent>();
            using var c = Database.Open();
            using var cmd = new SqlCommand("SELECT * FROM Agent ORDER BY Name", c);
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(Map(r));
            return list;
        }

        public List<Agent> Search(string text)
        {
            var list = new List<Agent>();
            using var c = Database.Open();
            using var cmd = new SqlCommand(
                "SELECT * FROM Agent WHERE Name LIKE @t OR Email LIKE @t OR Department LIKE @t ORDER BY Name", c);
            cmd.Parameters.AddWithValue("@t", $"%{text}%");
            using var r = cmd.ExecuteReader();
            while (r.Read()) list.Add(Map(r));
            return list;
        }

        public void Add(Agent a)
        {
            a.Id = "AGT-" + DateTime.Now.ToString("yyMMddHHmmss");
            using var c = Database.Open();
            using var cmd = new SqlCommand(
                "INSERT INTO Agent(Id,Name,Email,Department,Phone)VALUES(@Id,@Na,@Em,@De,@Ph)", c);
            cmd.Parameters.AddWithValue("@Id", a.Id);
            cmd.Parameters.AddWithValue("@Na", a.Name);
            cmd.Parameters.AddWithValue("@Em", a.Email);
            cmd.Parameters.AddWithValue("@De", a.Department);
            cmd.Parameters.AddWithValue("@Ph", a.Phone);
            cmd.ExecuteNonQuery();
        }

        public void Update(Agent a)
        {
            using var c = Database.Open();
            using var cmd = new SqlCommand(
                "UPDATE Agent SET Name=@Na,Email=@Em,Department=@De,Phone=@Ph WHERE Id=@Id", c);
            cmd.Parameters.AddWithValue("@Id", a.Id);
            cmd.Parameters.AddWithValue("@Na", a.Name);
            cmd.Parameters.AddWithValue("@Em", a.Email);
            cmd.Parameters.AddWithValue("@De", a.Department);
            cmd.Parameters.AddWithValue("@Ph", a.Phone);
            cmd.ExecuteNonQuery();
        }

        public void Delete(string id)
        {
            using var c = Database.Open();
            using var cmd = new SqlCommand("DELETE FROM Agent WHERE Id=@Id", c);
            cmd.Parameters.AddWithValue("@Id", id);
            cmd.ExecuteNonQuery();
        }
    }
}
