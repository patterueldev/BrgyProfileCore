using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using PasswordHashing;

namespace BrgyProfileCore.Core
{
    class BrgySession
    {
        public static User? current
        {
            get
            {
                var user = Properties.Settings.Default.UserLoggedIn;
                if(user != null && user.Trim() != "")
                {
                    var db = new BrgyContext();
                    var matchedUser = db.Users.Where(u => u.Username == user.Trim() ).ToList().First();
                    if(matchedUser != null)
                    {
                        return matchedUser;
                    }
                }
                return null;
            }
        }

        public static bool LoginUser(string username, string password)
        {
            var db = new BrgyContext();
            var matchedUser = db.Users.Where(u => u.Username == username).ToList().First();
            if (matchedUser != null)
            {
                var validated = PasswordHasher.Validate(password, matchedUser.Password);
                if (validated)
                {
                    Properties.Settings.Default.UserLoggedIn = username;
                    Properties.Settings.Default.Save();
                    return true;
                }
            }
            return false;
        }

        public static void LogoutUser()
        {
            Properties.Settings.Default.UserLoggedIn = "";
            Properties.Settings.Default.Save();
        }
    }
}
