using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileSample.Models;
using MobileSample.ViewModels;
using MobileSample.Services;

namespace MobileSample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : BaseContentPage<LoginViewModel>
    {
        public LoginPage() : base(null)
        {
            InitializeComponent();
            BindingContext = new LoginViewModel(Navigation, new DatabaseService(), new UserService());
        }
    }

}