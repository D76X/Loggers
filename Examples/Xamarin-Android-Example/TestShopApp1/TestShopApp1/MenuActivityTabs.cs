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

namespace TestShopApp1 {
    [Activity(Label = "MenuActivity", MainLauncher = true)]
    public class MenuActivityTabs : Activity {

        private Button orderButton;
        private Button cartButton;
        private Button aboutButton;
        private Button mapButton;
        private Button takePictureButton;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.mainMenu);

            FindViews();

            HandleEvents();
        }

        private void FindViews() {
            orderButton = FindViewById<Button>(Resource.Id.orderButton);
            cartButton = FindViewById<Button>(Resource.Id.cartButton);
            aboutButton = FindViewById<Button>(Resource.Id.aboutButton);
            mapButton = FindViewById<Button>(Resource.Id.mapButton);
            takePictureButton = FindViewById<Button>(Resource.Id.takePictureButton);
        }

        private void HandleEvents() {
            orderButton.Click += OrderButton_Click;
            aboutButton.Click += AboutButton_Click;
        }

        private void AboutButton_Click(object sender, EventArgs e) {

            var intent = new Intent(this, typeof(AboutActivity));

            StartActivity(intent);
        }

        private void OrderButton_Click(object sender, EventArgs e) {

            //------------------------------------------------------------
            // This takes to the view in which all the items are presented 
            // in a single list 
            //var intent = new Intent(this, typeof(ItemMenuActivity));
            //------------------------------------------------------------

            // This takes to teh view where the items are presented in Tabs
            var intent = new Intent(this, typeof(ItemMenuTabsActivity));

            StartActivity(intent);
        }
    }
}