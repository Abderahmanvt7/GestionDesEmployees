using System;
using System.Windows.Input;
using Xamarin.Forms; // Add this line
using MobileSample.Models;
using MobileSample.Services;
using MobileSample.Views;

namespace MobileSample.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private readonly INavigation navigation;
        private readonly DatabaseService databaseService;
        private readonly UserService userService;

        public ProfileViewModel(INavigation navigation, DatabaseService databaseService, UserService userService)
        {
            this.navigation = navigation;
            this.databaseService = databaseService;
            this.userService = userService;

            LoadProfile();
        }

        private User user;
        public User User
        {
            get { return user; }
            set { SetProperty(ref user, value); }
        }

        private void LoadProfile()
        {
            using (var databaseContext = databaseService.GetDatabaseContext())
            {
                User = databaseService.GetUserByUsername(databaseContext, userService.Username);
            }
        }

        public ICommand LogoutCommand => new Command(() =>
        {
            Application.Current.MainPage.DisplayAlert("Info", "Vous avez été déconnecté.", "OK");
            Application.Current.MainPage = new NavigationPage(new LoginPage());
        });
    }
}