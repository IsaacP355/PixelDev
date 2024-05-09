using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject AudioMenu;
    [SerializeField] GameObject SettingsMenu;
    public void GoToScene(string sceneName)
    {
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
    public void BackButton()
    {
        AudioMenu.SetActive(false);
    }
}
