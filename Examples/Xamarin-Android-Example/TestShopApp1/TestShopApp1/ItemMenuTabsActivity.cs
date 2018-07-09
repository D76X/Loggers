using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using TestApp1.Core;
using TestShopApp1.Adapters;
using TestShopApp1.Fragments;
using ActionBar = Android.Support.V7.App.ActionBar;
using Android.Views;

namespace TestShopApp1 {
    [Activity(Label = "Menu", MainLauncher = false)]
    public class ItemMenuTabsActivity : Activity {

        private ListView itemsListView;
        private List<HotDog> allItems;
        private HotDogDataService dataService;

        protected override void OnCreate(Bundle savedInstanceState) {
            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.ItemMenuTabsView);

            var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetActionBar(toolbar);
            ActionBar.Title = "My Toolbar";                     
        }

        /// <summary>
        /// To inflate the menu bar!
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public override bool OnCreateOptionsMenu(IMenu menu) {
            MenuInflater.Inflate(Resource.Menu.top_menus, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>
        /// When a user taps a menu item, Android calls the 
        /// OnOptionsItemSelected method and passes in the menu 
        /// item that was selected.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool OnOptionsItemSelected(IMenuItem item) {

            // https://docs.microsoft.com/en-us/xamarin/android/platform/fragments/managing-fragments

            //---------------------------------------------------------------
            // In this example, the implementation just displays a toast to 
            // indicate which menu item was tapped.

            //Toast.MakeText(this, "Action selected: " + item.TitleFormatted,
            //    ToastLength.Short).Show();

            //---------------------------------------------------------------
            
            // replace the fragments
            FragmentTransaction fragmentTx = this.FragmentManager.BeginTransaction();
            var fragment = this.FragmentManager.FindFragmentById(Resource.Id.fragmentContainer);
            var title = item.TitleFormatted.ToString().ToLower();
            if ( title == "meat") {
                fragmentTx.Replace(Resource.Id.fragmentContainer, new MeatLoversFragment());
            }
            else if(title == "veggi") {
                fragmentTx.Replace(Resource.Id.fragmentContainer, new VeggieLoversFragment());
            }
            // Commit the transaction.
            fragmentTx.Commit();

            return base.OnOptionsItemSelected(item);
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

                //var dialog = new AlertDialog.Builder(this);
                //dialog.SetTitle("Confirmation");
                //dialog.SetMessage(string.Format("You've added {0} time(s) the {1}", data.GetIntExtra("amount", 0), selectedHotDog.Name));
                //dialog.Show();
            }
        }
    }
}