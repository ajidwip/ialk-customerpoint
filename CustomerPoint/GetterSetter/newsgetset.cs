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
    class newsgetset
    {
        public byte[] image;
        public string desc;
        public string url;
        public newsgetset()
        {

        }
        public newsgetset(byte[] pimage, string pdesc,string purl)
        {

            this.image = pimage;
            this.desc = pdesc;
            this.url = purl;
        }

        public byte[] getimage() { return image; }
        public void Setimage(byte[] image) { this.image = image; }

        public string getdesc() { return desc; }
        public void Setdesc(string desc) { this.desc = desc; }

        public string geturl() { return url; }
        public void Seturl(string url) { this.url = url; }
    }
}