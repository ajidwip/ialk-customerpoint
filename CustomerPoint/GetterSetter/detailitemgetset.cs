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

namespace CustomerPoint.GetterSetter
{
    class detailitemgetset
    {
        public string item;
        public detailitemgetset()
        {

        }

        public detailitemgetset(string pitem)
        {
            this.item = pitem;
        

        }

        public string getitem() { return item; }
        public void Setitem(string item) { this.item = item; }
    }
}