using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using Firebase.Auth;
using System.Threading.Tasks;
using System;
using Firebase.Database;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;

    private DatabaseReference dbRef;
    private float volume;
    private float defaultVolume = 0.5f;

    private async void Start()
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser; // user is already logged in here from login screen
        Debug.Log("User currently at main menu is: " + user.Email);
        Debug.Log("User ID is: " + user.UserId);
        /*
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            setMusicVolume();
        }
        */
        // in database, user's volume will be found by their corresponding ID
        dbRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + user.UserId + "/volume");

        await LoadVolume(); // LoadVolume will retrieve volume from the user, if they haven't changed it, they will have a default of .5
        // check if user changes music slider volume value
        musicSlider.onValueChanged.AddListener((changeVolume) => MusicVolume());
    }

    public void MusicVolume() // making this function not get used by musicSlider object inside of "MainMenu" for some reason fixed an issue with retrieving and loading audio data, going to keep it like this so the code works
    {
        volume = musicSlider.value;
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
        // slider value saved to db
        dbRef.SetValueAsync(volume);
    }

    private async Task LoadVolume()
    {
        DataSnapshot getVolume = await dbRef.GetValueAsync();
        // if volume found for user
        if (getVolume.Exists)
        {
            string userVolume = getVolume.Value.ToString();
            volume = float.Parse(userVolume);
            musicSlider.value = volume;
            myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        }
        else // if volume not found, give user .5 default
        {
            musicSlider.value = defaultVolume;
            myMixer.SetFloat("music", Mathf.Log10(defaultVolume) * 20);
            dbRef.SetValueAsync(defaultVolume);
        }
    }

}
