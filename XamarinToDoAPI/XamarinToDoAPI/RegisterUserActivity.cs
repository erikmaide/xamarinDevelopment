using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using EDMTDialog;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XamarinToDoAPI.Interface;
using XamarinToDoAPI.Model;

namespace XamarinToDoAPI
{
    [Activity(Label = "RegisterUserActivity")]
    public class RegisterUserActivity : Activity
    {
        IMyAPI myAPI;
        public static string user, newPassword, firstname, lastname;
        Button register;
        EditText txt_user, txt_password, txt_firstname, txt_lastname;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register_user_layout);
            myAPI = RestService.For<IMyAPI>(Constants.ApiUrl);
            txt_user = FindViewById<EditText>(Resource.Id.txt_user);
            txt_password = FindViewById<EditText>(Resource.Id.txt_password);
            txt_firstname = FindViewById<EditText>(Resource.Id.txt_firstname);
            txt_lastname = FindViewById<EditText>(Resource.Id.txt_lastname);
            register = FindViewById<Button>(Resource.Id.btn_register);

            register.Click += async delegate
            {
                try
                {
                    Android.Support.V7.App.AlertDialog dialog = new EDMTDialogBuilder()
                    .SetContext(this)
                    .SetMessage("Please wait ...")
                    .Build();
                    if (!dialog.IsShowing)
                        dialog.Show();

                    PostContent register = new PostContent();
                    register.username = txt_user.Text;
                    register.newPassword = txt_password.Text;
                    register.firstname = txt_firstname.Text;
                    register.lastname = txt_lastname.Text;
                    PostContent result = await myAPI.RegisterUser(register);
                    if (dialog.IsShowing)
                        dialog.Dismiss();
                    Intent intent = new Intent(this, typeof(MainActivity));
                    StartActivity(intent);
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "Wrong password" + ex, ToastLength.Short).Show();
                }
            };
        }
    }
}