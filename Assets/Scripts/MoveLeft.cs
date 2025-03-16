using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;
    private PlayerController playerControllerScript;
    //private float leftBound = -30;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // FixedUpdate is called at a fixed interval and is independent of frame rate
    void FixedUpdate()
    {
        if (playerControllerScript == null)
            return;

        if (!playerControllerScript.gameover)
        {
            transform.Translate(Vector3.left * Time.fixedDeltaTime * speed);
        }

        //if (transform.position.x < leftBound && !gameObject.CompareTag("Background"))
        //{
        //    gameObject.SetActive(false);
        //}
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }
}
