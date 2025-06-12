using System;
using UnityEngine;

/// <summary>
/// Moves the GameObject to the left at a specified speed.
/// Stops movement when the game is over.
/// </summary>
public class MoveLeft : MonoBehaviour
{
    public float Speed;
    private PlayerController m_playerControllerScript;
    public event Action OnOutOfScreen;

    void Start()
    {
        m_playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        if (transform.position.x < -60) // hoặc điều kiện phù hợp
        {
            OnOutOfScreen?.Invoke();
            gameObject.SetActive(false); // hoặc gọi Return từ pool
        }

        if (m_playerControllerScript == null|| m_playerControllerScript.Gameover)
            return;

        if (m_playerControllerScript.AteSpeed)
            if(gameObject.tag == "Obstacle")
                gameObject.GetComponent<EdgeCollider2D>().enabled = false;

        transform.Translate(Vector3.left * Time.deltaTime * Speed, Space.World);
    }

    public void IncreaseSpeed(float amount)
    {
        Speed += amount;
    }
}
