using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using System.Threading.Tasks;
// https://firebase.google.com/docs/auth/unity/start

public class Authorize : MonoBehaviour
{ // all scripts must inherit from MonoBehavior (?)

    public string emailChoice;
    public string passwordChoice;
    private bool isLogin = false;

    public void ReadEmailInput(string choice)
    {
        emailChoice = choice;
        Debug.Log("Email input: " + emailChoice);
    }

    public void ReadPasswordInput(string choice)
    {
        passwordChoice = choice;
        Debug.Log("Password input: " + passwordChoice);
    }

    public void Login()
    {
        string email = emailChoice;
        string password = passwordChoice;
        // for testing, see if it correctly displays input
        Debug.Log("Attempting login with email: " + emailChoice + " and password: " + passwordChoice);
        isLogin = false;
        // authenticate user through firebase using the entered email and password
        FirebaseAuth.DefaultInstance.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("Sign-in through email and password was not completed.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Sign-in error, message: " + task.Exception);
                return;
            }

            // authentication successful
            AuthResult authResult = task.Result;
            FirebaseUser user = authResult.User;
            Debug.Log("Logged in successfully!: " + user.DisplayName);

            isLogin = true;

            SwitchScene("MainMenu");
        });
    }

    public async void SwitchScene(string name)
    {
        await Task.Delay(5000); // need to wait in order to see if isLogin boolean was true or not from the Login() function
        if (isLogin)
        {
            SceneManager.LoadScene(name);
        }
        else
        {
            Debug.LogWarning("Invalid login, cannot proceed to game.");
        }
    }

    public void Register()
    {
        string email = emailChoice;
        string password = passwordChoice;
        Debug.Log("Attempting to register email: " + emailChoice + " and password: " + passwordChoice);
        // register through firebase using the entered email and password
        FirebaseAuth.DefaultInstance.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("Registering an account was not completed.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("Register rror, message: " + task.Exception);
                return;
            }

            // Registration successful
            AuthResult authResult = task.Result;
            FirebaseUser newUser = authResult.User;
            Debug.Log("Registered successfully!: " + newUser.DisplayName);
        });
    }


    public void Guest_Login()
    {

    }

    void ErrorHandle(AuthError errorcode)
    {
        string message = "";
        message = errorcode.ToString();



        print(message);
    }
}