using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    public class ExcelUploadErr
    {//Dulaj 2018/Sep/14  SCM WEB MVC EXCEL UPLOAD ERROR LIST. THIS CAN USE SCM WEB MVC EXCEL SCHEET UPLOAD FUNCTION
        public Int32 LineNo { get; set; }
        public String  Code { get; set; }
        public String Description  { get; set; }

        public ExcelUploadErr() { }
        public ExcelUploadErr(int lineNo,string code,string description)
        {
            this.LineNo = lineNo;
            this.Code = code;
            this.Description = description;
        }
    }
}
