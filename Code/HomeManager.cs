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

public class HomeManager
{
    public static HomeManager Instance;
    private List<Food> breakfast, lunch, dinner;
    private int caloriesEaten;
    private int proteinEaten;

    public List<Food> Breakfast { get => breakfast; set => breakfast = value; }
    public List<Food> Lunch { get => lunch; set => lunch = value; }
    public List<Food> Dinner { get => dinner; set => dinner = value; }
    public int CaloriesEaten { get => caloriesEaten; set => caloriesEaten = value; }
    public int ProteinEaten { get => proteinEaten; set => proteinEaten = value; }

    public HomeManager() 
    {
        breakfast = new List<Food>();
        lunch = new List<Food>();
        dinner = new List<Food>();
        caloriesEaten = 0;
        proteinEaten = 0;
        Instance = this;
    }

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





    /*
     *         public void addToBreakfast(Food thisFood, double amount)
        {
            Food newFood = new Food(thisFood, amount);
            Instance.breakfastList.Add(newFood);
            Instance.UpdateTitle();

            breakFastLinear.AddView(RenderButton(newFood.Name));

        }
        public void addToLunch(Food thisFood, double amount)
        {
            Food newFood = new Food(thisFood, amount);
            Instance.lunchList.Add(newFood);
            Instance.UpdateTitle();
            lunchLinear.AddView(RenderButton(newFood.Name));

        }
        public void addToDinner(Food thisFood, double amount)
        {
            Food newFood = new Food(thisFood, amount);
            dinnerList.Add(newFood);
            UpdateTitle();
            dinnerLinear.AddView(RenderButton(newFood.Name));
        }
     */

}
