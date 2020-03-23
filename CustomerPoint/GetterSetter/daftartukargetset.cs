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
    class daftartukargetset
    {
        public string status;
        public string tanggal;
        public string redeemnumber;
        public string statuscode;
        public daftartukargetset()
        {

        }

        public daftartukargetset(string pstatus, string ptanggal, string predeemnumber,string pstatuscode)
        {
            this.status = pstatus;
            this.tanggal = ptanggal;
            this.redeemnumber = predeemnumber;
            this.statuscode = pstatuscode;
        }

        public string getstatus() { return status; }
        public void Setstatus(string status) { this.status = status; }

        public string gettanggal() { return tanggal; }
        public void Settanggal(string tanggal) { this.tanggal = tanggal; }

        public string getredeemnumber() { return redeemnumber; }
        public void Setredeemnumber(string redeemnumber) { this.redeemnumber = redeemnumber; }

        public string getstatuscode() { return statuscode; }
        public void Setstatuscode(string statuscode) { this.statuscode = statuscode; }
    }
}