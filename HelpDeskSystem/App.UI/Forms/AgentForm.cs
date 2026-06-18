using App.Core.Models;
using App.Core.Services;

namespace App.UI.Forms
{
    public partial class AgentForm : Form
    {
        private readonly AgentService _svc;
        private readonly Agent?       _agent;
        private readonly bool         _isEdit;

        public AgentForm(AgentService svc, Agent? agent = null)
        {
            _svc    = svc;
            _agent  = agent;
            _isEdit = agent != null;
            InitializeComponent();
        }

        private void AgentForm_Load(object sender, EventArgs e)
        {
            if (_isEdit && _agent != null)
            {
                txtId.Text   = _agent.Id;
                txtName.Text = _agent.Name;
                txtEmail.Text= _agent.Email;
                txtDept.Text = _agent.Department;
                txtPhone.Text= _agent.Phone;
                btnSave.Text = "Update Agent";
                Text         = "Edit Agent — " + _agent.Id;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            { MessageBox.Show("Name is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                var a = _agent ?? new Agent();
                a.Name       = txtName.Text.Trim();
                a.Email      = txtEmail.Text.Trim();
                a.Department = txtDept.Text.Trim();
                a.Phone      = txtPhone.Text.Trim();

                if (_isEdit) _svc.Update(a);
                else         _svc.Add(a);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e) { DialogResult = DialogResult.Cancel; Close(); }
    }
}
