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
    public partial class ProfilePage : BaseContentPage<ProfileViewModel>
    {
        public ProfilePage()
        {
            InitializeComponent();
            BindingContext = new ProfileViewModel(Navigation, new DatabaseService(), new UserService());
        }
    }

}