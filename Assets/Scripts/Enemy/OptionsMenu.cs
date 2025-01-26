using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void ReturnMainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void CreditsToMenuOptions()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void CreditsScene()
    {
        SceneManager.LoadScene("CreditsOption");
    }

    public void MenuToAudio()
    {
        SceneManager.LoadScene("AudioOption");
    }
}
