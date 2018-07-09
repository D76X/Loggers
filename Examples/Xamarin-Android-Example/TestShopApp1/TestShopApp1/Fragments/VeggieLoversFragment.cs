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

    public class VeggieLoversFragment : BaseLoversFragment {

        public VeggieLoversFragment() {
            
            // group 2 = veggie!
            itemsGroup = 2;
        }
    }
}