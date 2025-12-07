using System;
using Microsoft.Maui.Controls;

namespace ZahariaDeliaLab7
{
    public partial class ListEntryPage : ContentPage
    {
        public ListEntryPage()
        {
            InitializeComponent();
        }

        private void OnShopListAddedClicked(object sender, EventArgs e)
        {
            DisplayAlert("Add", "Shopping list added!", "OK");
        }

        private void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem != null)
            {
                DisplayAlert("Item Selected", "Ai selectat un element din listă.", "OK");

                listView.SelectedItem = null;
            }
        }

        private void OnSaveButtonClicked(object sender, EventArgs e)
        {
            DisplayAlert("Save", "Item saved!", "OK");
        }

        private void OnDeleteButtonClicked(object sender, EventArgs e)
        {
            DisplayAlert("Delete", "Item deleted!", "OK");
        }
    }
}
