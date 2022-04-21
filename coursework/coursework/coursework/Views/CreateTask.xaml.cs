using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    public partial class CreateTask : ContentPage
    {
        public CreateTaskViewModel ViewModel;
        public CreateTask()
        {
            InitializeComponent();

            BindingContext = ViewModel = new CreateTaskViewModel();

        }
    }
}