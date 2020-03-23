using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;

namespace CustomerPoint.FragmentAct
{
    public class faq : Fragment
    {
        private static ProgressBar spinner;
        static View itemView;
        static WebView webView;
        static string ShowOrHideWebViewInitialUse = "show";
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            itemView = LayoutInflater.From(container.Context).
                    Inflate(Resource.Layout.faq, container, false);

            spinner = itemView.FindViewById<ProgressBar>(Resource.Id.progressBar1);
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
            webView.LoadUrl("https://mitraaquaprooffaq.azurewebsites.net/");
        }

        private class CustomWebViewClient : WebViewClient
        {


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