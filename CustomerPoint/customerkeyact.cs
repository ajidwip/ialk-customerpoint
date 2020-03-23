using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.Hardware.Input;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;

namespace CustomerPoint
{
    [Activity(Label = "Customer Point", Theme = "@style/MyTheme", MainLauncher = true)]
    public class customerkeyact : AppCompatActivity
    {
        Android.App.AlertDialog.Builder _dialogBuilder;
        Android.App.AlertDialog alertdialog;
        static public SQLiteDatabase catalogdb;
        public sqliteHelper dbInstance;
        ISharedPreferences sharedPreferences;
        EditText edt1, edt2, edt3, edt4, edt5, edt6;
        DataTable dt = new DataTable();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.customerkey);

            dbInstance = CoreApplication.getInstance().getDatabase();
            catalogdb = dbInstance.WritableDatabase;
            sharedPreferences = this.GetSharedPreferences("sharedprefrences", 0);
            View parentLayout = FindViewById(Android.Resource.Id.Content);
            WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
            if (sharedPreferences.GetString("CustomerKey", null) != null)
            {
                try
                {
                    int cek = MyClient.cekisregister(sharedPreferences.GetString("CustomerKey", null)).cekisregister;

                    if (cek == 1)
                    {
                        Intent ib = new Intent(this, typeof(CustomerPin));
                        StartActivity(ib);
                        Finish();
                    }
                }catch(Exception ex)
                {
                    Snackbar snackbar = Snackbar.Make(parentLayout, "Error Connection", Snackbar.LengthLong);
                    snackbar.Show();
                }
            }
            edt1 = FindViewById<EditText>(Resource.Id.edt1);
            edt2 = FindViewById<EditText>(Resource.Id.edt2);
            edt3 = FindViewById<EditText>(Resource.Id.edt3);
            edt4 = FindViewById<EditText>(Resource.Id.edt4);
            edt5 = FindViewById<EditText>(Resource.Id.edt5);
            edt6 = FindViewById<EditText>(Resource.Id.edt6);
            InputMethodManager inputManager = (InputMethodManager)this
                    .GetSystemService(Activity.InputMethodService);
            edt1.TextChanged += delegate
            {
                if (edt1.Text != "")
                {
                    edt2.RequestFocus();
                }
            };

            edt2.TextChanged += delegate
            {
                if (edt2.Text != "")
                {
                    edt3.RequestFocus();
                }
            };

            edt3.TextChanged += delegate
            {
                if (edt3.Text != "")
                {
                    edt4.RequestFocus();
                }
            };

            edt4.TextChanged += delegate
            {
                if (edt4.Text != "")
                {
                    edt5.RequestFocus();
                }
            };

            edt5.TextChanged += delegate
            {
                if (edt5.Text != "")
                {
                    edt6.RequestFocus();
                }                
            };
            edt6.TextChanged += delegate
            {
                if (edt1.Text != "" && edt2.Text != "" && edt3.Text != "" && edt4.Text != "" && edt5.Text != "" && edt6.Text != "")
                {
                    string customerkey = edt1.Text.ToString() + edt2.Text.ToString() + edt3.Text.ToString() + edt4.Text.ToString() + edt5.Text.ToString() + edt6.Text.ToString();

                    inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, 0);


                    ProgressDialog progressDialog = ProgressDialog.Show(this, "", "loading...", true);
                    progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);

                    Handler h = new Handler();
                    Action myAction = () =>
                    {
                        try
                        {
                            int emp2 = MyClient.cekcustomerkey(customerkey.ToUpper()).cekcustomerkey1;
                            if (emp2 == 1)
                            {
                                int cek = MyClient.cekisregister(customerkey.ToUpper()).cekisregister;
                                if (cek == 1)
                                {
                                    progressDialog.Dismiss();
                                    ISharedPreferencesEditor editor = sharedPreferences.Edit();
                                    editor.PutString("CustomerKey", customerkey.ToUpper());
                                    editor.Commit();

                                    WebReference1.GetCustomerData emp = new WebReference1.GetCustomerData();
                                    emp = MyClient.GetCustomerData1(sharedPreferences.GetString("CustomerKey", null));
                                    DataTable dt = new DataTable();
                                    dt = emp.GetCustomerData1;

                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        catalogdb.ExecSQL("Insert into " + sqliteTable.T_MsCustomer + "(" + sqliteTable.MasterKey + "," + sqliteTable.CustName + "," + sqliteTable.NoHP + "," + sqliteTable.NoKTP + "," + sqliteTable.Alamat + "," + sqliteTable.NamaPemilik + "," + sqliteTable.NPWP + "," + sqliteTable.Area + "," + sqliteTable.kodepos + "," + sqliteTable.jenisusaha + ") select '" + dt.Rows[i][2] + "','" + dt.Rows[i][15] + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][6] + "','" + dt.Rows[i][8] + "','" + dt.Rows[i][4] + "','" + dt.Rows[i][7] + "','" + dt.Rows[i][16] + "','" + dt.Rows[i][10] + "','" + dt.Rows[i][14] + "'");
                                    }

                                    Intent ij = new Intent(this, typeof(CustomerPin));
                                    StartActivity(ij);
                                    Finish();
                                }
                                else
                                {
                                    progressDialog.Dismiss();
                                    ISharedPreferencesEditor editor = sharedPreferences.Edit();
                                    editor.PutString("CustomerKey", customerkey.ToUpper());
                                    editor.Commit();

                                    Intent I = new Intent(this, typeof(Register));
                                    StartActivity(I);
                                    Finish();

                                }
                            }
                            else
                            {
                                progressDialog.Dismiss();
                                Snackbar snackbar = Snackbar.Make(parentLayout, "Customer Key tidak ditemukan", Snackbar.LengthLong);
                                snackbar.Show();

                            }
                        }
                        catch (Exception ex)
                        {
                            progressDialog.Dismiss();
                            Snackbar snackbar = Snackbar.Make(parentLayout, "Error Connection", Snackbar.LengthLong);
                            snackbar.Show();
                        }
                    };

                    h.PostDelayed(myAction, 1000);
                }
            };

            if (sharedPreferences.GetString("persetujuan", null) == null)
            {
                try
                {
                    WebReference1.disclaimer1 emp3 = new WebReference1.disclaimer1();
                    emp3 = MyClient.getdisclaimer();

                    dt = emp3.disclaimertable;
                    using (_dialogBuilder = new Android.App.AlertDialog.Builder(this))
                    {
                        _dialogBuilder.SetTitle("Persetujuan");
                        _dialogBuilder.SetMessage(dt.Rows[0][0].ToString());
                        _dialogBuilder.SetCancelable(false);
                        _dialogBuilder.SetPositiveButton("OK", ok);
                        _dialogBuilder.SetNegativeButton("Cancel", cancel);
                        alertdialog = _dialogBuilder.Create();

                        alertdialog.Show();
                    }
                }catch(Exception ex)
                {
                    Snackbar snackbar = Snackbar.Make(parentLayout, "Error Connection", Snackbar.LengthLong);
                    snackbar.Show();
                }
            }
      
                // Create your application here
            }
        private void ok(object sender, DialogClickEventArgs e)
        {
            ISharedPreferencesEditor editor = sharedPreferences.Edit();
            editor.PutString("persetujuan", "1");
            editor.Commit();

            _dialogBuilder.Dispose();
            alertdialog.Dismiss();
        }
        private void cancel(object sender, DialogClickEventArgs e)
        {
           
            _dialogBuilder.Dispose();
            alertdialog.Dismiss();
            Finish();

        }
    }
    }