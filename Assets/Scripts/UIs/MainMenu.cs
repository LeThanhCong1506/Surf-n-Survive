using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_loadingScene;

    public void PlayGame()
    {
        //StartCoroutine(LoadScene());

        var checkPlayedStoryScene = SaveManager.Instance.GetBool("PlayedStoryScene");
        if (checkPlayedStoryScene != "True")
        {
            SceneManager.LoadScene(1);
            AudioManager.Instance.Pause();
        }
        else
            SceneManager.LoadScene(2);
    }

    //IEnumerator LoadScene()
    //{
    //    m_loadingScene.SetActive(true);
    //    for (int i = 0; i < 100; i++)
    //    {
    //        m_loadingScene.GetComponent<Slider>().value = i;
    //        yield return new WaitForSeconds(10f);
    //    }
    //    m_loadingScene.SetActive(false);
    //}

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonSound()
    {
        AudioManager.Instance.PlayButtonSound();
    }
}
