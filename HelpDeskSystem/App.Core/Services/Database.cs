using Microsoft.Data.SqlClient;

namespace App.Core.Services
{
    public static class Database
    {
        public static string ConnStr { get; private set; } = "";

        public static void Initialize()
        {
            // LocalDB comes with Visual Studio 2019/2022 - no setup needed
            string[] servers = {
                @"(localdb)\MSSQLLocalDB",
                @"(localdb)\ProjectsV13",
                @"(localdb)\v11.0",
                @".\SQLEXPRESS",
                @"localhost\SQLEXPRESS",
                @"localhost",
            };

            string? masterConn = null;
            foreach (var srv in servers)
            {
                try
                {
                    var t = $"Server={srv};Database=master;Integrated Security=True;TrustServerCertificate=True;Connect Timeout=3;";
                    using var c = new SqlConnection(t);
                    c.Open();
                    masterConn = t;
                    break;
                }
                catch { }
            }

            if (masterConn == null)
                throw new InvalidOperationException(
                    "No SQL Server found!\n\nVisual Studio 2022 includes LocalDB.\n" +
                    "Open 'Visual Studio Installer' > Modify > ensure\n'SQL Server Express LocalDB' component is installed.");

            // Create DB
            using (var c = new SqlConnection(masterConn))
            {
                c.Open();
                Exec(c, "IF NOT EXISTS(SELECT name FROM sys.databases WHERE name='HelpDeskDB') CREATE DATABASE HelpDeskDB");
            }

            ConnStr = masterConn
                .Replace("Database=master;", "Database=HelpDeskDB;")
                .Replace("Connect Timeout=3;", "");

            using (var c = new SqlConnection(ConnStr))
            {
                c.Open();
                CreateSchema(c);
                SeedData(c);
            }
        }

        public static SqlConnection Open()
        {
            var c = new SqlConnection(ConnStr);
            c.Open();
            return c;
        }

        private static void Exec(SqlConnection c, string sql)
        {
            using var cmd = new SqlCommand(sql, c) { CommandTimeout = 30 };
            cmd.ExecuteNonQuery();
        }

        private static void CreateSchema(SqlConnection c)
        {
            Exec(c, @"IF NOT EXISTS(SELECT * FROM sys.tables WHERE name='Agent')
CREATE TABLE Agent(
  Id         NVARCHAR(20)  PRIMARY KEY,
  Name       NVARCHAR(100) NOT NULL,
  Email      NVARCHAR(150) NOT NULL DEFAULT '',
  Department NVARCHAR(100) NOT NULL DEFAULT '',
  Phone      NVARCHAR(30)  NOT NULL DEFAULT ''
)");
            Exec(c, @"IF NOT EXISTS(SELECT * FROM sys.tables WHERE name='Ticket')
CREATE TABLE Ticket(
  Id          NVARCHAR(30)  PRIMARY KEY,
  Title       NVARCHAR(200) NOT NULL,
  Description NVARCHAR(MAX) NOT NULL DEFAULT '',
  SubmittedBy NVARCHAR(100) NOT NULL,
  AssignedTo  NVARCHAR(100) NOT NULL DEFAULT '',
  Category    NVARCHAR(50)  NOT NULL DEFAULT 'Other',
  Priority    NVARCHAR(20)  NOT NULL DEFAULT 'Medium',
  Status      NVARCHAR(20)  NOT NULL DEFAULT 'Open',
  CreatedAt   DATETIME      NOT NULL DEFAULT GETDATE(),
  ResolvedAt  DATETIME      NULL
)");
        }

        private static void SeedData(SqlConnection c)
        {
            using var chk = new SqlCommand("SELECT COUNT(*) FROM Agent", c);
            if ((int)chk.ExecuteScalar()! > 0) return;

            Exec(c, @"INSERT INTO Agent(Id,Name,Email,Department,Phone) VALUES
('AGT-001','Ali Hassan','ali@helpdesk.com','IT Support','0300-1234567'),
('AGT-002','Sara Ahmed','sara@helpdesk.com','Network Team','0301-2345678'),
('AGT-003','Usman Tariq','usman@helpdesk.com','Security','0302-3456789'),
('AGT-004','Fatima Malik','fatima@helpdesk.com','Software Support','0303-4567890'),
('AGT-005','Hamza Raza','hamza@helpdesk.com','Hardware Support','0304-5678901')");

            Exec(c, @"INSERT INTO Ticket(Id,Title,Description,SubmittedBy,AssignedTo,Category,Priority,Status,CreatedAt)VALUES
('TKT-001','Laptop not starting','Dell laptop black screen on boot.','Ahmed Khan','Ali Hassan','Hardware','High','Open',DATEADD(day,-10,GETDATE())),
('TKT-002','Cannot connect to VPN','VPN error code 800 on Windows 11.','Bilal Raza','Sara Ahmed','Network','Critical','InProgress',DATEADD(day,-8,GETDATE())),
('TKT-003','Email not receiving','Outlook not downloading new emails.','Zara Sheikh','Fatima Malik','Software','Medium','Open',DATEADD(day,-7,GETDATE())),
('TKT-004','Password reset','Forgot Windows login password.','Omar Farooq','Ali Hassan','Account','Low','Resolved',DATEADD(day,-6,GETDATE())),
('TKT-005','Printer offline','HP printer 3rd floor offline.','Hina Baig','Usman Tariq','Hardware','Medium','Open',DATEADD(day,-5,GETDATE())),
('TKT-006','Virus alert','Trojan detected on finance PC.','Kamran Ali','Usman Tariq','Security','Critical','InProgress',DATEADD(day,-4,GETDATE())),
('TKT-007','Software install','Need Adobe Acrobat installed.','Nadia Omer','Fatima Malik','Software','Low','Resolved',DATEADD(day,-4,GETDATE())),
('TKT-008','Internet slow','Speed 1Mbps in marketing wing.','Tariq Mehmood','Sara Ahmed','Network','High','InProgress',DATEADD(day,-3,GETDATE())),
('TKT-009','Monitor flickering','Second monitor flickering.','Sana Javed','Hamza Raza','Hardware','Medium','Open',DATEADD(day,-3,GETDATE())),
('TKT-010','Account locked','Locked after failed logins.','Faisal Qureshi','Ali Hassan','Account','High','Resolved',DATEADD(day,-2,GETDATE())),
('TKT-011','Teams not opening','Teams crashes after update.','Amna Siddiqui','Fatima Malik','Software','Medium','Open',DATEADD(day,-2,GETDATE())),
('TKT-012','Network switch down','Switch rebooting every 30 mins.','Imran Shah','Sara Ahmed','Network','Critical','InProgress',DATEADD(day,-1,GETDATE())),
('TKT-013','Keyboard broken','USB keyboard not working.','Rabia Noor','Hamza Raza','Hardware','Low','Closed',DATEADD(day,-1,GETDATE())),
('TKT-014','Backup failure','Nightly backup failed 3 nights.','Asad Butt','Usman Tariq','Security','High','Open',GETDATE()),
('TKT-015','New employee setup','Setup laptop and email for joiner.','HR Department','Ali Hassan','Account','Medium','Open',GETDATE())");

            Exec(c, "UPDATE Ticket SET ResolvedAt=DATEADD(day,1,CreatedAt) WHERE Id IN('TKT-004','TKT-007','TKT-010','TKT-013')");
        }
    }
}
