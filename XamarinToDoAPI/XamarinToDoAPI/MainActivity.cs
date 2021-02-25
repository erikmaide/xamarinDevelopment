using Android.App;
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
using Xamarin.Essentials;

namespace XamarinToDoAPI
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        IMyAPI myAPI;
        public static string access_token;
        public static string user;
        public static string password;
        Button login;
        EditText txt_user, txt_password;
       private void Accelerometer_ShakeDetected(object sender, EventArgs e)
        {
            MainThread.BeginInvokeOnMainThread(() =>
            {
                txt_user.Text = string.Empty;
                txt_password.Text = string.Empty;
            });
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            myAPI = RestService.For<IMyAPI>(Constants.ApiUrl);
            login = FindViewById<Button>(Resource.Id.btn_get_data);
            txt_user = FindViewById<EditText>(Resource.Id.txt_user);
            txt_password = FindViewById<EditText>(Resource.Id.txt_password);
            Accelerometer.ShakeDetected += Accelerometer_ShakeDetected;
            Accelerometer.Start(SensorSpeed.Game);

            login.Click += async delegate
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
                     PostContent result = await myAPI.Login(post);
                     access_token = result.access_token;
                     if (dialog.IsShowing)
                         dialog.Dismiss();
                     Intent intent = new Intent(this, typeof(GetUserTasksActivity));
                     StartActivity(intent);
                 }
                 catch (Exception ex)
                 {
                     Toast.MakeText(this, "Wrong password" + ex, ToastLength.Short).Show();
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