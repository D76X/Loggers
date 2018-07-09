using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using TestApp1.Core;
using TestShopApp1.Adapters;

namespace TestShopApp1.Fragments {

    public class BaseLoversFragment : ListFragment {

        int selectedItemId;
        protected HotDogDataService hotDogDataService;
        protected List<HotDog> hotDogs;
        protected int itemsGroup = 0;

        public BaseLoversFragment() {
            hotDogDataService = new HotDogDataService();
        }

        public override void OnActivityCreated(Bundle savedInstanceState) {

            base.OnActivityCreated(savedInstanceState);

            hotDogs = hotDogDataService.GetHotDogsForGroup(itemsGroup);

            //--------------------------------------------------------------------------------------------------------
            // Basic ListView Adapter pattern
            //var names = hotDogs.Select(h => h.Name).ToArray();
            //ListAdapter = new ArrayAdapter<String>(Activity, Android.Resource.Layout.SimpleListItemActivated1, names);
            //--------------------------------------------------------------------------------------------------------

            // use a custom list adapter
            ListAdapter = new ItemListAdapter(this.Activity, hotDogs);

            if (savedInstanceState != null) {
                selectedItemId = savedInstanceState.GetInt("selectedItemId", 0);
            }
        }

        public override void OnSaveInstanceState(Bundle outState) {
            base.OnSaveInstanceState(outState);
            outState.PutInt("selectedItemId", selectedItemId);
        }

        public override void OnListItemClick(
            ListView l, 
            View v, int 
            position, 
            long id) {

            ShowItemDetails(position);
        }

        void ShowItemDetails(int itemId) {

            //var intent = new Intent(Activity, typeof(PlayQuoteActivity));
            //intent.PutExtra("current_play_id", playId);
            //StartActivity(intent);
        }
    }
}



//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Util;
//using Android.Views;
//using Android.Widget;
//using TestApp1.Core;

//namespace TestShopApp1.Fragments {

//    public class BaseFragment : Fragment {

//        protected ListView listView;
//        protected HotDogDataService hotDogDataService;
//        protected List<HotDog> hotDogs;

//        public BaseFragment() {
//            // Being explicit about the requirement for a default constructor.
//            hotDogDataService = new HotDogDataService();
//        }

//        public override View OnCreateView(
//            LayoutInflater inflater, 
//            ViewGroup container, 
//            Bundle savedInstanceState) {

//            if (container == null) {
//                // returning null would cause an exception!
//                // return null;
//            }

//            return inflater.Inflate(Resource.Layout.meatLoversFragment, container, false); ;
//        }

//        protected void HandleEvents() {
//            listView.ItemClick += ListView_ItemClick;
//        }
//        protected void FindViews() {
//            listView = this.View.FindViewById<ListView>(Resource.Id.hotDogListView);
//        }

//        protected void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e) {
//            var hotDog = hotDogs[e.Position];

//            var intent = new Intent();
//            intent.SetClass(this.Activity, typeof(ItemDetailsActivity));
//            intent.PutExtra("selectedHotDogId", hotDog.HotDogId);

//            StartActivityForResult(intent, 100);
//        }

// ------------------------------------------------------------------------
// Auto generated code not required in this Fragment
//public override void OnCreate(Bundle savedInstanceState) {

//    base.OnCreate(savedInstanceState);

//    // Create your fragment here
//}

//public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
//    // Use this to return your custom view for this Fragment
//    // return inflater.Inflate(Resource.Layout.YourFragment, container, false);

//    return base.OnCreateView(inflater, container, savedInstanceState);
//}
// ------------------------------------------------------------------------
//}
//}