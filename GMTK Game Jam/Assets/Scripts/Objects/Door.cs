using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour, IInteractable
{
    public string keyColor;
    public AudioClip openSFX;
    public AudioClip lockedSFX;
    AudioSource audioSource;
    private bool startedOpening = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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

    void Open()
    {
        startedOpening = true;
        audioSource.PlayOneShot(openSFX);
        //animation stuff
        Destroy(gameObject, openSFX.length);
    }
}
