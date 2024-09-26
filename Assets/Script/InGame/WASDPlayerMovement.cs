using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WASDPlayerMovement : MonoBehaviour
{
    public float speed = 5.0f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Vector2 move = Vector2.zero;

        if (Input.GetKey(KeyCode.W)) move.y += 1;
        if (Input.GetKey(KeyCode.S)) move.y -= 1;
        if (Input.GetKey(KeyCode.A)) move.x -= 1;
        if (Input.GetKey(KeyCode.D)) move.x += 1;

        rb.velocity = move.normalized * speed;
    }
}
