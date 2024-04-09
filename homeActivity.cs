using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KfirCalorieCounterReal.objects;
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

        public static homeActivity Instance;

        public List<Food> breakfastList, lunchList , dinnerList;

        public int totalCalories, totalProtein;
        public int calorieGoal;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.home_layout);
            Instance = this;
            values = FindViewById<TextView>(Resource.Id.values);
            
            breakfastList = new List<Food>();
            lunchList = new List<Food>();
            dinnerList = new List<Food>();

            breakFastButton = FindViewById<Button>(Resource.Id.brakefastButton);
            lunchButton = FindViewById<Button>(Resource.Id.lunchButton);
            dinnerButton = FindViewById<Button>(Resource.Id.dinnerButton);

            breakFastScroll = FindViewById<HorizontalScrollView>(Resource.Id.breakfastScroll);
            lunchFastScroll = FindViewById<HorizontalScrollView>(Resource.Id.lunchScroll);
            dinnerFastScroll = FindViewById<HorizontalScrollView>(Resource.Id.dinnerScroll);
         
            breakFastButton.Click += BreakFastButton_Click;
            lunchButton.Click += LunchButton_Click;
            dinnerButton.Click += DinnerButton_Click;

            totalCalories = 0;
            totalProtein = 0;
            int goal = User.thisUser.CalorieGoal;
            changeTitle();
        }

        public void changeTitle()
        {
            int left = Instance.calorieGoal - Instance.totalCalories;
            Instance.values.Text = string.Format("{0} - {1} = {2}", Instance.calorieGoal, Instance.totalCalories, left);
        }


        private void DinnerButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(foodaddActivity));
            intent.PutExtra("meal", "dinner");
            StartActivity(intent);
        }

        private void LunchButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(foodaddActivity));
            intent.PutExtra("meal", "lunch");
            StartActivity(intent);
        }

        private void BreakFastButton_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(foodaddActivity));
            intent.PutExtra("meal", "breakfast");
            StartActivity(intent);
        }


        public static void addToBreakfast(Food thisFood, double amount)
        {
            Food newFood = new Food(thisFood, amount);
            Instance.breakfastList.Add(newFood);
            Instance.changeTitle();
        }
        public static void addToLunch(Food thisFood, double amount)
        {
            Food newFood = new Food(thisFood, amount);
            Instance.lunchList.Add(newFood);
            Instance.changeTitle();

        }
        public static void addToDinner(Food thisFood, double amount)
        {
            Food newFood = new Food(thisFood, amount);
            Instance.dinnerList.Add(newFood);
            Instance.changeTitle();

        }
    }
}