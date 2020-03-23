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
    class promoadapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick, ItemLongClick;
        List<promogetset> recyclelist;



        public promoadapter(List<promogetset> Recyclelist)
        {
            this.recyclelist = Recyclelist;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                     Inflate(Resource.Layout.promodetaillist, parent, false);

            RecycleViewHolderpromo vh = new RecycleViewHolderpromo(itemView);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            RecycleViewHolderpromo vh = holder as RecycleViewHolderpromo;
            vh.textperiode.Text = recyclelist[position].getperiode();
            vh.textketentuan.Text = recyclelist[position].getketentuan();

            
                Glide.With(Application.Context).Load(recyclelist[position].getimgpromo()).Apply(RequestOptions.SkipMemoryCacheOf(true)).Apply(RequestOptions.DiskCacheStrategyOf(DiskCacheStrategy.None)).Apply(RequestOptions.SignatureOf(new ObjectKey(position))).Into(vh.imgpromo);
            
        }


        public override int ItemCount
        {
            get { return recyclelist == null ? 0 : recyclelist.Count; }
        }
        //void OnClick(int position)
        //{
        //    ItemClick(this, position);
        //}
        //void OnLongClick(int position)
        //{
        //    ItemLongClick(this, position);
        //}
    }
    public class RecycleViewHolderpromo : RecyclerView.ViewHolder
    {
        public ImageView imgpromo { get; private set; }
        public TextView textperiode { get; private set; }
        public TextView textketentuan { get; private set; }
        public RecycleViewHolderpromo(View itemView) : base(itemView)
        {
            imgpromo = itemView.FindViewById<ImageView>(Resource.Id.imgpromo);
            textperiode = itemView.FindViewById<TextView>(Resource.Id.textperiode);
            textketentuan = itemView.FindViewById<TextView>(Resource.Id.textketentuan);

            //itemView.Click += (sender, e) => listener(base.LayoutPosition);

            
        }
    }
}