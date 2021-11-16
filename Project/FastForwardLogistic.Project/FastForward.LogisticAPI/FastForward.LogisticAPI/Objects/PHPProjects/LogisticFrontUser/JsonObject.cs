using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FastForward.LogisticAPI.Objects.PHPProjects.LogisticFrontUser
{
    public class JsonObject
    {
        private Object data;
        private Boolean success;
        private string message;
        private int errorNumber;
        private bool login;

        public Object Data { get { return data; } set { data = value; } }
        public Boolean Success { get { return success; } set { success = value; } }

        public string Message { get { return message; } set { message = value; } }

        public int ErrorNumber { get { return errorNumber; } set { errorNumber = value; } }
        public bool Login { get { return login; } set { login = value; } }
    }
}