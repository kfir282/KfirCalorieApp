using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Firestore.Core;
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
        public static foodaddActivity instanceActivity;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.addfood_layout);
            instanceActivity = this;
            title = FindViewById<TextView>(Resource.Id.title);
            createFoodButton = FindViewById<Button>(Resource.Id.createfoodButton);


            whatMeal = Intent.GetStringExtra("meal");
            title.Text = title.Text + " " + whatMeal;



            if (allFoods.Count == 0)
            {
                allFoods = await FireStoreHelper.GetAllFoods();
            }

            // SHOW TO USER

            GridView grid = FindViewById<GridView>(Resource.Id.gridView);

            GridAdapter adapter = new GridAdapter(allFoods, this, grid);

            GridAdapter.SetInstance(adapter);

            grid.Adapter = adapter;

            createFoodButton.Click += delegate
            {
                Intent create = new Intent(this, typeof(customfoodActivity));
                StartActivity(create);
            };


        }

        public static Food GetFoodByName(string name)
        {
            foreach (Food food in allFoods)
            {
                if (food.Name == name) return food;
            }
            return null;
        }
    }
    class GridAdapter : BaseAdapter<Food>
    {
        public static GridAdapter Instance;

        private List<Food> items;
        private foodaddActivity activity;
        private GridView gridView;

        public GridAdapter(List<Food> items, foodaddActivity thisActivity, GridView gridView)
        {
            this.items = items;
            this.activity = thisActivity;

            this.gridView = gridView;

            // Set item click listener for the grid view

        }
        public static void SetInstance(GridAdapter thisInstance)
        {
            GridAdapter.Instance = thisInstance;
        }

        public override int Count => items.Count;


        public override Food this[int position] => items[position];

        public override long GetItemId(int position) => position;

        public override Android.Views.View GetView(int position, Android.Views.View convertView, ViewGroup parent)
        {
            Button thisButton;

            if (convertView != null)
            {
                thisButton = (Button)convertView;
            }
            else
            {
                thisButton = new Button(activity)
                {
                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent),
                };
                thisButton.SetTextColor(Color.Black);
                thisButton.SetPadding(16, 16, 16, 16);
                thisButton.SetWidth(300);
                thisButton.SetHeight(300);
            }
            thisButton.Click += OnItemClick;
            thisButton.Text = items[position].Name;
            return thisButton;
        }

        private void OnItemClick(object sender, EventArgs e)
        {
            if(activity == null)
            {
                return;
            }
            try
            {
                Button thisButton = (Button)sender;
                string thisFoodName = thisButton.Text;
                string thisMealType = foodaddActivity.whatMeal;

                Intent intent = new Intent(activity, typeof(selectamountActivity));
                intent.PutExtra("food", thisFoodName);
                intent.PutExtra("meal", thisMealType);

                activity.StartActivity(intent);
            }
            catch (Exception ex)
            {

                return;
            }

        }

    }



}