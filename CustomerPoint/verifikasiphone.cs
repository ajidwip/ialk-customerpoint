using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace CustomerPoint
{
    [Activity(Label = "verifikasi", Theme = "@style/MyTheme")]
    public class verifikasiphone : AppCompatActivity
    {
        public TextView texttoolbar;
        public EditText nohp;
        public Button btnsend;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.verificationphone);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            texttoolbar = toolbar.FindViewById<TextView>(Resource.Id.texttoolbar);
            texttoolbar.Text = "Ubah PIN";
            btnsend = FindViewById<Button>(Resource.Id.btnsend);
            nohp = FindViewById<EditText>(Resource.Id.nohp);

            btnsend.Click +=delegate{
                try
                {
                    WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                    int cek = MyClient.ceknohp(nohp.Text.ToString()).ceknohpavail;
                    if (cek == 1)
                    {
                       // WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                        MyClient.createotp(nohp.Text.ToString());

                        var request = (HttpWebRequest)WebRequest.Create("http://192.168.8.4/execsmsconsole/");
                        request.AllowAutoRedirect = false;
                        var response = request.GetResponse();

                        Toast.MakeText(this, "sent", ToastLength.Short).Show();

                        Intent i = new Intent(this, typeof(lupapin));
                        i.PutExtra("nohp", nohp.Text.ToString());
                        StartActivity(i);
                        Finish();
                    }
                    else
                    {
                        Toast.MakeText(this, "No HP tidak terdaftar", ToastLength.Short).Show();
                    }
                }
                catch(Exception ex)
                {
                    Toast.MakeText(this, "Server Error,coba beberapa saat lagi", ToastLength.Short).Show();
                }
            };
            // Create your application here
        }
    }
}