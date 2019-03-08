using OpenGpsTracker.Model;
using OpenGpsTracker.Repository;
using OpenGpsTracker.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace OpenGpsTracker.ViewModel
{
    public class SettingsViewModel
    {
        public ICommand Save { get; }

        public string Login { get; set; }
        public string Password { get; set; }

        public SettingsViewModel()
        {
            Save = new Command(SaveUserSettings);
        }


        public void SaveUserSettings()
        {
            User user = new User();
            user.Login = this.Login;
            user.Password = this.Password;

            UserRepository userrep = new UserRepository(App.DATABASE_NAME);
            userrep.SaveUser(user);


        }
        

    }
}
