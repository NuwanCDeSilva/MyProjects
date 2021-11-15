using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Inventory
{
    public partial class InventoryDocumentCancellation : Base
    {
        #region Varialbe
        private List<InventoryRequestItem> SaveItemList = null;
        private List<ReptPickSerials> saveSerialList = null;
        string _aodIssueLoc = string.Empty;
        #endregion

        private void InitializeForm()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                dtpDate.Value = CHNLSVC.Security.GetServerDateTime().Date;
                saveSerialList = new List<ReptPickSerials>();
                SaveItemList = new List<InventoryRequestItem>();
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel(); 
            }
            finally
            {
                this.Cursor = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        public InventoryDocumentCancellation()
        {
            InitializeComponent();
        }

        private void InventoryDocumentCancellation_Load(object sender, EventArgs e)
        {
            bool _allowCurrentTrans = false;
            IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
        }

        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            StringBuilder seperator = new StringBuilder("|");
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.GitDocDateSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "AOD" + seperator + "0" + seperator + "A" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GitDocWithLocDateSearch:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + "AOD" + seperator + "0" + seperator + txtIncorrectLoc.Text.ToString() + seperator + "A" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        #endregion

        #region Call individual searching
        private void btnSearch_IncorrectLoc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtIncorrectLoc;
                _CommonSearch.ShowDialog();
                txtIncorrectLoc.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSearch_Documents_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
                _CommonSearch.ReturnIndex = 0;
                if (string.IsNullOrEmpty(txtIncorrectLoc.Text))
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GitDocDateSearch);
                else
                    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.GitDocWithLocDateSearch);

                DataTable _result = null;
                if (string.IsNullOrEmpty(txtIncorrectLoc.Text))
                    _result = CHNLSVC.CommonSearch.Search_GIT_AODs(_CommonSearch.SearchParams, null, null, dtpDate.Value.Date.AddMonths(-3), dtpDate.Value.Date);
                else
                    _result = CHNLSVC.CommonSearch.Search_GIT_AODs_WithLoc(_CommonSearch.SearchParams, null, null, dtpDate.Value.Date.AddMonths(-3), dtpDate.Value.Date);

                _CommonSearch.dtpFrom.Value = dtpDate.Value.Date.AddMonths(-3);
                _CommonSearch.dtpTo.Value = dtpDate.Value.Date;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtDocNo;
                _CommonSearch.ShowDialog();
                txtDocNo.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }
        #endregion

        private void txtIncorrectLoc_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_IncorrectLoc_Click(null, null);
        }

        private void txtIncorrectLoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_IncorrectLoc_Click(null, null);
            else if (e.KeyCode == Keys.Enter)
                txtDocNo.Focus();
        }

        private void txtIncorrectLoc_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtIncorrectLoc.Text))
                {
                    MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtIncorrectLoc.Text.ToString());
                    if (_masterLocation != null)
                    {
                        lblIncorrectLocation.Text = _masterLocation.Ml_loc_desc;
                    }
                    else
                    {
                        MessageBox.Show("Invalid location code!", "Incorrect Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtIncorrectLoc.Clear();
                        txtIncorrectLoc.Focus();
                        return;
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtDocNo_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Documents_Click(null, null);
        }

        private void txtDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
                btnSearch_Documents_Click(null, null);
            else if (e.KeyCode == Keys.Enter)
                txtRemarks.Focus();
        }

        private void txtDocNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDocNo.Text))
            {
                btnSearch_Click(null, null);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                bool _invalidDoc = true;
                int _lineNo = 0;

                _aodIssueLoc = string.Empty;

                InventoryHeader _invHdr = new InventoryHeader();

                _invHdr = CHNLSVC.Inventory.Get_Int_Hdr(txtDocNo.Text);
                #region Check Valid Document No
                if (_invHdr == null)
                {
                    _invalidDoc = false;
                    goto err;
                }
                if (_invHdr.Ith_doc_tp != "AOD")
                {
                    _invalidDoc = false;
                    goto err;
                }
                if (_invHdr.Ith_direct == true)
                {
                    _invalidDoc = false;
                    goto err;
                }
                if (_invHdr.Ith_sub_tp == "SERVICE")
                {
                    _invalidDoc = false;
                    goto err;
                }

                dtpDocDate.Value = _invHdr.Ith_doc_date.Date;
                txtManualRef.Text = _invHdr.Ith_manual_ref;
                txtOthRef.Text = _invHdr.Ith_oth_docno;
                txtRemarks.Text = _invHdr.Ith_remarks; 

                DataTable dtSCM = CHNLSVC.Inventory.CheckIsAodReceived(txtDocNo.Text.ToString());
                if (dtSCM != null)
                {
                    if (dtSCM.Rows.Count > 0)
                    {
                        MessageBox.Show("AOD Issue document already received!", "AOD No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDocNo.Clear();
                        txtDocNo.Focus();
                        return;
                    }
                }

            err:
                if (_invalidDoc == false)
                {
                    MessageBox.Show("Invalid AOD Issue Document No!", "AOD No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDocNo.Clear();
                    txtDocNo.Focus();
                    return;
                }
                else
                {
                    _aodIssueLoc = _invHdr.Ith_loc;
                    txtIncorrectLoc.Text = _invHdr.Ith_oth_loc;
                    MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtIncorrectLoc.Text.ToString());
                    if (_masterLocation != null)
                    {
                        lblIncorrectLocation.Text = _masterLocation.Ml_loc_desc;
                    }
                    SaveItemList = new List<InventoryRequestItem>();
                    saveSerialList = new List<ReptPickSerials>();
                }
                #endregion

                #region Get Serials
                SaveItemList = new List<InventoryRequestItem>();
                saveSerialList = new List<ReptPickSerials>();
                saveSerialList = CHNLSVC.Inventory.Get_Int_Ser(txtDocNo.Text);
                if (saveSerialList != null)
                {
                    var _scanItems = saveSerialList.GroupBy(x => new { x.Tus_itm_cd, x.Tus_itm_stus, x.Tus_itm_desc, x.Tus_itm_model, x.Tus_itm_brand, x.Tus_unit_cost }).Select(group => new { Peo = group.Key, theCount = group.Count() });
                    foreach (var itm in _scanItems)
                    {
                        _lineNo += 1;
                        InventoryRequestItem _itm = new InventoryRequestItem();
                        _itm.Itri_app_qty = Convert.ToDecimal(itm.theCount);
                        _itm.Itri_itm_cd = itm.Peo.Tus_itm_cd;
                        _itm.Itri_itm_stus = itm.Peo.Tus_itm_stus;
                        _itm.Itri_line_no = _lineNo;
                        _itm.Itri_qty = Convert.ToDecimal(itm.theCount);
                        _itm.Mi_longdesc = itm.Peo.Tus_itm_desc;
                        _itm.Mi_model = itm.Peo.Tus_itm_model;
                        _itm.Mi_brand = itm.Peo.Tus_itm_brand;
                        _itm.Itri_unit_price = itm.Peo.Tus_unit_cost;
                        SaveItemList.Add(_itm);

                    }

                    gvItems.AutoGenerateColumns = false;
                    gvItems.DataSource = SaveItemList;

                    gvSerial.AutoGenerateColumns = false;
                    gvSerial.DataSource = saveSerialList;

                    txtIncorrectLoc.Enabled = false;
                    btnSearch_IncorrectLoc.Enabled = false;
                }
                else
                {
                    MessageBox.Show("Item not found!", "AOD No", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtDocNo.Clear();
                    txtDocNo.Focus();
                    return;
                }

                #endregion
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                _aodIssueLoc = string.Empty;
                while (this.Controls.Count > 0)
                {
                    Controls[0].Dispose();
                }
                InitializeComponent();
                InitializeForm();
                bool _allowCurrentTrans = false;
                IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans);
                txtIncorrectLoc.Focus();
                this.Cursor = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            Process();
        }

        private void Process()
        {
            try
            {
                if (CheckServerDateTime() == false) return;

                if (BaseCls.GlbUserComCode != "AST")        //kapila 26/1/2016
                {
                    if (string.IsNullOrEmpty(txtIncorrectLoc.Text) || string.IsNullOrEmpty(txtDocNo.Text) || gvItems.Rows.Count <= 0)
                    {
                        MessageBox.Show("Please select the relevant AOD document!", "AOD Document", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtDocNo.Focus();
                        return;
                    }
                }
                if (string.IsNullOrEmpty(txtRemarks.Text))
                {
                    //MessageBox.Show("Please enter the remarks!", "AOD Document", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //txtRemarks.Focus();
                    //return;
                    txtRemarks.Text = "-";
                }

                bool _allowCurrentTrans = false;
                if (  IsAllowBackDateForModule(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, string.Empty, this.GlbModuleName, dtpDate, lblBackDateInfor, string.Empty, out _allowCurrentTrans) == false)
                {
                    if (_allowCurrentTrans == true)
                    {
                        if (dtpDate.Value.Date != DateTime.Now.Date)
                        {
                            dtpDate.Enabled = true;
                            MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dtpDate.Focus();
                            return;
                        }
                    }
                    else
                    {
                        dtpDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpDate.Focus();
                        return;
                    }
                }

                if (dtpDate.Value.Date != dtpDocDate.Value.Date)
                {
                    MessageBox.Show("Document date not match with the selected date!", "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpDate.Focus();
                    return;
                }

                if (MessageBox.Show("Are you sure do you want to cancel this entry?", "Calcellation...", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.No)
                {
                    Cursor.Current = Cursors.Default;
                    return;
                }

                Cursor.Current = Cursors.WaitCursor;
                string _msg = string.Empty;
                int result = CHNLSVC.Inventory.CancelOutwardEntry(txtDocNo.Text.ToString(), BaseCls.GlbUserID, out _msg);
                if (result ==1)
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show("Successfully Canceled!", "AOD Calcellation", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnClear_Click(null, null);
                }
                else
                {
                    Cursor.Current = Cursors.Default;
                    MessageBox.Show(_msg, "Process Terminated", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Cursor.Current = Cursors.Default;
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel(); 
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseAllChannels();  
            }
        }

    }
}
