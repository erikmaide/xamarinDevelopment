using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinToDoAPI.Model;


namespace XamarinToDoAPI.Interface
{
    public interface IMyAPI
    {
        [Post("/users/get-token")]
        Task<PostContent> Login([Body] PostContent post);

        [Get("/tasks")]
        Task<List<PostContent>> GetTasks([Header("Authorization")] string authorization);

        [Post("/tasks")]
        Task<PostContent> SubmitTask([Header("Authorization")] string authorization, [Body] PostContent post);
    }
}