using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D m_playerRb;
    private Animator m_animator;
    public float JumpForce;
    public float GravityModifier;
    private float m_distance = 0;
    public float Speed = 2;
    private int m_distanceToInt = 0;
    public float SpeedPowerUpDuration = 2;
    public bool IsOnGround { get; set; } = true;
    public bool Gameover { get; set; } = false;
    public float Distance { get; set; }
    public int DistanceToInt { get; set; }
    public bool AteSpeed { get; set; } = false;
    public bool WaitTurn { get; set; } = false;

    private PlayerMovement m_playerMovement;
    private PlayerCollision m_playerCollision;
    private PlayerPowerUp m_playerPowerUp;
    private PlayerAnimation m_playerAnimation;
    private GameManager m_gameManager;

    void Start()
    {
        m_playerRb = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Physics2D.gravity *= GravityModifier;

        m_playerAnimation = new PlayerAnimation(this, m_animator, m_gameManager);
        m_playerMovement = new PlayerMovement(this, m_playerRb);
        m_playerCollision = new PlayerCollision(this, m_animator, m_playerAnimation);
        m_playerPowerUp = new PlayerPowerUp(this, m_playerAnimation);
    }

    void Update()
    {
        if (Gameover)
            return;

        m_playerMovement.UpdateDistance(ref m_distance, ref m_distanceToInt, SpeedPowerUpDuration, AteSpeed);
        DistanceToInt = m_distanceToInt;


        if (Input.GetKeyDown(KeyCode.UpArrow) && IsOnGround)
        {
            m_playerMovement.HandleJump();
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) &&
            IsOnGround && !WaitTurn)
        {
            m_playerMovement.HandleHoldBend();
        }

        if (Input.GetKeyUp(KeyCode.DownArrow) &&
            IsOnGround && WaitTurn)
        {
            m_playerMovement.HandleReleaseBend();
        }
    }

    public void IncreaseCountMeter(float amount)
    {
        SpeedPowerUpDuration += amount;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        m_playerCollision.HandleCollision(collision);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_playerPowerUp.HandleTrigger(collision);
    }
}
