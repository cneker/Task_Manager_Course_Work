using System;
using coursework.Models;
using coursework.Services;
using coursework.ViewModels;
using coursework.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace coursework
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
            var userEmail = Preferences.Get("Email", "");
            var userPassword = Preferences.Get("Password", "");
            if (!userEmail.Equals("") && !userPassword.Equals(""))
            {
                UserSingleton.GetInstance(new User() { Email = userEmail, Password = userPassword });
                Shell.Current.GoToAsync("//Tasks");
            }
            else
            {
                Shell.Current.GoToAsync("//LoginPage");
            }
        }

        protected override void OnSleep()
        {
            
        }

        protected override void OnResume()
        {
        }
    }
}
