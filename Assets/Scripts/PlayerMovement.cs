using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerInput playerInput;
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sRender;

    [Header("Basic Movement")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float castDist = 0.6f;
    [SerializeField] private float gravityRise = 5f;
    [SerializeField] private float gravityFall = 5f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private LayerMask groundLayer;

    private Vector2 move;
    private bool grounded = false;
    private bool jump = false;

    [Header("Launch Ability")]
    [SerializeField] private float launchSpeed = 10f;
    [SerializeField] private float launchDuration = 0.2f;
    [SerializeField] private float launchSlowtime = 0.2f;

    private float launchTimer = 0f;
    private bool hasLaunch = false;
    private bool launch = false;
    private Vector2 launchDir;

    private void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sRender = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxisRaw("Vertical"));

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist, groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * castDist, Color.red);

        grounded = hit.collider != null;

        if (playerInput.actions["Jump"].WasPressedThisFrame() && grounded)
            jump = true;

        if (grounded && !launch)
            hasLaunch = true;

        if (playerInput.actions["Launch"].WasPressedThisFrame() && hasLaunch)
        {
            hasLaunch = false;
            launch = true;
            launchTimer = 0f;
            launchDir = -move.normalized;
        }

        if (move.x > 0.2f)
        {
            anim.SetBool("walking", true);
            sRender.flipX = false;
        }
        else if (move.x < -0.2f)
        {
            anim.SetBool("walking", true);
            sRender.flipX = true;
        }
        else
            anim.SetBool("walking", false);
    }

    private void FixedUpdate()
    {
        if (launch)
        {
            Vector2 launchVelocity = launchDir * launchSpeed;
            Vector2 moveVelocity = move * speed;

            if (launchTimer >= launchDuration + launchSlowtime)
            {
                launch = false;
                launchTimer = 0f;
                rb.velocity = moveVelocity;
            }
            else
            {
                if (launchTimer >= launchDuration)
                {
                    float ratio = Mathf.InverseLerp(launchDuration, launchDuration + launchSlowtime, launchTimer);
                    rb.velocity = Vector2.Lerp(launchVelocity, moveVelocity, ratio);
                }
                else
                    rb.velocity = launchDir * launchSpeed;

                launchTimer += Time.fixedDeltaTime;
            }
        }
        else
        {
            if (jump)
            {
                rb.velocity += Vector2.up * Mathf.Sqrt(-2f * jumpHeight * gravityRise * Physics2D.gravity.y);
                jump = false;
            }

            if (rb.velocity.y > 0)
                rb.gravityScale = gravityRise;
            else if (rb.velocity.y < 0)
                rb.gravityScale = gravityFall;

            rb.velocity = new Vector2(move.x * speed, rb.velocity.y);
        }
    }
}
