using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace CustomerPoint
{
    [Activity(Label = "editprofile", Theme = "@style/MyTheme")]
    public class editprofile : AppCompatActivity
    {
        Android.App.AlertDialog.Builder _dialogBuilder;
        Android.App.AlertDialog alertdialog;
        public SQLiteDatabase catalogdb;
        public sqliteHelper dbInstance;
        ISharedPreferences sharedPreferences;
        Button btnedit;
        EditText edtnama, edtalamat, edtnoktp, edtnohp, edtnpwp,edtkodepos, edtnamatoko;
        TextView texttoolbar;
        View parentLayout;
        RadioButton rdbadanusaha, rdToko;
        int flag = 0;
        ImageView back;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.editprofile);
            sharedPreferences = this.GetSharedPreferences("sharedprefrences", 0);
            dbInstance = CoreApplication.getInstance().getDatabase();
            catalogdb = dbInstance.WritableDatabase;

            parentLayout = FindViewById(Android.Resource.Id.Content);

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            texttoolbar = toolbar.FindViewById<TextView>(Resource.Id.texttoolbar);
            texttoolbar.Text = "Edit Profil";
            back = toolbar.FindViewById<ImageView>(Resource.Id.back);
            back.Click += delegate
            {
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
                Finish();

            };
            edtnama = FindViewById<EditText>(Resource.Id.edtnama);
            edtalamat = FindViewById<EditText>(Resource.Id.edtalamat);
            edtnoktp = FindViewById<EditText>(Resource.Id.edtnoktp);
            edtnohp = FindViewById<EditText>(Resource.Id.edtnohp);
            edtnpwp = FindViewById<EditText>(Resource.Id.edtNPWP);
            edtkodepos = FindViewById<EditText>(Resource.Id.edtkodepos);
            edtnamatoko = FindViewById<EditText>(Resource.Id.edtnamatoko);
            btnedit = FindViewById<Button>(Resource.Id.btnedit);
            rdbadanusaha = FindViewById<RadioButton>(Resource.Id.rdbadanusaha);
            rdToko = FindViewById<RadioButton>(Resource.Id.rdtoko);

            ICursor cursor = catalogdb.RawQuery("select " + sqliteTable.NoHP + ", " + sqliteTable.NoKTP + ", " + sqliteTable.Alamat + ", " + sqliteTable.NamaPemilik + ", " + sqliteTable.NPWP + ", " + sqliteTable.CustName + ", " + sqliteTable.kodepos + ", " + sqliteTable.jenisusaha + " from " + sqliteTable.T_MsCustomer + " where " + sqliteTable.MasterKey + "='"+sharedPreferences.GetString("CustomerKey", null)+"'", null);
            while (cursor.MoveToNext())
            {
                edtnamatoko.Text = cursor.GetString(5);
                edtnama.Text = cursor.GetString(3);
                edtnpwp.Text = cursor.GetString(4);
                edtalamat.Text = cursor.GetString(2);
                edtnoktp.Text = cursor.GetString(1);
                edtnohp.Text = cursor.GetString(0);
                edtkodepos.Text = cursor.GetString(6);
                if(cursor.GetString(7)=="1")
                {
                    rdbadanusaha.Checked = true;
                    flag = 1;
                }
                else if (cursor.GetString(7) == "2")
                {
                    rdToko.Checked = true;
                    flag = 2;
                }
            }

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

            btnedit.Click += delegate
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
        }
        private void ok(object sender, DialogClickEventArgs e)
        {
            try
            {
                WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                MyClient.Registercustomer(edtnama.Text.ToString(), edtnohp.Text.ToString(), edtnoktp.Text.ToString(), edtkodepos.Text.ToString(), edtalamat.Text.ToString(), edtnpwp.Text.ToString(), "", sharedPreferences.GetString("CustomerKey", null), "1",flag.ToString());

                WebReference1.GetCustomerData emp = new WebReference1.GetCustomerData();
                emp = MyClient.GetCustomerData1(sharedPreferences.GetString("CustomerKey", null));
                DataTable dt = new DataTable();
                dt = emp.GetCustomerData1;

                catalogdb.ExecSQL("delete from " + sqliteTable.T_MsCustomer + "");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    catalogdb.ExecSQL("Insert into " + sqliteTable.T_MsCustomer + "(" + sqliteTable.MasterKey + "," + sqliteTable.CustName + "," + sqliteTable.NoHP + "," + sqliteTable.NoKTP + "," + sqliteTable.Alamat + "," + sqliteTable.NamaPemilik + "," + sqliteTable.NPWP + "," + sqliteTable.Area + "," + sqliteTable.kodepos + "," + sqliteTable.jenisusaha + ") select '" + dt.Rows[i][2] + "','" + dt.Rows[i][15] + "','" + dt.Rows[i][5] + "','" + dt.Rows[i][6] + "','" + dt.Rows[i][8] + "','" + dt.Rows[i][4] + "','" + dt.Rows[i][7] + "','" + dt.Rows[i][16] + "','"+ dt.Rows[i][10] + "','" + dt.Rows[i][14] + "'");
                }

               Toast.MakeText(this, "Profile Berhasil Diubah", ToastLength.Long).Show();

                _dialogBuilder.Dispose();
                alertdialog.Dismiss();

                Intent I = new Intent(this, typeof(MainActivity));
                StartActivity(I);
                Finish();
            }
            catch(Exception ex)
            {
                Snackbar snackbar = Snackbar.Make(parentLayout, "Error Connection", Snackbar.LengthLong);
                snackbar.Show();
            }
           

        }
        private void cancel(object sender, DialogClickEventArgs e)
        {

            _dialogBuilder.Dispose();
            alertdialog.Dismiss();

        }
        public override void OnBackPressed()
        {

            base.OnBackPressed();

            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
            Finish();
        }
    }
}