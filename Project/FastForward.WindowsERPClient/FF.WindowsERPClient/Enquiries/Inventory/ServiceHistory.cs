using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;
using FF.WindowsERPClient.Services;

namespace FF.WindowsERPClient.Enquiries.Inventory
{
    public partial class ServiceHistory : Base
    {
        public ServiceHistory()
        {
            InitializeComponent();
        }

        public DataTable ServiceJobHistory { get; set; }
       // public string SerialNo { get; set; }
        //public string ItemCode { get; set; }

        private void ServiceHistory_Load(object sender, EventArgs e)
        {
            try
            {
                BindingSource _hisroty = new BindingSource();
                _hisroty.DataSource = ServiceJobHistory
                                        .AsEnumerable()
                                            .Select(x => new
                                            {
                                                Seq_No = x.Field<string>("Seq_No"),
                                                Job_No = x.Field<string>("Job_No"),
                                                Customer_Code = x.Field<string>("Customer_Code"),
                                                Customer_Name = x.Field<string>("Customer_Name"),
                                                //Stage = x.Field<string>("Stage"),
                                                Start_date = x.Field<string>("Start_Date"),
                                                End_date = x.Field<string>("End_Date"),
                                                Is_warranty_Replaced = x.Field<string>("Is_warranty_Replaced")
                                            }).ToList();
                dgvJobList.DataSource = _hisroty;
                dgvJobList.Refresh();
                dgvJobList_CellClick(null, new DataGridViewCellEventArgs(0, 0));
            }
            catch (Exception)
            {                
                throw;
            }
        }

        private void dgvJobList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                try
                {
                    if (dgvJobList.Rows.Count > 0)
                    {
                        if (ServiceJobHistory.Rows.Count > 0)
                        {
                            lblJobNo.Text = dgvJobList.Rows[e.RowIndex].Cells["Job_No"].Value.ToString().Trim();
                            lblJobStatus.Text = ServiceJobHistory.AsEnumerable().Where(x => x.Field<string>("Job_No") == lblJobNo.Text).Select(x => x.Field<string>("Stage")).First().ToString();

                            //load job item details
                            Service_Enquiry_Job_Hdr _jobHeader  = new Service_Enquiry_Job_Hdr();
                            List<Service_job_Det> _jobDetails = new List<Service_job_Det>();
                            string _returnMessage = null;
                            Int32 result = CHNLSVC.CustService.GetJobDetailsEnquiry(lblJobNo.Text, BaseCls.GlbUserID, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, out _jobDetails, out _jobHeader, out _returnMessage);
                            if ((result > 0) && (_jobDetails.Count > 0))
                            {
                                _jobDetails.Where(x => x.Jbd_warr_stus == 1).ToList().ForEach(x => x.Jbd_warr_stus_text = "Active");
                                _jobDetails.Where(x => x.Jbd_warr_stus == 0).ToList().ForEach(x => x.Jbd_warr_stus_text = "Inactive");

                                BindingSource _bindItems = new BindingSource();
                                _bindItems.DataSource = _jobDetails.Select(x => new 
                                                                                    { 
                                                                                        Jbd_jobno = x.Jbd_jobno,
                                                                                        Jbd_jobline = x.Jbd_jobline,
                                                                                        jbd_itm_cd = x.Jbd_itm_cd,
                                                                                        Jbd_itm_desc = x.Jbd_itm_desc, 
                                                                                        Jbd_model = x.Jbd_model, 
                                                                                        Jbd_ser1 = x.Jbd_ser1,
                                                                                        Jbd_ser2 = x.Jbd_ser2,
                                                                                        Jbd_warr = x.Jbd_warr,
                                                                                        Jbd_warr_stus_text  = x.Jbd_warr_stus_text,
                                                                                        jbd_warrstartdt = x.Jbd_warrstartdt,
                                                                                        jbd_warrperiod = x.Jbd_warrperiod,
                                                                                        jbd_supp_cd = x.Jbd_supp_cd,
                                                                                        jbd_invc_no = x.Jbd_invc_no,
                                                                                        Jbd_regno = x.Jbd_regno,
                                                                                        jbd_warrrmk = x.Jbd_warrrmk
                                                                                    });
                                dgvJobItems.DataSource = _bindItems;
                                dgvJobItems.Refresh();

                                dgvJobItems_CellClick(null, new DataGridViewCellEventArgs(0, 0));
                            }                            
                        }
                    }
                }
                catch (Exception)
                {                    
                    throw;
                }
            }            
        }

        private void dgvJobItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (dgvJobItems.Rows.Count > 0)
                {
                    try
                    {
                        Int32 _jobLineNo = Convert.ToInt32(dgvJobItems.Rows[e.RowIndex].Cells["Jbd_joblineD1"].Value.ToString());

                        string _message = null;
                        Decimal _totalAmount = 0;
                        List<Tuple<string, string, string>> ConRemark_Type_User = new List<Tuple<string, string, string>>();
                        Service_job_Det _jobDetails = new Service_job_Det();
                        List<Service_Job_Defects> _defects = new List<Service_Job_Defects>();
                        List<Service_Enquiry_TechAllo_Hdr> _allocations = new List<Service_Enquiry_TechAllo_Hdr>();
                        List<Service_Enquiry_Tech_Cmnt> _techComments = new List<Service_Enquiry_Tech_Cmnt>();
                        List<Service_Enquiry_StandByItems> _sandByItems = new List<Service_Enquiry_StandByItems>();

                        int result = CHNLSVC.CustService.GetAllJobDetailsEnquiry(lblJobNo.Text, _jobLineNo, BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, BaseCls.GlbUserDefProf, out _jobDetails, out _defects, out _allocations, out _techComments, out ConRemark_Type_User, out _sandByItems, out _message, out _totalAmount);
                        if ((result > 0) && (_defects != null))
                        {
                            BindingSource _bindDefects = new BindingSource();
                            _bindDefects.DataSource = _defects.Select(x => new
                                                                            {
                                                                                DEFECT_TYPE = x.SRD_STAGE == "J" ? "REPORTED DEFECTS" : x.SRD_STAGE == "W" ? "ACTUAL DEFECTS" : string.Empty,
                                                                                SDT_DESC = x.SDT_DESC,
                                                                                SRD_DEF_RMK = x.SRD_DEF_RMK
                                                                            }).ToList().OrderBy(x => x.DEFECT_TYPE);
                            dgvOpenDefets.DataSource = _bindDefects;
                            dgvOpenDefets.Refresh();
                        }
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
    }
}
