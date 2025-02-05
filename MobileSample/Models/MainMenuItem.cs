﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MobileSample.Models
{
    public enum MenuItemType
    {
        About,
        DepartmentList,
        EmployeeList,
        ProjetsList,
        Profile,
    }

    public class MainMenuItem
    {
        public MenuItemType Id { get; set; }
        public string Title { get; set; }

        public static List<MainMenuItem> GetMenus()
        {
            List<MainMenuItem> menus = new List<MainMenuItem>();
            menus.Add(new MainMenuItem() { Id = MenuItemType.About, Title = "Accueil" });
            menus.Add(new MainMenuItem() { Id = MenuItemType.EmployeeList, Title = "Employés" });
            menus.Add(new MainMenuItem() { Id = MenuItemType.ProjetsList, Title = "Projets" });
            menus.Add(new MainMenuItem() { Id = MenuItemType.DepartmentList, Title = "Départements" });
            menus.Add(new MainMenuItem() { Id = MenuItemType.Profile, Title = "Profile" });
            return menus;
        }
    }
}
