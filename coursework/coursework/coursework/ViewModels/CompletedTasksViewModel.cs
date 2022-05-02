﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using coursework.Models;
using coursework.Services;
using coursework.Views;
using Xamarin.Forms;

namespace coursework.ViewModels
{
    public class CompletedTasksViewModel : BaseViewModel
    {
        private User _currentUser;
        private ObservableCollection<Task> _tasks;
        private TaskService _taskService;

        //for refresh items in refreshview (may be put away)
        //public ICommand LoadTasksItemsCommand { get; set; }
        public ICommand AddTaskCommand { get; set; }
        public Command<Task> ItemTappedCommand { get; set; }
        public string CompletedToDoItems { get; set; }

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


        public CompletedTasksViewModel()
        {
            _taskService = new TaskService();

            AddTaskCommand = new Command(OnAddTask);
            ItemTappedCommand = new Command<Task>(OnItemTapped);
            //LoadTasksItemsCommand = new Command(LoadTasks1);
        }

        public async void OnAddTask() =>
            await Shell.Current.GoToAsync(nameof(CreateTask));

        private ObservableCollection<Task> LoadTasks() =>
            CurrentUser.Tasks != null ? new ObservableCollection<Task>(CurrentUser.Tasks) : new ObservableCollection<Task>();

        public async void OnAppearing()
        {
            CurrentUser = UserSingleton.GetInstance().GetUser();
            CurrentUser.Tasks = (await _taskService.GetAllUserTasks(CurrentUser.Email))
                .Where(t => t.IsCompleted == true).ToList();
            Tasks = LoadTasks();

            foreach (var task in Tasks)
            {
                task.CountOfCompletedToDo = task.ToDoList.Count(t => t.IsCompleted);
                if (task.UserEmail != CurrentUser.Email)
                {
                    task.TaskOwner = $"{task.UserEmail} task";
                    OnPropertyChanged(nameof(Tasks));
                }
                else
                    task.TaskOwner = "Your task";
            }
        }
        private async void OnItemTapped(Task task) =>
            await Shell.Current.GoToAsync($"{nameof(TaskInfo)}?Id={task.Id}");
    }
}
