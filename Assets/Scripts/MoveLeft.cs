using UnityEngine;

public class MoveLeft : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    public void IncreaseSpeed(float amount)
    {
        speed += amount;
    }
}
