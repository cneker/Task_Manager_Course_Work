using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using coursework.Annotations;
using coursework.Models;
using coursework.Services;
using coursework.Views;
using Xamarin.Forms;

namespace coursework.ViewModels
{
    public class AuthorizationViewModel :INotifyPropertyChanged
    {
        //private bool _initialized = false;
        private bool _isBusy;
        private User _currentUser;

        private UserService _userService = new UserService();

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand CreateUserCommand { protected set; get; }
        public ICommand GetUserCommand { protected set; get; }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                _isBusy = value;
                //OnPropertyChanged(nameof(IsBusy));
                //OnPropertyChanged(nameof(IsLoaded));
            }
        }

        public bool IsLoaded => !_isBusy;

        public AuthorizationViewModel()
        {
            _currentUser = new User();
            IsBusy = false;
            CreateUserCommand = new Command(CreateUser);
            GetUserCommand = new Command(GetUser);
        }

        public User CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                OnPropertyChanged();
            }
        }

        public async void CreateUser()
        {
            IsBusy = true;
            var user = await _userService.Add(_currentUser);
            if (user != null)
                await Shell.Current.GoToAsync($"//{nameof(TasksPage)}");
        }

        public async void GetUser()
        {
            IsBusy = true;
            var user = await _userService.Get(_currentUser);
            if (user != null)
                await Shell.Current.GoToAsync($"//{nameof(TasksPage)}");
        }


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
