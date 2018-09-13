using System;
using System.Collections.Generic;

namespace DongdaCS.Models {
    public class PhMaxJob {
        public string id { get; set; }
        public string user_id { get; set; }
        public string company_id { get; set; }
        public string date { get; set; }
        public string call { get; set; }
        public Dictionary<String, String> args { get; set; }
   }
}
