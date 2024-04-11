using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Icu.Text.CaseMap;

namespace KfirCalorieCounterReal.objects
{
    public class Day
    {
        private string title;
        private List<Food> breakfast;
        private List<Food> lunch;
        private List<Food> dinner;
        private int caloriesEaten;
        private int proteinEaten;

        

        public Day()
        {
            this.breakfast = new List<Food>();
            this.lunch = new List<Food>();
            this.dinner = new List<Food>();
            caloriesEaten = 0;
            proteinEaten = 0;

        }

        public Day(string title, List<Food> breakfast, List<Food> lunch, List<Food> dinner, int caloriesEaten, int proteinEaten)
        {
            this.title = title;
            this.breakfast = breakfast;
            this.lunch = lunch;
            this.dinner = dinner;
            this.caloriesEaten = caloriesEaten;
            this.proteinEaten = proteinEaten;
        }

        public string Title { get => title; set => title = value; }

        public List<Food> Breakfast { get => breakfast; set => breakfast = value; }
        public List<Food> Lunch { get => lunch; set => lunch = value; }
        public List<Food> Dinner { get => dinner; set => dinner = value; }

        public int CaloriesEaten { get => caloriesEaten; set => caloriesEaten = value; }
        public int ProteinEaten { get => proteinEaten; set => proteinEaten = value; }
        public void AddToBreakfast(Food food, double amount)
        {
            Food newFood = new Food(food, amount);
            breakfast.Add(newFood);
            caloriesEaten += newFood.Cal;
            proteinEaten += newFood.Pro;
        }
        public void AddToLunch(Food food, double amount)
        {
            Food newFood = new Food(food, amount);
            lunch.Add(newFood);
            caloriesEaten += newFood.Cal;
            proteinEaten += newFood.Pro;
        }
        public void AddToDinner(Food food, double amount)
        {
            Food newFood = new Food(food, amount);
            dinner.Add(newFood);
            caloriesEaten += newFood.Cal;
            proteinEaten += newFood.Pro;
        }
    }
}