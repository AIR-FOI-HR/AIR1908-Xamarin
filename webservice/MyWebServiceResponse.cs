using System;
using System.Collections.Generic;
using System.Text;

namespace webservice
{
    public class MyWebServiceResponse
    {
        public int ResponseId { get; set; }
        public String ResponseText { get; set; }
        public long TimeStamp { get; set; }
        public String Items { get; set; }
    }
}
