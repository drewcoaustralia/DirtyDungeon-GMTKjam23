using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 3f;
    private Rigidbody _rb;
    private Quaternion lastTargetRotation;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (input == Vector3.zero)
        {
            if (transform.rotation != lastTargetRotation) transform.rotation = Quaternion.Slerp(transform.rotation, lastTargetRotation, turnSpeed);
        }
        else
        {
            Vector3 direction = transform.position + input;
            Quaternion targetRotation = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, turnSpeed);
            lastTargetRotation = targetRotation;
        }
        _rb.velocity = input * moveSpeed;
    }
}
