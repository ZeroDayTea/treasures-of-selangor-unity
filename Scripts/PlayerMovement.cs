using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 17f;
    public Rigidbody rb;
    private Vector3 velocity;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody>();
        velocity = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        velocity.x = Input.GetAxisRaw("Horizontal");
        velocity.y = 0;
        velocity.z = Input.GetAxisRaw("Vertical");

        rb.MovePosition(rb.position + (velocity.normalized * moveSpeed * Time.fixedDeltaTime));
        transform.localEulerAngles = Camera.main.transform.localEulerAngles;
    }

    private void FixedUpdate() 
    {

        rb.MovePosition(rb.position + (velocity.normalized * moveSpeed * Time.fixedDeltaTime));
    }
}
