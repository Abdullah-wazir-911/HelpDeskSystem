namespace App.UI.Views
{
    partial class DashboardView
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing) { if (disposing && components != null) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            lblTitle=new Label(); btnRefresh=new Button(); dgv=new DataGridView();
            lblTotalLbl=new Label(); lblTotalVal=new Label();
            lblOpenLbl=new Label();  lblOpenVal=new Label();
            lblInProgLbl=new Label();lblInProgVal=new Label();
            lblResolvedLbl=new Label();lblResolvedVal=new Label();
            lblCriticalLbl=new Label();lblCriticalVal=new Label();
            lblHighLbl=new Label();  lblHighVal=new Label();
            lblRecentTitle=new Label();

            lblTitle.Text="Dashboard Overview"; lblTitle.Font=new Font("Segoe UI",13,FontStyle.Bold);
            lblTitle.ForeColor=Color.FromArgb(25,25,55); lblTitle.Location=new Point(18,15); lblTitle.AutoSize=true;

            btnRefresh.Text="🔄 Refresh"; btnRefresh.Location=new Point(18,48); btnRefresh.Size=new Size(110,28);
            btnRefresh.BackColor=Color.FromArgb(65,125,175); btnRefresh.ForeColor=Color.White;
            btnRefresh.FlatStyle=FlatStyle.Flat; btnRefresh.Font=new Font("Segoe UI",9);
            btnRefresh.Click+=btnRefresh_Click;

            // Cards
            string[] titles = {"Total","Open","In Progress","Resolved","Critical","High Priority"};
            Color[]  colors = {
                Color.FromArgb(65,125,175), Color.FromArgb(210,70,70),
                Color.FromArgb(230,130,0),  Color.FromArgb(50,160,70),
                Color.FromArgb(140,0,200),  Color.FromArgb(200,80,80)
            };
            Label[] vals = {lblTotalVal,lblOpenVal,lblInProgVal,lblResolvedVal,lblCriticalVal,lblHighVal};
            Label[] labs = {lblTotalLbl,lblOpenLbl,lblInProgLbl,lblResolvedLbl,lblCriticalLbl,lblHighLbl};

            int cx=18;
            for(int i=0;i<6;i++)
            {
                var card=new Panel{Location=new Point(cx,88),Size=new Size(145,100),BackColor=colors[i]};
                vals[i].Text="0"; vals[i].Font=new Font("Segoe UI",22,FontStyle.Bold);
                vals[i].ForeColor=Color.White; vals[i].Location=new Point(0,18); vals[i].Size=new Size(145,44); vals[i].TextAlign=ContentAlignment.MiddleCenter;
                labs[i].Text=titles[i]; labs[i].Font=new Font("Segoe UI",8);
                labs[i].ForeColor=Color.White; labs[i].Location=new Point(0,64); labs[i].Size=new Size(145,28); labs[i].TextAlign=ContentAlignment.MiddleCenter;
                card.Controls.Add(vals[i]); card.Controls.Add(labs[i]);
                Controls.Add(card); cx+=152;
            }

            lblRecentTitle.Text="Recent Tickets"; lblRecentTitle.Font=new Font("Segoe UI",11,FontStyle.Bold);
            lblRecentTitle.ForeColor=Color.FromArgb(25,25,55); lblRecentTitle.Location=new Point(18,205); lblRecentTitle.AutoSize=true;

            dgv.Location=new Point(18,230); dgv.Size=new Size(930,350);
            dgv.Anchor=AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Right|AnchorStyles.Bottom;

            Controls.Add(lblTitle); Controls.Add(btnRefresh);
            Controls.Add(lblRecentTitle); Controls.Add(dgv);
            BackColor=Color.FromArgb(245,246,250);
            Load+=DashboardView_Load;
        }

        private Label lblTitle, lblRecentTitle, btnRefreshLabel=new Label();
        private Button btnRefresh;
        private DataGridView dgv;
        private Label lblTotalLbl,lblTotalVal,lblOpenLbl,lblOpenVal;
        private Label lblInProgLbl,lblInProgVal,lblResolvedLbl,lblResolvedVal;
        private Label lblCriticalLbl,lblCriticalVal,lblHighLbl,lblHighVal;
    }
}
