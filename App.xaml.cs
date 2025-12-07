using System;
using System.IO;
using ZahariaDeliaLab7.Data;
using ZahariaDeliaLab7.Models;

namespace ZahariaDeliaLab7
{
    public partial class App : Application
    {
        static ShopListDatabase database;

        public static ShopListDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ShopListDatabase(
                        Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShoppingList.db3"));
                }
                return database;
            }
        }

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new ListPage
            {
                BindingContext = new ShopList()
            });
        }

    }
}
