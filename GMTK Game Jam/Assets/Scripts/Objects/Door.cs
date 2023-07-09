using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour, IInteractable
{
    public string keyColor;
    public Color hueShift;
    public AudioClip openSFX;
    public AudioClip lockedSFX;
    AudioSource audioSource;
    private bool startedOpening = false;
    public GameObject leftDoor;
    public GameObject rightDoor;
    public float openAngle;
    private float startTime;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // TODO change hue
    }

    public bool UsableWithObj(GameObject obj)
    {
        if (obj.GetComponent<Key>() != null) return true;
        return false;
    }

    public void Interact(GameObject source, GameObject obj=null)
    {
        if (startedOpening) return;
        InteractionController player = source.GetComponent<InteractionController>();
        Key key = null;
        if (!player.emptyHanded) key = obj.GetComponent<Key>();
        if (player.emptyHanded || key==null || key.keyColor != keyColor)
        {
            audioSource.PlayOneShot(lockedSFX);
            return;
        }
        else
        {
            key.Open();
            Open();
        }
    }

    void Update()
    {
        if (startedOpening)
        {
            float currentAngle = openAngle * ((Time.time - startTime) / openSFX.length);
            currentAngle = Mathf.Clamp(currentAngle, 0, openAngle);
            leftDoor.transform.localEulerAngles = new Vector3(0, -currentAngle, 0);
            rightDoor.transform.localEulerAngles = new Vector3(0, currentAngle, 0);
        }
    }

    void Open()
    {
        startedOpening = true;
        audioSource.PlayOneShot(openSFX);
        startTime = Time.time;
        // TODO animation stuff
        // fade out
        // get vector to player to calc door direction
        Destroy(gameObject, openSFX.length);
    }
}
