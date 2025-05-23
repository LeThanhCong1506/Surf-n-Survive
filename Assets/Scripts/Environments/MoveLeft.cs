using UnityEngine;

/// <summary>
/// Moves the GameObject to the left at a specified speed.
/// Stops movement when the game is over.
/// </summary>
public class MoveLeft : MonoBehaviour
{
    public float Speed;
    private PlayerController m_playerControllerScript;

    void Start()
    {
        m_playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void FixedUpdate()
    {
        if (m_playerControllerScript == null)
            return;

        if (m_playerControllerScript.Gameover)
            return;

        transform.Translate(Vector3.left * Time.fixedDeltaTime * Speed, Space.World);
    }

    public void IncreaseSpeed(float amount)
    {
        Speed += amount;
    }
}
