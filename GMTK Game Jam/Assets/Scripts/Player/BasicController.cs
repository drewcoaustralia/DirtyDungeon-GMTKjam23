using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BasicController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 720f;
    private Rigidbody _rb;
    private Quaternion lastTargetRotation;
    public Animator anim;
    public List<AudioClip> footstepSFX;
    AudioSource audioSource;
    private float lastFootstep = 0f;
    public float stepInterval = 0.6f;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
                lastFootstep = Time.time;
            }
        }
        else
        {
            anim.SetBool("isWalking", true);
            Quaternion targetRotation = Quaternion.LookRotation(input, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
            lastTargetRotation = targetRotation;
            if (Time.time - lastFootstep >= stepInterval)
            {
                if (footstepSFX.Count != 0)
                {
                    int idx = Random.Range (0, footstepSFX.Count);
                    audioSource.PlayOneShot(footstepSFX[idx]);
                }
                lastFootstep = Time.time;
            }
        }
    }
}
