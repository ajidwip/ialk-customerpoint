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
    public class katalogdetail : Fragment
    {
       
        static View itemView;
        static ISharedPreferences sharedPreferences;
        static Activity context;
        ImageView imgproduk,back;
        TextView txtdesc, texttoolbar,txtproduk2, txtdesc2;
        int flagoncreate = 0;
        WebView webView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          //  sharedPreferences = Activity.GetSharedPreferences("sharedprefrences", 0);
            // Create your fragment here
        }
       
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            itemView = LayoutInflater.From(container.Context).
                     Inflate(Resource.Layout.katalogdetail, container, false);

            sharedPreferences = Activity.GetSharedPreferences("sharedprefrences", 0);
            MainActivity.countback = 0;
            flagoncreate = 1;
            //MainActivity.bottomNavigationView.Visibility = ViewStates.Gone;
            Android.Support.V7.Widget.Toolbar toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            texttoolbar = toolbar.FindViewById<TextView>(Resource.Id.texttoolbar);
            back = toolbar.FindViewById<ImageView>(Resource.Id.back);

            texttoolbar.Text = "Katalog";

            imgproduk = itemView.FindViewById<ImageView>(Resource.Id.imgproduk);
            txtdesc = itemView.FindViewById<TextView>(Resource.Id.txtdesc);
            txtproduk2 = itemView.FindViewById<TextView>(Resource.Id.txtproduk2);
           // txtdesc2 = itemView.FindViewById<TextView>(Resource.Id.txtdesc2);

            int position = katalog.recyclelist.FindIndex(a => a.getproduk().Contains(sharedPreferences.GetString("produk", null)));

            Glide.With(Application.Context).Load(katalog.recyclelist[position].getgambar1()).Apply(RequestOptions.SkipMemoryCacheOf(true)).Apply(RequestOptions.DiskCacheStrategyOf(DiskCacheStrategy.None)).Apply(RequestOptions.SignatureOf(new ObjectKey(position))).Into(imgproduk);


            back.Click += delegate
            {
                if (!MainActivity.katalog.IsAdded)
                {

                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.home);
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.historypoint);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.news);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.inbox);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.katalog);
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
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.inbox);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.katalog).Commit();
                }
            };

            txtdesc.Text = katalog.recyclelist[position].getdesc();
            txtproduk2.Text = sharedPreferences.GetString("produk", null);
            //txtdesc2.Text = Html.FromHtml(katalog.recyclelist[position].getdesc2()).ToString() ;
            webView =itemView.FindViewById<WebView>(Resource.Id.webView);
            webView.LoadDataWithBaseURL(null, katalog.recyclelist[position].getdesc2(), "text/html", "utf-8", null);
            return itemView;
        }

        public override void OnHiddenChanged(bool hidden)
        {
            base.OnHiddenChanged(hidden);
            if (!hidden)
            {
                if (flagoncreate == 1)
                {
                    int position = katalog.recyclelist.FindIndex(a => a.getproduk().Contains(sharedPreferences.GetString("produk", null)));

                    Glide.With(Application.Context).Load(katalog.recyclelist[position].getgambar1()).Apply(RequestOptions.SkipMemoryCacheOf(true)).Apply(RequestOptions.DiskCacheStrategyOf(DiskCacheStrategy.None)).Apply(RequestOptions.SignatureOf(new ObjectKey(position))).Into(imgproduk);

                    txtdesc.Text = katalog.recyclelist[position].getdesc();
                    txtproduk2.Text = sharedPreferences.GetString("produk", null);
                    webView.LoadDataWithBaseURL(null, katalog.recyclelist[position].getdesc2(), "text/html", "utf-8", null);
                }
            }
        }
    }
}