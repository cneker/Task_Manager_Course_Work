//using App2.ViewModels;
//using coursework.Views;
using coursework.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace coursework
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("//LoginPage/RegistrationPage", typeof(RegistrationPage));
            Routing.RegisterRoute("//TasksPage/CreateTask", typeof(CreateTask));
            Routing.RegisterRoute("//TasksPage/Test", typeof(Test));
            //Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            //Routing.RegisterRoute(nameof(TasksPage), typeof(TasksPage));
            //Routing.RegisterRoute(nameof(AccountPage), typeof(AccountPage));

            //Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            if (true)
                GoToAsync("//LoginPage");
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}