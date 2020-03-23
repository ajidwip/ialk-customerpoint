using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Net.Http;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace CustomerPoint.FragmentAct
{
    public class news : Fragment
    {
        static ISharedPreferences sharedPreferences;
        private static ProgressBar spinner;
        static string ShowOrHideWebViewInitialUse = "show";
       static WebView webView;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View itemView = LayoutInflater.From(container.Context).
                     Inflate(Resource.Layout.news, container, false);
            sharedPreferences = Activity.GetSharedPreferences("sharedprefrences", 0);

            spinner =itemView.FindViewById< ProgressBar>(Resource.Id.progressBar1);
            webView = itemView.FindViewById<WebView>(Resource.Id.webview);
            webView.SetWebViewClient(new CustomWebViewClient());

            webView.Settings.JavaScriptEnabled = true;
            webView.Settings.BuiltInZoomControls = false;
            webView.Settings.SetSupportZoom(false);
            webView.ScrollBarStyle = ScrollbarStyles.OutsideOverlay;
            webView.ScrollbarFadingEnabled = false;
            view();
            return itemView;
        }
        public static void view()
        {
            string urlnews = sharedPreferences.GetString("url", null);
            webView.LoadUrl(urlnews);
        }
        private class CustomWebViewClient : WebViewClient
        {

            public override void OnReceivedSslError(WebView view, SslErrorHandler handler, SslError error)
            {
                handler.Proceed();
            }
            public override void OnPageStarted(WebView webview, string url, Bitmap favicon)
            {

                // only make it invisible the FIRST time the app is run
                if (ShowOrHideWebViewInitialUse == "show")
                {
                    webview.Visibility = ViewStates.Invisible;
                }
            }


            public override void OnPageFinished(WebView view, string url)
            {

                ShowOrHideWebViewInitialUse = "hide";
                spinner.Visibility = ViewStates.Gone;

                view.Visibility = ViewStates.Visible;
                base.OnPageFinished(view, url);

            }
        }
    }
}