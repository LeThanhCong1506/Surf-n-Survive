using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;


public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefab;
    public GameObject speedPrefab;
    public float startDelay = 2;
    public float repeatRate = 2;
    private bool checkSpeed = false;
    public UIDocument UIDoc;
    private Label m_DistanceLabel;
    private PlayerController playerControllerScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        InvokeRepeating("SpawnObstacle", startDelay, repeatRate);
        StartCoroutine(IncreaseSpeedOverTime());
        StartCoroutine(RamdomSpawnSpeedPrefabs());
        m_DistanceLabel = UIDoc.rootVisualElement.Q<Label>("Distance");
        m_DistanceLabel.text = playerControllerScript.distanceToInt +" m";
    }

    // Update is called once per frame
    void Update()
    {
        m_DistanceLabel.text = playerControllerScript.distanceToInt + " m";
    }

    void SpawnObstacle()
    {
        if (playerControllerScript.gameover)
            return;

        var obstacleIndex = Random.Range(0, obstaclePrefab.Length);

        if (obstacleIndex == 0 || obstacleIndex == 1)
        {
            var spawnPos = new Vector3(10, Random.Range(-2.0f, 0.7f), 0);
            Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);
        }
        else if (obstacleIndex == 3)
        {
            var spawnPos = new Vector3(10, Random.Range(-3.0f, -4.0f), 0);
            Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);
        }
        else
        {
            var spawnPos = new Vector3(10, Random.Range(-3.5f, -3.0f), 0);
            Instantiate(obstaclePrefab[obstacleIndex], spawnPos, obstaclePrefab[obstacleIndex].transform.rotation);
        }

        if (checkSpeed)
        {
            IncreaseAllMoveLeftSpeed(0.5f);
            checkSpeed = false;
        }
    }

    private IEnumerator RamdomSpawnSpeedPrefabs()
    {
        while (!playerControllerScript.gameover)
        {
            var randomTime = Random.Range(10, 50);
            yield return new WaitForSeconds(randomTime);
            Instantiate(speedPrefab, new Vector3(10, -2.7f, 0), speedPrefab.transform.rotation);
        }
    }

    private IEnumerator IncreaseSpeedOverTime()
    {

        while (!playerControllerScript.gameover)
        {
            yield return new WaitForSeconds(15); // Wait for 40 seconds
            checkSpeed = true;
        }
    }

    public void IncreaseAllMoveLeftSpeed(float amount)
    {
        var moveLeftScripts = FindObjectsByType<MoveLeft>(FindObjectsSortMode.None);
        foreach (var moveLeft in moveLeftScripts)
        {
            moveLeft.IncreaseSpeed(amount);
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
        var objects = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (var obj in objects)
        {
            obj.gameObject.GetComponent<EdgeCollider2D>().enabled = true;
        }
    }
}
