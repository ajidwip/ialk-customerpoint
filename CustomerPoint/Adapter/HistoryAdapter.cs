using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CustomerPoint.GetterSetter;

namespace CustomerPoint.Adapter
{
    class HistoryAdapter : RecyclerView.Adapter
    {
        public event EventHandler<int> ItemClick, ItemLongClick;
        List<HistoryGetSet> recyclelist;

        public HistoryAdapter(List<HistoryGetSet> Recyclelist)
        {
            this.recyclelist = Recyclelist;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            View itemView = LayoutInflater.From(parent.Context).
                     Inflate(Resource.Layout.historylist, parent, false);

            RecycleViewHolder2 vh = new RecycleViewHolder2(itemView, OnClick);
            return vh;
        }
        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            RecycleViewHolder2 vh = holder as RecycleViewHolder2;

            vh.txtketerangan.Text = recyclelist[position].getketerangan();
            vh.txtinv.Text = recyclelist[position].getinvoice();
            vh.txttanggal.Text = DateTime.Parse(recyclelist[position].gettanggal()).ToString("dd MMM yyyy");

            if (recyclelist[position].getMarker()=="-")
            {
                vh.points.SetTextColor(Color.ParseColor("#ff0000"));
                vh.points.Text = recyclelist[position].getMarker() + "" + recyclelist[position].getpoint();
            }
            else if(recyclelist[position].getMarker() == "+")
            {
                vh.points.SetTextColor(Color.ParseColor("#009FFF"));
                vh.points.Text = recyclelist[position].getMarker() + "" + recyclelist[position].getpoint();
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
    public class RecycleViewHolder2 : RecyclerView.ViewHolder
    {
        public TextView txtketerangan { get; private set; }
        public TextView txtinv { get; private set; }
        public TextView points { get; private set; }

        public TextView txttanggal { get; private set; }
        public RecycleViewHolder2(View itemView, Action<int> listener) : base(itemView)
        {
            points = itemView.FindViewById<TextView>(Resource.Id.points);
            txtketerangan = itemView.FindViewById<TextView>(Resource.Id.txtKeterangan);
            txtinv = itemView.FindViewById<TextView>(Resource.Id.txtinv);
            txttanggal = itemView.FindViewById<TextView>(Resource.Id.txttanggal);
        }
    }
}