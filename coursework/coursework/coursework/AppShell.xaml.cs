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
            Routing.RegisterRoute("//CurrentTasksPage/CreateTask", typeof(CreateTask));
            Routing.RegisterRoute("//CompletedTasksPage/CreateTask", typeof(CreateTask));
            Routing.RegisterRoute($"//CurrentTasksPage/{nameof(TaskInfo)}", typeof(TaskInfo));
            Routing.RegisterRoute($"//CompletedTasksPage/{nameof(TaskInfo)}", typeof(TaskInfo));

            if (true)
                GoToAsync("//LoginPage");
        }
        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}