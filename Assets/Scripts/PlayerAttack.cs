using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int knifeCapacity = 1;
    private int knifeCount;

    [SerializeField] private ThrowingKnife knifePrefab;
    [SerializeField] private float travelSpeed = 10f;
    [SerializeField] private float turnSpeed = 1f;

    private PlayerInput playerInput;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();

        knifeCount = knifeCapacity;
    }

    private void Update()
    {
        if (playerInput.actions["Shoot"].WasPressedThisFrame())
        {
            ThrowKnife();
        }
    }

    private void ThrowKnife()
    {
        if (knifeCount > 0)
        {
            Vector3 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (dir == Vector3.zero)
                dir = rb.velocity;

            dir = PlayerMovement.SnapAngle(dir.normalized);

            ThrowingKnife knife = Instantiate(knifePrefab);
            knife.Setup(this, transform, travelSpeed, turnSpeed, dir.normalized);

            knifeCount--;
        }
    }

    public void RegainKnife()
    {
        knifeCount++;
    }
}
