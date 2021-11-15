using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class ServiceTaskLocations
    {
        private string _code;
        private string _description;
        private Int32 _is_active;
        private string _type;
        private string _com;
        private int _is_default;
        private string _scs_pty_tp;
        private string _scs_pty_code;
        private string _scs_tp;
        private string _create_by;
        private decimal _capacity;
        private string _location;

        public string Location
        {
            get { return _location; }
            set { _location = value; }
        }
        public decimal Capacity
        {
            get { return _capacity; }
            set { _capacity = value; }
        }
        public string Create_by
        {
            get { return _create_by; }
            set { _create_by = value; }
        }

        public string Scs_tp
        {
            get { return _scs_tp; }
            set { _scs_tp = value; }
        }

        public string Scs_pty_code
        {
            get { return _scs_pty_code; }
            set { _scs_pty_code = value; }
        }

        public string Scs_pty_tp
        {
            get { return _scs_pty_tp; }
            set { _scs_pty_tp = value; }
        }
        public int Is_default
        {
            get { return _is_default; }
            set { _is_default = value; }
        }
        public string Com
        {
            get { return _com; }
            set { _com = value; }
        }
        public string Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public Int32 Is_active
        {
            get { return _is_active; }
            set { _is_active = value; }
        }
        
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
    }
}
