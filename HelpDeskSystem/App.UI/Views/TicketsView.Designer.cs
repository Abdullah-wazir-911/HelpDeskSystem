namespace App.UI.Views
{
    partial class TicketsView
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            lblTitle=new Label(); txtSearch=new TextBox(); cmbCat=new ComboBox();
            cmbSt=new ComboBox(); cmbPri=new ComboBox(); lblCount=new Label();
            dgv=new DataGridView(); btnAdd=new Button(); btnEdit=new Button();
            btnDelete=new Button(); btnRefresh=new Button();

            lblTitle.Text="Ticket Management"; lblTitle.Font=new Font("Segoe UI",13,FontStyle.Bold);
            lblTitle.ForeColor=Color.FromArgb(25,25,55); lblTitle.Location=new Point(18,15); lblTitle.AutoSize=true;

            // Toolbar buttons
            void TB(Button b, string txt, int x, Color bg) {
                b.Text=txt; b.Location=new Point(x,48); b.Size=new Size(118,30);
                b.BackColor=bg; b.ForeColor=Color.White; b.FlatStyle=FlatStyle.Flat;
                b.Font=new Font("Segoe UI",9); b.Cursor=Cursors.Hand;
            }
            TB(btnAdd,    "➕ Add Ticket",   18,  Color.FromArgb(50,150,80));
            TB(btnEdit,   "✏️ Edit",         144, Color.FromArgb(65,125,175));
            TB(btnDelete, "🗑 Delete",       268, Color.FromArgb(190,60,60));
            TB(btnRefresh,"🔄 Refresh",      392, Color.FromArgb(100,100,130));

            btnAdd.Click    +=btnAdd_Click;
            btnEdit.Click   +=btnEdit_Click;
            btnDelete.Click +=btnDelete_Click;
            btnRefresh.Click+=btnRefresh_Click;

            // Filters row
            Label FL(string t,int x){var l=new Label{Text=t,Location=new Point(x,90),AutoSize=true,Font=new Font("Segoe UI",9)};Controls.Add(l);return l;}
            FL("Search:",18);
            txtSearch.Location=new Point(72,87); txtSearch.Width=160; txtSearch.Font=new Font("Segoe UI",9);
            txtSearch.PlaceholderText="Search tickets..."; txtSearch.TextChanged+=txtSearch_TextChanged;

            FL("Category:",248);
            cmbCat.Location=new Point(315,87); cmbCat.Width=115; cmbCat.Font=new Font("Segoe UI",9);
            cmbCat.DropDownStyle=ComboBoxStyle.DropDownList; cmbCat.SelectedIndexChanged+=cmbCat_SelectedIndexChanged;

            FL("Status:",443);
            cmbSt.Location=new Point(496,87); cmbSt.Width=115; cmbSt.Font=new Font("Segoe UI",9);
            cmbSt.DropDownStyle=ComboBoxStyle.DropDownList; cmbSt.SelectedIndexChanged+=cmbSt_SelectedIndexChanged;

            FL("Priority:",622);
            cmbPri.Location=new Point(678,87); cmbPri.Width=115; cmbPri.Font=new Font("Segoe UI",9);
            cmbPri.DropDownStyle=ComboBoxStyle.DropDownList; cmbPri.SelectedIndexChanged+=cmbPri_SelectedIndexChanged;

            lblCount.Text=""; lblCount.Font=new Font("Segoe UI",8); lblCount.ForeColor=Color.Gray;
            lblCount.Location=new Point(18,118); lblCount.AutoSize=true;

            dgv.Location=new Point(18,138); dgv.Size=new Size(930,460);
            dgv.Anchor=AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right|AnchorStyles.Bottom;

            Controls.Add(lblTitle); Controls.Add(txtSearch);
            Controls.Add(cmbCat); Controls.Add(cmbSt); Controls.Add(cmbPri);
            Controls.Add(lblCount); Controls.Add(dgv);
            Controls.Add(btnAdd); Controls.Add(btnEdit); Controls.Add(btnDelete); Controls.Add(btnRefresh);
            BackColor=Color.FromArgb(245,246,250);
            Load+=TicketsView_Load;
        }

        private Label lblTitle, lblCount;
        private TextBox txtSearch;
        private ComboBox cmbCat, cmbSt, cmbPri;
        private DataGridView dgv;
        private Button btnAdd, btnEdit, btnDelete, btnRefresh;
    }
}
