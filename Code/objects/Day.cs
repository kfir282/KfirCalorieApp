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
using static Android.Icu.Text.CaseMap;

namespace KfirCalorieCounterReal.objects
{
    public class Day
    {
        private List<Food> brakefast;
        private List<Food> lunch;
        private List<Food> supper;

        public Day()
        {
            this.brakefast = new List<Food>();
            this.lunch = new List<Food>();
            this.supper = new List<Food>();
        }
    }
}