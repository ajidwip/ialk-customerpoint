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
   public class katalogGetSet
    {
        public byte[] gambar1;
        public byte[] gambar2;
        public string ukuran;
        public string desc;
        public string produk;

        public string desc2;
        public katalogGetSet()
        {

        }

        public katalogGetSet(byte[] pgambar1, byte[] pgambar2, string pukuran, string pdesc,string pproduk,string pdesc2)
        {
            this.gambar1 = pgambar1;
            this.gambar2 = pgambar2;
            this.ukuran = pukuran;
            this.desc = pdesc;
            this.produk = pproduk;
            this.desc2 = pdesc2;
        }

        public byte[] getgambar1() { return gambar1; }
        public void Setgambar1(byte[] gambar1) { this.gambar1 = gambar1; }

        public byte[] getgambar2() { return gambar2; }
        public void Setgambar2(byte[] gambar2) { this.gambar2 = gambar2; }

        public string getukuran() { return ukuran; }
        public void Setukuran(string ukuran) { this.ukuran = ukuran; }
        public string getdesc() { return desc; }
        public void Setdesc(string desc) { this.desc = desc; }

        public string getproduk() { return produk; }
        public void Setproduk(string produk) { this.produk = produk; }

        public string getdesc2() { return desc2; }
        public void Setdesc2(string desc2) { this.desc2 = desc2; }
    }
}