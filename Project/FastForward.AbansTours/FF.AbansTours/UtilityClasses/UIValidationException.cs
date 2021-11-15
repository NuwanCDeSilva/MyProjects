using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FF.AbansTours
{
    /// <summary>
    /// This class for  User Interface exception handling.
    /// Created By : Miginda Geeganage.
    /// Created On : 05/03/2012
    /// Modified By :
    /// Modified On :
    /// </summary>
    public class UIValidationException : Exception
    {
        private string errorMessege = string.Empty;

        public string ErrorMessege
        {
            get { return errorMessege; }
            set { errorMessege = value; }
        }

        public UIValidationException(string messege)
        {
            this.errorMessege = messege;
        }
    }
}