using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    private GameManager m_gameManager;
    [SerializeField] private GameObject m_pauseMenu;
    [SerializeField] private GameObject m_resumeMenu;

    private void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame &&
            !m_gameManager.m_playerControllerScript.Gameover)
        {
            TogglePause();
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
        m_gameManager.IsPause = true;
        Time.timeScale = 0;
    }

    public void Home()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        AudioManager.Instance.PlayBackgroundMenu();
    }

    public void Resume()
    {
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
