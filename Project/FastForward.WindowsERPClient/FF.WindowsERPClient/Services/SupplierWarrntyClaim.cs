using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Services
{
    public partial class SupplierWarrntyClaim : Base
    {

        string _docSts = string.Empty;
        string _docNo = "";
        string _jobStage = "2";
        Int32 _serId = 0;
        string _oldSer = string.Empty;
        private DataTable _CompanyItemStatus = null;
        private MasterItem _itemdetail = null;
        bool _isDecimalAllow = false;
        bool _isSerialize = false;
        int _ispart = 0;
        Int16 _jobLine = 1;
        string _invNo = "";
        bool _isRecall = false;
        public SupplierWarrntyClaim()
        {
            InitializeComponent();
            txtPC.Text = BaseCls.GlbUserDefLoca;
            UserPermissionforSuperUser();
            LoadCachedObjects();
            BindUserCompanyItemStatusDDLData(cmbStatus);
            BindUserCompanyItemStatusDDLData(cmbItemStatus);

            _claimItemList = new List<Service_job_Det>();


            dtpFrom.Value = dtpTo.Value.Date.AddMonths(-1);
            txtFrom.Value = txtTo.Value.Date.AddMonths(-1);
            getPendingJobs();

        }
        private void SystemErrorMessage(Exception ex)
        {
            CHNLSVC.CloseChannel();
            this.Cursor = Cursors.Default;
            MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        #region Common Searching Area
        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Currency:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Supplier:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ItemStatus:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PurchaseOrder:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.UserLocation:
                    {
                        if (!(string.IsNullOrEmpty(txtPC.Text)))
                        {
                            paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator + txtPC.Text + seperator);
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserID + seperator + BaseCls.GlbUserComCode + seperator);
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceSupWCNno:
                    {
                        if (chkApp.Checked == true)
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + "H");
                        }
                        else
                        {
                            paramsText.Append(BaseCls.GlbUserComCode + seperator + "P");
                        }
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceClaimSupplier:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtSuppMain.Text + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceJobSearch:
                    {
                        // paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPC.Text + seperator + _jobStage + seperator);
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPC.Text + seperator + string.Empty + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.JobSerial:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Brand:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Brand.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PartCode:
                    {
                        paramsText.Append("" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.PartCode.ToString() + seperator + "" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + null + seperator + null + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Loc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.SerialAvb:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + txtPC.Text + seperator + txtItem.Text.ToUpper() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceDetailSearials:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator + null + seperator + "SERIAL1" + seperator + null + seperator + dtpFrom.Value.ToString("dd-MM-yyyy") + seperator + dtpTo.Value.ToString("dd-MM-yyyy") + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.ServiceTaskCate:
                    {
                        paramsText.Append("ACTIVE" + seperator + BaseCls.GlbUserComCode + seperator + BaseCls.GlbUserDefLoca + seperator);
                        break;
                    }

                default:
                    break;
            }

            return paramsText.ToString();
        }
        #endregion
        
        //test comment
        private void SupplierWarrntyClaim_Load(object sender, EventArgs e)
        {



        }

        private void loadSuppDetails(string sup)
        {
            try
            {
                if (!string.IsNullOrEmpty(sup))
                {
                    if (!CHNLSVC.Inventory.IsValidSupplier(BaseCls.GlbUserComCode, sup, 1, "S"))
                    {
                        // MessageBox.Show("Invalid supplier code.", "Purchase Return", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSuppMain.Text = "";
                        txtSuppMain.Focus();
                        return;
                    }
                    else
                    {
                        MasterBusinessEntity _supDet = new MasterBusinessEntity();
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, sup, null, null, "S");

                        if (_supDet.Mbe_cd == null)
                        {
                            MessageBox.Show("Invalid supplier code.", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSuppMain.Text = "";
                            lblSupplierName.Text = "";
                            lblEmail.Text = "";
                            lblSupplierName.Focus();
                            return;
                        }
                        else
                        {
                            lblSupplierName.Text = _supDet.Mbe_name;
                            lblEmail.Text = _supDet.Mbe_email;
                            if (_supDet.Mbe_email == "NULL")
                            {
                                lblEmail.Text = "N/A";
                            }
                        }

                    }

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
            // List<MasterBusinessEntity> lstsup = CHNLSVC.Inventory.GetServiceAgent("S");
            //if (lstsup != null)
            //{
            //   List<MasterBusinessEntity> loadsup = (from _sup in lstsup
            //                   where _sup.Mbe_cd == sup && _sup.Mbe_com == BaseCls.GlbUserComCode && _sup.Mbe_act == true
            //                   select _sup).ToList<MasterBusinessEntity>();

            //    DataTable dt = loadsup.ToDataTable();

            //    //lblSupplierName.Text = dt.Rows[0]["MBE_NAME"].ToString();
            //    lblSupplierName.Text = loadsup[0].Mbe_name;
            //    string email = loadsup[0].Mbe_email;
            //    if (email == "NULL")
            //    {
            //        lblEmail.Text = "N/A";
            //    }
            //    else
            //    {
            //        lblEmail.Text = loadsup[0].Mbe_email;
            //    }



            //}
        }
        private void loadClaimSuppDetails(string sup)
        {
            //List<MasterBusinessEntity> lstsup = CHNLSVC.Inventory.GetServiceAgent("S");

            //MasterBusinessEntity _supDet = new MasterBusinessEntity();
            //_supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, sup, null, null, "S");
            //if (lstsup != null)
            //{
            //    List<MasterBusinessEntity> loadsup = (from _sup in lstsup
            //                                          where _sup.Mbe_cd == sup && _sup.Mbe_com == BaseCls.GlbUserComCode && _sup.Mbe_act == true
            //                                          select _sup).ToList<MasterBusinessEntity>();

            //    DataTable dt = loadsup.ToDataTable();


            //    lblClaimSupp.Text = loadsup[0].Mbe_name;
            //    string email = loadsup[0].Mbe_email;
            //    if (email == "NULL")
            //    {
            //        lblsupemail.Text = "N/A";
            //    }
            //    else
            //    {
            //        lblsupemail.Text = loadsup[0].Mbe_email;
            //    }



            //}

            try
            {
                if (!string.IsNullOrEmpty(sup))
                {
                    if (!CHNLSVC.Inventory.IsValidSupplier(BaseCls.GlbUserComCode, sup, 1, "S"))
                    {
                        // MessageBox.Show("Invalid supplier code.", "Purchase Return", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSuppMain.Text = "";
                        txtSuppMain.Focus();
                        return;
                    }
                    else
                    {
                        MasterBusinessEntity _supDet = new MasterBusinessEntity();
                        _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, sup, null, null, "S");

                        if (_supDet.Mbe_cd == null)
                        {
                            MessageBox.Show("Invalid supplier code.", "Supplier Calim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtSuppClaim.Text = "";
                            lblClaimSupp.Text = "";
                            lblsupemail.Text = "";

                            txtSuppClaim.Focus();
                            return;
                        }
                        else
                        {

                            lblClaimSupp.Text = _supDet.Mbe_name;
                            lblsupemail.Text = _supDet.Mbe_email;
                            if (_supDet.Mbe_email == "NULL")
                            {
                                lblsupemail.Text = "N/A";
                            }
                        }

                    }

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
        private MasterAutoNumber _ReqAppAuto = null;
        private Service_WCN_Hdr _ReqSupHdr = null;
        private List<Service_WCN_Detail> _ReqClaimDet = null;
        private Int32 _seq = 0;


        protected Int32 CollectReqApp(string _Sts, int _type)
        {
            Int32 _ItemLine = 1;
            _ReqSupHdr = new Service_WCN_Hdr();
            Service_WCN_Detail _tempReqClaim = new Service_WCN_Detail();
            _ReqClaimDet = new List<Service_WCN_Detail>();


            _ReqAppAuto = new MasterAutoNumber();

            _ReqSupHdr.Swc_com = BaseCls.GlbUserComCode;
            _ReqSupHdr.Swc_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            if (_Sts == "P")
            {
                _ReqSupHdr.Swc_seq_no = 0;
                _ReqSupHdr.Swc_doc_no = null;
            }
            else
            {
                _ReqSupHdr.Swc_seq_no = _seq;
                _ReqSupHdr.Swc_doc_no = txtWcnNo.Text;
            }
            _ReqSupHdr.Swc_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqSupHdr.Swc_tp = _type;
            _ReqSupHdr.Swc_com = BaseCls.GlbUserComCode;
            if (_Sts == "I")// 01-10-2015
            {
                _ReqSupHdr.Swc_loc = txtPC.Text;
            }
            else
            {
                _ReqSupHdr.Swc_loc = BaseCls.GlbUserDefLoca;
            }
            _ReqSupHdr.Swc_supp = txtSuppMain.Text;
            _ReqSupHdr.Swc_clm_supp = txtSuppClaim.Text;
            _ReqSupHdr.Swc_supp_tp = "S";
            _ReqSupHdr.Swc_othdocno = txtRequestNo.Text;
            _ReqSupHdr.Swc_rmks = txtReqRemarks.Text;
            _ReqSupHdr.Swc_air_bill_no = txtShipDocNo.Text;
            _ReqSupHdr.Swc_bill_dt = Convert.ToDateTime(dtpBillDate.Value).Date;
            _ReqSupHdr.Swc_eta = Convert.ToDateTime(dtp_ETA.Value).Date;
            _ReqSupHdr.Swc_ispick = 0;
            _ReqSupHdr.Swc_stus = _Sts;
            _ReqSupHdr.Swc_cre_by = BaseCls.GlbUserID;
            _ReqSupHdr.Swc_cre_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqSupHdr.Swc_mod_by = BaseCls.GlbUserID;
            _ReqSupHdr.Swc_mod_dt = Convert.ToDateTime(DateTime.Now).Date;
            _ReqSupHdr.Swc_isemail = 0;
            _ReqSupHdr.Swc_order_no = txtOrderNo.Text;
            _ReqSupHdr.SWC_HOLD_REASON = txtReason.Text;

            _ItemLine = 0;
            if (_claimItemList.Count > 0)
            {
                foreach (Service_job_Det item in _claimItemList)
                {
                    if (item.Jbd_select == true)
                    {
                        _ItemLine = _ItemLine + 1;
                        _tempReqClaim = new Service_WCN_Detail();
                        _tempReqClaim.SWD_SEQ_NO = 0;
                        _tempReqClaim.SWD_LINE = _ItemLine;
                        _tempReqClaim.SWD_DOC_NO = null;
                        _tempReqClaim.SWD_ITMCD = item.Jbd_itm_cd;
                        _tempReqClaim.SWD_SUPPITMCD = item.Jbd_itm_cd;
                        _tempReqClaim.SWD_SER1 = item.Jbd_ser1;
                        _tempReqClaim.SWD_SERID = Convert.ToInt32(item.Jbd_ser_id);
                        _tempReqClaim.SWD_WARRNO = item.Jbd_warr;
                        _tempReqClaim.SWD_SUPPWARRNO = item.Jbd_warr;
                        _tempReqClaim.SWD_OEMSERNO = item.Jbd_oem_no;
                        _tempReqClaim.SWD_CASEID = item.Jbd_case_id;
                        _tempReqClaim.SWD_SETTLED = 0;
                        _tempReqClaim.SWD_OTHDOCNO = txtRequestNo.Text;
                        _tempReqClaim.SWD_ISWCRN = 0;
                        _tempReqClaim.SWD_ISJOBCLOSE = 0;
                        _tempReqClaim.SWD_JOBNO = item.Jbd_jobno;
                        _tempReqClaim.SWD_JOBLINE = item.Jbd_jobline;
                        _tempReqClaim.SWD_OLDPARTSEQ = 0;
                        _tempReqClaim.SWD_QTY = 1;
                        _tempReqClaim.SWD_ITM_STUS = item.Jbd_itm_stus;
                        _tempReqClaim.SWD_OLD_SER = item.Jbd_serold;
                        _tempReqClaim.SWD_IS_PART = item.Isold_part;
                        _tempReqClaim.SWD_IS_STOCK = item.jbd_isstockupdate;

                        //Akila 2018/02/21
                        if (_Sts == "I") { _tempReqClaim.Original_Swd_line = item.Swd_Line; }
                        if (_Sts == "L")
                        {
                            _tempReqClaim.SWD_DOC_NO = txtWcnNo.Text;
                        }
                        _tempReqClaim.SWD_JOBNO_PRV = item.Jbd_mainjobno;
                        _tempReqClaim.SWD_JOBLINE_PRV = item.Jbd_oldjobline;
                        _tempReqClaim.old_pt_seq = item.Jbd_seq_no;
                        if (_Sts == "I")
                        {
                            Service_Chanal_parameter oPara = CHNLSVC.General.GetChannelParamers(BaseCls.GlbUserComCode, txtPC.Text);
                            if (oPara != null && oPara.sp_wcn_chk_cls_job == 1)
                            {
                                List<Service_job_Det> oItms;
                                oItms = CHNLSVC.CustService.GetJobDetails(item.Jbd_jobno, item.Jbd_jobline, BaseCls.GlbUserComCode);
                                if (oItms.Count > 0)
                                {
                                    if (oItms[0].Jbd_stage >= 6)
                                    {
                                        _tempReqClaim.Swd_need_chk = 1;
                                        _ReqSupHdr.Swc_need_chk = 1;
                                    }
                                }
                            }


                        }

                        _ReqClaimDet.Add(_tempReqClaim);
                    }
                }
            }
            if (_ReqClaimDet.Count == 0)
            {

                return 0;
            }

            _ReqAppAuto = new MasterAutoNumber();
            _ReqAppAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
            _ReqAppAuto.Aut_cate_tp = "PC";
            _ReqAppAuto.Aut_direction = 1;
            _ReqAppAuto.Aut_modify_dt = null;
            if (_type == 0)
            {
                _ReqAppAuto.Aut_moduleid = "SUPCL";
                _ReqAppAuto.Aut_number = 0;
                _ReqAppAuto.Aut_start_char = "SUPCL";
            }
            else
            {
                _ReqAppAuto.Aut_moduleid = "SUPCR";
                _ReqAppAuto.Aut_number = 0;
                _ReqAppAuto.Aut_start_char = "SUPCR";
            }
            _ReqAppAuto.Aut_year = Convert.ToDateTime(dtpClaimDate.Text).Year;

            return 1;
        }

        private void getJobDetails(Int32 lineNo, string _jobNo)
        {
            Service_JOB_HDR _scvjobHdr = new Service_JOB_HDR();
            List<Service_job_Det> _scvItemList = null;
            List<Service_Job_Defects> _scvDefList = null;
            List<Service_Tech_Aloc_Hdr> _scvEmpList = null;
            List<Service_Job_Det_Sub> _scvItemSubList = null;
            List<Service_Job_Det_Sub> _tempItemSubList = null;
            List<Service_TempIssue> _scvStdbyList = null;

            _scvItemList = new List<Service_job_Det>();
            _scvDefList = new List<Service_Job_Defects>();
            _scvEmpList = new List<Service_Tech_Aloc_Hdr>();
            _scvItemSubList = new List<Service_Job_Det_Sub>();
            _tempItemSubList = new List<Service_Job_Det_Sub>();
            _scvStdbyList = new List<Service_TempIssue>();

            int _returnStatus = 0;
            string _returnMsg = string.Empty;
            _returnStatus = CHNLSVC.CustService.GetScvJob(BaseCls.GlbUserComCode, _jobNo, out _scvjobHdr, out  _scvItemList, out  _scvItemSubList, out  _scvDefList, out  _scvEmpList, out  _scvStdbyList, out  _returnMsg);


            if (_returnStatus != 1)
            {
                SystemInformationMessage(_returnMsg, "Service Job");

                return;
            }


            txtNIC.Text = _scvjobHdr.SJB_B_NIC;
            txtCustMobile.Text = _scvjobHdr.SJB_B_MOBINO;
            txtCustCD.Text = _scvjobHdr.SJB_B_CUST_CD;
            txtCustName.Text = _scvjobHdr.SJB_B_CUST_NAME;
            //  cmbTitle.Text = _scvjobHdr.SJB_B_CUST_TIT;
            txtCustAddress.Text = _scvjobHdr.SJB_B_ADD1;
            txtCustAddress2.Text = _scvjobHdr.SJB_B_ADD2;
            txtCustTown.Text = _scvjobHdr.SJB_B_ADD3;
            txtCustTown.Tag = _scvjobHdr.SJB_B_TOWN;
            txtCustTelephone.Text = _scvjobHdr.SJB_B_PHNO;
            txtEmail.Text = _scvjobHdr.SJB_B_EMAIL;
            txtjContact.Text = _scvjobHdr.SJB_CNT_PERSON;
            //txtContNo.Text = _scvjobHdr.SJB_CNT_PHNO;
            //txtContLoc.text = _scvjobHdr.SJB_CNT_ADD1;

            lblJobDt.Text = Convert.ToString(_scvjobHdr.SJB_CRE_DT);

            SystemUser _user = CHNLSVC.Security.GetUserByUserID(_scvjobHdr.SJB_CRE_BY);

            if (_user != null && !string.IsNullOrEmpty(_user.Se_usr_id))
            {
                txtjobUser.Text = _user.Se_usr_name;
            }

            Service_Tech_Aloc_Hdr _Tech = CHNLSVC.CustService.GetAllocationDet(BaseCls.GlbUserComCode, _scvjobHdr.SJB_JOBNO);
            if (_Tech != null)
            {
                txtTechName.Text = _Tech.ESEP_FIRST_NAME;
            }
            List<Service_job_Det> ojob_Det = CHNLSVC.CustService.GetJobDetails(_jobNo, lineNo, BaseCls.GlbUserComCode);

            FillItemDetails(ojob_Det[0]);

        }



        private void FillItemDetails(Service_job_Det _warrItem)
        {
            MasterBusinessEntity _supDet = new MasterBusinessEntity();
            //txtJobItem.Text = _warrItem.Jbd_itm_cd;
            //lblItemDesc.Text = _warrItem.Jbd_itm_desc;
            //lblModel.Text = _warrItem.Jbd_model;
            //lblBrand.Text = _warrItem.Jbd_brand;
            //txtJobSerial.Text = _warrItem.Jbd_ser1;
            //lblWarNo.Text = _warrItem.Jbd_warr;
            string _warrStatus = string.Empty;

            if (_warrItem.Jbd_warrstartdt.AddMonths(_warrItem.Jbd_warrperiod).Date >= CHNLSVC.Security.GetServerDateTime().Date)
            {
                _warrStatus = "UNDER WARRANTY"; lblWarStus.ForeColor = Color.Green;
            }
            else
            {
                _warrStatus = "OVER WARRANTY"; lblWarStus.ForeColor = Color.Red;
            }

            lblWarStus.Text = _warrStatus;
            lblWarStart.Text = _warrItem.Jbd_warrstartdt.ToString("dd-MMM-yyyy");
            lblWarEnd.Text = _warrItem.Jbd_warrstartdt.AddMonths(_warrItem.Jbd_warrperiod).ToString("dd-MMM-yyyy");
            lblWarPrd.Text = _warrItem.Jbd_warrperiod.ToString();
            lblWarRemain.Text = (_warrItem.Jbd_warrstartdt.AddMonths(_warrItem.Jbd_warrperiod).Date - CHNLSVC.Security.GetServerDateTime().Date).TotalDays.ToString();
            lblWarRem.Text = _warrItem.Jbd_warrrmk;

            if (lblWarRemain.Text.Contains("-"))
            {
                lblWarRemain.Text = "0";
            }

            lblInv.Text = _warrItem.Jbd_invc_no;

            if (_warrItem.Jbd_date_pur < Convert.ToDateTime("01-01-1800"))
            {
                lblInvDt.Text = "";
            }
            else
            {
                lblInvDt.Text = _warrItem.Jbd_date_pur.ToString("dd-MMM-yyyy");
            }
            //lblAccNo.Text = _warrItem.Irsm_acc_no;
            //lblDelLoc.Text = _warrItem.Irsm_loc;
            //lblDelLocDesc.Text = _warrItem.Irsm_loc_desc;

            MasterItem _itemdetail = new MasterItem();
            _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _warrItem.Jbd_itm_cd);
            //txtDes.Text =   _itemdetail.Mi_shortdesc;
            //txtitembrand.Text = _itemdetail.Mi_brand ;
            //txtModel.Text = _itemdetail.Mi_model;
            txtjitem.Text = _warrItem.Jbd_itm_cd;
            txtjitemDes.Text = _itemdetail.Mi_shortdesc;
            txtjbrand.Text = _itemdetail.Mi_brand;
            txtjmodel.Text = _itemdetail.Mi_model;
            txtjSerial.Text = _warrItem.Jbd_ser1;
            //  txtjProductCode.Text=  _warrItem.Jbd_part_cd;
            txtjProductCode.Text = _warrItem.Jbd_ser2;
            //txtMobile.Text = _warrItem.Irsm_cust_mobile;
            //txtCustCode.Text = _warrItem.Irsm_cust_cd;
            //txtCusName.Text = _warrItem.Irsm_cust_name;
            //txtAddress1.Text = _warrItem.Irsm_cust_addr;
            txtjDefect.Text = "";
            DataTable _tblJobDef = CHNLSVC.CustService.getServicejobDef(_warrItem.Jbd_jobno, _warrItem.Jbd_jobline);
            foreach (DataRow r in _tblJobDef.Rows)
            {
                if ((string)r["srd_stage"] == "W")
                {
                    if (string.IsNullOrEmpty(txtjDefect.Text))
                    {
                        txtjDefect.Text = (string)r["SDT_DESC"];
                    }
                    else
                    {
                        txtjDefect.Text = txtjDefect.Text + " " + "\n" + (string)r["SDT_DESC"];
                    }
                }
            }

            Service_job_Det oJobDetaill;
            List<Service_Job_Defects> oJobDefects;
            List<Service_Enquiry_TechAllo_Hdr> oJobAllocations;
            List<Service_Enquiry_Tech_Cmnt> oTechComments;
            List<Service_Enquiry_StandByItems> oStandByItems;
            string msg;
            Decimal totalAmount = 0;
            List<Tuple<string, string, string>> ConRemark_Type_User = new List<Tuple<string, string, string>>();
            dgvActualDefectsD3.DataSource = new List<Service_Job_Defects>();
            dgvActualDefectsD3.AutoGenerateColumns = false;
            int result = CHNLSVC.CustService.GetAllJobDetailsEnquiry(_warrItem.Jbd_jobno, _warrItem.Jbd_jobline, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, out oJobDetaill, out oJobDefects, out oJobAllocations, out oTechComments, out ConRemark_Type_User, out oStandByItems, out msg, out totalAmount);
            if (result > 0)
            {
                if (oJobDefects.FindAll(x => x.SRD_STAGE == "W").Count > 0)
                {
                    dgvActualDefectsD3.AutoGenerateColumns = false;
                    dgvActualDefectsD3.DataSource = oJobDefects.FindAll(x => x.SRD_STAGE == "W");
                }
            }

            lblSuppCode.Text = _warrItem.Jbd_supp_cd;


            _supDet = CHNLSVC.Sales.GetBusinessCompanyDetail(BaseCls.GlbUserComCode, lblSuppCode.Text, null, null, "S");

            if (_supDet.Mbe_cd != null)
            {
                lblSuppName.Text = _supDet.Mbe_name;
            }


            //lblItmTp.Text = _warrItem.Jbd_itmtp;
            // lblItemCat.Text = (_itemdetail == null) ? string.Empty : _itemdetail.Mi_cate_1;

            int _returnStatus = 0;
            string _returnMsg = string.Empty;
            InventorySerialMaster VehicleObect = null;
            List<InventorySerialMaster> _warrMst = new List<InventorySerialMaster>();
            List<InventorySubSerialMaster> _warrMstSub = new List<InventorySubSerialMaster>();
            Dictionary<List<InventorySerialMaster>, List<InventorySubSerialMaster>> _warrMstDic = null;

            InventorySerialMaster veh = new InventorySerialMaster();

            if (txtJobSerial.Text == "N/A")
            {
                _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(null, "", null, txtWarrno.Text.Trim(), "", "", 0, out _returnStatus, out _returnMsg);
            }
            else
            {
                _warrMstDic = CHNLSVC.CustService.GetWarrantyMaster(null, "", txtJobSerial.Text.Trim(), null, "", "", 0, out _returnStatus, out _returnMsg);
            }

            if (_warrMstDic != null)
            {
                //SystemInformationMessage("There is no warranty details available.", "No warranty");
                //txtRegNo.Clear();
                //txtRegNo.Focus();
                //return;


                foreach (KeyValuePair<List<InventorySerialMaster>, List<InventorySubSerialMaster>> pair in _warrMstDic)
                {
                    _warrMst = pair.Key;
                    _warrMstSub = pair.Value;

                }

                VehicleObect = veh;
                veh = _warrMst[0];
                txtWarrno.Text = veh.Irsm_warr_no;
                lblSuppWara.Text = Convert.ToString(veh.Irsm_sup_warr_pd);
                lblSupWarRem.Text = veh.Irsm_sup_warr_rem;

                lblWarStartSup.Text = veh.Irsm_sup_warr_stdt.ToString("dd-MMM-yyyy");
                lblWarEndSup.Text = veh.Irsm_sup_warr_stdt.AddMonths(veh.Irsm_sup_warr_pd).ToString("dd-MMM-yyyy");

                //    lblSuppWara.Text = row["MWP_SUP_WARRANTY_PRD"].ToString();
                //    lblSupWarRem.Text = row["MWP_SUP_WARA_REM"].ToString();


                if (_warrItem.Jbd_date_pur < Convert.ToDateTime("01-01-1800"))
                {
                    lblInvDt.Text = "";
                }
                else
                {
                    lblInvDt.Text = veh.Irsm_invoice_dt.Date.ToString("dd-MMM-yyyy");
                }
            }
        }

        #region Common Message


        private void SystemInformationMessage(string _msg, string _title)
        { this.Cursor = Cursors.Default; MessageBox.Show(_msg, _title, MessageBoxButtons.OK, MessageBoxIcon.Information); }

        #endregion


        #region Payments
        private void LoadCurrancyCodes()
        {
            MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
            List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);
            if (_cur != null)
            {
                BindingSource _source = new BindingSource();
                _source.DataSource = _cur;
                cmbCurrancy.DataSource = _source;
                cmbCurrancy.DisplayMember = "Mcr_cd";
                cmbCurrancy.ValueMember = "Mcr_cd";

                cmbCurrancy.SelectedValue = _pc.Mpc_def_exrate;
            }
        }
        #endregion
        //private void Process()
        //{
        //    bool _allowCurrentTrans = false;
        //    if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, txtPC.Text, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpClaimDate, label4, dtpClaimDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
        //    {
        //        if (_allowCurrentTrans == true)
        //        {
        //            if (dtpClaimDate.Value.Date != DateTime.Now.Date)
        //            {
        //                dtpClaimDate.Enabled = true;
        //                MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                dtpClaimDate.Focus();
        //                return;
        //            }
        //        }
        //        else
        //        {
        //            dtpClaimDate.Enabled = true;
        //            MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //            dtpClaimDate.Focus();
        //            return;
        //        }
        //    }

        //    List<ReptPickSerials> reptPickSerialsList = new List<ReptPickSerials>();

        //    #region Fill InventoryHeader
        //    InventoryHeader inHeader = new InventoryHeader();
        //    DataTable dt_location = CHNLSVC.Inventory.Get_location_by_code(BaseCls.GlbUserComCode, txtPC.Text );
        //    foreach (DataRow r in dt_location.Rows)
        //    {
        //        // Get the value of the wanted column and cast it to string
        //        inHeader.Ith_sbu = (string)r["ML_OPE_CD"];
        //        if (System.DBNull.Value != r["ML_CATE_2"])
        //        {
        //            inHeader.Ith_channel = (string)r["ML_CATE_2"];
        //        }
        //        else
        //        {
        //            inHeader.Ith_channel = string.Empty;
        //        }
        //    }
        //    inHeader.Ith_acc_no = string.Empty;
        //    inHeader.Ith_anal_1 = "";
        //    inHeader.Ith_anal_2 = "";
        //    inHeader.Ith_anal_3 = "";
        //    inHeader.Ith_anal_4 = "";
        //    inHeader.Ith_anal_5 = "";
        //    inHeader.Ith_anal_6 = 0;
        //    inHeader.Ith_anal_7 = 0;
        //    inHeader.Ith_anal_8 = DateTime.MinValue;
        //    inHeader.Ith_anal_9 = DateTime.MinValue;
        //    inHeader.Ith_anal_10 = false;
        //    inHeader.Ith_anal_11 = false;
        //    inHeader.Ith_anal_12 = false;
        //    inHeader.Ith_bus_entity = "";
        //    inHeader.Ith_cate_tp  = "SERVICE";
        //    inHeader.Ith_com = BaseCls.GlbUserComCode;
        //    inHeader.Ith_com_docno = "";
        //    inHeader.Ith_cre_by = BaseCls.GlbUserID;
        //    inHeader.Ith_cre_when = DateTime.Now;
        //    inHeader.Ith_del_add1 = "";
        //    inHeader.Ith_del_add2 = "";
        //    inHeader.Ith_del_code = "";
        //    inHeader.Ith_del_party = "";
        //    inHeader.Ith_del_town = "";

        //     inHeader.Ith_direct = true;// +

        //    inHeader.Ith_doc_date = dtpClaimDate.Value.Date;
        //    inHeader.Ith_doc_no = string.Empty;
        //    inHeader.Ith_doc_tp = "AOD";
        //    inHeader.Ith_doc_year = dtpClaimDate.Value.Date.Year;
        //    inHeader.Ith_entry_no = string.Empty;
        //    inHeader.Ith_entry_tp  = string.Empty;
        //    inHeader.Ith_git_close = true;
        //    inHeader.Ith_git_close_date = DateTime.MinValue;
        //    inHeader.Ith_git_close_doc = string.Empty;
        //    inHeader.Ith_isprinted = false;
        //    inHeader.Ith_is_manual = false;
        //    inHeader.Ith_job_no = _job;
        //    inHeader.Ith_loading_point = string.Empty;
        //    inHeader.Ith_loading_user = string.Empty;
        //    inHeader.Ith_loc = txtPC.Text;
        //    inHeader.Ith_manual_ref = "N/A";
        //    inHeader.Ith_mod_by = BaseCls.GlbUserID;
        //    inHeader.Ith_mod_when = DateTime.Now;
        //    inHeader.Ith_noofcopies = 0;
        //    inHeader.Ith_oth_loc = string.Empty;
        //    inHeader.Ith_oth_docno = "N/A";
        //    inHeader.Ith_remarks = string.Empty;
        //    //inHeader.Ith_seq_no = 6; removed by Chamal 12-05-2013
        //    inHeader.Ith_session_id = BaseCls.GlbUserSessionID;
        //    inHeader.Ith_stus = "A";
        //    inHeader.Ith_sub_tp = "NOR";
        //    inHeader.Ith_vehi_no = string.Empty;

        //    #endregion
        //    #region Fill InventorySerials
        //    ReptPickSerials _pick = new ReptPickSerials();

        //    _pick.Tus_base_doc_no = _aodoutNo;
        //    _pick.Tus_base_itm_line = Convert.ToInt16(_dr["ITS_ITM_LINE"]);
        //    _pick.Tus_batch_line = Convert.ToInt16(_dr["ITS_BATCH_LINE"]);
        //    _pick.Tus_bin = _errLocBin;
        //    _pick.Tus_com = BaseCls.GlbUserComCode;
        //    _pick.Tus_cre_by = BaseCls.GlbUserName;
        //    _pick.Tus_cre_dt = System.DateTime.Now;
        //    _pick.Tus_cross_batchline = Convert.ToInt16(_dr["ITS_BATCH_LINE"]);
        //    _pick.Tus_cross_itemline = Convert.ToInt16(_dr["ITS_ITM_LINE"]);
        //    _pick.Tus_cross_seqno = Convert.ToInt32(_dr["ITS_SEQ_NO"]);
        //    _pick.Tus_cross_serline = Convert.ToInt16(_dr["ITS_SER_LINE"]);
        //    _pick.Tus_doc_dt = System.DateTime.Now.Date;
        //    //_pick.Tus_doc_no = _aodoutNo;

        //    _pick.Tus_exist_grncom = _dr["ITS_EXIST_GRNCOM"] == DBNull.Value ? string.Empty : (String)_dr["ITS_EXIST_GRNCOM"];
        //    _pick.Tus_exist_grnno = _dr["ITS_EXIST_GRNNO"] == DBNull.Value ? string.Empty : (String)_dr["ITS_EXIST_GRNNO"];
        //    _pick.Tus_exist_grndt = _dr["ITS_EXIST_GRNDT"] == DBNull.Value ? DateTime.MinValue : (DateTime)_dr["ITS_EXIST_GRNDT"];
        //    _pick.Tus_exist_supp = _dr["ITS_EXIST_SUPP"] == DBNull.Value ? string.Empty : (String)_dr["ITS_EXIST_SUPP"];
        //    _pick.Tus_itm_stus = (String)_dr["ITS_ITM_STUS"];
        //    _pick.Tus_unit_price = Convert.ToDecimal(_dr["ITB_UNIT_PRICE"]);

        //    _pick.Tus_itm_brand = _itmlist.Mi_brand;
        //    _pick.Tus_itm_cd = (String)_dr["ITS_ITM_CD"];
        //    _pick.Tus_itm_desc = _itmlist.Mi_longdesc;
        //    _pick.Tus_itm_line = Convert.ToInt16(_dr["ITS_ITM_LINE"]);
        //    _pick.Tus_itm_model = _itmlist.Mi_model;
        //    _pick.Tus_loc = _errLoc;
        //    _pick.Tus_new_remarks = String.Empty;
        //    _pick.Tus_new_status = String.Empty;

        //    _pick.Tus_orig_grncom = _dr["ITS_ORIG_GRNCOM"] == DBNull.Value ? string.Empty : (String)_dr["ITS_ORIG_GRNCOM"];
        //    _pick.Tus_orig_grndt = _dr["ITS_ORIG_GRNDT"] == DBNull.Value ? DateTime.MinValue : (DateTime)_dr["ITS_ORIG_GRNDT"];
        //    _pick.Tus_orig_grnno = _dr["ITS_ORIG_GRNNO"] == DBNull.Value ? string.Empty : (String)_dr["ITS_ORIG_GRNNO"];
        //    _pick.Tus_orig_supp = _dr["ITS_ORIG_SUPP"] == DBNull.Value ? string.Empty : (String)_dr["ITS_ORIG_SUPP"];

        //    //_pick.Tus_out_date = DateTime.Now.Date;
        //    _pick.Tus_qty = 1;
        //    _pick.Tus_seq_no = 0;
        //    _pick.Tus_ser_1 = _dr["ITS_SER_1"] == DBNull.Value ? string.Empty : (String)_dr["ITS_SER_1"];
        //    _pick.Tus_ser_2 = _dr["ITS_SER_2"] == DBNull.Value ? string.Empty : (String)_dr["ITS_SER_2"];
        //    _pick.Tus_ser_3 = _dr["ITS_SER_3"] == DBNull.Value ? string.Empty : (String)_dr["ITS_SER_3"];
        //    _pick.Tus_ser_4 = _dr["ITS_SER_4"] == DBNull.Value ? string.Empty : (String)_dr["ITS_SER_4"];
        //    _pick.Tus_ser_id = Convert.ToInt32(_dr["ITS_SER_ID"]);
        //    _pick.Tus_ser_line = Convert.ToInt16(_dr["ITS_SER_LINE"]);
        //    _pick.Tus_serial_id = String.Empty;
        //    _pick.Tus_session_id = _sessionID;
        //    _pick.Tus_unit_cost = Convert.ToDecimal(_dr["ITS_UNIT_COST"]);

        //    //_pick.Tus_usrseq_no = _seqAODIn;
        //    _pick.Tus_warr_no = _dr["ITS_WARR_NO"] == DBNull.Value ? string.Empty : (String)_dr["ITS_WARR_NO"];
        //    _pick.Tus_warr_period = Convert.ToInt16(_dr["ITS_WARR_PERIOD"]);

        //    reptPickSerialsList.Add(_pick);

        //    #endregion

        //}
        private void searchSupplier_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Supplier);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSuppMain;
                _CommonSearch.ShowDialog();
                txtSuppMain.Select();
                loadSuppDetails(txtSuppMain.Text.Trim());
                txtSuppClaim.Focus();
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

        private void txtSuppMain_DoubleClick(object sender, EventArgs e)
        {
            searchSupplier_Click(null, null);
        }

        private void txtSuppMain_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F2)
                {
                    searchSupplier_Click(null, null);
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    // loadSuppDetails(txtSuppMain.Text.Trim());
                    txtSuppClaim.Focus();
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

        private void txtSuppMain_Leave(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtSuppMain.Text))
                {
                    if (!CHNLSVC.Inventory.IsValidSupplier(BaseCls.GlbUserComCode, txtSuppMain.Text, 1, "S"))
                    {
                        MessageBox.Show("Invalid supplier code.", "Supplier Warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSuppMain.Text = "";
                        txtSuppMain.Focus();
                        return;
                    }
                    else
                    {
                        loadSuppDetails(txtSuppMain.Text.Trim());
                        txtSuppClaim.Focus();
                    }

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
        List<Service_job_Det> _claimItemList = new List<Service_job_Det>();
        List<Service_job_Det> _claimItemListdbt = null;
        //   private List<Service_job_Det> _claimItemListDist = null;
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //if (string.IsNullOrEmpty(txtSuppMain.Text))
                //{ MessageBox.Show("Please select the supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return;   }

                if (string.IsNullOrEmpty(txtPC.Text))
                { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }

                _claimItemListdbt = new List<Service_job_Det>();
                if (chkApp.Checked == false)
                {
                    _claimItemListdbt = CHNLSVC.CustService.getSupplierClaimRequest(BaseCls.GlbUserComCode, txtPC.Text, txtSuppMain.Text, txtjobNo.Text, txtpartno.Text, txtSerial.Text, txtBrand.Text, txtItemCode.Text, dtpFrom.Value, dtpTo.Value, txtTaskLoc.Text, 1);
                }
                else// Hold Request
                {
                    _claimItemListdbt = CHNLSVC.CustService.getSupplierClaimRequest(BaseCls.GlbUserComCode, txtPC.Text, txtSuppMain.Text, txtjobNo.Text, txtpartno.Text, txtSerial.Text, txtBrand.Text, txtItemCode.Text, dtpFrom.Value, dtpTo.Value, txtTaskLoc.Text, 3);

                }
                if (_claimItemListdbt != null)
                {

                    _claimItemList.AddRange(_claimItemListdbt);
                    // _claimItemList = _claimItemListDist.Distinct().ToList();

                }
                else
                {
                    MessageBox.Show("No data found", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }
                if (_claimItemList != null)
                {
                    var selectedList = _claimItemList.OrderBy(m => m.Jbd_reqwcndt).Select(m => new
                    {
                        m.Jbd_jobno,
                        m.Jbd_itm_cd,
                        m.Jbd_itm_desc,
                        m.Jbd_ser1,
                        m.Jbd_reqwcn,
                        m.Jbd_sentwcn,
                        m.Jbd_recwcn,
                        m.Jbd_takewcn,
                        m.Jbd_supp_cd,
                        m.Jbd_part_cd,
                        m.Jbd_oem_no,
                        m.Jbd_case_id,
                        m.Sjb_dt,
                        m.Jbd_itm_stus,
                        m.Jbd_warr,
                        m.Jbd_jobline,
                        m.Jbd_ser_id,
                        m.Jbd_serold,
                        m.Isold_part,
                        m.jbd_isstockupdate,
                        m.Jbd_reqwcndt,
                        m.Jbd_seq_no
                    }).Distinct().ToList();

                    _claimItemList = new List<Service_job_Det>();
                    Service_job_Det _tempReqClaim = new Service_job_Det();
                    foreach (var item in selectedList)
                    {
                        _tempReqClaim = new Service_job_Det();
                        _tempReqClaim.Jbd_jobno = item.Jbd_jobno;
                        _tempReqClaim.Jbd_itm_cd = item.Jbd_itm_cd;
                        _tempReqClaim.Jbd_itm_desc = item.Jbd_itm_desc;
                        _tempReqClaim.Jbd_ser1 = item.Jbd_ser1;
                        _tempReqClaim.Jbd_reqwcn = item.Jbd_reqwcn;
                        _tempReqClaim.Jbd_sentwcn = item.Jbd_sentwcn;
                        _tempReqClaim.Jbd_recwcn = item.Jbd_recwcn;
                        _tempReqClaim.Jbd_takewcn = item.Jbd_takewcn;
                        _tempReqClaim.Jbd_supp_cd = item.Jbd_supp_cd;
                        _tempReqClaim.Jbd_part_cd = item.Jbd_part_cd;
                        _tempReqClaim.Jbd_oem_no = item.Jbd_oem_no;
                        _tempReqClaim.Jbd_case_id = item.Jbd_case_id;
                        _tempReqClaim.Sjb_dt = item.Sjb_dt;
                        _tempReqClaim.Jbd_itm_stus = item.Jbd_itm_stus;
                        _tempReqClaim.Jbd_warr = item.Jbd_warr;
                        _tempReqClaim.Jbd_jobline = item.Jbd_jobline;
                        _tempReqClaim.Jbd_ser_id = item.Jbd_ser_id;
                        _tempReqClaim.Jbd_serold = item.Jbd_serold;
                        _tempReqClaim.Isold_part = item.Isold_part;
                        _tempReqClaim.jbd_isstockupdate = item.jbd_isstockupdate;
                        _tempReqClaim.Jbd_reqwcndt = item.Jbd_reqwcndt;
                        _tempReqClaim.Jbd_mainjobno = item.Jbd_jobno;
                        _tempReqClaim.Jbd_oldjobline = item.Jbd_jobline;
                        _tempReqClaim.Jbd_seq_no = item.Jbd_seq_no;
                        _claimItemList.Add(_tempReqClaim);
                    }

                    //  SelectView(true);
                    dgvClaimDetails.AutoGenerateColumns = false;
                    dgvClaimDetails.DataSource = new List<Service_job_Det>();
                    dgvClaimDetails.DataSource = _claimItemList;
                }
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        private void SelectView(Boolean _sel)
        {
            if (_claimItemList != null)
            {
                foreach (Service_job_Det _jitem in _claimItemList)
                {
                    _jitem.Jbd_select = _sel;
                }
            }
        }



        private void btnSearch_Wcn_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSupWCNno);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierClaimDoc(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtWcnNo;
                _CommonSearch.ShowDialog();
                _CommonSearch.IsSearchEnter = true;
                txtWcnNo.Select();
                txtWcnNo.Focus();
                LoadSupp_Claim();
                btnApprove.Enabled = false;
                btnSave.Enabled = false;
                // btnReceive.Enabled = false;
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

        private void btnSrh_Job_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            //  _jobStage = "3";
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            _CommonSearch.dtpTo.Value = DateTime.Now.Date;
            _CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-1);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtjobNo;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtjobNo.Focus();


        }

        //private void btnClear_Click(object sender, EventArgs e)
        //{
        //    if (MessageBox.Show("Do you need to clear?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //        Clear_Data();
        //}
        private void Clear_Data()
        {
            try
            {
                btnUpdate.Enabled = false;
                _isRecall = false;
                txtAmt.Text = "";
                lblStatus.Text = "";
                lblHold.Text = "0";
                _ReqSupHdr = new Service_WCN_Hdr();
                _ReqAppAuto = new MasterAutoNumber();
                _claimItemList = new List<Service_job_Det>();
                _ReqClaimDet = new List<Service_WCN_Detail>();
                _claimItemList = new List<Service_job_Det>();
                cmbType.SelectedIndex = -1;
                _serId = 0;
                txtSuppMain.Text = string.Empty;
                txtSuppClaim.Text = string.Empty;
                txtjobNo.Text = string.Empty;
                txtpartno.Text = string.Empty;
                txtShipDocNo.Text = string.Empty;
                txtRequestNo.Text = string.Empty;
                txtOrderNo.Text = string.Empty;
                txtReqRemarks.Text = string.Empty;
                lblsupemail.Text = string.Empty;
                lblClaimSupp.Text = string.Empty;
                List<RequestApprovalHeader> _TempReqAppHdr = new List<RequestApprovalHeader>();
                lblSupplierName.Text = "";
                lblEmail.Text = "";
                dgvPendings.DataSource = null;
                dgvClaimDetails.DataSource = null;
                btnApprove.Enabled = true;
                btnReject.Enabled = true;
                btnSave.Enabled = true;
                // btnReceive.Enabled = true;
                txtWcnNo.Text = "";

                txtItem.Text = "";
                txtSerialNo.Text = "";
                txtPart.Text = "";

                txtjitem.Text = "";
                txtjitemDes.Text = "";
                txtjbrand.Text = "";
                txtjmodel.Text = "";
                txtjSerial.Text = "";
                txtjProductCode.Text = "";
                txtjDefect.Text = "";
                txtjContact.Text = "";
                txtjobUser.Text = "";
                txtTechName.Text = "";
                txtEmail.Text = "";
                txtreleaseRem.Text = "";


                //lblItem.Text = "";
                //lblItemDesc.Text = "";
                //lblModel.Text = "";
                //lblBrand.Text = "";
                //lblSerNo.Text = "";
                lblWarStus.Text = "";
                lblWarStart.Text = "";
                lblWarEnd.Text = "";
                lblAttempt.Text = "";
                lblWarPrd.Text = "";
                lblWarRemain.Text = "";
                lblWarRem.Text = "";
                txtNIC.Text = "";
                txtCustMobile.Text = "";
                txtCustCD.Text = "";
                txtCustName.Text = "";
                txtEmail.Text = "";
                //  cmbTitle.Text = _scvjobHdr.SJB_B_CUST_TIT;
                txtCustAddress.Text = "";
                txtCustAddress2.Text = "";
                txtCustTown.Text = "";
                txtCustTown.Tag = "";
                txtCustTelephone.Text = "";
                lblSuppCode.Text = "";
                lblSuppName.Text = "";
                lblJobDt.Text = "";
                txtjob.Text = "";
                lblJobInvDt.Text = "";
                lblInv.Text = "";
                lblInvDt.Text = "";
                //lblWarNo.Text = "";
                //lblAddrss.Text = "";
                //lblStageText.Text = "";

                txtJobItem.Text = "";
                txtJobSerial.Text = "";
                txtOem.Text = "";
                ttxPartNo.Text = "";
                //  txtStatus.Text = "";
                cmbItemStatus.SelectedIndex = -1;
                txtjob.Text = "";
                lblSuppWara.Text = "";
                lblSupWarRem.Text = "";
                txtDes.Text = "";
                txtitembrand.Text = "";
                txtModel.Text = "";
                txtWarrno.Text = "";
                lblWarStartSup.Text = "";
                lblWarEndSup.Text = "";
                txtReason.Text = "";
                ucPayModes1.Amount.Enabled = false;
                ucPayModes1.AddButton.Visible = false;
                ucPayModes1.ClearControls();
                BindUserCompanyItemStatusDDLData(cmbStatus);

                BindUserCompanyItemStatusDDLData(cmbItemStatus);
                dgvPending.Rows.Clear();
                //txtPC.Text = BaseCls.GlbUserDefLoca;
                txt_loc.Text = BaseCls.GlbUserDefLoca;
                txtnJob.Enabled = false;
                schnJob.Enabled = false;
                cmbJobSerial.Enabled = false;
                txtnJob.Text = "";
                cmbJobSerial.SelectedIndex = -1;
                getPendingJobs();
                dgvChkHdr.DataSource = new List<RequestApprovalHeader>();

                dgvChkDet.DataSource = new List<Service_job_Det>();
                txtchkWcn.Text = "";
                txtchkWcn.Text = "";
                txtjobChk.Text = "";




            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }
        private void BindUserCompanyItemStatusDDLData(ComboBox ddl)
        {
            DataTable _tbl = _CompanyItemStatus;
            if (_tbl != null)
            {
                var _s = (from L in _tbl.AsEnumerable()
                          select new
                          {
                              MIS_DESC = L.Field<string>("MIS_DESC"),
                              MIC_CD = L.Field<string>("MIC_CD")
                          }).ToList();
                var _n = new { MIS_DESC = string.Empty, MIC_CD = string.Empty };
                _s.Insert(0, _n);
                ddl.DataSource = _s;
                ddl.DisplayMember = "MIS_DESC";
                ddl.ValueMember = "MIC_CD";
                ddl.Text = "GOOD";
            }
        }


        private void BindUserJobSerialDDLData(ComboBox ddl, string _job)
        {
            if (_job != "")
            {
                DataTable _tbl = CHNLSVC.CustService.getServicejobDet(_job, 0);
                if (_tbl != null)
                {
                    var _s = (from L in _tbl.AsEnumerable()
                              select new
                              {
                                  JBD_SER1 = L.Field<string>("JBD_SER1"),
                                  JBD_JOBLINE = Convert.ToInt32(L.Field<Int64>("JBD_JOBLINE"))


                              }).ToList();
                    var _n = new { JBD_SER1 = string.Empty, JBD_JOBLINE = 0 };
                    _s.Insert(0, _n);
                    ddl.DataSource = _s;
                    ddl.DisplayMember = "JBD_SER1";
                    ddl.ValueMember = "JBD_JOBLINE";

                }
            }
        }

        private void LoadCachedObjects()
        {
            _CompanyItemStatus = CacheLayer.Get<DataTable>(CacheLayer.Key.CompanyItemStatus.ToString());
        }
        private void UserPermissionforSuperUser()
        {
            if (string.IsNullOrEmpty(txtPC.Text))
            { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }
            string _masterLocation = (string.IsNullOrEmpty(txtPC.Text)) ? BaseCls.GlbUserDefProf : txtPC.Text;

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11059) == false)
            {
                chkJob.Enabled = false;

            }
            else
            {
                chkJob.Enabled = true;
            }

            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11058))
            {
                // btnApprove.Enabled = true;

                txtReqRemarks.ReadOnly = false;

                SystemUser _user = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);

                if (_user != null && !string.IsNullOrEmpty(_user.Se_usr_id))
                {
                    btnApprove.Enabled = true;
                    btnRelease.Enabled = true;
                    btnReject.Enabled = true;
                }
                else
                {
                    btnApprove.Enabled = false;
                    txtReqRemarks.ReadOnly = false;
                    btnRelease.Enabled = false;
                    btnReject.Enabled = false;
                }
            }


            if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11060))
            {
                btnReceive.Enabled = false;

                SystemUser _user = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                if (_user != null && !string.IsNullOrEmpty(_user.Se_usr_id))
                {
                }
                else
                {

                    btnReceive.Enabled = true;
                    //  btnApprove.Enabled = true;

                }
            }




        }
        private void LoadSupp_Claim()
        {

            _claimItemList = new List<Service_job_Det>();
            _claimItemList = CHNLSVC.CustService.sp_getSupClaimDetails(txtWcnNo.Text);
            if (_claimItemList != null)
            {
                foreach (Service_job_Det jobitem in _claimItemList)
                {
                    jobitem.Jbd_oldjobline = jobitem.Jbd_jobline;
                    jobitem.Jbd_mainjobno = jobitem.Jbd_jobno;

                }




                _isRecall = true;

                btnUpdate.Enabled = true;
                SelectView(true);
                dgvClaimDetails.AutoGenerateColumns = false;
                dgvClaimDetails.DataSource = new List<Service_job_Det>();
                dgvClaimDetails.DataSource = _claimItemList;


                List<Service_WCN_Hdr> _claimHdrList = new List<Service_WCN_Hdr>();
                _claimHdrList = CHNLSVC.CustService.getWCNHeader_basedRef(BaseCls.GlbUserComCode, txtWcnNo.Text);


                if (_claimHdrList != null)
                {
                    txtSuppMain.Text = _claimHdrList[0].Swc_supp;
                    txtSuppClaim.Text = _claimHdrList[0].Swc_clm_supp;

                    txtRequestNo.Text = _claimHdrList[0].Swc_othdocno;
                    txtReqRemarks.Text = _claimHdrList[0].Swc_rmks;
                    txtShipDocNo.Text = _claimHdrList[0].Swc_air_bill_no;
                    dtpBillDate.Value = Convert.ToDateTime(_claimHdrList[0].Swc_bill_dt).Date;
                    dtp_ETA.Value = Convert.ToDateTime(_claimHdrList[0].Swc_eta).Date;

                    txtOrderNo.Text = _claimHdrList[0].Swc_order_no;
                    txtReason.Text = _claimHdrList[0].SWC_HOLD_REASON;
                }

            }




        }

        private void RecallSupp()
        {
            List<Service_WCN_Hdr> _TempReqAppHdr = new List<Service_WCN_Hdr>();
            _TempReqAppHdr = CHNLSVC.CustService.getWCNHeader(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime("01-jan-1999").Date, Convert.ToDateTime(DateTime.Today).Date, "P", null, null, txtWcnNo.Text);
            if (_TempReqAppHdr != null && _TempReqAppHdr.Count > 0)
            {
                _isRecall = true;
                btnSave.Enabled = true;
                _seq = _TempReqAppHdr[0].Swc_seq_no;
                txtWcnNo.Text = _TempReqAppHdr[0].Swc_doc_no;
                txtSuppMain.Text = _TempReqAppHdr[0].Swc_supp;
                txtSuppClaim.Text = _TempReqAppHdr[0].Swc_clm_supp;
                txtShipDocNo.Text = _TempReqAppHdr[0].Swc_air_bill_no;
                txtRequestNo.Text = _TempReqAppHdr[0].Swc_othdocno;
                dtpBillDate.Text = Convert.ToString(_TempReqAppHdr[0].Swc_bill_dt);
                dtp_ETA.Text = Convert.ToString(_TempReqAppHdr[0].Swc_eta);
                txtOrderNo.Text = _TempReqAppHdr[0].Swc_order_no;
                txtReqRemarks.Text = _TempReqAppHdr[0].Swc_rmks;
                txtReason.Text = _TempReqAppHdr[0].SWC_HOLD_REASON;
            }
        }

        private void txtWcnNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void SearchClaimSupp_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceClaimSupplier);
                DataTable _result = CHNLSVC.CommonSearch.GetClaimSupplierData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSuppClaim;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtSuppClaim.Select();
                if (!string.IsNullOrEmpty(txtSuppClaim.Text))
                {
                    loadClaimSuppDetails(txtSuppClaim.Text.Trim());
                }
                txtSuppClaim.Focus();
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

        private void txtSuppClaim_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSuppMain_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSuppClaim_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (chkJob.Checked == true)
                {
                    txtjobNo.Focus();
                }
                else
                {
                    txtItem.Focus();
                }
            }
            else if (e.KeyCode == Keys.F2)
            {
                SearchClaimSupp_Click(null, null);
            }
        }

        private void txtjobNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtItemCode.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSrh_Job_Click(null, null);
            }
        }

        private void txtpartno_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBrand.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSrh_PartNo_Click(null, null);
            }

        }

        private void dtpFrom_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpTo.Focus();
            }

        }

        private void dtpTo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch.Focus();
            }

        }

        private void txtShipDocNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtRequestNo.Focus();
            }

        }

        private void txtRequestNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                dtpBillDate.Focus();
            }

        }

        private void dtpBillDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtReqRemarks.Focus();
            }

        }



        private void txtOrderNo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (btnSave.Enabled == true)
                {
                    btnSave.Focus();
                }
                if (btnSave.Enabled == true)
                {
                    btnReceive.Focus();
                }
                if (btnApprove.Enabled == true)
                {
                    btnApprove.Focus();
                }

            }

        }

        private void txtOrderNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDocSearch_Click(object sender, EventArgs e)
        {
            pnlType.Visible = false;
            List<Service_WCN_Hdr> _TempReqAppHdr = new List<Service_WCN_Hdr>();
            // btnApprove.Enabled = true;
            string _docSts = string.Empty;
            if (chkApp.Checked == true)
            {
                _docSts = "H";
            }
            if (chkRequest.Checked == true)
            {
                _docSts = "P";
            }
            if (chkRec.Checked == true)
            {
                _docSts = "P";
                pnlType.Visible = true;

            }

            if (string.IsNullOrEmpty(txtPC.Text))
            { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }
            _TempReqAppHdr = CHNLSVC.CustService.getWCNHeader(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(txtFrom.Value).Date, Convert.ToDateTime(txtTo.Value).Date, _docSts, txtSchJob.Text, txtOrder.Text, null);



            dgvPendings.AutoGenerateColumns = false;
            dgvPendings.DataSource = new List<RequestApprovalHeader>();

            if (_TempReqAppHdr == null)
            {
                MessageBox.Show("No any request / approval found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                if (chkRec.Checked == true)
                {
                    btnPayments.Enabled = true;
                }
            }

            dgvPendings.DataSource = _TempReqAppHdr;
        }

        private void dgvPendings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dgvPendings_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            List<Service_supp_claim_itm> Servicesuppclaimitm = new List<Service_supp_claim_itm>();

            try
            {

                if (dgvPendings.RowCount > 0)
                {
                    _isRecall = false;
                    grpSearch.Visible = false;
                    pnlItems.Visible = true;
                    _seq = Convert.ToInt32(dgvPendings.Rows[e.RowIndex].Cells["colSeq"].Value);
                    txtWcnNo.Text = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_reqNo"].Value);
                    txtSuppMain.Text = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["col_supp"].Value);
                    txtSuppClaim.Text = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["colCSup"].Value);
                    txtShipDocNo.Text = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["colAirBill"].Value);
                    txtRequestNo.Text = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["colOdoc"].Value);
                    dtpBillDate.Text = Convert.ToString(dgvPendings.Rows[e.RowIndex].Cells["colBillDate"].Value);
                    if (Convert.ToDateTime(dgvPendings.Rows[e.RowIndex].Cells["colETA_dt"].Value) < Convert.ToDateTime("01/JAN/1900"))
                    {
                        dtp_ETA.Value = Convert.ToDateTime("01/JAN/1900");
                    }
                    else
                    {
                        dtp_ETA.Value = Convert.ToDateTime(dgvPendings.Rows[e.RowIndex].Cells["colETA_dt"].Value);
                    }
                    txtOrderNo.Text = dgvPendings.Rows[e.RowIndex].Cells["colOrder"].Value.ToString();
                    txtReqRemarks.Text = dgvPendings.Rows[e.RowIndex].Cells["colRem"].Value.ToString();
                    _docSts = dgvPendings.Rows[e.RowIndex].Cells["colStatus"].Value.ToString();
                    txtReason.Text = dgvPendings.Rows[e.RowIndex].Cells["col_reason"].Value.ToString();
                    loadSuppDetails(txtSuppMain.Text.Trim());
                    loadClaimSuppDetails(txtSuppClaim.Text.Trim());

                    if (_docSts == "H") lblStatus.Text = "Hold";
                    else if (_docSts == "P") lblStatus.Text = "Pending";
                    else if (_docSts == "I") lblStatus.Text = "Received";
                    else if (_docSts == "F") lblStatus.Text = "Finished";
                    else if (_docSts == "R") lblStatus.Text = "Rejected";
                    else if (_docSts == "C") lblStatus.Text = "Cancelled";


                }
                if (_docSts == "P")
                {
                    // btnApprove.Enabled = false;

                }
                else
                {

                }
                LoadSupp_Claim();
                Servicesuppclaimitm = CHNLSVC.CustService.getSupClaimItems(BaseCls.GlbUserComCode, txtSuppMain.Text, txtSuppClaim.Text);
                if (Servicesuppclaimitm.Count > 0)
                {
                    if (Servicesuppclaimitm[0].SSC_CASH_ALW == 1) { btnPayments.Enabled = true; }
                    else { btnPayments.Enabled = false; }
                }




            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void BindInventoryRequestItemsGridData()
        {
            if (string.IsNullOrEmpty(txtItem.Text))
            {
                txtItem.Focus();
                MessageBox.Show("Please enter item code.", "Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            if (string.IsNullOrEmpty(cmbStatus.Text))
            {
                cmbStatus.Focus();
                MessageBox.Show("Please select a item status.", "Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //if (string.IsNullOrEmpty(txtQty.Text))
            //{
            //    txtQty.Focus();
            //    MessageBox.Show("Please enter required quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //if (IsNumeric(txtQty.Text.Trim()) == false)
            //{
            //    txtQty.Focus();
            //    MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            //if (Convert.ToDecimal(txtQty.Text.ToString()) <= 0)
            //{
            //    txtQty.Focus();
            //    MessageBox.Show("Please enter valid quantity.", "Qty", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);

            if (_item.Mi_is_ser1 == 1)
            {
                if (string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    MessageBox.Show("Enter serial.", "Supplier warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtSerialNo.Focus();
                    return;
                }
            }

            string _mainItemCode = txtItem.Text.Trim().ToUpper();
            string _itemStatus = cmbStatus.SelectedValue.ToString();

            //  decimal _mainItemQty = (string.IsNullOrEmpty(txtQty.Text)) ? 0 : Convert.ToDecimal(txtQty.Text.Trim());


            Service_job_Det _inventoryRequestItem = new Service_job_Det();


            foreach (Service_job_Det jobitem in _claimItemList)
            {
                if (!string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    if ((_claimItemList.FindAll(x => (x.Jbd_itm_cd == txtItem.Text) && (x.Jbd_ser1 == txtSerialNo.Text))).Count > 0)
                    {
                        MessageBox.Show("This serial alreay exist.", "Supplier warranty Claim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerialNo.Focus();
                        break;
                    }
                }

            }




            _inventoryRequestItem.Jbd_itm_cd = txtItem.Text;
            _inventoryRequestItem.Jbd_ser1 = txtSerialNo.Text;
            _inventoryRequestItem.Jbd_ser_id = Convert.ToString(_serId);
            _inventoryRequestItem.Jbd_warr = txtPart.Text;
            _inventoryRequestItem.Jbd_oem_no = string.Empty;
            _inventoryRequestItem.Jbd_case_id = string.Empty;
            _inventoryRequestItem.Jbd_jobno = string.Empty;
            _inventoryRequestItem.Jbd_jobline = 0;
            _inventoryRequestItem.Jbd_itm_stus = cmbStatus.SelectedValue.ToString();
            _inventoryRequestItem.Isold_part = 0;
            _inventoryRequestItem.jbd_isstockupdate = 1;
            _claimItemList.Add(_inventoryRequestItem);
            SelectView(true);
            dgvClaimDetails.AutoGenerateColumns = false;
            dgvClaimDetails.DataSource = new List<Service_job_Det>();
            dgvClaimDetails.DataSource = _claimItemList;
            txtItem.Text = "";
            txtSerialNo.Text = "";
            txtPart.Text = "";
            txtItem.Focus();

        }
        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }






        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void dgvClaimDetails_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void chkRec_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRec.Checked)
            {
                // Check job change permision
                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11069))
                {
                    txtnJob.Enabled = true;
                    schnJob.Enabled = true;
                    cmbJobSerial.Enabled = true;
                }
                else
                {
                    txtnJob.Enabled = false;
                    schnJob.Enabled = false;
                    cmbJobSerial.Enabled = false;
                }


                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11060))
                {
                    // btnApprove.Enabled = true;
                    btnReceive.Enabled = true;
                    btnCancel.Enabled = true;
                    grpSearch.Visible = true;
                    pnlItems.Visible = false;
                    //grpSearch.Height = 145;
                    //grpSearch.Width = 399;
                    BindCombo();
                }
                else
                {
                    btnCancel.Enabled = false;
                    btnReceive.Enabled = false;
                    btnApprove.Enabled = false;
                    btnReject.Enabled = false;
                    chkRec.Checked = false;
                    MessageBox.Show("you don't have permission to receive", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }


                chkApp.Checked = false;
                chkRequest.Checked = false;
                btnSave.Enabled = false;
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
                //   btnReceive.Enabled = true;
                btnEmail.Enabled = false;


            }
            else
            {
                grpSearch.Visible = false;
                pnlItems.Visible = true;

            }

        }

        private void chkApp_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApp.Checked)
            {
                _claimItemList = new List<Service_job_Det>();
                dgvClaimDetails.DataSource = null;

                if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11060))
                {
                    //  btnReceive.Enabled = true;
                    grpSearch.Visible = true;
                    btnRelease.Enabled = true;
                    pnlItems.Visible = false;
                }
                else
                {
                    btnRelease.Enabled = false;
                    btnApprove.Enabled = false;
                    btnReceive.Enabled = false;
                    btnReject.Enabled = false;
                    chkApp.Checked = false;
                    MessageBox.Show("you don't have permission to Release", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }

                chkRec.Checked = false;
                chkRequest.Checked = false;
                btnSave.Enabled = false;
                btnApprove.Enabled = false;
                //  btnReceive.Enabled = false;
                btnEmail.Enabled = false;
            }
        }

        private void chkRequest_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRequest.Checked)
            {
                grpSearch.Visible = true;
                //grpSearch.Height = 145;
                //grpSearch.Width = 399;
                chkRec.Checked = false;
                chkApp.Checked = false;
                btnSave.Enabled = false;
                btnApprove.Enabled = false;
                btnReject.Enabled = false;
                btnReceive.Enabled = false;
                btnEmail.Enabled = true;
            }
            else
            {
                grpSearch.Visible = false;

            }
        }

        private void txtAmt_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAmt_KeyPress(object sender, KeyPressEventArgs e)
        {
            int isNumber = 0;
            e.Handled = !int.TryParse(e.KeyChar.ToString(), out isNumber);
        }





        private void dgvClaimDetails_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataTable dt_warr = new DataTable();
            lblSuppWara.Text = "";
            lblSupWarRem.Text = "";

            if (dgvClaimDetails.Rows.Count > 0 && e.RowIndex != -1)
            {
                btnReceive.Enabled = false;
                pnlWarr.Visible = true;
                pnlItems.Visible = true;
                pnlItems.Width = 398;
                pnlItems.Height = 207;
                pnlWarr.Height = 497;
                txtJobItem.Text = dgvClaimDetails.Rows[e.RowIndex].Cells["clmItemcode"].Value.ToString();
                txtJobSerial.Text = dgvClaimDetails.Rows[e.RowIndex].Cells["clmserial"].Value.ToString();
                txtOem.Text = dgvClaimDetails.Rows[e.RowIndex].Cells["clmOEM"].Value.ToString();
                ttxPartNo.Text = dgvClaimDetails.Rows[e.RowIndex].Cells["clmpartcode"].Value.ToString();
                // txtStatus.Text = dgvClaimDetails.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                cmbItemStatus.SelectedValue = dgvClaimDetails.Rows[e.RowIndex].Cells["Status"].Value.ToString();
                txtjob.Text = dgvClaimDetails.Rows[e.RowIndex].Cells["clmjob"].Value.ToString();
                txtnJob.Text = dgvClaimDetails.Rows[e.RowIndex].Cells["clmjob"].Value.ToString();
                lblJobDt.Text = dgvClaimDetails.Rows[e.RowIndex].Cells["jobDate"].Value.ToString();
                _oldSer = dgvClaimDetails.Rows[e.RowIndex].Cells["Jbd_serold"].Value.ToString();
                _serId = Convert.ToInt32(dgvClaimDetails.Rows[e.RowIndex].Cells["Jbd_ser_id"].Value.ToString());
                _ispart = Convert.ToInt16(dgvClaimDetails.Rows[e.RowIndex].Cells["Isold_part"].Value.ToString());
                _jobLine = Convert.ToInt16(dgvClaimDetails.Rows[e.RowIndex].Cells["Jbd_jobline"].Value.ToString());
                if (string.IsNullOrEmpty(dgvClaimDetails.Rows[e.RowIndex].Cells["supp"].Value.ToString()) == false)
                {
                    txtSuppMain.Text = dgvClaimDetails.Rows[e.RowIndex].Cells["supp"].Value.ToString();

                    txtSuppClaim.Text = txtSuppMain.Text;
                }

                txtAmt.Text = "0";
                Int16 _SEQ = 0;
                List<Service_supp_claim_itm> Servicesuppclaimitm = new List<Service_supp_claim_itm>();
                List<SCV_SUPP_CLAIM_REC> _lstamt = new List<SCV_SUPP_CLAIM_REC>();
                Servicesuppclaimitm = CHNLSVC.CustService.getSupClaimItems(BaseCls.GlbUserComCode, txtSuppMain.Text, txtSuppClaim.Text);
                if (Servicesuppclaimitm.Count > 0)
                {
                    if (Servicesuppclaimitm[0].SSC_CASH_ALW == 1) { _SEQ = Convert.ToInt16(Servicesuppclaimitm[0].SSC_SEQ); _lstamt = CHNLSVC.CustService.GetSuppWaraPayment(_SEQ); }

                }


                if (_lstamt != null && _lstamt.Count > 0)
                {

                    int total = _lstamt.Sum(x => Convert.ToInt32(x.Scc_amt));
                    txtAmt.Text = Convert.ToString(total);
                }


                MasterItem _itemdetail = new MasterItem();
                _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtJobItem.Text);
                if (_itemdetail != null)
                {
                    txtDes.Text = _itemdetail.Mi_shortdesc;
                    txtitembrand.Text = _itemdetail.Mi_brand;
                    txtModel.Text = _itemdetail.Mi_model;
                }
                getJobDetails(_jobLine, txtjob.Text);
                if (chkRec.Checked)
                {
                    txtJobSerial.ReadOnly = false;
                }
                else
                {
                    txtJobSerial.ReadOnly = true;

                }
                //dt_warr = CHNLSVC.Sales.GetItemStatusWiseWarrantyPeriods(dgvClaimDetails.Rows[e.RowIndex].Cells["clmItemcode"].Value.ToString(), dgvClaimDetails.Rows[e.RowIndex].Cells["Status"].Value.ToString());

                //foreach (DataRow row in dt_warr.Rows)
                //{

                //    lblSuppWara.Text = row["MWP_SUP_WARRANTY_PRD"].ToString();
                //    lblSupWarRem.Text = row["MWP_SUP_WARA_REM"].ToString();
                //}

                if (chkRec.Checked == true)
                {
                    if (MessageBox.Show("Are you want to change the serial?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        txtJobSerial.Focus();
                    }
                    else
                    {
                        if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11060))
                        {
                            btnReceive.Enabled = true;
                            //  btnApprove.Enabled = true;
                        }
                    }
                }

            }


        }

        private void txtSuppClaim_DoubleClick(object sender, EventArgs e)
        {
            SearchClaimSupp_Click(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            InvoiceHeader _invheader = new InvoiceHeader();
            List<InvoiceItem> _lstInvItm = new List<InvoiceItem>();
            MasterAutoNumber _invoiceAuto = new MasterAutoNumber();

            if (string.IsNullOrEmpty(txtPC.Text))
            { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }


            bool _allowCurrentTrans = false;
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, txtPC.Text, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpClaimDate, label4, dtpClaimDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtpClaimDate.Value.Date != DateTime.Now.Date)
                    {
                        dtpClaimDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpClaimDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtpClaimDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpClaimDate.Focus();
                    return;
                }
            }



            string _docNo = "";
            if (string.IsNullOrEmpty(txtSuppMain.Text))
            { MessageBox.Show("Please select the supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtSuppClaim.Text))
            { MessageBox.Show("Please select the claim supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (dgvClaimDetails.Rows.Count <= 0)
            { MessageBox.Show("Please select the clim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtShipDocNo.Text))
            { MessageBox.Show("Please select the ship document", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtRequestNo.Text))
            { MessageBox.Show("Please select the request no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtOrderNo.Text))
            { MessageBox.Show("Please select the order no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (CollectReqApp("P", 0) == 0)
            { MessageBox.Show("Enter claim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Cancelled")
            { MessageBox.Show("Request is already cancelled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            //  List<Service_WCN_Hdr> _TempReqAppHdr = new List<Service_WCN_Hdr>();
            //17-07-2015 Nadeeka
            //foreach (Service_WCN_Detail _ser in _ReqClaimDet)
            //{
            //    _TempReqAppHdr = CHNLSVC.CustService.getWCNHeader(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(txtFrom.Value).Date, Convert.ToDateTime(txtTo.Value).Date, "P", _ser.SWD_JOBNO);
            //    if (_TempReqAppHdr != null || _TempReqAppHdr.Count > 0)
            //    {
            //        MessageBox.Show("Pending request avilable for this job. " + _ser.SWD_JOBNO, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
            //    }
            //}



            /////INVOICE
            string _customer = "CASH";
            DataTable _tbl = new DataTable();
            _tbl = CHNLSVC.Sales.LoadWarrantyClaimCompany(BaseCls.GlbUserComCode, txtSuppMain.Text);
            if (_tbl.Rows.Count > 0)
            {
                var _cust = _tbl.AsEnumerable().Select(x => x.Field<string>("SARSP_CUS_CD")).Distinct().ToList();
                _customer = Convert.ToString(_cust);
            }

            Int16 _SEQ = 0;
            List<Service_supp_claim_itm> Servicesuppclaimitm = new List<Service_supp_claim_itm>();
            Servicesuppclaimitm = CHNLSVC.CustService.getSupClaimItems(BaseCls.GlbUserComCode, txtSuppMain.Text, txtSuppClaim.Text);
            if (Servicesuppclaimitm.Count > 0)
            {
                if (Servicesuppclaimitm[0].SSC_CASH_ALW == 1) { _SEQ = Convert.ToInt16(Servicesuppclaimitm[0].SSC_SEQ); }

            }


            List<SCV_SUPP_CLAIM_REC> _lstamt = new List<SCV_SUPP_CLAIM_REC>();
            _lstamt = CHNLSVC.CustService.GetSuppWaraPayment(_SEQ);


            InvoiceItem _tmpInvItm = new InvoiceItem();
            if (_lstamt != null)
            {

                string _invoicePrefix = "";
                _invoicePrefix = CHNLSVC.Sales.GetInvoicePrefix(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, "CRED");

                _invoiceAuto = new MasterAutoNumber();
                _invoiceAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                _invoiceAuto.Aut_cate_tp = "PRO";
                _invoiceAuto.Aut_direction = 1;
                _invoiceAuto.Aut_modify_dt = null;
                _invoiceAuto.Aut_moduleid = "CRED";
                _invoiceAuto.Aut_number = 0;
                _invoiceAuto.Aut_start_char = _invoicePrefix;
                _invoiceAuto.Aut_year = Convert.ToDateTime(DateTime.Now).Year;

                _invheader.Sah_com = BaseCls.GlbUserComCode;
                _invheader.Sah_cre_by = BaseCls.GlbUserID;
                _invheader.Sah_cre_when = DateTime.Now;
                _invheader.Sah_currency = "LKR";
                _invheader.Sah_cus_add1 = string.Empty;
                _invheader.Sah_cus_add2 = string.Empty;
                _invheader.Sah_cus_cd = _customer;
                _invheader.Sah_cus_name = string.Empty;
                _invheader.Sah_d_cust_add1 = "";
                _invheader.Sah_d_cust_add2 = "";
                _invheader.Sah_d_cust_cd = _customer;
                _invheader.Sah_d_cust_name = string.Empty;
                _invheader.Sah_direct = true;
                _invheader.Sah_dt = Convert.ToDateTime(DateTime.Now);
                _invheader.Sah_epf_rt = 0;
                _invheader.Sah_esd_rt = 0;
                _invheader.Sah_ex_rt = 1;
                _invheader.Sah_inv_no = "na";
                _invheader.Sah_inv_sub_tp = "SA";
                _invheader.Sah_inv_tp = "CRED";
                _invheader.Sah_is_acc_upload = false;
                _invheader.Sah_man_ref = txtWcnNo.Text;
                _invheader.Sah_manual = false;//chkManualRef.Checked ? true : false;
                _invheader.Sah_mod_by = BaseCls.GlbUserID;
                _invheader.Sah_mod_when = DateTime.Now;
                _invheader.Sah_pc = BaseCls.GlbUserDefProf;
                _invheader.Sah_pdi_req = 0;
                _invheader.Sah_ref_doc = txtWcnNo.Text;
                _invheader.Sah_remarks = txtReqRemarks.Text;
                _invheader.Sah_sales_chn_cd = "";
                _invheader.Sah_sales_chn_man = "";
                _invheader.Sah_sales_ex_cd = "N/A";
                _invheader.Sah_sales_region_cd = "";
                _invheader.Sah_sales_region_man = "";
                _invheader.Sah_sales_sbu_cd = "";
                _invheader.Sah_sales_sbu_man = "";
                _invheader.Sah_sales_str_cd = "";
                _invheader.Sah_sales_zone_cd = "";
                _invheader.Sah_sales_zone_man = "";
                _invheader.Sah_seq_no = 1;
                _invheader.Sah_session_id = BaseCls.GlbUserSessionID;
                _invheader.Sah_structure_seq = "";
                _invheader.Sah_stus = "A";
                //if (chkDeliverLater.Checked == false || chkDeliverNow.Checked) _invheader.Sah_stus = "D";
                _invheader.Sah_town_cd = "";
                _invheader.Sah_tp = "INV";
                _invheader.Sah_wht_rt = 0;
                _invheader.Sah_direct = true;
                _invheader.Sah_tax_inv = false; // chkTaxPayable.Checked ? true : false;
                _invheader.Sah_anal_11 = 0;// (chkDeliverLater.Checked || chkDeliverNow.Checked) ? 0 : 1;
                _invheader.Sah_del_loc = BaseCls.GlbUserDefLoca; //(chkDeliverLater.Checked == false || chkDeliverNow.Checked) ? BaseCls.GlbUserDefLoca : !string.IsNullOrEmpty(txtDelLocation.Text) ? txtDelLocation.Text : string.Empty;
                _invheader.Sah_grn_com = "";
                _invheader.Sah_grn_loc = "";
                _invheader.Sah_is_grn = false;
                _invheader.Sah_grup_cd = "";
                _invheader.Sah_is_svat = false;// lblSVatStatus.Text == "Available" ? true : false;
                _invheader.Sah_tax_exempted = false; //lblVatExemptStatus.Text == "Available" ? true : false;
                _invheader.Sah_anal_4 = "";
                _invheader.Sah_anal_2 = "SCV";
                _invheader.Sah_anal_6 = "";
                _invheader.Sah_man_cd = "";
                _invheader.Sah_is_dayend = 0;
                _invheader.Sah_remarks = txtReqRemarks.Text.Trim();
                _invheader.Sah_anal_1 = "";

                _invheader.Sah_anal_7 = Convert.ToDecimal(txtAmt.Text); ;// -_totalReceiptAmt; //Total Invoice Amount - Total Receipt AmountBY DARSHANA 3/12/2012
                _invheader.Sah_anal_8 = 0;//Receipt Amount


                _tmpInvItm.Sad_disc_amt = 0;
                _tmpInvItm.Sad_disc_rt = 0;
                _tmpInvItm.Sad_do_qty = 0;
                _tmpInvItm.Sad_fws_ignore_qty = 0;
                _tmpInvItm.Sad_inv_no = "1";
                _tmpInvItm.Sad_itm_cd = _lstamt[0].Scc_code;
                _tmpInvItm.Sad_itm_line = 1;
                _tmpInvItm.Sad_itm_seq = 0;
                _tmpInvItm.Sad_itm_stus = "GOD";
                _tmpInvItm.Sad_itm_tax_amt = 0;
                _tmpInvItm.Sad_itm_tp = "V";
                _tmpInvItm.Sad_job_line = 0;
                _tmpInvItm.Sad_job_no = string.Empty;
                _tmpInvItm.Sad_pb_lvl = "A";
                _tmpInvItm.Sad_pb_price = 0;
                _tmpInvItm.Sad_pbook = "ABANS";
                _tmpInvItm.Sad_qty = 1;
                _tmpInvItm.Sad_seq = 0;
                _tmpInvItm.Sad_srn_qty = 0;
                _tmpInvItm.Sad_tot_amt = Convert.ToDecimal(txtAmt.Text);
                _tmpInvItm.Sad_unit_amt = Convert.ToDecimal(txtAmt.Text);
                _tmpInvItm.Sad_unit_rt = Convert.ToDecimal(txtAmt.Text);
                _tmpInvItm.Sad_uom = "NOS";
                _tmpInvItm.Sad_warr_period = 0;
                _tmpInvItm.Sad_warr_remarks = "";
                _lstInvItm.Add(_tmpInvItm);
            }



            int effet = 0;
            ///////////////





            if (_lstamt != null)
            {
                effet = CHNLSVC.CustService.Save_SupplierClaim(_ReqSupHdr, _ReqClaimDet, _ReqAppAuto, null, null, null, _invheader, _lstInvItm, _invoiceAuto, null, _isRecall, BaseCls.GlbUserSessionID, out _docNo);
            }
            else
            {
                effet = CHNLSVC.CustService.Save_SupplierClaim(_ReqSupHdr, _ReqClaimDet, _ReqAppAuto, null, null, null, null, null, null, null, _isRecall, BaseCls.GlbUserSessionID, out _docNo);

            }
            if (effet > 0)
            {
                //_sendMail("Supplier Warranty Claim", "SEND", txtjobNo.Text, _docNo);
                Clear_Data();
                MessageBox.Show(_docNo);
                // Clear_Data();
            }
            else
                MessageBox.Show("Process terminated. - " + _docNo);

        }

        private void btnReceive_Click(object sender, EventArgs e)
        {
            bool _allowCurrentTrans = false;


            if (string.IsNullOrEmpty(txtPC.Text))
            { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, txtPC.Text, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpClaimDate, label4, dtpClaimDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtpClaimDate.Value.Date != DateTime.Now.Date)
                    {
                        dtpClaimDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpClaimDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtpClaimDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpClaimDate.Focus();
                    return;
                }
            }

            string _docNo = "";
            if (string.IsNullOrEmpty(txtSuppMain.Text))
            { MessageBox.Show("Please select the supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtSuppClaim.Text))
            { MessageBox.Show("Please select the claim supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (dgvClaimDetails.Rows.Count <= 0)
            { MessageBox.Show("Please select the clim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtShipDocNo.Text))
            { MessageBox.Show("Please select the ship document", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtRequestNo.Text))
            { MessageBox.Show("Please select the request no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtOrderNo.Text))
            { MessageBox.Show("Please select the order no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            if (string.IsNullOrEmpty(txtWcnNo.Text) == true)
            {
                MessageBox.Show("Please select request number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(cmbType.Text) == true)
            {
                MessageBox.Show("Please receive type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);

                cmbType.Focus();
                return;
            }

            if (lblStatus.Text == "Received")
            {
                MessageBox.Show("Request is already received.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (lblStatus.Text == "Rejected")
            {
                MessageBox.Show("Request is already rejected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            //if (lblStatus.Text == "Pending")
            //{
            //    MessageBox.Show("Request is already pending.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (lblStatus.Text == "Cancelled")
            { MessageBox.Show("Request is already cancelled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (CollectReqApp("I", 1) == 0)
            { MessageBox.Show("Enter claim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            #region Payments
            RecieptHeader _recHeader = new RecieptHeader();
            MasterAutoNumber _receiptAuto = new MasterAutoNumber();
            string _customer = string.Empty;
            if (ucPayModes1.RecieptItemList != null)
            {
                if (ucPayModes1.RecieptItemList.Count > 0)
                {
                    DataTable _tbl = new DataTable();
                    _tbl = CHNLSVC.Sales.LoadWarrantyClaimCompany(BaseCls.GlbUserComCode, txtSuppMain.Text);
                    if (_tbl.Rows.Count > 0)
                    {
                        var _cust = _tbl.AsEnumerable().Select(x => x.Field<string>("SARSP_CUS_CD")).Distinct().ToList();
                        _customer = Convert.ToString(_cust);
                    }


                    _recHeader.Sar_acc_no = "";
                    _recHeader.Sar_act = true;
                    _recHeader.Sar_com_cd = BaseCls.GlbUserComCode;
                    _recHeader.Sar_comm_amt = 0;
                    _recHeader.Sar_create_by = BaseCls.GlbUserID;
                    _recHeader.Sar_create_when = DateTime.Now;
                    _recHeader.Sar_currency_cd = "LKR";
                    _recHeader.Sar_debtor_add_1 = "";
                    _recHeader.Sar_debtor_add_2 = "";
                    _recHeader.Sar_debtor_cd = _customer;
                    _recHeader.Sar_debtor_name = "";
                    _recHeader.Sar_direct = true;
                    _recHeader.Sar_direct_deposit_bank_cd = "";
                    _recHeader.Sar_direct_deposit_branch = "";
                    _recHeader.Sar_epf_rate = 0;
                    _recHeader.Sar_esd_rate = 0;
                    _recHeader.Sar_is_mgr_iss = false;
                    _recHeader.Sar_is_oth_shop = false;
                    _recHeader.Sar_is_used = false;
                    _recHeader.Sar_manual_ref_no = txtWcnNo.Text;
                    _recHeader.Sar_mob_no = "";
                    _recHeader.Sar_mod_by = BaseCls.GlbUserID;
                    _recHeader.Sar_mod_when = DateTime.Now;
                    _recHeader.Sar_nic_no = "";
                    _recHeader.Sar_oth_sr = "";
                    _recHeader.Sar_prefix = "";
                    _recHeader.Sar_profit_center_cd = BaseCls.GlbUserDefProf;
                    _recHeader.Sar_receipt_date = Convert.ToDateTime(dtpClaimDate.Value);
                    _recHeader.Sar_receipt_no = "na";
                    _recHeader.Sar_receipt_type = "DIR";
                    _recHeader.Sar_ref_doc = _invNo;
                    _recHeader.Sar_remarks = txtReqRemarks.Text;
                    _recHeader.Sar_seq_no = 1;
                    _recHeader.Sar_ser_job_no = "";
                    _recHeader.Sar_session_id = BaseCls.GlbUserSessionID;
                    _recHeader.Sar_tel_no = "";
                    _recHeader.Sar_tot_settle_amt = 0;
                    _recHeader.Sar_uploaded_to_finance = false;
                    _recHeader.Sar_used_amt = 0;
                    _recHeader.Sar_wht_rate = 0;


                    _receiptAuto = new MasterAutoNumber();
                    _receiptAuto.Aut_cate_cd = BaseCls.GlbUserDefProf;
                    _receiptAuto.Aut_cate_tp = "PRO";
                    _receiptAuto.Aut_direction = 1;
                    _receiptAuto.Aut_modify_dt = null;
                    _receiptAuto.Aut_moduleid = "RECEIPT";
                    _receiptAuto.Aut_number = 0;
                    _receiptAuto.Aut_start_char = "DIR";
                    _receiptAuto.Aut_year = Convert.ToDateTime(dtpClaimDate.Text).Year;




                }
            }

            #endregion


            int effet = CHNLSVC.CustService.Save_SupplierClaim(_ReqSupHdr, _ReqClaimDet, _ReqAppAuto, _recHeader, ucPayModes1.RecieptItemList, _receiptAuto, null, null, null, cmbType.SelectedValue.ToString(), _isRecall, BaseCls.GlbUserSessionID, out _docNo);


            if (effet > 0)
            {
                Clear_Data();
                MessageBox.Show(_docNo);
                // Clear_Data();
            }
            else
                MessageBox.Show("Process terminated. - " + _docNo);
        }

        private void btnApprove_Click(object sender, EventArgs e)
        {
            bool _allowCurrentTrans = false;

            if (string.IsNullOrEmpty(txtPC.Text))
            { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, txtPC.Text, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpClaimDate, label4, dtpClaimDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtpClaimDate.Value.Date != DateTime.Now.Date)
                    {
                        dtpClaimDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpClaimDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtpClaimDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpClaimDate.Focus();
                    return;
                }
            }

            string _docNo = "";
            //if (string.IsNullOrEmpty(txtWcnNo.Text) == true)
            //{
            //    MessageBox.Show("Please select request number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (string.IsNullOrEmpty(txtSuppMain.Text))
            { MessageBox.Show("Please select the supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtSuppClaim.Text))
            { MessageBox.Show("Please select the claim supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (dgvClaimDetails.Rows.Count <= 0)
            { MessageBox.Show("Please select the clim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            //if (string.IsNullOrEmpty(txtShipDocNo.Text))
            //{ MessageBox.Show("Please select the ship document", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            //if (string.IsNullOrEmpty(txtRequestNo.Text))
            //{ MessageBox.Show("Please select the request no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            //if (string.IsNullOrEmpty(txtOrderNo.Text))
            //{ MessageBox.Show("Please select the order no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Approved")
            { MessageBox.Show("Request is already approved.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Cancelled")
            { MessageBox.Show("Request is already cancelled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Rejected")
            { MessageBox.Show("Request is already rejected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtReason.Text))
            { MessageBox.Show("Please enter reason for hold", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (CollectReqApp("H", 0) == 0)
            { MessageBox.Show("Enter claim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            int effet = CHNLSVC.CustService.Save_SupplierClaim(_ReqSupHdr, _ReqClaimDet, _ReqAppAuto, null, null, null, null, null, null, null, _isRecall, BaseCls.GlbUserSessionID, out _docNo);
            Clear_Data();
            if (effet > 0)
            {
                MessageBox.Show(" Successfully Held. ");
                // MessageBox.Show(_docNo);
                // Clear_Data();
            }
            else
                MessageBox.Show("Process terminated. - " + _docNo);
        }

        private void btnEmail_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtWcnNo.Text))
            { MessageBox.Show("Please select the supplier Warranty Claim No", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(lblsupemail.Text))
            { MessageBox.Show("Supplier email is invalid", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            string _mail = "This Request is Approved ," + txtWcnNo.Text + Environment.NewLine;
            _mail += "User - " + BaseCls.GlbUserName + " (" + BaseCls.GlbUserID + ")";
            CHNLSVC.CommonSearch.Send_SMTPMail(lblsupemail.Text, "Supplier Claim Approval", _mail);
            MessageBox.Show("Successfully Send!");

        }

        private void _sendMail(string _heading, string _type, string _Jobno, string _refno)
        {
            if (_type == "SEND")
            {
                _heading = _heading + " Job#: " + _Jobno + " Doc#:" + _refno;
                string _mail = "/*** Part Order Requested. ***/" + Environment.NewLine;
                _mail += "Service Center - " + txtPC.Text.Trim() + "" + Environment.NewLine;
                _mail += "User - " + BaseCls.GlbUserName + " (" + BaseCls.GlbUserID + ")" + Environment.NewLine + Environment.NewLine;
                _mail += "Job # - " + txtPC.Text.Trim() + "" + Environment.NewLine;
                _mail += "Job Date - " + txtPC.Text.Trim() + "" + Environment.NewLine;
                _mail += "Serial # - " + txtPC.Text.Trim() + "" + Environment.NewLine + Environment.NewLine + Environment.NewLine;

                _mail += "Requested Part Details" + Environment.NewLine;
                _mail += "Item Code - " + txtPC.Text.Trim() + "" + Environment.NewLine;
                _mail += "Item Description - " + txtPC.Text.Trim() + "" + Environment.NewLine;
                _mail += "Serial # - " + txtPC.Text.Trim() + "" + Environment.NewLine;

                _mail += "Order # - " + txtPC.Text.Trim() + "" + Environment.NewLine;

                CHNLSVC.CommonSearch.Send_SMTPMail("amilasanjeewa@abansgroup.com", _heading, _mail);
            }



        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to clear?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Clear_Data();

            }
        }

        private void btnSrh_PartNo_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.PartCode);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtpartno;
                _CommonSearch.txtSearchbyword.Text = txtpartno.Text;
                _CommonSearch.ShowDialog();
                txtpartno.Focus();
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

        private void txtjobNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSrh_Serial_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;


                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceDetailSearials);
                DataTable _result = CHNLSVC.CommonSearch.GetServiceDetailSearials(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerial;
                _CommonSearch.ShowDialog();
                txtSerial.Select();
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

        private void btnSrh_itemCode_Click(object sender, EventArgs e)
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
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void btnSrh_brand_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Brand);
                DataTable _result = CHNLSVC.CommonSearch.GetCat_SearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtBrand;
                _CommonSearch.txtSearchbyword.Text = txtBrand.Text;
                _CommonSearch.ShowDialog();
                txtBrand.Focus();
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

        private void btnSrhLocation_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txtPC.Select();

            }
            catch (Exception err)
            {
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "Inventory Report Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                if (!string.IsNullOrEmpty(txtPC.Text))
                {
                    MasterLocation _masterLocation = CHNLSVC.General.GetLocationByLocCode(BaseCls.GlbUserComCode, txtPC.Text.ToString());
                    if (_masterLocation == null)
                    {

                        MessageBox.Show("Invalid location code!", "Incorrect Location", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPC.Clear();
                        txtPC.Focus();
                        return;
                    }

                    CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                    if (string.IsNullOrEmpty(txtPC.Text)) return;
                    //_CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation); //Sanjeewa 2017-02-13
                    //DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                    //var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtPC.Text.Trim()).ToList();
                    //if (_validate == null || _validate.Count <= 0)
                    //{
                    //    MessageBox.Show("Please select the valid location code", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    txtPC.Clear();
                    //    txtPC.Focus();
                    //    return;
                    //}
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
                CHNLSVC.CloseAllChannels();
            }
        }

        private void SupplierWarrntyClaim_DoubleClick(object sender, EventArgs e)
        {
            btnSrhLocation_Click(null, null);
        }



        private void txtPC_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSrhLocation_Click(null, null);
            }
        }

        private void btnSearch_Item_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Item);
                DataTable _result = CHNLSVC.CommonSearch.GetItemSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtItem;
                _CommonSearch.ShowDialog();
                txtItem.Select();
            }
            catch (Exception err)
            {
                Cursor.Current = Cursors.Default;
                CHNLSVC.CloseChannel();
                MessageBox.Show(err.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                CHNLSVC.CloseAllChannels();
            }
        }

        private void txtItem_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Item_Click(null, null);
        }

        private void txtItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSearch_Item_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                cmbStatus.Focus();
            }
        }

        private void txtItemCode_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtItemCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSrh_itemCode_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtpartno.Focus();
            }
        }

        private void txtItemCode_DoubleClick(object sender, EventArgs e)
        {
            btnSrh_itemCode_Click(null, null);
        }

        private void txtBrand_DoubleClick(object sender, EventArgs e)
        {
            btnSrh_brand_Click(null, null);
        }

        private void txtBrand_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSrh_brand_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                txtSerial.Focus();
            }
        }

        private void txtpartno_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtpartno_DoubleClick(object sender, EventArgs e)
        {
            btnSrh_PartNo_Click(null, null);
        }

        private void txtSerial_DoubleClick(object sender, EventArgs e)
        {
            btnSrh_Serial_Click(null, null);
        }

        private void txtSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnSrh_Serial_Click(null, null);
            }
            else if (e.KeyCode == Keys.Enter)
            {
                dtpFrom.Focus();
            }
        }

        private void txtjobNo_DoubleClick(object sender, EventArgs e)
        {
            btnSrh_Job_Click(null, null);
        }

        private void btnSearch_Serial_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.SerialAvb);
                DataTable _result = CHNLSVC.CommonSearch.GetAvailableSeriaSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtSerialNo;
                _CommonSearch.ShowDialog();
                txtSerialNo.Select();
                txtSerialNo.Focus();
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

        private void txtItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtSerialNo.Focus();
            }
        }

        private void txtSerialNo_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter)
            {
                btnAddItem.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                btnSearch_Serial_Click(null, null);
            }
        }

        private void txtQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAddItem.Focus();
            }
        }

        private void txtSerialNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                {
                    e.Handled = true;
                }
                IsDecimalAllow(_isDecimalAllow, sender, e);
            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }

        }

        private void txtItem_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtItem.Text.Trim())) return;
            try
            {
                //  if (string.IsNullOrEmpty(txtPC.Text)) { MessageBox.Show("Please select the dispatch location.", "Dispatch Location", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }
                if (!LoadItemDetail(txtItem.Text.Trim()))
                {
                    MessageBox.Show("Please check the item code", "Invalid Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtItem.Clear();
                    txtItem.Focus();
                    return;
                }
                if (_isSerialize == true)
                {
                    txtSerialNo.Enabled = true;
                }
                else
                {
                    txtSerialNo.Enabled = false;
                }


            }
            catch (Exception ex)
            { this.Cursor = Cursors.Default; SystemErrorMessage(ex); }
            finally { this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels(); }
        }

        private bool LoadItemDetail(string _item)
        {
            lblItemDescription.Text = "Description : " + string.Empty;
            lblItemModel.Text = "Model : " + string.Empty;
            lblItemBrand.Text = "Brand : " + string.Empty;

            _isDecimalAllow = false;
            _itemdetail = new MasterItem();

            bool _isValid = false;

            if (!string.IsNullOrEmpty(_item)) _itemdetail = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, _item);
            if (_itemdetail != null)
                if (!string.IsNullOrEmpty(_itemdetail.Mi_cd))
                {
                    _isValid = true;
                    string _description = _itemdetail.Mi_longdesc;
                    string _model = _itemdetail.Mi_model;
                    string _brand = _itemdetail.Mi_brand;
                    string _serialstatus = _itemdetail.Mi_is_subitem == true ? "Available" : "Non";

                    lblItemDescription.Text = "Description : " + _description;
                    lblItemModel.Text = "Model : " + _model;
                    lblItemBrand.Text = "Brand : " + _brand;
                    if (_itemdetail.Mi_is_ser1 == 1)
                    {
                        _isSerialize = true;
                    }
                    else
                    {
                        _isSerialize = false;
                    }

                    _isDecimalAllow = CHNLSVC.Inventory.IsUOMDecimalAllow(_item);

                }

            return _isValid;
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            BindInventoryRequestItemsGridData();
        }

        private void txtSerialNo_Leave(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(txtPC.Text) == true)
            //{
            //    MessageBox.Show("Please select location.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    txtPC.Focus();
            //    return;
            //}
            if (string.IsNullOrEmpty(txtItem.Text) == true)
            {
                MessageBox.Show("Please select item code.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtItem.Focus();
                return;
            }

            MasterItem _item = CHNLSVC.Inventory.GetItem(BaseCls.GlbUserComCode, txtItem.Text);

            if (_item.Mi_is_ser1 == 1)
            {

                if (!string.IsNullOrEmpty(txtSerialNo.Text))
                {
                    DataTable _dtSer = CHNLSVC.Inventory.GetSerialDetailsBySerial(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf, txtItem.Text, txtSerialNo.Text);
                    if (_dtSer.Rows.Count > 0)
                    {
                        txtPart.Text = _dtSer.Rows[0]["ins_warr_no"].ToString();
                        _serId = Convert.ToInt16(_dtSer.Rows[0]["ins_ser_id"].ToString());
                        cmbStatus.SelectedValue = _dtSer.Rows[0]["ins_itm_stus"].ToString();

                    }
                    else
                    {
                        MessageBox.Show("Invalid serial.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtSerialNo.Text = "";
                        txtSerialNo.Focus();
                        return;
                    }
                }
            }

        }

        private void chkJob_CheckedChanged(object sender, EventArgs e)
        {



            if (chkJob.Checked == true)
            {
                pnlItemAdd.Visible = false;

            }
            else
            {
                pnlItemAdd.Visible = true;
                pnlItemAdd.Width = 997;
            }


        }



        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void chkJob_Click(object sender, EventArgs e)
        {
            //if (CHNLSVC.Security.Is_OptionPerimitted(BaseCls.GlbUserComCode, BaseCls.GlbUserID, 11059) == false)
            //{

            //    MessageBox.Show("you don't have permission enter with out job number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
        }


        private void dtpClaimDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void dtpClaimDate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtShipDocNo.Focus();
            }

        }

        private void txtBrand_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSerial_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpFrom_ValueChanged(object sender, EventArgs e)
        {

        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtSerialNo_DoubleClick(object sender, EventArgs e)
        {
            btnSearch_Serial_Click(null, null);
        }

        private void btnReject_Click(object sender, EventArgs e)
        {
            bool _allowCurrentTrans = false;

            if (string.IsNullOrEmpty(txtPC.Text))
            { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, txtPC.Text, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpClaimDate, label4, dtpClaimDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtpClaimDate.Value.Date != DateTime.Now.Date)
                    {
                        dtpClaimDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpClaimDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtpClaimDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpClaimDate.Focus();
                    return;
                }
            }

            string _docNo = "";
            if (string.IsNullOrEmpty(txtWcnNo.Text) == true)
            {
                MessageBox.Show("Please select request number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtSuppMain.Text))
            { MessageBox.Show("Please select the supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtSuppClaim.Text))
            { MessageBox.Show("Please select the claim supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (dgvClaimDetails.Rows.Count <= 0)
            { MessageBox.Show("Please select the clim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtShipDocNo.Text))
            { MessageBox.Show("Please select the ship document", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtRequestNo.Text))
            { MessageBox.Show("Please select the request no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtOrderNo.Text))
            { MessageBox.Show("Please select the order no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Approved")
            { MessageBox.Show("Request is already approved.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Cancelled")
            { MessageBox.Show("Request is already cancelled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Rejected")
            { MessageBox.Show("Request is already rejected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            if (CollectReqApp("R", 0) == 0)
            { MessageBox.Show("Enter claim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            // int effet = CHNLSVC.CustService.Save_SupplierClaim(_ReqSupHdr, _ReqClaimDet, _ReqAppAuto, out _docNo);
            Clear_Data();
            //if (effet > 0)
            //{
            //    MessageBox.Show(_docNo);
            //    // Clear_Data();
            //}
            //else
            //    MessageBox.Show("Process terminated. - " + _docNo);
        }

        private void btnCanApp_Click(object sender, EventArgs e)
        {
            bool _allowCurrentTrans = false;

            if (string.IsNullOrEmpty(txtPC.Text))
            { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, txtPC.Text, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpClaimDate, label4, dtpClaimDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtpClaimDate.Value.Date != DateTime.Now.Date)
                    {
                        dtpClaimDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpClaimDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtpClaimDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpClaimDate.Focus();
                    return;
                }
            }

            string _docNo = "";
            if (string.IsNullOrEmpty(txtWcnNo.Text) == true)
            {
                MessageBox.Show("Please select request number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtSuppMain.Text))
            { MessageBox.Show("Please select the supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtSuppClaim.Text))
            { MessageBox.Show("Please select the claim supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (dgvClaimDetails.Rows.Count <= 0)
            { MessageBox.Show("Please select the clim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtShipDocNo.Text))
            { MessageBox.Show("Please select the ship document", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtRequestNo.Text))
            { MessageBox.Show("Please select the request no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtOrderNo.Text))
            { MessageBox.Show("Please select the order no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text != "Approved")
            { MessageBox.Show("Request is not an approved.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            if (lblStatus.Text == "Rejected")
            { MessageBox.Show("Request is already rejected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            if (lblStatus.Text == "Cancelled")
            { MessageBox.Show("Request is already cancelled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }


            if (CollectReqApp("C", 0) == 0)
            { MessageBox.Show("Enter claim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            //int effet = CHNLSVC.CustService.Save_SupplierClaim(_ReqSupHdr, _ReqClaimDet, _ReqAppAuto, out _docNo);
            //Clear_Data();
            //if (effet > 0)
            //{
            //    MessageBox.Show(_docNo);
            //    // Clear_Data();
            //}
            //else
            //    MessageBox.Show("Process terminated. - " + _docNo);
        }

        private void txtSuppClaim_Leave(object sender, EventArgs e)
        {

            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                if (string.IsNullOrEmpty(txtSuppClaim.Text)) return;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceClaimSupplier);
                DataTable _result = CHNLSVC.CommonSearch.GetClaimSupplierData(_CommonSearch.SearchParams, null, null);
                var _validate = _result.AsEnumerable().Where(x => x.Field<string>("CODE") == txtSuppClaim.Text.Trim()).ToList();
                if (_validate == null || _validate.Count <= 0)
                {
                    MessageBox.Show("Please select the valid claim supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtSuppClaim.Clear();
                    txtSuppClaim.Focus();
                    return;
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
                CHNLSVC.CloseAllChannels();
            }



            if (!string.IsNullOrEmpty(txtSuppClaim.Text))
            {
                loadClaimSuppDetails(txtSuppClaim.Text.Trim());

            }
        }

        private void txtShipDocNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtRequestNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtpBillDate_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtReqRemarks_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtReqRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtOrderNo.Focus();
            }

        }

        private void dtpTo_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtJobSerial_Leave(object sender, EventArgs e)
        {

        }

        private void txtJobSerial_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnMore1_Click(object sender, EventArgs e)
        {

        }

        private void label49_Click(object sender, EventArgs e)
        {

        }

        private void label84_Click(object sender, EventArgs e)
        {

        }

        private void btnHide_Click(object sender, EventArgs e)
        {
            pnlWarr.Visible = false;
        }

        private void btnAttachDocs_Click(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtjob.Text))
            {
                ImageUpload frm = new ImageUpload(txtjob.Text, _jobLine, "", 0);
                frm.ShowDialog();
            }
        }

        private void btnPayAdd_Click(object sender, EventArgs e)
        {
            //check value
            try
            {
                decimal val;
                if (!decimal.TryParse(txtCurrancyValue.Text, out val))
                {
                    MessageBox.Show("Pay Amount has to be a number", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(BaseCls.GlbUserComCode, cmbCurrancy.SelectedValue.ToString(), dtpClaimDate.Value, _pc.Mpc_def_exrate, BaseCls.GlbUserDefProf);
                if (_exc1 != null)
                {
                    _exchangRate = _exc1.Mer_bnkbuy_rt;
                }
                else
                {
                    MessageBox.Show("Exchange Rate not found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                ucPayModes1.InvoiceType = "CS";
                ucPayModes1.LoadData();
                ucPayModes1.Amount.Enabled = false;
                // ucPayModes1.Amount.Text = Math.Round((Convert.ToDecimal(txtCurrancyValue.Text) * _exchangRate), 4).ToString();
                ucPayModes1.Amount.Text = FormatToCurrency(Convert.ToString(Convert.ToDecimal(txtCurrancyValue.Text) * _exchangRate));
                ucPayModes1.TotalAmount = Convert.ToDecimal(ucPayModes1.Amount.Text.Trim());

                ucPayModes1.ExchangeRate = _exchangRate;
                ucPayModes1.CurrancyAmount = Convert.ToDecimal(txtCurrancyValue.Text);
                ucPayModes1.CurrancyCode = cmbCurrancy.SelectedValue.ToString();

                ucPayModes1.button1_Click(null, null);
                txtCurrancyValue.Text = "";
                lblLocalCurValue.Text = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void txtCurrancyValue_Leave(object sender, EventArgs e)
        {
            if (txtCurrancyValue.Text != "")
            {
                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(BaseCls.GlbUserComCode, cmbCurrancy.SelectedValue.ToString(), DateTime.Now, _pc.Mpc_def_exrate, BaseCls.GlbUserDefProf);
                if (_exc1 != null)
                {
                    _exchangRate = _exc1.Mer_bnkbuy_rt;
                    lblLocalCurValue.Text = (Convert.ToDecimal(txtCurrancyValue.Text) * _exchangRate).ToString();
                }
            }
        }

        private void btnPayments_Click(object sender, EventArgs e)
        {
            pnlItems.Height = 207;
            lblMore.Text = "More >>";
            pnlPayment.Width = 1008;
            pnlPayment.Visible = true;
            LoadCurrancyCodes();
            _invNo = string.Empty;
            // Nadeeka 
            DataTable _claimamt = CHNLSVC.CustService.get_supp_claim_amt(BaseCls.GlbUserComCode, txtWcnNo.Text);
            if (_claimamt.Rows.Count > 0)
            {
                txtCurrancyValue.Text = _claimamt.Rows[0]["Sah_anal_7"].ToString();
                _invNo = _claimamt.Rows[0]["sah_inv_no"].ToString();
            }



        }

        private void btnClosePayment_Click(object sender, EventArgs e)
        {
            pnlPayment.Visible = false;
        }

        private void cmbCurrancy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                decimal val;

                MasterProfitCenter _pc = CHNLSVC.Sales.GetProfitCenter(BaseCls.GlbUserComCode, BaseCls.GlbUserDefProf);
                decimal _exchangRate = 0;
                MasterExchangeRate _exc1 = CHNLSVC.Sales.GetExchangeRate(BaseCls.GlbUserComCode, cmbCurrancy.SelectedValue.ToString(), DateTime.Now, _pc.Mpc_def_exrate, BaseCls.GlbUserDefProf);
                if (_exc1 != null)
                {
                    _exchangRate = _exc1.Mer_bnkbuy_rt;
                }
                else
                {

                }
                if (_exchangRate > 0)
                    txtCurrancyValue.Text = (Math.Round((ucPayModes1.Balance / _exchangRate), 4).ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                CHNLSVC.CloseChannel();
                return;
            }
        }

        private void cmbCurrancy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCurrancyValue.Focus();
            }
        }

        private void txtCurrancyValue_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCurrancyValue_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void txtCurrancyValue_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnPayAdd.Focus();
            }
        }

        private void ucPayModes1_Load(object sender, EventArgs e)
        {

        }

        private void ucPayModes1_ItemAdded(object sender, EventArgs e)
        {


            cmbCurrancy_SelectedIndexChanged(null, null);
        }

        private void txtJobSerial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (txtJobSerial.Text != _oldSer)
                {
                    DataTable _teblSer = CHNLSVC.Inventory.getMovementSerial(txtJobItem.Text, txtJobSerial.Text);
                    if (_teblSer.Rows.Count > 0)
                    {
                        MessageBox.Show("This serial already avilable", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                        txtJobSerial.Focus();
                    }
                }



                if (_ispart == 1 && txtJobSerial.Text != _oldSer)
                {
                    int Isavl = CHNLSVC.Inventory.CheckSubSerialAvl(txtJobSerial.Text, txtJobItem.Text);
                    if (Isavl == 1)
                    {
                        MessageBox.Show("This part serial already avilable", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                        txtJobSerial.Focus();
                    }
                }

                if (txtJobSerial.Text == _oldSer)
                {
                    if (MessageBox.Show("you have entered same serial, Are you sure want to continue?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                        txtJobSerial.Focus();

                    }

                }

                dgvClaimDetails.Rows[dgvClaimDetails.CurrentRow.Index].Cells["clmserial"].Value = txtJobSerial.Text;
                dgvClaimDetails.Rows[dgvClaimDetails.CurrentRow.Index].Cells["select"].Value = true;
                MessageBox.Show("Successfully changed the serial", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnReceive.Enabled = true;
                btnApprove.Enabled = false;
                btnReceive.Focus();
                return;
            }
        }

        private void btnschjob_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            //  _jobStage = "3";
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            _CommonSearch.dtpTo.Value = DateTime.Now.Date;
            _CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-1);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtSchJob;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtSchJob.Focus();
        }

        private void txtSchJob_DoubleClick(object sender, EventArgs e)
        {
            btnschjob_Click(null, null);
        }

        private void txtSchJob_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnschjob_Click(null, null);
            }
        }

        private void lblMore_Click(object sender, EventArgs e)
        {
            if (lblMore.Text == "More >>")
            {
                pnlItems.Height = 458;
                lblMore.Text = "More <<";
            }
            else
            {
                pnlItems.Height = 207;
                lblMore.Text = "More >>";
            }
        }
        private void getPendingJobsAll()
        {
            dgvPending.Rows.Clear();
            List<Service_job_Det> _claimItemListdbtjob = null;
            _claimItemListdbtjob = CHNLSVC.CustService.getSupplierClaimRequest(BaseCls.GlbUserComCode, txtPC.Text, null, null, null, null, null, null, Convert.ToDateTime("12/12/1999"), Convert.ToDateTime(DateTime.Today).Date, null, 1);
            if (_claimItemListdbtjob != null)
            {
                var numberGroups = _claimItemListdbtjob.GroupBy(i => i.Jbd_brand);
                foreach (var grp in numberGroups)
                {
                    var number = grp.Key;
                    var total = grp.Count();
                    dgvPending.Rows.Add();
                    dgvPending["colbrand", dgvPending.Rows.Count - 1].Value = number;
                    dgvPending["coldes", dgvPending.Rows.Count - 1].Value = "Request Pending";
                    dgvPending["colCont", dgvPending.Rows.Count - 1].Value = total;
                }

            }

            //List<Service_WCN_Hdr> _TempReqAppHdr = new List<Service_WCN_Hdr>();
            //List<Service_job_Det> _claimItemListjob = new List<Service_job_Det>();
            //_TempReqAppHdr = CHNLSVC.CustService.getWCNHeader(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime("12/12/1999").Date, Convert.ToDateTime(DateTime.Today).Date, "P", null, null,null);
            //if (_TempReqAppHdr != null)
            //{
            //    lblRec.Text = Convert.ToString(_TempReqAppHdr.Count);
            //    foreach (var itemdet in _TempReqAppHdr)
            //    {

            //        List<Service_job_Det> _claimItemListjobtemp = CHNLSVC.CustService.sp_getSupClaimDetails(itemdet.Swc_doc_no);
            //        if (_claimItemListjobtemp != null)
            //        {
            //            _claimItemListjob.AddRange(_claimItemListjobtemp);

            //      }

            //   }
            //}

            DataTable _receivePng = CHNLSVC.CustService.getWCNHeaderBrand(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime("12/12/1999").Date, Convert.ToDateTime(DateTime.Today).Date, "P");
            foreach (DataRow r in _receivePng.Rows)
            {
                dgvPending.Rows.Add();
                dgvPending["colbrand", dgvPending.Rows.Count - 1].Value = (string)r["jbd_brand"];
                dgvPending["coldes", dgvPending.Rows.Count - 1].Value = "Receive Pending";
                dgvPending["colCont", dgvPending.Rows.Count - 1].Value = (Decimal)r["totCnt"];


            }
            // if (_claimItemListjob != null)
            //{
            //    var numberGroups = _claimItemListjob.GroupBy(i => i.Jbd_brand);
            //    foreach (var grp in numberGroups)
            //    {
            //        var number = grp.Key;
            //        var total = grp.Count();
            //        dgvPending.Rows.Add();
            //        dgvPending["colbrand", dgvPending.Rows.Count - 1].Value = number;
            //        dgvPending["coldes", dgvPending.Rows.Count - 1].Value = "Receive Pending";
            //        dgvPending["colCont", dgvPending.Rows.Count - 1].Value = total;
            //    }
            //}
            double Qty = 0;


        }
        private void getPendingJobs()
        {
            dgvPending.Rows.Clear();
            List<Service_job_Det> _claimItemListdbtjob = null;
            _claimItemListdbtjob = CHNLSVC.CustService.getSupplierClaimRequest(BaseCls.GlbUserComCode, txtPC.Text, null, null, null, null, null, null, Convert.ToDateTime("12/12/1999"), Convert.ToDateTime(DateTime.Today).Date, null, 1);
            if (_claimItemListdbtjob != null)
            {
                lblReq.Text = Convert.ToString(_claimItemListdbtjob.Count);

            }


            _claimItemListdbtjob = CHNLSVC.CustService.getSupplierClaimRequest(BaseCls.GlbUserComCode, txtPC.Text, null, null, null, null, null, null, Convert.ToDateTime("12/12/1999"), Convert.ToDateTime(DateTime.Today).Date, null, 3);
            if (_claimItemListdbtjob != null)
            {
                lblHold.Text = Convert.ToString(_claimItemListdbtjob.Count);

            }


            List<Service_WCN_Hdr> _TempReqAppHdr = new List<Service_WCN_Hdr>();
            List<Service_job_Det> _claimItemListjob = new List<Service_job_Det>();
            _TempReqAppHdr = CHNLSVC.CustService.getWCNHeader(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime("12/12/1999").Date, Convert.ToDateTime(DateTime.Today).Date, "P", null, null, null);
            if (_TempReqAppHdr != null)
            {
                lblRec.Text = Convert.ToString(_TempReqAppHdr.Count);

            }
            double Qty = 0;


        }
        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                SelectView(true);
                dgvClaimDetails.AutoGenerateColumns = false;
                dgvClaimDetails.DataSource = new List<Service_job_Det>();
                dgvClaimDetails.DataSource = _claimItemList;
            }
            else
            {
                SelectView(false);

                dgvClaimDetails.AutoGenerateColumns = false;
                dgvClaimDetails.DataSource = new List<Service_job_Det>();
                dgvClaimDetails.DataSource = _claimItemList;
            }
        }

        private void cmbItemStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cmbItemStatus_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (dgvClaimDetails.Rows.Count > 0)
                {
                    dgvClaimDetails.Rows[dgvClaimDetails.CurrentRow.Index].Cells["Status"].Value = cmbItemStatus.SelectedValue.ToString();
                }
            }
        }





        private void copyText(object sender, EventArgs e)
        {
            try
            {
                Label lbl = (Label)sender;
                Clipboard.SetText(lbl.Text.ToString());
                MessageBox.Show(lbl.Text, "Copy to Clipboard");
            }
            catch (Exception ex)
            {

            }
        }

        private void dgvActualDefectsD3_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (dgvActualDefectsD3.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;

                    if (_rowIndex != -1)
                    {
                        #region Copy text

                        string _copyText = dgvActualDefectsD3.Rows[_rowIndex].Cells[_colIndex].Value.ToString();
                        Clipboard.SetText(_copyText.ToString());
                        MessageBox.Show(_copyText, "Copy to Clipboard");

                        #endregion Copy text
                    }
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show("Error Occurred while processing...\n" + ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error); CHNLSVC.CloseChannel();
            }
            finally
            {
                this.Cursor = Cursors.Default; CHNLSVC.CloseAllChannels();
            }
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            bool _allowCurrentTrans = false;

            if (string.IsNullOrEmpty(txtPC.Text))
            { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, txtPC.Text, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpClaimDate, label4, dtpClaimDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtpClaimDate.Value.Date != DateTime.Now.Date)
                    {
                        dtpClaimDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpClaimDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtpClaimDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpClaimDate.Focus();
                    return;
                }
            }

            string _docNo = "";
            //if (string.IsNullOrEmpty(txtWcnNo.Text) == true)
            //{
            //    MessageBox.Show("Please select request number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}
            if (string.IsNullOrEmpty(txtSuppMain.Text))
            { MessageBox.Show("Please select the supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtSuppClaim.Text))
            { MessageBox.Show("Please select the claim supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (dgvClaimDetails.Rows.Count <= 0)
            { MessageBox.Show("Please select the clim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtShipDocNo.Text))
            { MessageBox.Show("Please select the ship document", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtRequestNo.Text))
            { MessageBox.Show("Please select the request no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtOrderNo.Text))
            { MessageBox.Show("Please select the order no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Approved")
            { MessageBox.Show("Request is already approved.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Cancelled")
            { MessageBox.Show("Request is already cancelled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Rejected")
            { MessageBox.Show("Request is already rejected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            //if (string.IsNullOrEmpty(txtReason.Text))
            //{ MessageBox.Show("Please enter reason for hold", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (CollectReqApp("L", 0) == 0)
            { MessageBox.Show("Enter claim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            int effet = CHNLSVC.CustService.Save_SupplierClaim(_ReqSupHdr, _ReqClaimDet, _ReqAppAuto, null, null, null, null, null, null, null, _isRecall, BaseCls.GlbUserSessionID, out _docNo);
            Clear_Data();
            if (effet > 0)
            {
                MessageBox.Show(" Successfully Released and Send request. - " + _docNo);
                //  MessageBox.Show(_docNo);
                // Clear_Data();
            }
            else
                MessageBox.Show("Process terminated. - " + _docNo);
        }

        private void btnReject_Click_1(object sender, EventArgs e)
        {
            bool _allowCurrentTrans = false;

            if (string.IsNullOrEmpty(txtPC.Text))
            { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, txtPC.Text, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpClaimDate, label4, dtpClaimDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtpClaimDate.Value.Date != DateTime.Now.Date)
                    {
                        dtpClaimDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpClaimDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtpClaimDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpClaimDate.Focus();
                    return;
                }
            }

            string _docNo = "";
            if (string.IsNullOrEmpty(txtWcnNo.Text) == true)
            {
                //MessageBox.Show("Please select request number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //return;
            }
            if (string.IsNullOrEmpty(txtSuppMain.Text))
            { MessageBox.Show("Please select the supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtSuppClaim.Text))
            { MessageBox.Show("Please select the claim supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (dgvClaimDetails.Rows.Count <= 0)
            { MessageBox.Show("Please select the clim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            //if (string.IsNullOrEmpty(txtShipDocNo.Text))
            //{ MessageBox.Show("Please select the ship document", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            //if (string.IsNullOrEmpty(txtRequestNo.Text))
            //{ MessageBox.Show("Please select the request no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            ////if (string.IsNullOrEmpty(txtOrderNo.Text))
            //{ MessageBox.Show("Please select the order no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Approved")
            { MessageBox.Show("Request is already approved.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Cancelled")
            { MessageBox.Show("Request is already cancelled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Rejected")
            { MessageBox.Show("Request is already rejected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            //if (string.IsNullOrEmpty(txtReason.Text))
            //{ MessageBox.Show("Please enter reason for hold", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (CollectReqApp("R", 0) == 0)
            { MessageBox.Show("Enter claim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            int effet = CHNLSVC.CustService.Save_SupplierClaim(_ReqSupHdr, _ReqClaimDet, _ReqAppAuto, null, null, null, null, null, null, null, _isRecall, BaseCls.GlbUserSessionID, out _docNo);
            Clear_Data();
            if (effet > 0)
            {
                MessageBox.Show(" Successfully Rejected.  ");
                //  MessageBox.Show(_docNo);
                // Clear_Data();
            }
            else
                MessageBox.Show("Process terminated. - " + _docNo);
        }

        private void btnSrhJob_Click(object sender, EventArgs e)
        {
            CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();
            _CommonSearch.ReturnIndex = 0;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceTaskCate);
            DataTable _result = CHNLSVC.CommonSearch.SearchScvTaskCateByLoc(_CommonSearch.SearchParams, null, null);
            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtTaskLoc;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtTaskLoc.Focus();
        }

        private void btnSrhOrder_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceSupWCNno);
                DataTable _result = CHNLSVC.CommonSearch.GetSupplierClaimDoc(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtOrder;
                _CommonSearch.ShowDialog();
                txtOrder.Select();
                txtOrder.Focus();

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

        private void btnClosePend_Click(object sender, EventArgs e)
        {
            pnlPending.Visible = false;
        }

        private void btnpPending_Click(object sender, EventArgs e)
        {
            pnlPending.Visible = true;
            pnlPending.Width = 406;
            pnlPending.Height = 219;
            getPendingJobsAll();
        }

        private void txtWcnNo_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtWcnNo.Text))
            {
                RecallSupp();
            }
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            if (pnlHistory.Visible == false)
            {
                pnlHistory.Width = 839;
                pnlHistory.Height = 518;
                pnlHistory.Visible = true;
                ucSupplierWarranty1.Visible = true;
                ucSupplierWarranty1.clerSWCLables();
                ucSupplierWarranty1.GblJobNumber = txtjob.Text;
                ucSupplierWarranty1.GblJobLine = _jobLine;
                ucSupplierWarranty1.LoadData();
            }
            else { pnlHistory.Visible = false; }


        }

        private void btnCloseSerialPnl_Click(object sender, EventArgs e)
        {
            pnlHistory.Visible = false;
        }


        private void BindCombo()
        {

            DataTable dtcnt = CHNLSVC.CustService.getWCNTypes(null);
            if (dtcnt != null && dtcnt.Rows.Count > 0)
            {
                cmbType.DataSource = dtcnt;
                cmbType.DisplayMember = "sst_des";
                cmbType.ValueMember = "sst_type";
                cmbType.SelectedIndex = -1;


            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bool _allowCurrentTrans = false;

            if (string.IsNullOrEmpty(txtPC.Text))
            { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, txtPC.Text, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpClaimDate, label4, dtpClaimDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtpClaimDate.Value.Date != DateTime.Now.Date)
                    {
                        dtpClaimDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpClaimDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtpClaimDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpClaimDate.Focus();
                    return;
                }
            }

            string _docNo = "";
            if (string.IsNullOrEmpty(txtWcnNo.Text) == true)
            {
                MessageBox.Show("Please select request number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrEmpty(txtSuppMain.Text))
            { MessageBox.Show("Please select the supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtSuppClaim.Text))
            { MessageBox.Show("Please select the claim supplier", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (dgvClaimDetails.Rows.Count <= 0)
            { MessageBox.Show("Please select the clim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtShipDocNo.Text))
            { MessageBox.Show("Please select the ship document", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtRequestNo.Text))
            { MessageBox.Show("Please select the request no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtOrderNo.Text))
            { MessageBox.Show("Please select the order no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Approved")
            { MessageBox.Show("Request is already approved.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Cancelled")
            { MessageBox.Show("Request is already cancelled.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (lblStatus.Text == "Rejected")
            { MessageBox.Show("Request is already rejected.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            //if (string.IsNullOrEmpty(txtReason.Text))
            //{ MessageBox.Show("Please enter reason for hold", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (CollectReqApp("C", 0) == 0)
            { MessageBox.Show("Enter claim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            int effet = CHNLSVC.CustService.Save_SupplierClaim(_ReqSupHdr, _ReqClaimDet, _ReqAppAuto, null, null, null, null, null, null, null, _isRecall, BaseCls.GlbUserSessionID, out _docNo);
            Clear_Data();
            if (effet > 0)
            {
                MessageBox.Show(" Successfully Cancelled. " + _docNo);
                //  MessageBox.Show(_docNo);
                // Clear_Data();
            }
            else
                MessageBox.Show("Process terminated. - " + _docNo);
        }

        private void ucSupplierWarranty1_Load(object sender, EventArgs e)
        {

        }

        private void btnclsHistory_Click(object sender, EventArgs e)
        {
            pnlHistory.Visible = false;

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtOrderNo.Text))
            { MessageBox.Show("Please select the supplier claim order no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtShipDocNo.Text))
            { MessageBox.Show("Please select the ship document", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(txtRequestNo.Text))
            { MessageBox.Show("Please select the request no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }


            CollectReqApp("P", 1);
            _ReqSupHdr.Swc_doc_no = txtWcnNo.Text;
            string _docNo = string.Empty;
            int effet = CHNLSVC.CustService.Update_SupplierClaim(_ReqSupHdr, out _docNo);
            Clear_Data();
            if (effet > 0)
            {

                MessageBox.Show(_docNo);

            }
        }

        private void schnJob_Click(object sender, EventArgs e)
        {

            this.Cursor = Cursors.WaitCursor;
            //  _jobStage = "3";
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            _CommonSearch.dtpTo.Value = DateTime.Now.Date;
            _CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-1);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtnJob;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtnJob.Focus();
        }

        private void txtnJob_Leave(object sender, EventArgs e)
        {

            if (txtnJob.Text != "")
            {
                Service_JOB_HDR OJobheader = CHNLSVC.CustService.GET_SCV_JOB_HDR(txtnJob.Text, BaseCls.GlbUserComCode);



                if (OJobheader == null || OJobheader.SJB_JOBNO == null)
                {
                    MessageBox.Show("Please enter correct job number", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtnJob.Clear();
                    txtnJob.Focus();
                    return;
                }



                BindUserJobSerialDDLData(cmbJobSerial, txtnJob.Text);
            }
        }

        private void cmbJobSerial_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtnJob_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnRemoveItem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtnJob.Text))
            { MessageBox.Show("Please select the job no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }
            if (string.IsNullOrEmpty(cmbJobSerial.Text))
            { MessageBox.Show("Please select the serial ", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            if (txtnJob.Text != "" && cmbJobSerial.Text != "")
            {
                Int32 _jobline = Convert.ToInt32(cmbJobSerial.SelectedValue.ToString());


                DataTable _dtl = CHNLSVC.CustService.GetServiceSupplierWarrantyJob(BaseCls.GlbUserComCode, txtnJob.Text, _jobline);
                string _relatdJOb = string.Empty;
                if (_dtl.Rows.Count > 0)
                {
                    foreach (DataRow r in _dtl.Rows)
                    {
                        if (Convert.ToInt32((string)r["SWD_JOBNO_PRV"]) == 1)
                        {
                            _relatdJOb = (string)r["SWD_JOBNO_PRV"];
                        }

                    }
                    if (MessageBox.Show("Already received clim item for this job based on " + _relatdJOb + " , Are you sure want to continue ?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.No)
                    {
                        return;
                    }

                }


                dgvClaimDetails.Rows[dgvClaimDetails.CurrentRow.Index].Cells["clmjob"].Value = txtnJob.Text;
                dgvClaimDetails.Rows[dgvClaimDetails.CurrentRow.Index].Cells["Jbd_jobline"].Value = cmbJobSerial.SelectedValue.ToString();
                MessageBox.Show("Successfully changed the job no", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void txtnJob_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cmbJobSerial.Focus();
            }
            else if (e.KeyCode == Keys.F2)
            {
                schnJob_Click(null, null);
            }
        }

        private void txtnJob_DoubleClick(object sender, EventArgs e)
        {
            schnJob_Click(null, null);
        }

        private void btnclsMRN_Click(object sender, EventArgs e)
        {
            pnlMRNRel.Visible = false;
            txtreleaseRem.Text = "";

        }

        private void btnrelmrn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtjob.Text))
            { MessageBox.Show("Please select job", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }


            if (_jobLine == 0)
            { MessageBox.Show("Please select job item", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }

            if (string.IsNullOrEmpty(txtreleaseRem.Text))
            { MessageBox.Show("Please enter remarks", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }


            string _docNo = string.Empty;
            int effet = CHNLSVC.CustService.UpdateSupplierClaimForMRN(txtjob.Text, _jobLine, BaseCls.GlbUserID, txtreleaseRem.Text, out _docNo);
            Clear_Data();
            if (effet > 0)
            {

                MessageBox.Show(_docNo, "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        private void btnmrn_Click(object sender, EventArgs e)
        {
            pnlMRNRel.Visible = true;
            pnlMRNRel.Width = 369;
            pnlMRNRel.Height = 119;
        }

        private void btnWaraChk_Click(object sender, EventArgs e)
        {
            pnlChecking.Visible = true;
            pnlChecking.Width = 548;
            pnlChecking.Height = 493;

        }

        private void btnSrhChk_Click(object sender, EventArgs e)
        {
            List<Service_WCN_Hdr> _TempReqAppHdr = new List<Service_WCN_Hdr>();

            string _docSts = string.Empty;
            _docSts = "I";

            if (string.IsNullOrEmpty(txtPC.Text))
            { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }
            _TempReqAppHdr = CHNLSVC.CustService.getWCNHeaderCheck(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(dtpFromchk.Value).Date, Convert.ToDateTime(dtpTochk.Value).Date, _docSts, txtjobChk.Text, null);



            dgvChkHdr.AutoGenerateColumns = false;
            dgvChkHdr.DataSource = new List<RequestApprovalHeader>();

            if (_TempReqAppHdr == null)
            {
                MessageBox.Show("No any request / approval found.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            dgvChkHdr.AutoGenerateColumns = false;
            dgvChkHdr.DataSource = _TempReqAppHdr;

        }

        private void btnsrhjobChk_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            //  _jobStage = "3";
            CommonSearch.CommonSearchDate _CommonSearch = new CommonSearch.CommonSearchDate();
            _CommonSearch.ReturnIndex = 1;
            _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.ServiceJobSearch);
            _CommonSearch.dtpTo.Value = DateTime.Now.Date;
            _CommonSearch.dtpFrom.Value = _CommonSearch.dtpTo.Value.Date.AddMonths(-1);
            DataTable _result = CHNLSVC.CommonSearch.GetServiceJobs(_CommonSearch.SearchParams, null, null, _CommonSearch.dtpFrom.Value.Date, _CommonSearch.dtpTo.Value.Date);

            _CommonSearch.dvResult.DataSource = _result;
            _CommonSearch.BindUCtrlDDLData(_result);
            _CommonSearch.obj_TragetTextBox = txtjobChk;
            this.Cursor = Cursors.Default;
            _CommonSearch.IsSearchEnter = true;
            _CommonSearch.ShowDialog();
            txtjobChk.Focus();
        }

        private void dgvChkHdr_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvChkHdr_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            List<Service_job_Det> _claimItemListchk = new List<Service_job_Det>();
            _claimItemList = new List<Service_job_Det>();

            txtchkWcn.Text = Convert.ToString(dgvChkHdr.Rows[e.RowIndex].Cells["colchkDoc"].Value);
            _claimItemListchk = CHNLSVC.CustService.sp_getSupClaimDetails(txtchkWcn.Text);

            foreach (Service_job_Det _ser in _claimItemListchk)
            {
                if (_ser.Jbd_need_chk == 1)
                {
                    _claimItemList.Add(_ser);
                }
            }

            if (_claimItemList != null)
            {
                foreach (Service_job_Det jobitem in _claimItemList)
                {
                    jobitem.Jbd_oldjobline = jobitem.Jbd_jobline;
                    jobitem.Jbd_mainjobno = jobitem.Jbd_jobno;

                }


                SelectView(true);
                dgvChkDet.AutoGenerateColumns = false;
                dgvChkDet.DataSource = new List<Service_job_Det>();
                dgvChkDet.AutoGenerateColumns = false;

                dgvChkDet.DataSource = _claimItemList;
            }

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bool _allowCurrentTrans = false;
            if (string.IsNullOrEmpty(txtchkWcn.Text))
            { MessageBox.Show("Please select the Cim document", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }



            List<Service_WCN_Hdr> _TempReqAppHdr = new List<Service_WCN_Hdr>();


            _TempReqAppHdr = CHNLSVC.CustService.getWCNHeaderCheck(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(dtpFromchk.Value).Date, Convert.ToDateTime(dtpTochk.Value).Date, _docSts, txtjobChk.Text, txtchkWcn.Text);




            if (string.IsNullOrEmpty(txtPC.Text))
            { MessageBox.Show("Please select the location", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); txtPC.Focus(); return; }
            if (IsAllowBackDateForModule(BaseCls.GlbUserComCode, txtPC.Text, string.Empty.ToUpper().ToString(), this.GlbModuleName, dtpClaimDate, label4, dtpClaimDate.Value.Date.ToString(), out _allowCurrentTrans) == false)
            {
                if (_allowCurrentTrans == true)
                {
                    if (dtpClaimDate.Value.Date != DateTime.Now.Date)
                    {
                        dtpClaimDate.Enabled = true;
                        MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        dtpClaimDate.Focus();
                        return;
                    }
                }
                else
                {
                    dtpClaimDate.Enabled = true;
                    MessageBox.Show("Selected date not allowed for transaction!", "Back Date Terminated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtpClaimDate.Focus();
                    return;
                }
            }

            string _docNo = "";
            Service_WCN_Detail _tempReqClaim = new Service_WCN_Detail();
            if (_ReqClaimDet == null)
            { _ReqClaimDet = new List<Service_WCN_Detail>(); }
            Int32 _ItemLine = 0;
            if (_claimItemList.Count > 0)
            {
                foreach (Service_job_Det item in _claimItemList)
                {
                    if (item.Jbd_select == true)
                    {
                        _ItemLine = _ItemLine + 1;
                        _tempReqClaim = new Service_WCN_Detail();
                        _tempReqClaim.SWD_SEQ_NO = 0;
                        _tempReqClaim.SWD_LINE = _ItemLine;
                        _tempReqClaim.SWD_DOC_NO = null;
                        _tempReqClaim.SWD_ITMCD = item.Jbd_itm_cd;
                        _tempReqClaim.SWD_SUPPITMCD = item.Jbd_itm_cd;
                        _tempReqClaim.SWD_SER1 = item.Jbd_ser1;
                        _tempReqClaim.SWD_SERID = Convert.ToInt32(item.Jbd_ser_id);
                        _tempReqClaim.SWD_WARRNO = item.Jbd_warr;
                        _tempReqClaim.SWD_SUPPWARRNO = item.Jbd_warr;
                        _tempReqClaim.SWD_OEMSERNO = item.Jbd_oem_no;
                        _tempReqClaim.SWD_CASEID = item.Jbd_case_id;
                        _tempReqClaim.SWD_SETTLED = 0;
                        _tempReqClaim.SWD_OTHDOCNO = txtRequestNo.Text;
                        _tempReqClaim.SWD_ISWCRN = 0;
                        _tempReqClaim.SWD_ISJOBCLOSE = 0;
                        _tempReqClaim.SWD_JOBNO = item.Jbd_jobno;
                        _tempReqClaim.SWD_JOBLINE = item.Jbd_jobline;
                        _tempReqClaim.SWD_OLDPARTSEQ = 0;
                        _tempReqClaim.SWD_QTY = 1;
                        _tempReqClaim.SWD_ITM_STUS = item.Jbd_itm_stus;
                        _tempReqClaim.SWD_OLD_SER = item.Jbd_serold;
                        _tempReqClaim.SWD_IS_PART = item.Isold_part;
                        _tempReqClaim.SWD_IS_STOCK = item.jbd_isstockupdate;

                        _tempReqClaim.SWD_JOBNO_PRV = item.Jbd_mainjobno;
                        _tempReqClaim.SWD_JOBLINE_PRV = item.Jbd_oldjobline;
                        _tempReqClaim.old_pt_seq = item.Jbd_seq_no;
                        _tempReqClaim.Swd_need_chk = 1;


                    }

                    _ReqClaimDet.Add(_tempReqClaim);
                }


            }

            if (_ReqClaimDet.Count == 0)

            { MessageBox.Show("Enter claim items", "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information); return; }



            int effet = CHNLSVC.CustService.Save_SupplierClaimChecking(_TempReqAppHdr[0], _ReqClaimDet, txtChkRem.Text, BaseCls.GlbUserID, BaseCls.GlbUserSessionID, out _docNo);


            if (effet > 0)
            {
                Clear_Data();

                List<Service_WCN_Hdr> _TempReqAppHdr1 = new List<Service_WCN_Hdr>();


                _docSts = "I";

                _TempReqAppHdr1 = CHNLSVC.CustService.getWCNHeaderCheck(BaseCls.GlbUserComCode, txtPC.Text, Convert.ToDateTime(dtpFromchk.Value).Date, Convert.ToDateTime(dtpTochk.Value).Date, _docSts, txtjobChk.Text, null);



                dgvChkHdr.AutoGenerateColumns = false;
                dgvChkHdr.DataSource = new List<RequestApprovalHeader>();
                dgvChkHdr.DataSource = _TempReqAppHdr1;
                dgvChkDet.DataSource = new List<Service_job_Det>();
                txtchkWcn.Text = "";
                txtChkRem.Text = "";
                MessageBox.Show(_docNo, "Supplier Claim", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Clear_Data();
            }
            else
                MessageBox.Show("Process terminated. - " + _docNo);
        }

        private void btncheck_Click(object sender, EventArgs e)
        {
            pnlChecking.Visible = false;
        }

        private void btn_loc_Click(object sender, EventArgs e)
        {
            try
            {
                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.UserLocation);
                DataTable _result = CHNLSVC.CommonSearch.GetUserLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txt_loc;
                _CommonSearch.IsSearchEnter = true;
                _CommonSearch.ShowDialog();
                txt_loc.Select();
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
