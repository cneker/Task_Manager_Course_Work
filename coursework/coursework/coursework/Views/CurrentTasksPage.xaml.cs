using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coursework.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace coursework.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentTasksPage : ContentPage
    {
        private CurrentTasksViewModel ViewModel;
        public CurrentTasksPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new CurrentTasksViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}