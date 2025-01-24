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

    public void SelectLevel()
    {
        SceneManager.LoadScene("SelectLevelToPlay");
    }

    public void OptionsGame()
    {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }





    public void QuitGame()
    {
        Application.Quit();
    }
}
