using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public bool moveSideways;
    public bool moveUpDown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (moveUpDown)
            rb.velocity = Vector2.up * speed;
        else if (moveSideways)
            rb.velocity = Vector2.right * speed;
    }

    private void Update()
    {
        transform.Rotate(0f,0f,1000f * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
        {
            rb.velocity = -rb.velocity;
        }
    }
}
