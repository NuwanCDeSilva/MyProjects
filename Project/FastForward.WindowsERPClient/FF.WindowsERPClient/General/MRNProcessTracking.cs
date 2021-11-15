using System;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FF.WindowsERPClient.General
{
    public partial class MRNProcessTracking : Base
    {
        public MRNProcessTracking()
        {
            InitializeComponent();
        }

        #region common search

        private void btnChannel_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtChanel;
                _CommonSearch.ShowDialog();
                txtChanel.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSubChannel_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChanel.Text))
                {
                    MessageBox.Show("Please select channel.", "Document Tracking", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSChanel.Text = "";
                    txtChanel.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSChanel;
                _CommonSearch.ShowDialog();
                txtSChanel.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnPc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);

                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                txtPC.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnMRN_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MRN_AllLoc);
                DataTable _result = CHNLSVC.CommonSearch.GetSearchMRN_AllLoc(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtMRNNo;
                _CommonSearch.ShowDialog();
                txtMRNNo.Select();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtChanel.Text + seperator + txtSChanel.Text + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MRN_AllLoc:
                    {
                        if (grpPC.Enabled && lstPC.Items.Count > 0)
                        {
                            string locList = "";
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Checked)
                                {
                                    locList = locList + "," + Item.Text;
                                }
                            }
                            if (locList != "")
                            {
                                paramsText.Append(BaseCls.GlbUserComCode + seperator + locList + seperator);
                            }
                            else
                            {
                                paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                            }
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocProInvoiceNo:
                    {
                        if (grpPC.Enabled && lstPC.Items.Count > 0)
                        {
                            string pcList = "";
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Checked)
                                {
                                    pcList = pcList + "," + Item.Text;
                                }
                            }
                            if (pcList != "")
                            {
                                paramsText.Append(pcList + seperator + BaseCls.GlbUserComCode);
                            }
                            else
                            {
                                paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);
                            }
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocProEngine:
                    {
                        if (grpPC.Enabled && lstPC.Items.Count > 0)
                        {
                            string pcList = "";
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Checked)
                                {
                                    pcList = pcList + "," + Item.Text;
                                }
                            }
                            if (pcList != "")
                            {
                                paramsText.Append(pcList + seperator + BaseCls.GlbUserComCode);
                            }
                            else
                            {
                                paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);
                            }
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocProChassis:
                    {
                        if (grpPC.Enabled && lstPC.Items.Count > 0)
                        {
                            string pcList = "";
                            foreach (ListViewItem Item in lstPC.Items)
                            {
                                if (Item.Checked)
                                {
                                    pcList = pcList + "," + Item.Text;
                                }
                            }
                            if (pcList != "")
                            {
                                paramsText.Append(pcList + seperator + BaseCls.GlbUserComCode);
                            }
                            else
                            {
                                paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);
                            }
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserDefLoca + seperator + BaseCls.GlbUserComCode);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Main.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        paramsText.Append(TextBoxMain.Text.ToUpper() + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        paramsText.Append(TextBoxMain.Text.ToUpper() + seperator + TextBoxSub.Text.ToUpper() + seperator + CommonUIDefiniton.SearchUserControlType.CAT_Sub1.ToString() + seperator + "" + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        #endregion common search

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            try
            {
                //lstPC.Clear();
                DataTable dt = CHNLSVC.Inventory.GetLOC_from_Hierachy(BaseCls.GlbUserComCode, txtChanel.Text, txtSChanel.Text, null, null, null, txtPC.Text);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["LOCATION"].ToString());
                }
                if (dt.Rows.Count <= 0)
                {
                    MessageBox.Show("No profit centers found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                txtChanel.Text = "";
                txtSChanel.Text = "";
                txtPC.Text = "";
                txtPC.Focus();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = true;
            }
        }

        private void btnNone_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in lstPC.Items)
            {
                Item.Checked = false;
            }
        }

        private void btnPcClear_Click(object sender, EventArgs e)
        {
            txtChanel.Text = "";
            txtSChanel.Text = "";
            txtPC.Text = "";
            lstPC.Clear();
            txtChanel.Focus();
        }

        private void MRNProcessTracking_Load(object sender, EventArgs e)
        {
            try
            {
                txtMRNNo.Focus();
                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "SCM2I"))
                {
                    grpPC.Enabled = true;
                }
                else
                    grpPC.Enabled = false;

                dtFrom.Value = DateTime.Now.AddMonths(-1).Date;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                while (this.Controls.Count > 0)
                {
                    Controls[0].Dispose();
                }
                InitializeComponent();

                if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, "SCM2I"))
                {
                    grpPC.Enabled = true;
                }
                else
                    grpPC.Enabled = false;

                dtFrom.Value = DateTime.Now.AddMonths(-1).Date;

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable _result = new DataTable();
                string mrnNo = (txtMRNNo.Text != "") ? mrnNo = txtMRNNo.Text : null;

                if (grpPC.Enabled && lstPC.Items.Count > 0)
                {
                    string locList = "";
                    foreach (ListViewItem Item in lstPC.Items)
                    {
                        if (Item.Checked)
                        {
                            locList = locList + "," + Item.Text;
                        }
                    }
                    if (locList != "")
                    {
                        _result = CHNLSVC.Inventory.GetMRNProcessTracking(mrnNo, dtFrom.Value.Date, dtTo.Value.Date, locList, BaseCls.GlbUserComCode);
                    }
                    else
                    {
                        _result = CHNLSVC.Inventory.GetMRNProcessTracking(mrnNo, dtFrom.Value.Date, dtTo.Value.Date, BaseCls.GlbUserDefLoca, BaseCls.GlbUserComCode);
                    }
                }
                else
                {
                    _result = CHNLSVC.Inventory.GetMRNProcessTracking(mrnNo, dtFrom.Value.Date, dtTo.Value.Date, BaseCls.GlbUserDefLoca, BaseCls.GlbUserComCode);
                }
                if (_result.Rows.Count <= 0)
                {
                    MessageBox.Show("No Data Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                grvMRNHeader.AutoGenerateColumns = false;
                grvMRNHeader.DataSource = _result;
                grvMRNItem.AutoGenerateColumns = false;
                grvMRNItem.DataSource = null;
                this.Cursor = Cursors.Default;

                modifyGrid();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void grvMRNHeader_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                try
                {
                    string mrn = grvMRNHeader.Rows[e.RowIndex].Cells["Column22"].Value.ToString();
                    string dispatch = grvMRNHeader.Rows[e.RowIndex].Cells["Column6"].Value.ToString();
                    string warehouse = grvMRNHeader.Rows[e.RowIndex].Cells["Column18"].Value.ToString();
                    string movment = grvMRNHeader.Rows[e.RowIndex].Cells["Column19"].Value.ToString();
                    DataTable _dt = CHNLSVC.Inventory.GetMRNTrackingDetails(BaseCls.GlbUserComCode, mrn, dispatch, warehouse, movment);
                    grvMRNItem.AutoGenerateColumns = false;

                    if (!string.IsNullOrEmpty(TextBoxMain.Text) && !string.IsNullOrEmpty(TextBoxSub.Text) && !string.IsNullOrEmpty(TextBoxRange.Text))
                    {
                        if (_dt.Select("Categori ='" + TextBoxMain.Text + "' AND Subcategori = '" + TextBoxSub.Text + "' AND Range = '" + TextBoxRange.Text + "'").Length > 0)
                        {
                            DataTable dtFilterd = _dt.Select("Categori ='" + TextBoxMain.Text + "' AND Subcategori = '" + TextBoxSub.Text + "' AND Range = '" + TextBoxRange.Text + "'").CopyToDataTable();
                            grvMRNItem.DataSource = dtFilterd;
                            return;
                        }
                        else
                        {
                            grvMRNItem.DataSource = _dt.NewRow();
                        }
                    }
                    else if (!string.IsNullOrEmpty(TextBoxMain.Text) && !string.IsNullOrEmpty(TextBoxSub.Text))
                    {
                        if (_dt.Select("Categori ='" + TextBoxMain.Text + "' AND Subcategori = '" + TextBoxSub.Text + "'").Length > 0)
                        {
                            DataTable dtFilterd = _dt.Select("Categori ='" + TextBoxMain.Text + "' AND Subcategori = '" + TextBoxSub.Text + "'").CopyToDataTable();
                            grvMRNItem.DataSource = dtFilterd;
                            return;
                        }
                        else
                        {
                            grvMRNItem.DataSource = _dt.NewRow();
                        }
                    }
                    else if (!string.IsNullOrEmpty(TextBoxMain.Text))
                    {
                        if (_dt.Select("Categori ='" + TextBoxMain.Text + "'").Length > 0)
                        {
                            DataTable dtFilterd = _dt.Select("Categori ='" + TextBoxMain.Text + "'").CopyToDataTable();
                            grvMRNItem.DataSource = dtFilterd;
                            return;
                        }
                        else
                        {
                            grvMRNItem.DataSource = _dt.NewRow();
                        }
                    }
                    else
                    {
                        grvMRNItem.DataSource = _dt;
                        return;
                    }
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
                }
                finally
                {
                    CHNLSVC.CloseAllChannels();
                }
            }
        }

        private void txtChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSChanel.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnChannel_Click(null, null);
            }
        }

        private void txtChanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnChannel_Click(null, null);
        }

        private void txtSChanel_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPC.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnSubChannel_Click(null, null);
            }
        }

        private void txtSChanel_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnSubChannel_Click(null, null);
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddItem.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnPc_Click(null, null);
            }
        }

        private void txtPC_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnPc_Click(null, null);
        }

        private void txtMRNNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtFrom.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                btnMRN_Click(null, null);
            }
        }

        private void txtMRNNo_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            btnMRN_Click(null, null);
        }

        private void dtFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtTo.Focus();
            }
        }

        private void dtTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtChanel.Focus();
            }
        }

        private void txtChanel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtChanel.Text)) return;
                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtChanel.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid main category code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtChanel.Clear();
                    txtChanel.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtSChanel_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSChanel.Text)) return;
                if (string.IsNullOrEmpty(txtChanel.Text))
                {
                    MessageBox.Show("Please select channel.", "Document Tracking", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSChanel.Text = "";
                    txtSChanel.Focus();
                    return;
                }

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtSChanel.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid main category code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSChanel.Clear();
                    txtSChanel.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtPC_Leave(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPC.Text)) return;

                string SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location);
                DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, null, null);

                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtPC.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid main category code", "Scheme creation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtPC.Clear();
                    txtPC.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void modifyGrid()
        {
            if (grvMRNHeader.Rows.Count > 0)
            {
                for (int i = 0; i < grvMRNHeader.Rows.Count; i++)
                {
                    if (!string.IsNullOrEmpty(grvMRNHeader.Rows[i].Cells["Column6"].Value.ToString()))
                    {
                        grvMRNHeader.Rows[i].Cells["StatusText"].Value = "Approved";
                    }
                    else
                    {
                        grvMRNHeader.Rows[i].Cells["StatusText"].Value = "Pending";
                    }

                    if (string.IsNullOrEmpty(grvMRNHeader.Rows[i].Cells["Column18"].Value.ToString()) && string.IsNullOrEmpty(grvMRNHeader.Rows[i].Cells["Column19"].Value.ToString()))
                    {
                        grvMRNHeader.Rows[i].Cells["WareHouseText"].Value = "Pending";
                    }
                    else if (!string.IsNullOrEmpty(grvMRNHeader.Rows[i].Cells["Column18"].Value.ToString()) && string.IsNullOrEmpty(grvMRNHeader.Rows[i].Cells["Column19"].Value.ToString()))
                    {
                        grvMRNHeader.Rows[i].Cells["WareHouseText"].Value = "WIP";
                    }
                    else if (!string.IsNullOrEmpty(grvMRNHeader.Rows[i].Cells["Column18"].Value.ToString()) && !string.IsNullOrEmpty(grvMRNHeader.Rows[i].Cells["Column19"].Value.ToString()))
                    {
                        grvMRNHeader.Rows[i].Cells["WareHouseText"].Value = "Dispatched";
                    }
                }
            }
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                TextBoxMain.Enabled = false;
                TextBoxSub.Enabled = false;
                TextBoxRange.Enabled = false;

                TextBoxMain.Clear();
                TextBoxSub.Clear();
                TextBoxRange.Clear();
            }
            else
            {
                TextBoxMain.Enabled = true;
                TextBoxSub.Enabled = true;
                TextBoxRange.Enabled = true;
            }
        }

        private void ImageButtonMain_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Main);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxMain;
                _CommonSearch.ShowDialog();
                TextBoxMain.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ImageButtonSub_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBoxMain.Text))
                {
                    MessageBox.Show("please enter a categori", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBoxMain.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub1);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxSub;
                _CommonSearch.ShowDialog();
                TextBoxSub.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void ImageButtonRange_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(TextBoxMain.Text))
                {
                    MessageBox.Show("please enter a categori", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBoxMain.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(TextBoxSub.Text))
                {
                    MessageBox.Show("please enter a sub categori", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    TextBoxSub.Focus();
                    return;
                }

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.CAT_Sub2);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = TextBoxRange;
                _CommonSearch.ShowDialog();
                TextBoxRange.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void TextBoxRange_DoubleClick(object sender, EventArgs e)
        {
            ImageButtonRange_Click(null, null);
        }

        private void TextBoxSub_DoubleClick(object sender, EventArgs e)
        {
            ImageButtonSub_Click(null, null);
        }

        private void TextBoxMain_DoubleClick(object sender, EventArgs e)
        {
            ImageButtonMain_Click(null, null);
        }

        private void TextBoxMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                ImageButtonMain_Click(null, null);
            }
        }

        private void TextBoxSub_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                ImageButtonSub_Click(null, null);
            }
        }

        private void TextBoxRange_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                ImageButtonRange_Click(null, null);
            }
        }
    }
}