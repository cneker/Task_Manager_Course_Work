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
    public class CreateTaskViewModel : BaseViewModel
    {
        private Task _concreteTask;
        private ObservableCollection<ToDoList> _toDo;
        private readonly TaskService _taskService;

        public ICommand CreateTaskCommand { get; set; }
        public ICommand CreateToDoItemCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public Command<ToDoList> DeleteToDoCommand { get; set; }
        public DateTime MinDate { get; }


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

        public CreateTaskViewModel()
        {
            _taskService = new TaskService();
            MinDate = DateTime.Now.Date;

            ConcreteTask = new Task()
                { UserId = UserSingleton.GetInstance().GetUser().Id, DeadLine = DateTime.Now.Date };
            ConcreteTask.ToDoList = new List<ToDoList>();

            ToDo = new ObservableCollection<ToDoList>();

            CreateTaskCommand = new Command(OnCreatingTask);
            CreateToDoItemCommand = new Command(OnCreatingToDoItem);
            BackCommand = new Command(Back);
            DeleteToDoCommand = new Command<ToDoList>(OnDeletingToDoItem);
        }

        private async void OnCreatingTask()
        {
            ConcreteTask.ToDoList.AddRange(ToDo);
            var response = await _taskService.Add(ConcreteTask);
            if (response != null)
            {
                var user = UserSingleton.GetInstance().GetUser();
                user.Tasks.Add(response);
                Back();
            }
        }

        private void OnCreatingToDoItem()
        {
            ToDo.Add(new ToDoList() { TaskId = ConcreteTask.Id, IsCompleted = false });
        }

        private void OnDeletingToDoItem(ToDoList toDo)
        {
            ToDo.Remove(toDo);
        }

        private async void Back() =>
            await Shell.Current.GoToAsync("..");


    }
}
