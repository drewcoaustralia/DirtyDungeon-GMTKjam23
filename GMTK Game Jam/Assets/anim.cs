using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim : MonoBehaviour
{
    Animator animat;
    private Quaternion lastTargetRotation;
    // Start is called before the first frame update
    void Start()
    {
        animat = GetComponent<Animator>();
        // anim.StartPlayback();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            animat.SetTrigger("Interacting");
        }

        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
        {
            animat.SetBool("isWalking", true);
        }
        else animat.SetBool("isWalking", false);

        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (input == Vector3.zero)
        {
            if (transform.rotation != lastTargetRotation) transform.rotation = Quaternion.Slerp(transform.rotation, lastTargetRotation, 1/Time.deltaTime);
        }
        else
        {
            Quaternion targetRotation = Quaternion.LookRotation(input);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1/Time.deltaTime);
            lastTargetRotation = targetRotation;
        }

    }
}
