using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Microsoft.EntityFrameworkCore;
using MobileSample.Models;
using MobileSample.Services;

namespace MobileSample.ViewModels
{
    public class EmployeeItemViewModel : BaseViewModel
    {
        private readonly INavigation navigation;
        private readonly DatabaseService databaseService;

        private List<string> isActiveItems;
        public List<string> IsActiveItems
        {
            get { return isActiveItems; }
            set { SetProperty(ref isActiveItems, value); }
        }
        public string IsActiveDisplay
        {
            get => Data.IsActive ? "Oui" : "Non";
            set => Data.IsActive = value == "Oui";
        }

        private List<Department> departmentItems;
        public List<Department> DepartmentItems
        {
            get { return departmentItems; }
            set { SetProperty(ref departmentItems, value); }
        }
        
        public EmployeeItemViewModel(INavigation navigation, DatabaseService databaseService)
        {
            this.navigation = navigation;
            this.databaseService = databaseService;

            IsActiveItems = new List<string> { "Oui", "Non" };
            DepartmentItems = new List<Department>();
            DepartmentItems.Add(new Department() { Id = Guid.Empty.ToString(), Name = " " });
            using (var databaseContext = databaseService.GetDatabaseContext())
            {
                DepartmentItems.AddRange(databaseService.GetDepartments(databaseContext).AsNoTracking().ToList());
            }
        }

        private bool isNewData;
        public bool IsNewData
        {
            get { return isNewData; }
            set { SetProperty(ref isNewData, value); }
        }

        private Employee data;
        public Employee Data
        {
            get { return data; }
            set { SetProperty(ref data, value); }
        }

        public override void Init(object initData)
        {
            base.Init(initData);
            Data = (Employee)initData;
            if (Data == null)
            {
                Title = $"Ajouter un employé";
                isNewData = true;
                Data = new Employee();
                Data.Id = Guid.NewGuid().ToString();
                Data.Birthday = DateTime.Now;
            }
            else
            {
                Title = $"Modifier un employé";
            }

            //Prepare the navigation property for binding
            Data.Department = DepartmentItems.Where(w => w.Id == Guid.Empty.ToString()).First();
            if (!string.IsNullOrEmpty(Data.DepartmentId))
            {
                Data.Department = DepartmentItems.Where(w => w.Id == Data.DepartmentId).FirstOrDefault();
            }
        }

        private void NormalizeForeignKeyBindings()
        {
            //Prepare the foreign key property after binding, to ensure successful database modification
            string departmentId = null;
            if (Data.Department != null) departmentId = Data.Department.Id;
            if (departmentId == Guid.Empty.ToString()) departmentId = null;
            Data.Department = null;
            Data.DepartmentId = departmentId;
        }

        public ICommand SaveCommand => new Command(async () =>
        {
            NormalizeForeignKeyBindings();

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

            NormalizeForeignKeyBindings();

            using (var databaseContext = databaseService.GetDatabaseContext())
            {
                databaseContext.Remove(Data);
                await databaseContext.SaveChangesAsync();
            }

            await navigation.PopAsync();
        });
    }
}
