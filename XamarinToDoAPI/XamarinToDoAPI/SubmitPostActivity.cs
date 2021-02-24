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
    [Activity(Label = "SubmitPostActivity")]
    public class SubmitPostActivity : Activity
    {
        IMyAPI myAPI;
        string bearerToken = Constants.BearerString + MainActivity.access_token;
        Button submitPost, goBack;
        EditText desc, title;
        public override void OnBackPressed()
        {
            return;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.submit_post_layout);

            myAPI = RestService.For<IMyAPI>(Constants.ApiUrl);
            submitPost = FindViewById<Button>(Resource.Id.btn_submit_post);
            goBack = FindViewById<Button>(Resource.Id.btn_go_back);
            desc = FindViewById<EditText>(Resource.Id.txt_description);
            title = FindViewById<EditText>(Resource.Id.txt_title);

            goBack.Click += delegate
            {
                Intent intent = new Intent(this, typeof(GetUserTasksActivity));
                StartActivity(intent);
            };

            submitPost.Click += async delegate
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
                    post.desc = desc.Text;
                    post.title = title.Text;
                    PostContent result = await myAPI.SubmitTask(bearerToken, post);
                    Intent intent = new Intent(this, typeof(GetUserTasksActivity));
                    StartActivity(intent);


                    if (dialog.IsShowing)
                        dialog.Dismiss();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "" + ex.Message, ToastLength.Short).Show();
                }
            };
        }
    }
}