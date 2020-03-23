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
    class redeemitemadapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick, ItemLongClick;
        List<redeemGetSet> recyclelist;
        public redeemitemadapter(List<redeemGetSet> Recyclelist)
        {
            this.recyclelist = Recyclelist;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                     Inflate(Resource.Layout.redeemitemlist, parent, false);

            RecycleViewHolder4 vh = new RecycleViewHolder4(itemView, OnClick);
            return vh;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            RecycleViewHolder4 vh = holder as RecycleViewHolder4;
            vh.txtitem.Text = recyclelist[position].getitem();

            vh.txtpoints.Text = recyclelist[position].getpoint();

            try
            {
                Glide.With(Application.Context).Load(recyclelist[position].getimgitem()).Apply(RequestOptions.SkipMemoryCacheOf(true)).Apply(RequestOptions.DiskCacheStrategyOf(DiskCacheStrategy.None)).Apply(RequestOptions.SignatureOf(new ObjectKey(position))).Into(vh.imgitem);
            }catch(Exception ex)
            {
                Glide.With(Application.Context).Load(Resource.Drawable.noimage).Apply(RequestOptions.SkipMemoryCacheOf(true)).Apply(RequestOptions.DiskCacheStrategyOf(DiskCacheStrategy.None)).Apply(RequestOptions.SignatureOf(new ObjectKey(position))).Into(vh.imgitem);

            }

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
    public class RecycleViewHolder4 : RecyclerView.ViewHolder
    {
        public ImageView imgitem { get; private set; }
        public TextView txtitem { get; private set; }
        public TextView txtpoints { get; private set; }

        public Button btnredeem { get; private set; }
        public RecycleViewHolder4(View itemView, Action<int> listener) : base(itemView)
        {
            imgitem = itemView.FindViewById<ImageView>(Resource.Id.imgitem);
            txtitem = itemView.FindViewById<TextView>(Resource.Id.txtitem);
            txtpoints = itemView.FindViewById<TextView>(Resource.Id.txtpoints);

            btnredeem = itemView.FindViewById<Button>(Resource.Id.btnredeem);

            btnredeem.Click += (sender, e) => listener(base.LayoutPosition);

        }
    }
}