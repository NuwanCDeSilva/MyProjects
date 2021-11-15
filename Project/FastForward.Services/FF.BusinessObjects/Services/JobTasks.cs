using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class JobTasks
    {
        private string _task;
        private string _taskRef;
        private DateTime _taskDate;
        private string _taskUpdBy;
        private DateTime _taskUpdTime;
        private string _taskuserName;
        private string _taskstutes;

        public string Task_Desc
        {
            get { return _task; }
            set { _task = value; }
        }
        public string Taskstutes
        {
            get { return _taskstutes; }
            set { _taskstutes = value; }
        }

        public string Task_Ref
        {
            get { return _taskRef; }
            set { _taskRef = value; }
        }

        public DateTime Task_Date
        {
            get { return _taskDate; }
            set { _taskDate = value; }
        }

        public string Task_UpdBy
        {
            get { return _taskUpdBy; }
            set { _taskUpdBy = value; }
        }

        public DateTime Task_UpdTime
        {
            get { return _taskUpdTime; }
            set { _taskUpdTime = value; }
        }
        public string Task_userName
        {
            get { return _taskuserName; }
            set { _taskuserName = value; }
        }

    }
}
