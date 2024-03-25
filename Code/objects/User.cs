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
        private Day currentDay;
        private List<Day> saved; 

        public User(string name, string email, string password)
        {
            this.name = name;
            this.email = email;
            this.password = password;
            this.currentDay = new Day();
            this.saved = new List<Day>();
        }

        public string GetPassword() {  return this.password; }
        public string GetName() { return this.name; }
        public string GetEmail() { return this.email; }

    }
}