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
  
    public class redeemdetailhis : Fragment
    {
        static detailitemadapter mAdapter;
        static RecyclerView mRecyclerView;
        static List<detailitemgetset> recyclelist = new List<detailitemgetset>();
        static ISharedPreferences sharedPreferences;
        static Activity context;
        public static ProgressDialog progressDialog;
        static View itemView;
       
        static TextView txtstatus, txttanggal, txtredeemnumber;
        Button btnkembali;
     
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            itemView = LayoutInflater.From(container.Context).
                     Inflate(Resource.Layout.redeemhisdetail, container, false);
            context = Activity;
            sharedPreferences = Activity.GetSharedPreferences("sharedprefrences", 0);
            MainActivity.countback = 0;

            btnkembali = itemView.FindViewById<Button>(Resource.Id.btnkembali);
            txtstatus = itemView.FindViewById<TextView>(Resource.Id.txtstatus);
            txttanggal = itemView.FindViewById<TextView>(Resource.Id.txttanggal);
            txtredeemnumber = itemView.FindViewById<TextView>(Resource.Id.txtredeemnumber);

         

            btnkembali.Click += delegate
            {
             //   Fragment fragment = new daftartukarpoint();

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
            };

            mRecyclerView = itemView.FindViewById<RecyclerView>(Resource.Id.recyclerView);

            RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(this.Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new detailitemadapter(recyclelist);

            new LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
            return itemView;
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
                    WebReference1.redeemhisdetail emp = new WebReference1.redeemhisdetail();
                    emp = MyClient.GetRedeemhisdetail(sharedPreferences.GetString("CustomerKey", null),sharedPreferences.GetString("RedeemNumber",null));

                    dt = emp.redeemhisdetailtable;
                    recyclelist.Clear();
                    if (dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            recyclelist.Add(new detailitemgetset(dt.Rows[i][0].ToString()));
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
                txtstatus.Text = sharedPreferences.GetString("status", null);
                txttanggal.Text = sharedPreferences.GetString("Tanggal", null);
                txtredeemnumber.Text = sharedPreferences.GetString("RedeemNumber", null);
                mRecyclerView.SetAdapter(mAdapter);


            }
        }
    }
}