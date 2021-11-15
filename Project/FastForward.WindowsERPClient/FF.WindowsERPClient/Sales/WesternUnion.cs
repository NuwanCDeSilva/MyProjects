using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using FF.BusinessObjects;

namespace FF.WindowsERPClient.Sales
{
    public partial class WesternUnion : FF.WindowsERPClient.Base
    {
        public WesternUnion()
        {
            InitializeComponent();
            LoadCountry();
            LoadCurrency();
        }


        public void LoadCountry()
        {
            DataTable _dt = CHNLSVC.General.GetCountry();
            BindingSource _source1 = new BindingSource();
            BindingSource _source2 = new BindingSource();
            BindingSource _source3 = new BindingSource();

            _source1.DataSource = _dt;
            _source2.DataSource = _dt;
            _source3.DataSource = _dt;

            cmbOriginCountry.DataSource = _source1;
            cmbSendCountry.DataSource = _source2;
            cmbReceCountry.DataSource = _source3;

            cmbOriginCountry.ValueMember = "MCU_CD";
            cmbOriginCountry.DisplayMember = "MCU_DESC";
            cmbSendCountry.ValueMember = "MCU_CD";
            cmbSendCountry.DisplayMember = "MCU_DESC";
            cmbReceCountry.ValueMember = "MCU_CD";
            cmbReceCountry.DisplayMember = "MCU_DESC";

        }

        public void LoadCurrency()
        {
            List<MasterCurrency> _cur = CHNLSVC.General.GetAllCurrency(null);
            BindingSource _source1 = new BindingSource();
            BindingSource _source2 = new BindingSource();
            _source1.DataSource = _cur;
            _source2.DataSource = _cur;

            cmbReceCurrancy.DataSource = _source1;
            cmbSendCurrancy.DataSource = _source2;

            cmbReceCurrancy.DisplayMember = "Mcr_cd";
            cmbReceCurrancy.ValueMember = "Mcr_cd";

            cmbSendCurrancy.DisplayMember = "Mcr_cd";
            cmbSendCurrancy.ValueMember = "Mcr_cd";

        }






    }
}
