using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameover = false;
    private Animator playerAnim;
    private float endHeight;
    private bool waitTurn = false;
    public float speed = 2;
    public float distance = 0;
    public int distanceToInt = 0;
    public SpawnManager spawnManager;
    private bool ateSpeed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        spawnManager = GameObject.Find("GameManager").GetComponent<SpawnManager>();
        Physics2D.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //count kilometers
        if (!gameover)
        {
            if(!ateSpeed)
                distance += Time.deltaTime;
            else
                distance += Time.deltaTime * 3;
            distanceToInt = Mathf.RoundToInt(distance);
            Debug.Log("Distance: " + distance);
        }


        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround)
        {
            playerAnim.SetBool("Jump", true);
            playerRb.AddForce(Vector2.up * jumpForce);
            isOnGround = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) && isOnGround && !waitTurn)
        {
            HandleHoldBend();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) && isOnGround && waitTurn)
        {
            HandleReleaseBend();
        }
    }

    private void HandleHoldBend()
    {
        waitTurn = true;
        playerAnim.SetInteger("Bend", 2);
        endHeight = gameObject.GetComponent<BoxCollider2D>().size.y;
        gameObject.GetComponent<BoxCollider2D>().size =
            new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x
           , endHeight * 0.33333f * 2);
        gameObject.GetComponent<BoxCollider2D>().offset =
           new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x
          , -1);
    }

    private void HandleReleaseBend()
    {
        playerAnim.SetInteger("Bend", 3);
        gameObject.GetComponent<BoxCollider2D>().size =
            new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x
           , endHeight);
        gameObject.GetComponent<BoxCollider2D>().offset =
               new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x
              , 0);
        waitTurn = false;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            gameover = true;
            Debug.Log("Game Over!");
            playerAnim.SetBool("Fall", true);
            playerAnim.SetBool("Jump", false);
            playerAnim.SetInteger("Bend", 3);
            collision.gameObject.GetComponent<EdgeCollider2D>().enabled = false;//tắt collider khi nhân vật va chạm khi trên không trung hoặc khi đáp xuống obstacle
            StartCoroutine(PLayAnimationDie());
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Debug.Log("On Ground!");
            isOnGround = true;
            playerAnim.SetBool("Jump", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Speed"))
        {
            ateSpeed = true;
            StartCoroutine(WaitForStartSpeedPowerUp());
            StartCoroutine(WaitForEndSpeedPowerUp());
        }
    }
    IEnumerator WaitForStartSpeedPowerUp()
    {
        playerAnim.SetBool("Speed", true);
        yield return new WaitForSeconds(0.2f);
        spawnManager.DeactivateEdgeCollider2D();
        spawnManager.IncreaseAllMoveLeftSpeed(20);
    }

    IEnumerator WaitForEndSpeedPowerUp()
    {
        yield return new WaitForSeconds(2);
        spawnManager.IncreaseAllMoveLeftSpeed(-5);
        yield return new WaitForSeconds(2);
        spawnManager.IncreaseAllMoveLeftSpeed(-5);
        playerAnim.SetBool("Speed", false);
        yield return new WaitForSeconds(2);
        spawnManager.IncreaseAllMoveLeftSpeed(-5);
        yield return new WaitForSeconds(1);
        spawnManager.IncreaseAllMoveLeftSpeed(-5);

        spawnManager.ActivateEdgeCollider2D();
        ateSpeed = false;
    }

    IEnumerator PLayAnimationDie()
    {
        float offsetIncrement = 0.4f;
        float waitTime = 0.17f;
        BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();

        for (int i = 1; i <= 9; i++)
        {
            boxCollider.offset = new Vector2(boxCollider.offset.x, i * offsetIncrement);
            yield return new WaitForSeconds(waitTime);
        }

        boxCollider.offset = new Vector2(boxCollider.offset.x, 3f);
        yield return new WaitForSeconds(0.05f);
        gameObject.SetActive(false);
    }
}
