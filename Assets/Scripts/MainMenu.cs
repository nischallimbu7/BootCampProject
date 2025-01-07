using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI loadingText;
    public void PlayGame()
    {
        loadingText.text = "Loading...";
        SceneManager.LoadScene("Makeen's scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
