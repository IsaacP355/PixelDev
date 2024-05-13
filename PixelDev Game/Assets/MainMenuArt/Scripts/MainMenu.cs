using UnityEngine;
using UnityEngine.SceneManagement;
using Firebase.Auth;
using Firebase.Database;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject AudioMenu;
    [SerializeField] GameObject SettingsMenu;
    private DatabaseReference dbRef;
    private bool Level1Clear = false;
    public async void GoToScene(string sceneName)
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser;
        dbRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/Level1Clear"); 

        DataSnapshot IsLevel1Clear = await dbRef.GetValueAsync();
        if (IsLevel1Clear.Exists)
            Level1Clear = (bool)IsLevel1Clear.Value; // if it is inside the table, Level1Clear should be setting to true
        else
            Debug.Log("User has not completed the first level");

        if (Level1Clear == true) // this means the user has already cleared Level 1
            SceneManager.LoadScene("Level 2");
        else
            SceneManager.LoadScene(sceneName); 
    }

    public void QuitApp()
    {
        Application.Quit();
        Debug.Log("Application has quit");
    }

    public void VolumeButton()
    {
        AudioMenu.SetActive(true);
    }

    public void SettingsButton()
    {
        SettingsMenu.SetActive(true);
    }
    public void AudioBackButton()
    {
        AudioMenu.SetActive(false);
    }
    public void SettingsBackButton()
    {
        SettingsMenu.SetActive(false);
    }
}
