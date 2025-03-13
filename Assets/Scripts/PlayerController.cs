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
<<<<<<< HEAD
    private bool isBending = false;
    //public AudioClip jumpSound;
    //public AudioClip crashSound;
    //private AudioSource playerAudio;
    
    // Biến để kiểm tra trạng thái animation đứng lên
    private bool isStandingUp = false;
=======
    private bool waitTurn = false;
    private float downArrowHoldTime = 0f;
>>>>>>> d4188d3f33f1bfbf06b65fad131d91d657676034

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
<<<<<<< HEAD
        Physics.gravity *= gravityModifier;
        
        // Lưu chiều cao ban đầu của collider
        endHeight = gameObject.GetComponent<BoxCollider2D>().size.y;
=======
        Physics2D.gravity *= gravityModifier;
>>>>>>> d4188d3f33f1bfbf06b65fad131d91d657676034
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround)
        {
            playerAnim.SetBool("Jump", true);
            playerRb.AddForce(Vector2.up * jumpForce);
            isOnGround = false;
        }

<<<<<<< HEAD
        // Xử lý khi nhấn phím mũi tên xuống
        if (Input.GetKeyDown(KeyCode.DownArrow) && isOnGround && !isBending)
        {
            BendDown();
        }
        
        // Xử lý khi nhả phím mũi tên xuống
        if (Input.GetKeyUp(KeyCode.DownArrow) && isBending)
        {
            StandUp();
        }
        
        // Kiểm tra khi animation Stand_Up đã kết thúc
        if (isStandingUp)
        {
            AnimatorStateInfo stateInfo = playerAnim.GetCurrentAnimatorStateInfo(0);
            
            // Kiểm tra nếu animation đã hoàn thành
            if (!stateInfo.IsName("Stand_Up") || stateInfo.normalizedTime >= 0.9f)
            {
                OnStandComplete();
                isStandingUp = false;
=======
        if (Input.GetKey(KeyCode.DownArrow) && isOnGround && !waitTurn)
        {
            downArrowHoldTime += Time.deltaTime;
            Debug.Log("Hold Time: " + downArrowHoldTime); // Debug log to check hold time


            if (downArrowHoldTime > 0.5&& downArrowHoldTime<=2)
            {
                HandleQuickBend();
                return; // Exit the Update method to skip the following conditions
            }
            else if (downArrowHoldTime > 2)
            {
                HandleHoldBend();
>>>>>>> d4188d3f33f1bfbf06b65fad131d91d657676034
            }
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) && isOnGround && waitTurn)
        {
            HandleReleaseBend();
            downArrowHoldTime = 0f; // Reset hold time when key is released
        }
    }
    private void HandleQuickBend()
    {
        waitTurn = true;
        playerAnim.SetBool("Bend_Fast", true);
        endHeight = gameObject.GetComponent<BoxCollider2D>().size.y;
        gameObject.GetComponent<BoxCollider2D>().size =
            new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x, endHeight * 0.33333f * 2);
        gameObject.GetComponent<BoxCollider2D>().offset =
            new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x, -1);
        StartCoroutine(CheckEndCourutine());
    }

    private void HandleHoldBend()
    {
        waitTurn = true;
        playerAnim.SetInteger("Bend", 1);
        StartCoroutine(TimeWaitAnimationBendDown());
    }

    private void HandleReleaseBend()
    {
        playerAnim.SetInteger("Bend", 3);
        StartCoroutine(TimeWaitAnimationBendUp());
    }
    
    // Hàm xử lý khi người chơi cúi xuống
    private void BendDown()
    {
        isBending = true;
        playerAnim.SetBool("Bend", true);
        
        // Điều chỉnh kích thước và vị trí của collider
        gameObject.GetComponent<BoxCollider2D>().size =
            new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x
           , endHeight * 0.33333f*2);
        gameObject.GetComponent<BoxCollider2D>().offset =
<<<<<<< HEAD
           new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x
          , -1);
    }
    
    // Hàm xử lý khi người chơi đứng lên
    private void StandUp()
    {
        isBending = false;
        isStandingUp = true;
        playerAnim.SetBool("Bend", false);
        
        // KHÔNG reset collider ngay lập tức
        // Sẽ được reset trong hàm OnStandComplete() sau khi animation hoàn thành
    }
    
    // Hàm được gọi khi animation đứng lên hoàn thành
    private void OnStandComplete()
    {
        // Khôi phục kích thước và vị trí của collider
=======
               new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x
              , 0);
        playerAnim.SetBool("Bend_Fast", false);
        waitTurn = false;
    }

    IEnumerator TimeWaitAnimationBendDown()
    {
        yield return new WaitForSeconds(0.2f);
        playerAnim.SetInteger("Bend", 2);
        endHeight = gameObject.GetComponent<BoxCollider2D>().size.y;
        gameObject.GetComponent<BoxCollider2D>().size =
            new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x
           , endHeight * 0.33333f * 2);
        gameObject.GetComponent<BoxCollider2D>().offset =
           new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x
          , -1);
    }

    IEnumerator TimeWaitAnimationBendUp()
    {
        yield return new WaitForSeconds(0.2f);
        playerAnim.SetInteger("Bend", 4);
>>>>>>> d4188d3f33f1bfbf06b65fad131d91d657676034
        gameObject.GetComponent<BoxCollider2D>().size =
            new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x
           , endHeight);
        gameObject.GetComponent<BoxCollider2D>().offset =
<<<<<<< HEAD
           new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x
          , 0);
=======
               new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x
              , 0);
        waitTurn = false;
>>>>>>> d4188d3f33f1bfbf06b65fad131d91d657676034
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))//!gameover
        {
            Debug.Log("On Ground!");
            isOnGround = true;
            playerAnim.SetBool("Jump", false);
            //dirtParticle.Play();
        }
        //else if (collision.gameObject.CompareTag("Obstacle"))
        //{
        //    gameover = true;
        //    Debug.Log("Game Over!");
        //    playerAnim.SetBool("Death_b", true);
        //    playerAnim.SetInteger("DeathType_int", 1);
        //    explosionParticle.Play();
        //    dirtParticle.Stop();
        //    playerAudio.PlayOneShot(crashSound, 1.0f);
        //}
    }
}
