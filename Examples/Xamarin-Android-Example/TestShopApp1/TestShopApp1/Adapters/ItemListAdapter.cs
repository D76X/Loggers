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
using TestShopApp1.Utilities;

namespace TestShopApp1.Adapters {

    public class ItemListAdapter : BaseAdapter<HotDog> {

        List<HotDog> items;
        Activity context;

        public ItemListAdapter(
            Activity context, 
            List<HotDog> items) : base() {

            this.context = context;
            this.items = items;
        }

        public override long GetItemId(int position) {
            return position;
        }

        public override HotDog this[int position] {
            get {
                return items[position];
            }
        }

        public override int Count {
            get {
                return items.Count;
            }
        }

        /// <summary>
        /// 
        /// This method is call each time a row is about to appear on 
        /// the device screen.
        /// 
        /// Android does automatic virtualization on any list view.
        /// 
        /// Rendered list items popped off the list as we scroll down it
        /// are placed in a Graveyard for later reuse. If Android finds 
        /// one such item view in the graveyard the convertView is not 
        /// null and there is no need to create a new view for the item.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="convertView"></param>
        /// <param name="parent"></param>
        /// <returns></returns>
        public override View GetView(
            int position, 
            View convertView, 
            ViewGroup parent) {

            var item = items[position];

            // There exists a number of built-in styles for a list item
            // There are lost of them > 15!

            // Examples

            // alternative Android ListView styles
            //convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.TestListItem, null);

            // this includes an image with each item placed on the right of any text!
            //convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.ActivityListItem, null);

            //---------------------------------------------------------------------------------------------------
            // This is for the SimpleListItem1
            //if (convertView == null) {
            //    // the is no row view in the graveyard!
            //    // Inflate = parse XML to instantiate objects
            //    // Android.Resource.Layout.SimpleListItem1 is the XML built-in resource to inflate
            //    convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);                
            //}

            // This part is style specific
            //// now that you have the Android backed in SimpleListItem1 view hydrated 
            //// search it for the resource id Text1 and assign the name of the item 
            //// to its Text property
            //convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.Name;
            //---------------------------------------------------------------------------------------------------

            //---------------------------------------------------------------------------------------------------
            // This is for the ActivityListItem
            //var imageBitmap = ImageHelper.GetImageBitmapFromUrl("http://gillcleerenpluralsight.blob.core.windows.net/files/" + item.ImagePath + ".jpg");

            //if (convertView == null) {
            //    convertView = context.LayoutInflater.Inflate(Android.Resource.Layout.ActivityListItem, null);
            //}

            //// This part is style specific
            //convertView.FindViewById<TextView>(Android.Resource.Id.Text1).Text = item.Name;
            //convertView.FindViewById<ImageView>(Android.Resource.Id.Icon).SetImageBitmap(imageBitmap);
            //---------------------------------------------------------------------------------------------------

            //---------------------------------------------------------------------------------------------------
            // This is an example of how to use a custom style for the list view.

            var imageBitmap = ImageHelper.GetImageBitmapFromUrl("http://gillcleerenpluralsight.blob.core.windows.net/files/" + item.ImagePath + ".jpg");

            if (convertView == null) {
                convertView = context.LayoutInflater.Inflate(Resource.Layout.itemRowView, null);
            }

            convertView.FindViewById<TextView>(Resource.Id.hotDogNameTextView).Text = item.Name;
            convertView.FindViewById<TextView>(Resource.Id.shortDescriptionTextView).Text = item.ShortDescription;
            convertView.FindViewById<TextView>(Resource.Id.priceTextView).Text = "$ " + item.Price;
            convertView.FindViewById<ImageView>(Resource.Id.hotDogImageView).SetImageBitmap(imageBitmap);

            //---------------------------------------------------------------------------------------------------

            return convertView;
        }

    }
}