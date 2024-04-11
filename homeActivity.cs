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

namespace KfirCalorieCounterReal
{
    [Activity(Label = "homeActivity")]
    public class homeActivity : Activity
    {
        TextView values;
        Button breakFastButton, lunchButton, dinnerButton;
        HorizontalScrollView breakFastScroll, lunchFastScroll, dinnerFastScroll;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.home_layout);
            
            values = FindViewById<TextView>(Resource.Id.values);
            
            breakFastButton = FindViewById<Button>(Resource.Id.brakefastButton);
            lunchButton = FindViewById<Button>(Resource.Id.lunchButton);
            dinnerButton = FindViewById<Button>(Resource.Id.dinnerButton);

            breakFastScroll = FindViewById<HorizontalScrollView>(Resource.Id.breakfastScroll);
            lunchFastScroll = FindViewById<HorizontalScrollView>(Resource.Id.lunchScroll);
            dinnerFastScroll = FindViewById<HorizontalScrollView>(Resource.Id.dinnerScroll);

            breakFastButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(foodaddActivity));
                intent.PutExtra("meal", "breakfast");
                StartActivity(intent);
            };
            lunchButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(foodaddActivity));
                intent.PutExtra("meal", "lunch");
                StartActivity(intent);
            };
            dinnerButton.Click += delegate
            {
                Intent intent = new Intent(this, typeof(foodaddActivity));
                intent.PutExtra("meal", "dinner");
                StartActivity(intent);
            };



        }
    }
}