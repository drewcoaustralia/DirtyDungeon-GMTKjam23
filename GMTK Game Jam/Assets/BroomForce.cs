using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BroomForce : MonoBehaviour
{
    public float force = 125f;

    public void ApplyForce(GameObject obj)
    {
        if (obj.GetComponent<Pickuppable>() != null && obj.GetComponent<Broom>() == null) obj.GetComponent<Pickuppable>().EnablePhysics();
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null) return;
        rb.AddForce(((transform.up/2)+transform.forward) * force);
        rb.AddTorque(transform.right * force*.1f);
        rb.AddTorque(transform.forward * force*.1f);
        rb.AddTorque(transform.up * force*.1f);
    }
}
