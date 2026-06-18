using App.Core.Models;
using App.Core.Services;

namespace App.UI.Views
{
    public partial class DashboardView : UserControl
    {
        private readonly TicketService _svc;
        public DashboardView(TicketService svc) { _svc = svc; InitializeComponent(); }

        private void DashboardView_Load(object sender, EventArgs e) => Refresh();

        private void btnRefresh_Click(object sender, EventArgs e) => Refresh();

        private new void Refresh()
        {
            try
            {
                var all = _svc.GetAll();
                lblTotalVal.Text    = all.Count.ToString();
                lblOpenVal.Text     = all.Count(t => t.Status == TicketStatus.Open).ToString();
                lblInProgVal.Text   = all.Count(t => t.Status == TicketStatus.InProgress).ToString();
                lblResolvedVal.Text = all.Count(t => t.Status == TicketStatus.Resolved || t.Status == TicketStatus.Closed).ToString();
                lblCriticalVal.Text = all.Count(t => t.Priority == TicketPriority.Critical).ToString();
                lblHighVal.Text     = all.Count(t => t.Priority == TicketPriority.High).ToString();

                dgv.DataSource = all.Take(12).Select(t => new {
                    t.Id, t.Title, t.SubmittedBy, t.AssignedTo,
                    t.Priority, t.Status,
                    Created = t.CreatedAt.ToString("dd/MM/yyyy")
                }).ToList();

                StyleGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading dashboard: " + ex.Message);
            }
        }

        private void StyleGrid()
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible   = false;
            dgv.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly            = true;
            dgv.BackgroundColor     = Color.White;
            dgv.GridColor           = Color.FromArgb(220,220,220);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI", 9);
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(30,30,60);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI",9,FontStyle.Bold);
            dgv.EnableHeadersVisualStyles = false;
            dgv.BorderStyle = BorderStyle.None;
        }
    }
}
