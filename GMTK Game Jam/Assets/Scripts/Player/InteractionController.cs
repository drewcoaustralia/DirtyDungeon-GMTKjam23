using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    private IInteractable interactable;
    private bool pickupAhead;
    private bool broomAhead;
    private bool broomInHands;
    public bool emptyHanded { get; private set; }
    public Pickuppable objInHands { get; private set; }
    public Transform raycastSource;
    public float rayDist = 2f;
    public Vector3 boxCastSize;
    private bool rayHit = false;
    public Animator anim;

    void Awake()
    {
        objInHands = null;
        emptyHanded = true;
        interactable = null;
        if (raycastSource == null) raycastSource = transform;
        pickupAhead = false;
    }

    void Update()
    {
        RaycastHit hit;
        float rayDistActual = rayDist;
        Color color = Color.white;
        int layerMask =~ LayerMask.GetMask("HeldObject");
        rayHit = Physics.BoxCast(raycastSource.position, boxCastSize/2, raycastSource.TransformDirection(Vector3.forward), out hit, Quaternion.identity, rayDist, layerMask);
        // rayHit = Physics.Raycast(raycastSource.position, raycastSource.TransformDirection(Vector3.forward), out hit, rayDist, layerMask);
        interactable = rayHit ? hit.transform.gameObject.GetComponent<IInteractable>() : null;
        if (rayHit && hit.transform.gameObject.GetComponent<Pickuppable>() != null)
        {
            pickupAhead = true;
            if (hit.transform.gameObject.GetComponent<Broom>() != null)
                broomAhead = true;
            else
                broomAhead = false;
        }
        else
        {
            pickupAhead = false;
            broomAhead = false;
        }

        if (objInHands == null) emptyHanded = true;

        // do stuff
        if (Input.GetButtonDown("Fire1"))
        {
            if (emptyHanded)
            {
                if (interactable != null)
                {
                    //check if pickup
                    if (pickupAhead)
                    {
                        anim.SetBool("isHolding", true);
                        anim.SetTrigger("pickup");
                        if (broomAhead)
                        {
                            anim.SetBool("isSweeping", true);
                            broomInHands = true;
                        }
                    }
                    else
                    {
                        anim.SetTrigger("interact");
                    }
                    interactable.Interact(gameObject);
                }
            }
            else // object in hands
            {
                if (interactable==null)
                {
                    // putting down object
                    anim.SetBool("isHolding", false);
                    anim.SetBool("isSweeping", false);
                    broomInHands = false;
                    anim.SetTrigger("putdown");
                    objInHands.Interact(gameObject);
                }
                else if (interactable.UsableWithObj(objInHands.gameObject))
                {
                    anim.SetBool("isHolding", false);
                    anim.SetTrigger("interact");
                    interactable.Interact(gameObject,objInHands.gameObject);
                }
            }
        }

        // drawing
        if (rayHit)
        {
            Debug.Log("Raycasting with: "+hit.transform.gameObject);
            rayDistActual = hit.distance;
            if (interactable != null) color = Color.green;
            else color = Color.red;
        }
        else Debug.Log("Raycasting with: NONE");
        Debug.DrawRay(raycastSource.position, raycastSource.TransformDirection(Vector3.forward) * rayDistActual, color);
    }


    void OnDrawGizmos()
    {
        DrawBoxLines(raycastSource.position, raycastSource.position+raycastSource.TransformDirection(Vector3.forward*rayDist), boxCastSize, true);
    }

    void DrawBoxLines(Vector3 p1, Vector3 p2, Vector3 extents, bool boxes = false)
    {
        Gizmos.color = rayHit ? Color.green : Color.white;
        var length = (p2 - p1).magnitude;
        var halfExtents = extents / 2;
        var halfExtentsZ = transform.forward*halfExtents.z;
        var halfExtentsY = transform.up*halfExtents.y;
        var halfExtentsX = transform.right*halfExtents.x;
        if (boxes)
        {
            var matrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(p1, transform.rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, extents);
            Gizmos.matrix = Matrix4x4.TRS(p2, transform.rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, extents);
            Gizmos.matrix = matrix;
        }
        // draw connect lines 1
        Gizmos.DrawLine(p1 - halfExtentsX - halfExtentsY - halfExtentsZ, p2 - halfExtentsX - halfExtentsY - halfExtentsZ);
        Gizmos.DrawLine(p1 + halfExtentsX - halfExtentsY - halfExtentsZ, p2 + halfExtentsX - halfExtentsY - halfExtentsZ);
        Gizmos.DrawLine(p1 - halfExtentsX + halfExtentsY - halfExtentsZ, p2 - halfExtentsX + halfExtentsY - halfExtentsZ);
        Gizmos.DrawLine(p1 + halfExtentsX + halfExtentsY - halfExtentsZ, p2 + halfExtentsX + halfExtentsY - halfExtentsZ);
        // draw connect lines 2
        Gizmos.DrawLine(p1 - halfExtentsX - halfExtentsY + halfExtentsZ, p2 - halfExtentsX - halfExtentsY + halfExtentsZ);
        Gizmos.DrawLine(p1 + halfExtentsX - halfExtentsY + halfExtentsZ, p2 + halfExtentsX - halfExtentsY + halfExtentsZ);
        Gizmos.DrawLine(p1 - halfExtentsX + halfExtentsY + halfExtentsZ, p2 - halfExtentsX + halfExtentsY + halfExtentsZ);
        Gizmos.DrawLine(p1 + halfExtentsX + halfExtentsY + halfExtentsZ, p2 + halfExtentsX + halfExtentsY + halfExtentsZ);
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
