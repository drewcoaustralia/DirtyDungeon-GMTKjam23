using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickuppable : MonoBehaviour, IInteractable
{
    private Collider col;
    private Rigidbody rb;
    [SerializeField] private bool pickedUp = false;
    private GameObject src;
    public float holdDist = 1f;

    void Awake()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
    }

    public void Interact(GameObject source)
    {
        src = source;
        if (!pickedUp) Pickup();
        else TryPutDown();
    }

    void Pickup()
    {
        pickedUp = true;
        gameObject.layer = LayerMask.NameToLayer("HeldObject");
        // rb.isKinematic = true;
        rb.useGravity = false;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        col.enabled = false;
        src.GetComponent<InteractionController>().Hold(this);
        transform.SetParent(src.transform, false);
        transform.localPosition = Vector3.forward * holdDist;
        transform.rotation = src.transform.rotation;
    }

    void TryPutDown()
    {
        //if cond
        PutDown();
    }

    void PutDown()
    {
        Debug.Log("Putting down!");
        pickedUp = false;
        gameObject.layer = LayerMask.NameToLayer("Environment");
        // rb.isKinematic = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        col.enabled = true;
        src.GetComponent<InteractionController>().Hold(null);
        transform.SetParent(null, true);
    }

    public void PutInChest(GameObject chest)
    {
        transform.SetParent(chest.transform, true);
        //TODO Add animation/slerp
        transform.position = chest.transform.position;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        Debug.Log("Putting in chest!");
        pickedUp = false;
        gameObject.layer = LayerMask.NameToLayer("Environment");
        // rb.isKinematic = false;
        // rb.useGravity = true;
        // rb.constraints = RigidbodyConstraints.None;
        col.enabled = false;
    }
}
