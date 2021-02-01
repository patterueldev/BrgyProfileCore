using System;
using System.Collections.Generic;
using System.Text;

namespace BrgyProfileCore
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }
}