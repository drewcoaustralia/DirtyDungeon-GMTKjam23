using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Chest : MonoBehaviour, IInteractable
{
    public bool isOpen = false;
    private bool isMoving = false;
    public float openSpeed = 0.5f;
    public float openAngle = -47.555f;
    [SerializeField] private GameObject top;
    [SerializeField] private GameObject empty;
    [SerializeField] private GameObject full;
    [Min(1)]public int maxItems;
    public bool isFull;
    public List<GameObject> inventory;
    private Quaternion targetRotation;
    private float startTime = 0f;
    private Collider col;
    public AudioClip openSFX;
    public AudioClip closeSFX;
    public AudioClip dumpSFX;
    public AudioClip denySFX;
    AudioSource audioSource;

    void Awake()
    {
        col = GetComponent<Collider>();
        targetRotation = Quaternion.Euler(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        audioSource = GetComponent<AudioSource>();
        isFull = inventory.Count >= maxItems;
        empty.SetActive(!isFull);
        full.SetActive(isFull);
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
        audioSource.PlayOneShot(openSFX);
        targetRotation = Quaternion.Euler(openAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);
        gameObject.layer = LayerMask.NameToLayer("OpenChest");
    }

    void Close()
    {
        audioSource.PlayOneShot(closeSFX);
        targetRotation = Quaternion.Euler(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        gameObject.layer = LayerMask.NameToLayer("Environment");
        //TODO ADD LOGIC TO EAT OR PUSH AWAY COLLIDING OBJECTS FIRST
    }

    public void Add(GameObject obj)
    {
        if (isFull || !isOpen)
        {
            audioSource.PlayOneShot(denySFX);
            return;
        }
        audioSource.PlayOneShot(dumpSFX);
        obj.GetComponent<Pickuppable>().PutInChest(gameObject);
        inventory.Add(obj);
        isFull = inventory.Count >= maxItems;
        if (isFull)
        {
            empty.SetActive(!isFull);
            full.SetActive(isFull);
        }
    }
}
