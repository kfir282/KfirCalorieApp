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
    [Activity(Label = "selectamountActivity")]
    public class selectamountActivity : Activity
    {
        TextView title, amounts, error;
        EditText gramsInput;
        Button addButton;
        Food thisFood;
        string mealType;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.select_amount_layout);

            error = FindViewById<TextView>(Resource.Id.error);

            // find the food object by name
            string foodName = Intent.GetStringExtra("food");
            thisFood = foodaddActivity.GetFoodByName(foodName);
            //get the meal type
            mealType = Intent.GetStringExtra("meal");

            title = FindViewById<TextView>(Resource.Id.title);
            amounts = FindViewById<TextView>(Resource.Id.amounts);
            gramsInput = FindViewById<EditText>(Resource.Id.grams);
            addButton = FindViewById<Button>(Resource.Id.addButton);    

            title.Text = "how much " + foodName + "?";
            amounts.Text = "calories: " + thisFood.Cal + " | protein: " + thisFood.Pro;

            addButton.Text = "add " + foodName + " to " + mealType;

            gramsInput.TextChanged += GramsInput_TextChanged;
            addButton.Click += AddButton_Click;
            foodaddActivity.instanceActivity.Finish();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            error.Visibility = ViewStates.Invisible;
            EditText input = gramsInput;
            string text = input.Text;
            if(text.Length <=  0)
            {
                error.Visibility = ViewStates.Visible;
                return;
            }
            double amount = double.Parse(text);
            // 100 - 100g
            // 200 - 10g
            if(mealType == "breakfast")
            {
                User.Instance.CurrentDay.AddToBreakfast(thisFood, amount);
            }
            if(mealType == "lunch")
            {
                User.Instance.CurrentDay.AddToLunch(thisFood, amount);
            }
            if (mealType == "dinner")
            {
                User.Instance.CurrentDay.AddToDinner(thisFood, amount);
            }
            Intent intent = new Intent(this, typeof(homeActivity));
            StartActivity(intent);
        }

        private void GramsInput_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            EditText input = (EditText)sender;
            double amount = 0;
            if (input.Text.Length > 0)
            {
                amount = double.Parse(input.Text);
            }
            // 100 - 100g
            // 200 - 10g
            double calories = (amount / 100) * thisFood.Cal;
            double protein = (amount / 100) * thisFood.Pro;
            amounts.Text = "calories: " + calories + " | protein: " + protein;

        }
    }
}