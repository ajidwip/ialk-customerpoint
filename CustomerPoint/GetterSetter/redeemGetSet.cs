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
    class redeemGetSet
    {
        public byte[] imgitem;
        public string item;
        public string point;
        public string itemcode;
        public redeemGetSet()
        {

        }

        public redeemGetSet(byte[] pimgitem, string pitem, string ppoint,string pitemcode)
        {
            this.imgitem = pimgitem;
            this.item = pitem;
            this.point = ppoint;
            this.itemcode = pitemcode;
        }

        public byte[] getimgitem() { return imgitem; }
        public void Setketerangan(byte[] imgitem) { this.imgitem = imgitem; }

        public string getitem() { return item; }
        public void Setitem(string item) { this.item = item; }

        public string getpoint() { return point; }
        public void Setpoint(string point) { this.point = point; }

        public string getitemcode() { return itemcode; }
        public void Setitemcode(string itemcode) { this.itemcode = itemcode; }
    }
}