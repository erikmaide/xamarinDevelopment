using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using FirstApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
namespace FirstApp
{
    [Activity(Label = "CarSpecActivity")]
    public class CarSpecActivity : Activity
    {
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
            SetContentView(Resource.Layout.car_spec_layout);
            // Create your application here
            var textView = FindViewById<TextView>(Resource.Id.textView1);
            
            int imageNr = Convert.ToInt32(Intent.GetStringExtra("carImage"));
            FindViewById<ImageView>(Resource.Id.carImageView).SetImageResource(imageNr);
        }
    }
}