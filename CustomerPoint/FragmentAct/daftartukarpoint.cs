using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
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
    public class daftartukarpoint : Fragment
    {
        static Activity context;
        public static ProgressDialog progressDialog;
        static List<daftartukargetset> recyclelist = new List<daftartukargetset>();
        static daftartukaradapter mAdapter;
        static RecyclerView mRecyclerView;
        static ISharedPreferences sharedPreferences;
        static View itemView;
        TextView texttoolbar;
        ImageView back;
        Bundle bundle = new Bundle();
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            itemView = LayoutInflater.From(container.Context).
                    Inflate(Resource.Layout.daftartukar, container, false);
            mRecyclerView = itemView.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            sharedPreferences = Activity.GetSharedPreferences("sharedprefrences", 0);
            context = Activity;
            RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(this.Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new daftartukaradapter(recyclelist);
            mAdapter.ItemClick += OnItemClick;
            Android.Support.V7.Widget.Toolbar toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            texttoolbar = toolbar.FindViewById<TextView>(Resource.Id.texttoolbar);
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
                texttoolbar.Text = "Daftar Transaksi";
            MainActivity.countback = 0;
            new LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
            return itemView;
        }
        void OnItemClick(object sender, int position)
        {
            if (!MainActivity.redeemdetail.IsAdded)
            {
                ISharedPreferencesEditor editor = sharedPreferences.Edit();
                editor.PutString("RedeemNumber", recyclelist[position].getredeemnumber());
                editor.PutString("Tanggal", recyclelist[position].gettanggal());
                editor.PutString("status", recyclelist[position].getstatus());
                editor.Commit();

                FragmentManager fragmentManager2 = this.FragmentManager;
                FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                fragmenttransaction.SetCustomAnimations(Resource.Drawable.sideinbottom, Resource.Drawable.sideinbottom);
                fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.redeemdetail);
                fragmenttransaction.Hide(MainActivity.home);
                fragmenttransaction.Hide(MainActivity.profile);
                fragmenttransaction.Hide(MainActivity.historypoint);
                fragmenttransaction.Hide(MainActivity.faq);
                fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                fragmenttransaction.Hide(MainActivity.katalog);
                fragmenttransaction.Hide(MainActivity.kataloddetail);
                fragmenttransaction.Hide(MainActivity.redeem);
                fragmenttransaction.Hide(MainActivity.inbox);
                fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.redeemdetail).Commit();
               
            }
            else
            {
                ISharedPreferencesEditor editor = sharedPreferences.Edit();
                editor.PutString("RedeemNumber", recyclelist[position].getredeemnumber());
                editor.PutString("Tanggal", recyclelist[position].gettanggal());
                editor.PutString("status", recyclelist[position].getstatus());
                editor.Commit();

                FragmentManager fragmentManager2 = this.FragmentManager;
                FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                fragmenttransaction.Hide(MainActivity.home);
                fragmenttransaction.Hide(MainActivity.profile);
                fragmenttransaction.Hide(MainActivity.historypoint);
                fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                fragmenttransaction.Hide(MainActivity.faq);
                fragmenttransaction.Hide(MainActivity.katalog);
                fragmenttransaction.Hide(MainActivity.redeem);
                fragmenttransaction.Hide(MainActivity.kataloddetail);
                fragmenttransaction.Hide(MainActivity.inbox);
                fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.redeemdetail).Commit();
                new redeemdetailhis.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
            }
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
                    WebReference1.redeemhis emp = new WebReference1.redeemhis();
                    emp = MyClient.GetRedeemhis(sharedPreferences.GetString("CustomerKey", null));

                    dt = emp.redeemhistable;


                    recyclelist.Clear();
                    if (dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            recyclelist.Add(new daftartukargetset(dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][2].ToString(), dt.Rows[i][3].ToString()));
                        }

                    }
                }
                catch (Exception ex)
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