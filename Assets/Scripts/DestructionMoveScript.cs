using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionMoveScript : MonoBehaviour
{

    public Rigidbody2D rb;

    private float moveSpeed = -4f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.velocity = transform.right * moveSpeed;
    }
}
