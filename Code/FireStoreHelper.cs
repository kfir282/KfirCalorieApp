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

using Java.Util.Functions;
using static Android.Media.TV.TvContract.Programs;
using static Java.Util.Jar.Attributes;
using KfirCalorieCounterReal.objects;


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

                            User thisUser = new User(username, email, password);
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
        }
    }
}