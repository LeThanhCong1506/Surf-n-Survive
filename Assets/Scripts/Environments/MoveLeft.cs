using UnityEngine;

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

        if (!m_playerControllerScript.Gameover)
        {
            transform.Translate(Vector3.left * Time.fixedDeltaTime * Speed);
        }
    }

    public void IncreaseSpeed(float amount)
    {
        Speed += amount;
    }
}
