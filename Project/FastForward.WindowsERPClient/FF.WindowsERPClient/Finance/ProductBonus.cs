using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using FF.BusinessObjects;

namespace FF.WindowsERPClient.Finance
{
    public partial class ProductBonus : Base
    {
        private List<PBonusVouDetail> _PBVouDetList = null;
        private List<PBonusVouHeader> _PBVouHdrList = null;
        private List<PBonusVouDedc> _PBVouDedList = null;
        private List<PBonusVouDedc> _PBVouRefList = null;

        private string _userType;
        private Boolean _checkUser = false;
        private Boolean _authUser = false;
        private static string _DedType = "";
        private static Int32 _isRef = 0;

        public ProductBonus()
        {
            InitializeComponent();

            _PBVouDetList = new List<PBonusVouDetail>();
            _PBVouHdrList = new List<PBonusVouHeader>();
            _PBVouDedList = new List<PBonusVouDedc>();
            _PBVouRefList = new List<PBonusVouDedc>();
        }

        private void VoucherEntry_Load(object sender, EventArgs e)
        {
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10068))
            {
                txtAccRem.Enabled = true;
                txtRefund.Enabled = true;
                txtRefRem.Enabled = true;
                btn_New.Enabled = true;
                btnPrint.Enabled = true;
                btnAccDed.Enabled = true;
                btnRef.Enabled = true;
                btnConfirm.Enabled = false;
                _userType = "ACC";

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10077))
                    _checkUser = true;
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10078))
                    _authUser = true;

            }
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10069))
            {
                btn_New.Enabled = false;
                _userType = "CC";
            }
            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 10070))
            {
                btn_New.Enabled = false;
                _userType = "CRD";
            }


            try
            {
                UtilityClasses.NumberToWord _num = new UtilityClasses.NumberToWord();

                //IsAllowBackDateForModule(BaseCls.GlbUserComCode, string.Empty, BaseCls.GlbUserDefProf, this.GlbModuleName, dateTimePickerTransaction, lblError, string.Empty);
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

        private void btnCircular_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtProfitCenter.Text))
            {
                MessageBox.Show("Please select the profit center", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.IncentiveCirc);
            DataTable _result = CHNLSVC.CommonSearch.GetSearchProdBonusCircular(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtCircular;
            _CommonSearch.ShowDialog();
            txtCircular.Focus();
            txtCircular_Leave(null, null);
        }

        private void bindSchemes()
        {
            cmbScheme.DataSource = null;
            List<IncentiveSch> _lst = CHNLSVC.General.GetIncentiveSchemes(txtCircular.Text);
            cmbScheme.DataSource = _lst;
            cmbScheme.DisplayMember = "INC_REF";
            cmbScheme.ValueMember = "INC_REF";
            cmbScheme.SelectedIndex = -1;

            if (_lst != null)
            {
                dtFrom.Value = Convert.ToDateTime(_lst[0].Inc_from);
                dtTo.Value = Convert.ToDateTime(_lst[0].Inc_to);
            }
        }

        private void Load_PC_Det()
        {
            MasterProfitCenter _masterPC = null;
            string _manCode = string.Empty;
            _masterPC = CHNLSVC.General.GetPCByPCCode(BaseCls.GlbUserComCode, txtProfitCenter.Text);
            if (_masterPC != null)
            {
                txtPCDesc.Text = _masterPC.Mpc_desc;
                _manCode = _masterPC.Mpc_man;
            }
            else
            {
                txtPCDesc.Text = "";
            }
            DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, _manCode);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                txtManager.Text = _dt.Rows[0]["ESEP_FIRST_NAME"].ToString() + ' ' + _dt.Rows[0]["ESEP_LAST_NAME"].ToString();
            }
            else
                txtManager.Text = "";

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            string _uType = "";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.EmployeeEPF:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "SRMGR");
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.IncentiveCirc:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtProfitCenter.Text + seperator + Convert.ToDateTime(dtFromDate.Value).ToShortDateString() + seperator + Convert.ToDateTime(dtToDate.Value).ToShortDateString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PBVoucher:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserProfitCenter:
                    {

                        paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        break;

                    }
                case CommonUIDefiniton.SearchUserControlType.Refund:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Deduction:
                    {
                        if (_userType == "ACC")
                        {
                            _uType = "A";
                        }
                        else if (_userType == "CC")
                        {
                            _uType = "S";
                        }
                        else if (_userType == "CRD")
                        {
                            _uType = "C";
                        }
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + _uType + seperator);
                        break;
                    }
                default:
                    break;
            }
            return paramsText.ToString();
        }

        private void txtCircular_Leave(object sender, EventArgs e)
        {
            bindSchemes();
        }

        private void btnPC_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserProfitCenter);
                DataTable _result = CHNLSVC.CommonSearch.GetUserProfitCentreSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtProfitCenter;
                _CommonSearch.txtSearchbyword.Text = txtProfitCenter.Text;
                _CommonSearch.ShowDialog();
                txtProfitCenter.Focus();
                txtProfitCenter_Leave(null, null);

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

        private void txtProfitCenter_Leave(object sender, EventArgs e)
        {
            Load_PC_Det();


        }

        private void Load_PB_Vou_Det()
        {
            //load p bonus details
            DataTable _dt = CHNLSVC.Financial.Get_PBonus_VOu(cmbScheme.Text, BaseCls.GlbUserComCode, txtProfitCenter.Text);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                //txtGross.Text = _dt.Rows[0]["pbih_gross"].ToString();
                lblValue.Text = Convert.ToDecimal(_dt.Rows[0]["pbih_gross"]).ToString("0.00");
                //txtNet.Text = _dt.Rows[0]["pbih_net"].ToString();
                //txtDedAcc.Text = _dt.Rows[0]["PBIH_ACC_DED"].ToString();
                //txtDedCC.Text = _dt.Rows[0]["PBIH_CC_DED"].ToString();
                //txtDedCrd.Text = _dt.Rows[0]["PBIH_CRD_DED"].ToString();
                //txtAccRem.Text = _dt.Rows[0]["PBIH_ACC_REM"].ToString();
                //txtCCRem.Text = _dt.Rows[0]["PBIH_CC_REM"].ToString();
                //txtCrdRem.Text = _dt.Rows[0]["PBIH_CRD_REM"].ToString();
                //txtRefund.Text = _dt.Rows[0]["PBIH_REFUND"].ToString();
                //txtRefRem.Text = _dt.Rows[0]["PBIH_REFUND_REM"].ToString();
                //txtPrepare.Text = _dt.Rows[0]["PBIH_PREP_BY"].ToString();
                //txtCheck.Text = _dt.Rows[0]["PBIH_CHK_BY"].ToString();
                ////txtAutho.Text = _dt.Rows[0]["PBIH_AUTH_BY"].ToString();
                //txtRef.Text = _dt.Rows[0]["pbih_vou_ref"].ToString();
                //if (_dt.Rows[0]["pbih_stus"].ToString() == "P")
                //{
                //    lblStus.Text = "Pending";
                //}
                //else
                //{
                //    lblStus.Text = "Confirmed";
                //}
                //dtFrom.Text = _dt.Rows[0]["pbih_valid_from"].ToString();
                //dtTo.Text = _dt.Rows[0]["pbih_valid_to"].ToString();

            }
            else
            {
                //txtRef.Text = "";
                //txtGross.Text = "0";
                lblValue.Text = "0.00";
                //txtNet.Text = "0";
                //txtDedAcc.Text = "0";
                //txtDedCC.Text = "0";
                //txtDedCrd.Text = "0";
                //txtAccRem.Text = "";
                //txtCCRem.Text = "";
                //txtCrdRem.Text = "";
                //txtRefund.Text = "0";
                //txtRefRem.Text = "";
                //txtPrepare.Text = "";
                //txtCheck.Text = "";
                ////txtAutho.Text = "";
                //lblStus.Text = "-";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string _refUpdBy = string.Empty;
            string _accUpdBy = string.Empty;
            string _ccUpdBy = string.Empty;
            string _crdUpdBy = string.Empty;
            string _prepBy = string.Empty;
            string _chkBy = string.Empty;
            string _authBy = string.Empty;
            string _docNo;

            if (string.IsNullOrEmpty(txtCircular.Text) && _userType == "ACC")
            {
                MessageBox.Show("Please select the circular", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            if (txtRefund.Enabled == true) _refUpdBy = BaseCls.GlbUserID;
            if (txtDedAcc.Enabled == true) _accUpdBy = BaseCls.GlbUserID;
            if (txtDedCC.Enabled == true) _ccUpdBy = BaseCls.GlbUserID;
            if (txtDedCrd.Enabled == true) _crdUpdBy = BaseCls.GlbUserID;
            if (txtPrepare.Enabled == true) _prepBy = BaseCls.GlbUserID;
            if (txtCheck.Enabled == true) _chkBy = BaseCls.GlbUserID;

            if (_userType == "ACC")
            {
                PBonusVouHeader _PBVouHdr = new PBonusVouHeader();

                _PBVouHdr.Pbph_gross = Convert.ToDecimal(txtGross.Text);
                _PBVouHdr.Pbph_manager = txtManager.Text;
                _PBVouHdr.Pbph_net = Convert.ToDecimal(txtNet.Text);
                _PBVouHdr.Pbph_pc = txtProfitCenter.Text;
                _PBVouHdr.Pbph_claim_stus = false;
                _PBVouHdr.Pbph_com = BaseCls.GlbUserComCode;
                _PBVouHdr.Pbph_dt = Convert.ToDateTime(dateTimePickerTransaction.Value).Date;
                _PBVouHdr.Pbph_valid_from = Convert.ToDateTime(dtFromDate.Value).Date;
                _PBVouHdr.Pbph_valid_to = Convert.ToDateTime(dtToDate.Value).Date;
                _PBVouHdr.Pbph_print_stus = false;

                _PBVouHdr.Pbph_acc_ded = Convert.ToDecimal(txtDedAcc.Text);
                _PBVouHdr.Pbph_acc_ref = txtAccRef.Text;
                _PBVouHdr.Pbph_acc_rem = txtAccRem.Text;

                _PBVouHdr.Pbph_adj = 0;

                _PBVouHdr.Pbph_refund = Convert.ToDecimal(txtRefund.Text);
                _PBVouHdr.Pbph_refund_1 = 0;
                _PBVouHdr.Pbph_refund_rem = txtRefRem.Text;
                _PBVouHdr.Pbph_refund_upd_by = BaseCls.GlbUserID; 

                _PBVouHdr.Pbph_auth_by = "";
                _PBVouHdr.Pbph_chk_by = "";

                _PBVouHdr.Pbph_acc_upd_by = BaseCls.GlbUserID;
                _PBVouHdr.Pbph_prep_by = BaseCls.GlbUserID;
                _PBVouHdr.Pbph_cc_ded = 0;
                _PBVouHdr.Pbph_crd_ded = 0;
                _PBVouHdr.PBPH_CONFIRM_ACC = true;

                _PBVouHdrList.Add(_PBVouHdr);

                MasterAutoNumber _auto = new MasterAutoNumber();
                _auto.Aut_cate_cd = BaseCls.GlbUserComCode;
                _auto.Aut_cate_tp = "COM";
                _auto.Aut_start_char = "PBVOU";
                _auto.Aut_moduleid = "PBVOU";
                _auto.Aut_direction = 1;
                _auto.Aut_year = null;
                _auto.Aut_modify_dt = null;


                Int32 x = CHNLSVC.Financial.UpdatePBonusVoucher(_auto, txtProfitCenter.Text, _PBVouHdrList, _PBVouDetList, _PBVouDedList, _PBVouRefList, out _docNo);
                Int32 y = CHNLSVC.Financial.ConfirmPBonusVoucher(_docNo, 1, 0, 0);

                MessageBox.Show("Successfully Saved. Voucher No - " + _docNo, "Update...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else     //credit or inventory
            {
                Int32 z = CHNLSVC.Financial.UpdatePBDeductions(txtRef.Text, Convert.ToDecimal(txtGross.Text), Convert.ToDecimal(txtDedCC.Text), txtCCRem.Text, BaseCls.GlbUserID, Convert.ToDecimal(txtDedCrd.Text), txtCrdRem.Text, BaseCls.GlbUserID, Convert.ToDecimal(txtNet.Text), _userType, _PBVouDedList);
                MessageBox.Show("Successfully Updated", "Update...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            pnlLoc.Enabled = true;
            btnClear_Click(null, null);

        }

        private void calc()
        {
            txtNet.Text = (Convert.ToDecimal(txtGross.Text) + Convert.ToDecimal(txtRefund.Text) - Convert.ToDecimal(txtDedCrd.Text) - Convert.ToDecimal(txtDedCC.Text) - Convert.ToDecimal(txtDedAcc.Text)).ToString();
        }

        private void txtRefund_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRefund.Text))
            {
                calc();
            }
        }

        private void txtDedAcc_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDedAcc.Text))
            {
                calc();
            }
        }

        private void txtDedCC_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDedCC.Text))
            {
                calc();
            }
        }

        private void txtDedCrd_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDedCrd.Text))
            {
                calc();
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRef.Text))
            {
                MessageBox.Show("Select the voucher", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            if (_userType == "CC")
            {
                Int32 y = CHNLSVC.Financial.ConfirmPBonusVoucher(txtRef.Text, 0, 1, 0);
            }
            if (_userType == "CRD")
            {
                Int32 y = CHNLSVC.Financial.ConfirmPBonusVoucher(txtRef.Text, 0, 0, 1);
            }
            
            MessageBox.Show("Successfully Confirmed", "Update...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnClear_Click(null, null);

        }


        private void btnClear_Click(object sender, EventArgs e)
        {
            txtGross.Text = "0";
            txtNet.Text = "0";
            txtDedAcc.Text = "0";
            txtDedCC.Text = "0";
            txtDedCrd.Text = "0";
            txtAccRem.Text = "";
            txtCCRem.Text = "";
            txtCrdRem.Text = "";
            txtRefund.Text = "0";
            txtRefRem.Text = "";
            txtPrepare.Text = "";
            txtCheck.Text = "";
            //txtAutho.Text = "";
            lblStus.Text = "-";
            txtCircular.Text = "";
            cmbScheme.SelectedIndex = -1;
            txtProfitCenter.Text = "";
            txtPCDesc.Text = "";
            txtManager.Text = "";
            txtRef.Text = "";
            txtAccRef.Text = "";
            txtRef.Enabled = true;
            btn_Srch_Vou.Enabled = true;
            pnlCirc.Enabled = false;
            pnlCircDt.Enabled = false;
            pnlLoc.Enabled = true;
            pnlDeduction.Visible = false;
            pnlRef.Visible = false;
            lblAccStus.Text = "-";
            lblCCStus.Text = "-";
            lblCrdStus.Text = "-";
            _PBVouDetList = new List<PBonusVouDetail>();
            _PBVouHdrList = new List<PBonusVouHeader>();
            _PBVouDedList = new List<PBonusVouDedc>();
            _PBVouRefList = new List<PBonusVouDedc>();
            grv.AutoGenerateColumns = false;
            gvDed.AutoGenerateColumns = false;
            gvRef.AutoGenerateColumns = false;
            grv.DataSource = _PBVouDetList;
            gvDed.DataSource = _PBVouDedList;
            gvRef.DataSource = _PBVouRefList;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            Reports.Finance.ReportViewerFinance _view = new Reports.Finance.ReportViewerFinance();
            _view.GlbReportName = "Prod_Bonus_Voucher.rpt";
            BaseCls.GlbReportName = "Prod_Bonus_Voucher.rpt";
            _view.GlbReportDoc = txtRef.Text; ;
            BaseCls.GlbReportDoc = txtRef.Text;
            BaseCls.GlbReportProfit = txtProfitCenter.Text;
            _view.Show();
            _view = null;
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            //DataTable param = new DataTable();
            //DataRow dr;

            //param.Columns.Add("circular", typeof(string));
            //param.Columns.Add("scheme", typeof(string));
            //param.Columns.Add("value", typeof(Decimal));

            //dr = param.NewRow();
            //dr["circular"] = txtCircular.Text;
            //dr["scheme"] = cmbScheme.Text;
            //dr["value"] = Convert.ToDecimal(lblValue.Text);
            //param.Rows.Add(dr);

            int rowIndex = -1;
            foreach (DataGridViewRow row in grv.Rows)
            {
                if (row.Cells[2].Value.ToString().Equals(cmbScheme.SelectedValue.ToString()))
                {
                    rowIndex = row.Index;
                    return;
                }
            }


            PBonusVouDetail _PBVouDet = new PBonusVouDetail();
            _PBVouDet.Pbpd_circ = txtCircular.Text;
            _PBVouDet.Pbpd_sch = cmbScheme.Text;
            _PBVouDet.Pbpd_value = Convert.ToDecimal(lblValue.Text);

            _PBVouDetList.Add(_PBVouDet);

            grv.AutoGenerateColumns = false;
            grv.DataSource = new List<PBonusVouDetail>();
            grv.DataSource = _PBVouDetList;

            txtGross.Text = (Convert.ToDecimal(txtGross.Text) + Convert.ToDecimal(lblValue.Text)).ToString("0.00");
            calc();
            pnlLoc.Enabled = false;
        }

        private void btn_New_Click(object sender, EventArgs e)
        {
            btnClear_Click(null, null);
            txtRef.Text = "";
            txtRef.Enabled = false;
            btn_Srch_Vou.Enabled = false;
            pnlCirc.Enabled = true;
            pnlCircDt.Enabled = true;
            pnlLoc.Enabled = true;
            btnRef.Enabled = true;
            btnAccDed.Enabled = true;
        }

        private void cmbScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            Load_PB_Vou_Det();
        }

        private void grv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtGross.Text = (Convert.ToDecimal(txtGross.Text) - _PBVouDetList[e.RowIndex].Pbpd_value).ToString("0.00");
                    _PBVouDetList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _PBVouDetList;
                    grv.DataSource = _source;
                    calc();

                }
            }
        }


        private void btn_Srch_Vou_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PBVoucher);
            DataTable _result = CHNLSVC.CommonSearch.GetPBonusVoucherData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRef;
            _CommonSearch.txtSearchbyword.Text = txtRef.Text;
            _CommonSearch.ShowDialog();
            txtRef.Focus();

            txtRef_Leave(null, null);
        }

        private void txtRef_Leave(object sender, EventArgs e)
        {
            //load

            DataTable _dt = CHNLSVC.Financial.Get_PBVoucher_detail(txtRef.Text);
            grv.AutoGenerateColumns = false;
            grv.DataSource = _dt;

            DataTable _dtH = CHNLSVC.Financial.Get_PBVoucher_header(txtRef.Text);
            if (_dt != null && _dt.Rows.Count > 0)
            {
                txtGross.Text = _dtH.Rows[0]["pbph_gross"].ToString();
                txtManager.Text = _dtH.Rows[0]["Pbph_manager"].ToString();
                txtNet.Text = _dtH.Rows[0]["Pbph_net"].ToString();
                txtProfitCenter.Text = _dtH.Rows[0]["Pbph_pc"].ToString();
                dateTimePickerTransaction.Text = _dtH.Rows[0]["Pbph_dt"].ToString();
                txtDedAcc.Text = _dtH.Rows[0]["Pbph_acc_ded"].ToString();
                txtAccRef.Text = _dtH.Rows[0]["Pbph_acc_ref"].ToString();
                txtAccRem.Text = _dtH.Rows[0]["Pbph_acc_rem"].ToString();
                txtRefund.Text = _dtH.Rows[0]["Pbph_refund"].ToString();

                txtRefRem.Text = _dtH.Rows[0]["Pbph_refund_rem"].ToString();

                txtPrepare.Text = _dtH.Rows[0]["pbph_prep_by"].ToString();
                txtAutho.Text = _dtH.Rows[0]["pbph_auth_by"].ToString();
                txtCheck.Text = _dtH.Rows[0]["pbph_chk_by"].ToString();

                txtDedCC.Text = _dtH.Rows[0]["Pbph_cc_ded"].ToString();
                txtCCRem.Text = _dtH.Rows[0]["Pbph_cc_rem"].ToString();

                txtDedCrd.Text = _dtH.Rows[0]["Pbph_crd_ded"].ToString();
                txtCrdRem.Text = _dtH.Rows[0]["Pbph_crd_rem"].ToString();

                if (_dtH.Rows[0]["pbph_confirm_acc"].ToString() == "0") { lblAccStus.Text = "Pending"; } else { lblAccStus.Text = "Confirmed"; }
                if (_dtH.Rows[0]["pbph_confirm_crd"].ToString() == "0") { lblCrdStus.Text = "Pending"; } else { lblCrdStus.Text = "Confirmed"; }
                if (_dtH.Rows[0]["pbph_confirm_cc"].ToString() == "0") { lblCCStus.Text = "Pending"; } else { lblCCStus.Text = "Confirmed"; }

                if (_dtH.Rows[0]["PBPH_CONFIRM_CHK_BY"].ToString() == "0") { lblChkStus.Text = "Pending"; } else { lblChkStus.Text = "Confirmed"; }
                if (_dtH.Rows[0]["PBPH_CONFIRM_AUTH_BY"].ToString() == "0") { lblAuthStus.Text = "Pending"; } else { lblAuthStus.Text = "Confirmed"; }

                dtFrom.Value = Convert.ToDateTime(_dtH.Rows[0]["pbph_valid_from"]);
                dtTo.Value = Convert.ToDateTime(_dtH.Rows[0]["pbph_valid_to"]);

                btnRef.Enabled = false;
                btnAccDed.Enabled = false;
                btnCrdDed.Enabled = false;
                btnCCDed.Enabled = false;

                if (_userType == "ACC") //ACC
                {
                    btnSave.Enabled = false;
                    btnConfirm.Enabled = false;

                    if (_checkUser == true)
                    {
                        btnChkConfirm.Enabled = true;
                        if (_dtH.Rows[0]["pbph_confirm_chk_by"].ToString() == "0") { btnChkConfirm.Enabled = true; } else { btnChkConfirm.Enabled = false; }
                    }
                    if (_authUser == true)
                    {
                        btnAuthConfirm.Enabled = true;
                        if (_dtH.Rows[0]["pbph_confirm_auth_by"].ToString() == "0") { btnAuthConfirm.Enabled = true; } else { btnAuthConfirm.Enabled = false; }
                    }

                }
                if (_userType == "CC")  //CC
                {
                    if (lblCCStus.Text == "Pending")
                    {
                        btnSave.Enabled = true;
                        btnConfirm.Enabled = true;
                        txtCCRem.Enabled = true;
                        btnCCDed.Enabled = true;
                    }
                }
                if (_userType == "CRD")  //CRD
                {
                    if (lblCrdStus.Text == "Pending")
                    {
                        btnSave.Enabled = true;
                        btnConfirm.Enabled = true;
                        txtCrdRem.Enabled = true;
                        btnCrdDed.Enabled = true;
                    }
                }
            }

        }

        private void txtRef_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtRef_Leave(null, null);

            if (e.KeyCode == Keys.F2)
                btn_Srch_Vou_Click(null, null);
        }

        private void txtRef_DoubleClick(object sender, EventArgs e)
        {
            btn_Srch_Vou_Click(null, null);
        }

        private void btn_srch_ded_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Deduction);
            DataTable _result = CHNLSVC.CommonSearch.GetDeductionRefData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtDedCode;
            _CommonSearch.txtSearchbyword.Text = txtDedCode.Text;
            _CommonSearch.ShowDialog();
            txtDedCode.Focus();

            txtDedCode_Leave(null, null);
        }

        private void txtDedCode_Leave(object sender, EventArgs e)
        {
            DataTable dt = CHNLSVC.General.GetDeductionDet(Convert.ToInt32(txtDedCode.Text));
            if (dt.Rows.Count != 0)
            {
                txtDed.Text = dt.Rows[0]["ded_desc"].ToString();
            }
            else
            {
                txtDed.Text = "";
            }
        }

        private void btn_loc_close_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDeduction.Visible = false;
            }
            catch (Exception ex)
            {
                pnlDeduction.Visible = false;
                //MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning); CHNLSVC.CloseChannel();
            }

        }

        private void btnAccDed_Click(object sender, EventArgs e)
        {
            pnlDeduction.Visible = true;
            gvDed.AutoGenerateColumns = false;
            gvDed.DataSource = _PBVouDedList;
        }

        private void btnCrdDed_Click(object sender, EventArgs e)
        {
            pnlDeduction.Visible = true;
            gvDed.AutoGenerateColumns = false;
            gvDed.DataSource = _PBVouDedList;
        }

        private void btnCCDed_Click(object sender, EventArgs e)
        {
            pnlDeduction.Visible = true;
            gvDed.AutoGenerateColumns = false;
            gvDed.DataSource = _PBVouDedList;
        }

        private void btnAddDed_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDedCode.Text))
            {
                MessageBox.Show("Select the deduction type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtDedAmt.Text))
            {
                MessageBox.Show("Enter the amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = -1;
            foreach (DataGridViewRow row in gvDed.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(txtDedCode.Text))
                {
                    rowIndex = row.Index;
                    return;
                }
            }

            string _uTp = "";
            if (_userType == "ACC")
            {
                _uTp = "A";
                txtDedAcc.Text = (Convert.ToDecimal(txtDedAcc.Text) + Convert.ToDecimal(txtDedAmt.Text)).ToString();
            }
            else if (_userType == "CC")
            {
                _uTp = "S";
                txtDedCC.Text = (Convert.ToDecimal(txtDedCC.Text) + Convert.ToDecimal(txtDedAmt.Text)).ToString();
            }
            else if (_userType == "CRD")
            {
                _uTp = "C";
                txtDedCrd.Text = (Convert.ToDecimal(txtDedCrd.Text) + Convert.ToDecimal(txtDedAmt.Text)).ToString();
            }
            PBonusVouDedc _PBVouDed = new PBonusVouDedc();
            _PBVouDed.Pbpdd_ded_cd = Convert.ToInt32(txtDedCode.Text);
            _PBVouDed.Pbpdd_tp = _uTp;
            _PBVouDed.Pbpdd_desc = txtDed.Text;
            _PBVouDed.Pbpdd_amt = Convert.ToDecimal(txtDedAmt.Text);

            _PBVouDedList.Add(_PBVouDed);

            gvDed.AutoGenerateColumns = false;
            gvDed.DataSource = new List<PBonusVouDedc>();
            gvDed.DataSource = _PBVouDedList;

            txtDedCode.Text = "";
            txtDed.Text = "";
            txtDedAmt.Text = "";
        }

        private void gvDed_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (_userType == "ACC")
                    {
                        txtDedAcc.Text = (Convert.ToDecimal(txtDedAcc.Text) - _PBVouDedList[e.RowIndex].Pbpdd_amt).ToString("0.00");
                    }
                    else if (_userType == "CC")
                    {
                        txtDedCC.Text = (Convert.ToDecimal(txtDedCC.Text) - _PBVouDedList[e.RowIndex].Pbpdd_amt).ToString("0.00");
                    }
                    else if (_userType == "CRD")
                    {
                        txtDedCrd.Text = (Convert.ToDecimal(txtDedCrd.Text) - _PBVouDedList[e.RowIndex].Pbpdd_amt).ToString("0.00");
                    }

                    _PBVouDedList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _PBVouDedList;
                    gvDed.DataSource = _source;
                }
            }
        }

        private void btnRef_Click(object sender, EventArgs e)
        {
            pnlRef.Visible = true;
            gvRef.AutoGenerateColumns = false;
            gvRef.DataSource = _PBVouRefList;
        }

        private void btn_Srch_Ref_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Refund);
            DataTable _result = CHNLSVC.CommonSearch.GetRefundRefData(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtRefCode;
            _CommonSearch.txtSearchbyword.Text = txtRefCode.Text;
            _CommonSearch.ShowDialog();
            txtDedCode.Focus();

            txtRefCode_Leave(null, null);
        }

        private void txtRefCode_Leave(object sender, EventArgs e)
        {
            DataTable dt = CHNLSVC.General.GetDeductionDet(Convert.ToInt32(txtRefCode.Text));
            if (dt.Rows.Count != 0)
            {
                txtRefDesc.Text = dt.Rows[0]["ded_desc"].ToString();
            }
            else
            {
                txtRefDesc.Text = "";
            }
        }

        private void btnAddRef_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRefCode.Text))
            {
                MessageBox.Show("Select the refund type", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtRefAmt.Text))
            {
                MessageBox.Show("Enter the amount", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int rowIndex = -1;
            foreach (DataGridViewRow row in gvRef.Rows)
            {
                if (row.Cells[1].Value.ToString().Equals(txtRefCode.Text))
                {
                    rowIndex = row.Index;
                    return;
                }
            }


            txtRefund.Text = (Convert.ToDecimal(txtRefund.Text) + Convert.ToDecimal(txtRefAmt.Text)).ToString();

            PBonusVouDedc _PBVouRef = new PBonusVouDedc();
            _PBVouRef.Pbpdd_ded_cd = Convert.ToInt32(txtRefCode.Text);
            _PBVouRef.Pbpdd_desc = txtRefDesc.Text;
            _PBVouRef.Pbpdd_amt = Convert.ToDecimal(txtRefAmt.Text);

            _PBVouRefList.Add(_PBVouRef);

            gvRef.AutoGenerateColumns = false;
            gvRef.DataSource = new List<PBonusVouDedc>();
            gvRef.DataSource = _PBVouRefList;

            txtRefCode.Text = "";
            txtRefDesc.Text = "";
            txtRefAmt.Text = "";
        }

        private void btn_ref_close_Click(object sender, EventArgs e)
        {
            try
            {
                pnlRef.Visible = false;
            }
            catch (Exception ex)
            {
                pnlRef.Visible = false;

            }
        }

        private void gvRef_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Are you Sure?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    txtRefund.Text = (Convert.ToDecimal(txtRefund.Text) - _PBVouRefList[e.RowIndex].Pbpdd_amt).ToString("0.00");

                    _PBVouRefList.RemoveAt(e.RowIndex);
                    BindingSource _source = new BindingSource();
                    _source.DataSource = _PBVouRefList;
                    gvRef.DataSource = _source;
                }
            }
        }

        private void txtRefAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnRef_Click(null, null);
            }
        }

        private void txtDedAmt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAccDed_Click(null, null);
            }
        }

        private void txtRefAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnChkConfirm_Click(object sender, EventArgs e)
        {
            if (lblChkStus.Text == "Confirmed")
            {
                MessageBox.Show("Already confirmed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (lblAccStus.Text == "Pending" || lblCCStus.Text == "Pending" || lblCrdStus.Text == "Pending")
            {
                MessageBox.Show("Cannot process. All department not confirmed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            Int16 X = CHNLSVC.Financial.confirmPBVoucher(BaseCls.GlbUserComCode,txtProfitCenter.Text, txtRef.Text, 1, 0);
            MessageBox.Show("Successfully Confirmed", "Update...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnClear_Click(null, null);
        }

        private void btnAuthConfirm_Click(object sender, EventArgs e)
        {
            if (lblAuthStus.Text == "Confirmed")
            {
                MessageBox.Show("Already confirmed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (lblChkStus.Text == "Pending")
            {
                MessageBox.Show("Checked by not confirmed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (lblAccStus.Text == "Pending" || lblCCStus.Text == "Pending" || lblCrdStus.Text == "Pending")
            {
                MessageBox.Show("Cannot process. All department not confirmed", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Are you sure?", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            Int16 X = CHNLSVC.Financial.confirmPBVoucher(BaseCls.GlbUserComCode, txtProfitCenter.Text, txtRef.Text, 0, 1);
     
            MessageBox.Show("Successfully Confirmed", "Update...", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnClear_Click(null, null);
        }

        private void btnSrchManager_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.EmployeeEPF);
                DataTable _result = CHNLSVC.CommonSearch.Get_employee_EPF(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtManCode;
                _CommonSearch.txtSearchbyword.Text = txtManCode.Text;
                _CommonSearch.ShowDialog();
                txtManCode.Focus();

                get_Man_Name();

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

        private void get_Man_Name()
        {
            txtManager.Text = "";
            try
            {
                DataTable _dt = CHNLSVC.Sales.GetEmployee(BaseCls.GlbUserComCode, txtManCode.Text);
                if (_dt != null && _dt.Rows.Count > 0)
                {
                    txtManager.Text = _dt.Rows[0]["ESEP_FIRST_NAME"].ToString() + ' ' + _dt.Rows[0]["ESEP_LAST_NAME"].ToString();
                }
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



    }
}
