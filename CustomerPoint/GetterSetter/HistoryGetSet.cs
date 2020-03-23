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
    class HistoryGetSet
    {
        public string keterangan;
        public string invoice;
        public string point;
        public string Marker;
        public string tanggal;
        public HistoryGetSet()
        {

        }

        public HistoryGetSet(string pket,string pinv,string pPoint,string pmarker,string ptanggal)
        {
            this.keterangan = pket;
            this.invoice = pinv;
            this.point = pPoint;
            this.Marker = pmarker;
            this.tanggal = ptanggal;
        }

        public string getketerangan() { return keterangan; }
        public void Setketerangan(string keterangan) { this.keterangan = keterangan; }

        public string getinvoice() { return invoice; }
        public void Setinvoice(string invoice) { this.invoice = invoice; }

        public string getpoint() { return point; }
        public void Setpoint(string point) { this.point = point; }
        public string getMarker() { return Marker; }
        public void SetMarker(string Marker) { this.Marker = Marker; }

        public string gettanggal() { return tanggal; }
        public void Settanggal(string tanggal) { this.tanggal = tanggal; }
    }
}