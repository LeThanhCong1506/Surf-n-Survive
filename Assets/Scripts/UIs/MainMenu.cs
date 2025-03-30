using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_loadingScreen;
    [SerializeField] private Slider m_loadingSlider;
    [SerializeField] private Sprite[] m_loadingImages;

    public void PlayGame()
    {
        StartCoroutine(SetLoading());
    }

    IEnumerator SetLoading()
    {
        m_loadingScreen.GetComponent<Image>().sprite
    = m_loadingImages[Random.Range(0, m_loadingImages.Length)];

        m_loadingSlider.value = 0;
        // Set loading screen
        while (m_loadingSlider.value < 1)
        {
            m_loadingSlider.value += 0.005f;
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(0.5f);

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
