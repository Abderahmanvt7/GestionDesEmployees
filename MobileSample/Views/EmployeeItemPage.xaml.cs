﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileSample.Models;
using MobileSample.ViewModels;

namespace MobileSample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EmployeeItemPage : BaseContentPage<EmployeeItemViewModel>
	{
		public EmployeeItemPage(object initData) : base(initData)
		{
			InitializeComponent();
            BindingContext = ViewModel;

            if (ViewModel.IsNewData)
            {
                for (int i = 0; i < ToolbarItems.Count; i++)
                {
                    if (ToolbarItems[i].Text == "Supprimer")
                    {
                        ToolbarItems.Remove(ToolbarItems[i]);
                        break;
                    }
                }
            }
        }

        public EmployeeItemPage() : this(null)
        {
        }
	}
}