using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Pickuppable : MonoBehaviour, IInteractable
{
    private Collider col;
    private Rigidbody rb;
    [SerializeField] private bool pickedUp = false;
    private GameObject src;
    public float holdDist = 1f;
    public bool animated = true;
    public float spinSpeed = 75f;
    public float floatDur = 1.25f;
    public float heightChange = .25f;
    private float startTime = Mathf.Infinity;
    private Vector3 bottom;
    private Vector3 top;
    private bool movingUp = true;
    public List<AudioClip> pickupSFX;
    private float pickupDuration;
    public List<AudioClip> collisionSFX;
    private float collisionDuration;
    AudioSource audioSource;
    public float throwForce = 100f;
    private bool shatterable;
    private bool notYetAnimated = true;

    void Awake()
    {
        col = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        if (gameObject.GetComponent<Shatter>() != null) gameObject.GetComponent<Shatter>().enabled=false;
    }

    public bool UsableWithObj(GameObject obj)
    {
        return false;
    }

    void Update()
    {
        if (animated && startTime != Mathf.Infinity)
        {
            if (movingUp) transform.position = Vector3.Lerp(bottom, top, (Time.time - startTime) / floatDur);
            else transform.position = Vector3.Lerp(top, bottom, (Time.time - startTime) / floatDur);
            if (Time.time - startTime >= floatDur)
            {
                startTime = Time.time;
                movingUp = !movingUp;
            }
            transform.Rotate(0,spinSpeed*Time.deltaTime,0);
        }
    }

    public void Interact(GameObject source, GameObject obj=null)
    {
        src = source;
        if (!pickedUp) Pickup();
        else TryPutDown();
    }

    void Pickup()
    {
        StopAnimation();
        if (pickupSFX.Count != 0)
        {
            int idx = Random.Range (0, pickupSFX.Count);
            audioSource.PlayOneShot(pickupSFX[idx]);
            pickupDuration = pickupSFX[idx].length;
        }
        pickedUp = true;
        gameObject.layer = LayerMask.NameToLayer("HeldObject");
        rb.isKinematic = true;
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

    public void PutDown()
    {
        pickedUp = false;
        gameObject.layer = LayerMask.NameToLayer("Environment");
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        col.enabled = true;
        src.GetComponent<InteractionController>().Hold(null);
        transform.SetParent(null, true);
        rb.AddForce((transform.up+transform.forward) * throwForce); // ADD RANDOM ROTATION
    }

    public void PutInChest(GameObject chest)
    {
        transform.SetParent(chest.transform, true);
        //TODO Add animation/slerp
        transform.position = chest.transform.position;
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        pickedUp = false;
        gameObject.layer = LayerMask.NameToLayer("Environment");
        // rb.isKinematic = false;
        // rb.useGravity = true;
        // rb.constraints = RigidbodyConstraints.None;
        col.enabled = false;
        src.GetComponent<InteractionController>().Hold(null);
        gameObject.SetActive(false);
    }

    void StartAnimation()
    {
        notYetAnimated = false;
        rb.isKinematic = true; //for spinning
        startTime = Time.deltaTime;
        bottom = transform.position;
        top = bottom + new Vector3 (0, heightChange, 0);
        if (gameObject.GetComponent<Shatter>() != null)
        {
            shatterable = true;
            gameObject.GetComponent<Shatter>().enabled=true;
        }
    }

    void StopAnimation()
    {
        rb.isKinematic = false; //for spinning
        startTime = Mathf.Infinity;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collisionSFX.Count != 0 && !notYetAnimated && collision.gameObject.layer == LayerMask.NameToLayer("Environment"))
        {
            int idx = Random.Range (0, collisionSFX.Count);
            audioSource.PlayOneShot(collisionSFX[idx]);
            collisionDuration = collisionSFX[idx].length;
        }
        if (animated && collision.gameObject.tag == "floor" && notYetAnimated && !shatterable)
        {
            StartAnimation();
        }
    }
}
