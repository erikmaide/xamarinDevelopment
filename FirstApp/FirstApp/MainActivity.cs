using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using Android.Content;
using Xamarin.Essentials;
using System;
using System.Threading.Tasks;

namespace FirstApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var textView = FindViewById<TextView>(Resource.Id.textView1);
            var counterView = FindViewById<TextView>(Resource.Id.textView2);
            var button = FindViewById<Button>(Resource.Id.button1);
            var vibrationControl = FindViewById<Button>(Resource.Id.vibrate_btn);
            var webButton = FindViewById<Button>(Resource.Id.toWebActivity);
            int counter = 0;
            var toCalculatorButton = FindViewById<Button>(Resource.Id.toCalculator);
            var toListButton = FindViewById<Button>(Resource.Id.toListButton);


            vibrationControl.Click += delegate
            {
                Vibration.Vibrate(5000);
            };


            toListButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(SampleListActivity));
                StartActivity(intent);
            };

            button.Click += async delegate
            {
                textView.Text = "Hello Xamarian";

                counter += 1;

                counterView.Text = counter.ToString();

                if (counter % 2 == 0)
                {
                    await Flashlight.TurnOnAsync();
                }
                else
                    await Flashlight.TurnOffAsync();
            };

            toCalculatorButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(CalculatorActivity));
                StartActivity(intent);
            };
            webButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(WebActivity));
                intent.PutExtra("address", "https://www.neti.ee");
                StartActivity(intent);
            };


        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}