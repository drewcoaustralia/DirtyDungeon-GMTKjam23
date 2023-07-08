using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickuppable : MonoBehaviour, IInteractable
{
    private Collider _col;
    [SerializeField] private bool pickedUp = false;
    private GameObject src;
    public float holdDist = 1f;

    void Awake()
    {
        _col = GetComponent<Collider>();
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
        src.GetComponent<InteractionController>().Hold(null);
        transform.SetParent(null, true);
    }
}
