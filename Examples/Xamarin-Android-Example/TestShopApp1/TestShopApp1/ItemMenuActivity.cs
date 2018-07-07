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
        }
    }
}