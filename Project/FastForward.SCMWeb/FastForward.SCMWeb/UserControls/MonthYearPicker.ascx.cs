using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FastForward.SCMWeb.UserControls
{
    public partial class MonthYearPicker : System.Web.UI.UserControl
    {
        private string _TextBoxCss;
        public string TextBoxCss
        {
            get { return _TextBoxCss; }
            set { _TextBoxCss = value; }
        }

        private string _SelectButtonCss;
        public string SelectButtonCss
        {
            get { return _SelectButtonCss; }
            set { _SelectButtonCss = value; }
        }

        private string _SetButtonCss;
        public string SetButtonCss
        {
            get { return _SetButtonCss; }
            set { _SetButtonCss = value; }
        }

        private string _PanelCss;
        public string PanelCss
        {
            get { return _PanelCss; }
            set { _PanelCss = value; }
        }

        private int _MinYear;
        public int MinYear
        {
            get { return _MinYear; }
            set { _MinYear = value; }
        }

        private int _MaxYear;
        public int MaxYear
        {
            get { return _MaxYear; }
            set { _MaxYear = value; }
        }

        private int _MinMonth;
        public int MinMonth
        {
            get { return _MinMonth; }
            set { _MinMonth = value; }
        }

        private int _MaxMonth;
        public int MaxMonth
        {
            get { return _MaxMonth; }
            set { _MaxMonth = value; }
        }

        private string _Value;
        public string Value
        {
            get
            {
                _Value = GetSelectMonthYear();
                return _Value;
            }
            set { _Value = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Page_Init(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {
                    txtValue.CssClass = _TextBoxCss;
                    lbtnSelect.CssClass = _SelectButtonCss;
                    lbtnSet.CssClass = _SetButtonCss;
                    pnlDate.CssClass = _PanelCss;

                    ddlYear.Items.Clear();
                    _MinYear = _MinYear == 0 ? DateTime.MinValue.Year : _MinYear;
                    _MaxYear = _MaxYear == 0 ? DateTime.MaxValue.Year : _MaxYear;
                    for (int i = _MinYear; i <= _MaxYear; i++)
                        ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));

                    ddlMonth.Items.Clear();
                    _MinMonth = _MinMonth == 0 ? DateTime.MinValue.Month : _MinMonth;
                    _MaxMonth = _MaxMonth == 0 ? DateTime.MaxValue.Month : _MaxMonth;
                    for (int i = _MinMonth; i <= _MaxMonth; i++)
                        ddlMonth.Items.Add(new ListItem(GetMonth(i), i.ToString()));
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("~/Error.aspx?Error=" + ex.Message + "");
            }
        }
        private string GetSelectMonthYear()
        {
            string _ReturnValue = string.Empty;
            string _Month = string.Empty;

            if (!string.IsNullOrEmpty(txtValue.Text))
            {
                string[] _strValue = txtValue.Text.Split(' ');

                switch (Convert.ToString(_strValue[0]).ToLower())
                {
                    case "jan":
                        _Month = "01";
                        break;
                    case "feb":
                        _Month = "02";
                        break;
                    case "mar":
                        _Month = "03";
                        break;
                    case "apr":
                        _Month = "04";
                        break;
                    case "may":
                        _Month = "05";
                        break;
                    case "jun":
                        _Month = "06";
                        break;
                    case "jul":
                        _Month = "07";
                        break;
                    case "aug":
                        _Month = "08";
                        break;
                    case "sep":
                        _Month = "09";
                        break;
                    case "oct":
                        _Month = "10";
                        break;
                    case "nov":
                        _Month = "11";
                        break;
                    case "dec":
                        _Month = "12";
                        break;
                    default:
                        break;
                }
                if (!(string.IsNullOrEmpty(_Month) & string.IsNullOrEmpty(Convert.ToString(_strValue[1]))))
                {
                    _ReturnValue = _Month + "," + Convert.ToString(_strValue[1]);
                }

            }
            return _ReturnValue;
        }

        private string GetMonth(int Month)
        {
            string _ReturnValue = string.Empty;
            switch (Month)
            {
                case 1:
                    _ReturnValue = "Jan";
                    break;
                case 2:
                    _ReturnValue = "Feb";
                    break;
                case 3:
                    _ReturnValue = "Mar";
                    break;
                case 4:
                    _ReturnValue = "Apr";
                    break;
                case 5:
                    _ReturnValue = "May";
                    break;
                case 6:
                    _ReturnValue = "Jun";
                    break;
                case 7:
                    _ReturnValue = "Jul";
                    break;
                case 8:
                    _ReturnValue = "Aug";
                    break;
                case 9:
                    _ReturnValue = "Sep";
                    break;
                case 10:
                    _ReturnValue = "Oct";
                    break;
                case 11:
                    _ReturnValue = "Nov";
                    break;
                case 12:
                    _ReturnValue = "Dec";
                    break;
                default:
                    break;
            }
            return _ReturnValue;
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            pnlDate.Visible = !pnlDate.Visible;
            if (pnlDate.Visible)
            {
                ddlMonth.SelectedValue = DateTime.Now.Month.ToString();
                ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            }
        }

        protected void btnSet_Click(object sender, EventArgs e)
        {
            txtValue.Text = ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedValue;
            pnlDate.Visible = false;
        }
    }
}