using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the pause menu functionality, including pausing and resuming the game,
/// navigating to the home screen, restarting the game, and handling UI interactions.
/// Integrates with GameManager and AudioManager for state management and audio feedback.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    private GameManager m_gameManager;
    [SerializeField] private GameObject m_pauseMenu;
    [SerializeField] private GameObject m_resumeMenu;
    [SerializeField] private GameObject m_rank;

    private void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_rank.activeSelf)
            {
                m_rank.SetActive(false);
                PlayButtonSound();
            }
            else if (!m_gameManager.m_playerControllerScript.Gameover)
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        if (m_gameManager.IsPause)
        {
            m_resumeMenu.GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            m_pauseMenu.GetComponent<Button>().onClick.Invoke();
        }
    }

    public void Pause()
    {
        Debug.Log("Pause called");
        m_gameManager.IsPause = true;
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        AudioManager.Instance.PlayBackgroundMenuGame();
    }

    public void Resume()
    {
        Debug.Log("Resume called");
        m_gameManager.IsPause = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PlayButtonSound()
    {
        AudioManager.Instance.PlayButtonSound();
    }
}
