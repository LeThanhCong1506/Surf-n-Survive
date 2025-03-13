using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public float startDelay = 2;
    public float repeatRate = 2;
    private bool checkSpeed = false;

    private PlayerController playerControllerScript;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        StartCoroutine(IncreaseSpeedOverTime());
    }

    void Update()
    {

    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameover)
            return;

        var obstacleIndex = Random.Range(0, obstaclePrefab.Length);

        if (obstacleIndex == 0 || obstacleIndex == 5)
        {
            var spawnPos = new Vector3(10, Random.Range(0, 2), 0);
            Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);
        }
        else
        {
            var spawnPos = new Vector3(10, Random.Range(-2.5f, -3.5f), 0);
            Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);
        }

        if (checkSpeed)
        {
            IncreaseAllMoveLeftSpeed(3);
            checkSpeed = false;
        }
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (!playerControllerScript.gameover)
        {
            yield return new WaitForSeconds(40); // Wait for 40 seconds
            checkSpeed = true;
        }
    }

    private void IncreaseAllMoveLeftSpeed(float amount)
    {
        var moveLeftScripts = FindObjectsByType<MoveLeft>(FindObjectsSortMode.None);
        foreach (var moveLeft in moveLeftScripts)
        {
            moveLeft.IncreaseSpeed(amount);
        }
    }
}
