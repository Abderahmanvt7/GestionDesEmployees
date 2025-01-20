using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using MobileSample.Models;
using MobileSample.Services;

namespace MobileSample.ViewModels
{
    public class DepartmentItemViewModel : BaseViewModel
    {
        private readonly INavigation navigation;
        private readonly DatabaseService databaseService;

        public DepartmentItemViewModel(INavigation navigation, DatabaseService databaseService)
        {
            this.navigation = navigation;
            this.databaseService = databaseService;
        }

        private bool isNewData;
        public bool IsNewData
        {
            get { return isNewData; }
            set { SetProperty(ref isNewData, value);  }
        }

        private Department data;
        public Department Data
        {
            get { return data; }
            set { SetProperty(ref data, value); }
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Data = (Department)initData;
            if (Data == null)
            {
                Title = $"Ajouter un département";
                isNewData = true;
                Data = new Department();
                Data.Id = Guid.NewGuid().ToString();
            }
            else
            {
                Title = $"Modifier un département";
            }
        }

        public ICommand SaveCommand => new Command(async () =>
        {
            using (var databaseContext = databaseService.GetDatabaseContext())
            {
                if (IsNewData)
                {
                    databaseContext.Add(Data);
                }
                else
                {
                    databaseContext.Update(Data);
                }
                await databaseContext.SaveChangesAsync();   
            }

            await navigation.PopAsync();
        });

        public ICommand DeleteCommand => new Command(async () =>
        {
            if (IsNewData) return;

            using (var databaseContext = databaseService.GetDatabaseContext())
            {
                Employee dependentEmployee = databaseService.GetEmployees(databaseContext).Where(w => w.DepartmentId == Data.Id).FirstOrDefault();
                if (dependentEmployee != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Attention", $"Ce département ne peut pas être supprimé car il contient des employés", "OK");
                    return;
                }

                databaseContext.Remove(Data);

                await databaseContext.SaveChangesAsync();
            }

            await navigation.PopAsync();
        });
    }
}
