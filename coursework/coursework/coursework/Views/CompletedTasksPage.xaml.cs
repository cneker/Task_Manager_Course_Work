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
    public partial class CompletedTasksPage : ContentPage
    {
        private CompletedTasksViewModel ViewModel;
        public CompletedTasksPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new CompletedTasksViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.OnAppearing();
        }
    }
}