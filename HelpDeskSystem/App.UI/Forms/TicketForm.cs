using App.Core.Models;
using App.Core.Services;

namespace App.UI.Forms
{
    public partial class TicketForm : Form
    {
        private readonly TicketService _svc;
        private readonly AgentService  _agents;
        private readonly Ticket?       _ticket;
        private readonly bool          _isEdit;

        public TicketForm(TicketService svc, AgentService agents, Ticket? ticket = null)
        {
            _svc    = svc;
            _agents = agents;
            _ticket = ticket;
            _isEdit = ticket != null;
            InitializeComponent();
        }

        private void TicketForm_Load(object sender, EventArgs e)
        {
            cmbCategory.Items.AddRange(Enum.GetNames(typeof(TicketCategory)));
            cmbPriority.Items.AddRange(Enum.GetNames(typeof(TicketPriority)));
            cmbStatus.Items.AddRange(Enum.GetNames(typeof(TicketStatus)));

            cmbAssignedTo.Items.Add("-- Unassigned --");
            foreach (var a in _agents.GetAll()) cmbAssignedTo.Items.Add(a.Name);
            cmbAssignedTo.SelectedIndex = 0;

            cmbCategory.SelectedIndex = 5; // Other
            cmbPriority.SelectedIndex = 1; // Medium
            cmbStatus.SelectedIndex   = 0; // Open

            if (_isEdit && _ticket != null)
            {
                txtId.Text          = _ticket.Id;
                txtTitle.Text       = _ticket.Title;
                txtDesc.Text        = _ticket.Description;
                txtSubmittedBy.Text = _ticket.SubmittedBy;
                cmbCategory.SelectedItem = _ticket.Category.ToString();
                cmbPriority.SelectedItem = _ticket.Priority.ToString();
                cmbStatus.SelectedItem   = _ticket.Status.ToString();

                int idx = cmbAssignedTo.Items.IndexOf(_ticket.AssignedTo);
                cmbAssignedTo.SelectedIndex = idx >= 0 ? idx : 0;

                btnSave.Text = "Update Ticket";
                Text         = "Edit Ticket — " + _ticket.Id;
                lblCreated.Text = "Created: " + _ticket.CreatedAt.ToString("dd MMM yyyy  HH:mm");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTitle.Text))
            { MessageBox.Show("Title is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (string.IsNullOrWhiteSpace(txtSubmittedBy.Text))
            { MessageBox.Show("Submitted By is required.", "Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            try
            {
                var t = _ticket ?? new Ticket();
                t.Title       = txtTitle.Text.Trim();
                t.Description = txtDesc.Text.Trim();
                t.SubmittedBy = txtSubmittedBy.Text.Trim();
                t.AssignedTo  = cmbAssignedTo.SelectedIndex == 0 ? "" : cmbAssignedTo.SelectedItem!.ToString()!;
                t.Category    = Enum.Parse<TicketCategory>(cmbCategory.SelectedItem!.ToString()!);
                t.Priority    = Enum.Parse<TicketPriority>(cmbPriority.SelectedItem!.ToString()!);
                t.Status      = Enum.Parse<TicketStatus>  (cmbStatus.SelectedItem!.ToString()!);

                if (_isEdit) _svc.Update(t);
                else         _svc.Add(t);

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
