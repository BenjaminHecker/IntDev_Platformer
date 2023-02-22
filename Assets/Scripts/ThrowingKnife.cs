using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowingKnife : MonoBehaviour
{
    private Rigidbody2D rb;

    private Transform target;
    private float travelSpeed = 10f;
    private float turnSpeed = 1f;

    private PlayerAttack playerAttack;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Setup(PlayerAttack playerAttack, Transform target, float travelSpeed, float turnSpeed, Vector3 dir)
    {
        this.playerAttack = playerAttack;
        this.target = target;
        this.travelSpeed = travelSpeed;
        this.turnSpeed = turnSpeed;

        transform.position = target.position + dir;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, dir);
        rb.velocity = dir * travelSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 dir = (Vector2) target.position - rb.position;
        float rotateAmount = Vector3.Cross(dir.normalized, transform.up).z;
        rb.angularVelocity = -turnSpeed * rotateAmount;
        rb.velocity = transform.up * travelSpeed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerAttack.RegainKnife();
            Destroy(gameObject);
        }
    }
}
