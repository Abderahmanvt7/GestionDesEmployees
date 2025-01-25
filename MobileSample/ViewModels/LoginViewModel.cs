using System;
using System.Windows.Input;
using Xamarin.Forms;
using MobileSample.Services;
using MobileSample.Views;
using MobileSample.Models;

namespace MobileSample.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly INavigation navigation;
        private readonly DatabaseService databaseService;
        private readonly UserService userService;

        public LoginViewModel(INavigation navigation, DatabaseService databaseService, UserService userService)
        {
            this.navigation = navigation;
            this.databaseService = databaseService;
            this.userService = userService;
        }

        private string username;
        public string Username
        {
            get { return username; }
            set { SetProperty(ref username, value); }
        }

        private string password;
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }

        public ICommand LoginCommand => new Command(async () =>
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                Application.Current.MainPage.DisplayAlert("Erreur", "Tous les champs doivent être remplis.", "OK");
                return;
            }

            using (var databaseContext = databaseService.GetDatabaseContext())
            {
                var isValidUser = databaseService.ValidateUser(databaseContext, Username, Password);
                if (isValidUser)
                {
                    userService.Username = Username;
                    Application.Current.MainPage.DisplayAlert("Succès", "Connexion réussie.", "OK");

                    Application.Current.MainPage = new MainPage();

                    var mainPage = Application.Current.MainPage as MainPage;
                    if (mainPage != null)
                    {
                        await mainPage.NavigateFromMenu((int)MenuItemType.About);
                    }
                }
                else
                {
                    Application.Current.MainPage.DisplayAlert("Erreur", "Nom d'utilisateur ou mot de passe incorrect..", "OK");
                }
            }
        });

        public ICommand NavigateToRegisterCommand => new Command(() =>
        {
            navigation.PushAsync(new RegisterPage());
        });
    }
}
