using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using Com.Bumptech.Glide;
using Com.Bumptech.Glide.Load.Engine;
using Com.Bumptech.Glide.Request;
using Com.Bumptech.Glide.Signature;
using CustomerPoint.GetterSetter;

namespace CustomerPoint.Adapter
{
    class katalogadapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick, ItemLongClick;
        List<katalogGetSet> recyclelist;



        public katalogadapter(List<katalogGetSet> Recyclelist)
        {
            this.recyclelist = Recyclelist;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                     Inflate(Resource.Layout.kataloglist, parent, false);

            RecycleViewHolder3 vh = new RecycleViewHolder3(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            RecycleViewHolder3 vh = holder as RecycleViewHolder3;

          //  vh.txtproduk.Text = recyclelist[position].getproduk();

            //vh.desc.Text = recyclelist[position].getdesc();
            Bitmap bitmap = BitmapFactory.DecodeByteArray(recyclelist[position].getgambar2(), 0, recyclelist[position].getgambar2().Length);
            BitmapDrawable background = new BitmapDrawable(bitmap);

            vh.imgkatalog1.SetBackgroundDrawable(background);
            //Glide.With(Application.Context).Load(recyclelist[position].getgambar2()).Apply(RequestOptions.SkipMemoryCacheOf(true)).Apply(RequestOptions.DiskCacheStrategyOf(DiskCacheStrategy.None)).Apply(RequestOptions.SignatureOf(new ObjectKey(position))).Into(vh.imgkatalog1);

            //Glide.With(Application.Context).Load(recyclelist[position].getgambar2()).Apply(RequestOptions.SkipMemoryCacheOf(true)).Apply(RequestOptions.DiskCacheStrategyOf(DiskCacheStrategy.None)).Apply(RequestOptions.SignatureOf(new ObjectKey(position))).Into(vh.imgkatalog2);

        }


        public override int ItemCount
        {
            get { return recyclelist == null ? 0 : recyclelist.Count; }
        }
        void OnClick(int position)
        {
            ItemClick(this, position);
        }
        void OnLongClick(int position)
        {
            ItemLongClick(this, position);
        }
    }
    public class RecycleViewHolder3 : RecyclerView.ViewHolder
    {
        public int count = 0;
        public RelativeLayout imgkatalog1 { get; private set; }
        //public ImageView imgkatalog2 { get; private set; }

       // public TextView txtproduk { get; private set; }

        //public TextView desc { get; private set; }
        public RecycleViewHolder3(View itemView, Action<int> listener) : base(itemView)
        {
            imgkatalog1 = itemView.FindViewById<RelativeLayout>(Resource.Id.imgkatalog1);
            //imgkatalog2 = itemView.FindViewById<ImageView>(Resource.Id.imgkatalog2);

          //  txtproduk = itemView.FindViewById<TextView>(Resource.Id.txtproduk);
            //desc = itemView.FindViewById<TextView>(Resource.Id.desc);

             itemView.Click += (sender, e) => listener(base.LayoutPosition);

            //  itemView.LongClick += (sender, e) => listener2(base.LayoutPosition);
        }
    }
}