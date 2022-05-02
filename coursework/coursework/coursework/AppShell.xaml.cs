using coursework.Views;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace coursework
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("//LoginPage/RegistrationPage", typeof(RegistrationPage));
            Routing.RegisterRoute("//Tasks/CreateTask", typeof(CreateTask));
            Routing.RegisterRoute($"//Tasks/{nameof(TaskInfo)}", typeof(TaskInfo));

        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            Preferences.Remove("Email");
            Preferences.Remove("Password");

            await GoToAsync("//LoginPage");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}