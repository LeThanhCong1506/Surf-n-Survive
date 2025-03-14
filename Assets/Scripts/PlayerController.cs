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
    //public AudioClip jumpSound;
    //public AudioClip crashSound;
    //private AudioSource playerAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround) //!gameover
        {
            playerAnim.SetBool("Jump", true);
            playerRb.AddForce(Vector2.up * jumpForce);
            isOnGround = false;

            //playerAudio.PlayOneShot(jumpSound, 3.5f);
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow) && isOnGround && !waitTurn)
        {
            waitTurn = true;
            playerAnim.SetBool("Bend", true);
            endHeight = gameObject.GetComponent<BoxCollider2D>().size.y;
            gameObject.GetComponent<BoxCollider2D>().size =
                new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x
               , endHeight * 0.33333f*2);
            gameObject.GetComponent<BoxCollider2D>().offset =
               new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x
              , -1);
            StartCoroutine(CheckEndCourutine());
        }
    }

    IEnumerator CheckEndCourutine()
    {
        yield return new WaitForSeconds(1);
        gameObject.GetComponent<BoxCollider2D>().size =
                new Vector2(gameObject.GetComponent<BoxCollider2D>().size.x
               , endHeight);
        gameObject.GetComponent<BoxCollider2D>().offset =
               new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x
              , 0);
        playerAnim.SetBool("Bend", false);
        waitTurn = false;
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
