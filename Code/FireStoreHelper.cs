using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Firebase.Firestore;
using Java.Util;
using Java.Lang;
using static Android.Provider.MediaStore.Audio;
using System.Threading.Tasks;
using Android.Gms.Tasks;
using Java.Interop;
using System;
using Firebase;
using JavaObjectExtensions = Java.Interop.JavaObjectExtensions;
using Java.Util.Functions;
using static Android.Media.TV.TvContract.Programs;
using static Java.Util.Jar.Attributes;
using KfirCalorieCounterReal.objects;
using Android.Gms.Extensions;
using Newtonsoft.Json;


namespace KfirCalorieCounterReal.Code
{
    public class FireStoreHelper
    {
        public static FirebaseFirestore DataBase { get; private set; }

        public static async Task<User> GetUser(string email)
        {
            //test@gmail.com
            Init();

            DocumentReference thisDoc = DataBase.Collection("users").Document(email);

            OnCompleteListener completeListener = new OnCompleteListener(); // put the box in this box

            thisDoc.Get().AddOnCompleteListener(completeListener);
            
            User thisUser = await completeListener.finalBox.Task;
            return thisUser;
        }


        public static void SaveUser(User user)
        {
            Init();

            HashMap map = new HashMap();
            map.Put("username", user.Name);
            map.Put("email", user.Email);
            map.Put("password", user.Password);
            map.Put("calorie goal", user.CalorieGoal);
            map.Put("days", new ArrayList());
            //REVIEW_CHANGE
            DocumentReference newDocPointer = DataBase.Collection("users").Document(user.Email); // point to an imaginary doc (it doesnt exist yet)
            newDocPointer.Set(map); // create the actual doc (first it was a pointer, now its an actual doc that exists).


        }
        public static void SaveFood(Food food)
        {
            Init();

            HashMap map = new HashMap();
            map.Put("name", food.Name);
            map.Put("calories", food.Cal);
            map.Put("protein", food.Pro);
            //REVIEW_CHANGE
            DocumentReference newDocPointer = DataBase.Collection("food").Document(food.Name); // point to an imaginary doc (it doesnt exist yet)
            newDocPointer.Set(map); // create the actual doc (first it was a pointer, now its an actual doc that exists).
        }
        public static void AddDay(Day day, User user)
        {
            Init();
            DocumentReference docRef = DataBase.Collection("users").Document(user.Email);
            var thisDay = new HashMap();
            
            
            thisDay.Put("breakfast", ParseFoodList(day.Breakfast));


            thisDay.Put("lunch", ParseFoodList(day.Lunch));

            thisDay.Put("dinner", ParseFoodList(day.Dinner));


            thisDay.Put("calories", day.CaloriesEaten);
            thisDay.Put("protein", day.ProteinEaten);
            thisDay.Put("title", day.Title);

            var updates = new Dictionary<string, Java.Lang.Object>
            {
                { "days", FieldValue.ArrayUnion(thisDay)}
            };
            docRef.Update(updates);

        }

        public static ArrayList ParseFoodList(List<Food> list)
        {
            ArrayList result = new ArrayList();
            foreach (var food in list)
            {
                HashMap thisFood = new HashMap();
                thisFood.Put("name", food.Name);
                thisFood.Put("calories", food.Cal);
                thisFood.Put("protein", food.Pro);
                result.Add(thisFood);
            }
            return result;

        }


        public async static Task<List<Food>> GetAllFoods()
        {
            Init();
            CollectionReference foods = DataBase.Collection("food");

            QuerySnapshot snap = await foods.Get().AsAsync<QuerySnapshot>();
            List<Food> foodsList = new List<Food>();
            foreach (DocumentSnapshot foodSnapshot in snap.Documents) 
            {
                var data = foodSnapshot.Data;
                string name = (string)data["name"];
                int calories = Convert.ToInt32(data["calories"]);
                int protein = Convert.ToInt32(data["protein"]);
                Food thisFood = new Food(name, calories, protein);
                foodsList.Add(thisFood);
            }
            return foodsList;

        }
        public static void Init()
        {
            DataBase = signupActivity.DataBase;

        }


        internal class OnCompleteListener : Java.Lang.Object, IOnCompleteListener
        {
            public TaskCompletionSource<User> finalBox;
            
            public OnCompleteListener()
            {
                finalBox = new TaskCompletionSource<User>();
            }



            public void OnComplete(Android.Gms.Tasks.Task task)
            {
                try
                {
                    if (task.IsSuccessful) // there is a document!
                    {
                        DocumentSnapshot document = (DocumentSnapshot)task.Result;
                        if (document.Exists())
                        {
                            var documentData = document.Data;
                            // create box from data
                            string email = (string)documentData["email"];
                            string username = (string)documentData["username"];
                            string password = (string)documentData["password"];
                            int calorieGoal = Convert.ToInt32(documentData["calorie goal"]);

                            List<Day> days = new List<Day>();
                            //get the fucking days
                            Java.Lang.Object daysFromDB = documentData["days"];
                            if(daysFromDB != null)
                            {
                                JavaList javaDays = (JavaList)documentData["days"];

                                 days = ParseDays(javaDays);

                            }
                            User thisUser = new User(username, email, password, calorieGoal, days);
                            finalBox.SetResult(thisUser);
                        }
                        else
                        {
                            finalBox.SetResult(null);
                        }
                    }
                    else
                    {
                        finalBox.SetResult(null);
                    }
                }
                catch (System.Exception ex)
                {
                    finalBox.SetResult(null);
                }
            }
        
            private List<Day> ParseDays(JavaList days)
            {
                List<Day> daysList = new List<Day>();
                foreach(JavaDictionary d in days)
                {
                    JavaList breakfastJava = (JavaList)d["breakfast"];
                    JavaList lunchJava = (JavaList)d["lunch"];
                    JavaList dinnerJava = (JavaList)d["dinner"];

                    List<Food> breakfast = ParseMeal(breakfastJava);
                    List<Food> lunch = ParseMeal(lunchJava);
                    List<Food> dinner = ParseMeal(dinnerJava);

                    int calories = Convert.ToInt32(d["calories"]);
                    int protein = Convert.ToInt32(d["protein"]);
                    string title = (string)d["title"];

                    Day day = new Day(title, breakfast, lunch, dinner, calories, protein);
                    daysList.Add(day);
                }
                return daysList;
            }

            private List<Food> ParseMeal(JavaList meal)
            {
                List<Food> result = new List<Food>();
                foreach(JavaDictionary item in meal)
                {

                    int calories = Convert.ToInt32(item["calories"]);
                    int protein = Convert.ToInt32(item["protein"]);
                    string name = (string)item["name"];
                    result.Add(new Food(name, calories, protein));  

                }
                return result;
            }
        }
    }
}