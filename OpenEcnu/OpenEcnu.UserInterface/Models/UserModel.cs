using System;

namespace OpenEcnu.UserInterface.Models
{
    public class UserModel
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
    }
}