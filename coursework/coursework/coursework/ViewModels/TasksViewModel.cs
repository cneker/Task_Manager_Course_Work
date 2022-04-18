using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Windows.Input;
using coursework.Models;
using coursework.Services;
using Xamarin.Forms;

namespace coursework.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        private User _currentUser;
        private ObservableCollection<Task> _tasks;
        private readonly UserService _userService;

        public ICommand LoadTasksItemsCommand { get; set; }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Task> Tasks
        {
            get => _tasks;
            set
            {
                _tasks = value;
                OnPropertyChanged();
            }
        }

        public TasksViewModel()
        {
            _userService = new UserService();
            CurrentUser = UserSingleton.GetInstance().GetUser();
            Tasks = LoadTasks();
            //LoadTasksItemsCommand = new Command(LoadTasks1);
        }

        //public void LoadTasks1()
        //{
        //    Tasks = LoadTasks();
        //}

        private ObservableCollection<Task> LoadTasks() =>
            CurrentUser.Tasks != null ? new ObservableCollection<Task>(CurrentUser.Tasks) : new ObservableCollection<Task>();
    }
}
