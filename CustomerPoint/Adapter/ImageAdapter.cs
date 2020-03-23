using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Load.Engine;
using Com.Bumptech.Glide.Request;
using Com.Bumptech.Glide.Request.Target;
using Com.Bumptech.Glide.Signature;
using Java.Lang;

namespace CustomerPoint
{
    public class ImageAdapter : PagerAdapter
    {
        private Context context;
        private List<byte[]> imageList = new List<byte[]>();
        LayoutInflater LayoutInflater;

        public ImageAdapter(Context context,List<byte[]> imagelist1)
        {
            this.context = context;
            this.imageList = imagelist1;
            LayoutInflater = LayoutInflater.From(context);
        }
        public override int Count
        {
            get
            {
                return imageList.Count;
            }
        }

        public override bool IsViewFromObject(View view, Java.Lang.Object @object)
        {
            return view.Equals(@object);
        }
        public override Java.Lang.Object InstantiateItem(ViewGroup container, int position)
        {
            View view = LayoutInflater.Inflate(Resource.Layout.item, container, false);
            ImageView imageView = view.FindViewById<ImageView>(Resource.Id.imageview);

            Glide.With(context).Load(imageList[position]).Apply(RequestOptions.SkipMemoryCacheOf(true)).Apply(RequestOptions.DiskCacheStrategyOf(DiskCacheStrategy.None)).Apply(RequestOptions.SignatureOf(new ObjectKey(position))).Into(imageView);
          
            container.AddView(view);
          
            return view;
        }
        public bool checkisonline()
        {
            bool connectStatus = true;
            ConnectivityManager ConnectionManager = (ConnectivityManager)Application.Context.GetSystemService(Context.ConnectivityService);
            NetworkInfo networkInfo = ConnectionManager.ActiveNetworkInfo;
            if (networkInfo == null)
            {
                connectStatus = false;
            }
            else
            {
                connectStatus = true;
            }


            return connectStatus;
        }
        public override void DestroyItem(ViewGroup container, int position, Java.Lang.Object @object)
        {
            //ViewPager vp = (ViewPager)container;
            //View view = (View)@object;
            //vp.RemoveView(view);
            //container.RemoveView((ImageView)@object);
        }
    }
}