using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Firebase;
using Firebase.Firestore;
using KfirCalorieCounterReal.Code;
using KfirCalorieCounterReal.objects;
namespace KfirCalorieCounterReal
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class signupActivity : AppCompatActivity
    {
        public static FirebaseFirestore DataBase;

        private EditText usernameInput, emailInput, passwordInput, caloriesInput;
        private Button signUpButton, loginbutton;
        private TextView error;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState); //tamid 
            Xamarin.Essentials.Platform.Init(this, savedInstanceState); //tamid
            FirebaseApp.InitializeApp(this);
            SetContentView(Resource.Layout.signup_layout);

            signupActivity.DataBase = FirebaseFirestore.Instance;



            signUpButton = FindViewById<Button>(Resource.Id.signUpButton);
            loginbutton = FindViewById<Button>(Resource.Id.loginButton);


            usernameInput = FindViewById<EditText>(Resource.Id.nameInput);
            emailInput = FindViewById<EditText>(Resource.Id.emailInput);
            passwordInput = FindViewById<EditText>(Resource.Id.passwordInput);
            caloriesInput = FindViewById<EditText>(Resource.Id.caloriesInput);
            error = FindViewById<TextView>(Resource.Id.error);
            
            signUpButton.Click += SignUpButtonClick;
            loginbutton.Click += LoginButton_Click;

        }

        private void LoginButton_Click(object sender, System.EventArgs e)
        {
            // create login activity
            Intent loginIntent = new Intent(this, typeof(loginActivity));
            StartActivity(loginIntent);
        }

        private void SignUpButtonClick(object sender, System.EventArgs e)
        {
            string username = usernameInput.Text;
            string email = emailInput.Text;
            string password = passwordInput.Text;
            string calorieGoal = caloriesInput.Text;
            if(usernameInput.Text.Length == 0 || emailInput.Text.Length == 0 || passwordInput.Text.Length == 0)
            {
                error.Visibility = Android.Views.ViewStates.Visible;
                return;
            }

            //REVIEW_CHANGE
            User newUser = new User(username, email, password, int.Parse(calorieGoal));
            FireStoreHelper.SaveUser(newUser);
            User.thisUser = newUser;
            Intent intent = new Intent(this, typeof(homeActivity));
            StartActivity(intent);


        }
    }
}