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

        public string Name { get => name; set => name = value; }
        public int Cal { get => cal; set => cal = value; }
        public int Pro { get => pro; set => pro = value; }
    }
}