using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Tốc độ quay tính theo độ trên giây.
    public float RotationSpeed;

    void Update()
    {
        transform.Rotate(0, 0, RotationSpeed * Time.deltaTime);
    }
}
