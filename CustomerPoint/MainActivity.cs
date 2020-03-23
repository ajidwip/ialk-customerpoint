using Android.App;
using Android.Widget;
using Android.OS;
using Android.InputMethodServices;
using Android.Support.V7.App;
using Android.Content;
using Android.Support.Design.Widget;
using CustomerPoint.FragmentAct;
using Android.Content.PM;
using Microsoft.AspNet.SignalR.Client;

namespace CustomerPoint
{
    [Activity(Label = "Mitra Aquaproof", Theme = "@style/MyTheme", ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : AppCompatActivity
    {
   
        public static int countback = 0;
        public static BottomNavigationView bottomNavigationView;
        public static Fragment home = new HomeFragment();
        public static Fragment news = new news();
        public static Fragment katalog = new katalog();
        public static Fragment redeem = new RedeemItem();
        public static Fragment profile = new profile();
        public static Fragment historypoint = new historypointfragment();
        public  static Fragment daftartukarpoint = new daftartukarpoint();
        public  static Fragment redeemdetail = new redeemdetailhis();
        public static Fragment inboxdetail = new inboxdetail();
        public static Fragment kataloddetail = new katalogdetail();
        public static Fragment inbox = new inbox();
        public static Fragment faq = new faq();
        public static int isloaded = 0;
        public static Fragment currentFragment;

       
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            pushNotif();

            SetContentView(Resource.Layout.Main);
            bottomNavigationView = FindViewById<BottomNavigationView>(Resource.Id.bottom_navigation);
            bottomNavigationView.NavigationItemSelected += BottomNavigation_NavigationItemSelected;
            BottomNavigationViewHelper.disableShiftMode(bottomNavigationView);
        
            if (home.IsAdded)
            {
                isloaded = 1;
                FragmentManager fragmentManager3 = this.FragmentManager;
                FragmentTransaction fragmenttransaction3 = fragmentManager3.BeginTransaction();
                fragmenttransaction3.Hide(profile);
                fragmenttransaction3.Hide(faq);
                fragmenttransaction3.Hide(redeem);
                fragmenttransaction3.Hide(historypoint);
                fragmenttransaction3.Hide(katalog);
                fragmenttransaction3.Hide(kataloddetail);
                fragmenttransaction3.Hide(daftartukarpoint);
                fragmenttransaction3.Hide(redeemdetail);
                fragmenttransaction3.Hide(news);
                fragmenttransaction3.Hide(inbox);
                fragmenttransaction3.Hide(currentFragment).Show(home).Commit();
                new HomeFragment.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);

            }
            else
            {
                isloaded = 0;
                FragmentManager fragmentManager2 = this.FragmentManager;

                FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                fragmenttransaction.Hide(profile);
                fragmenttransaction.Hide(redeem);
                fragmenttransaction.Hide(faq);
                fragmenttransaction.Hide(historypoint);
                fragmenttransaction.Hide(katalog);
                fragmenttransaction.Hide(kataloddetail);
                fragmenttransaction.Hide(daftartukarpoint);
                fragmenttransaction.Hide(redeemdetail);
                fragmenttransaction.Hide(news);
                fragmenttransaction.Hide(inbox);
                fragmenttransaction.Add(Resource.Id.containerbody, home);
                fragmenttransaction.Show(home).Commit();
                currentFragment = home;

            }
           
        }
        public override void OnBackPressed()
        {
            if (!(kataloddetail.IsHidden))
            {
                if (!katalog.IsAdded)
                {

                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Add(Resource.Id.containerbody, katalog);
                    fragmenttransaction.Hide(home);
                    fragmenttransaction.Hide(profile);
                    fragmenttransaction.Hide(faq);
                    fragmenttransaction.Hide(daftartukarpoint);
                    fragmenttransaction.Hide(historypoint);
                    fragmenttransaction.Hide(redeem);
                    fragmenttransaction.Hide(news);
                    fragmenttransaction.Hide(redeemdetail);
                    fragmenttransaction.Hide(kataloddetail);
                    fragmenttransaction.Hide(inbox);
                    fragmenttransaction.Hide(currentFragment).Show(katalog);
                    fragmenttransaction.CommitAllowingStateLoss();

                }
                else
                {

                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(home);
                    fragmenttransaction.Hide(profile);
                    fragmenttransaction.Hide(faq);
                    fragmenttransaction.Hide(daftartukarpoint);
                    fragmenttransaction.Hide(historypoint);
                    fragmenttransaction.Hide(redeem);
                    fragmenttransaction.Hide(news);
                    fragmenttransaction.Hide(redeemdetail);
                    fragmenttransaction.Hide(kataloddetail);
                    fragmenttransaction.Hide(inbox);
                    fragmenttransaction.Hide(currentFragment).Show(katalog).Commit();
                }

            }
            else if (!(redeemdetail.IsHidden))
            {
                if (daftartukarpoint.IsAdded)
                {
                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(home);
                    fragmenttransaction.Hide(profile);
                    fragmenttransaction.Hide(faq);
                    fragmenttransaction.Hide(historypoint);
                    fragmenttransaction.Hide(daftartukarpoint);
                    fragmenttransaction.Hide(katalog);
                    fragmenttransaction.Hide(kataloddetail);
                    fragmenttransaction.Hide(redeem);
                    fragmenttransaction.Hide(redeemdetail);
                    fragmenttransaction.Hide(inbox);
                    fragmenttransaction.Hide(currentFragment).Show(daftartukarpoint).Commit();
                    new daftartukarpoint.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);
                }
                else
                {
                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Add(Resource.Id.containerbody, daftartukarpoint);
                    fragmenttransaction.Hide(home);
                    fragmenttransaction.Hide(profile);
                    fragmenttransaction.Hide(faq);
                    fragmenttransaction.Hide(historypoint);
                    fragmenttransaction.Hide(daftartukarpoint);
                    fragmenttransaction.Hide(katalog);
                    fragmenttransaction.Hide(kataloddetail);
                    fragmenttransaction.Hide(redeem);
                    fragmenttransaction.Hide(redeemdetail);
                    fragmenttransaction.Hide(inbox);
                    fragmenttransaction.Hide(currentFragment).Show(daftartukarpoint).Commit();

                }
            }
            else if (!(daftartukarpoint.IsHidden))
            {
                if (profile.IsAdded)
                {
                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(home);
                    fragmenttransaction.Hide(redeem);
                    fragmenttransaction.Hide(faq);
                    fragmenttransaction.Hide(historypoint);
                    fragmenttransaction.Hide(katalog);
                    fragmenttransaction.Hide(daftartukarpoint);
                    fragmenttransaction.Hide(redeemdetail);
                    fragmenttransaction.Hide(news);
                    fragmenttransaction.Hide(kataloddetail);
                    fragmenttransaction.Hide(inbox);
                    fragmenttransaction.Hide(currentFragment).Show(profile).Commit();
                }
                else
                {
                    FragmentManager fragmentManager2 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Add(Resource.Id.containerbody, profile);
                    fragmenttransaction.Hide(home);
                    fragmenttransaction.Hide(redeem);
                    fragmenttransaction.Hide(historypoint);
                    fragmenttransaction.Hide(katalog);
                    fragmenttransaction.Hide(faq);
                    fragmenttransaction.Hide(daftartukarpoint);
                    fragmenttransaction.Hide(redeemdetail);
                    fragmenttransaction.Hide(news);
                    fragmenttransaction.Hide(kataloddetail);
                    fragmenttransaction.Hide(inbox);
                    fragmenttransaction.Hide(currentFragment).Show(profile);
                    fragmenttransaction.CommitAllowingStateLoss();
                }
            }
            else if (!(historypoint.IsHidden))
            {
                if (home.IsAdded)
                {
                    isloaded = 1;
                    FragmentManager fragmentManager3 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction3 = fragmentManager3.BeginTransaction();
                    fragmenttransaction3.Hide(profile);
                    fragmenttransaction3.Hide(redeem);
                    fragmenttransaction3.Hide(historypoint);
                    fragmenttransaction3.Hide(katalog);
                    fragmenttransaction3.Hide(kataloddetail);
                    fragmenttransaction3.Hide(daftartukarpoint);
                    fragmenttransaction3.Hide(redeemdetail);
                    fragmenttransaction3.Hide(news);
                    fragmenttransaction3.Hide(faq);
                    fragmenttransaction3.Hide(inbox);
                    fragmenttransaction3.Hide(currentFragment).Show(home).Commit();
                    new HomeFragment.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);

                }
                else
                {
                    isloaded = 0;
                    FragmentManager fragmentManager2 = this.FragmentManager;

                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(profile);
                    fragmenttransaction.Hide(redeem);
                    fragmenttransaction.Hide(historypoint);
                    fragmenttransaction.Hide(katalog);
                    fragmenttransaction.Hide(kataloddetail);
                    fragmenttransaction.Hide(daftartukarpoint);
                    fragmenttransaction.Hide(redeemdetail);
                    fragmenttransaction.Hide(news);
                    fragmenttransaction.Hide(faq);
                    fragmenttransaction.Hide(inbox);
                    fragmenttransaction.Add(Resource.Id.containerbody, home);
                    fragmenttransaction.Show(home).Commit();
                    currentFragment = home;

                }

            }
            else if (!(inbox.IsHidden))
            {
                if (home.IsAdded)
                {
                    isloaded = 1;
                    FragmentManager fragmentManager3 = this.FragmentManager;
                    FragmentTransaction fragmenttransaction3 = fragmentManager3.BeginTransaction();
                    fragmenttransaction3.Hide(profile);
                    fragmenttransaction3.Hide(redeem);
                    fragmenttransaction3.Hide(historypoint);
                    fragmenttransaction3.Hide(katalog);
                    fragmenttransaction3.Hide(kataloddetail);
                    fragmenttransaction3.Hide(daftartukarpoint);
                    fragmenttransaction3.Hide(redeemdetail);
                    fragmenttransaction3.Hide(news);
                    fragmenttransaction3.Hide(faq);
                    fragmenttransaction3.Hide(inbox);
                    fragmenttransaction3.Hide(currentFragment).Show(home).Commit();
                    new HomeFragment.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);

                }
                else
                {
                    isloaded = 0;
                    FragmentManager fragmentManager2 = this.FragmentManager;

                    FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                    fragmenttransaction.Hide(profile);
                    fragmenttransaction.Hide(redeem);
                    fragmenttransaction.Hide(historypoint);
                    fragmenttransaction.Hide(katalog);
                    fragmenttransaction.Hide(kataloddetail);
                    fragmenttransaction.Hide(daftartukarpoint);
                    fragmenttransaction.Hide(redeemdetail);
                    fragmenttransaction.Hide(news);
                    fragmenttransaction.Hide(faq);
                    fragmenttransaction.Hide(inbox);
                    fragmenttransaction.Add(Resource.Id.containerbody, home);
                    fragmenttransaction.Show(home).Commit();
                    currentFragment = home;

                }

            }
            else
            {
                if(countback==0)
                {
                    Toast.MakeText(this, "Tekan lagi untuk keluar", ToastLength.Short).Show();
                    countback++;
                }
                else
                {
                    
                    Finish();
                }
            }
                //  base.OnBackPressed();
            }
        private void BottomNavigation_NavigationItemSelected(object sender, BottomNavigationView.NavigationItemSelectedEventArgs e)
        {

            LoadFragment(e.Item.ItemId);

        }
        void LoadFragment(int id)
        {
            currentFragment = FragmentManager.FindFragmentById(Resource.Id.containerbody);
            switch (id)
            {

                case Resource.Id.action_Home:
                    if (home.IsAdded)
                    {
                        countback = 0;
                        FragmentManager fragmentManager3 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction3 = fragmentManager3.BeginTransaction();
                        fragmenttransaction3.Hide(profile);
                        fragmenttransaction3.Hide(redeem);
                        fragmenttransaction3.Hide(historypoint);
                        fragmenttransaction3.Hide(katalog);
                        fragmenttransaction3.Hide(kataloddetail);
                        fragmenttransaction3.Hide(news);
                        fragmenttransaction3.Hide(daftartukarpoint);
                        fragmenttransaction3.Hide(redeemdetail);
                        fragmenttransaction3.Hide(faq);
                        fragmenttransaction3.Hide(inbox);
                        fragmenttransaction3.Hide(currentFragment).Show(home).Commit();
                        new HomeFragment.LoadDataForActivity1().ExecuteOnExecutor(AsyncTask.ThreadPoolExecutor);

                    }
                    else
                    {
                        countback = 0;
                        FragmentManager fragmentManager2 = this.FragmentManager;

                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Add(Resource.Id.containerbody, home);
                        fragmenttransaction.Hide(profile);
                        fragmenttransaction.Hide(faq);
                        fragmenttransaction.Hide(redeem);
                        fragmenttransaction.Hide(historypoint);
                        fragmenttransaction.Hide(katalog);
                        fragmenttransaction.Hide(kataloddetail);
                        fragmenttransaction.Hide(news);
                        fragmenttransaction.Hide(daftartukarpoint);
                        fragmenttransaction.Hide(redeemdetail);
                        fragmenttransaction.Hide(inbox);
                        fragmenttransaction.Hide(currentFragment).Show(home);
                        fragmenttransaction.CommitAllowingStateLoss();

                    }

                    break;
                case Resource.Id.action_Profile:

                    if (profile.IsAdded)
                    {
                        countback = 0;
                        FragmentManager fragmentManager2 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Hide(home);
                        fragmenttransaction.Hide(redeem);
                        fragmenttransaction.Hide(faq);
                        fragmenttransaction.Hide(historypoint);
                        fragmenttransaction.Hide(katalog);
                        fragmenttransaction.Hide(daftartukarpoint);
                        fragmenttransaction.Hide(redeemdetail);
                        fragmenttransaction.Hide(news);
                        fragmenttransaction.Hide(kataloddetail);
                        fragmenttransaction.Hide(inbox);
                        fragmenttransaction.Hide(currentFragment).Show(profile).Commit();
                    }
                    else
                    {
                        countback = 0;
                        FragmentManager fragmentManager2 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Add(Resource.Id.containerbody, profile);
                        fragmenttransaction.Hide(home);
                        fragmenttransaction.Hide(redeem);
                        fragmenttransaction.Hide(historypoint);
                        fragmenttransaction.Hide(faq);
                        fragmenttransaction.Hide(katalog);
                        fragmenttransaction.Hide(daftartukarpoint);
                        fragmenttransaction.Hide(redeemdetail);
                        fragmenttransaction.Hide(news);
                        fragmenttransaction.Hide(kataloddetail);
                        fragmenttransaction.Hide(inbox);
                        fragmenttransaction.Hide(currentFragment).Show(profile);
                        fragmenttransaction.CommitAllowingStateLoss();
                    }


                    break;
                case Resource.Id.action_Reward:

                    if (redeem.IsAdded)
                    {
                        countback = 0;
                        FragmentManager fragmentManager2 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Hide(home);
                        fragmenttransaction.Hide(profile);
                        fragmenttransaction.Hide(faq);
                        fragmenttransaction.Hide(historypoint);
                        fragmenttransaction.Hide(daftartukarpoint);
                        fragmenttransaction.Hide(katalog);
                        fragmenttransaction.Hide(redeemdetail);
                        fragmenttransaction.Hide(news);
                        fragmenttransaction.Hide(kataloddetail);
                        fragmenttransaction.Hide(inbox);
                        fragmenttransaction.Hide(currentFragment).Show(redeem).Commit();
                    }
                    else
                    {
                        countback = 0;
                        FragmentManager fragmentManager2 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Add(Resource.Id.containerbody, redeem);
                        fragmenttransaction.Hide(home);
                        fragmenttransaction.Hide(faq);
                        fragmenttransaction.Hide(profile);
                        fragmenttransaction.Hide(historypoint);
                        fragmenttransaction.Hide(daftartukarpoint);
                        fragmenttransaction.Hide(katalog);
                        fragmenttransaction.Hide(redeemdetail);
                        fragmenttransaction.Hide(news);
                        fragmenttransaction.Hide(kataloddetail);
                        fragmenttransaction.Hide(inbox);
                        fragmenttransaction.Hide(currentFragment).Show(redeem);
                        fragmenttransaction.CommitAllowingStateLoss();
                    }


                    break;
                case Resource.Id.action_Katalog:

                    if (!katalog.IsAdded)
                    {
                        countback = 0;
                        FragmentManager fragmentManager2 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Add(Resource.Id.containerbody, katalog);
                        fragmenttransaction.Hide(home);
                        fragmenttransaction.Hide(profile);
                        fragmenttransaction.Hide(daftartukarpoint);
                        fragmenttransaction.Hide(historypoint);
                        fragmenttransaction.Hide(redeem);
                        fragmenttransaction.Hide(faq);
                        fragmenttransaction.Hide(news);
                        fragmenttransaction.Hide(redeemdetail);
                        fragmenttransaction.Hide(kataloddetail);
                        fragmenttransaction.Hide(inbox);
                        fragmenttransaction.Hide(currentFragment).Show(katalog);
                        fragmenttransaction.CommitAllowingStateLoss();

                    }
                    else
                    {
                        countback = 0;
                        FragmentManager fragmentManager2 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Hide(home);
                        fragmenttransaction.Hide(profile);
                        fragmenttransaction.Hide(faq);
                        fragmenttransaction.Hide(daftartukarpoint);
                        fragmenttransaction.Hide(historypoint);
                        fragmenttransaction.Hide(redeem);
                        fragmenttransaction.Hide(news);
                        fragmenttransaction.Hide(redeemdetail);
                        fragmenttransaction.Hide(kataloddetail);
                        fragmenttransaction.Hide(inbox);
                        fragmenttransaction.Hide(currentFragment).Show(katalog).Commit();
                    }

                    break;

                case Resource.Id.action_faq:

                    if (!faq.IsAdded)
                    {
                        countback = 0;
                        FragmentManager fragmentManager2 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Add(Resource.Id.containerbody, faq);
                        fragmenttransaction.Hide(home);
                        fragmenttransaction.Hide(profile);
                        fragmenttransaction.Hide(daftartukarpoint);
                        fragmenttransaction.Hide(historypoint);
                        fragmenttransaction.Hide(redeem);
                        fragmenttransaction.Hide(katalog);
                        fragmenttransaction.Hide(news);
                        fragmenttransaction.Hide(redeemdetail);
                        fragmenttransaction.Hide(kataloddetail);
                        fragmenttransaction.Hide(inbox);
                        fragmenttransaction.Hide(currentFragment).Show(faq);
                        fragmenttransaction.CommitAllowingStateLoss();

                    }
                    else
                    {
                        countback = 0;
                        FragmentManager fragmentManager2 = this.FragmentManager;
                        FragmentTransaction fragmenttransaction = fragmentManager2.BeginTransaction();
                        fragmenttransaction.Hide(home);
                        fragmenttransaction.Hide(profile);
                        fragmenttransaction.Hide(katalog);
                        fragmenttransaction.Hide(daftartukarpoint);
                        fragmenttransaction.Hide(historypoint);
                        fragmenttransaction.Hide(redeem);
                        fragmenttransaction.Hide(news);
                        fragmenttransaction.Hide(redeemdetail);
                        fragmenttransaction.Hide(kataloddetail);
                        fragmenttransaction.Hide(inbox);
                        fragmenttransaction.Hide(currentFragment).Show(faq).Commit();
                    }

                    break;
            }

            //if (fragment != null)
            //{

            //}
        }
        private static void pushNotif()
        {
            var hubConnection = new HubConnection("http://192.168.6.232:2208/signalrservices/signalr");
            var hub = hubConnection.CreateHubProxy("MyHub");
            hubConnection.Start().Wait();
            hub.Invoke("Show");
        }
    }
}

