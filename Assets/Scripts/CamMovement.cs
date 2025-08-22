using UnityEngine;

public class CamMovement : MonoBehaviour
{
    public float MovementSpeed = 5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + (Vector3.right * MovementSpeed) * Time.deltaTime;
    }
}