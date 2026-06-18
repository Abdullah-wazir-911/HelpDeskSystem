namespace App.UI.Views
{
    partial class AgentsView
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            lblTitle=new Label(); txtSearch=new TextBox(); lblCount=new Label();
            dgv=new DataGridView(); btnAdd=new Button(); btnEdit=new Button();
            btnDelete=new Button(); btnRefresh=new Button();

            lblTitle.Text="Agent Management"; lblTitle.Font=new Font("Segoe UI",13,FontStyle.Bold);
            lblTitle.ForeColor=Color.FromArgb(25,25,55); lblTitle.Location=new Point(18,15); lblTitle.AutoSize=true;

            void TB(Button b,string txt,int x,Color bg){
                b.Text=txt;b.Location=new Point(x,48);b.Size=new Size(118,30);
                b.BackColor=bg;b.ForeColor=Color.White;b.FlatStyle=FlatStyle.Flat;
                b.Font=new Font("Segoe UI",9);b.Cursor=Cursors.Hand;
            }
            TB(btnAdd,   "➕ Add Agent",  18, Color.FromArgb(50,150,80));
            TB(btnEdit,  "✏️ Edit",      144, Color.FromArgb(65,125,175));
            TB(btnDelete,"🗑 Delete",    268, Color.FromArgb(190,60,60));
            TB(btnRefresh,"🔄 Refresh",  392, Color.FromArgb(100,100,130));

            btnAdd.Click+=btnAdd_Click; btnEdit.Click+=btnEdit_Click;
            btnDelete.Click+=btnDelete_Click; btnRefresh.Click+=btnRefresh_Click;

            new Label{Text="Search:",Location=new Point(18,90),AutoSize=true,Font=new Font("Segoe UI",9)}.Parent=this;
            txtSearch.Location=new Point(72,87); txtSearch.Width=240; txtSearch.Font=new Font("Segoe UI",9);
            txtSearch.PlaceholderText="Search by name, email, department...";
            txtSearch.TextChanged+=txtSearch_TextChanged;

            lblCount.Font=new Font("Segoe UI",8); lblCount.ForeColor=Color.Gray;
            lblCount.Location=new Point(18,118); lblCount.AutoSize=true;

            dgv.Location=new Point(18,138); dgv.Size=new Size(930,460);
            dgv.Anchor=AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right|AnchorStyles.Bottom;

            Controls.Add(lblTitle); Controls.Add(txtSearch); Controls.Add(lblCount); Controls.Add(dgv);
            Controls.Add(btnAdd); Controls.Add(btnEdit); Controls.Add(btnDelete); Controls.Add(btnRefresh);
            BackColor=Color.FromArgb(245,246,250);
            Load+=AgentsView_Load;
        }

        private Label lblTitle, lblCount;
        private TextBox txtSearch;
        private DataGridView dgv;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh;
    }
}
