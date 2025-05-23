using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the spawning of speed power-ups at random intervals and positions.
/// Temporarily pauses obstacle spawning while a speed power-up is being spawned.
/// </summary>
public class SpeedPowerUpManager
{
    private GameManager m_gameManager;
    private GameObject m_speedPrefab;
    private ObstacleSpawner m_obstacleSpawner;

    public SpeedPowerUpManager(GameManager manager, GameObject prefab, ObstacleSpawner obstacleSpawner)
    {
        m_gameManager = manager;
        m_speedPrefab = prefab;
        m_obstacleSpawner = obstacleSpawner;
    }

    public void StartSpawning()
    {
        m_gameManager.StartCoroutine(RandomSpawnSpeedPrefabs());
    }

    private IEnumerator RandomSpawnSpeedPrefabs()
    {
        while (!m_gameManager.m_playerControllerScript.Gameover)
        {
            var randomTime = Random.Range(20.0f, 60.0f);
            yield return new WaitForSeconds(randomTime);

            m_obstacleSpawner.IsSpawningSpeedItem = true;
            Object.Instantiate(m_speedPrefab, new Vector3(15,Random.Range(-2.7f, 0.7f), 0), m_speedPrefab.transform.rotation);
            yield return new WaitForSeconds(1);
            m_obstacleSpawner.IsSpawningSpeedItem = false;
        }
    }
}
