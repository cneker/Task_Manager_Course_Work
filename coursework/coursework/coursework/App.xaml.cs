using System;
using coursework.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace coursework
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            //Routing.RegisterRoute(nameof(ConcreteTask), typeof(ConcreteTask));
            //Routing.RegisterRoute(nameof(AppShell), typeof(AppShell));
            ////Routing.RegisterRoute(nameof(ConcreteTask), typeof(ConcreteTask));
            ////Routing.RegisterRoute(nameof(ConcreteTask), typeof(ConcreteTask));
            ////Routing.RegisterRoute(nameof(ConcreteTask), typeof(ConcreteTask));

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
