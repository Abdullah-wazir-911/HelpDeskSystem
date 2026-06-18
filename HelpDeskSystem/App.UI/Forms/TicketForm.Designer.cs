namespace App.UI.Forms
{
    partial class TicketForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            txtId = new TextBox(); txtTitle = new TextBox(); txtDesc = new RichTextBox();
            txtSubmittedBy = new TextBox(); cmbCategory = new ComboBox();
            cmbPriority = new ComboBox(); cmbStatus = new ComboBox();
            cmbAssignedTo = new ComboBox(); btnSave = new Button();
            btnCancel = new Button(); lblCreated = new Label();

            int lx = 20, vx = 145, vw = 310, y = 18, gap = 36;

            Label L(string t) { var l = new Label { Text=t, Location=new Point(lx,y+3), AutoSize=true, Font=new Font("Segoe UI",9) }; Controls.Add(l); return l; }
            void Row(Control c, string label, int h=26) { L(label); c.Location=new Point(vx,y); c.Width=vw; c.Height=h; c.Font=new Font("Segoe UI",9); Controls.Add(c); y+=gap; }

            txtId.ReadOnly=true; txtId.BackColor=Color.FromArgb(240,240,240); txtId.Text="(auto)";
            Row(txtId,"Ticket ID:");
            Row(txtTitle,"Title: *");
            txtDesc.Height=65; Row(txtDesc,"Description:",65); y+=30;
            Row(txtSubmittedBy,"Submitted By: *");

            cmbCategory.DropDownStyle=ComboBoxStyle.DropDownList; Row(cmbCategory,"Category:");
            cmbPriority.DropDownStyle=ComboBoxStyle.DropDownList;  Row(cmbPriority,"Priority:");
            cmbStatus.DropDownStyle=ComboBoxStyle.DropDownList;    Row(cmbStatus,"Status:");
            cmbAssignedTo.DropDownStyle=ComboBoxStyle.DropDownList; Row(cmbAssignedTo,"Assign To:");

            lblCreated.Location=new Point(lx,y); lblCreated.AutoSize=true; lblCreated.Font=new Font("Segoe UI",8); lblCreated.ForeColor=Color.Gray; Controls.Add(lblCreated); y+=22;

            btnSave.Text="Add Ticket"; btnSave.Size=new Size(135,34); btnSave.Location=new Point(vx,y+6);
            btnSave.BackColor=Color.FromArgb(65,125,175); btnSave.ForeColor=Color.White;
            btnSave.FlatStyle=FlatStyle.Flat; btnSave.Font=new Font("Segoe UI",9,FontStyle.Bold);
            btnSave.Click+=btnSave_Click; Controls.Add(btnSave);

            btnCancel.Text="Cancel"; btnCancel.Size=new Size(100,34); btnCancel.Location=new Point(vx+145,y+6);
            btnCancel.BackColor=Color.FromArgb(190,60,60); btnCancel.ForeColor=Color.White;
            btnCancel.FlatStyle=FlatStyle.Flat; btnCancel.Font=new Font("Segoe UI",9);
            btnCancel.Click+=btnCancel_Click; Controls.Add(btnCancel);

            Text=_isEdit?"Edit Ticket":"Add New Ticket";
            Size=new Size(500, y+100); StartPosition=FormStartPosition.CenterParent;
            FormBorderStyle=FormBorderStyle.FixedDialog; MaximizeBox=false; BackColor=Color.White;
            Load+=TicketForm_Load;
        }

        private TextBox txtId, txtTitle, txtSubmittedBy;
        private RichTextBox txtDesc;
        private ComboBox cmbCategory, cmbPriority, cmbStatus, cmbAssignedTo;
        private Button btnSave, btnCancel;
        private Label lblCreated;
    }
}
