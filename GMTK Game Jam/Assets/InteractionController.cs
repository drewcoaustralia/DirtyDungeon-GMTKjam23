using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public float rayDist = 2f;
    private IInteractable interactable;
    [SerializeField] private bool emptyHanded;

    void Awake()
    {
        emptyHanded = true;
        interactable = null;
    }

    void FixedUpdate()
    {
        if (!emptyHanded)
        {
            // do stuff with held object only
            if (Input.GetButtonDown("Fire1"))
            {
                if (interactable != null) interactable.Interact(gameObject);
            }
            return;
        }
        RaycastHit hit;
        float rayDistActual = rayDist;
        Color color = Color.white;
        bool ray = Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, rayDist);
        interactable = ray ? hit.transform.gameObject.GetComponent<IInteractable>() : null;

        // do stuff
        if (Input.GetButtonDown("Fire1"))
        {
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

    public void Hold(Pickuppable obj)
    {
        if (obj != null)
        {
            emptyHanded = false;
            interactable = (IInteractable)obj;
        }
        else
        {
            emptyHanded = true;
            interactable = null;
        }
    }
}
