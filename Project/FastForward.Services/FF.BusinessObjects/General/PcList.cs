using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class PcList
    {
        private string _com_cd;
        private string _pc_cd;
        private string _Type;
        private string _Type_value;
        private string _active;
        private string _zone;
        private string _com;
        private string _gpc;


        public string com_cd
        {
            get { return _com_cd; }
            set { _com_cd = value; }
        }
        public string pc_cd
        {
            get { return _pc_cd; }
            set { _pc_cd = value; }
        }
        public string Type
        {
            get { return _Type; }
            set { _Type = value; }
        }
        public string Type_value
        {
            get { return _Type_value; }
            set { _Type_value = value; }
        }
        public string active
        {
            get { return _active; }
            set { _active = value; }
        }
        public string Zone
        {
            get { return _zone; }
            set { _zone = value; }
        }
        public string Com
        {
            get { return _com; }
            set { _com = value; }
        }
        public string gpc
        {
            get { return _gpc; }
            set { _gpc = value; }
        }
    }
}
