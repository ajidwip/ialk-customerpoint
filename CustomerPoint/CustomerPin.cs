using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using CustomerPoint.FragmentAct;

namespace CustomerPoint
{
    [Activity(Label = "CustomerPin",Theme = "@style/MyTheme")]
    public class CustomerPin : AppCompatActivity
    {
        public static ProgressDialog progressDialog;
        ISharedPreferences sharedPreferences;
        EditText edt1, edt2, edt3, edt4, edt5, edt6;
        Button btn0, btn1, btn2, btn3, btn4, btn5, btn6, btn7, btn8, btn9, btnbackspace, lupapin;
        int FlagMenu = 0;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.customerpin);
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
            btn8= FindViewById<Button>(Resource.Id.btn8);
            btn9 = FindViewById<Button>(Resource.Id.btn9);
            btnbackspace = FindViewById<Button>(Resource.Id.btnbackspace);
            lupapin = FindViewById<Button>(Resource.Id.lupapin);
            if (Intent.GetStringExtra("FlagMenu")!=null)
            {
                FlagMenu = int.Parse(Intent.GetStringExtra("FlagMenu"));
            }
            lupapin.Click += delegate
              {
                  Intent i = new Intent(this, typeof(verifikasiphone));
                  StartActivity(i);
                  FinishAffinity();
                 // Finish();
              };
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
                        string cekpin = MyClient.cekpin(sharedPreferences.GetString("CustomerKey", null), pin).cekpin1;
                        if (cekpin == "")
                        {
                            Intent i = new Intent(this, typeof(verifikasipin));
                            i.PutExtra("pin", pin);
                            StartActivity(i);
                            Finish();
                        }
                        else
                        {
                            if (cekpin == pin)
                            {
                                if (FlagMenu == 0)
                                {
                                    progressDialog = ProgressDialog.Show(this, "", "loading...", true);
                                    progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                                    Handler h = new Handler();
                                    Action myAction = () =>
                                    {
                                        progressDialog.Dismiss();
                                        Intent i = new Intent(this, typeof(MainActivity));
                                    StartActivity(i);
                                    Finish();
                                    };
                                    h.PostDelayed(myAction, 1000);
                                }
                                else if (FlagMenu == 1)
                                {
                                    progressDialog = ProgressDialog.Show(this, "", "loading...", true);
                                    progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
                                    Handler h = new Handler();
                                    Action myAction = () =>
                                    {
                                        progressDialog.Dismiss();
                                        Intent i = new Intent(this, typeof(editprofile));
                                        StartActivity(i);
                                        Finish();
                                    };
                                    h.PostDelayed(myAction, 1000);
                                }
                                else if (FlagMenu == 2)
                                {
                                    try
                                    {
                                        progressDialog = ProgressDialog.Show(this, "", "loading...", true);
                                        progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);

                                        Handler h = new Handler();
                                        Action myAction = () =>
                                        {
                                            try
                                            {
                                                string requestid = Intent.GetStringExtra("requestid");
                                                string masterkey = sharedPreferences.GetString("CustomerKey", null);
                                                string item = Intent.GetStringExtra("itemcode");
                                                MyClient.redeem(Intent.GetStringExtra("requestid"), "1", sharedPreferences.GetString("CustomerKey", null), Intent.GetStringExtra("itemcode"), "1");

                                                RedeemItem.flagtukar = 1;
                                                progressDialog.Dismiss();
                                                Finish();
                                            }
                                           catch(Exception ex)
                                            {
                                               
                                                Toast.MakeText(this, "Tidak dapat meproses", ToastLength.Short).Show();
                                                progressDialog.Dismiss();
                                            }
                                        };

                                        h.PostDelayed(myAction, 1000);
                                    }
                                    catch(Exception ex)
                                    {
                                        progressDialog.Dismiss();
                                        Toast.MakeText(this, "Tidak dapat meproses", ToastLength.Short).Show();

                                        //Snackbar snackbar = Snackbar.Make(parentLayout, "Tidak dapat memproses", Snackbar.LengthLong);
                                        //snackbar.Show();
                                    }
                                   
                                }
                            }
                            else
                            {
                                Toast.MakeText(this, "Harap periksa PIN anda!", ToastLength.Short).Show();
                            }
                        }
                    }
                }
                catch(Exception ex)
                {
                    progressDialog.Dismiss();
                    Toast.MakeText(this, "Error Connection", ToastLength.Short).Show();
                }
            };

            btnbackspace.Click += delegate
              {
                  if(edt6.Text!="")
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