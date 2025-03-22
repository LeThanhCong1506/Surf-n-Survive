using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;

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
    [HideInInspector] public BlurSpriteRenderer m_gameoverMenu;

    private ObstacleSpawner m_obstacleSpawner;
    private SpeedPowerUpManager m_speedPowerUpManager;
    private UIManager m_uiManager;
    private bool m_appliedBlur;

    void Start()
    {
        IsPause = false;
        m_appliedBlur = false;

        m_playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        m_gameoverMenu = GameObject.Find("PauseManager").GetComponent<BlurSpriteRenderer>();
        m_obstacleSpawner = new ObstacleSpawner(this, ObstaclePrefab, StartDelay, RepeatRate);
        m_speedPowerUpManager = new SpeedPowerUpManager(this, SpeedPrefab, m_obstacleSpawner);
        m_uiManager = new UIManager(m_distanceLabel, m_currentDistanceLabel, m_bestDistanceLabel);

        m_obstacleSpawner.StartSpawning();
        m_speedPowerUpManager.StartSpawning();
        m_uiManager.UpdateDistanceLabel(m_playerControllerScript.DistanceToInt);
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
                m_gameoverMenu.RemoveBlur();
                m_appliedBlur = false;
            }
        }
    }

    IEnumerator StartGameOverCoroutine()
    {
        m_uiManager.UpdateDistanceWhenPauseAndGameOver();
        yield return new WaitForSeconds(2.6f);
        Time.timeScale = 0;
        m_gameoverMenu.ApplyBlur();
        m_appliedBlur = true;
        m_uiManager.UpdateCurrentDistanceLabel($"{m_playerControllerScript.DistanceToInt} m");
        m_uiManager.UpdateBestDistanceLabel("1250 m");
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
}
