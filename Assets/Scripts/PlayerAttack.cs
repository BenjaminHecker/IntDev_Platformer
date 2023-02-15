using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private GameObject knifePrefab;
    [SerializeField] private float throwSpeed = 10f;
    [SerializeField] private float rotateSpeed = 1f;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // throw knife
        }
    }
}
