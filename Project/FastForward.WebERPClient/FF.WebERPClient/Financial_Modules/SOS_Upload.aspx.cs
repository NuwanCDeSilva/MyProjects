using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using FF.BusinessObjects;
using FF.WebERPClient.UserControls;
using System.IO;
//using IWshRuntimeLibrary;

namespace FF.WebERPClient.Financial_Modules
{
    public partial class SOS_Upload : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //txtFrom.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                txtPC.Text = GlbUserDefProf;
                bindData();
            }
        }

        protected void optSOS_OnCheckedChanged(object sender, System.EventArgs e)
        {

        }

        protected void optAcc_OnCheckedChanged(object sender, System.EventArgs e)
        {

        }

        protected void bindData()
        {
            ddlYear.Items.Clear();
            ddlYear.Items.Add(new ListItem("--Select Year--", "-1"));
            ddlYear.Items.Add("2012");
            ddlYear.Items.Add("2013");
            ddlYear.Items.Add("2014");
            ddlYear.Items.Add("2015");
            ddlYear.Items.Add("2016");

            ddlMonth.Items.Clear();
            ddlMonth.Items.Add(new ListItem("--Select Month--", "-1"));
            ddlMonth.Items.Add("January");
            ddlMonth.Items.Add("February");
            ddlMonth.Items.Add("March");
            ddlMonth.Items.Add("April");
            ddlMonth.Items.Add("May");
            ddlMonth.Items.Add("June");
            ddlMonth.Items.Add("July");
            ddlMonth.Items.Add("August");
            ddlMonth.Items.Add("September");
            ddlMonth.Items.Add("October");
            ddlMonth.Items.Add("November");
            ddlMonth.Items.Add("December");
        }

        protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlYear.SelectedValue.Equals("-1"))
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Select the year");
                return;
            }

            int month = ddlMonth.SelectedIndex;
            int year = 2011 + ddlYear.SelectedIndex;

            int numberOfDays = DateTime.DaysInMonth(year, month);
            DateTime lastDay = new DateTime(year, month, numberOfDays);

            txtTo.Text = lastDay.ToString("dd/MMM/yyyy");

            DateTime dtFrom = new DateTime(Convert.ToInt32( ddlYear.Text), month, 1);
            txtFrom.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");

        }

        protected void imgbtnPC_Click(object sender, ImageClickEventArgs e)
        {
            MasterCommonSearchUCtrl.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.AllProfitCenters);
            DataTable dataSource = CHNLSVC.CommonSearch.GetPC_SearchData(MasterCommonSearchUCtrl.SearchParams, null, null);

            MasterCommonSearchUCtrl.BindUCtrlDDLData(dataSource);
            MasterCommonSearchUCtrl.BindUCtrlGridData(dataSource);
            MasterCommonSearchUCtrl.ReturnResultControl = txtPC.ClientID;
            MasterCommonSearchUCtrl.UCModalPopupExtender.Show();


        }


        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

                case CommonUIDefiniton.SearchUserControlType.AllProfitCenters:
                    {
                        paramsText.Append(GlbUserComCode + seperator);
                        break;
                    }


                default:
                    break;
            }

            return paramsText.ToString();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Default.aspx", false);
        }

        protected void btnProcess_Click(object sender, EventArgs e)
        {
            string p_sun_user_id = string.Empty;
            string p_file_path = string.Empty;
            string p_source_path = string.Empty;
            string p_file_name = string.Empty;

            if (txtFrom.Text == string.Empty)
            {
                MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Select the year/month");
                return;
            }

            //accounting period
            string vAccPeriod = (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Year).ToString() + "0" + (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Month).ToString("00");

            //SOS upload--------------------------------------------------------------------------------------------------------------------------
            if (optSOS.Checked == true)
            {
                //check whether the period is finalized



                //check whether SUN user ID exist
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(GlbUserName);
                p_sun_user_id = _systemUser.Se_SUN_ID;
                if (string.IsNullOrEmpty(p_sun_user_id))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "SUN user ID not found !");
                    return;
                }

                //file name
                p_file_name = p_sun_user_id + txtPC.Text + "SOS" + Convert.ToDateTime(DateTime.Now.Date).Date.ToString("ddMMyy") + ".txt";

                //check whether local path is exist
                p_source_path = @"C:\\SUN";

                if (!Directory.Exists(p_source_path))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Path not found. " + p_source_path);
                    return;
                }
                p_source_path = p_source_path + "\\";
                //local file path to save 
                p_file_path = p_source_path + p_file_name;

                int X = CHNLSVC.Financial.ProcessSUNUpload(ddlMonth.SelectedValue, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), GlbUserComCode, txtPC.Text, GlbUserName, vAccPeriod, p_sun_user_id, p_file_path);

                //int Y = CHNLSVC.Financial.Generate_SOS_Text_File(ddlMonth.SelectedValue, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), GlbUserComCode, txtPC.Text, GlbUserName, vAccPeriod, p_sun_user_id, p_file_path);

                string p_destFilePath = @"\\\\192.168.1.50\\sos\\" + p_file_name;

                File_Copy(p_file_name, p_file_path, p_destFilePath);
            }

            //Dealer invoices upload--------------------------------------------------------------------------------------------------------------------------
            if (optDInv.Checked == true)
            {
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(GlbUserName);
                p_sun_user_id = _systemUser.Se_SUN_ID;
                if (string.IsNullOrEmpty(p_sun_user_id))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "SUN user ID not found !");
                    return;
                }
                //file name
                p_file_name = "INV" + Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd") + ".txt";

                //check whether local path is exist
                p_source_path = @"C:\\SUN";

                if (!Directory.Exists(p_source_path))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Path not found. " + p_source_path);
                    return;
                }
                p_source_path = p_source_path + "\\";
                //local file path to save 
                p_file_path = p_source_path + p_file_name;

                int X = CHNLSVC.Financial.ProcessSUNUpload_Invoice(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), GlbUserComCode, txtPC.Text, GlbUserName, vAccPeriod, 1, p_sun_user_id, p_file_path);

                //int Y = CHNLSVC.Financial.Generate_SOS_Text_File(GlbUserName, p_file_path);

                string p_destFilePath = @"\\\\192.168.1.224\\aal\\" + p_file_name;

                File_Copy(p_file_name, p_file_path, p_destFilePath);
            }

            //Receipts upload--------------------------------------------------------------------------------------------------------------------------
            if (optDRec.Checked == true)
            {
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(GlbUserName);
                p_sun_user_id = _systemUser.Se_SUN_ID;
                if (string.IsNullOrEmpty(p_sun_user_id))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "SUN user ID not found !");
                    return;
                }
                //file name
                p_file_name = "REC" + Convert.ToDateTime(txtTo.Text).ToString("yyyyMMdd") + ".txt";

                //check whether local path is exist
                p_source_path = @"C:\\SUN";

                if (!Directory.Exists(p_source_path))
                {
                    MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.warning, "Path not found. " + p_source_path);
                    return;
                }
                p_source_path = p_source_path + "\\";
                //local file path to save 
                p_file_path = p_source_path + p_file_name;

                int X = CHNLSVC.Financial.ProcessSUNUpload_Receipt(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), GlbUserComCode, txtPC.Text, GlbUserName, vAccPeriod, p_sun_user_id, p_file_path);

                //int Y = CHNLSVC.Financial.Generate_SOS_Text_File(Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), GlbUserComCode, txtPC.Text, GlbUserName, vAccPeriod, p_sun_user_id, p_file_path);

                string p_destFilePath = @"\\\\192.168.1.224\\aal\\" + p_file_name;

                File_Copy(p_file_name, p_file_path, p_destFilePath);
            }
            txtFile.Text = p_file_name;
            MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Information, "Successfully generated. File name is : " + p_file_name);
        }

        protected void File_Copy(string _fileName, string _sourceFilePath, string _destFilePath)
        {
            //try
            //{
                //IWshNetwork_Class network = new IWshNetwork_Class();
                //network.MapNetworkDrive("O:", @"\\192.168.1.224\aal", Type.Missing, "abans\\kapila", "kap@321");

                //System.IO.File.Delete(@"O:\" + _fileName);
                //System.IO.File.Copy(@"C:\SUN\" + _fileName, @"O:\" + _fileName);
                System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.224\\aal\\" + _fileName, true);
                //network.RemoveNetworkDrive("O:");
            //}
            //catch (UIValidationException ex)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, ex.ErrorMessege);
            //}
            //catch (Exception e1)
            //{
            //    this.MasterMsgInfoUCtrl.SetMessage(CommonUIDefiniton.MessageType.Error, e1.Message);
            //}
            //if (File.Exists(_destFilePath))
            //{
            //    File.Delete(_destFilePath);
            //}
            //File.Copy(_sourceFilePath, _destFilePath);
        }


    }
}

//IWshNetwork_Class network = new IWshNetwork_Class();
//network.MapNetworkDrive("K:", @"\\192.168.1.50\SOS", Type.Missing, "kapila", "wee@123");

//System.IO.File.Delete(@"K:\" + _fileName);
//System.IO.File.Copy(@"C:\SUN\" + _fileName, @"\\192.168.1.50\SOS\" + _fileName);