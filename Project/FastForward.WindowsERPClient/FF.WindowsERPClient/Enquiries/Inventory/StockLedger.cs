using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;
using FF.Interfaces;

namespace FF.WindowsERPClient.Enquiries.Inventory
{
    public partial class StockLedger : Base
    {
        DateTime minDate;

        public StockLedger()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you want to exit?", "Quit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void LoadSubLocation(string com,string loc,DataGridView grid)
        {
            try
            {
                //load all locs
                List<MasterLocation> _subLoc = CHNLSVC.Inventory.getAllLoc_WithSubLoc(com, loc);
                grid.DataSource = _subLoc;

                foreach (DataGridViewRow row in grid.Rows)
                {
                    DataGridViewCheckBoxCell chk = (DataGridViewCheckBoxCell)row.Cells[0];
                    chk.Value = true;
                }
                grid.EndEdit();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void StockLedger_Load(object sender, EventArgs e)
        {
            try
            {
                this.ActiveControl = txtItemCode;
                txtCompany.Text = BaseCls.GlbUserComCode;
                txtSerialCompany.Text = BaseCls.GlbUserComCode;
                ValidateAndLoadCompany(txtCompany.Text, lblCompany);
                ValidateAndLoadCompany(txtSerialCompany.Text, lblSerialCompany);
                gvSubLocation.AutoGenerateColumns = false;
                gvSerialLocs.AutoGenerateColumns = false;
                gvSerialDetails.AutoGenerateColumns = false;
                gvDocumentDetails.AutoGenerateColumns = false;
                string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
                //check company search permission
                if (!CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "INV6"))
                {
                    btnSearchCompany.Enabled = false;
                    txtCompany.Enabled = false;
                    btnSerialCompanySearch.Enabled = false;
                    txtSerialCompany.Enabled = false;
                }
                else
                {
                    btnSearchCompany.Enabled = true;
                    txtCompany.Enabled = true;
                    btnSerialCompanySearch.Enabled = true;
                    txtSerialCompany.Enabled = true;
                }
                DateTime _date = CHNLSVC.Security.GetServerDateTime();
                //Load default location
                txtLocation.Text = BaseCls.GlbUserDefLoca;
                txtSerialLocation.Text = BaseCls.GlbUserDefLoca;
                LoadSubLocation(txtCompany.Text, txtLocation.Text, gvSubLocation);
                LoadSubLocation(txtSerialCompany.Text, txtSerialLocation.Text, gvSerialLocs);
                txtLocation_Leave(null, null);
                txtSerialLocation_Leave(null, null);
                dateTimePickerFrom.Value = _date.AddMonths(-1);
                dateTimePickerSerialFrom.Value = _date.AddMonths(-1);
                tabControl1.SelectedTab = tabPage2;
                tabControl1.SelectedTab = tabPage1;


                DataTable _loc = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, txtSerialLocation.Text);
                if (_loc != null && _loc.Rows.Count > 0)
                {
                    if (_loc.Rows[0]["ml_scm2_st_dt"].ToString() != "")
                        minDate = Convert.ToDateTime(_loc.Rows[0]["ml_scm2_st_dt"]);
                    else
                        minDate = DateTime.MinValue;
                }
            }
            catch (Exception ex)
            {
               //var w32ex = ex.InnerException as Win32Exception;
               
                var exe = ex.InnerException as System.ServiceModel.CommunicationException;

                

                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }

        }

        private void chkSubLocAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSubLocAll.Checked)
                {
                    foreach (DataGridViewRow row in gvSubLocation.Rows)
                    {
                        row.Cells[0].Value = true;
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in gvSubLocation.Rows)
                    {
                        row.Cells[0].Value = false; ;
                    }
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

        private void ClearAll()
        {
            try
            {
                if (tabControl1.SelectedIndex == 0)
                {
                    txtItemCode.Text = "";
                    txtCompany.Text = BaseCls.GlbUserComCode;
                    txtLocation.Text = "";
                    gvDocumentDetails.DataSource = null;
                    gvSubLocation.DataSource = null;
                    chkStatus.Checked = false;
                    chkSubLocAll.Checked = true;
                    ValidateAndLoadCompany(txtCompany.Text, lblCompany);
                    DateTime _date = CHNLSVC.Security.GetServerDateTime();
                    dateTimePickerFrom.Value = _date.Date;
                    dateTimePickerTo.Value = _date.Date;

                    lblDesc.Text = "";
                    lblLocation.Text = "";
                    lblItemDes.Text = "";
                    dateTimePickerFrom.Value = _date.AddMonths(-1);
                    txtLocation.Text = BaseCls.GlbUserDefLoca;
                    ValidateAndLoadLocation(txtCompany.Text, txtLocation.Text, lblLocation);
                    LoadSubLocation(txtCompany.Text, txtLocation.Text, gvSubLocation);

                }
                else
                {
                    txtSerialItemCode.Text = "";
                    txtSerialCompany.Text = BaseCls.GlbUserComCode;
                    txtSerialLocation.Text = "";
                    gvSerialDetails.DataSource = null;
                    gvSerialLocs.DataSource = null;
                    chkSerialSubLocAll.Checked = true;
                    ValidateAndLoadCompany(txtSerialCompany.Text, lblSerialCompany);
                    DateTime _date = CHNLSVC.Security.GetServerDateTime();
                    dateTimePickerSerialFrom.Value = _date.Date;
                    dateTimePickerSerialTo.Value = _date.Date;

                    lblSerialItem.Text = "";
                    lblSerialLoc.Text = "";
                    lblSerialItem.Text = "";

                    dateTimePickerSerialFrom.Value = _date.AddMonths(-1);

                    txtSerialLocation.Text = BaseCls.GlbUserDefLoca;
                    ValidateAndLoadLocation(txtSerialCompany.Text, txtSerialLocation.Text, lblSerialLoc);
                    LoadSubLocation(txtSerialCompany.Text, txtSerialLocation.Text, gvSerialLocs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        #region button click events

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                ClearAll();
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

        private void btnReject_Click(object sender, EventArgs e)
        {

            try
            {
               //txtLocation_Leave(null, null);
                if (tabControl1.SelectedIndex == 0)
                {
                    this.Cursor = Cursors.WaitCursor;
                    string locs = "";
                    gvSubLocation.EndEdit();
                    foreach (DataGridViewRow row in gvSubLocation.Rows)
                    {
                        DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(chk.Value))
                        {
                            locs = locs + row.Cells[1].Value + ",";
                        }
                    }

                    DateTime _dtFrom = Convert.ToDateTime(dateTimePickerFrom.Value.Date);
                    DateTime _dtTo = Convert.ToDateTime(dateTimePickerTo.Value.Date);
                    DateTime _maxDate = _dtFrom.AddMonths(6).AddDays(-1);
                    if (_dtTo > _maxDate)
                    {
                        MessageBox.Show("Maximum date range allowed is 6 months period !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvDocumentDetails.DataSource = null;
                        return;
                    }

                    if (locs == "")
                    {
                        MessageBox.Show("Please select Location from location grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvDocumentDetails.DataSource = null;
                        return;
                    }
                    else if (txtItemCode.Text == "")
                    {
                        MessageBox.Show("Please select Item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvDocumentDetails.DataSource = null;
                        return;
                    }
                    else if (txtCompany.Text == "")
                    {
                        MessageBox.Show("Please select company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvDocumentDetails.DataSource = null;
                        return;
                    }
                    else
                    {
                        if (BaseCls.GlbUserComCode == "AST")
                        {
                            string _item = "";
                            //kapila 18/11/2013
                            if (txtItemCode.Text.Length == 16)
                                _item = txtItemCode.Text.Substring(1, 7);
                            else if (txtItemCode.Text.Length == 15)
                                _item = txtItemCode.Text.Substring(0, 7);
                            else if (txtItemCode.Text.Length == 8)
                                _item = txtItemCode.Text.Substring(1, 7);
                            else if (txtItemCode.Text.Length == 20)
                                _item = txtItemCode.Text.Substring(0, 12);
                            else
                                _item = txtItemCode.Text;

                            txtItemCode.Text = _item;
                        }
                        if (chkStatus.Checked)
                        {
                            MasterItem _mstItem = CHNLSVC.General.GetItemMaster(txtItemCode.Text);
                            DataTable dt = GetStockLedgerData(BaseCls.GlbUserID, "", _mstItem.Mi_brand, _mstItem.Mi_model, txtItemCode.Text.Trim(),
                                0, _mstItem.Mi_cate_1, _mstItem.Mi_cate_2, _mstItem.Mi_cate_3, 0, dateTimePickerFrom.Value.Date, 0, 1, txtCompany.Text.Trim(), locs, dateTimePickerFrom.Value.Date,
                                dateTimePickerTo.Value.Date, 1, txtDocTp.Text.Trim());
                            if (!string.IsNullOrEmpty(txtDocTp.Text))
                            {
                                for (int x = dt.Rows.Count - 1; x >= 0; x--)
                                {
                                    DataRow dr = dt.Rows[x];
                                    if (dr["DOC_TYPE"].ToString() == "OPERNING_BAL")
                                    {
                                        dr.Delete();
                                    }
                                }
                                dt.AcceptChanges();
                            }
                            else
                            {
                                dt = MakeGridBalWithStatus(dt);
                            }
                            DataTable tem = dt.Clone();
                            tem.Columns.Add("id", typeof(int));
                            int i = 0;
                            foreach (DataRow dr in dt.Rows)
                            {

                                DataRow drT = tem.NewRow();
                                drT[0] = dr[0].ToString();
                                drT[1] = dr[1].ToString();
                                drT[2] = dr[2].ToString();
                                drT[3] = dr[3].ToString();
                                drT[4] = dr[4].ToString();
                                drT[5] = dr[5].ToString();
                                drT[6] = dr[6].ToString();
                                drT[7] = dr[7].ToString();
                                drT[8] = dr[8].ToString();
                                if (dr["DOC_TYPE"].ToString().ToUpper() == "OPERNING_BAL")
                                {
                                    drT[9] = dr[9].ToString();
                                }
                                else
                                {
                                    int cou;
                                    if (dr[7].ToString() != "0")
                                        cou = Convert.ToInt32(dr[7]);
                                    else
                                        cou = -1 * Convert.ToInt32(dr[8]);
                                    var _result = (from _res in tem.AsEnumerable()
                                                   //group _res by new { column1 = _res[0], column2 = _res[10], Column3 = _res[9], Column4 = _res[2] } into _res1
                                                   where _res.Field<string>(10) == dr[10].ToString()
                                                   orderby _res.Field<int>(11) descending
                                                   select _res).FirstOrDefault();
                                    if (_result != null)
                                        drT[9] = (Convert.ToInt32(_result[9].ToString()) + cou).ToString();
                                    else
                                        drT[9] = cou;
                                }
                                drT[10] = dr[10].ToString();
                                drT[11] = i++;
                                tem.Rows.Add(drT);
                            }
                            if (tem.Rows.Count > 0)
                            {
                                //DataView dv = tem.DefaultView;
                                //dv.Sort = "doc_date,SEQ_NO,doc_no";
                                //tem = dv.ToTable();
                            }
                            //  tem = LoadBal(tem);
                            gvDocumentDetails.DataSource = tem;
                            gvDocumentDetails.Columns[7].Visible = true;

                            ////if (!string.IsNullOrWhiteSpace(txtDocTp.Text))
                            ////{
                            ////    gvDocumentDetails.Columns[10].Visible = false;
                            ////}
                            ////else
                            ////{
                            ////    gvDocumentDetails.Columns[10].Visible = true;
                            ////}

                            gvDocumentDetails.DataSource = tem;
                            gvDocumentDetails.Columns[7].Visible = true;

                        }
                        else
                        {
                            MasterItem _mstItem = CHNLSVC.General.GetItemMaster(txtItemCode.Text.ToUpper());
                            DataTable dt = new DataTable("tbl");
                            //DataTable dt = CHNLSVC.Inventory.StockBalanceSearch1(dateTimePickerDWFrom.Date, dateTimePickerDWTo.Date, txtItemCode.Text, _DWSublocs, txtDWCompany.Text, false, !string.IsNullOrEmpty(txtDocTp.Text)?txtDocTp.Text:null);
                            dt = GetStockLedgerData(BaseCls.GlbUserID, "", _mstItem.Mi_brand, _mstItem.Mi_model, txtItemCode.Text.Trim(),
                                0, _mstItem.Mi_cate_1, _mstItem.Mi_cate_2, _mstItem.Mi_cate_3, 0, dateTimePickerFrom.Value.Date, 0, 0, txtCompany.Text.Trim(), locs, dateTimePickerFrom.Value.Date,
                                dateTimePickerTo.Value.Date, 0, txtDocTp.Text.Trim());
                            if (!string.IsNullOrEmpty(txtDocTp.Text))
                            {
                                for (int x = dt.Rows.Count - 1; x >= 0; x--)
                                {
                                    DataRow dr = dt.Rows[x];
                                    if (dr["DOC_TYPE"].ToString() == "OPERNING_BAL")
                                    {
                                        dr.Delete();
                                    }
                                }
                                dt.AcceptChanges();
                            }
                            // dt = MakeGridBalWithoutStatus(dt);
                            dt = CHNLSVC.MsgPortal.MakeGridBalWithoutStatus(dt);
                            gvDocumentDetails.DataSource = dt;
                            gvDocumentDetails.Columns[7].Visible = false;

                            if (!string.IsNullOrWhiteSpace(txtDocTp.Text))
                            {
                                gvDocumentDetails.Columns[10].Visible = false;
                            }
                            else
                            {
                                gvDocumentDetails.Columns[10].Visible = true;
                            }

                        }
                        int inCount = 0;
                        int outCount = 0;
                        for (int i = 0; i < gvDocumentDetails.Rows.Count; i++)
                        {
                            if (gvDocumentDetails.Rows[i].Cells[6].Value.ToString() != "OPERNING_BAL")
                            {
                                inCount = inCount + (Convert.ToInt32(gvDocumentDetails.Rows[i].Cells[8].Value));
                                outCount = outCount + (Convert.ToInt32(gvDocumentDetails.Rows[i].Cells[9].Value));
                            }
                        }
                        txtIn.Text = inCount.ToString();
                        txtOut.Text = outCount.ToString();
                        this.Cursor = Cursors.Default;
                        if (gvDocumentDetails.Rows.Count <= 0)
                        {
                            MessageBox.Show("No Data found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                else
                {
                    this.Cursor = Cursors.WaitCursor;
                    string locs = "";
                    gvSerialLocs.EndEdit();
                    foreach (DataGridViewRow row in gvSerialLocs.Rows)
                    {
                        DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                        if (Convert.ToBoolean(chk.Value))
                        {
                            locs = locs + row.Cells[1].Value + ",";
                        }
                    }

                    DateTime _dtFrom = Convert.ToDateTime(dateTimePickerFrom.Value.Date);
                    DateTime _dtTo = Convert.ToDateTime(dateTimePickerTo.Value.Date);
                    DateTime _maxDate = _dtFrom.AddMonths(6).AddDays(-1);
                    if (_dtTo > _maxDate)
                    {
                        MessageBox.Show("Maximum date range allowed is 6 months period !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvDocumentDetails.DataSource = null;
                        return;
                    }

                    if (locs == "")
                    {
                        MessageBox.Show("Please select Location from location grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvSerialDetails.DataSource = null;
                        return;
                    }
                    else if (txtSerialItemCode.Text == "")
                    {
                        MessageBox.Show("Please select Item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvSerialDetails.DataSource = null;
                        return;
                    }
                    else if (txtSerialCompany.Text == "")
                    {
                        MessageBox.Show("Please select company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        gvSerialDetails.DataSource = null;
                        return;
                    }
                    else
                    {
                        if (BaseCls.GlbUserComCode == "AST")
                        {
                            string _item = "";
                            //kapila 18/11/2013
                            if (txtSerialItemCode.Text.Length == 16)
                                _item = txtSerialItemCode.Text.Substring(1, 7);
                            else if (txtSerialItemCode.Text.Length == 15)
                                _item = txtSerialItemCode.Text.Substring(0, 7);
                            else if (txtSerialItemCode.Text.Length == 8)
                                _item = txtSerialItemCode.Text.Substring(1, 7);
                            else if (txtSerialItemCode.Text.Length == 20)
                                _item = txtSerialItemCode.Text.Substring(0, 12);
                            else
                                _item = txtSerialItemCode.Text;

                            txtSerialItemCode.Text = _item;
                        }

                        DataTable dt = CHNLSVC.MsgPortal.SerialBalanceSearch1(dateTimePickerSerialFrom.Value, dateTimePickerSerialTo.Value, txtSerialItemCode.Text, locs, txtSerialCompany.Text);
                        gvSerialDetails.DataSource = dt;

                        if (gvSerialDetails.Rows.Count <= 0)
                        {
                            MessageBox.Show("No Data found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                this.Cursor = Cursors.Default;

                //old code comented by kapila 21/2/2017
                //////txtLocation_Leave(null, null);
                //////if (tabControl1.SelectedIndex == 0)
                //////{
                //////    this.Cursor = Cursors.WaitCursor;
                //////    string locs = "";
                //////    gvSubLocation.EndEdit();
                //////    foreach (DataGridViewRow row in gvSubLocation.Rows)
                //////    {
                //////        DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                //////        if (Convert.ToBoolean(chk.Value))
                //////        {
                //////            locs = locs + row.Cells[1].Value + ",";
                //////        }
                //////    }
                //////    if (locs == "")
                //////    {
                //////        MessageBox.Show("Please select Location from location grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //////        gvDocumentDetails.DataSource = null;
                //////        return;
                //////    }
                //////    else if (txtItemCode.Text == "")
                //////    {
                //////        MessageBox.Show("Please select Item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //////        gvDocumentDetails.DataSource = null;
                //////        return;
                //////    }
                //////    else if (txtCompany.Text == "")
                //////    {
                //////        MessageBox.Show("Please select company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //////        gvDocumentDetails.DataSource = null;
                //////        return;
                //////    }
                //////    else
                //////    {
                //////        if (BaseCls.GlbUserComCode == "AST")
                //////        {
                //////            string _item = "";
                //////            //kapila 18/11/2013
                //////            if (txtItemCode.Text.Length == 16)
                //////                _item = txtItemCode.Text.Substring(1, 7);
                //////            else if (txtItemCode.Text.Length == 15)
                //////                _item = txtItemCode.Text.Substring(0, 7);
                //////            else if (txtItemCode.Text.Length == 8)
                //////                _item = txtItemCode.Text.Substring(1, 7);
                //////            else if (txtItemCode.Text.Length == 20)
                //////                _item = txtItemCode.Text.Substring(0, 12);
                //////            else
                //////                _item = txtItemCode.Text;

                //////            txtItemCode.Text = _item;
                //////        }
                //////        if (chkStatus.Checked)
                //////        {
                //////            DataTable dt = CHNLSVC.MsgPortal.StockBalanceSearch1(dateTimePickerFrom.Value.Date, dateTimePickerTo.Value.Date, txtItemCode.Text, locs, txtCompany.Text, true);

                //////            DataTable tem = dt.Clone();
                //////            tem.Columns.Add("id", typeof(int));
                //////            int i = 0;
                //////            foreach (DataRow dr in dt.Rows)
                //////            {

                //////                DataRow drT = tem.NewRow();
                //////                drT[0] = dr[0].ToString();
                //////                drT[1] = dr[1].ToString();
                //////                drT[2] = dr[2].ToString();
                //////                drT[3] = dr[3].ToString();
                //////                drT[4] = dr[4].ToString();
                //////                drT[5] = dr[5].ToString();
                //////                drT[6] = dr[6].ToString();
                //////                drT[7] = dr[7].ToString();
                //////                drT[8] = dr[8].ToString();
                //////                if (dr["Doc_Type"].ToString().ToUpper() == "OPERNING_BAL")
                //////                {
                //////                    drT[9] = dr[9].ToString();
                //////                }
                //////                else
                //////                {
                //////                    int cou;
                //////                    if (dr[7].ToString() != "0")
                //////                        cou = Convert.ToInt32(dr[7]);
                //////                    else
                //////                        cou = -1 * Convert.ToInt32(dr[8]);
                //////                    var _result = (from _res in tem.AsEnumerable()
                //////                                   //group _res by new { column1 = _res[0], column2 = _res[10], Column3 = _res[9], Column4 = _res[2] } into _res1
                //////                                   where _res.Field<string>(10) == dr[10].ToString()
                //////                                   orderby _res.Field<int>(11) descending
                //////                                   select _res).FirstOrDefault();
                //////                    if (_result != null)
                //////                        drT[9] = (Convert.ToInt32(_result[9].ToString()) + cou).ToString();
                //////                    else
                //////                        drT[9] = cou;
                //////                }
                //////                drT[10] = dr[10].ToString();
                //////                drT[11] = i++;
                //////                tem.Rows.Add(drT);
                //////            }

                //////            gvDocumentDetails.DataSource = tem;
                //////            gvDocumentDetails.Columns[7].Visible = true;

                //////        }
                //////        else
                //////        {
                //////            DataTable dt = CHNLSVC.MsgPortal.StockBalanceSearch1(dateTimePickerFrom.Value.Date, dateTimePickerTo.Value.Date, txtItemCode.Text, locs, txtCompany.Text, false);
                //////            gvDocumentDetails.DataSource = dt;
                //////            gvDocumentDetails.Columns[7].Visible = false;
                //////        }
                //////        int inCount = 0;
                //////        int outCount = 0;
                //////        for (int i = 0; i < gvDocumentDetails.Rows.Count; i++)
                //////        {
                //////            if (gvDocumentDetails.Rows[i].Cells[6].Value.ToString() != "OPERNING_BAL")
                //////            {
                //////                inCount = inCount + (Convert.ToInt32(gvDocumentDetails.Rows[i].Cells[8].Value));
                //////                outCount = outCount + (Convert.ToInt32(gvDocumentDetails.Rows[i].Cells[9].Value));
                //////            }
                //////        }
                //////        txtIn.Text = inCount.ToString();
                //////        txtOut.Text = outCount.ToString();
                //////        this.Cursor = Cursors.Default;
                //////        if (gvDocumentDetails.Rows.Count <= 0)
                //////        {
                //////            MessageBox.Show("No Data found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //////            return;
                //////        }
                //////    }
                //////}
                //////else
                //////{
                //////    this.Cursor = Cursors.WaitCursor;
                //////    string locs = "";
                //////    gvSerialLocs.EndEdit();
                //////    foreach (DataGridViewRow row in gvSerialLocs.Rows)
                //////    {
                //////        DataGridViewCheckBoxCell chk = row.Cells[0] as DataGridViewCheckBoxCell;
                //////        if (Convert.ToBoolean(chk.Value))
                //////        {
                //////            locs = locs + row.Cells[1].Value + ",";
                //////        }
                //////    }
                //////    if (locs == "")
                //////    {
                //////        MessageBox.Show("Please select Location from location grid", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //////        gvSerialDetails.DataSource = null;
                //////        return;
                //////    }
                //////    else if (txtSerialItemCode.Text == "")
                //////    {
                //////        MessageBox.Show("Please select Item", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //////        gvSerialDetails.DataSource = null;
                //////        return;
                //////    }
                //////    else if (txtSerialCompany.Text == "")
                //////    {
                //////        MessageBox.Show("Please select company", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //////        gvSerialDetails.DataSource = null;
                //////        return;
                //////    }
                //////    else
                //////    {
                //////        if (BaseCls.GlbUserComCode == "AST")
                //////        {
                //////            string _item = "";
                //////            //kapila 18/11/2013
                //////            if (txtSerialItemCode.Text.Length == 16)
                //////                _item = txtSerialItemCode.Text.Substring(1, 7);
                //////            else if (txtSerialItemCode.Text.Length == 15)
                //////                _item = txtSerialItemCode.Text.Substring(0, 7);
                //////            else if (txtSerialItemCode.Text.Length == 8)
                //////                _item = txtSerialItemCode.Text.Substring(1, 7);
                //////            else if (txtSerialItemCode.Text.Length == 20)
                //////                _item = txtSerialItemCode.Text.Substring(0, 12);
                //////            else
                //////                _item = txtSerialItemCode.Text;

                //////            txtSerialItemCode.Text = _item;
                //////        }

                //////        DataTable dt = CHNLSVC.MsgPortal.SerialBalanceSearch1(dateTimePickerSerialFrom.Value, dateTimePickerSerialTo.Value, txtSerialItemCode.Text, locs, txtSerialCompany.Text);
                //////        gvSerialDetails.DataSource = dt;

                //////        if (gvSerialDetails.Rows.Count <= 0)
                //////        {
                //////            MessageBox.Show("No Data found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //////            return;
                //////        }
                //////    }
                //////}
                //////this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occurred while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }

            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }

        }

        #endregion

        private DataTable GetStockLedgerData(string _userr, string _chnl, string _brand, string _model, string _item, Int32 _itemSts, string _itmCat1, string _itmCat2, string _itmCat3,
            Int32 _withCost, DateTime _asAtDate, Int32 _withSer, Int32 _status, string _com, string _loc,
            DateTime _dtFrom, DateTime _dtTo, Int32 isStatus, string _doctype = null)
        {
            DataTable _dtOpenBal = new DataTable();
            DataTable _dtAllOpenBal = new DataTable();

            #region Create New Table
            DataTable _dtNew = new DataTable("tbl1");
            _dtNew.Columns.Add(new DataColumn("location"));
            _dtNew.Columns.Add(new DataColumn("other_loc"));
            _dtNew.Columns.Add(new DataColumn("doc_date"));
            _dtNew.Columns.Add(new DataColumn("doc_no"));
            _dtNew.Columns.Add(new DataColumn("other_doc"));
            _dtNew.Columns.Add(new DataColumn("man_ref"));
            _dtNew.Columns.Add(new DataColumn("doc_type"));
            _dtNew.Columns.Add(new DataColumn("in_cou"));
            _dtNew.Columns.Add(new DataColumn("out_cou"));
            _dtNew.Columns.Add(new DataColumn("balance"));
            _dtNew.Columns.Add(new DataColumn("status"));
            _dtNew.Columns.Add(new DataColumn("seq_no"));

            DataTable _dtNewData = new DataTable("tbl2");
            _dtNewData.Columns.Add(new DataColumn("location"));
            _dtNewData.Columns.Add(new DataColumn("other_loc"));
            _dtNewData.Columns.Add(new DataColumn("doc_date"));
            _dtNewData.Columns.Add(new DataColumn("doc_no"));
            _dtNewData.Columns.Add(new DataColumn("other_doc"));
            _dtNewData.Columns.Add(new DataColumn("man_ref"));
            _dtNewData.Columns.Add(new DataColumn("doc_type"));
            _dtNewData.Columns.Add(new DataColumn("in_cou"));
            _dtNewData.Columns.Add(new DataColumn("out_cou"));
            _dtNewData.Columns.Add(new DataColumn("balance"));
            _dtNewData.Columns.Add(new DataColumn("status"));
            _dtNewData.Columns.Add(new DataColumn("seq_no"));
            #endregion

            #region Get Open Bal
            string[] seperator = new string[] { "," };
            string[] searchParams = _loc.Split(seperator, StringSplitOptions.None);
            _asAtDate = _asAtDate.AddDays(-1);
            for (int i = 0; i < searchParams.Length; i++)
            {
                if (!string.IsNullOrEmpty(searchParams[i]))
                {
                    _dtOpenBal = CHNLSVC.MsgPortal.GetInventoryBalanceAsAt(_userr, _chnl, _brand, _model, _item, isStatus == 1 ? true : false, _itmCat1, _itmCat2, _itmCat3,
                        _withCost, _asAtDate, _withSer, _status, _com, searchParams[i]);
                    _dtAllOpenBal.Merge(_dtOpenBal);
                }
            }
            #endregion
            #region Genarate Open Bal
            List<MasterItemStatus> _statusList = CHNLSVC.General.GetAllStockTypes(_com);
            if (_dtAllOpenBal.Rows.Count > 0)
            {
                foreach (DataRow dr in _dtAllOpenBal.Rows)
                {
                    DataRow _newDr = _dtNewData.NewRow();
                    _newDr["LOCATION"] = dr["LOC_CODE"].ToString();
                    _newDr["OTHER_LOC"] = "";
                    _newDr["DOC_DATE"] = _dtFrom.ToString();
                    _newDr["DOC_NO"] = "";
                    _newDr["OTHER_DOC"] = "";
                    _newDr["MAN_REF"] = "";
                    _newDr["DOC_TYPE"] = "OPERNING_BAL";
                    _newDr["IN_COU"] = "0";
                    _newDr["OUT_COU"] = "0";
                    _newDr["BALANCE"] = Convert.ToDecimal(dr["QTY"].ToString());
                    _newDr["STATUS"] = "";//dr["ITEM_STATUS"].ToString(); 
                    var v = _statusList.Where(c => c.Mis_desc == dr["ITEM_STATUS"].ToString()).FirstOrDefault();
                    if (v != null)
                    {
                        _newDr["STATUS"] = v.Mis_cd;
                    }
                    _newDr["SEQ_NO"] = "0";
                    if (isStatus == 1)
                    {
                        _dtNewData.Rows.Add(_newDr);
                    }
                    if (isStatus == 0)
                    {
                        if (_dtNewData.Rows.Count == 0)
                        {
                            _dtNewData.Rows.Add(_newDr);
                        }
                        else
                        {
                            decimal _tmpQty = 0, _oldBal = 0, _curBal = 0, _newBal = 0;
                            _oldBal = decimal.TryParse(_dtNewData.Rows[0]["BALANCE"].ToString(), out _tmpQty) ? Convert.ToDecimal(_dtNewData.Rows[0]["BALANCE"].ToString()) : 0;
                            _curBal = decimal.TryParse(dr["QTY"].ToString(), out _tmpQty) ? Convert.ToDecimal(dr["QTY"].ToString()) : 0;
                            _newBal = _oldBal + _curBal;
                            _dtNewData.Rows[0]["BALANCE"] = _newBal;
                        }
                    }
                }
            }
            #endregion

            #region Get Trans Action Data
            DataTable _dtTrans = CHNLSVC.MsgPortal.StockBalanceSearchNew(_dtFrom, _dtTo, _item, _loc, _com, isStatus == 1 ? true : false, _doctype);
            for (int x = 0; x < _dtTrans.Rows.Count; x++)
            {
                DataRow dr = _dtTrans.Rows[x];
                if (dr["DOC_TYPE"].ToString() != "OPERNING_BAL")
                {
                    DataRow _newDr = _dtNewData.NewRow();
                    _newDr["location"] = dr["location"].ToString();
                    _newDr["other_loc"] = dr["other_loc"].ToString();
                    _newDr["doc_date"] = dr["doc_date"].ToString();
                    _newDr["doc_no"] = dr["doc_no"].ToString();
                    _newDr["other_doc"] = dr["other_doc"].ToString();
                    _newDr["man_ref"] = dr["man_ref"].ToString();
                    _newDr["doc_type"] = dr["doc_type"].ToString();
                    _newDr["in_cou"] = dr["in_cou"].ToString();
                    _newDr["out_cou"] = dr["out_cou"].ToString();
                    _newDr["balance"] = dr["balance"].ToString();
                    _newDr["status"] = dr["status"].ToString();
                    _newDr["seq_no"] = dr["seq_no"].ToString();
                    _dtNewData.Rows.Add(_newDr);
                }
            }
            #endregion
            return _dtNewData;
        }

        #region validation and loading events

        /// <summary>
        /// Validate and load location code
        /// </summary>
        private bool ValidateAndLoadLocation(string com, string loc, Label lbl)
        {
            try
            {
                MasterLocation location = CHNLSVC.General.GetLocationByLocCode(com.ToUpper(), loc.ToUpper());
                if (location != null)
                {
                    lbl.Text = location.Ml_loc_desc;
                    return true;
                }
                else
                {
                    lbl.Text = "";
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
                return true;
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
                
            }
        }

        /// <summary>
        /// Validate and load item code
        /// </summary>
        private bool ValidateAndLoadItemCode(string code, string com, Label lbl,Label lbl1)
        {
            try
            {
                MasterItem item = CHNLSVC.Inventory.GetItem(com.ToUpper(), code.ToUpper());
                if (item != null && item.Mi_cd != null)
                {
                    lbl.Text = item.Mi_shortdesc;
                    lbl1.Text = "Brand- " + item.Mi_brand + " " + "Model- " + item.Mi_model;
                    return true;
                }
                else
                {
                    lbl.Text = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return false;
            }
        }

        /// <summary>
        /// validate and load company code
        /// </summary>
        private bool ValidateAndLoadCompany(string code, Label lbl)
        {
            try
            {
                MasterCompany company = CHNLSVC.General.GetCompByCode(code.ToUpper());
                if (company != null)
                {
                    lbl.Text = company.Mc_desc;
                    return true;
                }
                else
                {
                    lbl.Text = "";
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while processing\n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return false;
            }
        }

        #endregion

        #region search

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        paramsText.Append(BaseCls.GlbUserID + seperator + txtCompany.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        paramsText.Append(txtCompany.Text + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void btnSearchItem_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItemCode;
                _CommonSearch.ShowDialog();
                txtItemCode.Select();
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

        private void btnSearchCompany_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtCompany;
                _CommonSearch.ShowDialog();
                txtCompany.Select();
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

        private void btnSearchLocation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtLocation;
                _CommonSearch.ShowDialog();
                txtLocation.Select();
                if (txtLocation.Text != "")
                {
                    DataTable _loc = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, txtLocation.Text);
                    if (_loc!=null && _loc.Rows.Count>0) {
                        //if (_loc.Rows[0]["ml_scm2_st_dt"].ToString() != "")
                        //    minDate = Convert.ToDateTime(_loc.Rows[0]["ml_scm2_st_dt"]);
                        //else
                            minDate = DateTime.MinValue;
                    }
                    if (ValidateAndLoadLocation(txtCompany.Text, txtLocation.Text, lblLocation))
                    {
                        LoadSubLocation(txtCompany.Text, txtLocation.Text, gvSubLocation);
                    }
                }
            }
            catch (Exception ex)
            {
                minDate = DateTime.MinValue;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSerialLocationSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerialLocation;
                _CommonSearch.ShowDialog();
                txtSerialLocation.Select();
                if (txtSerialLocation.Text != "")
                {
                    DataTable _loc = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, txtLocation.Text);
                    if (_loc != null && _loc.Rows.Count > 0)
                    {
                        if (_loc.Rows[0]["ml_scm2_st_dt"].ToString() != "")
                            minDate = Convert.ToDateTime(_loc.Rows[0]["ml_scm2_st_dt"]);
                        else
                            minDate = DateTime.MinValue;
                    }
                    if (ValidateAndLoadLocation(txtSerialCompany.Text, txtSerialLocation.Text, lblLocation))
                    {
                        LoadSubLocation(txtSerialCompany.Text, txtSerialLocation.Text, gvSerialLocs);
                    }
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

        private void btnSerialItemSearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerialItemCode;
                _CommonSearch.ShowDialog();
                txtSerialItemCode.Select();
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

        private void btnSerialCompanySearch_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Company);
                DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerialCompany;
                _CommonSearch.ShowDialog();
                txtSerialCompany.Select();
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

        #endregion

        #region key down events

        private void txtLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtLocation.Text != "")
                    {
                        if (!ValidateAndLoadLocation(txtCompany.Text, txtLocation.Text, lblLocation))
                        {
                            MessageBox.Show("Invalid Location Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtLocation.Text = "";
                        }
                        else
                        {
                            LoadSubLocation(txtCompany.Text, txtLocation.Text, gvSubLocation);
                            dateTimePickerFrom.Focus();
                        }
                    }
                }
                if (e.KeyCode == Keys.F2)
                {
                    btnSearchLocation_Click(null, null);
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

        private void txtCompany_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtCompany.Text != "")
                    {
                        if (!ValidateAndLoadCompany(txtCompany.Text, lblCompany))
                        {
                            MessageBox.Show("Invalid Company Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtCompany.Text = "";
                        }
                        else
                        {
                            txtLocation.Focus();
                        }
                    }
                }
                if (e.KeyCode == Keys.F2)
                {
                    btnSearchCompany_Click(null, null);
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

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtItemCode.Text != "")
                    {
                        if (BaseCls.GlbUserComCode == "AST")
                        {
                            string _item = "";
                            //kapila 18/11/2013
                            if (txtItemCode.Text.Length == 16)
                                _item = txtItemCode.Text.Substring(1, 7);
                            else if (txtItemCode.Text.Length == 15)
                                _item = txtItemCode.Text.Substring(0, 7);
                            else if (txtItemCode.Text.Length == 8)
                                _item = txtItemCode.Text.Substring(1, 7);
                            else if (txtItemCode.Text.Length == 20)
                                _item = txtItemCode.Text.Substring(0, 12);
                            else
                                _item = txtItemCode.Text;

                            txtItemCode.Text = _item;
                        }

                        if (!ValidateAndLoadItemCode(txtItemCode.Text, txtCompany.Text, lblDesc, lblItemDes))
                        {
                            MessageBox.Show("Invalid Item Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtItemCode.Text = "";
                        }
                        else
                            txtCompany.Focus();
                    }
                }
                if (e.KeyCode == Keys.F2)
                {
                    btnSearchItem_Click(null, null);
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

        private void dateTimePickerFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerTo.Focus();
            }
        }

        private void dateTimePickerTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{TAB}");
            }
        }

        private void chkStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnView.Select();
            }
        }

        private void txtSerialItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtSerialItemCode.Text != "")
                    {
                        if (!ValidateAndLoadItemCode(txtSerialItemCode.Text, txtSerialCompany.Text, lblSerialItem, lblISerialtemDes))
                        {
                            MessageBox.Show("Invalid Item Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtSerialItemCode.Text = "";
                        }
                        else
                        {
                            txtSerialCompany.Focus();
                        }
                    }
                }
                if (e.KeyCode == Keys.F2)
                {
                    btnSerialItemSearch_Click(null, null);
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

        private void txtSerialCompany_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtSerialCompany.Text != "")
                    {
                        if (!ValidateAndLoadCompany(txtSerialCompany.Text, lblSerialCompany))
                        {
                            MessageBox.Show("Invalid Item Code", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtSerialCompany.Text = "";
                        }
                        else
                            txtSerialLocation.Focus();
                    }
                }
                if (e.KeyCode == Keys.F2)
                {
                    btnSerialCompanySearch_Click(null, null);
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

        private void txtSerialLocation_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtSerialLocation.Text != "")
                    {
                        if (!ValidateAndLoadLocation(txtSerialLocation.Text, txtSerialLocation.Text, lblSerialLoc))
                        {
                            txtSerialLocation.Text = "";
                        }
                        else
                        {
                            LoadSubLocation(txtSerialCompany.Text, txtSerialLocation.Text, gvSerialLocs);
                            dateTimePickerSerialFrom.Focus();
                        }
                    }
                }
                if (e.KeyCode == Keys.F2)
                {
                    btnSerialLocationSearch_Click(null, null);
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

        private void dateTimePickerSerialFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dateTimePickerSerialTo.Focus();
            }
        }

        private void dateTimePickerSerialTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                toolStrip1.Focus();
                btnView.Select();
            }
        }

        #endregion

        #region textbox leave events

        private void txtItemCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtItemCode.Text != "")
                {
                    if (BaseCls.GlbUserComCode == "AST")
                    {
                        string _item = "";
                        //kapila 18/11/2013
                        if (txtItemCode.Text.Length == 16)
                            _item = txtItemCode.Text.Substring(1, 7);
                        else if (txtItemCode.Text.Length == 15)
                            _item = txtItemCode.Text.Substring(0, 7);
                        else if (txtItemCode.Text.Length == 8)
                            _item = txtItemCode.Text.Substring(1, 7);
                        else if (txtItemCode.Text.Length == 20)
                            _item = txtItemCode.Text.Substring(0, 12);
                        else
                            _item = txtItemCode.Text;

                        txtItemCode.Text = _item;
                    }
                    if (!ValidateAndLoadItemCode(txtItemCode.Text, txtCompany.Text, lblDesc, lblItemDes))
                    {
                        txtItemCode.Text = "";
                    }
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

        private void txtCompany_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtCompany.Text != "")
                {
                    if (!ValidateAndLoadCompany(txtCompany.Text, lblCompany))
                    {
                        txtCompany.Text = BaseCls.GlbUserComCode;
                    }
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

        private void txtLocation_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtLocation.Text != "")
                {
                    DataTable _loc = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, txtLocation.Text);
                    if (_loc != null && _loc.Rows.Count > 0)
                    {
                        if (_loc.Rows[0]["ml_scm2_st_dt"].ToString() != "")
                            minDate = Convert.ToDateTime(_loc.Rows[0]["ml_scm2_st_dt"]);
                        else
                            minDate = DateTime.MinValue;
                    }
                    if (!ValidateAndLoadLocation(txtCompany.Text, txtLocation.Text, lblLocation))
                    {
                        txtLocation.Text = "";
                    }
                    else
                        LoadSubLocation(txtCompany.Text, txtLocation.Text, gvSubLocation);
                }
            }
            catch (Exception ex)
            {
                minDate = DateTime.MinValue;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtSerialItemCode_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSerialItemCode.Text != "")
                {
                    if (!ValidateAndLoadItemCode(txtSerialItemCode.Text, txtSerialCompany.Text, lblSerialItem, lblISerialtemDes))
                    {
                        txtSerialItemCode.Text = "";
                    }
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

        private void txtSerialCompany_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSerialCompany.Text != "")
                {
                    if (!ValidateAndLoadCompany(txtSerialCompany.Text, lblSerialCompany))
                    {
                        txtSerialCompany.Text = BaseCls.GlbUserComCode;
                    }
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

        private void txtSerialLocation_Leave(object sender, EventArgs e)
        {
            try
            {
                if (txtSerialLocation.Text != "")
                {
                    DataTable _loc = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, txtSerialLocation.Text);
                    if (_loc != null && _loc.Rows.Count > 0)
                    {
                        if (_loc.Rows[0]["ml_scm2_st_dt"].ToString() != "")
                        minDate = Convert.ToDateTime(_loc.Rows[0]["ml_scm2_st_dt"]);
                        else
                            minDate = DateTime.MinValue;
                    }
                    if (!ValidateAndLoadLocation(txtSerialCompany.Text, txtSerialLocation.Text, lblSerialLoc))
                    {
                        txtSerialLocation.Text = "";
                    }
                    else
                        LoadSubLocation(txtSerialCompany.Text, txtSerialLocation.Text, gvSerialLocs);
                }
            }
            catch (Exception ex)
            {
                minDate = DateTime.MinValue;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void chkSerialSubLocAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (chkSerialSubLocAll.Checked)
                {
                    foreach (DataGridViewRow row in gvSerialLocs.Rows)
                    {
                        row.Cells[0].Value = true;
                    }
                }
                else
                {
                    foreach (DataGridViewRow row in gvSerialLocs.Rows)
                    {
                        row.Cells[0].Value = false;
                    }
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

        #endregion

        #region mouse double click

        private void txtItemCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchItem_Click(null, null);
        }

        private void txtCompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchCompany_Click(null, null);
        }

        private void txtLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSearchLocation_Click(null, null);
        }

        private void txtSerialItemCode_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSerialItemSearch_Click(null, null);
        }

        private void txtSerialCompany_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSerialCompanySearch_Click(null, null);
        }

        private void txtSerialLocation_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSerialLocationSearch_Click(null, null);
        }

        #endregion

        private void StockLedger_Shown(object sender, EventArgs e)
        {
            try
            {
                LoadSubLocation(txtCompany.Text, txtLocation.Text, gvSubLocation);
                LoadSubLocation(txtSerialCompany.Text, txtSerialLocation.Text, gvSerialLocs);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);CHNLSVC.CloseChannel(); 
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
            //ClearAll();
        }

        private void dateTimePickerSerialFrom_ValueChanged(object sender, EventArgs e)
        {
            //if (dateTimePickerSerialFrom.Value < minDate) {
            //    MessageBox.Show("Location minimum from date is " + minDate.ToString("dd/MMM/yyyy"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    dateTimePickerSerialFrom.Value = minDate;
            //    return;
            //}
        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {

        }

        private DataTable MakeGridBalWithStatus(DataTable dt)
        {
            DataTable _dtNew = new DataTable();
            try
            {
                #region MakeDataTable
                _dtNew.Columns.Add("location", typeof(string));
                _dtNew.Columns.Add("other_loc", typeof(string));
                _dtNew.Columns.Add("doc_date", typeof(DateTime));
                _dtNew.Columns.Add("doc_no", typeof(string));
                _dtNew.Columns.Add("other_doc", typeof(string));
                _dtNew.Columns.Add("man_ref", typeof(string));
                _dtNew.Columns.Add("doc_type", typeof(string));
                _dtNew.Columns.Add("in_cou", typeof(Int32));
                _dtNew.Columns.Add("out_cou", typeof(Int32));
                _dtNew.Columns.Add("balance", typeof(Int32));
                _dtNew.Columns.Add("status", typeof(string));
                #endregion
                DataRow _dr;
                Int32 _openBal = 0;
                Int32 _in = 0, _out = 0, _bal = 0;
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        List<OpenBalTemp> balList = new List<OpenBalTemp>();

                        for (int x = dt.Rows.Count - 1; x >= 0; x--)
                        {
                            DataRow row = dt.Rows[x];
                            if (row["DOC_TYPE"].ToString() == "OPERNING_BAL")
                            {
                                _dr = _dtNew.NewRow();
                                _dr["location"] = row["location"].ToString();
                                _dr["other_loc"] = row["other_loc"].ToString();
                                _dr["doc_date"] = Convert.ToDateTime(row["doc_date"].ToString());
                                _dr["doc_no"] = row["doc_no"].ToString();
                                _dr["other_doc"] = row["other_doc"].ToString();
                                _dr["man_ref"] = row["man_ref"].ToString();
                                _dr["doc_type"] = row["doc_type"].ToString();
                                _dr["in_cou"] = Convert.ToInt32(row["in_cou"].ToString());
                                _dr["out_cou"] = Convert.ToInt32(row["out_cou"].ToString());

                                _in = Int32.TryParse(row["BALANCE"].ToString(), out _in) ? Convert.ToInt32(row["BALANCE"].ToString()) : _in;
                                //_out = Int32.TryParse(row["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(row["OUT_COU"].ToString()) : _out;
                                _openBal = _in - _out;

                                _dr["balance"] = _in.ToString();
                                _dr["status"] = row["status"].ToString();

                                OpenBalTemp tmp = new OpenBalTemp();
                                tmp.Status = _dr["status"].ToString();
                                tmp.OpenBal = Convert.ToInt32(_dr["BALANCE"].ToString());
                                balList.Add(tmp);

                                _dtNew.Rows.Add(_dr);
                                row.Delete();
                            }
                        }
                        dt.AcceptChanges();
                        if (dt.Rows.Count > 0)
                        {
                            DataView dv = dt.DefaultView;
                            dv.Sort = "seq_no";
                            dt = dv.ToTable();
                        }

                        foreach (DataRow item in dt.Rows)
                        {
                            _dr = _dtNew.NewRow();
                            _dr["location"] = item["location"].ToString();
                            _dr["other_loc"] = item["other_loc"].ToString();
                            _dr["doc_date"] = Convert.ToDateTime(item["doc_date"].ToString());
                            _dr["doc_no"] = item["doc_no"].ToString();
                            _dr["other_doc"] = item["other_doc"].ToString();
                            _dr["man_ref"] = item["man_ref"].ToString();
                            _dr["doc_type"] = item["doc_type"].ToString();
                            _dr["in_cou"] = Convert.ToInt32(item["in_cou"].ToString());
                            _dr["out_cou"] = Convert.ToInt32(item["out_cou"].ToString());

                            string _status = item["status"].ToString();
                            var sts = balList.Where(c => c.Status == _status).FirstOrDefault();
                            if (sts != null)
                            {
                                _in = Int32.TryParse(item["IN_COU"].ToString(), out _in) ? Convert.ToInt32(item["IN_COU"].ToString()) : _in;
                                _out = Int32.TryParse(item["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(item["OUT_COU"].ToString()) : _out;
                                sts.OpenBal = sts.OpenBal + _in - _out;
                                _dr["BALANCE"] = sts.OpenBal.ToString();
                            }
                            else
                            {
                                OpenBalTemp tmp = new OpenBalTemp();
                                tmp.Status = item["status"].ToString();
                                tmp.OpenBal = Convert.ToInt32(item["BALANCE"].ToString());
                                balList.Add(tmp);
                            }
                            _dr["status"] = item["status"].ToString();
                            _dtNew.Rows.Add(_dr);
                        }
                    }
                }
                #region sum grid data
                /* DataTable _dtTemp = new DataTable();
                _dtTemp.Columns.Add("location", typeof(string));
                _dtTemp.Columns.Add("other_loc", typeof(string));
                _dtTemp.Columns.Add("doc_date", typeof(DateTime));
                _dtTemp.Columns.Add("doc_no", typeof(string));
                _dtTemp.Columns.Add("other_doc", typeof(string));
                _dtTemp.Columns.Add("man_ref", typeof(string));
                _dtTemp.Columns.Add("doc_type", typeof(string));
                _dtTemp.Columns.Add("in_cou", typeof(Int32));
                _dtTemp.Columns.Add("out_cou", typeof(Int32));
                _dtTemp.Columns.Add("balance", typeof(Int32));
                _dtTemp.Columns.Add("status", typeof(string));
                foreach (DataRow item in _dtNew.Rows)
                {
                    bool _ifAva = false;
                    foreach (DataRow rw in _dtNew.Rows)
                    {
                        if (rw["location"].ToString() == item["location"].ToString()
                            && rw["other_loc"].ToString() == item["other_loc"].ToString()
                            && Convert.ToDateTime(rw["doc_date"].ToString()) == Convert.ToDateTime(item["doc_date"].ToString())
                            && rw["doc_no"].ToString() == item["doc_no"].ToString()
                            && rw["other_doc"].ToString() == item["other_doc"].ToString()
                            && rw["man_ref"].ToString() == item["man_ref"].ToString()
                            && rw["doc_type"].ToString() == item["doc_type"].ToString()
                            && rw["status"].ToString() == item["status"].ToString()
                            && rw["DOC_TYPE"].ToString() != "OPERNING_BAL"
                            )
                        {
                            rw["in_cou"] = Convert.ToInt32(rw["in_cou"].ToString()) + Convert.ToInt32(item["in_cou"].ToString());
                            rw["out_cou"] = Convert.ToInt32(rw["out_cou"].ToString()) + Convert.ToInt32(item["out_cou"].ToString());
                            _in = Int32.TryParse(item["IN_COU"].ToString(), out _in) ? Convert.ToInt32(item["IN_COU"].ToString()) : _in;
                            _out = Int32.TryParse(item["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(item["OUT_COU"].ToString()) : _out;
                            _bal = _bal + _in - _out;
                            rw["BALANCE"] = _bal.ToString();
                            _ifAva = true;
                        }
                    }
                    if (!_ifAva)
                    {
                        //_dr["in_cou"] = Convert.ToInt32(item["in_cou"].ToString());
                        //_dr["out_cou"] = Convert.ToInt32(item["out_cou"].ToString());
                        //_in = Int32.TryParse(item["IN_COU"].ToString(), out _in) ? Convert.ToInt32(item["IN_COU"].ToString()) : _in;
                        //_out = Int32.TryParse(item["OUT_COU"].ToString(), out _out) ? Convert.ToInt32(item["OUT_COU"].ToString()) : _out;
                        //_bal = _bal + _in - _out;

                        //item["BALANCE"] = _bal.ToString();
                        //_dr["BALANCE"] = _bal.ToString();
                        //_dr["status"] = item["status"].ToString();
                        //_dtNew.Rows.Add(_dr);
                    }
                }*/
                #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : Grid Balance", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return _dtNew;
        }

    }

    public class OpenBalTemp
    {
        public string Status { get; set; }
        public int OpenBal { get; set; }
    }
}
