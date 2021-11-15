using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.CommonSearch
{
    public partial class VIewManagerReminds : Base
    {
        private bool mouseIsDown = false;
        private Point firstPoint;
        public string AccountNo = string.Empty;

        private List<HPReminder> oHPReminder = new List<HPReminder>();

        private static Int32 _SeqNo;

        private Int32 selectedSeq = 0;

        public VIewManagerReminds(List<HPReminder> AccountNs)
        {
            InitializeComponent();
            oHPReminder = AccountNs;
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            int currentIndex = oHPReminder.IndexOf(oHPReminder.Find(x => x.Hra_seq == _SeqNo));

            try
            {
                int nextSeq = oHPReminder[currentIndex - 1].Hra_seq;
                viewDetails(nextSeq);
            }
            catch (Exception ex)
            {
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int currentIndex = oHPReminder.IndexOf(oHPReminder.Find(x => x.Hra_seq == _SeqNo));

            try
            {
                int nextSeq = oHPReminder[currentIndex + 1].Hra_seq;
                viewDetails(nextSeq);
            }
            catch (Exception ex)
            {
            }
        }

        private void btnInactive_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you want to accept this reminder ?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            HPReminder _rmd = new HPReminder();
            _rmd.Hra_seq = _SeqNo;
            _rmd.Hra_stus = "C";
            _rmd.Hra_stus_dt = DateTime.Now;
            _rmd.Hra_mod_by = BaseCls.GlbUserID;

            int result = CHNLSVC.Sales.UpdateHPReminder(_rmd);
            pnlReminder.Visible = false;
        }

        private void btnRMClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseIsDown)
            {
                int xDiff = firstPoint.X - e.Location.X;
                int yDiff = firstPoint.Y - e.Location.Y;

                int x = this.Location.X - xDiff;
                int y = this.Location.Y - yDiff;
                this.Location = new Point(x, y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void VIewManagerReminds_Load(object sender, EventArgs e)
        {
            selectedSeq = oHPReminder[0].Hra_seq;
            viewDetails(selectedSeq);
        }

        private void VIewManagerReminds_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnInactive_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void txtReminder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void viewDetails(int seq)
        {
            HPReminder obTemp = new HPReminder();
            obTemp = oHPReminder.Find(x => x.Hra_seq == seq);
            txtReminder.Text = obTemp.Hra_rmd;
            _SeqNo = Convert.ToInt32(obTemp.Hra_seq);
            lblAccount.Text = obTemp.Hra_ref;
            lblCount.Text = "Total Reminds : " + oHPReminder.FindAll(x => x.Hra_ref == obTemp.Hra_ref).Count.ToString();
            lblSeqNum.Text = _SeqNo.ToString();
            btnInactive.Focus();

            lblPossition.Text = (oHPReminder.IndexOf(obTemp) + 1).ToString() + "/" + oHPReminder.Count.ToString();
        }
    }
}