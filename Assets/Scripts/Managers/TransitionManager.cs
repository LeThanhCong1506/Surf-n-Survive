using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionManager : MonoBehaviour
{
    [SerializeField] private GameObject m_tutorialWindow;
    [SerializeField] private GameObject m_backGround;
    [SerializeField] private GameObject m_backGround1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_backGround.SetActive(false);
            m_backGround1.SetActive(true);
            PlayButtonSound();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_backGround.SetActive(true);
            m_backGround1.SetActive(false);
            PlayButtonSound();
        }
    }

    private void Start()
    {
        StartCoroutine(TurnOffVideo());
        StartCoroutine(TurnOnTutorialWindow());
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
        //tạo một biến với string là true vào savemanager
        SaveManager.Instance.SaveBool("PlayedStoryScene", true);
    }

    IEnumerator TurnOffVideo()
    {
        yield return new WaitForSeconds(37.7f);
        GameObject.Find("Screen").SetActive(false);
    }

    IEnumerator TurnOnTutorialWindow()
    {
        yield return new WaitForSeconds(30);
        m_tutorialWindow.SetActive(true);
    }

    public void PlayButtonSound()
    {
        AudioManager.Instance.PlayButtonSound();
    }
}
