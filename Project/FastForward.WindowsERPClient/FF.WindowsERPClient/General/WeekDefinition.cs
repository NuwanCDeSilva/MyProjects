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
using FF.WindowsERPClient.General;
using System.IO;
//using IWshRuntimeLibrary;

namespace FF.WindowsERPClient.General
{
    public partial class WeekDefinition : Base
    {

        public WeekDefinition()
        {
            InitializeComponent();
            bindData();
        }

        protected void bindData()
        {
            gvWeek.AutoGenerateColumns = false;

            cmbYear.Items.Add("2012");
            cmbYear.Items.Add("2013");
            cmbYear.Items.Add("2014");
            cmbYear.Items.Add("2015");
            cmbYear.Items.Add("2016");
            cmbYear.Items.Add("2017");
            cmbYear.Items.Add("2018");

            int _Year = DateTime.Now.Year;
            cmbYear.SelectedIndex = _Year % 2013 + 1;

            cmbMonth.Items.Add("January");
            cmbMonth.Items.Add("February");
            cmbMonth.Items.Add("March");
            cmbMonth.Items.Add("April");
            cmbMonth.Items.Add("May");
            cmbMonth.Items.Add("June");
            cmbMonth.Items.Add("July");
            cmbMonth.Items.Add("August");
            cmbMonth.Items.Add("September");
            cmbMonth.Items.Add("October");
            cmbMonth.Items.Add("November");
            cmbMonth.Items.Add("December");
            cmbMonth.SelectedIndex = -1;

            cmbWeek.SelectedIndex = -1;

            List<MasterCompany> _com = CHNLSVC.General.GetALLMasterCompaniesData();
            gvCompany.AutoGenerateColumns = false;
            gvCompany.DataSource = _com;
        }

        private void cmbMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            var _selection = from DataGridViewRow r in gvCompany.Rows
                             where Convert.ToBoolean(r.Cells["c_select"].Value)
                             select r.Cells["c_code"].Value;

            DateTime _from;
            DateTime _to;
            if (string.IsNullOrEmpty(cmbYear.Text))
            {
                MessageBox.Show("Select the year");
                return;
            }

            Int32 month = cmbMonth.SelectedIndex + 1;
            Int32 year = Convert.ToInt32(cmbYear.Text);

            if (month != 0)
            {
                int numberOfDays = DateTime.DaysInMonth(year, month);
                DateTime lastDay = new DateTime(year, month, numberOfDays);

                txtToDate.Text = lastDay.ToString("dd/MMM/yyyy");

                DateTime dtFrom = new DateTime(Convert.ToInt32(cmbYear.Text), month, 1);
                txtFromDate.Text = (dtFrom.AddDays(-(dtFrom.Day - 1))).ToString("dd/MMM/yyyy");
            }
            DataTable _weekDef = CHNLSVC.General.GetWeekDefinition(month, year, 0, out _from, out _to,BaseCls.GlbUserComCode, "ALL");
            DataTable _merge = new DataTable();
            foreach (var _n in _selection)
            {
                var _lst = _weekDef.AsEnumerable().Where(s => s.Field<string>("gw_com").Trim() == Convert.ToString(_n).Trim()).ToList();
                if (_lst != null && _lst.Count > 0)
                    _merge.Merge(_lst.CopyToDataTable());
            }
            if (_selection == null || _selection.Count() <= 0) _merge = _weekDef;
            gvWeek.DataSource = _merge;
            if (gvWeek.RowCount > 0) gvWeek.Sort(w_com, ListSortDirection.Ascending);
            Int32 x = _weekDef.Rows.Count;
            if (x < 5) { cmbWeek.SelectedIndex = Convert.ToInt32(x); }
            else { cmbWeek.SelectedIndex = -1; }

        }

        private void cmbYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbMonth_SelectedIndexChanged(null, null);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            DateTime _frm;
            DateTime _to;
            ////check whether week def already defined
            //if (CHNLSVC.Financial.IsWeekDefFound(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbWeek.Text)) == true)
            //{
            //    MessageBox.Show("Cannot Save. Already exist !", "Week Definition", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            var _selection = from DataGridViewRow r in gvCompany.Rows
                             where Convert.ToBoolean(r.Cells["c_select"].Value)
                             select r.Cells["c_code"].Value;

            if (_selection == null || _selection.Count() <= 0)
            {
                MessageBox.Show("Please select the companies", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<string> _company = new List<string>();

            foreach (var _n in _selection)
            {
                var _dups = from DataGridViewRow r in gvWeek.Rows
                            where Convert.ToString(r.Cells["w_year"].Value) == Convert.ToString(cmbYear.Text) && Convert.ToInt32(r.Cells["w_month"].Value) == Convert.ToInt32(cmbMonth.SelectedIndex + 1) && Convert.ToInt32(r.Cells["w_week"].Value) == Convert.ToInt32(cmbWeek.Text)
                            && r.Cells["w_com"].Value == _n
                            select r;
                _company.Add(Convert.ToString(_n));
                if (_dups != null && _dups.Count() > 0)
                {
                    MessageBox.Show("Cannot Save. Already exist in " + _n + "!", "Week Definition", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (MessageBox.Show("Are you sure ?", "Week Definition", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }



            Int32 eff = CHNLSVC.Financial.Save_gnr_week(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbWeek.Text), Convert.ToDateTime(txtFromDate.Text).Date, Convert.ToDateTime(txtToDate.Text).Date, BaseCls.GlbUserID, _company);

            DataTable _weekDef = CHNLSVC.General.GetWeekDefinition(Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbYear.Text), 0, out _frm, out _to,BaseCls.GlbUserComCode,"ALL");
            DataTable _merge = new DataTable();
            foreach (var _n in _selection)
            {
                var _lst = _weekDef.AsEnumerable().Where(s => s.Field<string>("gw_com").Trim() == Convert.ToString(_n).Trim()).ToList();
                if (_lst != null && _lst.Count > 0)
                    _merge.Merge(_lst.CopyToDataTable());
            }
            if (_selection == null || _selection.Count() <= 0) _merge = _weekDef;
            gvWeek.DataSource = _merge;
            if (gvWeek.RowCount > 0) gvWeek.Sort(w_com, ListSortDirection.Ascending);
            //gvWeek.DataSource = _weekDef;

            Int32 x = Convert.ToInt32(cmbWeek.Text);
            if (x < 5) { cmbWeek.SelectedIndex = Convert.ToInt32(x); }
            else { cmbWeek.SelectedIndex = -1; }

            cmbWeek_SelectedIndexChanged(null, null);

            MessageBox.Show("Successfully Completed", "Week Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void cmbWeek_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime _from;
            DateTime _to;
            if (!string.IsNullOrEmpty(cmbWeek.Text))
            {
                DataTable _weekDef = CHNLSVC.General.GetWeekDefinition(Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbWeek.Text), out _from, out _to, BaseCls.GlbUserComCode, "");
                if (_from != Convert.ToDateTime("31/Dec/9999"))
                {
                    txtFromDate.Text = _from.Date.ToString();
                    txtToDate.Text = _to.Date.ToString();
                }
                else
                {
                    if (Convert.ToInt32(cmbWeek.Text) - 1 != 0)
                    {
                        DataTable _weekDef1 = CHNLSVC.General.GetWeekDefinition(Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbWeek.Text) - 1, out _from, out _to, BaseCls.GlbUserComCode, "");
                        if (_from != Convert.ToDateTime("31/Dec/9999"))
                        {
                            DateTime _nextDay = _to.AddDays(1);
                            txtFromDate.Text = Convert.ToDateTime(_nextDay).ToString();

                            int numberOfDays = DateTime.DaysInMonth(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedIndex + 1));
                            DateTime lastDay = new DateTime(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedIndex + 1), numberOfDays);

                            txtToDate.Text = lastDay.ToString("dd/MMM/yyyy");
                        }
                    }
                }
            }

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            DateTime _frm;
            DateTime _to;
            ////check whether week def already defined
            //if (CHNLSVC.Financial.IsWeekDefFound(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbWeek.Text)) == false)
            //{
            //    MessageBox.Show("Cannot Edit. Week definition not found !", "Week Definition", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            var _selection = from DataGridViewRow r in gvCompany.Rows
                             where Convert.ToBoolean(r.Cells["c_select"].Value)
                             select r.Cells["c_code"].Value;

            if (_selection == null || _selection.Count() <= 0)
            {
                MessageBox.Show("Please select the companies", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            List<string> _company = new List<string>();
            foreach (var _n in _selection)
            {
                var _dups = from DataGridViewRow r in gvWeek.Rows
                            where Convert.ToInt32(r.Cells["w_year"].Value) == Convert.ToInt32(cmbYear.Text) && Convert.ToInt32(r.Cells["w_month"].Value) == Convert.ToInt32(cmbMonth.SelectedIndex + 1) && Convert.ToInt32(r.Cells["w_week"].Value) == Convert.ToInt32(cmbWeek.Text)
                            && Convert.ToString(r.Cells["w_com"].Value) == Convert.ToString(_n)
                            select r;
                _company.Add(Convert.ToString(_n));
                if (_dups == null || _dups.Count() <= 0)
                {
                    MessageBox.Show("Cannot Edit. Week definition not found !", "Week Definition", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }



            if (MessageBox.Show("Are you sure ?", "Week Definition", MessageBoxButtons.YesNo) == DialogResult.No)
            {
                return;
            }

            Int32 eff = CHNLSVC.Financial.Save_gnr_week(Convert.ToInt32(cmbYear.Text), Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbWeek.Text), Convert.ToDateTime(txtFromDate.Text).Date, Convert.ToDateTime(txtToDate.Text).Date, BaseCls.GlbUserID, _company);

            DataTable _weekDef = CHNLSVC.General.GetWeekDefinition(Convert.ToInt32(cmbMonth.SelectedIndex + 1), Convert.ToInt32(cmbYear.Text), 0, out _frm, out _to,BaseCls.GlbUserComCode, "");
            gvWeek.DataSource = _weekDef;

            Int32 x = Convert.ToInt32(cmbWeek.Text);
            if (x < 5) { cmbWeek.SelectedIndex = Convert.ToInt32(x); }
            else { cmbWeek.SelectedIndex = -1; }

            cmbWeek_SelectedIndexChanged(null, null);

            MessageBox.Show("Successfully Updated", "Week Definition", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void gvCompany_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (gvCompany.Rows.Count > 0)
            {
                if (e.RowIndex != -1)
                {
                    if (Convert.ToBoolean(gvCompany.Rows[e.RowIndex].Cells["c_select"].Value))
                    {
                        gvCompany.Rows[e.RowIndex].Cells["c_select"].Value = false;
                    }
                    else
                    {
                        gvCompany.Rows[e.RowIndex].Cells["c_select"].Value = true;
                    }
                    cmbMonth_SelectedIndexChanged(null, null);
                }
            }
        }








    }
}
