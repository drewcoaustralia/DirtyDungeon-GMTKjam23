using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 720f;
    private Rigidbody _rb;
    private Quaternion lastTargetRotation;
    public Animator anim;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        input.Normalize();
        _rb.velocity = input * moveSpeed;
        if (input == Vector3.zero)
        {
            if (transform.rotation != lastTargetRotation)
            {
                transform.rotation = Quaternion.RotateTowards(transform.rotation, lastTargetRotation, turnSpeed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("isWalking", false);
            }
        }
        else
        {
            anim.SetBool("isWalking", true);
            Quaternion targetRotation = Quaternion.LookRotation(input, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            lastTargetRotation = targetRotation;
        }
    }
}
