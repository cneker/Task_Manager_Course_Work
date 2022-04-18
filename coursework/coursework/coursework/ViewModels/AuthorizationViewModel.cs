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
    public class AuthorizationViewModel : BaseViewModel
    {
        private bool _isBusy;
        private User _currentUser;

        private readonly UserService _userService = new UserService();

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
            var user = await _userService.Reg(_currentUser);
            if (user != null)
                await Shell.Current.GoToAsync($"//{nameof(TasksPage)}?Id={user.Id.ToString()}");
        }

        public async void GetUser()
        {
            IsBusy = true;
            var user = await _userService.Log(_currentUser);
            if (user != null)
            {
                UserSingleton.GetInstance(user);
                await Shell.Current.GoToAsync($"//{nameof(TasksPage)}");
            }
        }
    }
}
