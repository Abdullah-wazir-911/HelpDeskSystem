namespace App.UI.Forms
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            pnlHeader  = new Panel();
            pnlNav     = new Panel();
            pnlContent = new Panel();
            lblAppName = new Label();
            lblSub     = new Label();
            btnDashboard = new Button();
            btnTickets   = new Button();
            btnAgents    = new Button();

            // Header
            pnlHeader.BackColor = Color.FromArgb(20, 20, 50);
            pnlHeader.Dock      = DockStyle.Top;
            pnlHeader.Height    = 58;

            lblAppName.Text      = "  🎫  Help Desk Ticket System";
            lblAppName.Font      = new Font("Segoe UI", 14, FontStyle.Bold);
            lblAppName.ForeColor = Color.White;
            lblAppName.AutoSize  = true;
            lblAppName.Location  = new Point(8, 8);

            lblSub.Text      = "  Support Management Portal";
            lblSub.Font      = new Font("Segoe UI", 8);
            lblSub.ForeColor = Color.LightSteelBlue;
            lblSub.AutoSize  = true;
            lblSub.Location  = new Point(8, 36);

            pnlHeader.Controls.Add(lblAppName);
            pnlHeader.Controls.Add(lblSub);

            // Nav
            pnlNav.BackColor = Color.FromArgb(25, 25, 55);
            pnlNav.Dock      = DockStyle.Left;
            pnlNav.Width     = 155;

            void NavBtn(Button b, string text, int top)
            {
                b.Text      = text;
                b.Font      = new Font("Segoe UI", 10);
                b.ForeColor = Color.Silver;
                b.BackColor = Color.FromArgb(25, 25, 55);
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderSize = 0;
                b.TextAlign = ContentAlignment.MiddleLeft;
                b.Dock      = DockStyle.Top;
                b.Height    = 48;
                b.Cursor    = Cursors.Hand;
                b.Padding   = new Padding(10, 0, 0, 0);
            }

            NavBtn(btnAgents,    "  👤  Agents",    144);
            NavBtn(btnTickets,   "  🎫  Tickets",    96);
            NavBtn(btnDashboard, "  📊  Dashboard",  48);

            btnDashboard.Click += btnDashboard_Click;
            btnTickets.Click   += btnTickets_Click;
            btnAgents.Click    += btnAgents_Click;

            pnlNav.Controls.Add(btnAgents);
            pnlNav.Controls.Add(btnTickets);
            pnlNav.Controls.Add(btnDashboard);

            // Content
            pnlContent.BackColor = Color.FromArgb(245, 246, 250);
            pnlContent.Dock      = DockStyle.Fill;

            // Form
            Text            = "Help Desk Ticket System";
            Size            = new Size(1100, 680);
            MinimumSize     = new Size(900, 550);
            StartPosition   = FormStartPosition.CenterScreen;
            BackColor       = Color.FromArgb(245, 246, 250);
            Load           += MainForm_Load;

            Controls.Add(pnlContent);
            Controls.Add(pnlNav);
            Controls.Add(pnlHeader);
        }

        private Panel  pnlHeader, pnlNav, pnlContent;
        private Label  lblAppName, lblSub;
        private Button btnDashboard, btnTickets, btnAgents;
    }
}
