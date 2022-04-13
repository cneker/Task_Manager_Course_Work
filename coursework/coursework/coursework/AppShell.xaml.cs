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
            //Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            //Routing.RegisterRoute("//LoginPage//RegistrationPage", typeof(RegistrationPage));
            //Routing.RegisterRoute("//TasksPage//Task", typeof(ConcreteTask));
            Routing.RegisterRoute(nameof(ConcreteTask), typeof(ConcreteTask));
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}