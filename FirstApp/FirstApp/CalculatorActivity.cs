using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FirstApp
{
    [Activity(Label = "CalculatorActivity")]
    public class CalculatorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            SetContentView(Resource.Layout.calculator_layout);

            var calculate = FindViewById<Button>(Resource.Id.calculateButton);
            var answer = FindViewById<TextView>(Resource.Id.answer);
            var num1 = FindViewById<EditText>(Resource.Id.firstField);
            var num2 = FindViewById<EditText>(Resource.Id.secondField);
            var calculatorReturn = FindViewById<Button>(Resource.Id.returnButton);

            calculate.Click += delegate
            {
                answer.Text = (int.Parse(num1.Text) + int.Parse(num2.Text)).ToString();

            };
            calculatorReturn.Click += delegate
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);

            };
        }
    }
}