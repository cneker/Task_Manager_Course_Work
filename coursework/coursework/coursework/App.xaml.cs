﻿using System;
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
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            base.OnSleep();
        }

        protected override void OnResume()
        {
        }
    }
}
