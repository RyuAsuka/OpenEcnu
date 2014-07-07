using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleClient.Models
{
    public class AccessTokenModel
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public string user_id { get; set; }
    }
}