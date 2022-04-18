using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using coursework.Models;
using coursework.Services;
using Xamarin.Forms;

namespace coursework.ViewModels
{
    public class ConcreteTaskViewModel : BaseViewModel, IQueryAttributable
    {
        private Task _concreteTask;
        private TaskService _taskService;


        public Task ConcreteTask
        {
            get => _concreteTask;
            set
            {
                _concreteTask = value;
                OnPropertyChanged();
            }
        }

        public ConcreteTaskViewModel()
        {
            _taskService = new TaskService();
        }

        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            var id = int.Parse(HttpUtility.UrlDecode(query["Id"]));
        }

        private async void GetTask(int id)
        {
            var task = await _taskService.Get(id);
            if (task != null)
            {
                ConcreteTask = task;
            }
        }
    }
}
