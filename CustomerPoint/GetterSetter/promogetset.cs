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
    class promogetset
    {
        public byte[] imgpromo;
        public string periode;
        public string ketentuan;

        public promogetset()
        {

        }
        public promogetset(byte[] pimgpromo, string pperiode, string pketentuan)
        {
            this.imgpromo = pimgpromo;
            this.periode = pperiode;
            this.ketentuan = pketentuan;

        }
        public byte[] getimgpromo() { return imgpromo; }
        public void Setimgpromo(byte[] imgpromo) { this.imgpromo = imgpromo; }

        public string getperiode() { return periode; }
        public void Setperiode(string periode) { this.periode = periode; }
        public string getketentuan() { return ketentuan; }
        public void Setketentuan(string ketentuan) { this.ketentuan = ketentuan; }
    }
}