using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    private PlayerController playerControllerScript;
    private float leftBound = -15;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (!playerControllerScript.gameover)
        //{
            
        //}
        transform.Translate(Vector3.left * Time.deltaTime * speed);
        //if (transform.position.x<leftBound && gameObject.CompareTag("Obstacle"))
        //{
        //    Destroy(gameObject);
        //}
    }
}
