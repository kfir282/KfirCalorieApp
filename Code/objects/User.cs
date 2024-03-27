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

namespace KfirCalorieCounterReal.objects
{
    public class User
    {
        private string name;
        private string email;
        private string password;
        private int calorieGoal;
        private Day currentDay;
        private List<Day> saved;
        public static User thisUser;

        public User(string name, string email, string password, int calorieGoal)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            this.currentDay = new Day();
            this.saved = new List<Day>();
            this.calorieGoal = calorieGoal;
        }

        public string Name { get => name; set => name = value; }
        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public int CalorieGoal { get => calorieGoal; set => calorieGoal = value; }
        public Day CurrentDay { get => currentDay; set => currentDay = value; }
        public List<Day> Saved { get => saved; set => saved = value; }
    }
}