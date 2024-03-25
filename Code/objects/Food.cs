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

        public Food(string name, int cal)
        {
            this.name = name;
            this.cal = cal;
        }
    }
}