using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
            }
            else if (!m_gameManager.m_playerControllerScript.Gameover)
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        //kích hoạt nút pause và resume
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
        AudioManager.Instance.PlayBackgroundMenuGame();
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
