using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    private GameManager m_gameManager;
    private PlayerController m_playerController;

    private void Start()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_playerController = GameObject.Find("Player").GetComponent<PlayerController>();
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
    }

    public void Resume()
    {
        m_gameManager.IsPause = false;
        Time.timeScale = 1;
    }

    public void Restart()
    {
        // Đặt lại Time.timeScale về 1
        Time.timeScale = 1;

        // Tải lại cảnh hiện tại
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
