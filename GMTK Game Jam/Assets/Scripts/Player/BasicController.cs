using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 720f;
    private Rigidbody _rb;
    private Quaternion lastTargetRotation;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input.Normalize();
        _rb.velocity = input * moveSpeed;
        if (_rb.velocity == Vector3.zero)
        {
            if (transform.rotation != lastTargetRotation) transform.rotation = Quaternion.RotateTowards(transform.rotation, lastTargetRotation, turnSpeed * Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(input, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            lastTargetRotation = targetRotation;
        }
    }
}
