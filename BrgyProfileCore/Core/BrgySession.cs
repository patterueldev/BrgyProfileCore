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
                    var matchedUsers = db.Users.Where(u => u.Username == user.Trim() ).ToList();
                    if(matchedUsers.Count == 1)
                    {
                        return matchedUsers.First();
                    }
                }
                return null;
            }
        }

        public static bool LoginUser(string username, string password)
        {
            var db = new BrgyContext();
            var matchedUsers = db.Users.Where(u => u.Username == username).ToList();
            if (matchedUsers.Count == 1)
            {
                var matchedUser = matchedUsers.First();
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
