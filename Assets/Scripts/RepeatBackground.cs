using UnityEngine;

public class RepeatBackground : MonoBehaviour
{
    private Vector3 startPos;
    private float endWith;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position;
        endWith = GetComponent<BoxCollider>().size.x / 2.26f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - endWith)
        {
            transform.position = startPos;
        }
    }
}
