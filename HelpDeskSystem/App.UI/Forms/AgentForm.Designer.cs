namespace App.UI.Forms
{
    partial class AgentForm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            txtId=new TextBox(); txtName=new TextBox(); txtEmail=new TextBox();
            txtDept=new TextBox(); txtPhone=new TextBox();
            btnSave=new Button(); btnCancel=new Button();

            int lx=20, vx=130, vw=260, y=18, gap=36;
            Label L(string t) { var l=new Label{Text=t,Location=new Point(lx,y+3),AutoSize=true,Font=new Font("Segoe UI",9)}; Controls.Add(l); return l; }
            void Row(TextBox c, string label) { L(label); c.Location=new Point(vx,y); c.Width=vw; c.Font=new Font("Segoe UI",9); Controls.Add(c); y+=gap; }

            txtId.ReadOnly=true; txtId.BackColor=Color.FromArgb(240,240,240); txtId.Text="(auto)";
            Row(txtId,"Agent ID:");
            Row(txtName,"Full Name: *");
            Row(txtEmail,"Email:");
            Row(txtDept,"Department:");
            Row(txtPhone,"Phone:");

            btnSave.Text="Add Agent"; btnSave.Size=new Size(120,34); btnSave.Location=new Point(vx,y+6);
            btnSave.BackColor=Color.FromArgb(65,125,175); btnSave.ForeColor=Color.White;
            btnSave.FlatStyle=FlatStyle.Flat; btnSave.Font=new Font("Segoe UI",9,FontStyle.Bold);
            btnSave.Click+=btnSave_Click; Controls.Add(btnSave);

            btnCancel.Text="Cancel"; btnCancel.Size=new Size(100,34); btnCancel.Location=new Point(vx+130,y+6);
            btnCancel.BackColor=Color.FromArgb(190,60,60); btnCancel.ForeColor=Color.White;
            btnCancel.FlatStyle=FlatStyle.Flat; btnCancel.Font=new Font("Segoe UI",9);
            btnCancel.Click+=btnCancel_Click; Controls.Add(btnCancel);

            Text=_isEdit?"Edit Agent":"Add New Agent";
            Size=new Size(430,y+100); StartPosition=FormStartPosition.CenterParent;
            FormBorderStyle=FormBorderStyle.FixedDialog; MaximizeBox=false; BackColor=Color.White;
            Load+=AgentForm_Load;
        }

        private TextBox txtId, txtName, txtEmail, txtDept, txtPhone;
        private Button btnSave, btnCancel;
    }
}
