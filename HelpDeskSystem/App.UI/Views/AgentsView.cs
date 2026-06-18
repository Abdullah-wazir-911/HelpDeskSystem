using App.Core.Models;
using App.Core.Services;
using App.UI.Forms;

namespace App.UI.Views
{
    public partial class AgentsView : UserControl
    {
        private readonly AgentService _svc;
        public AgentsView(AgentService svc) { _svc = svc; InitializeComponent(); }

        private void AgentsView_Load(object sender, EventArgs e) => LoadData();

        private void LoadData()
        {
            try
            {
                var data = string.IsNullOrWhiteSpace(txtSearch.Text)
                    ? _svc.GetAll() : _svc.Search(txtSearch.Text);
                dgv.DataSource = data;
                lblCount.Text  = $"Total {data.Count} agent(s)";
                StyleGrid();
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        private void StyleGrid()
        {
            dgv.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.Fill;
            dgv.RowHeadersVisible=false; dgv.SelectionMode=DataGridViewSelectionMode.FullRowSelect;
            dgv.ReadOnly=true; dgv.BackgroundColor=Color.White; dgv.BorderStyle=BorderStyle.None;
            dgv.GridColor=Color.FromArgb(220,220,220);
            dgv.DefaultCellStyle.Font=new Font("Segoe UI",9);
            dgv.ColumnHeadersDefaultCellStyle.BackColor=Color.FromArgb(30,30,60);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor=Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font=new Font("Segoe UI",9,FontStyle.Bold);
            dgv.EnableHeadersVisualStyles=false;
        }

        private Agent? Selected => dgv.CurrentRow?.DataBoundItem as Agent;

        private void btnAdd_Click(object sender, EventArgs e)
        { using var f=new AgentForm(_svc); if(f.ShowDialog()==DialogResult.OK) LoadData(); }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (Selected==null){MessageBox.Show("Select an agent first.");return;}
            using var f=new AgentForm(_svc,Selected);
            if(f.ShowDialog()==DialogResult.OK) LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Selected==null){MessageBox.Show("Select an agent first.");return;}
            if(MessageBox.Show($"Delete agent '{Selected.Name}'?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            { _svc.Delete(Selected.Id); LoadData(); }
        }

        private void btnRefresh_Click(object sender, EventArgs e) => LoadData();
        private void txtSearch_TextChanged(object sender, EventArgs e) => LoadData();
    }
}
