using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.BusinessObjects.General;
using System.Data;

namespace FF.WindowsERPClient.MDINotification
{
    public partial class frmNotification : Base
    {
        private MasterProfitCenter _masterProfitCenter = null;

        public frmNotification()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            setPanalPossitions();
        }

        private void frmNotification_Load(object sender, EventArgs e)
        {
            ShowDetails(1);
            setPanalPossitions();
        }

        private void lstReminders_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lstReminders.SelectedItems.Count > 0)
                {
                    if (Convert.ToInt16(lstReminders.SelectedItems[0].SubItems[2].Text) == (Int16)NotificationTypes.AccountReminders)
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10084)) return;
                        if (!checkFormExist())
                        {
                            Cursor = Cursors.WaitCursor;
                            frmAccountReminders frm = new frmAccountReminders();
                            frm.ShowDialog();
                        }
                    }
                    else if (Convert.ToInt16(lstReminders.SelectedItems[0].SubItems[2].Text) == (Int16)NotificationTypes.GIT)
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10083)) return;
                        if (FormOpenAlready("InterCompanyInWardEntry") == true) return;
                        Inventory.InterCompanyInWardEntry _inward = new Inventory.InterCompanyInWardEntry();
                        _inward.GlbModuleName = "m_Trans_Inventory_StockTransferInward";
                        _inward.FormClosing += child_FormClosed;
                        _inward.MdiParent = this.MdiParent;

                        //(MainMenu)this.MdiParent.tss

                        this.Close();
                        _inward.Show();
                    }
                    else if (Convert.ToInt16(lstReminders.SelectedItems[0].SubItems[2].Text) == (Int16)NotificationTypes.LastDayendprocessedDate)
                    {
                        if (!CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10085)) return;
                        frmDayendHistory frm = new frmDayendHistory();
                        frm.ShowDialog();
                    }

                    else if (Convert.ToInt16(lstReminders.SelectedItems[0].SubItems[2].Text) == (Int16)NotificationTypes.OtherShopCollection)
                    {
                        frmOtherShopCollections frm = new frmOtherShopCollections();
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Cursor = Cursors.Default;
        }

        private bool checkFormExist()
        {
            bool status = false;

            foreach (Form item in Application.OpenForms)
            {
                if (item.Name == "frmAccountReminders")
                {
                    status = true;
                }
            }
            return status;
        }

        private void lstControlAct_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (lstControlAct.SelectedItems.Count > 0)
                {
                    if (Convert.ToInt16(lstControlAct.SelectedItems[0].SubItems[2].Text) == (Int16)NotificationTypes.GIT)
                    {
                        if (FormOpenAlready("InterCompanyInWardEntry") == true) return;
                        Inventory.InterCompanyInWardEntry _inward = new Inventory.InterCompanyInWardEntry();
                        //_inward.GlbModuleName = ((ToolStripMenuItem)sender).Name.ToString();
                        _inward.GlbModuleName = "m_Trans_Inventory_StockTransferInward";
                        //_inward.FormClosed += child_FormClosed;
                        _inward.MdiParent = this.MdiParent;
                        this.Close();
                        _inward.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Boolean FormOpenAlready(string formName)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Name == formName)
                {
                    f.WindowState = FormWindowState.Normal;
                    f.Activate();
                    return true;
                }
            }
            return false;
        }

        public void DisposeAllButThis(Form form)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm.GetType() == form.GetType()
                    && frm != form)
                {
                    frm.Close();
                }
            }
        }

        private void frmNotification_Resize(object sender, EventArgs e)
        {
            setPanalPossitions();
        }

        public void setPanalPossitions()
        {
            pnlCA.Width = (this.Width / 2) - 4 - 2;
            pnlRM.Width = (this.Width / 2) - 4 - 2;
            pnlTop.Width = this.Width - 4 - 4;
            pnlBottom.Width = pnlTop.Width;

            pnlTop.Location = new System.Drawing.Point(4, 4);
            pnlBottom.Height = monthCalendar1.Height + 2 * (monthCalendar1.Height / 4);

            pnlTop.Height = pnlCA.Height + (pictureBox1.Height + 6) * 2;

            pnlCA.Height = pnlTop.Height - (2 * (pictureBox1.Height + 6));
            pnlCA.Location = new System.Drawing.Point(4, pictureBox1.Height + 6 + 4);
            lstControlAct.Width = pnlCA.Width;

            pnlRM.Height = pnlTop.Height - (2 * (pictureBox1.Height + 6));
            pnlRM.Location = new System.Drawing.Point(4 + pnlCA.Width + 4, pictureBox1.Height + 6 + 4);

            pnlBottom.Location = new System.Drawing.Point(4, pnlTop.Height + 4);

            monthCalendar1.Location = new System.Drawing.Point(pnlBottom.Width - monthCalendar1.Width - 20, 5);

            lblThoughtHeading.Location = new System.Drawing.Point(10, 10);
            richTextBox1.Location = new System.Drawing.Point(20, 40);
            richTextBox1.Width = pnlBottom.Width - 20 - 20 - monthCalendar1.Width;

            pnlBottom.Height = this.Height - 4 - pnlTop.Height - 4;

            picLog.Location = new Point(pnlBottom.Width - picLog.Width, pnlBottom.Height - picLog.Height);

            panel1.Width = pnlTop.Width;
            panel1.Enabled = false;

            if (lstControlAct.Items.Count == 0)
            {
                pnlCA.Visible = false;
                pnlRM.Width = this.Width - 4 - 4;
                pnlRM.Location = new System.Drawing.Point(4, pictureBox1.Height + 6 + 4);
            }
            else
            {
                pnlCA.Visible = true;
            }

            if (lstReminders.Items.Count == 0)
            {
                pnlRM.Visible = false;
                pnlCA.Width = this.Width - 4 - 4;
            }
            else
            {
                pnlRM.Visible = true;
            }
        }

        private void child_FormClosed(object sender, FormClosingEventArgs e)
        {
            GetNotificationDetails();
        }

        public void GetNotificationDetails()
        {
            if (FormOpenAlready("frmNotification") == true)
            {
                return;
            }

            frmNotification frm = new frmNotification();

            frm.MdiParent = Application.OpenForms[1];
            frm.ControlBox = false;
            frm.Show();
        }

        public void ShowDetails(int _initial)
        {
            lstControlAct.Clear();
            lstReminders.Clear();
            _masterProfitCenter = CacheLayer.Get<MasterProfitCenter>(CacheLayer.Key.ProfitCenter.ToString());

            lstControlAct.Columns.Add("Discription",160);
            lstControlAct.Columns.Add("Value", 180, HorizontalAlignment.Left);
            lstControlAct.Columns.Add("NotificationTypes", 200);

            lstReminders.Columns.Add("Discription", 550);
            lstReminders.Columns.Add("NotificationTypes", 0);

            this.Dock = DockStyle.Fill;
            List<Notification> controlActivity = new List<Notification>();
            List<Notification> Remonders = new List<Notification>();
            List<Thoughts> oThoughts = new List<Thoughts>();

            oThoughts.Clear();
            Remonders.Clear();
            controlActivity.Clear();
            lstControlAct.Items.Clear();
            lstReminders.Items.Clear();

            if (_initial == 0)  //-- Sanjeewa 2016-11-15 Remove login issue when Standby Down
            {
                if (CHNLSVC.General.GetNotification(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, BaseCls.GlbUserID, out controlActivity, out Remonders, out oThoughts))
                {
                    int ItemCount = 1;
                    for (int i = 0; i < controlActivity.Count; i++)
                    {
                        if (Convert.ToInt16(controlActivity[i].NotificationType) == (Int16)NotificationTypes.FDSales)
                        {
                            if (_masterProfitCenter == null)
                            {
                                continue;
                            }
                            ListViewItem lsItem = new ListViewItem("(" + (ItemCount).ToString() + ") " + controlActivity[i].Discription);
                            lsItem.SubItems.Add(_masterProfitCenter.Mpc_max_fwdsale.ToString());
                            //lsItem.SubItems.Add(controlActivity[i].NotificationType.ToString());
                            lsItem.Font = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Bold);
                            lstControlAct.Items.Add(lsItem);
                            ItemCount++;
                        }
                        else if (Convert.ToInt16(controlActivity[i].NotificationType) == (Int16)NotificationTypes.SalesFigureItem || Convert.ToInt16(controlActivity[i].NotificationType) == (Int16)NotificationTypes.AccountStatusItem)
                        {
                            controlActivity[i].Discription = ConvertTo_ProperCase(controlActivity[i].Discription.ToString());
                            ListViewItem lsItem = new ListViewItem("       " + controlActivity[i].Discription);
                            lsItem.SubItems.Add(controlActivity[i].Value);
                            lsItem.SubItems.Add(controlActivity[i].Value2);
                            lsItem.Font = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Regular);
                            lstControlAct.Items.Add(lsItem);
                        }
                        else
                        {
                            ListViewItem lsItem = new ListViewItem("(" + (ItemCount).ToString() + ") " + controlActivity[i].Discription);
                            lsItem.SubItems.Add(controlActivity[i].Value);
                            lsItem.SubItems.Add(controlActivity[i].Value2);
                            lsItem.Font = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Bold);
                            lstControlAct.Items.Add(lsItem);
                            ItemCount++;
                        }
                    }
                    for (int i = 0; i < Remonders.Count; i++)
                    {
                        ListViewItem lsItem = new ListViewItem("(" + (i + 1).ToString() + ") " + Remonders[i].Discription);
                        lsItem.SubItems.Add(Remonders[i].Value);
                        lsItem.SubItems.Add(Remonders[i].NotificationType.ToString());
                        lsItem.Font = new System.Drawing.Font("Tahoma", 8, System.Drawing.FontStyle.Regular);
                        lstReminders.Items.Add(lsItem);
                    }
                }
            }

            if (oThoughts.Count > 0)
            {
                if (oThoughts[0].RDT_IS_PERIOD == 1)
                {
                    lblThoughtHeading.Visible = false;
                    richTextBox1.ForeColor = Color.Crimson;
                }
                else
                {
                    lblThoughtHeading.Visible = true;
                    richTextBox1.ForeColor = Color.RoyalBlue;
                }
                richTextBox1.Text = oThoughts[0].RDT_COL1 + "\n" + oThoughts[0].RDT_COL2 + "\n" + oThoughts[0].RDT_COL3 + "\n" + oThoughts[0].RDT_COL4 + "\n" + oThoughts[0].RDT_COL5;
            }
            else
            {
                lblThoughtHeading.Visible = false;
            }
            SetLogoImage();
            setLogOnInfor();    //kapila 23/2/2017
        }

        private void setLogOnInfor()
        {
            DataTable _dt = CHNLSVC.Security.GetUserLastLogTrans(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 0);
           gvRec.AutoGenerateColumns = false;
           gvRec.DataSource = _dt;
        }

        private void richTextBox1_Click(object sender, EventArgs e)
        {
            monthCalendar1.Focus();
        }

        private void SetLogoImage()
        {
            MasterCompany oMasterCompany = new MasterCompany();
            oMasterCompany = CHNLSVC.General.GetCompByCode(BaseCls.GlbUserComCode);
            picLog.BackColor = Color.White;

            if (!string.IsNullOrEmpty(oMasterCompany.Mc_anal20))
            {
                Stream s = this.GetType().Assembly.GetManifestResourceStream("FF.WindowsERPClient.Resources." + oMasterCompany.Mc_anal20);
                if (s == null)
                {
                    return;
                }
                Bitmap bmp = new Bitmap(s);
                this.picLog.Image = bmp;
                picLog.SizeMode = PictureBoxSizeMode.StretchImage;
                s.Close();
            }
            setColours(oMasterCompany.Mc_anal21);
        }

        public static Bitmap GetImageByName(string imageName)
        {
            System.Reflection.Assembly asm = System.Reflection.Assembly.GetExecutingAssembly();
            string resourceName = asm.GetName().Name + ".Properties.Resources";
            var rm = new System.Resources.ResourceManager(resourceName, asm);
            return (Bitmap)rm.GetObject("Abans.png");
        }

        private void setColours(string Colour)
        {
            switch (Colour)
            {
                case "purple":

                    Color clrPnlHeading = Color.FromArgb(126, 25, 116);
                    Color clrPnlBody = Color.FromArgb(235, 217, 255);

                    rectangleShape1.BackColor = clrPnlHeading;
                    lblPnlCAHeading.BackColor = clrPnlHeading;
                    pnlCA.BackColor = clrPnlBody;
                    lstControlAct.BackColor = clrPnlBody;
                    panel1.BackColor = clrPnlHeading;

                    rectangleShape2.BackColor = clrPnlHeading;
                    label1.BackColor = clrPnlHeading;
                    pnlRM.BackColor = clrPnlBody;
                    lstReminders.BackColor = clrPnlBody;



                    break;

                case "blue":

                    Color clrPnlHeadingblue = Color.FromArgb(41, 41, 121);
                    Color clrPnlBodyblue = Color.FromArgb(207, 226, 255);

                    rectangleShape1.BackColor = clrPnlHeadingblue;
                    lblPnlCAHeading.BackColor = clrPnlHeadingblue;
                    pnlCA.BackColor = clrPnlBodyblue;
                    lstControlAct.BackColor = clrPnlBodyblue;
                    panel1.BackColor = clrPnlHeadingblue;

                    rectangleShape2.BackColor = clrPnlHeadingblue;
                    label1.BackColor = clrPnlHeadingblue;
                    pnlRM.BackColor = clrPnlBodyblue;
                    lstReminders.BackColor = clrPnlBodyblue;
                    break;
            }
        }

        private void frmNotification_FormClosed(object sender, FormClosedEventArgs e)
        {

        }
    }
}