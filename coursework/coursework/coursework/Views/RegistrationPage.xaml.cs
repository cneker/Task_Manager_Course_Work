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
    public partial class RegistrationPage : ContentPage
    {
        public AuthorizationViewModel ViewModel;
        public RegistrationPage()
        {
            InitializeComponent();
            ViewModel = new AuthorizationViewModel();
            BindingContext = ViewModel;

        }
    }
}