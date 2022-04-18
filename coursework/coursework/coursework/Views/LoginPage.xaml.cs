using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using coursework.Models;
using coursework.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace coursework.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public AuthorizationViewModel ViewModel;

        public LoginPage()
        {
            InitializeComponent();
            BindingContext = ViewModel = new AuthorizationViewModel();
        }

        private async void OnRegistrationClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage/RegistrationPage");
        }
    }
}