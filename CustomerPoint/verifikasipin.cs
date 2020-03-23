using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace CustomerPoint
{
    [Activity(Label = "verifikasipin", Theme = "@style/MyTheme")]
    public class verifikasipin : AppCompatActivity
    {
        ISharedPreferences sharedPreferences;
        EditText edt1, edt2, edt3, edt4, edt5, edt6;
        Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btnbackspace;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.customerpinverifikasi);
            sharedPreferences = this.GetSharedPreferences("sharedprefrences", 0);
            View parentLayout = FindViewById(Android.Resource.Id.Content);

            edt1 = FindViewById<EditText>(Resource.Id.edt1);
            edt2 = FindViewById<EditText>(Resource.Id.edt2);
            edt3 = FindViewById<EditText>(Resource.Id.edt3);
            edt4 = FindViewById<EditText>(Resource.Id.edt4);
            edt5 = FindViewById<EditText>(Resource.Id.edt5);
            edt6 = FindViewById<EditText>(Resource.Id.edt6);

            btn0 = FindViewById<Button>(Resource.Id.btn0);
            btn1 = FindViewById<Button>(Resource.Id.btn1);
            btn2 = FindViewById<Button>(Resource.Id.btn2);
            btn3 = FindViewById<Button>(Resource.Id.btn3);
            btn4 = FindViewById<Button>(Resource.Id.btn4);
            btn5 = FindViewById<Button>(Resource.Id.btn5);
            btn6 = FindViewById<Button>(Resource.Id.btn6);
            btn7 = FindViewById<Button>(Resource.Id.btn7);
            btn8 = FindViewById<Button>(Resource.Id.btn8);
            btn9 = FindViewById<Button>(Resource.Id.btn9);
            btnbackspace = FindViewById<Button>(Resource.Id.btnbackspace);
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
            edt6.AfterTextChanged += delegate
            {
                try
                {
                    if (edt1.Text != "" && edt2.Text != "" && edt3.Text != "" && edt4.Text != "" && edt5.Text != "" && edt6.Text != "")
                    {
                        string pin = edt1.Text.ToString() + edt2.Text.ToString() + edt3.Text.ToString() + edt4.Text.ToString() + edt5.Text.ToString() + edt6.Text.ToString();

                        WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                        if (Intent.GetStringExtra("pin") == pin)
                        {
                            MyClient.setuppin(sharedPreferences.GetString("CustomerKey", null), pin);
                            Intent i = new Intent(this, typeof(MainActivity));
                            StartActivity(i);
                            Finish();
                        }
                        else
                        {
                            edt1.Text = "";
                            edt2.Text = "";
                            edt3.Text = "";
                            edt4.Text = "";
                            edt5.Text = "";
                            edt6.Text = "";
                            Toast.MakeText(this, "Pin tidak sesuai", ToastLength.Short).Show();
                        }
                    }
                }catch(Exception ex)
                {
                    Snackbar snackbar = Snackbar.Make(parentLayout, "Error Connection", Snackbar.LengthLong);
                    snackbar.Show();
                }
            };

            btnbackspace.Click += delegate
            {
                if (edt6.Text != "")
                {

                    edt6.Text = "";
                    edt6.RequestFocus();
                }
                else if (edt5.Text != "")
                {

                    edt5.Text = "";
                    edt5.RequestFocus();
                }
                else if (edt4.Text != "")
                {

                    edt4.Text = "";
                    edt4.RequestFocus();
                }
                else if (edt3.Text != "")
                {

                    edt3.Text = "";
                    edt3.RequestFocus();
                }
                else if (edt2.Text != "")
                {

                    edt2.Text = "";
                    edt2.RequestFocus();
                }
                else if (edt1.Text != "")
                {

                    edt1.Text = "";
                    edt1.RequestFocus();
                }
            };

            btn0.Click += delegate
            {
                if (edt1.Text == "")
                {
                    edt1.Text = btn0.Text;
                }
                else if (edt2.Text == "")
                {
                    edt2.Text = btn0.Text;
                }
                else if (edt3.Text == "")
                {
                    edt3.Text = btn0.Text;
                }
                else if (edt4.Text == "")
                {
                    edt4.Text = btn0.Text;
                }
                else if (edt5.Text == "")
                {
                    edt5.Text = btn0.Text;
                }
                else if (edt6.Text == "")
                {
                    edt6.Text = btn0.Text;
                }
            };
            btn1.Click += delegate
            {
                if (edt1.Text == "")
                {
                    edt1.Text = btn1.Text;
                }
                else if (edt2.Text == "")
                {
                    edt2.Text = btn1.Text;
                }
                else if (edt3.Text == "")
                {
                    edt3.Text = btn1.Text;
                }
                else if (edt4.Text == "")
                {
                    edt4.Text = btn1.Text;
                }
                else if (edt5.Text == "")
                {
                    edt5.Text = btn1.Text;
                }
                else if (edt6.Text == "")
                {
                    edt6.Text = btn1.Text;
                }
            };
            btn2.Click += delegate
            {
                if (edt1.Text == "")
                {
                    edt1.Text = btn2.Text;
                }
                else if (edt2.Text == "")
                {
                    edt2.Text = btn2.Text;
                }
                else if (edt3.Text == "")
                {
                    edt3.Text = btn2.Text;
                }
                else if (edt4.Text == "")
                {
                    edt4.Text = btn2.Text;
                }
                else if (edt5.Text == "")
                {
                    edt5.Text = btn2.Text;
                }
                else if (edt6.Text == "")
                {
                    edt6.Text = btn2.Text;
                }
            };
            btn3.Click += delegate
            {
                if (edt1.Text == "")
                {
                    edt1.Text = btn3.Text;
                }
                else if (edt2.Text == "")
                {
                    edt2.Text = btn3.Text;
                }
                else if (edt3.Text == "")
                {
                    edt3.Text = btn3.Text;
                }
                else if (edt4.Text == "")
                {
                    edt4.Text = btn3.Text;
                }
                else if (edt5.Text == "")
                {
                    edt5.Text = btn3.Text;
                }
                else if (edt6.Text == "")
                {
                    edt6.Text = btn3.Text;
                }
            };

            btn4.Click += delegate
            {
                if (edt1.Text == "")
                {
                    edt1.Text = btn4.Text;
                }
                else if (edt2.Text == "")
                {
                    edt2.Text = btn4.Text;
                }
                else if (edt3.Text == "")
                {
                    edt3.Text = btn4.Text;
                }
                else if (edt4.Text == "")
                {
                    edt4.Text = btn4.Text;
                }
                else if (edt5.Text == "")
                {
                    edt5.Text = btn4.Text;
                }
                else if (edt6.Text == "")
                {
                    edt6.Text = btn4.Text;
                }
            };

            btn5.Click += delegate
            {
                if (edt1.Text == "")
                {
                    edt1.Text = btn5.Text;
                }
                else if (edt2.Text == "")
                {
                    edt2.Text = btn5.Text;
                }
                else if (edt3.Text == "")
                {
                    edt3.Text = btn5.Text;
                }
                else if (edt4.Text == "")
                {
                    edt4.Text = btn5.Text;
                }
                else if (edt5.Text == "")
                {
                    edt5.Text = btn5.Text;
                }
                else if (edt6.Text == "")
                {
                    edt6.Text = btn5.Text;
                }
            };
            btn6.Click += delegate
            {
                if (edt1.Text == "")
                {
                    edt1.Text = btn6.Text;
                }
                else if (edt2.Text == "")
                {
                    edt2.Text = btn6.Text;
                }
                else if (edt3.Text == "")
                {
                    edt3.Text = btn6.Text;
                }
                else if (edt4.Text == "")
                {
                    edt4.Text = btn6.Text;
                }
                else if (edt5.Text == "")
                {
                    edt5.Text = btn6.Text;
                }
                else if (edt6.Text == "")
                {
                    edt6.Text = btn6.Text;
                }
            };
            btn7.Click += delegate
            {
                if (edt1.Text == "")
                {
                    edt1.Text = btn7.Text;
                }
                else if (edt2.Text == "")
                {
                    edt2.Text = btn7.Text;
                }
                else if (edt3.Text == "")
                {
                    edt3.Text = btn7.Text;
                }
                else if (edt4.Text == "")
                {
                    edt4.Text = btn7.Text;
                }
                else if (edt5.Text == "")
                {
                    edt5.Text = btn7.Text;
                }
                else if (edt6.Text == "")
                {
                    edt6.Text = btn7.Text;
                }
            };
            btn8.Click += delegate
            {
                if (edt1.Text == "")
                {
                    edt1.Text = btn8.Text;
                }
                else if (edt2.Text == "")
                {
                    edt2.Text = btn8.Text;
                }
                else if (edt3.Text == "")
                {
                    edt3.Text = btn8.Text;
                }
                else if (edt4.Text == "")
                {
                    edt4.Text = btn8.Text;
                }
                else if (edt5.Text == "")
                {
                    edt5.Text = btn8.Text;
                }
                else if (edt6.Text == "")
                {
                    edt6.Text = btn8.Text;
                }
            };
            btn9.Click += delegate
            {
                if (edt1.Text == "")
                {
                    edt1.Text = btn9.Text;
                }
                else if (edt2.Text == "")
                {
                    edt2.Text = btn9.Text;
                }
                else if (edt3.Text == "")
                {
                    edt3.Text = btn9.Text;
                }
                else if (edt4.Text == "")
                {
                    edt4.Text = btn9.Text;
                }
                else if (edt5.Text == "")
                {
                    edt5.Text = btn9.Text;
                }
                else if (edt6.Text == "")
                {
                    edt6.Text = btn9.Text;
                }
            };
            // Create your application here
        }
    }
}