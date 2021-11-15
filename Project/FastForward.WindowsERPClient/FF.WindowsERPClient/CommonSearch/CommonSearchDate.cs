using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace FF.WindowsERPClient.CommonSearch
{
    public partial class CommonSearchDate : Base
    {
        public TextBox obj_TragetTextBox;
        public Boolean IsDateSearch = false;
        public string SearchParams = "";
        public int ReturnIndex = 0;
        public bool IsSearchEnter = false;

        #region Functions
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {

                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }
            if (BaseCls.GlbUserComCode=="AAL")
            {
              this.cmbSearchbykey.SelectedIndex = 1;
            }
            else
                this.cmbSearchbykey.SelectedIndex = 0;


            
        }

        public void GetSelectedRowData()
        {
            int i = 0;
            string _resultString = "";
            if (dvResult.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dvResult.SelectedRows)
                {
                    for (i = 0; i < dvResult.ColumnCount; ++i)
                    {
                        if (i == 0)
                            _resultString = row.Cells[i].Value.ToString();
                        else
                            _resultString += '|' + row.Cells[i].Value.ToString();
                    }
                }
            }

            GlbSelectData = _resultString;
        }

        public string GetResult(string _resultAll, int _index)
        {
            int _currentIndex = 0;
            string _currentResult = string.Empty;

            string[] _arrResultAll = _resultAll.Split('|');
            foreach (string _result in _arrResultAll)
            {
                if (_currentIndex == _index)
                {
                    _currentResult = _result;
                }
                _currentIndex += 1;
            }

            return _currentResult;
        }
        #endregion

        #region Individual Common Search Functions
        public void GetCommonSearchDetails(string _initialParams, string _searchCatergory, string _searchText)
        {
            //Split and get the SearchUserControlType. 
            string[] param = _initialParams.Split(new string[] { ":" }, StringSplitOptions.None);
            CommonUIDefiniton.SearchUserControlType searchType = (CommonUIDefiniton.SearchUserControlType)Convert.ToInt32(param[0].ToString());

            //Get the remaining SP parameters.
            string searchParams = param[1];

            switch (searchType)
            {
                    //kapila
                case CommonUIDefiniton.SearchUserControlType.InsReqInvoice:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchInv4InsReq(searchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Chamal 14-03-2013
                case CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(searchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Chamal 02-04-2013
                case CommonUIDefiniton.SearchUserControlType.GitDocDateSearch:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Search_GIT_AODs(searchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Chamal 02-04-2013
                case CommonUIDefiniton.SearchUserControlType.GitDocWithLocDateSearch:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Search_GIT_AODs_WithLoc(searchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added by Prabhath on 20/05/2013
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithDate:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchInvoice(searchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added by darshana on 26-11-2013

                case CommonUIDefiniton.SearchUserControlType.ReceiptDate:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetReceiptsDate(searchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AccountDate:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetHpAccountDateSearchData(searchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(SearchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchF3:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetServiceJobsF3(SearchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceRequests:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetServiceRequests(SearchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceInvoiceSearch:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchServiceInvoice(SearchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.POByDate:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrdersByDate(SearchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWIP:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetServiceJobsWIP(SearchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        //for (int i = 0; i < dvResult.Rows.Count; i++)
                        //{
                        //    DataTable dt = CHNLSVC.CustService.getPriorityDataByCode(dvResult.Rows[i].Cells["PRORITY"].Value.ToString());
                        //    Color newColor = System.Drawing.ColorTranslator.FromHtml(dt.Rows[0]["scp_color"].ToString());
                        //    dvResult.Rows[i].DefaultCellStyle.BackColor = newColor;
                        //}
                        //dvResult.Refresh();
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJObSerarhEnquiry:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetServiceJobsEnqeuiy(SearchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchWarrClaim:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetServiceJobsWarrClaim(SearchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        DataTable _result = CHNLSVC.Inventory.SearchAuditJobs(SearchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SubJob:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetsubJobNo(SearchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Do_qua_serch:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.get_quo_to_inv(SearchParams, _searchCatergory, _searchText, dtpFrom.Value.Date, dtpTo.Value.Date);
                        dvResult.DataSource = _result;
                        break;
                    }

                default:
                    break;
            }

          
        }
        #endregion

        #region Form Events
        public CommonSearchDate()
        {
            InitializeComponent();
            //txtSearchbyword.Text = obj_TragetTextBox.Text;
            txtSearchbyword.Select();
        }

        private void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (IsSearchEnter == false)
                {
                    if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                    {
                        GetCommonSearchDetails(SearchParams, cmbSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                    }
                    else
                    {
                        GetCommonSearchDetails(SearchParams, null, null);
                    }
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtSearchbyword_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (dvResult.Rows.Count > 0)
                {
                    dvResult.Select();
                }
            }
            else if (e.KeyCode == Keys.Enter)
            {
                if (IsSearchEnter == false)
                {
                    cmbSearchbykey.Focus();
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                    {
                        GetCommonSearchDetails(SearchParams, cmbSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                    }
                    else
                    {
                        GetCommonSearchDetails(SearchParams, null, null);
                    }
                    //kapila 20/6/2015
                    if (dvResult.Rows.Count == 0)
                        MessageBox.Show("No data found for the search details. Please re-check again", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        public void dvResult_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                GetSelectedRowData();
                obj_TragetTextBox.Text = GetResult(GlbSelectData, ReturnIndex);
                this.Close();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void dvResult_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            GetSelectedRowData();
            obj_TragetTextBox.Text = GetResult(GlbSelectData, ReturnIndex);
            this.Close();
        }

        private void cmbSearchbykey_SelectedIndexChanged(object sender, EventArgs e)
        {


            //txtSearchbyword.Text = string.Empty;
            //txtSearchbyword.Select();
        }

        private void cmbSearchbykey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSearchbyword.Focus();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                txtSearchbyword_TextChanged(null, null);

                GetCommonSearchDetails(SearchParams, null, null);

                Cursor.Current = Cursors.Default;

                if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                {
                    GetCommonSearchDetails(SearchParams, cmbSearchbykey.SelectedItem.ToString(), txtSearchbyword.Text.ToString());
                }
                //kapila 20/6/2015
                if (dvResult.Rows.Count == 0)
                    MessageBox.Show("No data found for the selected criteria. Please re-check the criteria", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        #endregion

        private void CommonSearchDate_Load(object sender, EventArgs e)
        {

        }

        #region Sample Coding | StockAdjustment Module | CommonSearchDAL

        //private void btnSearch_DocumentNo_Click(object sender, EventArgs e)
        //{
        //    CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
        //    _CommonSearch.ReturnIndex = 0;
        //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.MovementDocDateSearch);
        //    DataTable _result = CHNLSVC.CommonSearch.Search_int_hdr_Infor(_CommonSearch.SearchParams, null, null, dtpDate.Value.Date.AddMonths(-1), dtpDate.Value.Date);
        //    _CommonSearch.dtpFrom.Value = dtpDate.Value.Date.AddMonths(-1);
        //    _CommonSearch.dtpTo.Value = dtpDate.Value.Date;
        //    _CommonSearch.dvResult.DataSource = _result;
        //    _CommonSearch.BindUCtrlDDLData(_result);
        //    _CommonSearch.obj_TragetTextBox = txtDocumentNo;
        //    _CommonSearch.ShowDialog();
        //    txtDocumentNo.Select();
        //}

        //--- DAL method | CommonSearchDAL
        //public DataTable Search_int_hdr_Infor(string _initialSearchParams, string _searchCatergory, string _searchText, DateTime _fromDate, DateTime _toDate)

        #endregion

    }
}
