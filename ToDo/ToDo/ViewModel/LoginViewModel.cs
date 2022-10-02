using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ToDo.Model;
using ToDo.Services;
using Xamarin.Forms;

namespace ToDo.ViewModel
{
    public class LoginViewModel : ContentPage
    {
        public Page Page { get; set; }

        public string username;
        public string Username
        {
            get { return username; }
            set
            {
                username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public string password;
        public string Password
        {
            get { return password; }
            set
            {
                password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public Command AddUserCommand { get; set; }
        public Command UserAuthCommand { get; set; }


        public LoginViewModel(Page page)
        {
            Page = page;
            AddUserCommand = new Command(async () => await AddUser(username, password));
            UserAuthCommand = new Command(async () => await UserAuth(username, password));
        }

        public async Task AddUser(string username, string password)
        {
            byte[] data = Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hashPassword = Encoding.ASCII.GetString(data);

            bool isOk = await DynamoDBUserService._instance.AddUser(username, hashPassword);
            if (isOk)
            {
                CurrentUser.Username = username;
                Application.Current.MainPage = new NavigationPage(new MainPage());
                return;
            }
            else
            {
                await Page.DisplayAlert("This username is used", "Please try again", "Ok");
            }
        }

        public async Task<bool> UserAuth(string username, string password)
        {
            byte[] data = Encoding.ASCII.GetBytes(password);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            string hashPassword = Encoding.ASCII.GetString(data);

            bool isOk = await DynamoDBUserService._instance.UserAuth(username, hashPassword);
            if (isOk)
            {
                CurrentUser.Username = username;
                Application.Current.MainPage = new NavigationPage(new MainPage());
                return true;
            }
            else
            {
                await Page.DisplayAlert("Invalid username or password", "Please try again", "Ok");
                return false;
            }
        }
    }
}
