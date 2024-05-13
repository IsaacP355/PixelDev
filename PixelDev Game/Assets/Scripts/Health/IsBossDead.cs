using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Auth;

public class CheckBossHealth : MonoBehaviour
{
    private Health bossHealth;
    private bool loading = false;
    private DatabaseReference dbRef;

    private void Start()
    {
        FirebaseUser user = FirebaseAuth.DefaultInstance.CurrentUser; // user is already logged in here from login screen
        dbRef = FirebaseDatabase.DefaultInstance.GetReference("users/" + FirebaseAuth.DefaultInstance.CurrentUser.UserId + "/Level1Clear"); // in realtime db, save if user beats level 1 under Level1Clear
        bossHealth = GetComponent<Health>(); // https://forum.unity.com/threads/hwo-to-getcomponent-of-a-scrip-from-a-object-that-spawn-in-run-time.1591824/ allows this script to get the health component from slime boss in level 1 (this script is only added on slime boss level 1)
        
    }

    private void Update()
    {
        // is boss health zero and scene loading not in progress?
        if (bossHealth.currentHealth == 0 && !loading)
        {
            Debug.Log("Boss has been slain, taking you to level 2 now!");
            SwitchScene("Level 2");
        }
    }

    public async void SwitchScene(string name)
    {
        loading = true;
        dbRef.SetValueAsync(true); // set LevelClear to logged in user so that if they log back in, they can start at level 2
        await Task.Delay(3000); // before level 2 load, wait 3 seconds
        AsyncOperation load = SceneManager.LoadSceneAsync(name);
        // reduce lag as without this, the game generates way too many Level 2 (is loading) for some reason. Only one switch happens
        loading = false;
    }
}
