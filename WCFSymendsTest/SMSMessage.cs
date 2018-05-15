using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WCFSymendsTest
{
    public class SMSMessage
    {

        public int id { get; set; }
        public string message_in { get; set; }
        public string message_out { get; set; }
        public DateTime Timestamp { get; set; }
      
    }
}