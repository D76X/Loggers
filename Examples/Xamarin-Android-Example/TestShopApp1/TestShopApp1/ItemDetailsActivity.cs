using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TestApp1.Core;

namespace TestShopApp1 {
    [Activity(Label = "Item details", MainLauncher = true)]
    public class ItemDetailsActivity : Activity {

        private ImageView itemImageView;
        private TextView itemNameTextView;
        private TextView itemShortDescriptionTextView;
        private TextView itemDescriptionTextView;
        private TextView itemPriceTextView;
        private EditText itemAmountEditText;
        private Button cancelButton;
        private Button orderButton;

        private HotDog selectedItem;
        private HotDogDataService dataService;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            
            // Build the project to regenerate the Resource.Designer.cs

            // First link the activity to the corresponding layout
            SetContentView(Resource.Layout.ItemDetailsView);

            // grab the services
            this.dataService = new HotDogDataService();
            // grab the data
            selectedItem = dataService.GetHotDogById(1);
            
            // keep references to the controls (views in Android)
            this.FindViews();
        }

        private void FindViews() {

            this.itemImageView = FindViewById<ImageView>(Resource.Id.itemImageView);

            this.itemNameTextView = FindViewById<TextView>(Resource.Id.itemNameTextView);
            this.itemShortDescriptionTextView = FindViewById<TextView>(Resource.Id.itemShortDescriptionTextView);
            this.itemDescriptionTextView = FindViewById<TextView>(Resource.Id.itemDescriptionTextView);
            this.itemPriceTextView = FindViewById<TextView>(Resource.Id.itemPriceTextView);

            this.itemAmountEditText = FindViewById<EditText>(Resource.Id.itemAmountEditText);

            this.cancelButton = FindViewById<Button>(Resource.Id.cancelButton);
            this.orderButton = FindViewById<Button>(Resource.Id.orderButton);
        }
    }
}