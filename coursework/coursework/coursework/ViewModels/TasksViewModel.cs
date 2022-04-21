using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Web;
using System.Windows.Input;
using coursework.Models;
using coursework.Services;
using coursework.Views;
using Xamarin.Forms;

namespace coursework.ViewModels
{
    public class TasksViewModel : BaseViewModel
    {
        private User _currentUser;
        private ObservableCollection<Task> _tasks;
        private readonly UserService _userService;

        //for refresh items in refreshview (may be put away)
        //public ICommand LoadTasksItemsCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }
        public Command<Task> ItemTappedCommand { get; set; }

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

            AddTaskCommand = new Command(OnAddTask);
            ItemTappedCommand = new Command<Task>(OnItemTapped);
            //LoadTasksItemsCommand = new Command(LoadTasks1);
        }

        public async void OnAddTask() =>
            await Shell.Current.GoToAsync(nameof(CreateTask));

        private ObservableCollection<Task> LoadTasks() =>
            CurrentUser.Tasks != null ? new ObservableCollection<Task>(CurrentUser.Tasks) : new ObservableCollection<Task>();

        public void OnAppearing()
        {
            CurrentUser = UserSingleton.GetInstance().GetUser();
            Tasks = LoadTasks();
        }

        private async void OnItemTapped(Task task) =>
            await Shell.Current.GoToAsync($"{nameof(TaskInfo)}?Id={task.Id}");
    }
}
