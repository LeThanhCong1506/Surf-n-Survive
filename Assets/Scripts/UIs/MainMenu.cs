using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        var checkPlayedStoryScene = SaveManager.Instance.GetBool("PlayedStoryScene");
        if (checkPlayedStoryScene != "True")
        {
            SceneManager.LoadScene(1);
            AudioManager.Instance.Pause();
        }
        else
            SceneManager.LoadScene(2);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonSound()
    {
        AudioManager.Instance.PlayButtonSound();
    }
}
