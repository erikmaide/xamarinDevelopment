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
    [Activity(Label = "GetUserTasksActivity")]
    public class GetUserTasksActivity : Activity
    {

        IMyAPI myAPI;
        string bearerToken;
        Button getTasks, goToSubmitPost;
        ListView userTasks;
        public override void OnBackPressed()
        {
            return;
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.get_user_tasks_layout);

            myAPI = RestService.For<IMyAPI>(Constants.ApiUrl);
            getTasks = FindViewById<Button>(Resource.Id.btn_get_tasks);
            userTasks = FindViewById<ListView>(Resource.Id.my_task);
            goToSubmitPost = FindViewById<Button>(Resource.Id.btn_go_to_submit_post);

            goToSubmitPost.Click += delegate
            {
                Intent intent = new Intent(this, typeof(SubmitPostActivity));
                StartActivity(intent);
            };

                getTasks.Click += async delegate
            {
                try
                {
                    Android.Support.V7.App.AlertDialog dialog = new EDMTDialogBuilder()
                    .SetContext(this)
                    .SetMessage("Please wait ...")
                    .Build();
                  
                    getTasks.Visibility = ViewStates.Gone;
                    if (!dialog.IsShowing)
                        dialog.Show();

                    PostContent get = new PostContent();
                    bearerToken = Constants.BearerString + MainActivity.access_token;
                    List<PostContent> tasks = await myAPI.GetTasks(bearerToken);
                    List<string> task_title = new List<string>();

                    foreach (var task in tasks)
                    task_title.Add(task.title);
                    var adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, task_title);
                    userTasks.Adapter = adapter;
                    
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