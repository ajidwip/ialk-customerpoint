using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace CustomerPoint
{
    [Activity(Label = "ubahpin", Theme = "@style/MyTheme")]
    public class ubahpin : AppCompatActivity
    {
        public SQLiteDatabase catalogdb;
        public sqliteHelper dbInstance;
        ISharedPreferences sharedPreferences;
        TextView  texttoolbar;
        EditText newpin, oldpin, confirmnewpin;
        Button btnkirim;
        ImageView back;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ubahpin);
            sharedPreferences = this.GetSharedPreferences("sharedprefrences", 0);

            dbInstance = CoreApplication.getInstance().getDatabase();
            catalogdb = dbInstance.WritableDatabase;
            View parentLayout = FindViewById(Android.Resource.Id.Content);
            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            texttoolbar = toolbar.FindViewById<TextView>(Resource.Id.texttoolbar);
            texttoolbar.Text = "Ubah PIN";
            back = toolbar.FindViewById<ImageView>(Resource.Id.back);
            back.Click += delegate
            {
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
                Finish();

            };
            newpin = FindViewById<EditText>(Resource.Id.newpin);
            oldpin = FindViewById<EditText>(Resource.Id.oldpin);
            confirmnewpin = FindViewById<EditText>(Resource.Id.confirmnewpin);

            btnkirim= FindViewById<Button>(Resource.Id.btnkirim);

            btnkirim.Click += delegate
            {
                if(confirmnewpin.Text.ToString() == newpin.Text.ToString())
                {
                    try
                    {
                        WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                        MyClient.ubahpin(oldpin.Text.ToString(), confirmnewpin.Text.ToString(), sharedPreferences.GetString("CustomerKey", null));

                        Intent i = new Intent(this, typeof(MainActivity));
                        StartActivity(i);
                        Finish();
                    }catch(Exception ex)
                    {
                        Snackbar snackbar = Snackbar.Make(parentLayout, "Error Connection", Snackbar.LengthLong);
                        snackbar.Show();
                    }
                }
                else
                {
                    confirmnewpin.Error = "pin tidak sesuai";
                }
            };
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