using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FF.BusinessObjects.Enadoc
{
    public class DocSearchParam
    {
        public string DocumentType { get; set; }
        public string DocumentNo { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public int IsUpload { get; set; }
        public string GlblLoc { get; set; }
        public string GlblCom{ get; set; }
    }
}