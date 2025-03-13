using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject obstaclePrefab;
    private Vector3 spawnPos = new Vector3(25, 0, 0);
    public float startDelay = 2;
    public float repeatRate = 2;
    private PlayerController playerControllerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnObstacle()
    {
        if (!playerControllerScript.gameover)
        {
            var spawnPos = new Vector3(10, Random.Range(-1, 2), 0);
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
