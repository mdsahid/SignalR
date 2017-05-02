using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SignalRAuth.Models
{
    public class UserMessage
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Messages { get; set; }
        public DateTime PostDate { get; set; }
    }
}