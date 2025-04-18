using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_loadingScreen;
    [SerializeField] private Slider m_loadingSlider;
    [SerializeField] private Sprite[] m_loadingImages;
    [SerializeField] private GameObject m_background;
    [SerializeField] private GameObject m_background1;
    [SerializeField] private GameObject m_setting;
    [SerializeField] private GameObject m_instruction;
    [SerializeField] private GameObject m_rank;
    [SerializeField] private GameObject m_credit;
    [SerializeField] private GameObject m_darkOverlay;
    [SerializeField] private GameObject m_blur;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_background.SetActive(false);
            m_background1.SetActive(true);
            PlayButtonSound();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_background.SetActive(true);
            m_background1.SetActive(false);
            PlayButtonSound();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_credit.activeSelf)
            {
                m_credit.SetActive(false);
            }

            if (m_instruction.activeSelf)
            {
                m_instruction.SetActive(false);
            }

            if (m_rank.activeSelf)
            {
                m_rank.SetActive(false);
            }

            if (m_setting.activeSelf)
            {
                m_setting.SetActive(false);
            }

            m_darkOverlay.SetActive(false);
            m_blur.GetComponent<BlurRawImages>().RemoveBlur();
            PlayButtonSound();
        }
    }

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
