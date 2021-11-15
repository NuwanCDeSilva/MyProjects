using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Rpc
{
    public class User
    {
        public Guid Id { get; set; }
        public string PhoneNo { get; set; }
        public string Name { get; set; }
        public string CardNo { get; set; }
        public string Email { get; set; }
        public decimal PointAmount { get; set; }
        public UserStatus Status { get; set; }
    }
}
