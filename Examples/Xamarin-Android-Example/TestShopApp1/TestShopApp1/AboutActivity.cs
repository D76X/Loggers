using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace TestShopApp1 {

    [Activity(Label = "AboutActivity", Exported = true)]
    public class AboutActivity : Activity {

        private TextView phoneNumberTextView;

        protected override void OnCreate(Bundle savedInstanceState) {

            base.OnCreate(savedInstanceState);

            // Create your application here
            SetContentView(Resource.Layout.AboutView);

            FindViews();
            HandleEvents();
        }

        private void FindViews() {
            phoneNumberTextView = FindViewById<TextView>(Resource.Id.phoneNumberTextView);
        }

        private void HandleEvents() {
            phoneNumberTextView.Click += PhoneNumberTextView_Click;
        }

        private void PhoneNumberTextView_Click(object sender, EventArgs e) {

            // Permissions In Xamarin.Android
            // https://docs.microsoft.com/en-us/xamarin/android/app-fundamentals/permissions?tabs=vswin

            const string permission = Manifest.Permission.CallPhone;

            if (ContextCompat.CheckSelfPermission(this, permission) == (int)Permission.Granted) {

                // this is an example of Android's implicit intent and activity
                var intent = new Intent(Intent.ActionCall);
                
                // notice that the format of the data must for the ActionCall must be 
                // tel:...
                intent.SetData(Android.Net.Uri.Parse("tel:" + phoneNumberTextView.Text));
                StartActivity(intent);
            }
            else {

                if (ActivityCompat.ShouldShowRequestPermissionRationale(this, permission)) {

                    // Provide an additional rationale to the user if the permission was not granted
                    // and the user would benefit from additional context for the use of the permission.
                    // For example if the user has previously denied the permission.
                    Log.Info("TAG", "Displaying phone call rationale to provide additional context.");

                    var requiredPermissions = new String[] { Manifest.Permission.CallPhone };

                    //Snackbar.Make(
                    //    layout,
                    //    Resource.String.permission_location_rationale,
                    //    Snackbar.LengthIndefinite)
                    //    .SetAction(
                    //    Resource.String.ok,
                    //    new Action<View>(delegate (View obj) {
                    //    ActivityCompat.RequestPermissions(this, requiredPermissions, REQUEST_LOCATION);
                    //    })).Show();
                }
                else {
                    
                    //ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.CallPhone }, REQUEST_PHONE_CALL);
                    ActivityCompat.RequestPermissions(this, new String[] { Manifest.Permission.CallPhone }, 1);
                }
            }            
        }
    }
}