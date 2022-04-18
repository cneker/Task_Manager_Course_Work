using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using coursework.Models;
using coursework.Services;
using Xamarin.Forms;

namespace coursework.ViewModels
{
    public class TasksViewModel : BaseViewModel, IQueryAttributable
    {
        private User _currentUser;
        private ObservableCollection<Task> _tasks;
        private readonly UserService _userService;

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
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            string id = HttpUtility.UrlDecode(query["Id"]);
            GetUser(int.Parse(id));
        }

        private async void GetUser(int id)
        {
            var user = await _userService.Get(id);
            if (user != null)
                CurrentUser = user;
            Tasks = LoadTasks();
        }

        private ObservableCollection<Task> LoadTasks() =>
            CurrentUser.Tasks != null ? new ObservableCollection<Task>(CurrentUser.Tasks) : new ObservableCollection<Task>();
    }
}
