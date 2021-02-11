using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstApp
{
    [Activity(Label = "WebActivity")]
    public class WebActivity : Activity
    {
        WebView _webView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.webLayout);
            // Create your application here

            var aadress = Intent.GetStringExtra("address");
            _webView = FindViewById<WebView>(Resource.Id.webView1);
            _webView.Settings.JavaScriptEnabled = true;
            _webView.SetWebViewClient(new SimpleWebViewClient());
            _webView.LoadUrl(aadress);

            var addressField= FindViewById<EditText>(Resource.Id.addressField); ;
            var GoButton = FindViewById<Button>(Resource.Id.GoButton);

            GoButton.Click += delegate
                {
                    string goToAddress;
                    goToAddress = addressField.Text;
                    Intent intent = new Intent(this, typeof(WebActivity));
                    intent.PutExtra("address", goToAddress);
                    StartActivity(intent);
                };

        }

        public override bool OnKeyDown([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if(keyCode == Keycode.Back && _webView.CanGoBack())
            {
                _webView.GoBack();
                return true;
            }

            return base.OnKeyDown(keyCode, e);
        }

    }
}