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
using TestShopApp1.Adapters;

namespace TestShopApp1 {
    [Activity(Label = "Menu", MainLauncher = false)]
    public class ItemMenuActivity : Activity {

        private ListView itemsListView;
        private List<HotDog> allItems;
        private HotDogDataService dataService;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ItemMenuView);

            itemsListView = FindViewById<ListView>(Resource.Id.menuListView);

            dataService = new HotDogDataService();

            allItems = dataService.GetAllHotDogs();

            itemsListView.Adapter = new ItemListAdapter(this, allItems);

            // this activates the Android feature that displays the scroll
            // thump on the side of the list to let the user scroll fast 
            // through it when there is a long list of data.
            itemsListView.FastScrollEnabled = true;

            itemsListView.ItemClick += itemsListView_ItemClick;
        }

        private void itemsListView_ItemClick(
            object sender, 
            AdapterView.ItemClickEventArgs e) {

            var hotDog = allItems[e.Position];

            var intent = new Intent();
            intent.SetClass(this, typeof(ItemDetailsActivity));
            
            // PutExtra is used to pass values into a dictionary
            // that is accessible to the activity that it is 
            // navigated to
            intent.PutExtra("selectedItemId", hotDog.HotDogId);

            // StartAactivity => used for one-way navigation
            // StartActivityForResult => is a two-way navigation to return to 
            // the calling activity
            // requestCode = 100 identifies the navigation request
            // this activity gets back the same requestCode when the returning 
            // intent is received by it.
            StartActivityForResult(intent, 100);
        }

        /// <summary>
        /// This is called once the item details activity calls the 
        /// SetResult method. The calling activity has the opportunity 
        /// to process the result of the navigation to the details
        /// activity.
        /// </summary>
        /// <param name="requestCode">This is the ID of the originating call!</param>
        /// <param name="resultCode">The outcome of the call to the details activity</param>
        /// <param name="data">any data from the details activity</param>
        /// <param name="data">any data from the details activity</param>
        protected override void OnActivityResult(
            int requestCode, 
            Result resultCode, 
            Intent data) {

            base.OnActivityResult(requestCode, resultCode, data);

            // we originally gave requestCode == 100 so we check for this ID
            // only in this example.
            if (resultCode == Result.Ok && requestCode == 100) {

                var selectedHotDog = dataService.GetHotDogById(data.GetIntExtra("selectedItemId", 0));

                var dialog = new AlertDialog.Builder(this);
                dialog.SetTitle("Confirmation");
                dialog.SetMessage(string.Format("You've added {0} time(s) the {1}", data.GetIntExtra("amount", 0), selectedHotDog.Name));
                dialog.Show();
            }
        }
    }
}