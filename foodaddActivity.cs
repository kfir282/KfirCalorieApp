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

        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            SetContentView(Resource.Layout.addfood_layout);

            title = FindViewById<TextView>(Resource.Id.title);
            createFoodButton = FindViewById<Button>(Resource.Id.createfoodButton);


            string where = Intent.GetStringExtra("meal");
            title.Text = title.Text + " " + where;
            
            // TODO: get all foods from database
            if(allFoods.Count == 0)
            {
                allFoods = await FireStoreHelper.GetAllFoods();
            }
            
            GridView grid = FindViewById<GridView>(Resource.Id.gridView);
            GridAdapter adapter = new GridAdapter(this, allFoods);
            grid.Adapter = adapter;
/*            grid.OnItemClickListener = new EventHandler<AdapterView.ItemClickEventArgs>((sender, e) =>
            {
                // Handle item click here
                int position = e.Position;
                // You can get the item from the adapter using the position
                // For example:
                // var clickedItem = adapter.GetItem(position);
                // Do something with the clicked item
            });*/
            createFoodButton.Click += delegate
            {
                Intent create = new Intent(this, typeof(customfoodActivity));
                StartActivity(create);
            };

        }
    }

    class GridAdapter : BaseAdapter<Food>
    {
        public static GridAdapter Instance; 
        private readonly List<Food> items;
        private readonly Context context;

        public GridAdapter(Context context, List<Food> items)
        {
            this.context = context;
            this.items = items;
            Instance = this;
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
                thisButton = new Button(context)
                {
                    LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.MatchParent),
                   
                       
                };
                thisButton.SetTextColor(Color.Black);
                thisButton.SetPadding(16, 16, 16, 16);
                thisButton.SetWidth(300);
                thisButton.SetHeight(300);
            }
            thisButton.Text = items[position].Name;
            thisButton.Click += delegate
            {
                // TODO: add food selection (grams)
                // and after selection add to meal
                // and after adding to meal add to total calories
            };

            return thisButton;
        }
    }



}