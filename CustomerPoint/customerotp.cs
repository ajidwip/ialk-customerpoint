using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.Util.Concurrent;

namespace CustomerPoint
{
    [Activity(Label = "Customer Point", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class customerotp : AppCompatActivity
    {
        ISharedPreferences sharedPreferences;
        EditText edt1, edt2, edt3, edt4, edt5, edt6;
        public SQLiteDatabase catalogdb;
        public sqliteHelper dbInstance;
        string customerkey = "";
        Button btnkirimiulang;
        TextView countdown;
        private System.Timers.Timer _timer;
        private int _countSeconds;
        public long minuteinmilliscond;
        public long secondinmilliscond;
        View parentLayout;
        string NoHp, NamaPemilik, NoKTP, KodePos, Alamat, npwp, flag, jenisusaha;

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _timer.Stop();
            minuteinmilliscond = 0;
            secondinmilliscond = 0;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.customerotp);

             parentLayout = FindViewById(Android.Resource.Id.Content);
            sharedPreferences = this.GetSharedPreferences("sharedprefrences", 0);
            dbInstance = CoreApplication.getInstance().getDatabase();
            catalogdb = dbInstance.WritableDatabase;

            customerkey = sharedPreferences.GetString("CustomerKey", null);

            edt1 = FindViewById<EditText>(Resource.Id.edt1);
            edt2 = FindViewById<EditText>(Resource.Id.edt2);
            edt3 = FindViewById<EditText>(Resource.Id.edt3);
            edt4 = FindViewById<EditText>(Resource.Id.edt4);
            edt5 = FindViewById<EditText>(Resource.Id.edt5);
            edt6 = FindViewById<EditText>(Resource.Id.edt6);
            btnkirimiulang = FindViewById<Button>(Resource.Id.btnkirimiulang);
            countdown = FindViewById<TextView>(Resource.Id.countdown);

            NoHp = Intent.GetStringExtra("NoHp");
            NamaPemilik = Intent.GetStringExtra("NamaPemilik");
            NoKTP = Intent.GetStringExtra("NoKTP");
            KodePos = Intent.GetStringExtra("kodepos");
            Alamat = Intent.GetStringExtra("Alamat");
            npwp = Intent.GetStringExtra("npwp");
            flag = Intent.GetStringExtra("flag");
            jenisusaha = Intent.GetStringExtra("jenisusaha");

            countdowntime();


            btnkirimiulang.Click += delegate
            {
                try
                {
                    _timer.Stop();

                    WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                    MyClient.setotpexpired(NoHp);
                    MyClient.Registercustomer(NamaPemilik, NoHp, NoKTP, KodePos, Alamat, npwp, "", customerkey, flag, jenisusaha);


                    //var request = (HttpWebRequest)WebRequest.Create("http://146.196.98.139/execsmsconsole/");
                    //request.AllowAutoRedirect = false;
                    //var response = request.GetResponse();

                   
                    countdowntime();

                    Toast.MakeText(this, "sent", ToastLength.Short).Show();
                }
                catch(Exception ex)
                {
                    Snackbar snackbar = Snackbar.Make(parentLayout, "Error Connection", Snackbar.LengthLong);
                    snackbar.Show();
                }

               
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
              
                    if (edt1.Text != "" && edt2.Text != "" && edt3.Text != "" && edt4.Text != "" && edt5.Text != "" && edt6.Text != "")
                    {
                        string otp = edt1.Text.ToString() + edt2.Text.ToString() + edt3.Text.ToString() + edt4.Text.ToString() + edt5.Text.ToString() + edt6.Text.ToString();


                        ProgressDialog progressDialog = ProgressDialog.Show(this, "", "loading...", true);
                        progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);

                        Handler h = new Handler();
                        Action myAction = () =>
                        {
                            try
                            {
                                WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();

                                int emp2 = MyClient.cekotp(customerkey, otp).cekotp1;

                                if (emp2 == 1)
                                {
                                    WebReference1.GetCustomerData emp = new WebReference1.GetCustomerData();
                                    emp = MyClient.GetCustomerData1(customerkey);
                                    DataTable dt = new DataTable();
                                    dt = emp.GetCustomerData1;
                                    catalogdb.ExecSQL("delete from " + sqliteTable.T_MsCustomer + "");
                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        catalogdb.ExecSQL("Insert into " + sqliteTable.T_MsCustomer + "(" + sqliteTable.MasterKey + "," + sqliteTable.CustName + "," + sqliteTable.NoHP + "," + sqliteTable.NoKTP + "," + sqliteTable.Alamat + "," + sqliteTable.NamaPemilik + "," + sqliteTable.NPWP + "," + sqliteTable.Area + "," + sqliteTable.kodepos + "," + sqliteTable.jenisusaha + ") select '" + dt.Rows[i][2] + "','" + dt.Rows[i][15] + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][6] + "','" + dt.Rows[i][8] + "','" + dt.Rows[i][4] + "','" + dt.Rows[i][7] + "','" + dt.Rows[i][16] + "','" + dt.Rows[i][10] + "','" + dt.Rows[i][14] + "'");
                                    }

                                    Intent I = new Intent(this, typeof(CustomerPin));
                                    StartActivity(I);
                                    Finish();
                                }
                                else
                                {
                                    Snackbar snackbar = Snackbar.Make(parentLayout, "OTP yang dimasukkan tidak sesuai", Snackbar.LengthLong);
                                    snackbar.Show();
                                }
                            }catch(Exception ex)
                            {
                                progressDialog.Dismiss();
                                Snackbar snackbar = Snackbar.Make(parentLayout, "Error Connection", Snackbar.LengthLong);
                                snackbar.Show();
                            }
                        };

                        h.PostDelayed(myAction, 1000);
                    
                }
            };
            // Create your application here
        }

        public void countdowntime()
        {
            minuteinmilliscond = 180000;
            secondinmilliscond = 60000;
            _timer = new System.Timers.Timer();
            //Trigger event every second
            _timer.Interval = 1000;
            _timer.Elapsed += OnTimedEvent;
            //count down 5 seconds
           // _countSeconds = 180;

            _timer.Enabled = true;
        }
        private void OnTimedEvent(object sender, System.Timers.ElapsedEventArgs e)
        {
            minuteinmilliscond -= 1000;
            if (minuteinmilliscond == 0)
            {
                if (secondinmilliscond == 1000)
                {
                  
                    secondinmilliscond -= 1000;
                }

                RunOnUiThread(() =>
                {
                    countdown.Text = TimeUnit.Milliseconds.ToMinutes(minuteinmilliscond).ToString() + ":" + TimeUnit.Milliseconds.ToSeconds(secondinmilliscond).ToString();
                });

                _timer.Stop();

                try
                {

                    WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                    MyClient.setotpexpired(NoHp);
                    MyClient.Registercustomer(NamaPemilik, NoHp, NoKTP, KodePos,Alamat,npwp, "", customerkey, flag,jenisusaha);
                    
                   
                    //var request = (HttpWebRequest)WebRequest.Create("http://146.196.98.139/execsmsconsole/");
                    //request.AllowAutoRedirect = false;
                    //var response = request.GetResponse();
                    countdowntime();


                    RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, "sent", ToastLength.Short).Show();

                    });
                   
                }
                catch (Exception ex)
                {
                    RunOnUiThread(() =>
                    {
                        Toast.MakeText(this, "Error Connection", ToastLength.Short).Show();
                    });
                }

               // countdowntime();
            }
            else
            {
                if (secondinmilliscond == 1000)
                {
                    secondinmilliscond = 60000;
                    secondinmilliscond -= 1000;
                    if (minuteinmilliscond != 0)
                    {
                        minuteinmilliscond -= 1000;

                    }
                }
                else
                {
                    secondinmilliscond -= 1000;
                }

                RunOnUiThread(() => {
                    countdown.Text = TimeUnit.Milliseconds.ToMinutes(minuteinmilliscond).ToString() + ":" + TimeUnit.Milliseconds.ToSeconds(secondinmilliscond).ToString();
                });
            }
        }
    }
}