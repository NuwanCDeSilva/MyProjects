using System;
using System.Collections.Generic;
using FF.BusinessObjects;
using System.Linq;

namespace FF.WindowsERPClient.Services
{
    public partial class CustomerAlert : Base
    {
        private String gblJobNumer = string.Empty;

        public CustomerAlert(String JobNum)
        {
            InitializeComponent();
            gblJobNumer = JobNum;
            dgvTaskLevels.AutoGenerateColumns = false;
            dgvAlertLevels.AutoGenerateColumns = false;
            dgvAlertEmps.AutoGenerateColumns = false;
        }

        private void CustomerAlert_Load(object sender, EventArgs e)
        {
            Service_JOB_HDR oHedaer = CHNLSVC.CustService.GetServiceJobHeader(gblJobNumer, BaseCls.GlbUserComCode);
            List<MST_BUSPRIT_LVL> oItems = CHNLSVC.CustService.GetCustomerPriorityLevel(oHedaer.SJB_B_CUST_CD, BaseCls.GlbUserComCode);
            if (oItems != null && oItems.Count > 0)
            {
                MST_BUSPRIT_LVL oSelectedLvl = new MST_BUSPRIT_LVL();
                String PartyCode = String.Empty;
                String PartyType = String.Empty;
                if (oItems.FindAll(x => x.Mbl_pty_tp == "LOC").Count > 0)
                {
                    List<MST_BUSPRIT_LVL> ot1 = oItems.FindAll(x => x.Mbl_pty_tp == "LOC" && x.Mbl_pty_cd == BaseCls.GlbUserDefLoca);
                    oSelectedLvl = ot1[0];
                    PartyCode = BaseCls.GlbUserDefLoca;
                    PartyType = "LOC";
                    lblPriorityLevelText.Text = ot1[0].SCP_DESC;
                }
                else if (oItems.FindAll(x => x.Mbl_pty_tp == "SCHNL").Count > 0)
                {
                    List<MST_BUSPRIT_LVL> ot1 = oItems.FindAll(x => x.Mbl_pty_tp == "SCHNL" && x.Mbl_pty_cd == BaseCls.GlbDefChannel);
                    oSelectedLvl = ot1[0];
                    PartyCode = BaseCls.GlbDefChannel;
                    PartyType = "SCHNL";
                    lblPriorityLevelText.Text = ot1[0].SCP_DESC;
                }

                MST_BUSPRIT_TASK InPara = new MST_BUSPRIT_TASK();
                InPara.Mbt_cd = oHedaer.SJB_B_CUST_CD;
                InPara.Mbt_com = BaseCls.GlbUserComCode;
                InPara.Mbt_pty_tp = PartyType;
                InPara.Mbt_pty_cd = PartyCode;
                InPara.Mbt_prit_cd = oSelectedLvl.Mbl_prit_cd;

                List<MST_BUSPRIT_TASK> oTasks = CHNLSVC.CustService.GetCustomerPriorityTask(InPara);

                if (oTasks != null && oTasks.Count > 0)
                {
                    oTasks.ForEach(x => x.Mbt_expt_durText = x.Mbt_expt_dur.ToString() + " " + x.Mbt_expt_tp);
                    dgvTaskLevels.DataSource = new List<MST_BUSPRIT_TASK>();
                    dgvTaskLevels.DataSource = oTasks;

                    List<SCV_ALRT_LVL> oAlertLeves = CHNLSVC.CustService.GetAlertLevel(oTasks[0].Mbt_alrt_seq);
                    if (oAlertLeves != null && oAlertLeves.Count > 0)
                    {
                        oAlertLeves.ForEach(X => X.Srl_nxt_alrt_dur_Text = X.Srl_nxt_alrt_dur.ToString() + " " + X.Srl_nxt_alrt_tp);
                        dgvAlertLevels.DataSource = new List<SCV_ALRT_LVL>();
                        dgvAlertLevels.DataSource = oAlertLeves;
                    }

                    List<SCV_ALRT_EMP> oEmps = CHNLSVC.CustService.GetAlertEmployees(oTasks[0].Mbt_alrt_seq);
                    if (oEmps != null && oEmps.Count > 0)
                    {
                        foreach (SCV_ALRT_EMP item in oEmps)
                        {
                            if (item.Sre_is_mail == 1)
                            {
                                item.Sre_is_mail_Text = "Yes";
                            }
                            else
                            {
                                item.Sre_is_mail_Text = "No";
                            }

                            if (item.Sre_is_sms == 1)
                            {
                                item.Sre_is_sms_Text = "Yes";
                            }
                            else
                            {
                                item.Sre_is_sms_Text = "No";
                            }
                            if (item.Sre_is_sys == 1)
                            {
                                item.Sre_is_sys_Text = "Yes";
                            }
                            else
                            {
                                item.Sre_is_sys_Text = "No";
                            }
                            item.Sre_lvl_no_Text = item.Sre_lvl_no + " Level";
                        }

                        dgvAlertEmps.DataSource = new List<SCV_ALRT_EMP>();
                        dgvAlertEmps.DataSource = oEmps;
                    }

                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}