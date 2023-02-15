using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float castDist = 0.6f;
    [SerializeField] private float gravityRise = 5f;
    [SerializeField] private float gravityFall = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sRender;

    private float horizontalMove;
    private bool grounded = false;
    private bool jump = false;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump") && grounded)
            jump = true;

        if (horizontalMove > 0.2f)
        {
            anim.SetBool("walking", true);
            sRender.flipX = false;
        }
        else if (horizontalMove < -0.2f)
        {
            anim.SetBool("walking", true);
            sRender.flipX = true;
        }
        else
            anim.SetBool("walking", false);
    }

    private void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;

        if (jump)
        {
            rb.velocity += Vector2.up * Mathf.Sqrt(-2f * jumpHeight * gravityRise * Physics2D.gravity.y);
            jump = false;
        }

        if (rb.velocity.y > 0)
            rb.gravityScale = gravityRise;
        else if (rb.velocity.y < 0)
            rb.gravityScale = gravityFall;

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red);

        grounded = hit.collider != null;

        rb.velocity = new Vector3(moveSpeed, rb.velocity.y, 0);
    }
}
