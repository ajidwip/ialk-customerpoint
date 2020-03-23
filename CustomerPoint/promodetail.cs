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
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CustomerPoint.Adapter;
using CustomerPoint.GetterSetter;

namespace CustomerPoint
{
    [Activity(Label = "promodetail", Theme = "@style/MyTheme")]
    public class promodetail : AppCompatActivity
    {
        static public SQLiteDatabase catalogdb;
        public sqliteHelper dbInstance;
        static promoadapter mAdapter;
        static RecyclerView mRecyclerView;
        public static ProgressDialog progressDialog;
        static View parentLayout;
        static Context context;
        TextView texttoolbar;
        static List<promogetset> recyclelist = new List<promogetset>();
        ImageView back;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.promodetail);
            parentLayout = FindViewById(Android.Resource.Id.Content);
            context = this;
            dbInstance = CoreApplication.getInstance().getDatabase();
            catalogdb = dbInstance.WritableDatabase;

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            texttoolbar = toolbar.FindViewById<TextView>(Resource.Id.texttoolbar);
            texttoolbar.Text = "Promo";

            back = toolbar.FindViewById<ImageView>(Resource.Id.back);
            back.Click += delegate
            {
                Intent i = new Intent(this, typeof(MainActivity));
                StartActivity(i);
                Finish();

            };

            mRecyclerView = FindViewById<RecyclerView>(Resource.Id.recyclerView);
            RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(this);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new promoadapter(recyclelist);

            new LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
            // Create your application here
        }
        public override void OnBackPressed()
        {
            base.OnBackPressed();
            Intent i = new Intent(this, typeof(MainActivity));
            StartActivity(i);
            Finish();
          
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

                    recyclelist.Clear();
                    ICursor cursor = catalogdb.RawQuery("select " + sqliteTable.GambarPromo + "," + sqliteTable.validfrom + "," + sqliteTable.validto + "," + sqliteTable.SK + " from " + sqliteTable.T_MsPromo + "",null);
                    while (cursor.MoveToNext())
                    {
                        string a = cursor.GetString(3);
                            recyclelist.Add(new promogetset(cursor.GetBlob(0), DateTime.Parse(cursor.GetString(1)).ToString("dd MMM")+" - "+DateTime.Parse(cursor.GetString(2)).ToString("dd MMM yyyy"), cursor.GetString(3)));

                    }
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

                mRecyclerView.SetAdapter(mAdapter);

            }
        }
    }
}