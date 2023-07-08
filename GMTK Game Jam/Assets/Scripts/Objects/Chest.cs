using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    private bool isMoving = false;
    public float openSpeed = 0.5f;
    [SerializeField] private GameObject top;
    private Quaternion targetRotation = Quaternion.Euler(0, 0, 0);
    private float startTime = 0f;
    private Collider col;

    void Awake()
    {
        col = GetComponent<Collider>();
    }

    public void Interact(GameObject source)
    {
        isMoving = true;
        startTime = Time.time;
        if (isOpen) Close();
        else Open();
    }

    void Update()
    {
        if (isMoving)
        {
            top.transform.rotation = Quaternion.Slerp(top.transform.rotation, targetRotation, (Time.time - startTime)/openSpeed);
            if (top.transform.rotation == targetRotation)
            {
                isOpen = !isOpen;
                isMoving = false;
            }
        }
    }

    void Open()
    {
        targetRotation = Quaternion.Euler(-30, 0, 0);
        gameObject.layer = LayerMask.NameToLayer("OpenChest");
    }

    void Close()
    {
        targetRotation = Quaternion.Euler(0, 0, 0);
        gameObject.layer = LayerMask.NameToLayer("Environment");
        //TODO ADD LOGIC TO EAT OR PUSH AWAY COLLIDING OBJECTS FIRST
    }

    public void Add(GameObject obj)
    {
        Debug.Log("adding");
        obj.GetComponent<Pickuppable>().PutInChest(gameObject);
    }
}
