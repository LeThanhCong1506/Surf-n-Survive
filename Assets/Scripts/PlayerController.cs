using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float jumpForce;
    public float gravityModifier;
    public bool isOnGround = true;
    public bool gameover = false;
    private Animator playerAnim;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    private AudioSource playerAudio;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameover)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirtParticle.Stop();
            playerAudio.PlayOneShot(jumpSound, 3.5f);
        }
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Ground") && !gameover)
    //    {
    //        isOnGround = true;
    //        dirtParticle.Play();
    //    }
    //    else if (collision.gameObject.CompareTag("Obstacle"))
    //    {
    //        gameover = true;
    //        Debug.Log("Game Over!");
    //        playerAnim.SetBool("Death_b", true);
    //        playerAnim.SetInteger("DeathType_int", 1);
    //        explosionParticle.Play();
    //        dirtParticle.Stop();
    //        playerAudio.PlayOneShot(crashSound, 1.0f);
    //    }
    //}
}
