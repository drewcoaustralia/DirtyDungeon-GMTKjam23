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
    public List<AudioClip> openSFX;
    public List<AudioClip> closeSFX;
    public List<AudioClip> dumpSFX;
    public List<AudioClip> fullSFX;
    public float openDuration;
    public float closeDuration;
    public float dumpDuration;
    public float fullDuration;
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

    public bool UsableWithObj(GameObject obj)
    {
        if (obj.GetComponent<Coin>() != null) return true;
        return false;
    }

    public void Interact(GameObject source, GameObject obj=null)
    {
        if (obj==null)
        {
            isMoving = true;
            startTime = Time.time;
            if (isOpen) Close();
            else Open();
        }
        else Add(obj);
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
        if (openSFX.Count != 0)
        {
            int idx = Random.Range (0, openSFX.Count);
            audioSource.PlayOneShot(openSFX[idx]);
            openDuration = openSFX[idx].length;
        }
        targetRotation = Quaternion.Euler(openAngle, transform.localEulerAngles.y, transform.localEulerAngles.z);
        gameObject.layer = LayerMask.NameToLayer("OpenChest");
    }

    void Close()
    {
        if (closeSFX.Count != 0)
        {
            int idx = Random.Range (0, closeSFX.Count);
            audioSource.PlayOneShot(closeSFX[idx]);
            closeDuration = closeSFX[idx].length;
        }
        targetRotation = Quaternion.Euler(0, transform.localEulerAngles.y, transform.localEulerAngles.z);
        gameObject.layer = LayerMask.NameToLayer("Environment");
        //TODO ADD LOGIC TO EAT OR PUSH AWAY COLLIDING OBJECTS FIRST
    }

    public void Add(GameObject obj)
    {
        if (isFull || !isOpen)
        {
            if (fullSFX.Count != 0)
            {
                int idx = Random.Range (0, fullSFX.Count);
                audioSource.PlayOneShot(fullSFX[idx]);
                fullDuration = fullSFX[idx].length;
            }
            return;
        }
        if (dumpSFX.Count != 0)
        {
            int idx = Random.Range (0, dumpSFX.Count);
            audioSource.PlayOneShot(dumpSFX[idx]);
            dumpDuration = dumpSFX[idx].length;
        }
        obj.GetComponent<Pickuppable>().PutInChest(gameObject);
        inventory.Add(obj);
        isFull = inventory.Count >= maxItems;
        if (isFull)
        {
            empty.SetActive(!isFull);
            full.SetActive(isFull);
            if (TryGetComponent(out TaskInstance taskInstance)) {
                taskInstance.Complete();
            }
        }
    }
}
