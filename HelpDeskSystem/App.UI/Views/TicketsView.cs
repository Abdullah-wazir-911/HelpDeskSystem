using App.Core.Models;
using App.Core.Services;
using App.UI.Forms;

namespace App.UI.Views
{
    public partial class TicketsView : UserControl
    {
        private readonly TicketService _svc;
        private readonly AgentService  _agents;
        private List<Ticket> _data = new();

        public TicketsView(TicketService svc, AgentService agents)
        {
            _svc    = svc;
            _agents = agents;
            InitializeComponent();
        }

        private void TicketsView_Load(object sender, EventArgs e)
        {
            cmbCat.Items.Add("All"); foreach (var v in Enum.GetNames(typeof(TicketCategory))) cmbCat.Items.Add(v); cmbCat.SelectedIndex=0;
            cmbSt.Items.Add("All");  foreach (var v in Enum.GetNames(typeof(TicketStatus)))   cmbSt.Items.Add(v);  cmbSt.SelectedIndex=0;
            cmbPri.Items.Add("All"); foreach (var v in Enum.GetNames(typeof(TicketPriority)))  cmbPri.Items.Add(v); cmbPri.SelectedIndex=0;
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                _data = _svc.Search(txtSearch.Text, cmbCat.SelectedItem?.ToString()??"All",
                                    cmbSt.SelectedItem?.ToString()??"All", cmbPri.SelectedItem?.ToString()??"All");
                dgv.DataSource = _data;
                lblCount.Text  = $"Showing {_data.Count} ticket(s)";
                StyleGrid();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void StyleGrid()
        {
            dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible   = false;
            dgv.SelectionMode       = DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly            = true;
            dgv.BackgroundColor     = Color.White;
            dgv.BorderStyle         = BorderStyle.None;
            dgv.GridColor           = Color.FromArgb(220,220,220);
            dgv.DefaultCellStyle.Font = new Font("Segoe UI",9);
            dgv.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(30,30,60);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font=new Font("Segoe UI",9,FontStyle.Bold);
            dgv.EnableHeadersVisualStyles=false;

            foreach (DataGridViewRow row in dgv.Rows)
            {
                if (row.DataBoundItem is Ticket t)
                    row.DefaultCellStyle.BackColor = t.Status switch {
                        TicketStatus.Open       => Color.FromArgb(255,235,235),
                        TicketStatus.InProgress => Color.FromArgb(255,248,215),
                        TicketStatus.Resolved   => Color.FromArgb(225,255,225),
                        TicketStatus.Closed     => Color.FromArgb(240,240,240),
                        _ => Color.White
                    };
            }
        }

        private Ticket? Selected => dgv.CurrentRow?.DataBoundItem as Ticket;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using var f = new TicketForm(_svc, _agents);
            if (f.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Selected == null) { MessageBox.Show("Select a ticket first."); return; }
            using var f = new TicketForm(_svc, _agents, Selected);
            if (f.ShowDialog() == DialogResult.OK) LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Selected == null) { MessageBox.Show("Select a ticket first."); return; }
            if (MessageBox.Show($"Delete ticket '{Selected.Title}'?", "Confirm",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            { _svc.Delete(Selected.Id); LoadData(); }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();
        private void txtSearch_TextChanged(object sender, EventArgs e) => LoadData();
        private void cmbCat_SelectedIndexChanged(object sender, EventArgs e) => LoadData();
        private void cmbSt_SelectedIndexChanged(object sender, EventArgs e)  => LoadData();
        private void cmbPri_SelectedIndexChanged(object sender, EventArgs e) => LoadData();
    }
}
