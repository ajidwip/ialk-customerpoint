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
    class inboxadapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick, ItemLongClick;
        List<inboxGetSet> recyclelist;

        public inboxadapter(List<inboxGetSet> Recyclelist)
        {
            this.recyclelist = Recyclelist;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                     Inflate(Resource.Layout.inboxlist, parent, false);

            RecycleViewHolderInbox vh = new RecycleViewHolderInbox(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            RecycleViewHolderInbox vh = holder as RecycleViewHolderInbox;

            //vh.circleView.SetImageResource(Resource.Drawable.noimage);
            vh.title.Text = recyclelist[position].gettitle();
            var a = recyclelist[position].getstatus();
            if (recyclelist[position].getstatus() == "True")
            {
                vh.title.SetTypeface(null, TypefaceStyle.Normal);
                vh.desc.SetTypeface(null, TypefaceStyle.Normal);
            }
            else
            {
                vh.title.SetTypeface(null, TypefaceStyle.Bold);
                vh.desc.SetTypeface(null, TypefaceStyle.Bold);
                vh.desc.SetTextColor(Android.Graphics.Color.Black);
            }
            vh.desc.Text = recyclelist[position].getdesc();
            vh.tanggal.Text = recyclelist[position].gettanggal();
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
    public class RecycleViewHolderInbox : RecyclerView.ViewHolder
    {
        public int count = 0;
        //public ImageView circleView { get; private set; }
        public TextView title { get; private set; }
        public TextView desc { get; private set; }
        public TextView tanggal { get; private set; }

        public RecycleViewHolderInbox(View itemView, Action<int> listener) : base(itemView)
        {
            //circleView = itemView.FindViewById<ImageView>(Resource.Id.circleView);
            title = itemView.FindViewById<TextView>(Resource.Id.title);
            desc = itemView.FindViewById<TextView>(Resource.Id.desc);
            tanggal = itemView.FindViewById<TextView>(Resource.Id.tanggal);

            itemView.Click += (sender, e) => listener(base.LayoutPosition);
            
        }
    }
}