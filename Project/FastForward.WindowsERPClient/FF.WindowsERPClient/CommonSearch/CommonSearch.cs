using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Globalization;

namespace FF.WindowsERPClient.CommonSearch
{
    public partial class CommonSearch : Base
    {
        public TextBox obj_TragetTextBox;
        public TextBox obj_AllResult;
        public Boolean IsDateSearch = false;
        public string SearchParams = "";
        public int ReturnIndex = 0;
        public bool IsSearchEnter = false;
        public string SearchType = "";   //kapila 25/1/2017
        public bool IsReturnFullRow = false;
        public string UserSelectedRow = null;
        

        #region Functions
        public void BindUCtrlDDLData(DataTable _dataSource)
        {
            this.cmbSearchbykey.Items.Clear();

            foreach (DataColumn col in _dataSource.Columns)
            {
                this.cmbSearchbykey.Items.Add(col.ColumnName);
            }
      
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
                if (string.IsNullOrEmpty(obj_AllResult.Text))
                    obj_AllResult.Text = _result;
                else
                    obj_AllResult.Text += "|" + _result;
                _currentIndex += 1;
            }

            return _currentResult;
        }
        #endregion

        #region Individual Common Search Functions
        public bool IsRawData = false;
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
                case CommonUIDefiniton.SearchUserControlType.QuoByCust:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetQuotationAll(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Darshana 24-Sep-2015
                case CommonUIDefiniton.SearchUserControlType.ModelMaster:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetModelMaster(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.ServiceGatePass:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GatePassSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.MasterType:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchMasterType(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.AgrType:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAgrTypeSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.AgrClaimType:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAgrClaimTypeSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.EmployeeAll:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_employee_All(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.ServiceJobStage:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchJobStage(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //Chamal 2014-DEC-24
                case CommonUIDefiniton.SearchUserControlType.ServiceStageChages:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchScvStageChg(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //Chamal 2014-DEC-24
                case CommonUIDefiniton.SearchUserControlType.ServiceTaskCate:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchScvTaskCateByLoc(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Chamal 2014-DEC-24
                case CommonUIDefiniton.SearchUserControlType.ServicePriority:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchWarranty(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Chamal 18-11-2014
                case CommonUIDefiniton.SearchUserControlType.ServiceSerial:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchWarranty(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.AdjType:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.searchFinAdjTypeData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.SerialNIC:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSerialNIC(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.SerialNICAll:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSerialNICAll(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.PayCircular:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.searchPayCircularData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.ItemSupplier:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetItemSupplierSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.ChequeNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDepositChequeSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.Towns:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.searchTownData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Office:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.searchOfficeTownData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.ServiceCustSatisfact:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.CustSatisfacSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.BusDesig:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchBusDesigData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.BusDept:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchBusDeptData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila  26/5/2014
                case CommonUIDefiniton.SearchUserControlType.BankALL:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompanyData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila  26/5/2014
                case CommonUIDefiniton.SearchUserControlType.Area:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Getareasearchdata(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila 
                case CommonUIDefiniton.SearchUserControlType.CustQuest:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCustQuestSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila 
                case CommonUIDefiniton.SearchUserControlType.CustSatis:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCustSatisSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila 
                case CommonUIDefiniton.SearchUserControlType.CustGrade:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCustGradeSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila 26/5/2014
                case CommonUIDefiniton.SearchUserControlType.Region:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Getregionsearchdata(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila 26/5/2014
                case CommonUIDefiniton.SearchUserControlType.Zone:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Getzonesearchdata(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Location:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added by Prabhath on 14/03/2014
                case CommonUIDefiniton.SearchUserControlType.ToLocation:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Company:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCompanySearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                //    {
                //        DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(SearchParams, _searchCatergory, _searchText);
                //        dvResult.DataSource = _result;
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        //kapila 25/1/2017
                        foreach (DataGridViewRow row in dvResult.Rows)
                        {
                            if (Convert.ToInt32(row.Cells[5].Value) ==1)
                            {
                                if ((Convert.ToDateTime(row.Cells[6].Value.ToString()) -Convert.ToDateTime( DateTime.Now.Date)).Days < BaseCls.GlbDiscontItemDays)
                                row.DefaultCellStyle.BackColor = Color.Tan;
                            }
                        }
                        break;
                    }
                //kapila 15/5/2013
                case CommonUIDefiniton.SearchUserControlType.RCC:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetRCC(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila 
                case CommonUIDefiniton.SearchUserControlType.IncentiveCirc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSearchProdBonusCircular(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.GenDiscount:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetGenDiscSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila 
                case CommonUIDefiniton.SearchUserControlType.IncentiveCircular:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSearchPBonusCircular(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RCCReq:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetRCC_REQ(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By Prabhath on 24/12/2012
                case CommonUIDefiniton.SearchUserControlType.Customer:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, _searchCatergory, _searchText, CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_CD));
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.Agreement:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchAgreementData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.GRNNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.searchGRNData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By Prabhath on 24/12/2012
                case CommonUIDefiniton.SearchUserControlType.NIC:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, _searchCatergory, _searchText, CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_NIC));
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By Prabhath on 24/12/2012
                case CommonUIDefiniton.SearchUserControlType.Mobile:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCustomerGenaral(SearchParams, _searchCatergory, _searchText, CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB.ToString(), CommonUIDefiniton.ReturnCustomerSearchDisplay(CommonUIDefiniton.SearchUserControlCustomerType.MBE_MOB));
                        dvResult.DataSource = _result;
                        break;
                    }

                //Added By kapila 16/5/2013
                case CommonUIDefiniton.SearchUserControlType.ServiceAgent:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetServiceAgent(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By kapila 21/5/2014
                case CommonUIDefiniton.SearchUserControlType.ServiceAgentLoc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetServiceAgentLocation(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TaxCode:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchMasterTaxCodes(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TaxrateCodes:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchMasterTaxRateCodes(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By Prabhath on 24/12/2012
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoice:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By Prabhath on 24/12/2012
                case CommonUIDefiniton.SearchUserControlType.ItemAvailableSerial:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Invoice(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By Prabhath on 24/12/2012
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssable:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila 12/9/2013
                case CommonUIDefiniton.SearchUserControlType.CashComCirc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCashComCircSearchDataByComp(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By Prabhath on 24/12/2012
                case CommonUIDefiniton.SearchUserControlType.Employee_Executive:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetEmployeeData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By Prabhath on 24/12/2012
                case CommonUIDefiniton.SearchUserControlType.HpAccount:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetHpAccountSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.InsPayAcc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInsPayReqAccSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //kapila
                case CommonUIDefiniton.SearchUserControlType.HpAccountAdj:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetHpAccountAdjSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By Prabhath on 24/12/2012
                case CommonUIDefiniton.SearchUserControlType.Receipt:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetReceipts(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.ManIssRec:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetManagerIssuReceipts(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PBVoucher:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetPBonusVoucherData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {

                        DataTable _result = CHNLSVC.CommonSearch.GetPC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;

                        //paramsText.Append(GlbUserComCode + seperator);
                        //break;
                    }
                //added by darshana on 01-01-2013
                case CommonUIDefiniton.SearchUserControlType.ReceiptType:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetReceiptTypes(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //added by darshana on 01-01-2013
                case CommonUIDefiniton.SearchUserControlType.Division:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDivision(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Deduction:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDeductionRefData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Refund:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetRefundRefData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Bank: //Edited By Sachith on 31/12/2012
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetBusinessCompany(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.BankBranch:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchBankBranchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Province:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetProvinceData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.District:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDistrictByProvinceData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DepositBankBranch:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchBankBranchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.OPE:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetOPE(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetTown(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Town_new:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetTown_new(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //darshana 04-01-2013
                case CommonUIDefiniton.SearchUserControlType.CustomerCommon:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommon(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //Nadeeka 15-12-2014
                case CommonUIDefiniton.SearchUserControlType.SupplierCommon:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSupplierCommon(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //darshana 07-01-2013
                case CommonUIDefiniton.SearchUserControlType.OutstandingInv:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetOutstandingInvoice(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //sachith 09-01-2013
                case CommonUIDefiniton.SearchUserControlType.AllSalesInvoice:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAllInvoiceSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //sachith 09-01-2013
                case CommonUIDefiniton.SearchUserControlType.DeliverdSerials:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDeliverdInvoiceItemSerials(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //darshana 10-01-2013
                case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAllInvoiceSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //darshana 10-01-2013
                case CommonUIDefiniton.SearchUserControlType.InvoiceByCus:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoicebyCustomer(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //darshana 10-01-2013
                case CommonUIDefiniton.SearchUserControlType.InvoiceItems:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceItem(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //darshana 10-01-2013
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithOth:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialWithOthSerialSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //darshana 10-01-2013
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInsuCompany(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //darshana 10-01-2013
                case CommonUIDefiniton.SearchUserControlType.InsuPolicy:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInsuPolicy(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AcJobNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchAcServicesJobs(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MRN:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSearchMRN(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.WHAREHOUSE:
                    {
                        DataTable _result = CHNLSVC.Inventory.GetLocationByType(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableSerial:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AvailableNoneSerial:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAvailableNoneSerialSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SerialNonSerial:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAvailableSerialNonSerialSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by Nadeeka 2015-02-12
                case CommonUIDefiniton.SearchUserControlType.SerialAvb:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAvailableSeriaSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.GRNItem:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetGRNItemSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CashInvoiceByCus:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCashInvoicebyCustomer(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DINRequestNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDIN(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBook:
                    {
                        //GetPriceBookData(_initialSearchParams, _searchCatergory, _searchText);
                        DataTable _result = CHNLSVC.CommonSearch.GetPriceBookData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //darshana 01-02-2013
                case CommonUIDefiniton.SearchUserControlType.HPInvoiceByCus:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetHPInvoicebyCustomer(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //darshana 01-02-2013
                case CommonUIDefiniton.SearchUserControlType.HpInvoices:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetHpInvoices(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SatReceipt: //SearchReceipt
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchReceipt(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //-----------------------------------------------------------------------------------------------------------------------------
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        DataTable _result;
                        if (IsRawData)
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, _searchCatergory, _searchText);
                        else
                            _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        DataTable _result;
                        if (IsRawData)
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, _searchCatergory, _searchText);
                        else
                            _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        DataTable _result;
                        if (IsRawData)
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, _searchCatergory, _searchText);
                        else
                            _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        DataTable _result;
                        if (IsRawData)
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, _searchCatergory, _searchText);
                        else
                            _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        DataTable _result;
                        if (IsRawData)
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, _searchCatergory, _searchText);
                        else
                            _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Getloc_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //----------------------------------------------------------------------------------------------------------------------------------
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        DataTable _result;
                        if (IsRawData)
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, _searchCatergory, _searchText);
                        else
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        DataTable _result = null;
                        if (IsRawData)
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, _searchCatergory, _searchText);
                        else
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        DataTable _result;
                        if (IsRawData)
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, _searchCatergory, _searchText);
                        else
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        DataTable _result;
                        if (IsRawData)
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, _searchCatergory, _searchText);
                        else
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        DataTable _result;
                        if (IsRawData)
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchRawData(SearchParams, _searchCatergory, _searchText);
                        else
                            _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_PC_HIRC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //---------------------------------------------------------------------------------
                //Added By darshana on 09/02/2013
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversal:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversal(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Main:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub1:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CAT_Sub2:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Model:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAllModels(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_Type:
                    {
                        DataTable _result = CHNLSVC.General.GetSalesTypes(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Sales_SubType:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_sales_subtypes(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added by Prabhath on 13/02/2013
                case CommonUIDefiniton.SearchUserControlType.InterTransferRequest:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSearchInterTransferRequest(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.CashInvoice: //Edited By Prabhath on 27/07/2012
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceByInvType(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HireInvoice: //Edited By Prabhath on 27/07/2012
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceByInvType(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InterTransferReceipt:     //Added by Prabhath on 13/02/201
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetReceiptsByType(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.InterTransferInvoice:     //Added by Prabhath on 14/02/201
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceforInterTransferSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circualr:     //Added by Prabhath on 14/02/201
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CircularByComp:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchDataByComp(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAdjType:     //Added by DARSHANA on 23/02/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSearchHpAdjuestment(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceInvoice:     //Added by DARSHANA on 26/02/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSerInvSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Cheque:     //Added by Shani on 26/02/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Search_cheque(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReturnCheque:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Search_return_cheque(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MgrIssueCheque:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSearchChqByDate(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.satReceiptByAnal3:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchReceiptByAnal3(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllPBLevelByBook:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAllPBLevelByBookData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CLS_ALW_LOC:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_CLS_ALW_LOC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Scheme:     //Added by Prabhath on 04/03/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_SchemesCD_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.WarrantyClaimInvoice:     //Added by Prabhath on 06/03/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetWarrantyClaimableInvoice(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WarrantyClaimSerial:     //Added by Prabhath on 06/03/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetWarrantyClaimableSerial(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType:     //Added by Chamal on 09/03/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetMovementDocSubTypes(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RegistrationDet:     //Added by darshana on 11/03/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Search_Registration_Det(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //2013/03/13 sachith
                case CommonUIDefiniton.SearchUserControlType.VehicleInsuranceRef:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetVehicalInsuranceRef(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //2013/03/13 sachith
                case CommonUIDefiniton.SearchUserControlType.CashCommissionCircular:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCashCommissionCircularNo(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuaranceDet:     //Added by darshana on 14/03/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Search_Insuarance_Det(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //2013/03/15 sachith
                case CommonUIDefiniton.SearchUserControlType.VehicalInsuranceDebit:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetVehicalInsuranceDebitNo(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //2013/03/19 Darshana
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCurrencyData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VehicalInsuranceRegNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetvehicalInsuranceRegistrationNUmber(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Hp_ActiveAccounts:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Hp_ActiveAccounts(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By Prabhath on 27/03/2013
                case CommonUIDefiniton.SearchUserControlType.RawPriceBook:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetRawPriceBook(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By Prabhath on 27/03/2013
                case CommonUIDefiniton.SearchUserControlType.RawPriceLevel:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetRawPriceLevel(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //ADD BY DARSHANA 28-03-2013
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetPurchaseOrders(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //ADD BY SACHITH 05-04-2013
                case CommonUIDefiniton.SearchUserControlType.VehicalJobRegistrationNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetVehicalJobRegistrationNo(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //ADD BY DARSHANA 05-04-2013
                case CommonUIDefiniton.SearchUserControlType.Schema_category:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSchemaCategory(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //add by darshana on 05-04-2013
                case CommonUIDefiniton.SearchUserControlType.SchemeTypeByCate:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSchemaTypeByCate(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //add by darshana on 10-04-2013
                case CommonUIDefiniton.SearchUserControlType.AllScheme:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAllScheme(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceDet:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_invoiceDet(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //add by darshana on 11-04-2013
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetPriceBookByCompanyData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //add by darshana on 11-04-2013
                case CommonUIDefiniton.SearchUserControlType.PriceLevelByBook:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetPriceLevelByBookData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added by Prabhath on 12/04/2013
                case CommonUIDefiniton.SearchUserControlType.TransactionType:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchTransactionType(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added by Prabhath on 12/04/2013
                case CommonUIDefiniton.SearchUserControlType.Promotion:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PartyType:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Party_Types(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PartyCode:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Party_Codes(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //add by sachith2013/04/16
                case CommonUIDefiniton.SearchUserControlType.VoucherNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetVoucheNos(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by darshana 16-04-2013
                case CommonUIDefiniton.SearchUserControlType.ItemBrand:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetItemBrands(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by darshana 16-04-2013
                case CommonUIDefiniton.SearchUserControlType.promoCode:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Promotion_Codes(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by darshana on 19-04-2013
                case CommonUIDefiniton.SearchUserControlType.CircularForSerial:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchForSerial(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CancelSerialCirc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchForCancel(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //by kapila 19-04-2013
                case CommonUIDefiniton.SearchUserControlType.OutsideParty:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetOutsidePartySearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by kapila 19-04-2013
                case CommonUIDefiniton.SearchUserControlType.Group_Sale:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetGroupSaleSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by Darshana 19-04-2013
                case CommonUIDefiniton.SearchUserControlType.AllProofDoc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAllProofDocs(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //by darshana on 19-04-2013
                case CommonUIDefiniton.SearchUserControlType.SerialForCircular:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSerialDetForCir(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By darshana on 22/04/2013
                case CommonUIDefiniton.SearchUserControlType.InvSalesInvoiceForReversalOth:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceForReversalOth(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //added by darshana on 23-04-2013
                case CommonUIDefiniton.SearchUserControlType.HPInvoiceOth:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetHpInvoicesOth(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }


                //by sachith 23/04/2013
                case CommonUIDefiniton.SearchUserControlType.InternalVoucherExpense:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInternalVoucherExpense(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by Shani 23/04/2013
                case CommonUIDefiniton.SearchUserControlType.GPC:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_GPC(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by sachith 23/04/2013
                case CommonUIDefiniton.SearchUserControlType.WarraWarrantyNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetWarrantySearchByWarrantyNoSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by sachith 23/04/2013
                case CommonUIDefiniton.SearchUserControlType.WarraSerialNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetWarrantySearchBySerialNoSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by sachith 23/04/2013
                case CommonUIDefiniton.SearchUserControlType.PriceItem:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetPriceItemSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by Chamal 23/04/2013
                case CommonUIDefiniton.SearchUserControlType.AvailableSerialWithTypes:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Search_inr_ser_infor(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by darshana on 24/04/2013
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                    //kapila
                case CommonUIDefiniton.SearchUserControlType.PendADVNum:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_PendingDoc_ByRefNo(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_DocNum_ByRefNo(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by sachith 02/05/2013
                case CommonUIDefiniton.SearchUserControlType.GiftVoucher:
                    {
                        DataTable _result = CHNLSVC.Inventory.SearchGiftVoucher(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //Add by Chamal 03/05/2013
                case CommonUIDefiniton.SearchUserControlType.MovementTypes:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetMovementTypes(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Circular:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //Add by Chamal 03/05/2013
                case CommonUIDefiniton.SearchUserControlType.InventoryDirection:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInventoryDirections(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //darshana on 09-05-2013
                case CommonUIDefiniton.SearchUserControlType.GetCompanyInvoice:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetComInvoice(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added By Prabhath on 08/05/2013
                case CommonUIDefiniton.SearchUserControlType.AvailableGiftVoucher:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchAvailableGiftVoucher(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //added by sachith 15/05/2013
                case CommonUIDefiniton.SearchUserControlType.CreditNote:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCreditNote(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //added by Shani 15/05/2013
                case CommonUIDefiniton.SearchUserControlType.UserRole:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_system_role(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //added by sachith
                case CommonUIDefiniton.SearchUserControlType.CustomerId:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCustomerId(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //added by darshana 18-05-2013
                case CommonUIDefiniton.SearchUserControlType.SchByCir:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSchemeComByCircular(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //added by Shani 15/05/2013
                case CommonUIDefiniton.SearchUserControlType.SysOptGroups:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_system_option_Groups(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }


                //Included by Prabhath on 03/05/2013
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCard:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCard(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchLoyaltyCardNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchLoyaltyCardNo(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Promot_Comm:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchPromoCommDefData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //darshanaa on 05/06/2013
                case CommonUIDefiniton.SearchUserControlType.CircularByBook:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCircularSearchByBookAndLevel(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //darshana on 06-06-2013
                case CommonUIDefiniton.SearchUserControlType.SearchGsByCus:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchGsByCus(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //DARSHANA ON 08-06-2013
                case CommonUIDefiniton.SearchUserControlType.searchCircular:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //sachith on 10/06/2013
                case CommonUIDefiniton.SearchUserControlType.LoyaltyCardNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetLoyaltyCardNos(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //sachith on 11/06/2013
                case CommonUIDefiniton.SearchUserControlType.DocProInvoiceNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDocProInvoice(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Included by Prabhath on 11/06/2013
                case CommonUIDefiniton.SearchUserControlType.InvoiceExecutive:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchEmployeeAssignToProfitCenter(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //sachith on 11/06/2013
                case CommonUIDefiniton.SearchUserControlType.DocProEngine:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDocProEngine(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //sachith on 11/06/2013
                case CommonUIDefiniton.SearchUserControlType.DocProChassis:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDocProChassis(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //kapila
                case CommonUIDefiniton.SearchUserControlType.SerialNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSearchDataBySerial(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.FixAssetRefNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GET_FixAsset_ref(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SubLoc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchSubLocationData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.VehRegTxn:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetVehical_regTxn(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MRN_AllLoc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSearchMRN_AllLoc(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SystemRole:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_All_Roles(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.SystemUser:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_All_SystemUsers(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.SecUsrPermTp:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_All_SecUsersPerimssionTypes(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.CustomerQuo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetQuotationAll(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //kapila
                case CommonUIDefiniton.SearchUserControlType.CustomerQuoInv:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetQuotation4Inv(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.SupplierQuo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSupplierQuotation(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Designation:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Designations(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Department:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Departments(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceItemUnAssableByModel:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetItemforInvoiceSearchDataByModel(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.WarrantExtendItem:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSearchWarrantyExtend(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AcServChgCode:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_AC_SevCharge_itmes(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EliteCircular:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSearchEliteCommCircular(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PromotionCode://Added by Prabhath on 5/7/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromotion(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PromoByComp:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetSearchDataForPromoByComp(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ApprovePermCode://Added by Shani on 12/7/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Search_Approve_Permissions(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ApprovePermLevelCode://Added by Shani on 12/7/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Search_ApprovePermission_Levels(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.POrder://Added by Shani on 16/7/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Search_PurchaseOrders(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EmployeeSubCategory://Added by sachith on 17/7/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_employee_sub_categories(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PromotionalCircular://Added by sachith on 16/7/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchPromotinalCircular(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        dvResult.Columns["STATUS"].Visible = false;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AdvancedReciept://Added by sachith on 16/7/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAdvancedReciept(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by sachith 02/05/2013
                case CommonUIDefiniton.SearchUserControlType.GiftVoucherByPage:
                    {
                        DataTable _result = CHNLSVC.Inventory.SearchGiftVoucherByPage(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.LoyaltyCustomer:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCustomerCodeLoyalty(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //darshana 21-08-2013
                case CommonUIDefiniton.SearchUserControlType.CustomerCommonByNIC:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCustomerCommonByNIC(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //sachith 27/08/2013
                case CommonUIDefiniton.SearchUserControlType.InvTrcChnl:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInventoryTrackeChannel(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchReversal:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceReversal(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CancelReq:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCancelRequest(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchRevAcc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetReverseAccDet(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AuditCashVerify:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAuditCashVerification(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AuditStockVerify:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAuditStockVerification(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ExchangeInvoice:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceforExchange(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added by Prabhath on 08/11/2013
                case CommonUIDefiniton.SearchUserControlType.ExchangeJob:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchServiceJob(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added by sachith on 12/11/2013
                case CommonUIDefiniton.SearchUserControlType.InventoryItem:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInventoryItem(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Added by Prabhath on 20/11/2013
                case CommonUIDefiniton.SearchUserControlType.GvCategory:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchGvCategory(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //added by sachith 2013/12/20
                case CommonUIDefiniton.SearchUserControlType.PromotionalDiscount:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetPromotionlDiscountHeader(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //added by sachith 2014/01/20
                case CommonUIDefiniton.SearchUserControlType.AccountChecklist:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAccountChecklist(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //added by sachith 2014/01/21
                case CommonUIDefiniton.SearchUserControlType.AccountChecklistPOD:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAccountChecklistPOD(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //added by darshana 26-01-2014
                case CommonUIDefiniton.SearchUserControlType.SupplierFrmSerial:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Search_inr_ser_Supinfor(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AdvanceRecForCus://Added by Darshana 21-02-2014
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAdvancedRecieptForCus(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAccountStus:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAllHpAccountSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.HpAcc4Act:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetHpAcc4ActiveSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.LocationCat3:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCategory3(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //add by darshana on 10-04-2013
                case CommonUIDefiniton.SearchUserControlType.AllInactiveScheme:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAllInactiveScheme(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //add by darshana on 10-06-2014
                case CommonUIDefiniton.SearchUserControlType.DisVouTp:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDisVouTp(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //add by darshana on 11-07-2014
                case CommonUIDefiniton.SearchUserControlType.AvailableSer4Itm:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchAvlbleSerial4Item(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //Witten By Tharaka on 2014-07-18
                case CommonUIDefiniton.SearchUserControlType.PromotionVoucherCricular:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetPromotionVoucherByCircular(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //Witten By Tharaka on 05-08-2014
                case CommonUIDefiniton.SearchUserControlType.DiscountCircularPending://Added by sachith on 16/7/2013
                    {
                        if (CHNLSVC.CommonSearch.SearchPromotinalCircular(searchParams, _searchCatergory, _searchText).Select("STATUS = '2'").Length > 0)
                        {
                            DataTable _result = CHNLSVC.CommonSearch.SearchPromotinalCircular(searchParams, _searchCatergory, _searchText).Select("STATUS = '2'").CopyToDataTable();
                            dvResult.DataSource = _result;
                            dvResult.Columns["STATUS"].Visible = false;
                            break;

                        }
                        else
                        {
                            dvResult.DataSource = null;
                            break;

                        }
                    }
                //Witten By Tharaka on 05-08-2014
                case CommonUIDefiniton.SearchUserControlType.PriceAsignApprovalPendingCricular:
                    {
                        if (CHNLSVC.CommonSearch.GetPromotionSearch(searchParams, _searchCatergory, _searchText).Select("STATUS = 'P'").Length > 0)
                        {
                            DataTable _result = CHNLSVC.CommonSearch.GetPromotionSearch(searchParams, _searchCatergory, _searchText).Select("STATUS = 'P'").CopyToDataTable();
                            dvResult.DataSource = _result;
                            dvResult.Columns["STATUS"].Visible = false;
                            break;
                        }
                        else
                        {
                            dvResult.DataSource = null;
                            break;
                        }

                    }
                case CommonUIDefiniton.SearchUserControlType.BankAccount:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetBankAccounts(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AllBankAccount:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAllBankAccounts(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.JournalEntryAccount:
                    {
                        DataTable _result = CHNLSVC.Financial.GetChequeVoucherAccount(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ChequeVouchers:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetChequeVouchers(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.FundTransferVouchers:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetChequeVouchers(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceJobDetails:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetServiceJobDetails(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EMP_ALL:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAllEmp(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.JobRegNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetJobRegNoSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DefectTypes:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDefectTypes(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceEstimate:
                    {
                        DateTime fromDateValue;
                        //string s = "15/07/2012";
                        if (_searchCatergory == "DATE")
                        {
                            var formats = new[] { "dd/MMM/yyyy", "yyyy-MMM-dd", "dd/MM/yyyy", "dd-MMM-yyyy" };
                            if (DateTime.TryParseExact(_searchText, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out fromDateValue))
                            {
                                DataTable _result = CHNLSVC.CommonSearch.Get_Service_Estimates(SearchParams, _searchCatergory, _searchText);
                                dvResult.DataSource = _result;
                                break;
                            }
                            else
                            {
                                break;
                            }
                        }
                        else
                        {
                            DataTable _result = CHNLSVC.CommonSearch.Get_Service_Estimates(SearchParams, _searchCatergory, _searchText);
                            dvResult.DataSource = _result;
                            break;
                            break;
                        }


                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceWIPMRN_Loc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_LOC_SCV_MRN(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ChequeBook:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GET_CHEQUE_BOOKs(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TechComments:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SERCH_TECHCOMMTBYCHNNL(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EstimateItems:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SEARCH_ESTIMATE_ITEMS(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ConsumableItms:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SEARCH_CONSUMABLE_ITEMS(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //tharaka 2014-12-01
                case CommonUIDefiniton.SearchUserControlType.Service_JobReqNum:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_REQNO(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //tharaka 2014-12-01
                case CommonUIDefiniton.SearchUserControlType.Service_conf_jobSearch:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_JOB(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //tharaka 2014-12-01
                case CommonUIDefiniton.SearchUserControlType.Service_conf_customer:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_CUST(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //tharaka 2014-12-04
                case CommonUIDefiniton.SearchUserControlType.EetimateByJob:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Service_Estimates_ByJob(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //  Nadeeka 2014-12-10
                case CommonUIDefiniton.SearchUserControlType.SerDef_Code:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Service_Def_Code(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //  Nadeeka 2014-12-10
                case CommonUIDefiniton.SearchUserControlType.UtiliLocation:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_Utilization_Location(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //Tharaka 2014-12-23
                case CommonUIDefiniton.SearchUserControlType.ServiceDetailSearials:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetServiceDetailSearials(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Darshana 2014-12-24
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCompanyItemStatusData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Tharaka 2015-02-07
                case CommonUIDefiniton.SearchUserControlType.DO_InoiceNumber:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDOInvoiceNumber(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //Error in SP
                case CommonUIDefiniton.SearchUserControlType.GetItmByType:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetItemByType(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //Tharka 2015-01-17
                case CommonUIDefiniton.SearchUserControlType.GetItmByTypeNew:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetItemByTypeNew(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //Tharka 2015-02-12
                case CommonUIDefiniton.SearchUserControlType.ItemSearchComp:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GET_ITEM_COMP(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //by Nadeeka 2015-02-12
                case CommonUIDefiniton.SearchUserControlType.JobSerial:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetJobSearchBySerialNoSearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                //by Nadeeka 2015-03-09
                case CommonUIDefiniton.SearchUserControlType.MainItem:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetMainItemSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //Darshana 2015-03-18
                case CommonUIDefiniton.SearchUserControlType.ConfByJob:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SEARCH_SCV_CONF_BY_JOB(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TourEnquiry:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SEARCH_ENQUIRY(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.TourFacCom:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SEARCH_FAC_COM(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.spromotor:
                    { // Nadeeeka 11-05-2015
                        DataTable _result = CHNLSVC.CommonSearch.SearchPromotor(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.ServiceSupWCNno:
                    { // Nadeeeka 11-05-2015
                        DataTable _result = CHNLSVC.CommonSearch.GetSupplierClaimDoc(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                // Item Master  
                case CommonUIDefiniton.SearchUserControlType.masterItem:
                    { // Nadeeeka 11-05-2015
                        DataTable _result = CHNLSVC.CommonSearch.GetItemSearchDataMaster(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.masterCat1:
                    { // Nadeeeka 11-05-2015
                        DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat2:
                    { // Nadeeeka 11-05-2015
                        DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat3:
                    { // Nadeeeka 11-05-2015
                        DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.masterCat4:
                    { // Nadeeeka 11-05-2015
                        DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterCat5:
                    { // Nadeeeka 11-05-2015
                        DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchDataMaster(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterUOM:
                    { // Nadeeeka 11-05-2015
                        DataTable _result = CHNLSVC.CommonSearch.GetItemSearchUOMMaster(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterColor:
                    { // Nadeeeka 11-05-2015
                        DataTable _result = CHNLSVC.CommonSearch.GetItemSearchColorMaster(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterTax:
                    { // Nadeeeka 11-05-2015
                        DataTable _result = CHNLSVC.CommonSearch.GetItemSearchTaxMaster(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.masterContry:
                    { // Nadeeeka 11-05-2015
                        DataTable _result = CHNLSVC.CommonSearch.GetCountrySearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                // Nadeeka 10-09-2015
                case CommonUIDefiniton.SearchUserControlType.CircularDef:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetPriceDefCircularSearch(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.CreditNoteAud: //Sanjeewa 2015-11-04
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetCreditNoteAll(searchParams, _searchCatergory, _searchText, BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemByLocation:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetItemByLocation(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InvoiceWithoutReversal:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetInvoiceWithoutReversal(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearchByCustomer:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetServiceJobsByCustomer(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.CreditNoteWithoutSRN: //By Akila 2017/01/24
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchCreditNotes(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Auditors: //By Akila 2017/02/14
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchAllEmployee(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;

                        //DataTable _result = CHNLSVC.Inventory .SearchAuditMembers(searchParams, _searchCatergory, _searchText);
                        //dvResult.DataSource = _result;
                        //break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ExchangeRequest: //By Akila 2017/04/19
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchExchangeRequest(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;

                        //DataTable _result = CHNLSVC.Inventory .SearchAuditMembers(searchParams, _searchCatergory, _searchText);
                        //dvResult.DataSource = _result;
                        //break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ForwardInvoice: //By Akila 2017/04/20
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchForwardInvoice(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.FixAssetRequest: //By Akila 2017/04/20
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchFixAssetRequest(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobsWithStage:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchServiceJobInStage(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                    //Tharanga
                case CommonUIDefiniton.SearchUserControlType.ItemSerNo:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetItemSerNo(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.EMP_ADVISOR:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchServiceAdvisor(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //updated by akila 2017/06/28
                case CommonUIDefiniton.SearchUserControlType.POSupplier:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchSupplierData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ReservationNo: //by akila 2017/058/04
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Search_INT_RES(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Route_cd:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_route_SearchData(searchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SearchSalesType: //Akila 2017/10/26
                    {
                        DataTable _result = CHNLSVC.General.SearchSalesTypes(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Nationality: //Akila 2017/11/27
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchNationality(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.brandmngr: //tharanga 2017/11/21
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Getbrandmng(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                case CommonUIDefiniton.SearchUserControlType.Ref_code: //Tharindu 2017/12/08
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_RefernceTypes(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServicePrioritybycust: //Tharanga 2017/12/22
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GET_PRIT_BY_CUST(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RccByCompleteStage:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetRCCByStage(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ADJREQ:
                    {

                        DataTable _result = CHNLSVC.CommonSearch.GetADJREQ_DET(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.RegisterdEvents: //Akila 2018/01/25
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchEvents(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PreferLoc: //tharanga 2018/03/24
                    {
                       


                        DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(SearchParams, _searchCatergory, _searchText);
                        string SearchParamsnew = "";
                     
                        SearchParamsnew = "391:" + BaseCls.GlbUserComCode + "|";
                        _result.Merge(CHNLSVC.CommonSearch.GetLocationSearchData(SearchParamsnew, _searchCatergory, _searchText));
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DistrictCode: //Akila 2018/03/29
                    {
                        DataTable _result = CHNLSVC.CommonSearch.SearchDistrictDetails(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.MID_SERCH: //tharanga 2018/04/09
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Search_mid_account(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.vehregdet: //tharanga 2018/04/09
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Search_veh_reg_ref(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DepositBank_cred:
                    {

                        DataTable _result = CHNLSVC.CommonSearch.searchDepositBankCode_cred_card(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.DocSubType_ADJ_REQ:     //Added by Chamal on 09/03/2013
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetDocSubTypes_ADJ(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                          
                case CommonUIDefiniton.SearchUserControlType.ACInsReq:     //Added by tharanga on 20/09/2018
                    {
                        DataTable _result = CHNLSVC.CommonSearch.GetAcInsSearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }

                    
                case CommonUIDefiniton.SearchUserControlType.tec_team_cd:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_tec_by_teamcd(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Scv_loc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_scv_location(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.serch_pc_byloc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.serch_pcbysvcloc(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.serch_service_loc:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_scv_location_BY_LOCTIONTABLE(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                //case CommonUIDefiniton.SearchUserControlType.ACInsReq:
                //    {
                //        DataTable _result = CHNLSVC.CommonSearch.GetAcInsSearchData(SearchParams, _searchCatergory, _searchText);
                //        dvResult.DataSource = _result;
                //        break;
                //    }
                case CommonUIDefiniton.SearchUserControlType.Scv_agent:
                    {
                        DataTable _result = CHNLSVC.CommonSearch.Get_scv_agent(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.AC_SVC_ALW_LOC:
                    {

                        DataTable _result = CHNLSVC.CommonSearch.Get_CLS_ALW_LOC_SearchData(SearchParams, _searchCatergory, _searchText);
                        dvResult.DataSource = _result;
                        break;

                        
                      
                    }
                //
                //Get_Hp_ActiveAccounts
                //case CommonUIDefiniton.SearchUserControlType.SatReceipt:
                //    {
                //        DataTable _result = CHNLSVC.CommonSearch.Get_sales_subtypes(SearchParams, _searchCatergory, _searchText);
                //        dvResult.DataSource = _result;
                //        break;
                //    }
                //SatReceipt
                //case CommonUIDefiniton.SearchUserControlType.SalesInvoice:
                //    {
                //        htmlText = CommonUIOperations.ConvertDataTableToHtml(chnlOpt.CommonSearch.GetInvoiceSearchData(searchParams, _searchCatergory, _searchText));
                //        break;
                //    }
                //---------------------------------------------------------------------------------------------------------------------------------
                default:
                    break;
            }

        }
        #endregion

        #region Form Events
        public CommonSearch()
        {
            InitializeComponent();
            //txtSearchbyword.Text = obj_TragetTextBox.Text;
            txtSearchbyword.Select();
            obj_AllResult = new TextBox();

        }

        private void initGridView()
        {
            foreach (DataGridViewRow row in dvResult.Rows)
            {
                if (Convert.ToInt32(row.Cells[5].Value) == 1)
                {
                    if ((Convert.ToDateTime(row.Cells[6].Value.ToString()) - Convert.ToDateTime(DateTime.Now.Date)).Days < BaseCls.GlbDiscontItemDays)
                        row.DefaultCellStyle.BackColor = Color.Tan;
                }
            }
        }
        private void txtSearchbyword_TextChanged(object sender, EventArgs e)
        {
            try
            {
               if (IsSearchEnter == false)
                {
                    if (!string.IsNullOrEmpty(txtSearchbyword.Text))
                    {
                        GetCommonSearchDetails(SearchParams, cmbSearchbykey.SelectedItem.ToString(), "%" + txtSearchbyword.Text.ToString());
                    }
                    else
                    {
                        GetCommonSearchDetails(SearchParams, null, null);
                    }
                }
            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Common Search Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                //CHNLSVC.CloseAllChannels();
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

                //Add by akila 2017/05/16
                if (IsReturnFullRow)
                {
                    UserSelectedRow = GlbSelectData;
                }
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

            //Add by akila 2017/05/16
            if (IsReturnFullRow)
            {
                UserSelectedRow = GlbSelectData;
            }

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
        #endregion

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        #region Sample Coding

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

        //    _CommonSearch.ReturnIndex = 2;
        //    _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
        //    DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
        //    _CommonSearch.dvResult.DataSource = _result;
        //    _CommonSearch.BindUCtrlDDLData(_result);
        //    _CommonSearch.obj_TragetTextBox = txtCustomer;
        //    _CommonSearch.ShowDialog();
        //    txtCustomer.Select();
        //}

        #endregion


        public string GetLoc_HIRC_SearchDesc(int i, string _code)
        {
            if (i > 41 || i < 35)
            {
                return null;
            }
            ChannelOperator chnlOpt = new ChannelOperator();
            CommonUIDefiniton.SearchUserControlType _type = (CommonUIDefiniton.SearchUserControlType)i;

            Base _basePage = new Base();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new Base();
            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }

                default:
                    break;
            }


            DataTable dt = chnlOpt.CommonSearch.Getloc_HIRC_SearchData(paramsText.ToString(), "CODE", _code);
            if (dt == null)
            {
                return null;
            }
            if (dt.Rows.Count <= 0 || dt == null || dt.Rows.Count > 1)
                return null;
            else
                return dt.Rows[0][1].ToString();
        }

        public string Get_pc_HIRC_SearchDesc(int i, string _code)
        {
            //    chnlOpt = new ChannelOperator();
            //    CommonUIDefiniton.SearchUserControlType _type = (CommonUIDefiniton.SearchUserControlType)i;
            //    _basePage = new BasePage();
            //    StringBuilder paramsText = new StringBuilder();
            //    string seperator = "|";
            //    paramsText.Append(((int)_type).ToString() + ":");
            //    _basePage = new BasePage();

            ///////////////////////////////////////
            ChannelOperator chnlOpt = new ChannelOperator();
            CommonUIDefiniton.SearchUserControlType _type = (CommonUIDefiniton.SearchUserControlType)i;
            Base _basePage = new Base();
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");
            _basePage = new Base();

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append("" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
              



                default:
                    break;
            }

            try
            {
                DataTable dt = chnlOpt.CommonSearch.Get_PC_HIRC_SearchData(paramsText.ToString(), "CODE", _code);
                if (dt == null)
                {
                    return "";
                }
                if (dt.Rows.Count <= 0 || dt == null || dt.Rows.Count > 1)
                    return "";
                else
                    return dt.Rows[0][1].ToString();
            }
            catch (Exception ex)
            {
                return "";
            }


        }

        private void CommonSearch_Load(object sender, EventArgs e)
        {
            if (SearchType == "ITEMS")
                initGridView();
        }

    }
}
