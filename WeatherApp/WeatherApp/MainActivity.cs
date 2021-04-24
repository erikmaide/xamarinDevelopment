using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using WeatherApp.Services;
using System.Linq;
using System;

namespace WeatherApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            var dataService = new RemoteDataService();

            var cityEditText = FindViewById<EditText>(Resource.Id.cityTextView);
            var searchButton = FindViewById<Button>(Resource.Id.searchButton);
            var tempTextView = FindViewById<TextView>(Resource.Id.tempTextView);
            var weatherImage = FindViewById<ImageView>(Resource.Id.weatherImage);
            ListView weatherInfo = FindViewById<ListView>(Resource.Id.weather_TextView);
           

            searchButton.Click += async delegate

            {
                string[] temperatures = { };

                var data = await dataService.GetCityWeather(cityEditText.Text);

                tempTextView.Text = data.list[0].main.temp.ToString() + "°C";

                for (var i = 1; i<=39; i++)
                {
                    var temp = data.list[i].main.temp.ToString() + "°C";
                    var dateTime = data.list[i].dt_txt.ToString();
                    temp = dateTime +"                 " + temp;
                    temperatures = temperatures.Concat(new string[] { temp }).ToArray();
                }
              
              
                var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, temperatures);
                weatherInfo.Adapter = adapter;


                // tempTextView.Text = $"{data.main.temp.ToString()} C";


                 using (var bm = await dataService.GetImageFromUrl($"https://openweathermap.org/img/wn/{data.list[0].weather[0].icon}@2x.png"))
                   weatherImage.SetImageBitmap(bm);
            };
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}