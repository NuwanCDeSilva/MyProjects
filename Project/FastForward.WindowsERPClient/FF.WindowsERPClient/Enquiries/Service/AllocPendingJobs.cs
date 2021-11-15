using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Enquiries.Service
{
    public partial class AllocPendingJobs : FF.WindowsERPClient.Base
    {
        private static string glbEmpcat = "";   //kapila 15/2/2016

        public AllocPendingJobs()
        {
            InitializeComponent();

        }

        public void setEmpCategory(String _empCat)
        {
            glbEmpcat = _empCat;

        }

        private string SetCommonSearchInitialParameters(CommonUIDefiniton.SearchUserControlType _type)
        {
            StringBuilder paramsText = new StringBuilder();
            string seperator = "|";
            paramsText.Append(((int)_type).ToString() + ":");

            switch (_type)
            {
                case CommonUIDefiniton.SearchUserControlType.Item:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.InsuCom:
                    {
                        paramsText.Append("INS" + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Company.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Channel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_SubChannel.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Area.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Region.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Zone.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + "" + seperator + CommonUIDefiniton.SearchUserControlType.Pc_HIRC_Location.ToString() + seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.GPC:
                    {
                        paramsText.Append(seperator);
                        break;
                    }
                case CommonUIDefiniton.SearchUserControlType.PriceBookByCompany:
                    {
                        paramsText.Append(BaseCls.GlbUserComCode + seperator);
                        break;
                    }
        
                case CommonUIDefiniton.SearchUserControlType.InsuPolicy:
                    {
                        paramsText.Append("" + seperator);
                        break;
                    }
                default:
                    break;
            }

            return paramsText.ToString();
        }

        private void AllocPendingJobs_Load(object sender, EventArgs e)
        {
            SystemUser _sysUser = new SystemUser();
            _sysUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);

            dtpDate.Value = DateTime.Today.AddMonths(-1);
            dtpTo.Value = DateTime.Today;

            int _isTech=0;
            if(glbEmpcat=="TECH")
                _isTech=1;

            DataTable _dt = CHNLSVC.CustService.getAllocatedPendingJobs(BaseCls.GlbUserComCode, _sysUser.Se_emp_cd, _isTech, BaseCls.GlbDefSubChannel, dtpDate.Value, dtpTo.Value);
            grvPending.AutoGenerateColumns = false;
            grvPending.DataSource = _dt;
            txtPend.Text = _dt.Rows.Count.ToString();

            DataTable _dt1 = CHNLSVC.CustService.getNotAllocatedJobs(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, dtpDate.Value, dtpTo.Value);
            grvNotAloc.AutoGenerateColumns = false;
            grvNotAloc.DataSource = _dt1;
            txtNotAlloc.Text = _dt1.Rows.Count.ToString();


        }

        private void grvPending_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 && e.RowIndex != -1)
                {
                    string _jobNo = grvPending.Rows[e.RowIndex].Cells["sjb_jobno"].Value.ToString();
                    Services.ServiceWIP frm = new Services.ServiceWIP(_jobNo);
                    this.Close();
                }
            }
            catch (Exception err)
            { }
        }

        private void grvNotAloc_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (grvNotAloc.ColumnCount > 0)
                {
                    Int32 _rowIndex = e.RowIndex;
                    Int32 _colIndex = e.ColumnIndex;

                    if (_rowIndex != -1)
                    {
                        #region Copy text

                        string _copyText = grvNotAloc.Rows[_rowIndex].Cells[_colIndex].Value.ToString();
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SystemUser _sysUser = new SystemUser();
            _sysUser = CHNLSVC.Security.GetUserByUserID(BaseCls.GlbUserID);

            int _isTech = 0;
            if (glbEmpcat == "TECH")
                _isTech = 1;

            DataTable _dt = CHNLSVC.CustService.getAllocatedPendingJobs(BaseCls.GlbUserComCode, _sysUser.Se_emp_cd, _isTech, BaseCls.GlbDefSubChannel, dtpDate.Value, dtpTo.Value);
            grvPending.AutoGenerateColumns = false;
            grvPending.DataSource = _dt;
            txtPend.Text = _dt.Rows.Count.ToString();

            DataTable _dt1 = CHNLSVC.CustService.getNotAllocatedJobs(BaseCls.GlbUserComCode, BaseCls.GlbUserDefLoca, dtpDate.Value, dtpTo.Value);
            grvNotAloc.AutoGenerateColumns = false;
            grvNotAloc.DataSource = _dt1;
            txtNotAlloc.Text = _dt1.Rows.Count.ToString();
        }

    }
}
