using System;
using System.Collections.Generic;
using System.Text;
using coursework.Models;
using Xamarin.Forms.PlatformConfiguration;

namespace coursework.Services
{
    public class UserSingleton
    {
        private static UserSingleton _instance;
        private static User _user;

        private UserSingleton(User user)
        {
            _user = user;

        }

        public static UserSingleton GetInstance(User user = null)
        {
            if (user != null)
            {
                _instance = new UserSingleton(user);
            }

            return _instance;
        }

        public User GetUser()
        {
            return _user;
        }

    }
}
