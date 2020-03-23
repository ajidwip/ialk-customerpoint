using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Load.Engine;
using Com.Bumptech.Glide.Request;
using Com.Bumptech.Glide.Signature;
using CustomerPoint.GetterSetter;

namespace CustomerPoint.FragmentAct
{
    public class inboxdetail : Fragment
    {

        static View itemView;
        static ISharedPreferences sharedPreferences;
        static Activity context;
        ImageView back;
        TextView title, desc, tanggal, fulldesc, texttoolbar;
        int flagoncreate = 0;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //  sharedPreferences = Activity.GetSharedPreferences("sharedprefrences", 0);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            itemView = LayoutInflater.From(container.Context).
                     Inflate(Resource.Layout.inboxdetail, container, false);

            sharedPreferences = Activity.GetSharedPreferences("sharedprefrences", 0);
            MainActivity.countback = 0;
            flagoncreate = 1;
            Android.Support.V7.Widget.Toolbar toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            texttoolbar = toolbar.FindViewById<TextView>(Resource.Id.texttoolbar);
            back = toolbar.FindViewById<ImageView>(Resource.Id.back);

            texttoolbar.Text = "INBOX";
            
            title = itemView.FindViewById<TextView>(Resource.Id.title);
            desc = itemView.FindViewById<TextView>(Resource.Id.desc);
            tanggal = itemView.FindViewById<TextView>(Resource.Id.tanggal);
            fulldesc = itemView.FindViewById<TextView>(Resource.Id.fulldesc);

            back.Click += delegate
            {
                if (!MainActivity.inbox.IsAdded)
                {

                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.inbox);
                    fragmenttransaction.Hide(MainActivity.home);
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.historypoint);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.news);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.inboxdetail);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.inbox);
                    fragmenttransaction.CommitAllowingStateLoss();
                }
                else
                {

                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(MainActivity.home);
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.historypoint);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.news);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.inboxdetail);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.inbox).Commit();
                    new inbox.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
                }
            };
            title.Text = sharedPreferences.GetString("title", null);
            desc.Text = sharedPreferences.GetString("desc", null);
            tanggal.Text = sharedPreferences.GetString("tanggal", null);
            fulldesc.Text = sharedPreferences.GetString("fulldesc", null);
            return itemView;
        }
        public override void OnHiddenChanged(bool hidden)
        {
            base.OnHiddenChanged(hidden);
            if (!hidden)
            {
                if (flagoncreate == 1)
                {
                    title.Text = sharedPreferences.GetString("title", null);
                    desc.Text = sharedPreferences.GetString("desc", null);
                    tanggal.Text = sharedPreferences.GetString("tanggal", null);
                    fulldesc.Text = sharedPreferences.GetString("fulldesc", null);
                }
            }
        }
    }
}