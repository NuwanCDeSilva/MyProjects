using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.MDINotification
{
    public partial class frmAccountReminders : Base
    {
        private static Int32 _SeqNo;

        public frmAccountReminders()
        {
            InitializeComponent();
        }

        private bool mouseIsDown = false;
        private Point firstPoint;

        private List<HPReminder> oHPReminder = new List<HPReminder>();
        private List<HPReminder> oHPReminderSelected = new List<HPReminder>();

        //DataTable dtReminders = new DataTable();
        private DataRow[] SeletedRows;

        private int currentIndex = 0;

        private void frmAccountReminders_Load(object sender, EventArgs e)
        {
            pnlReminder.Visible = false;
            gvView.AutoGenerateColumns = false;
            oHPReminder = CHNLSVC.General.Notification_Get_AccountRemindersDetails(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            gvView.DataSource = oHPReminder;
            Cursor = Cursors.Default;
        }

        private void gvView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (gvView.SelectedRows.Count > 0)
                {
                    string selectedAcc = gvView.SelectedRows[0].Cells[0].Value.ToString();
                    string selectedSeq = gvView.SelectedRows[0].Cells[4].Value.ToString();

                    oHPReminderSelected = oHPReminder.FindAll(x => x.Hra_ref == selectedAcc);

                    if (oHPReminderSelected.Count > 0)
                    {
                        txtReminder.Text = oHPReminderSelected.Find(x => x.Hra_seq == Convert.ToInt32(selectedSeq)).Hra_rmd;
                        _SeqNo = Convert.ToInt32(oHPReminderSelected.Find(x => x.Hra_seq == Convert.ToInt32(selectedSeq)).Hra_seq);
                        lblAccount.Text = oHPReminderSelected.Find(x => x.Hra_seq == Convert.ToInt32(selectedSeq)).Hra_ref;
                        lblCount.Text = "# Of Reminders : " + oHPReminder.FindAll(x => x.Hra_ref == selectedAcc).Count.ToString();

                        int currentIndex = oHPReminderSelected.IndexOf(oHPReminder.Find(x=>x.Hra_ref == lblAccount.Text) ) + 1;
                        lblSeqNum.Text = _SeqNo.ToString();
                        txtReminder.Focus();
                        pnlReminder.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void btnPre_Click(object sender, EventArgs e)
        {
            int currentIndex = oHPReminderSelected.IndexOf(oHPReminder.Find(x => x.Hra_seq == _SeqNo));

            try
            {
                int nextSeq = oHPReminderSelected[currentIndex - 1].Hra_seq;
                viewDetails(nextSeq);
            }
            catch (Exception ex)
            {
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            int currentIndex = oHPReminderSelected.IndexOf(oHPReminder.Find(x => x.Hra_seq == _SeqNo));

            try
            {
                int nextSeq = oHPReminderSelected[currentIndex + 1].Hra_seq;
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
            frmAccountReminders_Load(null, null);
        }

        private void pnlReminder_MouseDown(object sender, MouseEventArgs e)
        {
            firstPoint = e.Location;
            mouseIsDown = true;
        }

        private void pnlReminder_MouseMove(object sender, MouseEventArgs e)
        {

        }

        private void frmAccountReminders_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void gvView_KeyDown(object sender, KeyEventArgs e)
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
                pnlReminder.Visible = false;
            }
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

                int x = pnlReminder.Location.X - xDiff;
                int y = pnlReminder.Location.Y - yDiff;
                this.pnlReminder.Location = new Point(x, y);
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseIsDown = false;
        }

        private void btnRMClose_Click(object sender, EventArgs e)
        {
            pnlReminder.Visible = false;
        }

        private void viewDetails(int seq)
        {
            HPReminder obTemp = new HPReminder();
            obTemp = oHPReminder.Find(x => x.Hra_seq == seq);
            txtReminder.Text = obTemp.Hra_rmd;
            _SeqNo = Convert.ToInt32(obTemp.Hra_seq);
            lblAccount.Text = obTemp.Hra_ref;
            lblCount.Text = "# Of Reminders : " + oHPReminder.FindAll(x => x.Hra_ref == obTemp.Hra_ref).Count.ToString();
            lblSeqNum.Text = _SeqNo.ToString();
        }

    }
}