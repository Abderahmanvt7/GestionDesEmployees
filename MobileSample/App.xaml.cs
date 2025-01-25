using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileSample.Views;
using Autofac;
using MobileSample.Services;
using MobileSample.ViewModels;
using Xamarin.Forms.Internals;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MobileSample
{
    public partial class App : Application
    {
        public static IContainer Container;
        private static readonly ContainerBuilder builder = new ContainerBuilder();

        // Track user authentication state
        public static bool IsAuthenticated { get; set; } = false;

        public App()
        {
            InitializeComponent();

            // Register dependencies
            DependencyResolver.ResolveUsing(type => Container.IsRegistered(type) ? Container.Resolve(type) : null);
            builder.RegisterType<DatabaseService>();
            builder.RegisterType<UserService>().AsSelf().SingleInstance(); // Register UserService
            builder.RegisterType<AboutViewModel>();
            builder.RegisterType<DepartmentListViewModel>();
            builder.RegisterType<DepartmentItemViewModel>();
            builder.RegisterType<EmployeeListViewModel>();
            builder.RegisterType<EmployeeItemViewModel>();
            builder.RegisterType<ProjetListViewModel>();
            builder.RegisterType<RegisterViewModel>();
            builder.RegisterType<LoginViewModel>();
            builder.RegisterType<ProfileViewModel>();

            Container = builder.Build();

            // Set the initial page based on authentication state
            if (IsAuthenticated)
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new RegisterPage());
            }
        }

        public static void RedirectToMainPage()
        {
            Current.MainPage = new NavigationPage(new MainPage());
        }

        public static void RedirectToLoginPage()
        {
            Current.MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
