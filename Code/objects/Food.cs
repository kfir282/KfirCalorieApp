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
    public class Food
    {


        private string name;
        private int cal;
        private int pro;

        public Food(string name, int cal, int pro)
        {
            this.name = name;
            this.cal = cal;
            this.pro = pro;
        }
        public Food(Food other, double amount)
        {
            this.name = other.name;
            this.cal = (int)((amount / 100) * other.Cal);
            this.pro = (int)((amount / 100) * other.Pro);

        }

        public string Name { get => name; set => name = value; }
        public int Cal { get => cal; set => cal = value; }
        public int Pro { get => pro; set => pro = value; }
    }
}