using Android.App;
using Android.Content;
using Android.Graphics;
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
    [Activity(Label = "foodaddActivity")]
    public class foodaddActivity : Activity
    {
        Button createFoodButton;
        TextView title;
        ListView foodListView;

        public static List<Food> allFoods = new List<Food>();
        public static string whatMeal;
        public static foodaddActivity InstanceActivity;
        public static string debugValue;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
          
            SetContentView(Resource.Layout.addfood_layout);

            title = FindViewById<TextView>(Resource.Id.title);
            createFoodButton = FindViewById<Button>(Resource.Id.createfoodButton);


            whatMeal = Intent.GetStringExtra("meal");
            title.Text = title.Text + " " + whatMeal;
            
            foodaddActivity.InstanceActivity = this;
            
            if(allFoods.Count == 0)
            {
                allFoods = await FireStoreHelper.GetAllFoods();
            }

            // SHOW TO USER

            GridView grid = FindViewById<GridView>(Resource.Id.gridView);
            

            GridAdapter adapter = new GridAdapter(allFoods);
            
            GridAdapter.SetInstance(adapter);

            grid.Adapter = adapter;

            createFoodButton.Click += delegate
            {
                Intent create = new Intent(this, typeof(customfoodActivity));
                StartActivity(create);
            };
            debugValue = "finished oncreate";
        }
    

        public static Food GetFoodByName(string name)
        {
            foreach(Food food in allFoods)
            {
                if(food.Name == name) return food;
            }
            return null;
        }
/*        public void StartSelect(object sender, EventArgs e, string foodName)
        {
            Intent intent = new Intent(this, typeof(foodaddActivity));
            intent.PutExtra("food", foodName);
            intent.PutExtra("meal", whatMeal);
            StartActivity(intent);
            Finish();
            Dispose();

        }*/

        internal void StartSelect(object sender, EventArgs e)
        {
            Console.WriteLine(debugValue);
            Button b = (Button)sender;
            string text = b.Text;
            Intent intent = new Intent(InstanceActivity, typeof(selectamountActivity));
            intent.PutExtra("food", text);
            intent.PutExtra("meal", whatMeal);
            StartActivity(intent);
            Finish();
            Dispose();
        }
    }
    class GridAdapter : BaseAdapter<Food>
    {
        public static GridAdapter Instance; 

        private List<Food> items;

        public GridAdapter(List<Food> items)
        {
            this.items = items;
        }
        public static void SetInstance(GridAdapter thisInstance)
        {
            GridAdapter.Instance = thisInstance;
        }

        public override int Count => items.Count;


        public override Food this[int position] => items[position];

        public override long GetItemId(int position) => position;

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            // convert  view is for memory. if it is not null, we can use it. if it is - we need to create a new one.
            Button thisButton;
            if (convertView != null)
            {
                thisButton = (Button) convertView;
            }
            else
            {
                thisButton = new Button(foodaddActivity.InstanceActivity)
                {
                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent),
                   
                       
                };
                thisButton.SetTextColor(Color.Black);
                thisButton.SetPadding(16, 16, 16, 16);
                thisButton.SetWidth(300);
                thisButton.SetHeight(300);
            }
            thisButton.Text = items[position].Name;
            thisButton.Click += foodaddActivity.InstanceActivity.StartSelect;
            // TODO: add food selection (grams)
            // and after selection add to meal
            // and after adding to meal add to total calories
            return thisButton;
        }
    }



}