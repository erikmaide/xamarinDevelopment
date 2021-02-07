﻿using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Runtime;
using Android.Widget;
using System;
using XamarinToDoAPI.Interface;
using Refit;
using EDMTDialog;
using XamarinToDoAPI.Model;
using Android.Content;

namespace XamarinToDoAPI
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        IMyAPI myAPI;
        public static string access_token;
        public static string user;
        public static string password;
        Button btn_get_data;
        TextView txt_user, txt_password;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            myAPI = RestService.For<IMyAPI>("https://demo2.z-bit.ee");
            btn_get_data = FindViewById<Button>(Resource.Id.btn_get_data);
            txt_user = FindViewById<TextView>(Resource.Id.txt_user);
            txt_password = FindViewById<TextView>(Resource.Id.txt_password);

            btn_get_data.Click += async delegate
             {
                 try
                 {
                     Android.Support.V7.App.AlertDialog dialog = new EDMTDialogBuilder()
                     .SetContext(this)
                     .SetMessage("Please wait ...")
                     .Build();
                     if (!dialog.IsShowing)
                         dialog.Show();

                     PostContent post = new PostContent();
                     post.username = txt_user.Text;              
                     post.password = txt_password.Text;
                     PostContent result = await myAPI.SubmitPost(post);
                     access_token = result.access_token;
                     if (dialog.IsShowing)
                         dialog.Dismiss();
                     Intent intent = new Intent(this, typeof(GetUserTasksActivity));
                     StartActivity(intent);
                 }
                 catch (Exception ex)
                 {
                     Toast.MakeText(this, "" + ex.Message, ToastLength.Short).Show();
                 }
             };

        }

 
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}