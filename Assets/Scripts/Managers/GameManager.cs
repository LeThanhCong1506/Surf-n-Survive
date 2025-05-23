using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

/// <summary>
/// Manages the overall game flow, including spawning obstacles, handling power-ups,
/// updating UI elements, and managing game states such as pause and game over.
/// </summary>
public class GameManager : MonoBehaviour
{
    public GameObject[] ObstaclePrefab;
    public GameObject SpeedPrefab;
    public float StartDelay = 2;
    public float RepeatRate = 2;
    //public UIDocument UIDoc;

    [HideInInspector] public bool IsPause;
    [SerializeField] private GameObject m_gameOver;
    [SerializeField] private GameObject m_darkOverlay;
    [SerializeField] private GameObject m_pauseButton;
    [SerializeField] private GameObject m_resumeButton;
    [SerializeField] private TextMeshProUGUI m_currentDistanceLabel;
    [SerializeField] private TextMeshProUGUI m_bestDistanceLabel;
    [SerializeField] private TextMeshProUGUI m_distanceLabel;
    [HideInInspector] public PlayerController m_playerControllerScript;
    [HideInInspector] public BlurSpriteRenderer m_blurManager;

    private ObstacleSpawner m_obstacleSpawner;
    private SpeedPowerUpManager m_speedPowerUpManager;
    private UIManager m_uiManager;
    private bool m_appliedBlur;

    void Start()
    {
        IsPause = false;
        m_appliedBlur = false;

        m_playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        m_blurManager = GameObject.Find("BlurManager").GetComponent<BlurSpriteRenderer>();
        m_obstacleSpawner = new ObstacleSpawner(this, ObstaclePrefab, StartDelay, RepeatRate);
        m_speedPowerUpManager = new SpeedPowerUpManager(this, SpeedPrefab, m_obstacleSpawner);
        m_uiManager = new UIManager(m_distanceLabel, m_currentDistanceLabel, m_bestDistanceLabel);

        m_obstacleSpawner.StartSpawning();
        m_speedPowerUpManager.StartSpawning();
        m_uiManager.UpdateDistanceLabel(m_playerControllerScript.DistanceToInt);
        AudioManager.Instance.PlayBackgroundMusicGame();
    }

    void Update()
    {
        if (IsPause)
            m_uiManager.UpdateDistanceWhenPauseAndGameOver();
        else
            m_uiManager.UpdateDistanceLabel(m_playerControllerScript.DistanceToInt);

        if (m_playerControllerScript.Gameover)
        {
            StartCoroutine(StartGameOverCoroutine());
            return;
        }
        else
        {
            if (m_appliedBlur)
            {
                m_blurManager.RemoveBlur();
                m_appliedBlur = false;
            }
        }
    }

    IEnumerator StartGameOverCoroutine()
    {
        m_uiManager.UpdateDistanceWhenPauseAndGameOver();
        yield return new WaitForSeconds(2.6f);
        Time.timeScale = 0;
        m_blurManager.ApplyBlur();
        m_appliedBlur = true;
        m_uiManager.UpdateCurrentDistanceLabel($"{m_playerControllerScript.DistanceToInt} m");
        SaveManager.Instance.SaveHighScore(m_playerControllerScript.DistanceToInt);
        m_uiManager.UpdateBestDistanceLabel($"{SaveManager.Instance.GetHighScore()} m");
        m_gameOver.SetActive(true);
        m_darkOverlay.SetActive(true);
        m_pauseButton.SetActive(false);
        m_resumeButton.SetActive(false);
    }

    public void IncreaseAllMoveLeftSpeed(float amount, float increaseCountMeter)
    {
        var moveLeftScripts = FindObjectsByType<MoveLeft>(FindObjectsSortMode.None);
        foreach (var moveLeft in moveLeftScripts)
        {
            moveLeft.IncreaseSpeed(amount);
            m_playerControllerScript.IncreaseCountMeter(increaseCountMeter);
        }
    }

    public void DeactivateEdgeCollider2D()
    {
        var edgeCollider2Ds = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var edgeCollider2D in edgeCollider2Ds)
        {
            edgeCollider2D.gameObject.GetComponent<EdgeCollider2D>().enabled = false;
        }
    }

    public void ActivateEdgeCollider2D()
    {
        var edgeCollider2Ds = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var edgeCollider2D in edgeCollider2Ds)
        {
            edgeCollider2D.gameObject.GetComponent<EdgeCollider2D>().enabled = true;
        }
    }

    public void SaveHighScore()
    {
        if (m_playerControllerScript != null)
        {
            int currentScore = m_playerControllerScript.DistanceToInt;
            if (SaveManager.Instance != null)
            {
                SaveManager.Instance.SaveHighScore(currentScore);
            }
            else
            {
                // Fallback nếu SaveManager chưa được khởi tạo
                int highScore = PlayerPrefs.GetInt("HighScore", 0);
                if (currentScore > highScore)
                {
                    PlayerPrefs.SetInt("HighScore", currentScore);
                    PlayerPrefs.Save();
                }
            }
        }
    }
}
