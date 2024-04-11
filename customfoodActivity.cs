using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using KfirCalorieCounterReal.Code;
using KfirCalorieCounterReal.objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KfirCalorieCounterReal
{
    [Activity(Label = "customfActivity")]
    public class customfoodActivity : Activity
    {
        TextView error;
        EditText nameInput, caloriesInput, proteinInput;
        Button addFood;
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.customfood_layout);


            error = FindViewById<TextView>(Resource.Id.error);
            nameInput = FindViewById<EditText>(Resource.Id.nameInput);
            caloriesInput = FindViewById<EditText>(Resource.Id.caloriesInput);
            proteinInput = FindViewById<EditText>(Resource.Id.proteinInput);

            addFood = FindViewById<Button>(Resource.Id.addButton);
            addFood.Click += AddFood_Click;

        }

        private void AddFood_Click(object sender, EventArgs e)
        {
            error.Visibility = ViewStates.Invisible;
            string name = nameInput.Text;
            string calories = caloriesInput.Text;
            string protein = proteinInput.Text; 
            if(name.Length == 0 || calories.Length == 0 || protein.Length == 0) 
            { 
                error.Visibility = ViewStates.Visible;
                return;
            }
            Food thisFood = new Food(name, int.Parse(calories), int.Parse(protein));
            foodaddActivity.allFoods.Add(thisFood);
            FireStoreHelper.SaveFood(thisFood);
            GridAdapter.Instance.NotifyDataSetChanged();
            Finish();
        }
    }
}