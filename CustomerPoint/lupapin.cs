using System;
using System.Collections.Generic;
using System.Linq;
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
    [Activity(Label = "lupapin", Theme = "@style/MyTheme")]
  
    public class lupapin : AppCompatActivity
    {
        public TextView texttoolbar;
        EditText edt1, edt2, edt3, edt4, edt5, edt6,pinbaru,verifikasipinbaru;
        Button btnsend;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.resetpin);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            texttoolbar = toolbar.FindViewById<TextView>(Resource.Id.texttoolbar);
            texttoolbar.Text = "Ubah PIN";

            edt1 = FindViewById<EditText>(Resource.Id.edt1);
            edt2 = FindViewById<EditText>(Resource.Id.edt2);
            edt3 = FindViewById<EditText>(Resource.Id.edt3);
            edt4 = FindViewById<EditText>(Resource.Id.edt4);
            edt5 = FindViewById<EditText>(Resource.Id.edt5);
            edt6 = FindViewById<EditText>(Resource.Id.edt6);
            pinbaru = FindViewById<EditText>(Resource.Id.pinbaru);
            verifikasipinbaru = FindViewById<EditText>(Resource.Id.verifikasipinbaru);
            btnsend = FindViewById<Button>(Resource.Id.btnsend);


            edt1.AfterTextChanged += delegate
            {
                if (edt1.Text != "")
                {
                    edt2.RequestFocus();
                }
            };

            edt2.AfterTextChanged += delegate
            {
                if (edt2.Text != "")
                {
                    edt3.RequestFocus();
                }
            };

            edt3.AfterTextChanged += delegate
            {
                if (edt3.Text != "")
                {
                    edt4.RequestFocus();
                }
            };

            edt4.AfterTextChanged += delegate
            {
                if (edt4.Text != "")
                {
                    edt5.RequestFocus();
                }
            };

            edt5.AfterTextChanged += delegate
            {
                if (edt5.Text != "")
                {
                    edt6.RequestFocus();
                }
            };

            btnsend.Click += delegate
            {
                try
                {
                    if (edt1.Text != "" && edt2.Text != "" && edt3.Text != "" && edt4.Text != "" && edt5.Text != "" && edt6.Text != "" && pinbaru.Text!="" && verifikasipinbaru.Text!="" )
                    {
                        if(pinbaru.Text==verifikasipinbaru.Text)
                        {
                            string otp = edt1.Text.ToString() + edt2.Text.ToString() + edt3.Text.ToString() + edt4.Text.ToString() + edt5.Text.ToString() + edt6.Text.ToString();
                            WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                            string nohp = Intent.GetStringExtra("nohp");
                            MyClient.lupapin(nohp, otp, verifikasipinbaru.Text.ToString());

                            Toast.MakeText(this, "PIN berhasil diubah", ToastLength.Short).Show();

                            Intent i = new Intent(this, typeof(MainActivity));
                            StartActivity(i);
                            Finish();
                        }
                        else
                        {
                            verifikasipinbaru.Error = "Tidak sesuai";
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Mohon melengkapi OTP dan PIN baru", ToastLength.Short).Show();
                    }
                }
                catch(Exception ex)
                {
                    Toast.MakeText(this, "PIN gagal diubah, coba beberapa saat lagi", ToastLength.Short).Show();
                }
            };
            // Create your application here
        }
    }
}