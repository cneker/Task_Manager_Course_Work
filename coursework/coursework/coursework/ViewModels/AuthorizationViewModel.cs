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
using Xamarin.Essentials;
using Xamarin.Forms;

namespace coursework.ViewModels
{
    public class AuthorizationViewModel : BaseViewModel
    {
        private User _currentUser;

        private readonly UserService _userService = new UserService();

        public ICommand CreateUserCommand { protected set; get; }
        public ICommand GetUserCommand { protected set; get; }
        public ICommand GoToRegistrationCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

        public AuthorizationViewModel()
        {
            _currentUser = new User();
            CreateUserCommand = new Command(CreateUser);
            GetUserCommand = new Command(GetUser);
            GoToRegistrationCommand = new Command(GoToRegistration);
            BackCommand = new Command(Back);
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
            var user = await _userService.Reg(_currentUser);
            if (user != null)
            {
                UserSingleton.GetInstance(user);
                await Shell.Current.GoToAsync($"//Tasks");
            }
        }

        public async void GetUser()
        {
            var user = await _userService.Log(_currentUser);
            if (user != null)
            {
                Preferences.Set("Email", user.Email);
                Preferences.Set("Password", user.Password);

                UserSingleton.GetInstance(user);
                await Shell.Current.GoToAsync($"//Tasks");
            }
        }

        public async void GoToRegistration()
        {
            await Shell.Current.GoToAsync("//LoginPage/RegistrationPage");
        }

        public async void Back()
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
