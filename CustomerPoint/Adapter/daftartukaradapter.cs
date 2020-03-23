using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using CustomerPoint.FragmentAct;
using CustomerPoint.GetterSetter;

namespace CustomerPoint.Adapter
{
    class daftartukaradapter : RecyclerView.Adapter
    {
        Context context;
        Android.App.AlertDialog.Builder _dialogBuilder;
        Android.App.AlertDialog alertdialog;
        public event EventHandler<int> ItemClick, ItemLongClick;
        List<daftartukargetset> recyclelist;
        Android.Support.V7.Widget.PopupMenu menu;
        int count = 0;
        int posisi2 = 0;
        public daftartukaradapter(List<daftartukargetset> Recyclelist)
        {
           
            this.recyclelist = Recyclelist;
        }
        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            context = parent.Context;
            View itemView = LayoutInflater.From(parent.Context).
                     Inflate(Resource.Layout.daftartukarlist, parent, false);

            RecycleViewHolderdaftartukaradapter vh = new RecycleViewHolderdaftartukaradapter(itemView, OnClick);
            return vh;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {

            RecycleViewHolderdaftartukaradapter vh = holder as RecycleViewHolderdaftartukaradapter;
            vh.txtredeemnumber.Text = recyclelist[position].getredeemnumber();
            vh.txtstatus.Text = recyclelist[position].getstatus();
            vh.txttanggal.Text = DateTime.Parse(recyclelist[position].gettanggal()).ToString("dd MMM yyyy");
            vh.showpopupmenu.Text = "⋮";
            vh.showpopupmenu.Click += delegate {

                menu = new Android.Support.V7.Widget.PopupMenu(context, vh.showpopupmenu);

                // Call inflate directly on the menu:
                menu.Inflate(Resource.Menu.popupmenu);

                // A menu item was clicked:
                menu.MenuItemClick += (s1, arg1) =>
                {
                    posisi2 = vh.AdapterPosition;
                    switch (arg1.Item.ItemId)
                    {
                        case Resource.Id.requestcancel:
                            if (recyclelist[posisi2].getstatuscode() == "1")
                            {
                                using (_dialogBuilder = new Android.App.AlertDialog.Builder(context))
                                {
                                    _dialogBuilder.SetTitle("Informasi");
                                    _dialogBuilder.SetMessage("Apakah anda yakin?");
                                    _dialogBuilder.SetCancelable(false);
                                    _dialogBuilder.SetPositiveButton("OK", ok);
                                    _dialogBuilder.SetNegativeButton("Cancel", cancel);
                                    alertdialog = _dialogBuilder.Create();

                                    alertdialog.Show();
                                }
                            }
                            else
                            {
                                Toast.MakeText(context, "Transaksi tidak dapat di cancel, sedang dalam proses", ToastLength.Short).Show();
                            }
                            break;
                        case Resource.Id.selesai:
                            if (recyclelist[posisi2].getstatuscode() == "5")
                            {

                                try {
                                    WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                                    MyClient.updatestatuspengiriman(recyclelist[posisi2].getredeemnumber());

                                    if (MainActivity.daftartukarpoint.IsAdded)
                                    {
                                        FragmentManager fragmentManager2 = ((AppCompatActivity)context).FragmentManager;
                                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                                        fragmenttransaction.Hide(MainActivity.home);
                                        fragmenttransaction.Hide(MainActivity.profile);
                                        fragmenttransaction.Hide(MainActivity.historypoint);
                                        fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                                        fragmenttransaction.Hide(MainActivity.katalog);
                                        fragmenttransaction.Hide(MainActivity.redeem);
                                        fragmenttransaction.Hide(MainActivity.redeemdetail);
                                        fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.daftartukarpoint).Commit();
                                        new daftartukarpoint.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
                                    }
                                    else
                                    {
                                        FragmentManager fragmentManager2 = ((AppCompatActivity)context).FragmentManager;
                                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                                        fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.daftartukarpoint);
                                        fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.daftartukarpoint).Commit();

                                    }
                                } catch(Exception)
                                {
                                    Toast.MakeText(context, "Mohon coba beberapa saat lagi", ToastLength.Short).Show();
                                }
                            }
                            
                            break;
                    }
                   
                   


                };

                // Menu was dismissed:
                menu.DismissEvent += (s2, arg2) =>
                {
                    count = 0;
                    Console.WriteLine("menu dismissed");
                };
                if (count == 0)
                {
                    menu.Show();
                    count++;
                }


            };
        }
        private void ok(object sender, DialogClickEventArgs e)
        {
            try
            {
                WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                MyClient.cancelredeem(recyclelist[posisi2].getredeemnumber());

                if (MainActivity.daftartukarpoint.IsAdded)
                {
                    FragmentManager fragmentManager2 = ((AppCompatActivity)context).FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(MainActivity.home);
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.historypoint);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.daftartukarpoint).Commit();
                    new daftartukarpoint.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
                }
                else
                {
                    FragmentManager fragmentManager2 = ((AppCompatActivity)context).FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.daftartukarpoint).Commit();

                }
                _dialogBuilder.Dispose();
                alertdialog.Dismiss();
            }
            catch(Exception ex)
            {
                _dialogBuilder.Dispose();
                alertdialog.Dismiss();
            }

            
        }
        private void cancel(object sender, DialogClickEventArgs e)
        {
            _dialogBuilder.Dispose();
            alertdialog.Dismiss();
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
    public class RecycleViewHolderdaftartukaradapter : RecyclerView.ViewHolder
    {
        public TextView txttanggal { get; private set; }
        public TextView txtstatus { get; private set; }
        public TextView txtredeemnumber { get; private set; }

        public Button showpopupmenu { get; private set; }
        public RecycleViewHolderdaftartukaradapter(View itemView, Action<int> listener) : base(itemView)
        {

            txtredeemnumber = itemView.FindViewById<TextView>(Resource.Id.txtredeemnumber);
            txttanggal = itemView.FindViewById<TextView>(Resource.Id.txttanggal);
            txtstatus = itemView.FindViewById<TextView>(Resource.Id.txtstatus);
            showpopupmenu = itemView.FindViewById<Button>(Resource.Id.showpopupmenu);
            itemView.Click += (sender, e) => listener(base.LayoutPosition);

            //  itemView.LongClick += (sender, e) => listener2(base.LayoutPosition);
        }
    }
}