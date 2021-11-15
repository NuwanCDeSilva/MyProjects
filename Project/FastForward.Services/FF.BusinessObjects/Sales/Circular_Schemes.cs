using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Sales
{
    public class Circular_Schemes
    {
        private string _circ_Sch;
        private string _circ;

        public string Circ_Sch
        {
            get { return _circ_Sch; }
            set { _circ_Sch = value; }
        }
        public string Circ
        {
            get { return _circ; }
            set { _circ = value; }
        }
    }
}
