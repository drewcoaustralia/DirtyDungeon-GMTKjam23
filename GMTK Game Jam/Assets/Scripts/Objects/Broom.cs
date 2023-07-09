using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broom : MonoBehaviour
{
    public Renderer rend;

    public void Pickup()
    {
        rend.enabled = false;
        gameObject.GetComponent<Collider>().enabled = false;
        // gameObject.GetComponent<Rigidbody>().enabled = false;
    }

    public void PutDown()
    {
        rend.enabled = true;
        gameObject.GetComponent<Collider>().enabled = true;
        // gameObject.GetComponent<Rigidbody>().enabled = true;
    }
}
