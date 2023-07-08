using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public float rayDist = 2f;
    private IInteractable interactable;

    void Awake()
    {
        interactable = null;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        float rayDistActual = rayDist;
        Color color = Color.white;
        bool ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDist);
        interactable = ray ? hit.transform.gameObject.GetComponent<IInteractable>() : null;

        // do stuff
        if (Input.GetButtonDown("Fire1"))
        {
            Debug.Log("button detected and interactable is: "+interactable);
            if (interactable != null) interactable.Interact(gameObject);
        }


        // drawing
        if (ray)
        {
            rayDistActual = hit.distance;
            if (interactable != null) color = Color.green;
            else color = Color.red;
        }
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * rayDistActual, color);
    }
}
