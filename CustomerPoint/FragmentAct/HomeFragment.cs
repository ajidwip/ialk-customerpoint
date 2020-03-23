using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using Android.App;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;

using CustomerPoint.Adapter;
using CustomerPoint.GetterSetter;
using Microsoft.AspNet.SignalR.Client;

namespace CustomerPoint.FragmentAct
{
    public class HomeFragment : Fragment
    {
        static List<newsgetset> recyclelist = new List<newsgetset>();
        static RecyclerView mRecyclerView;
        static newsadapter mAdapter;
        public static List<byte[]> imagelist = new List<byte[]>();
        static public SQLiteDatabase catalogdb;
        public sqliteHelper dbInstance;
        static ISharedPreferences sharedPreferences;
        public static ProgressBar progressbar1, progressbar2, progressbar3;
        static TextView poin, tanggalberlaku,notifinbox;
        static  Activity context;
    
        public static ViewPager viewPager;
        static TabLayout tabLayout;
        public static TextView namatoko;
        LinearLayout lnhispoint;
       
        Bundle bundle=new Bundle();
        static View itemView;
        TextView txtlihatsemua;
        static Timer timer;

        Button btnriwayat;
        ImageView btnmsg;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetHasOptionsMenu(true);
            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
             itemView = LayoutInflater.From(container.Context).
                     Inflate(Resource.Layout.home, container, false);

            context = Activity;

            sharedPreferences = Activity.GetSharedPreferences("sharedprefrences", 0);

            dbInstance = CoreApplication.getInstance().getDatabase();
            catalogdb = dbInstance.WritableDatabase;
            viewPager = itemView.FindViewById<ViewPager>(Resource.Id.viewPager);

            lnhispoint = itemView.FindViewById<LinearLayout>(Resource.Id.lnhispoint);
            tabLayout = itemView.FindViewById<TabLayout>(Resource.Id.tab_layout);
            tabLayout.SetupWithViewPager(viewPager, true);
            txtlihatsemua = itemView.FindViewById<TextView>(Resource.Id.txtlihatsemua);
            namatoko = itemView.FindViewById<TextView>(Resource.Id.namatoko);
            btnriwayat = itemView.FindViewById<Button>(Resource.Id.btnriwayat);
            btnmsg = itemView.FindViewById<ImageView>(Resource.Id.btnmsg);

            progressbar1 = itemView.FindViewById<ProgressBar>(Resource.Id.progressBar1);
            progressbar2 = itemView.FindViewById<ProgressBar>(Resource.Id.progressBar2);
            progressbar3 = itemView.FindViewById<ProgressBar>(Resource.Id.progressBar3);

            mRecyclerView = itemView.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            RecyclerView.LayoutManager  mLayoutManager = new LinearLayoutManager(Activity, LinearLayoutManager.Horizontal, false);
            //RecyclerView.LayoutManager mLayoutManager = new LinearLayoutManager(this.Activity);
            mRecyclerView.SetLayoutManager(mLayoutManager);
            mAdapter = new newsadapter(recyclelist);


            btnriwayat.Click += delegate
            {
                if (MainActivity.historypoint.IsAdded)
                {
                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(MainActivity.home);
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.news);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.inbox);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.historypoint).Commit();
                    new historypointfragment.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
                }
                else
                {
                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.historypoint);
                    fragmenttransaction.Hide(MainActivity.home);
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.news);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.inbox);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.historypoint).Commit();
                }
            };

            btnmsg.Click += delegate
            {
                if (MainActivity.inbox.IsAdded)
                {
                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(MainActivity.home);
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.historypoint);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.news);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.inbox).Commit();
                    new inbox.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
                }
                else
                {
                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.inbox);
                    fragmenttransaction.Hide(MainActivity.home);
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.historypoint);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.news);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.inbox).Commit();
                }
            };

            txtlihatsemua.Click += delegate
            {
                Intent i = new Intent(Activity,typeof(promodetail));
                StartActivity(i);
                Activity.Finish();
            };

            lnhispoint.Click += delegate
            {

                if (MainActivity.historypoint.IsAdded)
                {
                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(MainActivity.home);
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.news);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.inbox);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.historypoint).Commit();
                    new historypointfragment.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
                }
                else
                {
                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.historypoint);
                    fragmenttransaction.Hide(MainActivity.home);
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.news);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.inbox);
                    fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.historypoint).Commit();
                }

            };
            mAdapter.ItemClick += OnItemClick;
            //  prepare();



            poin = itemView.FindViewById<TextView>(Resource.Id.poin);
            tanggalberlaku = itemView.FindViewById<TextView>(Resource.Id.tanggalberlaku);
            notifinbox = itemView.FindViewById<TextView>(Resource.Id.notifinbox);

            new LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
           
          
            return itemView;
        }
       
        void OnItemClick(object sender, int position)
        {
            ISharedPreferencesEditor editor = sharedPreferences.Edit();
            editor.PutString("url", recyclelist[position].geturl());
            editor.Commit();

            if (MainActivity.news.IsAdded)
            {
                FragmentManager fragmentManager2 = this.FragmentManager;
                FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                fragmenttransaction.Hide(MainActivity.home);
                fragmenttransaction.Hide(MainActivity.profile);
                fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                fragmenttransaction.Hide(MainActivity.historypoint);
                fragmenttransaction.Hide(MainActivity.redeem);
                fragmenttransaction.Hide(MainActivity.katalog);
                fragmenttransaction.Hide(MainActivity.faq);
                fragmenttransaction.Hide(MainActivity.kataloddetail);
                fragmenttransaction.Hide(MainActivity.redeemdetail);
                fragmenttransaction.Hide(MainActivity.inbox);
                fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.news).Commit();
                news.view();

            }
            else
            {
                FragmentManager fragmentManager2 = this.FragmentManager;
                FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.news);
                fragmenttransaction.Hide(MainActivity.home);
                fragmenttransaction.Hide(MainActivity.profile);
                fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                fragmenttransaction.Hide(MainActivity.historypoint);
                fragmenttransaction.Hide(MainActivity.redeem);
                fragmenttransaction.Hide(MainActivity.katalog);
                fragmenttransaction.Hide(MainActivity.kataloddetail);
                fragmenttransaction.Hide(MainActivity.faq);
                fragmenttransaction.Hide(MainActivity.redeemdetail);
                fragmenttransaction.Hide(MainActivity.inbox);
                fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.news).Commit();
            }

        }
        public class LoadDataForActivity1 : AsyncTask
        {
            string namatoko1 = "";
            DataTable dt = new DataTable();
            DataTable dtinbox = new DataTable();
            DataTable dtupdatedata = new DataTable();
            public static int flag = 0;
            protected override void OnPreExecute()
            {
               
               
            }

            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                try
                {
                    if (MainActivity.isloaded == 0)
                    {
                        WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                        WebReference1.getpoinheader emp = new WebReference1.getpoinheader();
                        emp = MyClient.getpoinheader(sharedPreferences.GetString("CustomerKey", null));

                        dt = emp.poinheadertable;
                        
                        WebReference1.CountInbox empinbox = new WebReference1.CountInbox();
                        empinbox = MyClient.GetCountInbox(sharedPreferences.GetString("CustomerKey", null));

                        dtinbox = empinbox.CountInboxtable;

                        WebReference1.UpdateData empupdatedata = new WebReference1.UpdateData();
                        empupdatedata = MyClient.GetUpdateData("0", "T_MsInbox");

                        dtupdatedata = empupdatedata.UpdateDatatable;

                        var a = dtupdatedata.Rows[0][0].ToString();
                        var b = DateTime.Parse(dtupdatedata.Rows[0][1].ToString());
                        var c = DateTime.Now; ;

                        if (b > c)
                        {
                        }

                        catalogdb.ExecSQL("delete from " + sqliteTable.T_MsPromo + "");

                        WebReference1.Promo emp2 = new WebReference1.Promo();
                        emp2 = MyClient.getpromonew();
                        DataTable dt3 = new DataTable();
                        dt3 = emp2.tablePromo;

                        if (dt3.Rows.Count > 0)
                        {

                            for (int i = 0; i < dt3.Rows.Count; i++)
                            {
                                if (dt3.Rows[i][1] == DBNull.Value)
                                {
                                    Drawable drawable = context.Resources.GetDrawable(Resource.Drawable.noimage);
                                    Bitmap bitmap = ((BitmapDrawable)drawable).Bitmap;
                                    MemoryStream stream = new MemoryStream();
                                    bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                                    byte[] bitMapData = stream.ToArray();

                                    ContentValues values = new ContentValues();
                                    values.Put(sqliteTable.NamaPromo, dt3.Rows[i][0].ToString());
                                    values.Put(sqliteTable.SK, dt3.Rows[i][2].ToString());
                                    values.Put(sqliteTable.validfrom, DateTime.Parse(dt3.Rows[i][3].ToString()).ToString("dd MMMM yyyy"));
                                    values.Put(sqliteTable.validto, DateTime.Parse(dt3.Rows[i][4].ToString()).ToString("dd MMMM yyyy"));
                                    values.Put(sqliteTable.GambarPromo, bitMapData);

                                    catalogdb.Insert(sqliteTable.T_MsPromo, null, values);
                                }
                                else
                                {
                                    ContentValues values = new ContentValues();
                                    values.Put(sqliteTable.NamaPromo, dt3.Rows[i][0].ToString());
                                    values.Put(sqliteTable.SK, dt3.Rows[i][2].ToString());
                                    values.Put(sqliteTable.validfrom, DateTime.Parse(dt3.Rows[i][3].ToString()).ToString("dd MMMM yyyy"));
                                    values.Put(sqliteTable.validto, DateTime.Parse(dt3.Rows[i][4].ToString()).ToString("dd MMMM yyyy"));
                                    values.Put(sqliteTable.GambarPromo, (byte[])dt3.Rows[i][1]);

                                    catalogdb.Insert(sqliteTable.T_MsPromo, null, values);
                                }

                            }

                        }

                        recyclelist.Clear();
                        WebReference1.news emp3 = new WebReference1.news();
                        emp3 = MyClient.getnews();
                        DataTable dt2 = new DataTable();
                        dt2 = emp3.tablenews;
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            if (dt2.Rows[i][2] == DBNull.Value)
                            {
                                Drawable drawable = context.Resources.GetDrawable(Resource.Drawable.noimage);
                                Bitmap bitmap = ((BitmapDrawable)drawable).Bitmap;
                                MemoryStream stream = new MemoryStream();
                                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                                byte[] bitMapData = stream.ToArray();
                                recyclelist.Add(new newsgetset(bitMapData, dt2.Rows[i][0].ToString(), dt2.Rows[i][1].ToString()));
                            }
                            else
                            {
                                recyclelist.Add(new newsgetset((byte[])dt2.Rows[i][2], dt2.Rows[i][0].ToString(), dt2.Rows[i][1].ToString()));
                            }

                        }

                        ICursor cursor2 = catalogdb.RawQuery("select distinct " + sqliteTable.GambarPromo + " from " + sqliteTable.T_MsPromo + "", null);
                        imagelist.Clear();

                        while (cursor2.MoveToNext())
                        {
                            byte[] img = cursor2.GetBlob(0);
                            imagelist.Add(img);
                        }
                        cursor2.Close();

                        ICursor cursor3 = catalogdb.RawQuery("select distinct " + sqliteTable.CustName + " from " + sqliteTable.T_MsCustomer + " where " + sqliteTable.MasterKey + "='" + sharedPreferences.GetString("CustomerKey", null) + "'", null);

                        while (cursor3.MoveToNext())
                        {
                            namatoko1 = cursor3.GetString(0);

                        }
                        cursor3.Close();

                      
                    }
                    else
                    {
                        WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                        WebReference1.getpoinheader emp = new WebReference1.getpoinheader();
                        emp = MyClient.getpoinheader(sharedPreferences.GetString("CustomerKey", null));

                        dt = emp.poinheadertable;

                        WebReference1.CountInbox empinbox = new WebReference1.CountInbox();
                        empinbox = MyClient.GetCountInbox(sharedPreferences.GetString("CustomerKey", null));

                        dtinbox = empinbox.CountInboxtable;

                        /*WebReference1.UpdateData empupdatedata = new WebReference1.UpdateData();
                        empupdatedata = MyClient.GetUpdateData("0", "T_MsInbox");

                        dtupdatedata = empupdatedata.UpdateDatatable;

                        var a = dtupdatedata.Rows[0][0].ToString();
                        var b = DateTime.Parse(dtupdatedata.Rows[0][1].ToString());
                        var c = DateTime.Now; ;

                        if (b > c)
                        {
                            pushNotif();
                        }*/
                    }
                }catch(Exception ex)
                {
                    Console.WriteLine(ex);
                    Snackbar snackbar = Snackbar.Make(itemView, "Error Connection", Snackbar.LengthLong);
                    snackbar.Show();
                    
                }
                return null;
            }

            protected override void OnPostExecute(Java.Lang.Object result)
            {
                if (MainActivity.isloaded == 0)
                {
                    progressbar1.Visibility = ViewStates.Gone;
                    progressbar2.Visibility = ViewStates.Gone;
                    progressbar3.Visibility = ViewStates.Gone;


                    poin.Text = string.Format("{0:n0}", Convert.ToDouble(dt.Rows[0][0].ToString()));

                    if (dtinbox.Rows[0][0].ToString() != "0")
                    {
                        int countinbox = Int32.Parse(dtinbox.Rows[0][0].ToString());
                        if (countinbox >= 100)
                        {
                            notifinbox.Text = "100+";
                            notifinbox.Visibility = ViewStates.Visible;
                        }
                        else
                        {
                            notifinbox.Text = dtinbox.Rows[0][0].ToString();
                            notifinbox.Visibility = ViewStates.Visible;
                        }
                    }
                    else
                    {
                        notifinbox.Visibility = ViewStates.Gone;
                    }

                    if (dt.Rows[0][1].ToString()!="")
                    {
                        tanggalberlaku.Text = DateTime.Parse(dt.Rows[0][1].ToString()).ToString("dd-MM-yyyy");
                    }
                  

                    mRecyclerView.SetAdapter(mAdapter);
                    
                        ImageAdapter adapter = new ImageAdapter(context, imagelist);
                        viewPager.Adapter = adapter;

                        namatoko.Text = namatoko1;

                        timer = new System.Timers.Timer();
                        timer.Interval = 4000;
                        timer.Enabled = true;
                        int page = 0;
                        timer.Elapsed += (sender, args) =>
                        {
                            context.RunOnUiThread(() =>
                            {
                                if (viewPager.Adapter != null)
                                {
                                    if (page < viewPager.Adapter.Count)
                                    {
                                        page++;
                                    }
                                    else
                                    {
                                        page = 0;
                                    }
                                    tabLayout.SetScrollPosition(page, 0f, true);
                                    viewPager.SetCurrentItem(page, true);
                                }
                            });
                          
                        };
                       
                    
                    MainActivity.isloaded = 1;
                }
                else
                {

                    poin.Text = string.Format("{0:n0}", Convert.ToDouble(dt.Rows[0][0].ToString()));

                    if (dtinbox.Rows[0][0].ToString() != "0")
                    {
                        notifinbox.Text = dtinbox.Rows[0][0].ToString();
                        notifinbox.Visibility = ViewStates.Visible;
                    }
                    else
                    {
                        notifinbox.Visibility = ViewStates.Gone;
                    }

                    if (dt.Rows[0][1].ToString()!="")
                    {
                        tanggalberlaku.Text = DateTime.Parse(dt.Rows[0][1].ToString()).ToString("dd-MM-yyyy");
                    }
                 

                    mRecyclerView.SetAdapter(mAdapter);

                    progressbar1.Visibility = ViewStates.Gone;
                    progressbar2.Visibility = ViewStates.Gone;
                    progressbar3.Visibility = ViewStates.Gone;

                }
            }
        }
        /*private static void pushNotif()
        {
            var hubConnection = new HubConnection("http://192.168.6.232:2208/signalrservices/signalr");
            var hub = hubConnection.CreateHubProxy("MyHub");
            hubConnection.Start().Wait();
            hub.Invoke("Show");
        }*/
    }
}