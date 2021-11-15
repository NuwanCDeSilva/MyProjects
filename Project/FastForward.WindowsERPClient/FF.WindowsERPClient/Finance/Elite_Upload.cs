using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using FF.BusinessObjects;
using FF.Interfaces;
using System.Linq;
using System.Linq.Expressions;
using FF.WindowsERPClient.Finance;
using System.IO;
//using IWshRuntimeLibrary;

namespace FF.WindowsERPClient.Finance
{
    public partial class Elite_Upload : Base
    {

        public Elite_Upload()
        {
            InitializeComponent();
            bindData();
        }

        private void txtPC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {

                CommonSearch.CommonSearch _CommonSearch = new CommonSearch.CommonSearch();

                _CommonSearch.ReturnIndex = 0;
                _CommonSearch.SearchParams = SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType.Location);
                DataTable _result = CHNLSVC.CommonSearch.GetLocationSearchData(_CommonSearch.SearchParams, null, null);
                _CommonSearch.dvResult.DataSource = _result;
                _CommonSearch.BindUCtrlDDLData(_result);
                _CommonSearch.obj_TragetTextBox = txtPC;
                _CommonSearch.ShowDialog();
                txtPC.Select();
            }
        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {

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



        protected void bindData()
        {
            txtPC.Text = BaseCls.GlbUserDefProf;
            txtComp.Text = BaseCls.GlbUserComCode;
            txtPC1.Text = BaseCls.GlbUserDefProf;

          
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            string p_sun_user_id = string.Empty;
            string p_file_path = string.Empty;
            string p_source_path = string.Empty;
            string p_file_name = string.Empty;

          
      

            this.Cursor = Cursors.WaitCursor;
            //accounting period
            string vAccPeriod = (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Year).ToString() + "0" + (Convert.ToDateTime(Convert.ToDateTime(txtTo.Text).Date.AddMonths(Convert.ToInt16(-3))).Month).ToString("00");

      
        
            {
                //check whether the period is finalized



                //check whether SUN user ID exist
                SystemUser _systemUser = null;
                _systemUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);
                p_sun_user_id = _systemUser.Se_SUN_ID;
                if (string.IsNullOrEmpty(p_sun_user_id))
                {
                    MessageBox.Show("SUN user ID not found !", "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Cursor = Cursors.Default;
                    return;
                }

                //file name
              //  p_file_name = p_sun_user_id + txtPC.Text + "Elite" + Convert.ToDateTime(DateTime.Now.Date).Date.ToString("ddMMyy") + ".txt";
                p_file_name =  txtPC.Text + "Elite" + Convert.ToDateTime(DateTime.Now.Date).Date.ToString("ddMMyy") + ".txt";
                //check whether local path is exist
                 p_source_path = @"C:\\SUN";
                
                if (!Directory.Exists(p_source_path))
                {

                    MessageBox.Show("Path not found. " + p_source_path, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Cursor = Cursors.Default;
                    return;
                }
                p_source_path = p_source_path + "\\";
                //local file path to save 
                p_file_path = p_source_path + p_file_name;

                DataTable X = CHNLSVC.Financial.ProcessSunUploadElite(BaseCls.GlbUserID, Convert.ToDateTime(txtFrom.Text), Convert.ToDateTime(txtTo.Text), BaseCls.GlbUserComCode, txtPC.Text);
             
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(p_file_path))
                {
                    string _STR = string.Empty;
                    for (int i = 0; i < X.Rows.Count; i++)
                    {
                        _STR = X.Rows[i]["STR_01"].ToString() +
                            X.Rows[i]["STR_02"].ToString() + X.Rows[i]["STR_03"].ToString() + X.Rows[i]["STR_04"].ToString() +
                            X.Rows[i]["STR_05"].ToString() + X.Rows[i]["STR_06"].ToString() + X.Rows[i]["STR_07"].ToString() +
                            X.Rows[i]["STR_08"].ToString() + X.Rows[i]["STR_09"].ToString() + X.Rows[i]["STR_10"].ToString() +
                            X.Rows[i]["STR_11"].ToString() + X.Rows[i]["STR_12"].ToString() + X.Rows[i]["STR_13"].ToString() +
                            X.Rows[i]["STR_14"].ToString() + X.Rows[i]["STR_15"].ToString() + X.Rows[i]["STR_16"].ToString() +
                            X.Rows[i]["STR_17"].ToString() + X.Rows[i]["STR_18"].ToString() + X.Rows[i]["STR_19"].ToString() +
                            X.Rows[i]["STR_20"].ToString() + X.Rows[i]["STR_21"].ToString() + X.Rows[i]["STR_22"].ToString() +
                            X.Rows[i]["STR_23"].ToString() + X.Rows[i]["STR_24"].ToString() + X.Rows[i]["STR_25"].ToString() +
                            X.Rows[i]["STR_26"].ToString() + X.Rows[i]["STR_27"].ToString() + X.Rows[i]["STR_28"].ToString() +
                            X.Rows[i]["STR_29"].ToString() + X.Rows[i]["STR_30"].ToString() + X.Rows[i]["STR_31"].ToString() +
                            X.Rows[i]["STR_32"].ToString() + X.Rows[i]["STR_33"].ToString() + X.Rows[i]["STR_34"].ToString() +
                            X.Rows[i]["STR_35"].ToString() + X.Rows[i]["STR_36"].ToString() + X.Rows[i]["STR_37"].ToString() +
                            X.Rows[i]["STR_38"].ToString();
                        file.WriteLine(_STR);
                    }

                    file.Close();
                }

                File_Copy(p_file_name, p_file_path, "Elite");
                txtFile.Text = p_file_name;
                MessageBox.Show("Successfully generated. File name is : " + p_file_name, "SUN Upload", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

      
        

         

        }

        protected void File_Copy(string _fileName, string _sourceFilePath, string _upType)
        {
            try
            {
                System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.224\\sun426\\" + _fileName, true);
                //if (_upType == "REC" || _upType == "DINV")
                //{
                //    System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.224\\aal\\" + _fileName, true);
                //}
                //else if (_upType == "SOS")
                //{
                //    System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.50\\sos\\" + _fileName, true);
                //}
                //else if (_upType == "SCAN")
                //{
                //    System.IO.File.Copy(@"C:\SUN\" + _fileName, "\\\\192.168.1.50\\work\\" + _fileName, true);
                //}
              
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error Uploading", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       

         

        private void optScan_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            string com = txtComp.Text;
            string chanel = txtChanel.Text;
            string subChanel = txtSChanel.Text;
            string area = txtArea.Text;
            string region = txtRegion.Text;
            string zone = txtZone.Text;
            string pc = txtPC.Text;

            lstPC.Clear();
            string _masterLocation = (string.IsNullOrEmpty(BaseCls.GlbUserDefLoca)) ? BaseCls.GlbUserDefProf : BaseCls.GlbUserDefLoca;
            if (CHNLSVC.Inventory.CheckUserPermission(BaseCls.GlbUserID, BaseCls.GlbUserComCode, _masterLocation, "REPS"))
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep_All(com, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
            }
            else
            {
                DataTable dt = CHNLSVC.Sales.GetPC_from_Hierachy_Rep(BaseCls.GlbUserID, com, chanel, subChanel, area, region, zone, pc);
                foreach (DataRow drow in dt.Rows)
                {
                    lstPC.Items.Add(drow["PROFIT_CENTER"].ToString());
                }
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

        private void button1_Click(object sender, EventArgs e)
        {
            lstPC.Clear();
        }

   






    }
}
