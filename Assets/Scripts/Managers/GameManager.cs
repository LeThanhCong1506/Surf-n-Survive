using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public GameObject[] ObstaclePrefab;
    public GameObject SpeedPrefab;
    public float StartDelay = 2;
    public float RepeatRate = 2;
    public UIDocument UIDoc;
    public PlayerController m_playerControllerScript { get; private set; }
    private ObstacleSpawner m_obstacleSpawner;
    private SpeedPowerUpManager m_speedPowerUpManager;
    private UIManager m_uiManager;

    void Start()
    {
        m_playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        m_obstacleSpawner = new ObstacleSpawner(this, ObstaclePrefab, StartDelay, RepeatRate);
        m_speedPowerUpManager = new SpeedPowerUpManager(this, SpeedPrefab);
        m_uiManager = new UIManager(UIDoc);

        m_obstacleSpawner.StartSpawning();
        m_speedPowerUpManager.StartSpawning();
        m_uiManager.UpdateDistanceLabel(m_playerControllerScript.DistanceToInt);
    }

    void Update()
    {
        m_uiManager.UpdateDistanceLabel(m_playerControllerScript.DistanceToInt);
    }

    public void IncreaseAllMoveLeftSpeed(float amount, float increaseCountMeter)
    {
        var moveLeftScripts = GameObject.FindObjectsByType<MoveLeft>(FindObjectsSortMode.None);
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
