using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
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
    class newsadapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick, ItemLongClick;
        List<newsgetset> recyclelist;



        public newsadapter(List<newsgetset> Recyclelist)
        {
            this.recyclelist = Recyclelist;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                     Inflate(Resource.Layout.newlist, parent, false);

            RecycleViewHolder1 vh = new RecycleViewHolder1(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            RecycleViewHolder1 vh = holder as RecycleViewHolder1;

            vh.textnews.Text = recyclelist[position].getdesc();

        
                Glide.With(Application.Context).Load(recyclelist[position].getimage()).Apply(RequestOptions.SkipMemoryCacheOf(true)).Apply(RequestOptions.DiskCacheStrategyOf(DiskCacheStrategy.None)).Apply(RequestOptions.SignatureOf(new ObjectKey(position))).Into(vh.imgnews);
            
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
    public class RecycleViewHolder1 : RecyclerView.ViewHolder
    {
        public int count = 0;
        public ImageView imgnews { get; private set; }
        public TextView textnews { get; private set; }
       

        public RecycleViewHolder1(View itemView, Action<int> listener) : base(itemView)
        {
            imgnews = itemView.FindViewById<ImageView>(Resource.Id.imgnews);
            textnews = itemView.FindViewById<TextView>(Resource.Id.textnews);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);

          //  itemView.LongClick += (sender, e) => listener2(base.LayoutPosition);
        }
    }
}
