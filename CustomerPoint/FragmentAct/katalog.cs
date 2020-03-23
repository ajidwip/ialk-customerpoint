using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Util;
using Android.Views;
using Android.Widget;
using CustomerPoint.Adapter;
using CustomerPoint.GetterSetter;


namespace CustomerPoint.FragmentAct
{
    public class katalog : Fragment
    {
       public static List<katalogGetSet> recyclelist = new List<katalogGetSet>();
        static ISharedPreferences sharedPreferences;
        static Activity context;
        static RecyclerView mRecyclerView;
        public static ProgressDialog progressDialog;
        TextView texttoolbar;
        static katalogadapter mAdapter;
        static View itemView;
        ImageView back;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }
      
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
             itemView = LayoutInflater.From(container.Context).
                      Inflate(Resource.Layout.katalog, container, false);
            context = Activity;
            sharedPreferences = Activity.GetSharedPreferences("sharedprefrences", 0);

            Android.Support.V7.Widget.Toolbar toolbar = itemView.FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            texttoolbar = toolbar.FindViewById<TextView>(Resource.Id.texttoolbar);
            texttoolbar.Text = "KATALOG";
            back = toolbar.FindViewById<ImageView>(Resource.Id.back);
            back.Click += delegate
            {
                if (MainActivity.home.IsAdded)
                {
                    MainActivity.isloaded = 1;
                    FragmentManager fragmentManager3 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction3 = fragmentManager3.BeginTransaction();
                    fragmenttransaction3.Hide(MainActivity.profile);
                    fragmenttransaction3.Hide(MainActivity.faq);
                    fragmenttransaction3.Hide(MainActivity.redeem);
                    fragmenttransaction3.Hide(MainActivity.historypoint);
                    fragmenttransaction3.Hide(MainActivity.katalog);
                    fragmenttransaction3.Hide(MainActivity.kataloddetail);
                    fragmenttransaction3.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction3.Hide(MainActivity.redeemdetail);
                    fragmenttransaction3.Hide(MainActivity.news);
                    fragmenttransaction3.Hide(MainActivity.inbox);
                    fragmenttransaction3.Hide(MainActivity.currentFragment).Show(MainActivity.home).Commit();
                    new HomeFragment.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);

                }
                else
                {
                    MainActivity.isloaded = 0;
                    FragmentManager fragmentManager2 = this.FragmentManager;

                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(MainActivity.profile);
                    fragmenttransaction.Hide(MainActivity.redeem);
                    fragmenttransaction.Hide(MainActivity.faq);
                    fragmenttransaction.Hide(MainActivity.historypoint);
                    fragmenttransaction.Hide(MainActivity.katalog);
                    fragmenttransaction.Hide(MainActivity.kataloddetail);
                    fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                    fragmenttransaction.Hide(MainActivity.redeemdetail);
                    fragmenttransaction.Hide(MainActivity.news);
                    fragmenttransaction.Hide(MainActivity.inbox);
                    fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.home);
                    fragmenttransaction.Show(MainActivity.home).Commit();
                    MainActivity.currentFragment = MainActivity.home;

                }

            };

            mRecyclerView = itemView.FindViewById<RecyclerView>(Resource.Id.recyclerView);
            mRecyclerView.SetLayoutManager(new GridLayoutManager(this.Activity,2));
            mAdapter = new katalogadapter(recyclelist);
            mAdapter.ItemClick += OnItemClick;

            new LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
            return itemView;
        }
        void OnItemClick(object sender, int position)
        {
            ISharedPreferencesEditor editor = sharedPreferences.Edit();
            editor.PutString("produk", recyclelist[position].getproduk());
            editor.Commit();
            if (!MainActivity.kataloddetail.IsAdded)
            {

                FragmentManager fragmentManager2 = this.FragmentManager;
                FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                fragmenttransaction.Add(Resource.Id.containerbody, MainActivity.kataloddetail);
                fragmenttransaction.Hide(MainActivity.home);
                fragmenttransaction.Hide(MainActivity.profile);
                fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                fragmenttransaction.Hide(MainActivity.historypoint);
                fragmenttransaction.Hide(MainActivity.redeem);
                fragmenttransaction.Hide(MainActivity.news);
                fragmenttransaction.Hide(MainActivity.redeemdetail);
                fragmenttransaction.Hide(MainActivity.katalog);
                fragmenttransaction.Hide(MainActivity.faq);
                fragmenttransaction.Hide(MainActivity.inbox);
                fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.kataloddetail);
                fragmenttransaction.Commit();

            }
            else
            {

                FragmentManager fragmentManager2 = this.FragmentManager;
                FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                fragmenttransaction.Hide(MainActivity.home);
                fragmenttransaction.Hide(MainActivity.profile);
                fragmenttransaction.Hide(MainActivity.daftartukarpoint);
                fragmenttransaction.Hide(MainActivity.historypoint);
                fragmenttransaction.Hide(MainActivity.redeem);
                fragmenttransaction.Hide(MainActivity.news);
                fragmenttransaction.Hide(MainActivity.redeemdetail);
                fragmenttransaction.Hide(MainActivity.katalog);
                fragmenttransaction.Hide(MainActivity.faq);
                fragmenttransaction.Hide(MainActivity.inbox);
                fragmenttransaction.Hide(MainActivity.currentFragment).Show(MainActivity.kataloddetail).Commit();
            }
        }
        public class LoadDataForActivity1 : AsyncTask
        {

            DataTable dt = new DataTable();
            public static int flag = 0;
            protected override void OnPreExecute()
            {

                progressDialog = ProgressDialog.Show(context, "", "loading...", true);
                progressDialog.SetProgressStyle(ProgressDialogStyle.Spinner);


            }

            protected override Java.Lang.Object DoInBackground(params Java.Lang.Object[] @params)
            {
                WebReference1.BasicHttpBinding_IService1 MyClient = new WebReference1.BasicHttpBinding_IService1();
                try
                {
                   
                    WebReference1.Katalog emp = new WebReference1.Katalog();
                    emp = MyClient.GetKatalog();

                    dt = emp.Katalogtable;


                    recyclelist.Clear();
                    if (dt.Rows.Count > 0)
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if(dt.Rows[i][2]==System.DBNull.Value && dt.Rows[i][3]!=DBNull.Value)
                            {
                                Drawable drawable = context.Resources.GetDrawable(Resource.Drawable.noimage);
                                Bitmap bitmap = ((BitmapDrawable)drawable).Bitmap;
                                MemoryStream stream = new MemoryStream();
                                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                                byte[] bitMapData = stream.ToArray();
                                recyclelist.Add(new katalogGetSet(bitMapData, (byte[])dt.Rows[i][3], dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(),dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString()));
                            }
                            else if (dt.Rows[i][3] == System.DBNull.Value && dt.Rows[i][2] != DBNull.Value)
                            {
                                Drawable drawable = context.Resources.GetDrawable(Resource.Drawable.noimage);
                                Bitmap bitmap = ((BitmapDrawable)drawable).Bitmap;
                                MemoryStream stream = new MemoryStream();
                                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                                byte[] bitMapData = stream.ToArray();
                                recyclelist.Add(new katalogGetSet((byte[])dt.Rows[i][2], bitMapData, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString()));
                            }
                            else if (dt.Rows[i][3] == System.DBNull.Value && dt.Rows[i][2] == DBNull.Value)
                            {
                                Drawable drawable = context.Resources.GetDrawable(Resource.Drawable.noimage);
                                Bitmap bitmap = ((BitmapDrawable)drawable).Bitmap;
                                MemoryStream stream = new MemoryStream();
                                bitmap.Compress(Bitmap.CompressFormat.Jpeg, 100, stream);
                                byte[] bitMapData = stream.ToArray();
                                recyclelist.Add(new katalogGetSet(bitMapData, bitMapData, dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString()));
                            }
                            else
                            {
                                recyclelist.Add(new katalogGetSet((byte[])dt.Rows[i][2], (byte[])dt.Rows[i][3], dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString(), dt.Rows[i][4].ToString(), dt.Rows[i][5].ToString()));
                            }
                            
                        }

                    }

                }
                catch(Exception ex)
                {
                  
                    Snackbar snackbar = Snackbar.Make(itemView, "Error Connection", Snackbar.LengthLong);
                    snackbar.Show();
                }
                return null;
            }
            protected override void OnPostExecute(Java.Lang.Object result)
            {

                progressDialog.Dismiss();

                mRecyclerView.SetAdapter(mAdapter);


            }
        }
    }
}