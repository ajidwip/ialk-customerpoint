using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Net;

namespace CustomerPoint
{
    [Activity(Label = "Register", Theme = "@style/MyTheme")]
    public class Register : AppCompatActivity
    {
        public static ProgressDialog progressDialog;
        Android.App.AlertDialog.Builder _dialogBuilder;
        Android.App.AlertDialog alertdialog;
        Button btnRegister;
       static EditText edtnama, edtalamat,edtnoktp, edtnohp, edtnpwp, edtkodepos, edtnamatoko;
        string customerkey = "";
       static ISharedPreferences sharedPreferences;
        RadioButton rdbadanusaha, rdToko;
        int flag = 0;
        static View parentLayout;
        static Context context;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register);
            sharedPreferences = this.GetSharedPreferences("sharedprefrences", 0);
            context = this;
            parentLayout = FindViewById(Android.Resource.Id.Content);
            edtnama = FindViewById<EditText>(Resource.Id.edtnama);
            edtnamatoko = FindViewById<EditText>(Resource.Id.edtnamatoko);
            edtalamat = FindViewById<EditText>(Resource.Id.edtalamat);
            edtnoktp = FindViewById<EditText>(Resource.Id.edtnoktp);
            edtnohp = FindViewById<EditText>(Resource.Id.edtnohp);
            edtnpwp = FindViewById<EditText>(Resource.Id.edtNPWP);
            edtkodepos = FindViewById<EditText>(Resource.Id.edtkodepos);
            btnRegister = FindViewById<Button>(Resource.Id.btnRegister);
            rdbadanusaha = FindViewById<RadioButton>(Resource.Id.rdbadanusaha);
            rdToko = FindViewById<RadioButton>(Resource.Id.rdtoko);
            
            customerkey = sharedPreferences.GetString("CustomerKey", null);

            rdbadanusaha.Click += delegate
            {
                flag = 1;
                rdToko.Checked = false;
                edtnama.SetHint(Resource.String.namapengurus);
                edtalamat.SetHint(Resource.String.alamatbadanusaha);
                edtnamatoko.SetHint(Resource.String.namabadanusaha);
            };
            rdToko.Click += delegate
            {
                flag = 2;
                rdbadanusaha.Checked = false;
                edtnama.SetHint(Resource.String.nama);
                edtalamat.SetHint(Resource.String.alamat);
                edtnamatoko.SetHint(Resource.String.namatoko);
            };

            btnRegister.Click += delegate
            {
    
                using (_dialogBuilder = new Android.App.AlertDialog.Builder(this))
                {
                    _dialogBuilder.SetTitle("Informasi");
                    _dialogBuilder.SetMessage("Apakah anda yakin?");
                    _dialogBuilder.SetCancelable(false);
                    _dialogBuilder.SetPositiveButton("OK", ok);
                    _dialogBuilder.SetNegativeButton("Cancel", cancel);
                    alertdialog = _dialogBuilder.Create();

                    alertdialog.Show();
                }
              
            };
            new LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
            // Create your application here
        }
        private void ok(object sender, DialogClickEventArgs e)
        {
            if (rdbadanusaha.Checked)
            {
                if (edtnpwp.Text != "" && edtnoktp.Text != "" && edtnama.Text!="" && edtnamatoko.Text != "" && edtkodepos.Text != "" && edtnohp.Text != "" && edtalamat.Text != "")
                {
                    WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                    int cek = MyClient.ceknohp(edtnohp.Text.ToString()).ceknohpavail;
                    if (cek == 0)
                    {
                        MyClient.Registercustomer(edtnama.Text.ToString(), edtnohp.Text.ToString(), edtnoktp.Text.ToString(), edtkodepos.Text.ToString(), edtalamat.Text.ToString(), edtnpwp.Text.ToString(), "", customerkey, "0", flag.ToString());


                        //var request = (HttpWebRequest)WebRequest.Create("http://146.196.98.139/execsmsconsole");
                        //request.AllowAutoRedirect = false;
                        //var response = request.GetResponse();
                        //Log.Debug("SMS Log :", ((HttpWebResponse)response).StatusDescription);
                        //response.Close();

                        Intent I = new Intent(this, typeof(customerotp));
                        I.PutExtra("NamaPemilik", edtnama.Text.ToString());
                        I.PutExtra("NamaToko", edtnamatoko.Text.ToString());
                        I.PutExtra("Alamat", edtalamat.Text.ToString());
                        I.PutExtra("NoHp", edtnohp.Text.ToString());
                        I.PutExtra("NoKTP", edtnoktp.Text.ToString());
                        I.PutExtra("kodepos", edtkodepos.Text.ToString());
                        I.PutExtra("npwp", edtnpwp.Text.ToString());
                        I.PutExtra("flag", "0");
                        I.PutExtra("jenisusaha", flag.ToString());
                        StartActivity(I);
                        Finish();
                    }
                    else
                    {
                        edtnohp.Error = "Sudah Terdaftar";
                    }
                }
                else
                {
                    if (edtnpwp.Text == "")
                    {
                        edtnpwp.Error = "Tidak boleh kosong";
                    }
                    if (edtnoktp.Text == "")
                    {
                        edtnoktp.Error = "Tidak boleh kosong";
                    }
                    if (edtnama.Text == "")
                    {
                        edtnama.Error = "Tidak boleh kosong";
                    }
                    if (edtkodepos.Text == "")
                    {
                        edtkodepos.Error = "Tidak boleh kosong";
                    }
                    if (edtnohp.Text == "" )
                    {
                        edtnohp.Error = "Tidak boleh kosong";
                    }
                    if (edtalamat.Text == "")
                    {
                        edtalamat.Error = "Tidak boleh kosong";
                    }
                }
            }
            else if (rdToko.Checked)
            {
                if (edtnoktp.Text != "" && edtnama.Text != "" && edtnamatoko.Text != "" && edtkodepos.Text != "" && edtnohp.Text != "" && edtalamat.Text != "")
                {

                    WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                    int cek = MyClient.ceknohp(edtnohp.Text.ToString()).ceknohpavail;
                    if (cek == 0)
                    {
                    
                        MyClient.Registercustomer(edtnama.Text.ToString(), edtnohp.Text.ToString(), edtnoktp.Text.ToString(), edtkodepos.Text.ToString(), edtalamat.Text.ToString(), edtnpwp.Text.ToString(), "", customerkey, "0", flag.ToString());


                        //var request = (HttpWebRequest)WebRequest.Create("http://146.196.98.139/execsmsconsole/");
                        //request.AllowAutoRedirect = false;
                        //var response = request.GetResponse();
                        //Log.Debug("SMS Log :", ((HttpWebResponse)response).StatusDescription);
                        //response.Close();


                        Intent I = new Intent(this, typeof(customerotp));
                        I.PutExtra("NamaPemilik", edtnama.Text.ToString());
                        I.PutExtra("NamaToko", edtnamatoko.Text.ToString());
                        I.PutExtra("Alamat", edtalamat.Text.ToString());
                        I.PutExtra("NoHp", edtnohp.Text.ToString());
                        I.PutExtra("NoKTP", edtnoktp.Text.ToString());
                        I.PutExtra("kodepos", edtkodepos.Text.ToString());
                        I.PutExtra("npwp", edtnpwp.Text.ToString());
                        I.PutExtra("flag", "0");
                        I.PutExtra("jenisusaha", flag.ToString());
                        StartActivity(I);
                        Finish();
                    }
                    else
                    {
                        edtnohp.Error = "Sudah Terdaftar";
                    }
                }
                else
                {
                    if (edtnoktp.Text == "")
                    {
                        edtnoktp.Error = "Tidak boleh kosong";
                    }
                    if (edtnama.Text == "")
                    {
                        edtnama.Error = "Tidak boleh kosong";
                    }
                    if (edtkodepos.Text == "")
                    {
                        edtkodepos.Error = "Tidak boleh kosong";
                    }
                    if (edtnohp.Text == "")
                    {
                        edtnohp.Error = "Tidak boleh kosong";
                    }
                    if (edtalamat.Text == "")
                    {
                        edtalamat.Error = "Tidak boleh kosong";
                    }
                }
            }
            _dialogBuilder.Dispose();
            alertdialog.Dismiss();
        }
        private void cancel(object sender, DialogClickEventArgs e)
        {

            _dialogBuilder.Dispose();
            alertdialog.Dismiss();

        }

        public class LoadDataForActivity1 : AsyncTask
        {

            DataTable dt = new DataTable();
            public static int flag = 0;
            protected override void OnPreExecute()
            {

                progressDialog = ProgressDialog.Show(context, "", "loading...", true);
                progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);
            }

            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                try
                {
                    WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                    WebReference1.GetCustomerData emp = new WebReference1.GetCustomerData();
                    emp = MyClient.GetCustomerData1(sharedPreferences.GetString("CustomerKey", null));
                    
                    dt = emp.GetCustomerData1;
                }
                catch (Exception ex)
                {
                    Snackbar snackbar = Snackbar.Make(parentLayout, "Error Connection", Snackbar.LengthLong);
                    snackbar.Show();
                }

                return null;
            }
            protected override void OnPostExecute(Java.Lang.Object result)
            {

                progressDialog.Dismiss();
                for(int i=0;i<dt.Rows.Count;i++)
                {
                    edtnamatoko.Text = dt.Rows[i][15].ToString();
                    edtalamat.Text = dt.Rows[i][17].ToString();
                }

            }
        }
    }
}