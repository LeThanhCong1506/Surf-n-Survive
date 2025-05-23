using System.Collections;
using UnityEngine;

/// <summary>
/// Handles the spawning of obstacles at random positions and intervals.
/// Gradually increases the speed of obstacles over time to enhance difficulty.
/// Pauses obstacle spawning when speed power-ups are being spawned.
/// </summary>
public class ObstacleSpawner
{
    private GameManager m_gameManager;
    private GameObject[] m_obstaclePrefab;
    private float m_startDelay;
    private float m_repeatRate;
    private bool m_checkSpeed = false;
    public bool IsSpawningSpeedItem = false; // Thêm biến này

    public ObstacleSpawner(GameManager manager, GameObject[] prefabs, float delay, float rate)
    {
        m_gameManager = manager;
        m_obstaclePrefab = prefabs;
        m_startDelay = delay;
        m_repeatRate = rate;
    }

    public void StartSpawning()
    {
        m_gameManager.StartCoroutine(SpawnObstacle());
        m_gameManager.StartCoroutine(IncreaseSpeedOverTime());
    }

    private IEnumerator SpawnObstacle()
    {
        yield return new WaitForSeconds(m_startDelay);
        while (!m_gameManager.m_playerControllerScript.Gameover)
        {
            if (IsSpawningSpeedItem) // Kiểm tra nếu đang spawn speed item
            {
                yield return null; // Tạm dừng việc spawn obstacle
                continue;
            }

            var obstacleIndex = Random.Range(0, m_obstaclePrefab.Length);
            Vector3 spawnPos;

            if (obstacleIndex == 0 || obstacleIndex == 1)
            {
                spawnPos = new Vector3(10, Random.Range(-1.4f, 0.7f), 0);
            }
            else if (obstacleIndex == 4)
            {
                spawnPos = new Vector3(10, -3.5f, 0);
            }
            else
            {
                spawnPos = new Vector3(10, Random.Range(-3.3f, -3.0f), 0);
            }

            Object.Instantiate(m_obstaclePrefab[obstacleIndex], spawnPos, m_obstaclePrefab[obstacleIndex].transform.rotation);

            if (m_checkSpeed)
            {
                m_gameManager.IncreaseAllMoveLeftSpeed(0.9f, 0.03f);
                m_checkSpeed = false;
            }

            yield return new WaitForSeconds(m_repeatRate);
        }
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (!m_gameManager.m_playerControllerScript.Gameover)
        {
            yield return new WaitForSeconds(10);
            m_checkSpeed = true;
        }
    }
}
