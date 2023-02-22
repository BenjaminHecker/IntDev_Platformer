using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private int knifeCapacity = 3;
    private int knifeCount;

    [SerializeField] private ThrowingKnife knifePrefab;
    [SerializeField] private float travelSpeed = 10f;
    [SerializeField] private float turnSpeed = 1f;

    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

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
            Vector3 dir = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
            if (dir == Vector3.zero)
                dir = Vector3.right;

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
