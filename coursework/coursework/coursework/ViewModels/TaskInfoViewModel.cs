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
        private readonly ToDoService _toDoService;

        public ICommand UpdateTaskCommand { get; set; }
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

        public TaskInfoViewModel()
        {
            _taskService = new TaskService();
            _toDoService = new ToDoService();
            MinDate = DateTime.Now.Date;

            ToDo = new ObservableCollection<ToDoList>();

            UpdateTaskCommand = new Command(OnUpdatingTask);
            CreateToDoItemCommand = new Command(OnCreatingToDoItem);
            BackCommand = new Command(Back);
            DeleteToDoCommand = new Command<ToDoList>(OnDeletingToDoItem);
        }

        private async void OnUpdatingTask()
        {
            var toDoForDelete = ConcreteTask.ToDoList
                .Except(ToDo)
                .Where(t => t.Id != 0)
                .ToList();
            ConcreteTask.ToDoList.Clear();
            ConcreteTask.ToDoList.AddRange(ToDo);
            var response = await _taskService.Update(ConcreteTask);
            if (response != null)
            {
                foreach (var toDo in toDoForDelete)
                {
                    await _toDoService.Delete(toDo.Id);
                }

                var tasks = await _taskService.GetAllUserTasks(ConcreteTask.UserId);
                UserSingleton.GetInstance().GetUser().Tasks = tasks.ToList();
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
