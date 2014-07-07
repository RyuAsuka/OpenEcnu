using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SampleClient.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Birthday { get; set; }
    }
}