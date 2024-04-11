using Android.App;
using Android.Content;
using Android.Nfc;
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
        Button breakFastButton, lunchButton, dinnerButton, saveDayButton;
        LinearLayout breakFastLinear, lunchLinear, dinnerLinear;
        GridLayout daysGrid;
        public static homeActivity Instance;

        public List<Food> breakfastList, lunchList, dinnerList;
        public List<Day> savedDays;
        public int totalCalories, totalProtein;
        public int calorieGoal;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.home_layout);
            InitValues();
            UpdateFromManager();
        }
        public void InitValues()
        {
            Instance = this;
            daysGrid = FindViewById<GridLayout>(Resource.Id.daysGrid);
            values = FindViewById<TextView>(Resource.Id.values);
            saveDayButton = FindViewById<Button>(Resource.Id.save);
            breakfastList = new List<Food>();
            lunchList = new List<Food>();
            dinnerList = new List<Food>();

            breakFastButton = FindViewById<Button>(Resource.Id.brakefastButton);
            lunchButton = FindViewById<Button>(Resource.Id.lunchButton);
            dinnerButton = FindViewById<Button>(Resource.Id.dinnerButton);

            breakFastLinear = FindViewById<LinearLayout>(Resource.Id.breakfastLinear);
            lunchLinear = FindViewById<LinearLayout>(Resource.Id.lunchLinear);
            dinnerLinear = FindViewById<LinearLayout>(Resource.Id.dinnerLinear);

            breakFastButton.Click += BreakFastButton_Click;
            lunchButton.Click += LunchButton_Click;
            dinnerButton.Click += DinnerButton_Click;

            totalCalories = 0;
            totalProtein = 0;
            calorieGoal = User.Instance.CalorieGoal;
            saveDayButton.Click += SaveDayButton_Click;
        }

        private void SaveDayButton_Click(object sender, EventArgs e)
        {
            //prompt for name
            int daysAmount = User.Instance.Saved.Count;

            string dayTitle = "day number " + (daysAmount + 1);
            User.Instance.CurrentDay.Title = dayTitle;
            User.Instance.SaveCurrentDay();
            Intent newIntent = new Intent (this, typeof(homeActivity));
            StartActivity(newIntent);
        }

        public void UpdateFromManager()
        {
            breakfastList = User.Instance.CurrentDay.Breakfast;
            lunchList = User.Instance.CurrentDay.Lunch;
            dinnerList = User.Instance.CurrentDay.Dinner;
            totalCalories = User.Instance.CurrentDay.CaloriesEaten;
            totalProtein = User.Instance.CurrentDay.ProteinEaten;
            savedDays = User.Instance.Saved;
            UpdateScrolls();
            UpdateTitle();
            UpdateSavedDays();
        }

        private void UpdateSavedDays()
        {
            /*
             *         <Button
            android:text="Button1"
            android:layout_margin="20px"

            android:minWidth="25px"
            android:minHeight="25px"
            android:id="@+id/bu124tton1"
            android:layout_width="wrap_content"
            android:layout_height="wrap_content" />
             */
            foreach (Day day in savedDays)
            {
                Button thisDayButton = new Button(this)
                {
                    Text = day.Title,
                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent)
                };
                thisDayButton.SetPadding(20,20,20,20);
                thisDayButton.Click += DayButton_Click;
                daysGrid.AddView(thisDayButton);
            }
        }

        private void DayButton_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string dayName = button.Text;
            Day thisDay = User.Instance.GetDay(dayName);
            User.Instance.CurrentDay = thisDay;
            Intent newIntent = new Intent(this, typeof(homeActivity));
            StartActivity(newIntent);
        }

        private void UpdateScrolls()
        {
            foreach (Food item in breakfastList)
            {
                breakFastLinear.AddView(RenderButton(item.Name));
            }
            foreach (Food item in lunchList)
            {
                lunchLinear.AddView(RenderButton(item.Name));
            }
            foreach (Food item in dinnerList)
            {
                dinnerLinear.AddView(RenderButton(item.Name));
            }
        }

        public void UpdateTitle()
        {
            int left = calorieGoal - totalCalories;
            string text = string.Format(
                "Calorie Goal: {0}\nCalories Eaten: {1}\nCalories Left: {2}\nProtein Eaten: {3}",
                calorieGoal, totalCalories, (calorieGoal - totalCalories), totalProtein
                );;
            values.Text = text;
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


        public static Button RenderDayButton(Day day)
        {

            return null;
        }

        public static Button RenderButton(string foodName)
        {
            /*
             *         <Button
            android:text="תרנגול ב50 שח"
            android:layout_width="match_parent"
            android:layout_height="match_parent"
            android:minWidth="25px"
            android:minHeight="25px"
            android:layout_marginLeft="30px"
            android:id="@+id/button1" />

             */
            Button b = new Button(Instance)
            {
                Text = foodName,
                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.MatchParent, ViewGroup.LayoutParams.MatchParent),
            };
            b.SetPadding(30, 0, 0, 0);
            b.Tag = foodName;
            return b;
        }
    }
}