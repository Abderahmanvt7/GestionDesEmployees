using System;
using System.Windows.Input;
using Xamarin.Forms;
using MobileSample.Models;
using MobileSample.Services;
using MobileSample.Views;

namespace MobileSample.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly INavigation navigation;
        private readonly DatabaseService databaseService;

        public RegisterViewModel(INavigation navigation, DatabaseService databaseService)
        {
            this.navigation = navigation;
            this.databaseService = databaseService;
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

        private string confirmPassword;
        public string ConfirmPassword
        {
            get { return confirmPassword; }
            set { SetProperty(ref confirmPassword, value); }
        }

        public ICommand RegisterCommand => new Command(() =>
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password) || string.IsNullOrWhiteSpace(ConfirmPassword))
            {
                Application.Current.MainPage.DisplayAlert("Erreur", "Tous les champs doivent être remplis.", "OK");
                return;
            }

            if (Password != ConfirmPassword)
            {
                Application.Current.MainPage.DisplayAlert("Erreur", "Les mots de passe ne sont pas identiques.", "OK");
                return;
            }

            using (var databaseContext = databaseService.GetDatabaseContext())
            {
                var existingUser = databaseService.GetUserByUsername(databaseContext, Username);
                if (existingUser != null)
                {
                    Application.Current.MainPage.DisplayAlert("Erreur", "Nom d'utilisateur existe déjà.", "OK");
                    return;
                }

                var newUser = new User {
                    Id = Guid.NewGuid().ToString(),
                    Username = Username, 
                    Password = Password 
                };
                databaseService.AddUser(databaseContext, newUser);
                Application.Current.MainPage.DisplayAlert("Succès", "Utilisateur enregistré avec succès.", "OK");
                navigation.PopAsync();
            }
        });

        public ICommand NavigateToLoginCommand => new Command(() =>
        {
            navigation.PushAsync(new LoginPage());
        });
    }
}
