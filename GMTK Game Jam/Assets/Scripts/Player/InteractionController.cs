using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    public float rayDist = 2f;
    private IInteractable interactable;
    [SerializeField] private bool emptyHanded;
    private Pickuppable objInHands = null;
    public Transform raycastSource;

    void Awake()
    {
        emptyHanded = true;
        interactable = null;
        if (raycastSource == null) raycastSource = transform;
    }

    void FixedUpdate()
    {
        RaycastHit hit;
        float rayDistActual = rayDist;
        Color color = Color.white;
        int layerMask =~ LayerMask.GetMask("HeldObject");
        bool ray = Physics.Raycast(raycastSource.position, raycastSource.TransformDirection(Vector3.forward), out hit, rayDist, layerMask);
        interactable = ray ? hit.transform.gameObject.GetComponent<IInteractable>() : null;

        // do stuff
        if (Input.GetButtonDown("Fire1"))
        {
            if (!emptyHanded)
            {
                if (LookingAtChest())
                {
                    ((Chest)interactable).Add(objInHands.gameObject);
                }
                else objInHands.Interact(gameObject);
            }
            else if (interactable != null) interactable.Interact(gameObject);
        }


        // drawing
        if (ray)
        {
            rayDistActual = hit.distance;
            if (interactable != null) color = Color.green;
            else color = Color.red;
        }
        Debug.DrawRay(raycastSource.position, raycastSource.TransformDirection(Vector3.forward) * rayDistActual, color);
    }

    public bool LookingAtChest()
    {
        if (interactable != null && ((MonoBehaviour)interactable).gameObject.tag == "chest") return true;
        return false;
    }

    public void Hold(Pickuppable obj)
    {
        if (obj != null)
        {
            emptyHanded = false;
            objInHands = obj;
        }
        else
        {
            emptyHanded = true;
            objInHands = null;
        }
    }
}
