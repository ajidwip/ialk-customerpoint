using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using CustomerPoint.Adapter;
using CustomerPoint.GetterSetter;

namespace CustomerPoint.FragmentAct
{
    public class RedeemItem : Fragment
    {
        Random generator = new Random();
        static public SQLiteDatabase catalogdb;
        public sqliteHelper dbInstance;
        static Activity context;
        public static ProgressDialog progressDialog;
        static List<redeemGetSet> recyclelist = new List<redeemGetSet>();
        static RecyclerView mRecyclerView;
        static redeemitemadapter mAdapter;
        static ISharedPreferences sharedPreferences;
        TextView texttoolbar;
        static View itemView;
        int posisi=0;
        string random1 = "";
        Android.App.AlertDialog.Builder _dialogBuilder;
        Android.App.AlertDialog alertdialog;
        string requestid = "";
        public static int flagtukar=0;
        ImageView back;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            this.RetainInstance = true;
            // Create your fragment here
        }
        public override void  OnResume()
        {
            base.OnResume();
            if (flagtukar == 1)
            {
                flagtukar = 0;
                using (_dialogBuilder = new Android.App.AlertDialog.Builder(Activity))
                {
                    _dialogBuilder.SetTitle("Informasi");
                    _dialogBuilder.SetMessage("Penukaran Point berhasil");
                    _dialogBuilder.SetCancelable(false);
                    _dialogBuilder.SetPositiveButton("Lihat Daftar", lihat);
                    _dialogBuilder.SetNegativeButton("Cancel", cancel);
                    alertdialog = _dialogBuilder.Create();

                    alertdialog.Show();
                }
            }
        }
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
             itemView = LayoutInflater.From(container.Context).
                      Inflate(Resource.Layout.redeemitem, container, false);
            context = Activity;

            

            Android.Support.V7.Widget.Toolbar toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            texttoolbar = toolbar.FindViewById<TextView>(Resource.Id.texttoolbar);
            texttoolbar.Text = "HADIAH";
            back = toolbar.FindViewById<ImageView>(Resource.Id.back);
            back.Click += delegate
            {
                if (MainActivity.home.IsAdded)
                {
                    MainActivity.isloaded = 1;
                    FragmentManager fragmentManager3 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction3 = fragmentManager3.BeginTransaction();
                    fragmenttransaction3.Hide(MainActivity.profile);
                    fragmenttransaction3.Hide(MainActivity.faq);
                    fragmenttransaction3.Hide(MainActivity.redeem);
                    fragmenttransaction3.Hide(MainActivity.historypoint);
                    fragmenttransaction3.Hide(MainActivity.katalog);
                    fragmenttransaction3.Hide(MainActivity.kataloddetail);
                    fragmenttransaction3.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction3.Hide(MainActivity.redeemdetail);
                    fragmenttransaction3.Hide(MainActivity.news);
                    fragmenttransaction3.Hide(MainActivity.inbox);
                    fragmenttransaction3.Hide(MainActivity.currentFragment).Show(MainActivity.home).Commit();
                    new HomeFragment.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);

                }
                else
                {
                    MainActivity.isloaded = 0;
                    FragmentManager fragmentManager2 = this.FragmentManager;

                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.historypoint);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.news);
                    fragmenttransaction.Hide(MainActivity.inbox);
                    fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.home);
                    fragmenttransaction.Show(MainActivity.home).Commit();
                    MainActivity.currentFragment = MainActivity.home;

                }

            };
            sharedPreferences = Activity.GetSharedPreferences("sharedprefrences", 0);

            dbInstance = CoreApplication.getInstance().getDatabase();
            catalogdb = dbInstance.WritableDatabase;

            mRecyclerView = itemView.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            //RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(Activity, LinearLayoutManager.Horizontal, false);
            RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(this.Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new redeemitemadapter(recyclelist);
            mAdapter.ItemClick += OnItemClick;
            new LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
            return itemView;
        }
    
        void OnItemClick(object sender, int position)
        {
           
                posisi = position;

                using (_dialogBuilder = new Android.App.AlertDialog.Builder(Activity))
                {
                    _dialogBuilder.SetTitle("Informasi");
                    _dialogBuilder.SetMessage("Apakah anda yakin tukar poin sebesar "+recyclelist[position].getpoint()+"?");
                    _dialogBuilder.SetCancelable(false);
                    _dialogBuilder.SetPositiveButton("OK", ok);
                    _dialogBuilder.SetNegativeButton("Cancel", cancel);
                    alertdialog = _dialogBuilder.Create();

                    alertdialog.Show();
                }
         
            
          
        }
        private void ok(object sender, DialogClickEventArgs e)
        {
           
                
                random1 = generator.Next(0, 1000000).ToString("D6");
                requestid = sharedPreferences.GetString("CustomerKey", null) + "-" + DateTime.Now.ToString("yyyyMMdd") + "-" + random1;

                Intent i = new Intent(Activity, typeof(CustomerPin));
                i.PutExtra("FlagMenu", "2");
                i.PutExtra("requestid", requestid);
                i.PutExtra("itemcode", recyclelist[posisi].getitemcode());
                StartActivity(i);

              
                _dialogBuilder.Dispose();
                alertdialog.Dismiss();
   
        }
        private void lihat(object sender, DialogClickEventArgs e)
        {
            _dialogBuilder.Dispose();
            alertdialog.Dismiss();

            if (MainActivity.daftartukarpoint.IsAdded)
            {
                FragmentManager fragmentManager2 = this.FragmentManager;
                FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                fragmenttransaction.Hide(MainActivity.home);
                fragmenttransaction.Hide(MainActivity.profile);
                fragmenttransaction.Hide(MainActivity.historypoint);
                fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                fragmenttransaction.Hide(MainActivity.katalog);
                fragmenttransaction.Hide(MainActivity.redeem);
                fragmenttransaction.Hide(MainActivity.redeemdetail);
                fragmenttransaction.Hide(MainActivity.faq);
                fragmenttransaction.Hide(MainActivity.inbox);
                fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.daftartukarpoint).Commit();
                new daftartukarpoint.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
            }
            else
            {
                FragmentManager fragmentManager2 = this.FragmentManager;
                FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.daftartukarpoint);
                fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.daftartukarpoint).Commit();

            }

          

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

                    ICursor cursor = catalogdb.RawQuery("select " + sqliteTable.Area + " from " + sqliteTable.T_MsCustomer + " where " + sqliteTable.MasterKey + "='" + sharedPreferences.GetString("CustomerKey", null) + "'", null);

                    while (cursor.MoveToNext())
                    {
                        WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                        WebReference1.redeemitem emp = new WebReference1.redeemitem();
                        emp = MyClient.GetRedeemitem(cursor.GetString(0));

                        dt = emp.redeemitemtable;


                        recyclelist.Clear();
                        if (dt.Rows.Count > 0)
                        {

                            for (int i = 0; i < dt.Rows.Count; i++)
                            {
                                if(dt.Rows[i][3]==DBNull.Value)
                                {
                                    Drawable drawable = context.Resources.GetDrawable(Resource.Drawable.noimage);
                                    Bitmap bitmap = ((BitmapDrawable)drawable).Bitmap;
                                    MemoryStream stream = new MemoryStream();
                                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                                    byte[] bitMapData = stream.ToArray();
                                    recyclelist.Add(new redeemGetSet(bitMapData, dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][0].ToString()));
                                }
                                else
                                {
                                    recyclelist.Add(new redeemGetSet((byte[])dt.Rows[i][3], dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][0].ToString()));
                                }
                                
                            }

                        }

                    }
                }catch(Exception ex)
                {
                    Snackbar snackbar = Snackbar.Make(itemView, "Error Connection", Snackbar.LengthLong);
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