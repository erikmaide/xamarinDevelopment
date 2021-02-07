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
        string access_token = MainActivity.access_token;
        string ticketToken = "Bearer ";
        Button btn_get_tasks;
        ListView userTasks;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.get_user_tasks_layout);

            myAPI = RestService.For<IMyAPI>("https://demo2.z-bit.ee");
            btn_get_tasks = FindViewById<Button>(Resource.Id.btn_get_tasks);
            userTasks = FindViewById<ListView>(Resource.Id.my_task);

            btn_get_tasks.Click += async delegate
            {
                try
                {
                    Android.Support.V7.App.AlertDialog dialog = new EDMTDialogBuilder()
                    .SetContext(this)
                    .SetMessage("Please wait ...")
                    .Build();

                    if (!dialog.IsShowing)
                        dialog.Show();

                    PostContent get = new PostContent();
                    ticketToken += access_token;
                    List<PostContent> tasks = await myAPI.GetTasks(ticketToken);
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