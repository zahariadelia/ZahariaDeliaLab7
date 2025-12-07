using System;
using Microsoft.Maui.Controls;
using ZahariaDeliaLab7.Models;

namespace ZahariaDeliaLab7
{
    public partial class ProductPage : ContentPage
    {
        private ShopList sl;

        public ProductPage(ShopList slist)
        {
            InitializeComponent();
            sl = slist;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listView.ItemsSource = await App.Database.GetProductsAsync();
        }

        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var product = (Product)BindingContext;
            await App.Database.SaveProductAsync(product);
            listView.ItemsSource = await App.Database.GetProductsAsync();
            BindingContext = new Product(); // reset editor
        }

        async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var product = listView.SelectedItem as Product;
            if (product != null)
            {
                await App.Database.DeleteProductAsync(product);
                listView.ItemsSource = await App.Database.GetProductsAsync();
            }
            else
            {
                await DisplayAlert("Atenție", "Selectează un produs.", "OK");
            }
        }

        async void OnAddButtonClicked(object sender, EventArgs e)
        {
            var p = listView.SelectedItem as Product;
            if (p == null)
            {
                await DisplayAlert("Atenție", "Selectează un produs din listă.", "OK");
                return;
            }

            var lp = new ListProduct
            {
                ShopListID = sl.ID,
                ProductID = p.ID
            };
            await App.Database.SaveListProductAsync(lp);

            // Return to ListPage
            await Navigation.PopAsync();
        }
    }
}
