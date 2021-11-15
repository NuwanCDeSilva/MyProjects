using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.UserControls
{
    public partial class ucServicePriority : UserControl
    {
        private Base _basePage;

        private String _JobNumer = string.Empty;
        private String _CustCode = string.Empty;

        public String GblJobNumber
        {
            get { return _JobNumer; }
            set { _JobNumer = value; }
        }

        public String GblCustCode
        {
            get { return _CustCode; }
            set { _CustCode = value; }
        }

        public ucServicePriority()
        {
            InitializeComponent();
            dgvTaskLevels.AutoGenerateColumns = false;
        }

        private void ucServicePriority_Load(object sender, EventArgs e)
        {
            lblPriorityLevelText.Text = "";
            dgvTaskLevels.DataSource = new List<scv_prit_task>();
        }

        public void LoadData()
        {
            _basePage = new Base();

            Service_JOB_HDR oHedaer;
            List<MST_BUSPRIT_LVL> oItems;

            if (String.IsNullOrEmpty(_CustCode))
            {
                oHedaer = _basePage.CHNLSVC.CustService.GetServiceJobHeader(GblJobNumber, BaseCls.GlbUserComCode);
                oItems = _basePage.CHNLSVC.CustService.GetCustomerPriorityLevel(oHedaer.SJB_B_CUST_CD, BaseCls.GlbUserComCode);
            }
            else
            {
                oItems = _basePage.CHNLSVC.CustService.GetCustomerPriorityLevel(GblCustCode, BaseCls.GlbUserComCode);
                if (oItems == null || oItems.Count == 0)
                {
                    oItems = _basePage.CHNLSVC.CustService.GetCustomerPriorityLevel("CASH", BaseCls.GlbUserComCode);
                }
            }

            if (oItems != null && oItems.Count > 0)
            {
                MST_BUSPRIT_LVL oSelectedLvl = new MST_BUSPRIT_LVL();
                String PartyCode = String.Empty;
                String PartyType = String.Empty;
                if (oItems.FindAll(x => x.Mbl_pty_tp == "LOC" && x.Mbl_pty_cd == BaseCls.GlbUserDefLoca).Count > 0)
                {
                    List<MST_BUSPRIT_LVL> ot1 = oItems.FindAll(x => x.Mbl_pty_tp == "LOC" && x.Mbl_pty_cd == BaseCls.GlbUserDefLoca);
                    oSelectedLvl = ot1[0];
                    PartyCode = BaseCls.GlbUserDefLoca;
                    PartyType = "LOC";
                    lblPriorityLevelText.Text = ot1[0].SCP_DESC;
                }
                else if (oItems.FindAll(x => x.Mbl_pty_tp == "SCHNL" && x.Mbl_pty_cd == BaseCls.GlbDefSubChannel).Count > 0)
                {
                    List<MST_BUSPRIT_LVL> ot1 = oItems.FindAll(x => x.Mbl_pty_tp == "SCHNL" && x.Mbl_pty_cd == BaseCls.GlbDefSubChannel);

                    if (ot1.Count > 0)
                    {
                        oSelectedLvl = ot1[0];
                        PartyCode = BaseCls.GlbDefSubChannel;
                        PartyType = "SCHNL";
                        lblPriorityLevelText.Text = ot1[0].SCP_DESC;
                    }
                }

                scv_prit_task InPara = new scv_prit_task();
                InPara.Spit_com = BaseCls.GlbUserComCode;
                InPara.Spit_pty_tp = PartyType;
                InPara.Spit_pty_cd = PartyCode;
                InPara.Spit_prit_cd = oSelectedLvl.Mbl_prit_cd;

                try
                {
                    var myColor = "#[color from database]";
                    Color newColor = System.Drawing.ColorTranslator.FromHtml(oSelectedLvl.SCP_COLOR);
                    lblPriorityLevelText.BackColor = newColor;
                    label1.BackColor = newColor;
                }
                catch (Exception ex)
                {
                    switch (oSelectedLvl.SCP_COLOR)
                    {
                        case "Red":
                            lblPriorityLevelText.BackColor = Color.Red;
                            break;

                        case "Coral":
                            lblPriorityLevelText.BackColor = Color.Coral;
                            break;

                        case "Gainsboro":
                            lblPriorityLevelText.BackColor = Color.Gainsboro;
                            break;

                        case "Green":
                            lblPriorityLevelText.BackColor = Color.Green;
                            break;

                        default:
                            break;
                    }
                }

                List<scv_prit_task> oPTask = _basePage.CHNLSVC.CustService.GetPriorityTask(InPara);
                dgvTaskLevels.DataSource = new List<scv_prit_task>();
                foreach (scv_prit_task item in oPTask)
                {
                    if (item.Spit_expt_tp == "DD")
                    {
                        item.Spit_expt_durTxt = item.Spit_expt_dur + " Days";
                    }
                    else if (item.Spit_expt_tp == "MM")
                    {
                        item.Spit_expt_durTxt = item.Spit_expt_dur + " Mins";
                    }
                    else if (item.Spit_expt_tp == "HH")
                    {
                        item.Spit_expt_durTxt = item.Spit_expt_dur + " Hours";
                    }
                    else if (item.Spit_expt_tp == "YY")
                    {
                        item.Spit_expt_durTxt = item.Spit_expt_dur + " Years";
                    }
                }
                dgvTaskLevels.DataSource = oPTask;
            }
        }

        public void clearAll()
        {
            lblPriorityLevelText.Text = "";
            dgvTaskLevels.DataSource = new List<scv_prit_task>();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}