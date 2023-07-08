using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shatter : MonoBehaviour
{
    public GameObject shattered;
    public float force;

    void Update()
    {
        if(Input.GetKeyDown("f")) ShatterThis();        
    }

    void OnCollisionEnter(Collision collision)
    {
        ShatterThis();
    }

    void ShatterThis()
    {
        GameObject shatt = Instantiate(shattered, transform.position, transform.rotation);

        foreach(Rigidbody rb in shatt.GetComponentsInChildren<Rigidbody>())
        {
            // Vector3 frc = (rb.transform.position - transform.position).normalized * force; // add random angles?
            Vector3 frc = (Random.onUnitSphere).normalized * force; // add random angles?
            rb.AddForce(frc);
            Debug.Log(frc);
        }
        Destroy(gameObject);
    }
}
