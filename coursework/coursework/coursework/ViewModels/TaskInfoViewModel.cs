using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Input;
using coursework.Models;
using coursework.Services;
using Xamarin.Forms;

namespace coursework.ViewModels
{
    public class TaskInfoViewModel : BaseViewModel, IQueryAttributable
    {
        private Task _concreteTask;
        private ObservableCollection<ToDoList> _toDo;
        private readonly TaskService _taskService;
        private readonly UserService _userService;
        private ToDoList _selectedItem;

        public ICommand UpdateTaskCommand { get; set; }
        public ICommand CreateToDoItemCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand DeleteToDoCommand { get; set; }


        public Task ConcreteTask
        {
            get => _concreteTask;
            set
            {
                _concreteTask = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ToDoList> ToDo
        {
            get => _toDo;
            set
            {
                _toDo = value;
                OnPropertyChanged();
            }
        }

        public ToDoList SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public TaskInfoViewModel()
        {
            _taskService = new TaskService();
            _userService = new UserService();

            ToDo = new ObservableCollection<ToDoList>();

            UpdateTaskCommand = new Command(OnUpdatingTask);
            CreateToDoItemCommand = new Command(OnCreatingToDoItem);
            BackCommand = new Command(Back);
            DeleteToDoCommand = new Command(OnDeletingToDoItem);
        }

        private async void OnUpdatingTask()
        {
            ConcreteTask.ToDoList.Clear();
            ConcreteTask.ToDoList.AddRange(ToDo);
            var response = await _taskService.Update(ConcreteTask);
            if (response != null)
            {
                var tasks = await _taskService.GetAllUserTasks(ConcreteTask.UserId);
                UserSingleton.GetInstance().GetUser().Tasks = tasks.ToList();
                Back();
            }
        }

        private void OnCreatingToDoItem()
        {
            ToDo.Add(new ToDoList() { TaskId = ConcreteTask.Id, IsCompleted = false });
        }

        private void OnDeletingToDoItem()
        {
            ToDo.Remove(SelectedItem);
            SelectedItem = null;
        }

        private async void Back() =>
            await Shell.Current.GoToAsync("..");

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            var id = HttpUtility.UrlDecode(query["Id"]);
            LoadTask(int.Parse(id));
        }

        private async void LoadTask(int id)
        {
            var task = await _taskService.Get(id);
            ConcreteTask = task;
            InitToDo(ConcreteTask.ToDoList);
        }

        private void InitToDo(IEnumerable<ToDoList> value)
        {
            foreach (var toDoList in value)
            {
                ToDo.Add(toDoList);
            }
        }
    }
}
