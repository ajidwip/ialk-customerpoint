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
using CustomerPoint.GetterSetter;

namespace CustomerPoint.Adapter
{
    class detailitemadapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick, ItemLongClick;
        List<detailitemgetset> recyclelist;



        public detailitemadapter(List<detailitemgetset> Recyclelist)
        {
            this.recyclelist = Recyclelist;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                     Inflate(Resource.Layout.itemredeem, parent, false);

            RecycleViewHolderdetailitemadapter vh = new RecycleViewHolderdetailitemadapter(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            RecycleViewHolderdetailitemadapter vh = holder as RecycleViewHolderdetailitemadapter;
            vh.txtitem.Text = recyclelist[position].getitem();
           
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
    public class RecycleViewHolderdetailitemadapter : RecyclerView.ViewHolder
    {
        public TextView txtitem { get; private set; }
        
        public RecycleViewHolderdetailitemadapter(View itemView, Action<int> listener) : base(itemView)
        {


            txtitem = itemView.FindViewById<TextView>(Resource.Id.item);

        }
    
}
}