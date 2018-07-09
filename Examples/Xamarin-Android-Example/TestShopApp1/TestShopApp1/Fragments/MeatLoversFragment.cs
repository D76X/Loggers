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

    public class MeatLoversFragment : BaseLoversFragment {

        public MeatLoversFragment() {
            // group 1 = meat!
            itemsGroup = 1;
        }              

    }
}