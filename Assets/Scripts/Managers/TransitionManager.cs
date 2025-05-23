using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages scene transitions, including starting the game, handling tutorial windows,
/// and playing button sounds. Integrates with SaveManager and AudioManager for state persistence and audio feedback.
/// </summary>
public class TransitionManager : MonoBehaviour
{
    [SerializeField] private GameObject m_tutorialWindow;
    [SerializeField] private GameObject m_backGround;
    [SerializeField] private GameObject m_backGround1;

    private void Start()
    {
        StartCoroutine(TurnOffVideo());
        StartCoroutine(TurnOnTutorialWindow());
    }

    public void StartGame()
    {
        SceneManager.LoadScene(2);
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
