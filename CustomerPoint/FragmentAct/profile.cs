using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace CustomerPoint.FragmentAct
{
    public class profile : Fragment
    {
        static public SQLiteDatabase catalogdb;
        public sqliteHelper dbInstance;
        static ISharedPreferences sharedPreferences;
        TextView nohp, custname, noktp, alamat,texttoolbar;
        Button btnedit,btnsignout;
        LinearLayout ubahpin,daftartukarpoint;
        ImageView back;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View itemView = LayoutInflater.From(container.Context).
                    Inflate(Resource.Layout.profile, container, false);

            Android.Support.V7.Widget. Toolbar toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            texttoolbar = toolbar.FindViewById<TextView>(Resource.Id.texttoolbar);
            texttoolbar.Text = "AKUN SAYA";
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

            btnsignout = itemView.FindViewById<Button>(Resource.Id.btnsignout);
            btnedit = itemView.FindViewById<Button>(Resource.Id.btnedit);
            custname = itemView.FindViewById<TextView>(Resource.Id.custname);
            nohp = itemView.FindViewById<TextView>(Resource.Id.nohp);
            noktp = itemView.FindViewById<TextView>(Resource.Id.noktp);
            alamat = itemView.FindViewById<TextView>(Resource.Id.alamat);
            ubahpin = itemView.FindViewById<LinearLayout>(Resource.Id.ubahpin);
            daftartukarpoint = itemView.FindViewById<LinearLayout>(Resource.Id.daftartukar);

            ICursor cursor3 = catalogdb.RawQuery("select distinct " + sqliteTable.CustName + "," + sqliteTable.NoHP + "," + sqliteTable.NoKTP + "," + sqliteTable.Alamat + "    from " + sqliteTable.T_MsCustomer + " where " + sqliteTable.MasterKey + "='" + sharedPreferences.GetString("CustomerKey", null) + "'", null);

            while (cursor3.MoveToNext())
            {
                custname.Text = cursor3.GetString(0);
                nohp.Text = cursor3.GetString(1);
                noktp.Text = cursor3.GetString(2);
                alamat.Text = cursor3.GetString(3);
            }
            cursor3.Close();
            daftartukarpoint.Click += delegate
            {
                if (MainActivity.daftartukarpoint.IsAdded)
                {
                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(MainActivity.home);
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.historypoint);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.inbox);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.daftartukarpoint).Commit();
                    new daftartukarpoint.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
                }
                else
                {
                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.home);
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.historypoint);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.inbox);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.daftartukarpoint).Commit();
                   
                }

            };
            ubahpin.Click += delegate {
                Intent i = new Intent(Activity,typeof(ubahpin));
                StartActivity(i);
                Activity.Finish();
            };

            btnedit.Click += delegate
              {
                  Intent i = new Intent(Activity, typeof(CustomerPin));
                  i.PutExtra("FlagMenu", "1");
                  StartActivity(i);
                  Activity.Finish();
              };
            btnsignout.Click += delegate
            {
                ISharedPreferencesEditor editor = sharedPreferences.Edit();
                editor.Remove("CustomerKey");
                editor.Remove("RedeemNumber");
                editor.Remove("Tanggal");
                editor.Remove("status");
                editor.Remove("url");
                editor.Remove("produk");
                editor.Commit();

                catalogdb.ExecSQL("delete from " + sqliteTable.T_MsCustomer + "");

                Intent i = new Intent(Activity, typeof(customerkeyact));
                StartActivity(i);
                Activity.Finish();
            };
            return itemView;
        }
    }
}