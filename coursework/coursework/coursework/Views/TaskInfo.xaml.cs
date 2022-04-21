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
    public partial class TaskInfo : ContentPage
    {
        private TaskInfoViewModel ViewModel;
        public TaskInfo()
        {
            InitializeComponent();

            BindingContext = ViewModel = new TaskInfoViewModel();
        }
    }
}