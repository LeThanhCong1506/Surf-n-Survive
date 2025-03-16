using System.Collections;
using UnityEngine;

public class SpeedPowerUpManager
{
    private GameManager m_gameManager;
    private GameObject m_speedPrefab;

    public SpeedPowerUpManager(GameManager manager, GameObject prefab)
    {
        m_gameManager = manager;
        m_speedPrefab = prefab;
    }

    public void StartSpawning()
    {
        m_gameManager.StartCoroutine(RandomSpawnSpeedPrefabs());
    }

    private IEnumerator RandomSpawnSpeedPrefabs()
    {
        while (!m_gameManager.m_playerControllerScript.Gameover)
        {
            var randomTime = Random.Range(10, 50);
            yield return new WaitForSeconds(randomTime);
            Object.Instantiate(m_speedPrefab, new Vector3(10, -2.7f, 0), m_speedPrefab.transform.rotation);
        }
    }
}
