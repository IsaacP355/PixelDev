using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mobmove : MonoBehaviour
{
     public GameObject player;
    public float speed;
    public float distanceBetween;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;

        direction.Normalize();

        if (distance < distanceBetween)
        {
            rb.velocity = direction * speed;

            // Flip character if facing left
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            // Otherwise, ensure character faces right
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            rb.velocity = Vector2.zero; // Stop moving when out of range
        }

        // Rotate the mob to face the player
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }
}
