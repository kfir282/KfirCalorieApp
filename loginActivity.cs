using Android.App;
using Android.Content;
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
    [Activity(Label = "loginActivity")]
    public class loginActivity : Activity
    {
        private EditText  emailInput, passwordInput;
        private Button loginButton;
        private TextView error;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.login_layout);

            loginButton = FindViewById<Button>(Resource.Id.loginButton);

            emailInput = FindViewById<EditText>(Resource.Id.emailInput);
            passwordInput = FindViewById<EditText>(Resource.Id.passwordInput);
            error = FindViewById<TextView>(Resource.Id.error);

            loginButton.Click += LoginButton_Click;


        }

        private async void LoginButton_Click(object sender, EventArgs e)
        {
            error.Visibility = ViewStates.Invisible;
            string email = emailInput.Text;
            string password = passwordInput.Text;
            if(email.Length == 0 ||  password.Length == 0)
            {
                error.Visibility= ViewStates.Visible;
                return;
            }
            User user =  await FireStoreHelper.GetUser(email);
            if(user == null)
            {
                error.Visibility = ViewStates.Visible;
            }
            else
            {
                if (password == user.GetPassword())
                {
                    // log in
                }
                else {
                    error.Text = "Invalid Password";
                    error.Visibility = ViewStates.Visible;
                }

            }

        }
    }
}