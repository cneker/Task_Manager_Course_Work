using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using coursework.Annotations;
using coursework.Models;
using coursework.Services;
using Xamarin.Forms;

namespace coursework.ViewModels
{
    public class AuthorizationViewModel :INotifyPropertyChanged
    {
        //private bool _initialized = false;
        private bool _isBusy;
        private User _selectedUser;

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
                OnPropertyChanged(nameof(IsBusy));
                OnPropertyChanged(nameof(IsLoaded));
            }
        }

        public bool IsLoaded => !_isBusy;

        public AuthorizationViewModel()
        {
            IsBusy = false;
            //CreateUserCommand = new Command(CreateUser);
            //GetUserCommand = new Command(GetUser);
        }

        //public User SelectedUser
        //{
        //    get => _selectedUser;
        //    set
        //    {
        //        var addedUser = 
        //    }
        //}

        //public async User CreateUser()
        //{
        //    IsBusy = true;
        //    var user = await _userService.Add()
        //}


        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
