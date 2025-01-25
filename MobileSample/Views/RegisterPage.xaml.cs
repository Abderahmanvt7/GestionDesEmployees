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
    public partial class RegisterPage : BaseContentPage<RegisterViewModel>
    {
        public RegisterPage() : base(null)
        {
            InitializeComponent();
            BindingContext = new RegisterViewModel(Navigation, new DatabaseService());
        }
    }

}