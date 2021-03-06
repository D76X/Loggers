﻿using System;
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
using TestShopApp1.Utilities;

namespace TestShopApp1 {
    [Activity(Label = "Item details", MainLauncher = false)]
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

            // you may reach this activity and view by means of an
            // intent to navigation from another activity i.e. the
            // activity whose view is the list of items after the 
            // user tap one one of them
            var selectedItemId = Intent.Extras.GetInt("selectedItemId");

            if (selectedItemId == 0) {
                // case when the navigation is direct i.e. during testing
                // when this activity is marcked with MainLauncher = true
                selectedItem = dataService.GetHotDogById(1);
            }
            else {
                // navigation to this activity has happened following 
                // the intent from the list of items
                selectedItem = dataService.GetHotDogById(selectedItemId);
            }        
            
            // keep references to the controls (views in Android)
            this.FindViews();

            // binds to the seletced data item
            this.BindData();

            // provide all the event handlers that the controls require
            // for the view's behaviour
            this.HanldeEvents();
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

        private void BindData() {

            this.itemNameTextView.Text = this.selectedItem.Name;
            this.itemShortDescriptionTextView.Text = this.selectedItem.ShortDescription;
            this.itemDescriptionTextView.Text = this.selectedItem.Description;
            this.itemPriceTextView.Text = "Price: " + this.selectedItem.Price;

            var imageBitMap = ImageHelper
                .GetImageBitmapFromUrl("http://gillcleerenpluralsight.blob.core.windows.net/files/" + selectedItem.ImagePath + ".jpg");

            this.itemImageView.SetImageBitmap(imageBitMap);
        }

        private void HanldeEvents() {

            this.orderButton.Click += OrderButton_Click;
            this.cancelButton.Click += CancelButton_Click;
        }
        
        private void OrderButton_Click(object sender, EventArgs e) {

            var amount = Int32.Parse(this.itemAmountEditText.Text);

            //this.AddToCart(this.selectedItem, amount);

            // prototype code
            //---------------------------------------------------------------
            //var dialog = new AlertDialog.Builder(this);
            //dialog.SetTitle("Confirmation");
            //dialog.SetMessage("Your hot dog has been added to your cart!");
            //dialog.Show();
            //---------------------------------------------------------------

            //---------------------------------------------------------------

            var intent = new Intent();
            intent.PutExtra("selectedItemId", this.selectedItem.HotDogId);
            intent.PutExtra("amount", amount);

            // SetResult causes the OnActivityResult to run on the activity 
            // that generated the intent to navigate to this activity that 
            // is the items list activity! 
            SetResult(Result.Ok, intent);

            // Finish destroys this activity and pops it from teh Activity Stack!
            this.Finish();
        }

        public void AddToCart(HotDog hotDog, int amount) {
            //CartDataService cartDataService = new CartDataService();
            //cartDataService.AddCartItem(hotDog, amount);
        }

        private void CancelButton_Click(object sender, EventArgs e) {

            //SetResult(Result.Canceled);
            //this.Finish();
        }
    }
}