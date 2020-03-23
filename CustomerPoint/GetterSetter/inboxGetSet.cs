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
    public class inboxGetSet
    {
        //public byte[] gambar;
        public string id;
        public string title;
        public string desc;
        public string tanggal;
        public string status;
        public string fulldesc;
        public inboxGetSet()
        {

        }

        public inboxGetSet(/*byte[] pgambar, */string pid, string ptitle, string pdesc, string ptanggal, string pstatus, string pfulldesc)
        {
            //this.gambar = pgambar;
            this.id = pid;
            this.title = ptitle;
            this.desc = pdesc;
            this.tanggal = ptanggal;
            this.status = pstatus;
            this.fulldesc = pfulldesc;
        }
        /*public byte[] getgambar() { return gambar; }
        public void Setgambar(byte[] gambar) { this.gambar = gambar; }*/
        public string getid() { return id; }
        public void Setid(string id) { this.id = id; }
        public string gettitle() { return title; }
        public void Settitle(string title) { this.title = title; }
        public string getdesc() { return desc; }
        public void Setdesc(string desc) { this.desc = desc; }
        public string gettanggal() { return tanggal; }
        public void Settanggal(string tanggal) { this.tanggal = tanggal; }
        public string getstatus() { return status; }
        public void Setstatus(string status) { this.status = status; }
        public string getfulldesc() { return fulldesc; }
        public void Setfulldesc(string fulldesc) { this.fulldesc = fulldesc; }

    }
}