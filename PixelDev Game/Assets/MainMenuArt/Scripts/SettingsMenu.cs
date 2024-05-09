using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] GameObject settings;

    public void Settings()
    {
        settings.SetActive(true);
    }
}
