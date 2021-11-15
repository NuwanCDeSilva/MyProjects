using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using FF.BusinessObjects.Sales;
using System.Globalization;
using System.Transactions;
using FF.WebERPClient.UserControls;

namespace FF.WebERPClient.General_Modules
{
    public partial class ExchangeRateSetup : BasePage
    {
        protected List<MasterExchangeRate> _ExchangeRate
        {
            get { return (List<MasterExchangeRate>)ViewState["_ExchangeRate"]; }
            set { ViewState["_ExchangeRate"] = value; }
        }

        
        protected Int32 _lineNo
        {
            get { return (Int32)ViewState["_lineNo"]; }
            set { ViewState["_lineNo"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                this.ClearData();

                _ExchangeRate = new List<MasterExchangeRate>();
                _lineNo = 0;
            }
        }

        protected void BindCurrency(DropDownList _ddl)
        {
            _ddl.Items.Clear();
            List<MasterCurrency> _Currency = CHNLSVC.General.GetAllCurrency(string.Empty);
            if (_Currency != null)
            {
                _ddl.DataSource = _Currency.OrderBy(items => items.Mcr_cd);
                _ddl.DataTextField = "Mcr_cd";
                _ddl.DataValueField = "Mcr_cd";
                _ddl.DataBind();
            }
        }

        private void ClearData()
        {
            BindCurrency(ddlFromCur);
            BindCurrency(ddlToCur);
            ddlFromCur.Text = "LKR";
            ddlToCur.Text = "LKR";
            txtBankBuy.Text = "";
            txtBankSelling.Text = "";
            txtCustom.Text = "";
            _ExchangeRate = new List<MasterExchangeRate>();
            txtFDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());
            txtTDate.Text = Convert.ToString(DateTime.Today.ToShortDateString());

            DataTable _table = new DataTable();
            gvExchange.DataSource = _table;
            gvExchange.DataBind();

            ddlFromCur.Focus();
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            this.ClearData();
            _ExchangeRate = new List<MasterExchangeRate>();
            
        }

        private MasterExchangeRate AssignDataToObject()
        {

            //String.Format("{0:0,0.0}", 12345.67); 
            string _Amt = "";
            MasterExchangeRate _tempItem = new MasterExchangeRate();

            _tempItem.Mer_id = _lineNo;
            _tempItem.Mer_com = GlbUserComCode;
            _tempItem.Mer_cur = ddlFromCur.SelectedValue;
            _tempItem.Mer_vad_from = Convert.ToDateTime(txtFDate.Text);
            _tempItem.Mer_vad_to = Convert.ToDateTime(txtTDate.Text);
            _tempItem.Mer_bnksel_rt = Convert.ToDecimal(txtBankSelling.Text);
            _tempItem.Mer_bnkbuy_rt = Convert.ToDecimal(txtBankBuy.Text);
            _tempItem.Mer_cussel_rt = Convert.ToDecimal(txtCustom.Text);
            _tempItem.Mer_buyvad_from = Convert.ToDateTime(txtFDate.Text);
            _tempItem.Mer_buyvad_to = Convert.ToDateTime(txtTDate.Text);
            _tempItem.Mer_act = true;
            _tempItem.Mer_cre_by = GlbUserName;
            _tempItem.Mer_cre_dt = Convert.ToDateTime(txtTDate.Text);
            _tempItem.Mer_mod_by = GlbUserName;
            _tempItem.Mer_mod_dt = Convert.ToDateTime(txtTDate.Text);
            _tempItem.Mer_session_id = GlbUserSessionID;
            _tempItem.Mer_to_cur = ddlToCur.SelectedValue;

            return _tempItem;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            _lineNo += 1;
            _ExchangeRate.Add(AssignDataToObject());
            BindAddItem();
            ddlFromCur.Text = "LKR";
            ddlToCur.Text = "LKR";
            txtBankBuy.Text = "";
            txtBankSelling.Text = "";
            txtCustom.Text = "";
            ddlFromCur.Focus();

        }

        protected void BindAddItem()
        {
            gvExchange.DataSource = _ExchangeRate;
            gvExchange.DataBind();

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string _msg = "";
            Int32 _rowEffect = 0;
            if (gvExchange.Rows.Count <= 0)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Details not found.");
                return;
            }

            _rowEffect = (Int32)CHNLSVC.Sales.SaveExchangeRate(_ExchangeRate);

            if (_rowEffect == 1)
            {
                this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully created");
                ClearData();
                return;
                
            }
            else
            {
                if (!string.IsNullOrEmpty(_msg))
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, _msg);
                }
                else
                {
                    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Creation Fail.");
                }
            }


        }
    }
}