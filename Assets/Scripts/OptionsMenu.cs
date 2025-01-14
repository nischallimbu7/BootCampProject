using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour
{
    public void ReturnMainmenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
