using System;
using Microsoft.Maui.Controls;
using ZahariaDeliaLab7.Models;

namespace ZahariaDeliaLab7
{
    public partial class ListPage : ContentPage
    {
        public ListPage()
        {
            InitializeComponent();
        }

        private async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            var shopl = (ShopList)BindingContext;
            await App.Database.SaveShopListAsync(shopl);
            await DisplayAlert("Succes", "Lista a fost salvată!", "OK");
        }

        private async void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            var shopl = (ShopList)BindingContext;
            if (shopl.ID != 0)
            {
                await App.Database.DeleteShopListAsync(shopl);
                await DisplayAlert("Șters", "Lista a fost ștearsă.", "OK");
                await Navigation.PopAsync();
            }
        }

        async void OnChooseButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
            {
                BindingContext = new Product()
            });
        }

        async void OnDeleteItemClicked(object sender, EventArgs e)
        {
            var selectedProduct = listView.SelectedItem as Product;
            if (selectedProduct == null)
            {
                await DisplayAlert("Atenție", "Selectează un produs din listă.", "OK");
                return;
            }

            var shopl = (ShopList)BindingContext;
            var assoc = await App.Database.GetListProductAssociationAsync(shopl.ID, selectedProduct.ID);
            if (assoc != null)
            {
                await App.Database.DeleteListProductAsync(assoc);
            }

            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var shopl = (ShopList)BindingContext;
            listView.ItemsSource = await App.Database.GetListProductsAsync(shopl.ID);
        }
    }
}
