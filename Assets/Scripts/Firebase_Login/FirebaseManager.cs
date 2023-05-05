using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Database;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    //Firebase variables
    [Header("Firebase")]
    public DependencyStatus dependencyStatus;
    public FirebaseAuth auth;
    public FirebaseUser User;

    public DatabaseReference DBreference;


    //Login variables
    [Header("Login")]
    public InputField emailLoginField;
    public InputField UsernameField;
    public InputField passwordLoginField;
    public Text warningLoginText;
    public Text confirmLoginText;

    //Register variables
    [Header("Register")]
    public InputField emailRegisterField;
    public InputField passwordRegisterField;
    public Toggle Male;
    public Text warningRegisterText;

    //Reset variables
    [Header("Register")]
    public InputField ResetEmailText;
    public Text warningResetText;

    public UserClass MadeUser;

    private bool isGuest = false;

    void Awake()
    {
        //Check that all of the necessary dependencies for Firebase are present on the system
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                //If they are avalible Initialize Firebase
                InitializeFirebase();
            }
            else
            {
                Debug.LogError("Could not resolve all Firebase dependencies: " + dependencyStatus);
            }
        });
    }

    private void InitializeFirebase()
    {
        Debug.Log("Setting up Firebase Auth");
        //Set the authentication instance object
        auth = FirebaseAuth.DefaultInstance;
        DBreference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    //Function for the login button
    public void LoginButton()
    {
        //Call the login coroutine passing the email and password
        StartCoroutine(Login(emailLoginField.text, passwordLoginField.text));
    }
    //Function for the register button
    public void RegisterButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(Register(emailRegisterField.text, passwordRegisterField.text));
    }

    public void ResetButton()
    {
        //Call the register coroutine passing the email, password, and username
        StartCoroutine(ResetPassword(ResetEmailText.text));
        //ResetPassword(ResetEmailText.text);
    }

    private IEnumerator Login(string _email, string _password)
    {
        //Call the Firebase auth signin function passing the email and password
        var LoginTask = auth.SignInWithEmailAndPasswordAsync(_email, _password);
        //Wait until the task completes
        yield return new WaitUntil(predicate: () => LoginTask.IsCompleted);

        if (LoginTask.Exception != null)
        {
            //If there are errors handle them
            Debug.LogWarning(message: $"Failed to register task with {LoginTask.Exception}");
            FirebaseException firebaseEx = LoginTask.Exception.GetBaseException() as FirebaseException;
            AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

            string message = "Login Failed!";
            switch (errorCode)
            {
                case AuthError.MissingEmail:
                    message = "Missing Email";
                    break;
                case AuthError.MissingPassword:
                    message = "Missing Password";
                    break;
                case AuthError.WrongPassword:
                    message = "Wrong Password";
                    break;
                case AuthError.InvalidEmail:
                    message = "Invalid Email";
                    break;
                case AuthError.UserNotFound:
                    message = "Account does not exist";
                    break;
            }
            warningLoginText.text = message;
        }
        else
        {
            //User is now logged in
            //Now get the result
            User = LoginTask.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})", User.DisplayName, User.Email);
            warningLoginText.text = "";
            confirmLoginText.text = "Logged In";

            DontDestroyOnLoad(transform.gameObject);

            /*MadeUser = new UserClass(emailRegisterField.text, Male.isOn, UsernameField.text);
            string json = JsonUtility.ToJson(MadeUser);*/
            var DBTask = DBreference.Child("users").Child(User.UserId).GetValueAsync().ContinueWithOnMainThread(task =>
            {
                if (task.IsFaulted)
                {
                    Debug.Log("Fout");
                }
                else if (task.IsCompleted)
                {
                    //DataSnapshot snapshot = task.Result;
                    MadeUser = JsonUtility.FromJson<UserClass>(task.Result.Value.ToString());
                }
            });



            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            yield return new WaitForSeconds(1f);


            SceneManager.LoadScene("Selectionscreen");
        }
    }

    private IEnumerator Register(string _email, string _password)
    {
        if (_email == "")
        {
            //If the email field is blank show a warning
            warningRegisterText.text = "Missing email";
        }
        else
        {
            //Call the Firebase auth signin function passing the email and password
            var RegisterTask = auth.CreateUserWithEmailAndPasswordAsync(_email, _password);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => RegisterTask.IsCompleted);

            if (RegisterTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to register task with {RegisterTask.Exception}");
                FirebaseException firebaseEx = RegisterTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Register Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.MissingPassword:
                        message = "Missing Password";
                        break;
                    case AuthError.WeakPassword:
                        message = "Weak Password";
                        break;
                    case AuthError.EmailAlreadyInUse:
                        message = "Email Already In Use";
                        break;
                }
                warningRegisterText.text = message;
            }
            else
            {
                //User has now been created
                //Now get the result
                User = RegisterTask.Result;

                if (User != null)
                {
                    //Create a user profile and set the username
                    UserProfile profile = new UserProfile { DisplayName = _email };

                    //Call the Firebase auth update user profile function passing the profile with the username
                    var ProfileTask = User.UpdateUserProfileAsync(profile);
                    //Wait until the task completes
                    yield return new WaitUntil(predicate: () => ProfileTask.IsCompleted);

                    if (ProfileTask.Exception != null)
                    {
                        //If there are errors handle them
                        Debug.LogWarning(message: $"Failed to register task with {ProfileTask.Exception}");
                        FirebaseException firebaseEx = ProfileTask.Exception.GetBaseException() as FirebaseException;
                        AuthError errorCode = (AuthError)firebaseEx.ErrorCode;
                        warningRegisterText.text = "Username Set Failed!";
                    }
                    else
                    {
                        //Username is now set
                        //Now return to login screen

                        StartCoroutine(FindObjectOfType<FirebaseManager>().CreateUserInDatabase());
                        //StartCoroutine(FindObjectOfType<FirebaseManager>().UpdateUSer());

                        FindObjectOfType<LoginInput>().BackToLogin();
                        warningRegisterText.text = "";
                    }
                }
            }
        }
    }

    public void GuestLogin()
    {
        MadeUser = new UserClass("guest", true, "guest");
        isGuest = true;
        DontDestroyOnLoad(transform.gameObject);
        SceneManager.LoadScene("Selectionscreen");
    }


    public IEnumerator ResetPassword(string ResetEmail)
    {
        if (ResetEmail == "")
        {
            //If the email field is blank show a warning
            warningResetText.text = "Missing email";
        }
        else
        {
            var ResetTask = auth.SendPasswordResetEmailAsync(ResetEmail);
            //Wait until the task completes
            yield return new WaitUntil(predicate: () => ResetTask.IsCompleted);

            if (ResetTask.Exception != null)
            {
                //If there are errors handle them
                Debug.LogWarning(message: $"Failed to reset task with {ResetTask.Exception}");
                FirebaseException firebaseEx = ResetTask.Exception.GetBaseException() as FirebaseException;
                AuthError errorCode = (AuthError)firebaseEx.ErrorCode;

                string message = "Reset Failed!";
                switch (errorCode)
                {
                    case AuthError.MissingEmail:
                        message = "Missing Email";
                        break;
                    case AuthError.InvalidEmail:
                        message = "Invalid email";
                        break;
                }
                warningResetText.text = message;
            }
            else
            {
                //FindObjectOfType<LoginInput>().BackToLogin();
                warningResetText.text = "Reset email was sent!";
            }

        }
    }



    public class UserClass
    {
        public string email;
        public string username;
        public bool male;
        public List<animal> animals;

        public UserClass(string Email, bool Male, string Username)
        {
            email = Email;
            male = Male;
            username = Username;
            animals = new List<animal>() { new animal("Bird"), new animal("Fish"), new animal("Chameleon"), new animal("Frog"), new animal("Ladybug"), new animal("Red Panda") };
        }
    }


    [System.Serializable]
    public class animal
    {
        public bool finished = false;
        public string name;

        public animal(string Name)
        {
            name = Name;
        }
    }

    public IEnumerator CreateUserInDatabase()
    {
        MadeUser = new UserClass(emailRegisterField.text, Male.isOn, UsernameField.text);
        string json = JsonUtility.ToJson(MadeUser);
        var DBTask = DBreference.Child("users").Child(User.UserId).SetValueAsync(json);

        yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

        if (DBTask.Exception != null)
        {
            Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
        }
    }

    public IEnumerator UpdateUSer(int index)
    {
        MadeUser.animals[index].finished = true;

        if (!isGuest)
        {
            string json = JsonUtility.ToJson(MadeUser);
            var DBTask = DBreference.Child("users").Child(User.UserId).SetValueAsync(json);



            yield return new WaitUntil(predicate: () => DBTask.IsCompleted);

            if (DBTask.Exception != null)
            {
                Debug.LogWarning(message: $"Failed to register task with {DBTask.Exception}");
            }
            else
            {
                Debug.Log("update gelukt");
            }
        }



    }

}
