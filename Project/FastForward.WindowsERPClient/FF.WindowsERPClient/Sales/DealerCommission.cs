using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.Sales
{
    public partial class DealerCommission : Base
    {
        public DealerCommission()
        {
            InitializeComponent();
            ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
            ucProfitCenterSearch1.ChangeCompany(false);
            ucProfitCenterSearch1.TextBoxCompany_Leave(null, null);
        }

        private void btnAddPC_Click(object sender, EventArgs e)
        {
            try
            {
                string com = ucProfitCenterSearch1.Company;
                string chanel = ucProfitCenterSearch1.Channel;
                string subChanel = ucProfitCenterSearch1.SubChannel;
                string area = ucProfitCenterSearch1.Area;
                string region = ucProfitCenterSearch1.Regien;
                string zone = ucProfitCenterSearch1.Zone;
                string pc = ucProfitCenterSearch1.ProfitCenter;

                dataGridViewPC.AutoGenerateColumns = false;
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy(com, chanel, subChanel, area, region, zone, pc);
                dataGridViewPC.DataSource = dt;

                foreach (DataGridViewRow gr in dataGridViewPC.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridViewPC.Rows[gr.Index].Cells[0];
                    chk.Value = "true";
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                this.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            dataGridViewSearchResult.AutoGenerateColumns = false;
            dataGridViewSearchResult.DataSource = Search();
           
        }

        private DataTable Search()
        {
            #region validation

            if (dataGridViewPC.Rows.Count <= 0)
            {
                MessageBox.Show("Please select profit centers","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return null;
            }

            if (dateTimePickerFrom.Value > dateTimePickerTo.Value) {
                MessageBox.Show("From date can not greater than To date", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return null;
            }

            #endregion

            DataTable _dt = new DataTable();
            foreach (DataGridViewRow gr in dataGridViewPC.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridViewPC.Rows[gr.Index].Cells[0];
                if (chk.EditedFormattedValue.ToString().ToUpper() == "TRUE")
                {
                    DataTable _tem = CHNLSVC.Sales.GetDelaerCommissionDetails(ucProfitCenterSearch1.Company.ToUpper(), gr.Cells[1].Value.ToString(), dateTimePickerFrom.Value.Date, dateTimePickerTo.Value.Date);
                    _dt.Merge(_tem);
                }
            }
            if (_dt.Rows.Count <= 0)
            {
                MessageBox.Show("Data not available", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return _dt;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.No) {
                    return;
                }

                if (dataGridViewPC.Rows.Count <= 0) {
                    MessageBox.Show("Please select profit centers", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                List<InvoiceItem> _itmList = new List<InvoiceItem>();
                DataTable _dt = Search();
                if (_dt != null)
                {
                    foreach (DataRow dr in _dt.Rows)
                    {
                        InvoiceItem _ii = new InvoiceItem();
                        _ii.Sad_itm_cd = dr["SAD_ITM_CD"].ToString();
                        _ii.Mi_cd = dr["SAH_PC"].ToString();
                        _ii.Sad_inv_no = dr["SAH_INV_NO"].ToString();
                        _ii.Sad_pbook = dr["SAD_PBOOK"].ToString();
                        _ii.Sad_pb_lvl = dr["SAD_PB_LVL"].ToString();
                        _ii.Mi_cre_dt = Convert.ToDateTime(dr["SAH_DT"]);
                        _ii.Mi_itm_tp = dr["SAH_INV_TP"].ToString();
                        _ii.Sad_qty = Convert.ToDecimal(dr["SAD_QTY"]);
                        _ii.Sad_unit_amt = Convert.ToDecimal(dr["SAD_UNIT_AMT"]);
                        _itmList.Add(_ii);
                    }
                }

                int result = CHNLSVC.Sales.UpdateItemCommission(_itmList);
                if (result > 0)
                {
                    MessageBox.Show("Record updated Successfully!!","Information",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    
                }
                else
                {
                    MessageBox.Show("Nothing Updated!!","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    
                }
                btnSearch_Click(null, null);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPCAll_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in dataGridViewPC.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridViewPC.Rows[gr.Index].Cells[0];
                chk.Value = "true";
            }
        }

        private void btnPCNone_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow gr in dataGridViewPC.Rows)
            {
                DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)dataGridViewPC.Rows[gr.Index].Cells[0];
                chk.Value = "false";
            }
        }

        private void btnPCClear_Click(object sender, EventArgs e)
        {
            dataGridViewPC.DataSource = null;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
           // DateTime _date = CHNLSVC.Security.GetServerDateTime();
           // dataGridViewPC.DataSource = null;
           // dateTimePickerFrom.Value = _date;
           // dateTimePickerTo.Value = _date;
           // dataGridViewSearchResult.DataSource = null;

           //// ucProfitCenterSearch1 = new UserControls.ucProfitCenterSearch();
           // ucProfitCenterSearch1.Company = BaseCls.GlbUserComCode;
           // ucProfitCenterSearch1.TextBoxCompany_Leave(null, null);
           // ucProfitCenterSearch1.ProfitCenter = BaseCls.GlbUserDefProf;
           // ucProfitCenterSearch1.Channel = "";
           // ucProfitCenterSearch1.SubChannel = "";
           // ucProfitCenterSearch1.Area = "";
           // ucProfitCenterSearch1.Regien = "";
           // ucProfitCenterSearch1.Zone = "";

            DealerCommission formnew = new DealerCommission();
            formnew.MdiParent = this.MdiParent;
            formnew.Location = this.Location;
            formnew.Show();
            this.Close();
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
