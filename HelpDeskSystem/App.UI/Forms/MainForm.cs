using App.Core.Services;
using App.UI.Views;

namespace App.UI.Forms
{
    public partial class MainForm : Form
    {
        private readonly TicketService _tickets = new();
        private readonly AgentService  _agents  = new();

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowDashboard();
        }

        private void btnDashboard_Click(object sender, EventArgs e) => ShowDashboard();
        private void btnTickets_Click(object sender, EventArgs e)   => ShowTickets();
        private void btnAgents_Click(object sender, EventArgs e)    => ShowAgents();

        private void ShowDashboard()
        {
            Activate(btnDashboard);
            LoadView(new DashboardView(_tickets));
        }

        private void ShowTickets()
        {
            Activate(btnTickets);
            LoadView(new TicketsView(_tickets, _agents));
        }

        private void ShowAgents()
        {
            Activate(btnAgents);
            LoadView(new AgentsView(_agents));
        }

        private void LoadView(UserControl view)
        {
            pnlContent.Controls.Clear();
            view.Dock = DockStyle.Fill;
            pnlContent.Controls.Add(view);
        }

        private void Activate(Button btn)
        {
            foreach (Control c in pnlNav.Controls)
                if (c is Button b) { b.BackColor = Color.FromArgb(25, 25, 55); b.ForeColor = Color.Silver; }
            btn.BackColor = Color.FromArgb(65, 125, 175);
            btn.ForeColor = Color.White;
        }
    }
}
