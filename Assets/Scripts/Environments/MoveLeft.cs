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

        if (m_playerControllerScript.Gameover)
            return;

        // sử dụng tọa độ của thế giới để di chuyển vật thể sang trái,
        //không phụ thuộc vào hướng của vật thể.
        transform.Translate(Vector3.left * Time.fixedDeltaTime * Speed, Space.World);
    }

    public void IncreaseSpeed(float amount)
    {
        Speed += amount;
    }
}
